using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Nodes.Dapper;
using Nodes.Entities;

namespace Nodes.DBHelper
{
    /// <summary>
    /// 退货单数据访问类
    /// </summary>
    public class ReturnManageDal
    {
        private const string SELECT_BILL_BODY = @"SELECT A.BILL_ID, A.BILL_NO, A.BILL_STATE, A.WH_CODE, A.BILL_TYPE, A.CREATOR, 
                C1.ITEM_DESC BILL_TYPE_NAME,  C2.ITEM_DESC STATUS_NAME, u.USER_NAME, A.ORIGINAL_BILL_NO, A.PRINTED, 
                A.SALES_MAN, A.CONTRACT_NO, A.S_CODE, S.C_NAME, A.RETURN_REASON, A.RETURN_REMARK, C3.ITEM_DESC REASON_DESC, 
                A.SHIP_NO, A.REMARK, A.WMS_REMARK, A.ROW_COLOR, A.CREATE_DATE, A.CLOSE_DATE, A.CRN_AMOUNT, 
                W.WH_NAME FROM_WH_NAME, A.RETURN_AMOUNT, A.RETURN_DATE, A.RETURN_DRIVER, A.HANDING_PERSON, A.SENTORDER_NO  
                FROM WM_CRN_HEADER A 
                LEFT JOIN CUSTOMERS S ON A.S_CODE = S.C_CODE 
                INNER JOIN WM_BASE_CODE C1 ON A.BILL_TYPE = C1.ITEM_VALUE 
                INNER JOIN WM_BASE_CODE C2 ON A.BILL_STATE = C2.ITEM_VALUE 
                LEFT JOIN WM_BASE_CODE C3 ON A.RETURN_REASON = C3.ITEM_VALUE
                LEFT JOIN WM_WAREHOUSE W ON A.WH_CODE = W.WH_CODE
                LEFT JOIN users u ON u.USER_CODE = A.CREATOR ";

        public ReturnHeaderEntity GetHeaderInfoByBillID(int billID)
        {
            string sql = SELECT_BILL_BODY + "WHERE A.BILL_ID = @BillID";

            IMapper map = DatabaseInstance.Instance();
            return map.QuerySingle<ReturnHeaderEntity>(sql, new { BillID = billID });
        }

