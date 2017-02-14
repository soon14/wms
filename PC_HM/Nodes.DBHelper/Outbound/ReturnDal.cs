using System;
using System.Collections.Generic;
using System.Data;
using Nodes.Dapper;
using Nodes.Entities;

namespace Nodes.DBHelper
{
    public class ReturnDal
    {
        ///<summary>
        ///查询所有计量单位
        ///</summary>
        ///<returns></returns>
        public List<UnitEntity> GetAllUnitBySku(string skuCode)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = @"SELECT UM_CODE, UM_NAME FROM v_wm_unit_cast WHERE sku_code =  @SkuCode GROUP BY UM_CODE, UM_NAME ";
            return map.Query<UnitEntity>(sql, new { SkuCode = skuCode });
        }

        /// <summary>
        /// 根据条件查询包装单位转换率
        /// </summary>
        /// <param name="skuCode"></param>
        /// <param name="skuBarcode"></param>
        /// <param name="unitCode"></param>
        /// <param name="minUnitCode"></param>
        /// <returns></returns>
        public decimal GetCastRateBySku(string skuCode, string skuBarcode, string unitCode, string minUnitCode)
        {
            string sql = @"SELECT IFNULL(cast_rate,1) FROM v_wm_unit_cast 
                WHERE sku_code = @SkuCode AND sku_barcode = @SkuBarcode AND um_code = @UmCode AND min_um_code = @MinUnitCode";

            IMapper map = DatabaseInstance.Instance();
            return map.ExecuteScalar<decimal>(sql, new { SkuCode = skuCode, SkuBarcode = skuBarcode, UmCode = unitCode, MinUnitCode = minUnitCode });
        }

        /// <summary>
        /// 根据产品编码和产品条码获取最小包装单位的编码
        /// </summary>
        /// <param name="skuCode"></param>
        /// <param name="skuBarcode"></param>
        /// <returns></returns>
        public string GetMinUnitCodeBySku(string skuCode, string skuBarcode)
        {
            string sql = @"SELECT um_code FROM v_wm_unit_base WHERE sku_barcode = @SkuBarcode AND sku_code = @SkuCode GROUP BY um_code";

            IMapper map = DatabaseInstance.Instance();
            return map.ExecuteScalar<string>(sql, new { SkuBarcode = skuBarcode, SkuCode = skuCode });
        }

        /// <summary>
        /// 退货时获取销售单明细
        /// </summary>
        /// <param name="billID">单据号</param>
        /// <returns></returns>
        public List<ReturnDetailEntity> GetReturnDetails(int billID)
        {
            string sql = @"SELECT D.ID, D.BILL_ID, D.ROW_NO, D.SKU_CODE, wuc.SKU_NAME, wuc.SKU_BARCODE, IFNULL(D.SUIT_NUM, 1) SUIT_NUM, D.SPEC,
                      D.COM_MATERIAL, D.QTY, D.UM_CODE, wu.UM_NAME, D.DUE_DATE, D.BATCH_NO, D.PRICE, D.REMARK, D.PICK_QTY, D.IS_CASE,
                      (SELECT IFNULL(SUM(wcd.RETURN_QTY),0) FROM wm_crn_detail wcd 
                        LEFT JOIN v_wm_unit_cast wuc1 ON wuc1.UM_CODE = wcd.UM_CODE AND wuc1.SKU_CODE = wcd.SKU_CODE
                        WHERE  wcd.SO_ID = D.ID) AS RETURNED_QTY, IFNULL(vwuc.QTY, 1) AS CAST_RATE, 
                       IFNULL(wuc.MIN_UM_CODE, wu.UM_CODE) AS MIN_UM_CODE, IFNULL(wuc.MIN_UM_NAME, wu.UM_NAME) AS MIN_UM_NAME, 
                      (IFNULL(vwuc.QTY, 1) * D.QTY) AS MIN_SO_QTY, (IFNULL(D.PICK_QTY, 1) * IFNULL(vwuc.QTY, 1)) AS MIN_PICK_QTY
                      FROM WM_SO_DETAIL D
                       INNER JOIN wm_um wu ON wu.UM_CODE = D.UM_CODE
                       LEFT JOIN v_wm_unit_cast wuc ON wuc.SKU_CODE = D.SKU_CODE AND wuc.S_UNIT = '0' AND wuc.QTY = 1 
                       LEFT JOIN v_wm_unit_cast vwuc ON vwuc.SKU_CODE = D.SKU_CODE  AND vwuc.UM_CODE = D.UM_CODE AND vwuc.QTY > 1 " +
                    "WHERE D.BILL_ID = @BillID";
            IMapper map = DatabaseInstance.Instance();
            return map.Query<ReturnDetailEntity>(sql, new { BillID = billID });
        }