        /// <summary>
        /// 按照库房、收货方式、状态（是小于某个状态）的单据
        /// </summary>
        /// <param name="warehouse"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public List<ReturnHeaderEntity> QueryBillsQuickly(string outboundType, DateTime? dateFrom, DateTime? dateTo)
        {
            string whereCondition = string.Format("WHERE A.BILL_STATE < '{0}'", BaseCodeConstant.ASN_STATE_CODE_COMPLETE);
            
            if (dateFrom != null)
                whereCondition += string.Format(" AND A.CREATE_DATE >= '{0}'", dateFrom.Value);

            if (dateTo != null)
                whereCondition += string.Format(" AND A.CREATE_DATE <= '{0}'", dateTo.Value);

            string sql = SELECT_BILL_BODY + whereCondition;
            IMapper map = DatabaseInstance.Instance();
            return map.Query<ReturnHeaderEntity>(sql);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="billNO"></param>
        /// <param name="customer"></param>
        /// <param name="saleMan"></param>
        /// <param name="billType"></param>
        /// <param name="billStatus"></param>
        /// <param name="outboundType"></param>
        /// <param name="shipNO"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="warehouse"></param>
        /// <returns></returns>
        public List<ReturnHeaderEntity> QueryBills(string billNO, string customer, string saleMan, string itemDesc,
            string billStatus, string returnDriver, string shipNO, DateTime dateFrom, DateTime dateTo)
        {
            string sql = SELECT_BILL_BODY +
                "WHERE (@BillNO IS NULL OR A.BILL_NO = @BillNO) AND  " +
                "(@Customer IS NULL OR S.C_NAME like '%" + customer + "%') AND " +
                "(@SalesMan IS NULL OR A.SALES_MAN like '%" + saleMan + "%') AND " +
                "(@ItemDesc IS NULL OR A.BILL_ID IN (SELECT wcd.BILL_ID FROM wm_crn_detail wcd JOIN wm_sku ws ON wcd.SKU_CODE = ws.SKU_CODE WHERE ws.SKU_NAME LIKE '%" + itemDesc + "%')) AND " +
                "(@BillStatus IS NULL OR C2.ITEM_DESC = @BillStatus) AND " +
                "(@ReturnDriver IS NULL OR A.RETURN_DRIVER like '%" + returnDriver + "%') AND " +
                "(@StartTime IS NULL OR A.CREATE_DATE >= @StartTime) AND " +
                "(@EndTime IS NULL OR A.CREATE_DATE <= @EndTime)";

            if (!string.IsNullOrEmpty(billStatus))
            {
                sql = string.Format(SELECT_BILL_BODY +
                "WHERE (@BillNO IS NULL OR A.BILL_NO = @BillNO) AND " +
                "(@Customer IS NULL OR S.C_NAME like '%" + customer + "%') AND " +
                "(@SalesMan IS NULL OR A.SALES_MAN like '%" + saleMan + "%') AND " +
                "(@ItemDesc IS NULL OR A.BILL_ID IN (SELECT wcd.BILL_ID FROM wm_crn_detail wcd JOIN wm_sku ws ON wcd.SKU_CODE = ws.SKU_CODE WHERE ws.SKU_NAME LIKE '%" + itemDesc + "%')) AND " +
                "(A.BILL_STATE in ({0})) AND " +
                "(@ReturnDriver IS NULL OR A.RETURN_DRIVER like '%" + returnDriver + "%') AND " +
                "(@StartTime IS NULL OR A.CREATE_DATE >= @StartTime) AND " +
                "(@EndTime IS NULL OR A.CREATE_DATE <= @EndTime)", DBUtil.FormatParameter(billStatus));
            }

            IMapper map = DatabaseInstance.Instance();
            return map.Query<ReturnHeaderEntity>(sql,
                new
                {
                    BillNO = billNO,
                    Customer = customer,
                    SalesMan = saleMan,
                    ItemDesc = itemDesc,
                    BillStatus = billStatus,
                    ReturnDriver = returnDriver,
                    StartTime = dateFrom,
                    EndTime = dateTo
                });
        }

        public void UpdatePrintedFlag(int billID)
        {
            IMapper map = DatabaseInstance.Instance();
            map.Execute("UPDATE WM_CRN_HEADER SET PRINTED = PRINTED + 1,LAST_UPDATETIME=NOW(), PRINTED_TIME = NOW() WHERE BILL_ID = @BillID;", new { BillID = billID });
        }

        public int ModifyReturnAmount(int billID, decimal crnAmount)
        {
            IMapper map = DatabaseInstance.Instance();
            return map.Execute("UPDATE WM_CRN_HEADER SET CRN_AMOUNT = @CrnAmount, LAST_UPDATETIME=NOW() WHERE BILL_ID = @BillID;", new { CrnAmount = crnAmount, BillID = billID });
        }

        /// <summary>
        /// 删除一张退货单据及其明细
        /// </summary>
        public int DeleteReturnBill(int headerID)
        {
            string sqlDetail = "DELETE FROM wm_crn_detail WHERE BILL_ID = @HeaderID";
            IMapper map = DatabaseInstance.Instance();
            IDbTransaction trans = map.BeginTransaction();
            int rtn = map.Execute(sqlDetail, new { HeaderID = headerID });
            if (rtn < 0)
            {
                trans.Rollback();
                return rtn;
            }
            string sqlHeader = "DELETE FROM WM_CRN_HEADER WHERE BILL_ID = @HeaderID";
            rtn = map.Execute(sqlHeader, new { HeaderID = headerID });
            if (rtn < 0)
            {
                trans.Rollback();
                return rtn;
            }
            else
            {
                trans.Commit();
            }
            return rtn;
        }

        /// <summary>
        /// 获取退货明细
        /// </summary>
        public List<ReturnDetailsEntity> GetReturnDetails(int headerID)
        {
            string sql = @"SELECT D.ID, D.BILL_ID, D.ROW_NO, D.SKU_CODE, wus.SKU_BARCODE, ws.SKU_NAME, D.COM_MATERIAL, wsd.IS_CASE, D.RETURN_REASON,D.SALEORDER_NO,
                    D.QTY, D.SPEC, D.UM_CODE, wu.UM_NAME, D.EXP_DATE, D.BATCH_NO, D.PRICE, D.REMARK,wus.QTY  AS CAST_RATE, 
                     wus.UM_CODE AS MIN_UM_CODE, wu.UM_NAME AS MIN_UM_NAME, IFNULL(wsd.SUIT_NUM, 1) SUIT_NUM, 
                    D.RETURN_QTY, IFNULL(D.PUT_QTY, 0) AS MIN_PUT_QTY, IFNULL(D.CHECK_QTY, 0) as MIN_CHECK_QTY,wsd.PICK_QTY
--                     ,  D.QTY * IFNULL(vwuc.QTY, 1) as HAS_PICK_QTY
                      , IFNULL((SELECT SUM(wcd.RETURN_QTY) FROM wm_crn_detail wcd 
                                WHERE wcd.ID <> D.ID AND wcd.BILL_ID <> D.BILL_ID AND wcd.SKU_CODE = D.SKU_CODE AND wcd.SO_ID = D.SO_ID), 0) AS RETURNED_QTY
                    FROM wm_crn_detail D 
                    LEFT JOIN wm_so_header wsh ON D.SALEORDER_NO =wsh.BILL_NO
                    LEFT JOIN wm_so_detail wsd ON wsd.BILL_ID = wsh.BILL_ID AND D.SKU_CODE =wsd.SKU_CODE
                    LEFT JOIN wm_um wu ON wu.UM_CODE = D.UM_CODE
                    LEFT JOIN wm_um_sku wus ON D.SKU_CODE =wus.SKU_CODE AND wus.SKU_LEVEL='1'
                    LEFT JOIN wm_sku ws ON wus.SKU_CODE =ws.SKU_CODE
--                     LEFT JOIN v_wm_unit_cast wuc ON wuc.MIN_UM_CODE = D.RETURN_UNITCODE AND wuc.SKU_CODE = D.SKU_CODE and wuc.S_UNIT = '0' and wuc.QTY = 1  
--                     left join v_wm_unit_cast vwuc on vwuc.UM_CODE = D.UM_CODE AND vwuc.SKU_CODE = D.SKU_CODE AND vwuc.QTY > 1 
                    WHERE D.BILL_ID = @HeaderID
                    GROUP BY D.ID ";
            IMapper map = DatabaseInstance.Instance();
            return map.Query<ReturnDetailsEntity>(sql, new { HeaderID = headerID });
        }

        //根据订单ID返回订单号
        public string GetReturnBillNo(int billID)
        {
            string sql = @"SELECT wch.BILL_NO FROM wm_crn_header wch WHERE wch.BILL_ID = @BillID";

            IMapper map = DatabaseInstance.Instance();
            return map.ExecuteScalar<string>(sql, new { BillID = billID });
        }

        #region 2015-06-11
        public List<ReturnHeaderEntity> GetReturnBill(string cusCode)
        {
            string sql = SELECT_BILL_BODY + String.Format(" where A.S_CODE='{0}' and a.PRINTED=0 ;", cusCode);
            IMapper map = DatabaseInstance.Instance();
            return map.Query<ReturnHeaderEntity>(sql);
        }
        /// <summary>
        /// 打印设置
        /// </summary>
        /// <param name="billID">退货单ID</param>
        /// <param name="BillNO">发货单编号</param>
        public void UpdatePrintedFlag(int billID,string billNO,string shipNO)
        {
            IMapper map = DatabaseInstance.Instance();
            map.Execute("UPDATE WM_CRN_HEADER SET PRINTED = PRINTED + 1,LAST_UPDATETIME=NOW(), PRINTED_TIME = NOW(),SENTORDER_NO=@BillNO,SHIP_NO=@ShipNO WHERE BILL_ID = @BillID;", new { BillNO = billNO, BillID = billID, ShipNO = shipNO });
        }
        #endregion

        public DataTable GetRelatingStackInfo(int billID)
        {
            string sql = @"SELECT wac.CT_CODE, wbc.ITEM_DESC, ws.SKU_NAME, vwuc.UM_NAME, wac.QTY, wac.PRODUCT_DATE,
  U.USER_NAME CREATOR, wac.CHECK_NAME
  FROM wm_asn_container wac
  INNER JOIN wm_crn_header wch ON wch.BILL_ID = wac.BILL_ID
  INNER JOIN wm_container_state wcs ON wcs.CT_CODE = wac.CT_CODE
  INNER JOIN wm_base_code wbc ON wbc.ITEM_VALUE = wcs.CT_STATE
  INNER JOIN v_wm_unit_cast vwuc ON vwuc.UM_ID = wac.UM_SKU_ID AND vwuc.QTY = 1
  INNER JOIN wm_sku ws ON ws.SKU_CODE = wac.SKU_CODE
  LEFT JOIN customers c ON c.C_CODE = wch.S_CODE
  LEFT JOIN USERS U ON U.USER_CODE = wac.CREATOR
  WHERE wac.BILL_ID = {0}";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(string.Format(sql, billID));
        }

        public string CloseReturn(int billID)
        {
            IMapper map = DatabaseInstance.Instance();
            DynamicParameters p = new DynamicParameters();
            p.Add("V_BILLID", billID);
            p.AddOut("V_RESULT", DbType.String);
            map.Execute("P_CRN_CLOSE", p, CommandType.StoredProcedure);
            return p.Get<string>("V_RESULT");
        }

    }
}