        /// <summary>
        /// 保存手工创建的退货单
        /// </summary>
        /// <param name="bill"></param>
        /// <param name="creator">zxq</param>
        /// <returns>0：更新表头时失败；1：成功</returns>
        public string SaveReturnBill(SOHeaderEntity soHeader, List<ReturnDetailEntity> lstBill, string whCode, DateTime retDate, string retDriver,
            string handPerson, string remark, decimal retAmount, string creator, string returnReason, string returnRemark, decimal crnAmount, out string errMsg)
        {
            errMsg = string.Empty;

            IMapper map = DatabaseInstance.Instance();
            IDbTransaction trans = map.BeginTransaction();

            try
            {
                DynamicParameters parms = new DynamicParameters();
                parms.Add("V_SO_BILL_ID", soHeader.BillID);
                parms.Add("V_BILL_NO", "T0" + whCode + soHeader.BillNO);
                parms.Add("V_RETURN_AMOUNT", retAmount);
                parms.Add("V_CRN_AMOUNT", crnAmount);
                parms.Add("V_RETURN_DATE", retDate);
                parms.Add("V_RETURN_DRIVER", retDriver);
                parms.Add("V_HAND_PERSON", handPerson);
                parms.Add("V_WMSREMARK", remark);
                parms.Add("V_USER_CODE", creator);
                parms.Add("V_RETURN_REASON", returnReason);
                parms.Add("V_RETURN_REMARK", returnRemark);
                parms.AddOut("V_NEW_ID", DbType.Int32, 11);
                parms.AddOut("V_RESULT", DbType.String, 2);

                //先写入主表
                map.Execute("P_CRN_SAVE_HEADER", parms, trans, CommandType.StoredProcedure);

                //获取返回值，只有1表示成功
                string retVal = parms.Get<string>("V_RESULT");
                if (retVal != "1")
                {
                    trans.Rollback();
                    return retVal;
                }
                int newID = parms.Get<Int32>("V_NEW_ID");
                //保存明细
                parms = new DynamicParameters();
                parms.Add("V_SO_ID");
                parms.Add("V_HEADER_ID");
                parms.Add("V_QTY");
                parms.Add("V_UM_CODE");
                parms.AddOut("V_RESULT", DbType.String, 2);

                //再写明细
                foreach (ReturnDetailEntity line in lstBill)
                {
                    //退货数量是0的不记录进退货明细
                    if (line.ReturnQty == 0) continue;
                    parms.Set("V_SO_ID", line.SoDetailID);
                    parms.Set("V_HEADER_ID", newID);
                    parms.Set("V_QTY", line.ReturnQty);
                    parms.Set("V_UM_CODE", line.ReturnUnitCode);

                    map.Execute("P_CRN_SAVE_DETAIL", parms, trans, CommandType.StoredProcedure);
                    retVal = parms.Get<string>("V_RESULT");
                    if (retVal != "1")
                    {
                        if (retVal == "11")
                            errMsg = line.MaterialCode;

                        trans.Rollback();
                        break;
                    }
                }

                if (retVal == "1")
                {
                    trans.Commit();
                }

                return retVal;
            }
            catch (Exception ex) //只能是insert语句产生了异常，先回滚再抛出异常信息
            {
                trans.Rollback();
                throw ex;
            }
        }

        public int UpdateReturnDetails(List<ReturnDetailsEntity> lstBill)
        {
            int rtn = 0;
            string sql = @"update wm_crn_detail a set a.RETURN_QTY = {0}, a.LAST_UPDATETIME = NOW() where a.ID = {1} ";
            IMapper map = DatabaseInstance.Instance();
            foreach (ReturnDetailsEntity itm in lstBill)
            {
                rtn += map.Execute(string.Format(sql, itm.ReturnQty, itm.BillID));
            }
            return rtn;
        }

        /// <summary>
        ///  根据销售单获取发货车牌号和司机
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public string GetVhicleInfo(int billID, out string DriverName)
        {
            string sql = @"SELECT IFNULL(wv.VH_NO,'') , u.user_name 
                          FROM wm_so_header wsh JOIN wm_vehicle wv ON wsh.SHIP_NO=wv.ID 
                          left join users u on u.user_code = wv.user_code 
                           WHERE wsh.BILL_ID = @BillID ";
            IMapper map = DatabaseInstance.Instance();
            DataTable dt = new DataTable();
            dt = map.LoadTable(sql, new { BillID = billID });
            string vehicleNo = "";
            DriverName = "";
            if (dt.Rows.Count > 0)
            {
                vehicleNo = dt.Rows[0][0].ToString().Trim();
                DriverName = dt.Rows[0][1].ToString().Trim();
            }
            return vehicleNo;
        }


        #region 彭伟 2015-08-04
        /// <summary>
        /// 获取生成拣货任务时，“采购退货单”库足不足的订单信息
        /// </summary>
        /// <returns></returns>
        public static List<SOHeaderEntity> GetBillsByPickError()
        {
            string sql = "SELECT H.BILL_ID, H.BILL_NO, H.BILL_STATE, H.WH_CODE, H.PICK_ZN_TYPE,H.BILL_TYPE, H.OUTSTORE_TYPE, " +
                "H.SALES_MAN, H.CONTRACT_NO, H.SHIP_NO, H.C_CODE, H.CLOSE_DATE, H.CREATE_DATE, H.ORIGINAL_BILL_NO, " +
                "H.PRINTED, H.REMARK, H.WMS_REMARK, H.ROW_COLOR, H.IS_DELETED, H.LAST_UPDATETIME, H.FROM_WH_CODE, " +
                "H.SYNC_STATE, H.CONFIRM_DATE, H.RECEIVE_AMOUNT, H.REAL_AMOUNT, H.CRN_AMOUNT, H.OTHER_AMOUNT, " +
                "H.CONFIRM_FLAG, H.PAYED_AMOUNT, H.OTHER_REMARK, H.PAYMENT_DATE, H.PAYMENT_BY, H.PAYMENT_FLAG, " +
                "H.DELAYMARK, H.PAY_METHOD FROM WM_PICK_TEMP_ERROR PTE " +
                "INNER JOIN WM_SO_HEADER H ON H.BILL_ID = PTE.BILL_ID " +
                "WHERE H.BILL_TYPE = 124 " +
                "GROUP BY PTE.BILL_ID ";
            IMapper map = DatabaseInstance.Instance();
            return map.Query<SOHeaderEntity>(sql);
        }
        #endregion

        public static DataTable GetCrnRecords(DateTime dateBegin, DateTime dateEnd, string userCode)
        {
            string sql = "SELECT H.BILL_NO, C.SKU_CODE, WS.SKU_NAME, C.QTY CHECK_QTY, WU.UM_NAME, " +
                "C.CREATE_DATE, C.CHECK_STATE " +
                "FROM WM_CRN_HEADER H " +
                "LEFT JOIN WM_ASN_CONTAINER C ON C.BILL_ID = H.BILL_ID " +
                "LEFT JOIN WM_UM_SKU WUS ON WUS.ID = C.UM_SKU_ID " +
                "LEFT JOIN WM_UM WU ON WU.UM_CODE = WUS.UM_CODE " +
                "LEFT JOIN WM_SKU WS ON WS.SKU_CODE = C.SKU_CODE " +
                "WHERE C.CREATOR = @UserCode AND C.CREATE_DATE > @DateBegin AND C.CREATE_DATE <= @DateEnd";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { UserCode = userCode, DateBegin = dateBegin, DateEnd = dateEnd });
        }
    }
}
