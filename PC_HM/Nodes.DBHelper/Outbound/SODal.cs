using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Nodes.Dapper;
using Nodes.Entities;
using System.Text;
using Nodes.Utils;

namespace Nodes.DBHelper
{
    /// <summary>
    /// 到货通知单数据访问类
    /// </summary>
    public class SODal
    {
        private const string SELECT_BILL_BODY = "SELECT A.BILL_ID, A.BILL_NO,A.WH_CODE, A.FROM_WH_CODE, A.BILL_TYPE, " +
                "C1.ITEM_DESC BILL_TYPE_NAME, A.BILL_STATE, C2.ITEM_DESC STATUS_NAME, A.OUTSTORE_TYPE, C3.ITEM_DESC OUTSTORE_TYPE_NAME, " +
                "A.SALES_MAN, A.CONTRACT_NO, A.C_CODE, S.C_NAME, S.ADDRESS, S.CONTACT, S.PHONE, " +
                "A.SHIP_NO, A.REMARK, A.WMS_REMARK, A.ROW_COLOR, A.CREATE_DATE, A.CLOSE_DATE,A.CONFIRM_DATE, " +
                "W.WH_NAME FROM_WH_NAME,WW.WH_NAME WH_NAME, A.PICK_ZN_TYPE, C4.ITEM_DESC PICK_ZN_TYPE_NAME, A.RECEIVE_AMOUNT, " +
                "A.REAL_AMOUNT,A.CRN_AMOUNT,A.OTHER_AMOUNT,A.CONFIRM_FLAG ,ifnull(A.PAYED_AMOUNT,0) PAYED_AMOUNT, " +
                "A.DELAYMARK, A.PAY_METHOD,A.PRINTED,A.PRINTED_TIME, u.MOBILE_PHONE, u.USER_NAME, A.CANCEL_FLAG " +
                "FROM WM_SO_HEADER A " +
                "LEFT JOIN CUSTOMERS S ON A.C_CODE = S.C_CODE " +
                "INNER JOIN WM_BASE_CODE C1 ON A.BILL_TYPE = C1.ITEM_VALUE " +
                "INNER JOIN WM_BASE_CODE C2 ON A.BILL_STATE = C2.ITEM_VALUE " +
                "INNER JOIN WM_BASE_CODE C3 ON A.OUTSTORE_TYPE = C3.ITEM_VALUE " +
                "INNER JOIN WM_BASE_CODE C4 ON A.PICK_ZN_TYPE = C4.ITEM_VALUE " +
                "LEFT JOIN wm_vehicle wv ON A.SHIP_NO = wv.ID " +
                "LEFT JOIN users u ON u.USER_CODE = wv.USER_CODE " +
                "LEFT JOIN WM_WAREHOUSE W ON A.FROM_WH_CODE = W.WH_CODE " +
                "LEFT JOIN WM_WAREHOUSE WW ON A.WH_CODE = WW.WH_CODE ";

        /// <summary>
        /// 按照库房、收货方式、状态（是小于某个状态）的单据
        /// </summary>
        /// <param name="warehouse"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public List<SOHeaderEntity> QueryBillsQuickly(string outboundType, DateTime? dateFrom, DateTime? dateTo)
        {
            string whereCondition = string.Format("WHERE A.BILL_STATE <= '{0}'", BaseCodeConstant.SO_STATUS_CLOSE);
            whereCondition += " OR A.BILL_STATE = '691' ";
            if (!string.IsNullOrEmpty(outboundType))
                whereCondition += string.Format(" AND A.OUTSTORE_TYPE = '{0}'", outboundType);

            if (dateFrom != null)
                whereCondition += string.Format(" AND A.CREATE_DATE >= '{0}'", dateFrom.Value);

            if (dateTo != null)
                whereCondition += string.Format(" AND A.CREATE_DATE <= '{0}'", dateTo.Value);

            string sql = SELECT_BILL_BODY + whereCondition + " ORDER BY A.BILL_STATE ASC";
            IMapper map = DatabaseInstance.Instance();
            return map.Query<SOHeaderEntity>(sql);
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
        /// <param name="material"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="warehouse"></param>
        /// <returns></returns>
        public List<SOHeaderEntity> QueryBills(string billNO, string customer, string saleMan, string billType,
            string billStatus, string outboundType, string material, DateTime dateFrom, DateTime dateTo)
        {
            string sql = SELECT_BILL_BODY + " WHERE 1=1 ";
            if (!string.IsNullOrEmpty(billNO))
                sql += string.Format(" AND A.BILL_NO = '{0}' ", billNO);
            if (!string.IsNullOrEmpty(billType))
                sql += string.Format(" AND A.BILL_TYPE = '{0}' ", billType);
            if (!string.IsNullOrEmpty(outboundType))
                sql += string.Format(" AND A.OUTSTORE_TYPE = '{0}' ", outboundType);
            if (!string.IsNullOrEmpty(saleMan))
                sql += string.Format(" AND A.SALES_MAN = '{0}' ", saleMan);
            if (!string.IsNullOrEmpty(customer))
                sql += string.Format(" AND S.C_NAME LIKE '%{0}%' ", customer);
            if (!string.IsNullOrEmpty(material))
                sql += string.Format(@" AND A.BILL_ID IN(SELECT D.BILL_ID FROM WM_SO_DETAIL D
  INNER JOIN WM_SKU WS ON WS.SKU_CODE = D.SKU_CODE AND (WS.SKU_CODE LIKE '%{0}%' OR WS.SKU_NAME LIKE '%{0}%')) ", material);
            if (!string.IsNullOrEmpty(billStatus))
                sql += string.Format(" AND (A.BILL_STATE in ({0})) ", billStatus);

            sql += @" AND (@StartTime IS NULL OR A.CREATE_DATE >= @StartTime) AND (@EndTime IS NULL OR A.CREATE_DATE <= @EndTime) ORDER BY A.BILL_STATE ASC";


            IMapper map = DatabaseInstance.Instance();
            return map.Query<SOHeaderEntity>(sql,
                new
                {
                    StartTime = dateFrom,
                    EndTime = dateTo
                });
        }

        /// <summary>
        /// 修改单据的备注（含备注和背景色）
        /// </summary>
        /// <param name="remark"></param>
        /// <param name="colorArgb"></param>
        public void UpdateWmsRemark(int billID, string remark, int? colorArgb)
        {
            IMapper map = DatabaseInstance.Instance();
            map.Execute("UPDATE WM_SO_HEADER SET WMS_REMARK = @Remark, ROW_COLOR = @Color WHERE BILL_ID = @BillID",
                new { Remark = remark, Color = colorArgb, BillID = billID });
        }

        //public void UpdatePrintedFlag(int billID)
        //{
        //    IMapper map = DatabaseInstance.Instance();
        //    map.Execute("UPDATE WM_SO_HEADER SET PRINTED = 1,BILL_STATE='68',SYNC_STATE=6,LAST_UPDATETIME=NOW(), CLOSE_DATE = NOW() WHERE BILL_ID = @BillID;", new { BillID = billID });
        //}
        public void UpdatePrintedFlag(int billID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("V_BILLID", billID);


            IMapper map = DatabaseInstance.Instance();
            map.Execute("P_SO_PRINT", param, CommandType.StoredProcedure);
        }
        public void UpdatePrintedFlag(int billID, string creator)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("V_BILLID", billID);
            param.Add("V_CREATOR", creator);


            IMapper map = DatabaseInstance.Instance();
            map.Execute("P_SO_PRINT", param, CommandType.StoredProcedure);
        }

        public string AutoAssignTask()
        {
            DynamicParameters param = new DynamicParameters();
            param.AddOut("V_RESULT", DbType.String);

            IMapper map = DatabaseInstance.Instance();
            map.Execute("P_SO_AUTO_TASK", param, 60 * 60, CommandType.StoredProcedure);
            return param.Get<string>("V_RESULT");
        }

        /// <summary>
        /// 根据订单状态查询，支持多状态情况（用逗号隔开），例如status可以是100901，也可以是100901,100902或'100901','100902'
        /// </summary>
        /// <param name="status"></param>
        /// <param name="warehouse"></param>
        /// <returns></returns>
        public List<SOHeaderEntity> QueryBillsByStatus(string status, int setting)
        {
            status = DBUtil.FormatParameter(status);
            string sql = string.Format(@"SELECT A.BILL_ID, A.BILL_NO, A.FROM_WH_CODE, A.BILL_TYPE, 
                          C1.ITEM_DESC BILL_TYPE_NAME, A.BILL_STATE, C2.ITEM_DESC STATUS_NAME, A.OUTSTORE_TYPE, 
                          C3.ITEM_DESC OUTSTORE_TYPE_NAME, (CASE WHEN OS.VEHICLE_NO IS NULL THEN TDM.GROUP_NO ELSE OS.VEHICLE_NO END) VEHICLE_NO, 
                          A.SALES_MAN, A.CONTRACT_NO, A.C_CODE, S.C_NAME, S.ADDRESS, S.CONTACT, S.PHONE, 
                          A.SHIP_NO, A.REMARK, A.WMS_REMARK, A.ROW_COLOR, A.CREATE_DATE, A.CLOSE_DATE, 
                          W.WH_NAME FROM_WH_NAME, A.PICK_ZN_TYPE, C4.ITEM_DESC PICK_ZN_TYPE_NAME, A.DELAYMARK
                          FROM WM_SO_HEADER A 
                          LEFT JOIN TMS_DATA_DETAIL TDD ON TDD.BILL_NO = A.BILL_NO
                          LEFT JOIN TMS_DATA_MARKET TDM ON TDM.MARKET_ID = TDD.MARKET_ID
                          LEFT JOIN CUSTOMERS S ON A.C_CODE = S.C_CODE 
                          INNER JOIN WM_BASE_CODE C1 ON A.BILL_TYPE = C1.ITEM_VALUE 
                          INNER JOIN WM_BASE_CODE C2 ON A.BILL_STATE = C2.ITEM_VALUE 
                          INNER JOIN WM_BASE_CODE C3 ON A.OUTSTORE_TYPE = C3.ITEM_VALUE 
                          INNER JOIN WM_BASE_CODE C4 ON A.PICK_ZN_TYPE = C4.ITEM_VALUE 
                          LEFT JOIN WM_WAREHOUSE W ON A.FROM_WH_CODE = W.WH_CODE 
                          LEFT JOIN WM_ORDER_SORT OS ON OS.BILL_NO = A.BILL_NO " +
                "WHERE {0} " +
                "GROUP BY A.BILL_ID ",
                setting == 1 ? "A.BILL_STATE <= 61 AND A.BILL_NO IN (SELECT BILL_NO FROM TMS_DATA_DETAIL)" : DBUtil.FormatParameter("A.BILL_STATE", status));

            IMapper map = DatabaseInstance.Instance();
            return map.Query<SOHeaderEntity>(sql);
        }

        public List<SOHeaderEntity> QueryAllCaseBill(int setting)
        {
            string sql = string.Format(@"SELECT A.BILL_ID, A.BILL_NO, A.FROM_WH_CODE, A.BILL_TYPE, D.CASE_STR,
  C1.ITEM_DESC BILL_TYPE_NAME, A.BILL_STATE, C2.ITEM_DESC STATUS_NAME, A.OUTSTORE_TYPE, 
  C3.ITEM_DESC OUTSTORE_TYPE_NAME, (CASE WHEN OS.VEHICLE_NO IS NULL THEN TDM.GROUP_NO ELSE OS.VEHICLE_NO END) VEHICLE_NO, 
  A.SALES_MAN, A.CONTRACT_NO, A.C_CODE, S.C_NAME, S.ADDRESS, S.CONTACT, S.PHONE, 
  A.SHIP_NO, A.REMARK, A.WMS_REMARK, A.ROW_COLOR, A.CREATE_DATE, A.CLOSE_DATE, 
  W.WH_NAME FROM_WH_NAME, A.PICK_ZN_TYPE, C4.ITEM_DESC PICK_ZN_TYPE_NAME, A.DELAYMARK
  FROM WM_SO_HEADER A 
  INNER JOIN (SELECT D.BILL_ID, GROUP_CONCAT(D.IS_CASE) CASE_STR FROM WM_SO_DETAIL D
                INNER JOIN WM_SO_HEADER H ON H.BILL_ID = D.BILL_ID AND H.BILL_STATE = '61'
                GROUP BY D.BILL_ID) D ON A.BILL_ID = D.BILL_ID AND D.CASE_STR NOT LIKE '%2%' AND D.CASE_STR NOT LIKE '%3%'
  LEFT JOIN TMS_DATA_DETAIL TDD ON TDD.BILL_NO = A.BILL_NO
  LEFT JOIN TMS_DATA_MARKET TDM ON TDM.MARKET_ID = TDD.MARKET_ID
  LEFT JOIN WM_SO_GROUP G ON G.BILL_ID = A.BILL_ID 
  LEFT JOIN CUSTOMERS S ON A.C_CODE = S.C_CODE 
  INNER JOIN WM_BASE_CODE C1 ON A.BILL_TYPE = C1.ITEM_VALUE 
  INNER JOIN WM_BASE_CODE C2 ON A.BILL_STATE = C2.ITEM_VALUE 
  INNER JOIN WM_BASE_CODE C3 ON A.OUTSTORE_TYPE = C3.ITEM_VALUE 
  INNER JOIN WM_BASE_CODE C4 ON A.PICK_ZN_TYPE = C4.ITEM_VALUE 
  LEFT JOIN WM_WAREHOUSE W ON A.FROM_WH_CODE = W.WH_CODE 
  LEFT JOIN WM_ORDER_SORT OS ON OS.BILL_NO = A.BILL_NO " +
                "WHERE {0} " +
                "GROUP BY A.BILL_ID " +
                "ORDER BY G.ID ASC",
                setting == 1 ? "A.BILL_STATE <= 61 AND A.BILL_NO IN (SELECT BILL_NO FROM TMS_DATA_DETAIL)" : "A.BILL_STATE = '61' ");

            IMapper map = DatabaseInstance.Instance();
            return map.Query<SOHeaderEntity>(sql);
        }

        public List<SOHeaderEntity> QueryBillsByStatusUnUse(string status)
        {
            status = DBUtil.FormatParameter(status);
            string sql = string.Format("SELECT A.BILL_ID, A.BILL_NO, A.FROM_WH_CODE, A.BILL_TYPE, " +
                "C1.ITEM_DESC BILL_TYPE_NAME, A.BILL_STATE, C2.ITEM_DESC STATUS_NAME, A.OUTSTORE_TYPE, C3.ITEM_DESC OUTSTORE_TYPE_NAME, " +
                "A.SALES_MAN, A.CONTRACT_NO, A.C_CODE, S.C_NAME, S.ADDRESS, S.CONTACT, S.PHONE, " +
                "A.SHIP_NO, A.REMARK, A.WMS_REMARK, A.ROW_COLOR, A.CREATE_DATE, A.CLOSE_DATE, " +
                "W.WH_NAME FROM_WH_NAME, A.PICK_ZN_TYPE, C4.ITEM_DESC PICK_ZN_TYPE_NAME, A.DELAYMARK " +
                "FROM WM_SO_GROUP G " +
                "INNER JOIN WM_SO_HEADER A ON G.BILL_ID = A.BILL_ID " +
                "LEFT JOIN CUSTOMERS S ON A.C_CODE = S.C_CODE " +
                "INNER JOIN WM_BASE_CODE C1 ON A.BILL_TYPE = C1.ITEM_VALUE " +
                "INNER JOIN WM_BASE_CODE C2 ON A.BILL_STATE = C2.ITEM_VALUE " +
                "INNER JOIN WM_BASE_CODE C3 ON A.OUTSTORE_TYPE = C3.ITEM_VALUE " +
                "INNER JOIN WM_BASE_CODE C4 ON A.PICK_ZN_TYPE = C4.ITEM_VALUE " +
                "LEFT JOIN WM_WAREHOUSE W ON A.FROM_WH_CODE = W.WH_CODE " +
                "WHERE {0} AND (A.SHIP_NO = '' OR IFNULL(A.SHIP_NO, 0) = 0)" +
                "GROUP BY A.BILL_ID " +
                "ORDER BY G.ID ASC", DBUtil.FormatParameter("A.BILL_STATE", status));

            IMapper map = DatabaseInstance.Instance();
            return map.Query<SOHeaderEntity>(sql);
        }

        public SOHeaderEntity GetHeaderInfoByBillID(int billID)
        {
            string sql = SELECT_BILL_BODY + "WHERE A.BILL_ID = @BillID";

            IMapper map = DatabaseInstance.Instance();
            return map.QuerySingle<SOHeaderEntity>(sql, new { BillID = billID });
        }
        /// <summary>
        ///获取订单ID
        /// </summary>
        /// <param name="vhNO"></param>
        /// <param name="tpye">1-装车信息；2-车次信息</param>
        /// <returns></returns>
        public List<SOHeaderEntity> GetHeaderInfoByBillNOS(string vhNO, int tpyeOpe)
        {
            string sql = "";
            if (tpyeOpe == 1)
            {
                sql = "SELECT A.BILL_ID FROM WM_SO_HEADER A "
                        + "WHERE EXISTS (SELECT B.BILL_NO FROM WM_LOADING_DETAIL B WHERE B.VH_TRAIN_NO=@vhNO AND A.BILL_NO=B.BILL_NO )";
            }
            else if (tpyeOpe == 2)
            {
                sql = "SELECT A.BILL_ID FROM WM_SO_HEADER A "
                        + "WHERE EXISTS (SELECT B.BILL_NO FROM WM_VEHICLE_TRAIN_DETAIL B WHERE B.VH_TRAIN_NO=@vhNO AND A.BILL_NO=B.BILL_NO )";
            }
            IMapper map = DatabaseInstance.Instance();
            return map.Query<SOHeaderEntity>(sql, new { vhNO = vhNO });
        }

        public SOHeaderEntity GetHeaderInfoByBillNO(string billNO)
        {
            string sql = SELECT_BILL_BODY + "WHERE A.BILL_NO = @BillNO";

            IMapper map = DatabaseInstance.Instance();
            return map.QuerySingle<SOHeaderEntity>(sql, new { BillNO = billNO });
        }

        /// <summary>
        /// 生成拣货计划，在内存中，并没有保存到数据库
        /// </summary>
        /// <param name="billIDs"></param>
        /// <param name="warehouse"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public void CreatePickPlan(string billIDs, out string tempID, out string errMsg)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("V_BILL_IDS", billIDs);
            param.AddOut("V_TEMP_ID", DbType.String);
            param.AddOut("V_ERR_MSG", DbType.String);

            IMapper map = DatabaseInstance.Instance();
            map.Execute("P_SO_CREATE_PICK_PLAN", param, 60 * 60, CommandType.StoredProcedure);
            tempID = param.Get<string>("V_TEMP_ID");
            errMsg = param.Get<string>("V_ERR_MSG");
        }
        /// <summary>
        /// 判断拣货计划是否已经生成！
        /// </summary>
        /// <param name="billIDs"></param>
        /// <returns></returns>
        public bool JudgeIsNext(string billIDs)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = String.Format(@"SELECT COUNT(1) FROM (SELECT * FROM WM_PICK_TEMP
                                          UNION
                                          SELECT * FROM WM_PICK_TEMP_ERROR) T
                                          WHERE T.BILL_ID IN ({0});", billIDs);
            int result = ConvertUtil.ToInt(map.ExecuteScalar<object>(sql));
            if (result <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<PickPlanEntity> GetTempPickResult(string tempID)
        {
            IMapper map = DatabaseInstance.Instance();

            string sql = string.Format("SELECT T.BILL_ID, H.BILL_NO, C.C_NAME, T.DETAIL_ID, T.STOCK_ID, T.IS_CASE, T.STOCK_ID, D.SKU_CODE, M.SKU_NAME, " +
                "D.COM_MATERIAL, M.UM_CODE, UM.UM_NAME, STK.LC_CODE, T.QTY, D.ROW_NO " +
                "FROM WM_PICK_TEMP T " +
                "INNER JOIN WM_SO_DETAIL D ON T.DETAIL_ID = D.ID " +
                "INNER JOIN WM_SO_HEADER H ON D.BILL_ID = H.BILL_ID " +
                "INNER JOIN WM_SKU M ON D.SKU_CODE = M.SKU_CODE " +
                "INNER JOIN WM_STOCK STK ON T.STOCK_ID = STK.ID " +
                "INNER JOIN WM_UM UM ON STK.UM_CODE = UM.UM_CODE " +
                "LEFT JOIN CUSTOMERS C ON H.C_CODE = C.C_CODE " +
                "WHERE T.G_ID = '{0}'", tempID);
            return map.Query<PickPlanEntity>(sql);
        }
        public void DeleteTempPickAll(string billIds)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = @"DELETE FROM WM_PICK_TEMP WHERE BILL_ID IN (@BillIds);
DELETE FROM WM_PICK_TEMP_ERROR WHERE BILL_ID IN (@BillIds);";
            map.Execute(sql);
        }

        /// <summary>
        /// 获取拣配没有通过的订单行
        /// </summary>
        /// <param name="tempID"></param>
        /// <returns></returns>
        public List<PickPlanEntity> GetTempPickResultError(string tempID)
        {
            IMapper map = DatabaseInstance.Instance();

            //string sql = string.Format("SELECT H.BILL_NO, C.C_NAME, T.QTY, T.STOCK_QTY, M.SKU_NAME, D.COM_MATERIAL " +
            //    "FROM WM_PICK_TEMP_ERROR T " +
            //    "INNER JOIN WM_SO_DETAIL D ON T.DETAIL_ID = D.ID " +
            //    "INNER JOIN WM_SO_HEADER H ON D.BILL_ID = H.BILL_ID " +
            //    "INNER JOIN WM_SKU M ON D.SKU_CODE = M.SKU_CODE " +
            //    "INNER JOIN CUSTOMERS C ON H.C_CODE = C.C_CODE " +
            //    "WHERE T.G_ID = '{0}'", tempID);
            string sql = string.Format(
                @"SELECT H.BILL_ID, H.BILL_NO, H.PICK_ZN_TYPE, C.C_NAME, T.QTY, T.STOCK_QTY, B.DisableQty, 
  A.DisableQty DisableQty2, M.SECURITY_QTY, D.SKU_CODE, M.SKU_NAME, D.COM_MATERIAL, U.UM_NAME, 
  SALE_UM.QTY SALE_QTY 
  FROM WM_PICK_TEMP_ERROR T 
  INNER JOIN WM_SO_DETAIL D ON T.DETAIL_ID = D.ID 
  INNER JOIN WM_UM_SKU US ON D.SKU_CODE = US.SKU_CODE AND US.SKU_LEVEL = 1 
  INNER JOIN WM_UM_SKU SALE_UM ON SALE_UM.UM_CODE = D.UM_CODE AND SALE_UM.SKU_CODE = D.SKU_CODE
  INNER JOIN WM_UM U ON US.UM_CODE = U.UM_CODE 
  INNER JOIN WM_SO_HEADER H ON D.BILL_ID = H.BILL_ID 
  INNER JOIN WM_SKU M ON D.SKU_CODE = M.SKU_CODE 
  LEFT JOIN CUSTOMERS C ON H.C_CODE = C.C_CODE 
  LEFT JOIN (SELECT S.SKU_CODE, SUM(S.QTY - S.PICKING_QTY - S.OCCUPY_QTY) DisableQty FROM WM_STOCK S
              INNER JOIN WM_LOCATION L ON S.LC_CODE = L.LC_CODE 
              INNER JOIN WM_ZONE Z ON L.ZN_CODE = Z.ZN_CODE 
              WHERE S.QTY > 0 AND Z.ZT_CODE = '71' AND L.IS_ACTIVE = 'Y' 
              GROUP BY S.SKU_CODE) B ON D.SKU_CODE = B.SKU_CODE 
  LEFT JOIN (SELECT S.SKU_CODE, SUM(S.QTY - S.PICKING_QTY - S.OCCUPY_QTY) DisableQty FROM WM_STOCK S
              INNER JOIN WM_LOCATION L ON S.LC_CODE = L.LC_CODE 
              INNER JOIN WM_ZONE Z ON L.ZN_CODE = Z.ZN_CODE 
              WHERE S.QTY > 0 AND Z.ZT_CODE = '73' AND L.IS_ACTIVE = 'Y' 
              GROUP BY S.SKU_CODE) A ON D.SKU_CODE = A.SKU_CODE  
  WHERE T.G_ID = '{0}'", tempID);
            return map.Query<PickPlanEntity>(sql);
        }

        public string SavePickPlan(List<PickPlanEntity> data, string userName, EWarehouseType whType)
        {
            string errMsg = string.Empty;

            //取出不重复的单据编号
            var billids = (from d in data select d.BillID).Distinct();
            
            int result = -1;
            bool hasDetail = false;
            IMapper map = DatabaseInstance.Instance();
            IDbTransaction trans = map.BeginTransaction();
            try
            {
                DynamicParameters parms = new DynamicParameters();
                parms.Add("V_BILL_ID");
                parms.Add("V_USER_NAME", userName);
                parms.AddOut("V_RESULT", DbType.Int32);

                DynamicParameters parmsDetail = new DynamicParameters();
                parmsDetail.Add("V_DETAIL_ID");
                parmsDetail.Add("V_STOCK_ID");
                parmsDetail.Add("V_QTY");
                parmsDetail.Add("V_IS_CASE");//新增字段
                parmsDetail.AddOut("V_RESULT", DbType.Int32);

                foreach (int billID in billids)
                {
                    //先更新单据状态
                    parms.Set("V_BILL_ID", billID);
                    map.Execute("P_SO_BEFORE_SAVE_PICK", parms, trans, CommandType.StoredProcedure);
                    result = parms.Get<int>("V_RESULT");
                    if (result == -1)
                    {
                        errMsg = string.Format("未找到单据“{0}”，可能已经被删除。", billID);

                        trans.Rollback();
                        break;
                    }
                    else if (result == -2)
                    {
                        errMsg = string.Format("单据“{0}”的状态不是“未拣配计算”，保存已取消。", billID);

                        trans.Rollback();
                        break;
                    }


                    //再循环每一拣配结果行
                    var details = from d in data where d.BillID == billID && d.STOCK_ID != 0 select d;
                    foreach (PickPlanEntity detail in details)
                    {
                        hasDetail = true;
                        parmsDetail.Set("V_DETAIL_ID", detail.DetailID);
                        parmsDetail.Set("V_STOCK_ID", detail.STOCK_ID);
                        parmsDetail.Set("V_QTY", detail.Qty);
                        parmsDetail.Set("V_IS_CASE", detail.IsCase);

                        map.Execute("P_SO_SAVE_PICK_PLAN", parmsDetail, trans, CommandType.StoredProcedure);
                        result = parmsDetail.Get<int>("V_RESULT");
                        if (result == -1)
                        {
                            errMsg = string.Format("单据“{0}”，商品“{1}”对应的库存不足。", detail.BillNO, detail.MaterialName);

                            trans.Rollback();
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        trans.Rollback();
                        break;
                    }
                    else
                    {
                        if (hasDetail)
                        {
                            // 插入任务表
                            TaskDal.CreateTask(billID, "143");
                        }
                    }
                    hasDetail = false;
                }

                if (string.IsNullOrEmpty(errMsg))
                    trans.Commit();
                // 验证保存的拣货计划是否存在漏品的现象
                foreach (int billID in billids)
                {
                    bool tempResult = false;
                    if (whType == EWarehouseType.混合仓 && (this.QueryDetailNotInPick(billID, 1) > 0 || this.QueryDetailNotInPick(billID, 2) > 0))
                        tempResult = true;
                    else if (whType == EWarehouseType.整货仓 && this.QueryDetailNotInPick(billID, 1) > 0)
                        tempResult = true;
                    if (tempResult)
                    {
                        PickPlanEntity entity = (from d in data where d.BillID == billID select d).First();
                        if (entity == null)
                            continue;
                        errMsg = string.Format("单据“{0}”保存拣货计划时漏品，现已还原订单；请重新生成。", entity.BillNO);
                        RestoreBill(entity.BillNO);
                    }
                }
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            return errMsg;
        }

        public string RestoreBill(string billNO)
        {
            string errMsg = string.Empty;
            DynamicParameters parms = new DynamicParameters();
            parms.Add("V_BILL_NO", billNO);
            parms.AddOut("V_MSG", DbType.String);
            IMapper map = DatabaseInstance.Instance();
            map.Execute("P_SO_RESTORE_BILL_11111111", parms, CommandType.StoredProcedure);

            return parms.Get<string>("V_MSG");
        }

        public int QueryDetailNotInPick(int billID, int isCase)
        {
            string sql = @"SELECT COUNT(1)
  FROM WM_SO_DETAIL D 
  LEFT JOIN WM_UM_SKU US ON US.SKU_CODE = D.SKU_CODE AND US.UM_CODE = D.UM_CODE
  LEFT JOIN (SELECT S.SKU_CODE, SUM(IFNULL(S.QTY, 0)) - SUM(IFNULL(S.PICKING_QTY, 0)) STOCK_QTY
              FROM WM_STOCK S
              INNER JOIN WM_LOCATION L ON L.LC_CODE = S.LC_CODE AND L.IS_ACTIVE = 'Y'
              LEFT JOIN WM_ZONE Z ON Z.ZN_CODE = L.ZN_CODE
              WHERE Z.ZT_CODE = (SELECT H.PICK_ZN_TYPE FROM WM_SO_HEADER H WHERE H.BILL_ID = @BillID) AND S.QTY > 0
              GROUP BY S.SKU_CODE) S ON S.SKU_CODE = D.SKU_CODE
  WHERE D.BILL_ID = @BillID 
  AND D.ID NOT IN (SELECT P.DETAIL_ID FROM WM_SO_PICK P WHERE P.BILL_ID = @BillID) 
  AND D.IS_CASE = @IsCase AND S.STOCK_QTY >= D.QTY * US.QTY";
            IMapper map = DatabaseInstance.Instance();
            return ConvertUtil.ToInt(map.ExecuteScalar<object>(sql, new { BillID = billID, IsCase = isCase }));
        }

        /// <summary>
        /// 将物流箱和发货单关联
        /// </summary>
        /// <param name="ctCode"></param>
        /// <param name="billID"></param>
        /// <returns></returns>
        public int JoinContainerBill(string ctCode, int billID, string userName)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("V_CT_CODE", ctCode);
            parms.Add("V_BILL_ID", billID);
            parms.Add("V_USER_NAME", userName);
            parms.AddOut("V_RESULT", DbType.Int32);

            IMapper map = DatabaseInstance.Instance();
            map.Execute("P_SO_CONTAINER_JOIN_BILL", parms, CommandType.StoredProcedure);

            return parms.Get<int>("V_RESULT");
        }

        /// <summary>
        /// 删除一张单据，同时删除拣配计算的结果，策略等
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="userName"></param>
        /// <returns>0: 成功；-1：未查到单据；-2：单据已开始拣货</returns>
        public int DeleteBill(int billID, string userName)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("BILL_ID", billID);
            parms.Add("USER_NAME", userName);
            parms.AddOut("RET_VAL", DbType.Int32);

            IMapper map = DatabaseInstance.Instance();
            map.Execute("P_SO_DELETE_BILL", parms, CommandType.StoredProcedure);

            return parms.Get<int>("RET_VAL");
        }

        /// <summary>
        /// 删除已有拣配计算的结果，必须是未开始拣货
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="creator"></param>
        /// <returns>0：成功；-1：单据未找到；-2：单据状态不对；-3：已开始拣货，无法删除</returns>
        public int DeletePickPlan(int billID, string userName)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("V_BILL_ID", billID);
            parms.Add("V_USER_NAME", userName);
            parms.AddOut("V_RESULT", DbType.Int32);

            IMapper map = DatabaseInstance.Instance();
            map.Execute("P_SO_DELETE_PICK_PLAN", parms, CommandType.StoredProcedure);

            return parms.Get<int>("V_RESULT");
        }

        public List<PickPlanEntity> GetPickPlan(int billID)
        {
            string sql = @"SELECT T.BILL_ID, H.BILL_NO, T.DETAIL_ID, T.STOCK_ID,T.CREATE_DATE, 
  D.SKU_CODE, M.SKU_NAME, 
  D.COM_MATERIAL, M.UM_CODE, UM.UM_NAME, SALE_US.QTY SALE_TRANS_VALUE, SALE_UM.UM_NAME SALE_UNIT, 
  (CASE WHEN STK.LC_CODE IS NULL THEN T.LC_CODE ELSE STK.LC_CODE END) LC_CODE, STK.EXP_DATE, T.QTY, US.SKU_BARCODE, D.ROW_NO 
  FROM WM_SO_PICK T 
  INNER JOIN WM_SO_DETAIL D ON T.DETAIL_ID = D.ID 
  INNER JOIN WM_SO_HEADER H ON D.BILL_ID = H.BILL_ID 
  INNER JOIN WM_SKU M ON D.SKU_CODE = M.SKU_CODE 
  LEFT JOIN WM_STOCK STK ON T.STOCK_ID = STK.ID 
  LEFT JOIN WM_UM_SKU US ON US.SKU_CODE = M.SKU_CODE AND US.SKU_LEVEL = 1
  LEFT JOIN WM_UM_SKU SALE_US ON SALE_US.SKU_CODE = D.SKU_CODE AND SALE_US.SKU_LEVEL = 1
  LEFT JOIN WM_UM UM ON UM.UM_CODE = US.UM_CODE
  LEFT JOIN WM_UM SALE_UM ON SALE_UM.UM_CODE = SALE_US.UM_CODE 
  WHERE T.BILL_ID = @BillID";
            IMapper map = DatabaseInstance.Instance();
            return map.Query<PickPlanEntity>(sql, new { BillID = billID });
        }

        /// <summary>
        /// 获取到货通知单明细信息
        /// </summary>
        /// <param name="billID">单据号</param>
        /// <returns>到货通知单明细信息集合</returns>
        public List<SODetailEntity> GetDetails(int billID)
        {
            string sql = @"SELECT D.ID DETAIL_ID, D.BILL_ID, D.ROW_NO, D.SKU_CODE, D.SPEC, M.SKU_NAME, WUS.SKU_BARCODE,ifnull(D.SUIT_NUM,1) SUIT_NUM, " +
                "D.COM_MATERIAL, D.QTY, D.UM_CODE, UM.UM_NAME, D.DUE_DATE, D.BATCH_NO, D.PRICE, D.REMARK, D.PICK_QTY, IS_CASE,D.ROW_NO " +
                "FROM WM_SO_DETAIL D " +
                "LEFT JOIN WM_SKU M ON D.SKU_CODE = M.SKU_CODE " +
                "LEFT JOIN WM_UM UM ON UM.UM_CODE = D.UM_CODE " +
                "LEFT JOIN WM_UM_SKU WUS ON D.SKU_CODE = WUS.SKU_CODE AND D.UM_CODE = WUS.UM_CODE " +
                "WHERE D.BILL_ID = @BillID";
            IMapper map = DatabaseInstance.Instance();
            return map.Query<SODetailEntity>(sql, new { BillID = billID });
        }
        public List<SODetailEntity> GetDetails(int billID, int isCase)
        {
            string sql = @"SELECT D.ID, D.BILL_ID, D.ROW_NO, D.SKU_CODE, D.SPEC, M.SKU_NAME, WUS.SKU_BARCODE,ifnull(D.SUIT_NUM,1) SUIT_NUM, " +
                "D.COM_MATERIAL, D.QTY, D.UM_CODE, UM.UM_NAME, D.DUE_DATE, D.BATCH_NO, D.PRICE, D.REMARK, D.PICK_QTY, IS_CASE,D.ROW_NO " +
                "FROM WM_SO_DETAIL D " +
                "INNER JOIN WM_SKU M ON D.SKU_CODE = M.SKU_CODE " +
                "INNER JOIN WM_UM UM ON UM.UM_CODE = D.UM_CODE " +
                "LEFT JOIN WM_UM_SKU WUS ON D.SKU_CODE = WUS.SKU_CODE AND D.UM_CODE = WUS.UM_CODE " +
                "WHERE D.BILL_ID = @BillID ";
            if (isCase == 1)
            {
                sql += " AND D.IS_CASE = 1 ";
            }
            else if (isCase == 2)
            {
                sql += " AND D.IS_CASE = 2 ";
            }
            IMapper map = DatabaseInstance.Instance();
            return map.Query<SODetailEntity>(sql, new { BillID = billID });
        }

        public List<SODetailReportEntity> GetDetailsForPrint(int billID, int type)
        {
            List<SODetailReportEntity> pd = new List<SODetailReportEntity>();
            SODetailReportEntity oneRow = null;
            List<SODetailEntity> details = GetDetails(billID);
            foreach (SODetailEntity d in details)
            {
                oneRow = new SODetailReportEntity();
                oneRow = GetReportDetail(d);
                if (string.IsNullOrEmpty(d.CombMaterial) || type == 1)
                {
                    oneRow.SkuTypeName = "商品";
                    if (type == 0)
                        oneRow.SkuCombName = d.ProductName;
                    else if (type == 1)
                        oneRow.SkuCombName = d.MaterialName;
                    oneRow.SkuType = "1";
                    pd.Add(oneRow);
                }
                else if (type == 0)
                {
                    SODetailReportEntity ddd = pd.Find(s => s.CombMaterial == d.CombMaterial && s.RowNO == d.RowNO);
                    if (ddd == null)
                    {
                        oneRow.SkuCombName = d.ProductName;
                        if (d.RowNO != 0)
                        {
                            oneRow.SkuTypeName = "套装";
                            oneRow.SkuType = "2";
                        }
                        else
                        {
                            oneRow.SkuTypeName = "积分兑换";
                            oneRow.SkuType = "3";
                        }
                        oneRow.Spec = "";
                        oneRow.RowNO = d.RowNO;
                        pd.Add(oneRow);
                    }
                    else
                    {
                        ddd.SkuCombName += " + " + d.ProductName;
                        // 对比谁是套餐中最小套餐量
                        decimal value1 = ddd.Qty == 0 || ddd.SuitNum == 0 ? 0.00m : ddd.PickQty / (ddd.Qty / ddd.SuitNum);
                        decimal value2 = d.Qty == 0 || d.SuitNum == 0 ? 0.00m : d.PickQty / (d.Qty / d.SuitNum);
                        if (value1 > value2)
                        {
                            ddd.PickQty = d.PickQty;
                            ddd.Qty = d.Qty;
                            ddd.SuitNum = d.SuitNum;
                        }
                    }
                }
            }
            return pd;
        }

        public SODetailReportEntity GetReportDetail(SODetailEntity detailEntity)
        {
            SODetailReportEntity entity = new SODetailReportEntity();
            entity.DetailID = detailEntity.DetailID;
            entity.DueDate = detailEntity.DueDate;
            entity.BillID = detailEntity.BillID;
            entity.BatchNO = detailEntity.BatchNO;
            entity.MaterialCode = detailEntity.MaterialCode;
            entity.PickQty = detailEntity.PickQty;
            entity.Spec = detailEntity.Spec;
            entity.UnitName = detailEntity.UnitName;
            entity.Price1 = detailEntity.Price1;
            entity.CombMaterial = detailEntity.CombMaterial;
            entity.MaterialName = detailEntity.MaterialName;
            entity.SuitNum = detailEntity.SuitNum;
            entity.Qty = detailEntity.Qty;
            return entity;
        }

        /// <summary>
        /// 是否做过拣货策略
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public bool HasStrategyData(int billID)
        {
            string sql = @"SELECT D.BILL_ID FROM SO_STRATEGY D WHERE D.BILL_ID = @BillID";
            IMapper map = DatabaseInstance.Instance();
            object _billID = map.ExecuteScalar<object>(sql, new { BillID = billID });
            if (_billID == null)
                return false;
            else
                return true;
        }

        public List<SODetailEntity> GetStrategyData(int billID)
        {
            string sql = @"SELECT D.BILL_ID, D.DETAIL_ID, D.ROW_NO, D.MATERIAL, M.NAM MATERIAL_NAME, " +
                "D.COM_MATERIAL, M.UNIT, D.QTY, M.DUE_DATE, M.BATCH_NO, M.STAT, M.LOCATION, D.PRICE, D.REMARK " +
                "FROM SO_STRATEGY S " +
                "INNER JOIN SO_DETAIL D on S.DETAIL_ID = S.DETAIL_ID " +
                "INNER JOIN MATERIAL M ON D.MATERIAL = M.COD " +
                "WHERE D.BILL_ID = @BillID";
            IMapper map = DatabaseInstance.Instance();
            return map.Query<SODetailEntity>(sql, new { BillID = billID });
        }

        /// <summary>
        /// 保存发货方式及拣货区域
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="pickType"></param>
        /// <param name="znType"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public int SaveStrategy(int billID, string pickType, string znType, string userCode)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("V_BILL_ID", billID);
            parms.Add("V_OUTSTORE_TYPE", pickType);
            parms.Add("V_ZN_TYPE", znType);
            parms.Add("V_USER_CODE", userCode);
            parms.AddOut("V_RESULT", DbType.Int32);
            IMapper map = DatabaseInstance.Instance();
            return map.Execute("P_SO_CHANGE_PICK_TYPE", parms, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 关闭单据，并清除任务
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="userName"></param>
        public void CloseBill(int billID, string userName)
        {
            IMapper map = DatabaseInstance.Instance();
            map.Execute("P_SO_CLOSE_BILL", new { V_BILL_ID = billID, V_USER_NAME = userName }, CommandType.StoredProcedure);
        }


        public DataTable GetDetailTableWithoutSummary(int billID)
        {
            string sql = @"SELECT D.BILL_ID, D.DETAIL_ID, D.ROW_NO, D.MATERIAL, M.NAM MATERIAL_NAME, " +
                "D.COM_MATERIAL, M.UNIT, D.QTY, D.DUE_DATE, D.BATCH_NO, D.PRICE, D.REMARK, D.LOCATION, D.STAT " +
                "FROM SO_DETAIL D " +
                "INNER JOIN MATERIAL M ON D.MATERIAL = M.COD " +
                "WHERE D.BILL_ID = @BillID";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { BillID = billID });
        }

        public SOHeaderEntity GetBillStatus(int billID)
        {
            string sql = @"SELECT H.BILL_STATE, C.ITEM_DESC STATUS_NAME FROM WM_SO_HEADER H INNER JOIN WM_BASE_CODE C ON H.BILL_STATE = C.ITEM_VALUE WHERE H.BILL_ID = @BillID";
            IMapper map = DatabaseInstance.Instance();
            return map.QuerySingle<SOHeaderEntity>(sql, new { BillID = billID });
        }

        public string UpdateBillStatus(int billID, string status)
        {
            string sql = @"UPDATE WM_SO_HEADER SET BILL_STATE = @Status WHERE BILL_ID = @BillID";
            IMapper map = DatabaseInstance.Instance();
            return map.ExecuteScalar<string>(sql, new { Status = status, BillID = billID });
        }

        public int UpdateBillsState(string billIds, string status, string billState)
        {
            string sql = @"UPDATE WM_SO_HEADER SET BILL_STATE = @Status WHERE BILL_NO IN (@BillIDs) AND BILL_STATE= @BILL_STATE ";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new { Status = status, BillIDs = billIds, BILL_STATE = billState });
        }

        /// <summary>
        /// 执行越库出库
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="userName"></param>
        /// <returns>0: 成功；1：单据不存在；2：未拣配计算；3：发货类型不是越库发货</returns>
        public int AcrossOutbound(int billID, string userCode, string userName)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("V_BILL_ID", billID);
            parms.Add("V_USER_CODE", userCode);
            parms.AddOut("V_RESULT", DbType.Int32);

            IMapper map = DatabaseInstance.Instance();
            map.Execute("P_SO_CROSS_DOCK", parms, CommandType.StoredProcedure);

            return parms.Get<int>("V_RESULT");
        }


        public int SaveSortOrders(string billIDs, string userName, out string errBillNO)
        {
            DynamicParameters parms = new DynamicParameters();

            parms.Add("V_BILL_IDS", billIDs);
            parms.Add("V_USER_NAME", userName);
            parms.Add("V_GROUP_ID", Guid.NewGuid().ToString().Replace("-", ""));
            parms.AddOut("V_RESULT", DbType.Int32);
            parms.AddOut("V_BILL_NO", DbType.String);

            IMapper map = DatabaseInstance.Instance();
            map.Execute("P_SO_SAVE_ORDER", parms, CommandType.StoredProcedure);
            errBillNO = parms.Get<string>("V_BILL_NO");
            return parms.Get<int>("V_RESULT");
        }

        /// <summary>
        /// 查询已分组的订单
        /// </summary>
        /// <returns></returns>
        public List<SoGroupEntity> QueryBillsForGroup()
        {
            string sql = @"select sd.BILL_ID, sh.BILL_NO, bc2.ITEM_DESC as BILL_TYPE, bc1.ITEM_DESC as BILL_STATE, SUM(sd.QTY) as QTY, sum((sd.PRICE * sd.QTY)) as PRICE, 
                            c.C_NAME, c.ADDRESS, (c.X_COOR - wh.X_COOR) as x, (c.Y_COOR - wh.Y_COOR) as y, sh.WH_CODE, gp.gp_state as POSITION_TYPE 
                            from wm_so_detail sd 
                            inner join wm_so_header sh on sd.BILL_ID = sh.BILL_ID
                            inner join wm_warehouse wh on wh.WH_CODE = sh.WH_CODE
                            inner join wm_base_code bc1 on bc1.ITEM_VALUE = sh.BILL_STATE
                            inner join wm_base_code bc2 on bc2.ITEM_VALUE = sh.BILL_TYPE
                            inner join wm_base_code bc3 on bc3.ITEM_VALUE = sh.OUTSTORE_TYPE
                            left join customers c on c.C_CODE = sh.C_CODE
                            inner join wm_so_group gp on gp.BILL_ID = sd.BILL_ID
                            where sh.BILL_STATE = '{0}'  
                            group by sd.BILL_ID, sh.BILL_NO order by sh.BILL_NO ";

            sql = string.Format(sql, BaseCodeConstant.SO_WAIT_PICK);
            IMapper map = DatabaseInstance.Instance();
            return map.Query<SoGroupEntity>(sql);
        }

        /// <summary>
        /// 获取最大分组ID
        /// </summary>
        /// <returns></returns>
        public int GetMaxGroupID()
        {
            string sql = @"select case when max(group_id) is null then 0 else max(group_id) end from wm_so_group";
            IMapper map = DatabaseInstance.Instance();
            object _groupID = map.ExecuteScalar<object>(sql);
            if (_groupID == null)
                return 1;
            else
                return Convert.ToInt32(_groupID) + 1;
        }

        /// <summary>
        /// 手动更新分组编号
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="groupNo"></param>
        /// <param name="maxGroupID"></param>
        /// <returns></returns>
        public int ManualUpdateGroupNo(int billID, string groupNo, int maxGroupID)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("V_BILL_ID", billID);
            parms.Add("V_GROUP_NO", groupNo);
            parms.Add("V_MAX_GROUP", maxGroupID);
            parms.AddOut("V_RESULT", DbType.Int32);

            IMapper map = DatabaseInstance.Instance();
            map.Execute("P_SO_MANUAL_GROUP", parms, CommandType.StoredProcedure);

            return parms.Get<int>("V_RESULT");
        }

        /// <summary>
        /// 根据单据ID更新状态和标记
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public int UpdateDelayedOrder(int billID, string creator, string billNO)
        {
            string sql = "UPDATE wm_so_header set BILL_STATE = '60', SHIP_NO = NULL, DELAYMARK = 1 WHERE BILL_ID = @BillID";
            IMapper map = DatabaseInstance.Instance();
            int result = map.Execute(sql, new { BillID = billID });
            sql = String.Format("UPDATE wm_loading_detail SET FLAG = 1 WHERE BILL_NO='{0}'", billNO);
            map.Execute(sql);
            sql = String.Format("UPDATE wm_so_header wsh SET wsh.SHIP_NO =NULL WHERE BILL_NO='{0}'", billNO);
            map.Execute(sql);

            // 添加日志
            sql = "INSERT INTO WM_SO_LOG(BILL_ID, EVT, CREATE_DATE, CREATOR) " +
                  "VALUES(@BillID, '" + ESOOperationType.二次发货.ToString() + "', @CreateDate, @Creator)";
            map.Execute(sql, new { BillID = billID, CreateDate = DateTime.Now, Creator = creator });
            return result;
        }

        /// <summary>
        /// 根据单据ID更新状态和标记
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public int UpdateDelayedOrder1(int billID)
        {
            string sql = "";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new { BillID = billID });
        }


        /// <summary>
        /// 根据单据ID查找单据明细
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public List<SoGroupEntity> QueryBillDetailByID(int billID)
        {
            string sql = @"select sd.BILL_ID, sh.BILL_NO, bc2.ITEM_DESC as BILL_TYPE, bc1.ITEM_DESC as BILL_STATE, SUM(sd.QTY) as QTY, sum((sd.PRICE * sd.QTY)) as PRICE,sd.SPEC, 
                            c.C_NAME, c.ADDRESS, sh.WH_CODE, s.SKU_CODE, s.SKU_NAME 
                            from wm_so_detail sd 
                            inner join wm_so_header sh on sd.BILL_ID = sh.BILL_ID
                            inner join wm_warehouse wh on wh.WH_CODE = sh.WH_CODE
							inner join wm_sku s on s.SKU_CODE = sd.SKU_CODE
                            inner join wm_base_code bc1 on bc1.ITEM_VALUE = sh.BILL_STATE
                            inner join wm_base_code bc2 on bc2.ITEM_VALUE = sh.BILL_TYPE
                            inner join wm_base_code bc3 on bc3.ITEM_VALUE = sh.OUTSTORE_TYPE
                            left join customers c on c.C_CODE = sh.C_CODE
                            group by sd.BILL_ID, sh.BILL_NO order by sh.BILL_NO, sd.SKU_CODE ";
            string whereSql = string.Format(sql, BaseCodeConstant.SO_WAIT_GROUP, BaseCodeConstant.SO_WAIT_PICK, billID);
            IMapper map = DatabaseInstance.Instance();
            return map.Query<SoGroupEntity>(whereSql);
        }
        /// <summary>
        /// 查询单据日志
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public DataTable GetBillLog(int billID)
        {//(@rowNO := @rowNo+1)
            string sql = string.Format("SET @rowNO = 0; SELECT (@rowNO := @rowNO+1) as ID, EVT, CREATE_DATE, CREATOR FROM WM_SO_LOG a WHERE BILL_ID = {0}; ", billID);
            IMapper map = DatabaseInstance.Instance();
            DynamicParameters pram = new DynamicParameters();
            pram.Add("V_BILL_ID", billID);
            return map.LoadTable("P_SO_GET_LOG", pram, CommandType.StoredProcedure);
        }


        #region zhangyj 称重
        public DataTable GetSOBillMsg(string ctCode)
        {
            //string sql = "SELECT WCS.BILL_HEAD_ID, WSH.BILL_NO, C.C_NAME, C.ADDRESS, C.CONTACT, WSH.BILL_STATE, WC.CT_WEIGHT, WSH.CANCEL_FLAG " +
            string sql = "SELECT WCS.BILL_HEAD_ID, WSH.BILL_NO, C.C_NAME, C.ADDRESS, C.CONTACT, WSH.BILL_STATE, WC.CT_WEIGHT,WSH.CANCEL_FLAG " +
                "FROM WM_CONTAINER_STATE WCS " +
                "INNER JOIN WM_CONTAINER WC ON WCS.CT_CODE = WC.CT_CODE " +
                "INNER JOIN WM_SO_HEADER WSH ON WCS.BILL_HEAD_ID = WSH.BILL_ID " +
                "LEFT JOIN CUSTOMERS C ON WSH.C_CODE = C.C_CODE " +
                "WHERE WCS.CT_CODE = @CtCode";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { CtCode = ctCode });
        }
        /// <summary>
        /// 获取物流箱内商品信息
        /// </summary>
        /// <param name="billID">订单ID</param>
        /// <param name="ctCode">物流箱ID</param>
        /// <returns></returns>
        public DataTable GetContainerSKU(int billID, string ctCode)
        {
            string sql = "SELECT WS.SKU_NAME, WUS.SKU_BARCODE, WSP.QTY, WSP.PICK_QTY, " +
                "IFNULL(WSPR.PICK_QTY * WUS.WEIGHT, 0) AS WEIGHT, WU.UM_NAME " +
                "FROM WM_SO_PICK_RECORD WSPR " +
                "INNER JOIN WM_SO_PICK WSP ON WSP.ID = WSPR.PICK_ID " +
                "INNER JOIN WM_UM_SKU WUS ON WSPR.UM_SKU_ID = WUS.ID " +
                "INNER JOIN WM_SKU WS ON WUS.SKU_CODE = WS.SKU_CODE " +
                "INNER JOIN WM_UM WU ON WUS.UM_CODE = WU.UM_CODE " +
                "WHERE WSPR.BILL_ID = @BillID AND WSPR.CT_CODE = @CtCode";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { BillID = billID, CtCode = ctCode });
        }
        public List<SOWeightPrint> GetContainerSKU2(int billID, string ctCode)
        {
            string sql = "SELECT ws.SKU_NAME,wus.SKU_BARCODE,wsp.QTY,wsp.PICK_QTY,IFNULL( wspr.PICK_QTY * WUS.WEIGHT,0) AS WEIGHT,wu.UM_NAME FROM wm_so_pick wsp "
                      + "LEFT JOIN wm_so_pick_record wspr ON wsp.ID=wspr.PICK_ID "
                      + "LEFT JOIN WM_UM_SKU WUS ON wspr.UM_SKU_ID = WUS.ID "
                      + "INNER JOIN wm_so_detail wsd ON wsp.DETAIL_ID=wsd.ID "
                      + "INNER JOIN wm_sku ws ON wsd.SKU_CODE=ws.SKU_CODE "
                      + "INNER JOIN wm_um wu ON WUS.UM_CODE=wu.UM_CODE "
                      + "WHERE wspr.BILL_ID = {0} AND wspr.CT_CODE =  {1};";
            IMapper map = DatabaseInstance.Instance();
            return map.Query<SOWeightPrint>(String.Format(sql, billID, ctCode));
        }
        /// <summary>
        /// 保存称重记录
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="ctCode"></param>
        /// <returns></returns>
        public int SaveCheckWeight(string ctCode, decimal weight, string userCode, string authUserCode)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("V_WEIGHT", weight);
            parms.Add("V_CT_CODE", ctCode);
            parms.Add("V_USER_CODE", userCode);
            parms.Add("V_AUTH_USER_CODE", authUserCode);
            parms.AddOut("V_RESULT", DbType.Int32, 4);

            IMapper map = DatabaseInstance.Instance();
            map.Execute("P_SO_CONTAINER_WEIGHT", parms, CommandType.StoredProcedure);

            return parms.Get<int>("V_RESULT");
        }
        /// <summary>
        /// 保存装车称重的记录
        /// </summary>
        /// <param name="ctCode"></param>
        /// <param name="weight"></param>
        /// <param name="userCode"></param>
        /// <param name="authUserCode"></param>
        /// <param name="vhivleNO"></param>
        /// <returns></returns>
        public int SaveCheckWeight(string ctCode, decimal weight, decimal weightWuLiuXiang, string userCode, string authUserCode, string vhivleNO, out decimal calcWeight)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("V_WEIGHT", weight);
            parms.Add("V_WEIGHT_WULIUXIANG", weightWuLiuXiang);
            parms.Add("V_CT_CODE", ctCode);
            parms.Add("V_USER_CODE", userCode);
            parms.Add("V_AUTH_USER_CODE", authUserCode);
            parms.Add("V_VEHICLE_NO", vhivleNO);
            parms.AddOut("V_CALC_WEIGHT", DbType.Decimal, 32);
            parms.AddOut("V_RESULT", DbType.Int32, 4);

            IMapper map = DatabaseInstance.Instance();
            map.Execute("P_SO_CONTAINER_WEIGHT_LOADING", parms, CommandType.StoredProcedure);

            calcWeight = parms.Get<decimal>("V_CALC_WEIGHT");
            return parms.Get<int>("V_RESULT");
        }
        /// <summary>
        /// 物流箱配送称重状态
        /// </summary>
        /// <param name="WuLiuXiangLPN"></param>
        /// <returns></returns>
        public int UpdateWuLiuXiangState(string WuLiuXiangLPN)
        {
            string sql = "UPDATE  wm_container_state wcs SET wcs.CT_STATE='892' WHERE wcs.CT_CODE='{0}'";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(String.Format(sql, WuLiuXiangLPN));
        }
        /// <summary>
        /// 查询是否有未称重的物流箱
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public DataTable GetBillWuLiuXiangState(string billID)
        {
            string sql = "SELECT wcs.CT_CODE FROM wm_container_state wcs "
                      + "JOIN wm_container wc ON  wcs.CT_CODE=wc.CT_CODE "
                      + "WHERE wc.CT_TYPE='51' AND wcs.CT_STATE<>'892' AND  wcs.BILL_HEAD_ID='{0}';";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(String.Format(sql, billID));
        }

        /// <summary>
        /// 保存称重记录
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="ctCode"></param>
        /// <returns></returns>
        public int SaveCheckWeight(string ctCode, decimal weight, string userCode, string authUserCode,
            out int ctWeightIndex, out int ctTotalCount)
        {
            ctWeightIndex = ctTotalCount = 0;

            DynamicParameters parms = new DynamicParameters();
            parms.Add("V_WEIGHT", weight);
            parms.Add("V_CT_CODE", ctCode);
            parms.Add("V_USER_CODE", userCode);
            parms.Add("V_AUTH_USER_CODE", authUserCode);
            parms.AddOut("V_ORDER_INDEX", DbType.Int32, 4);
            parms.AddOut("V_CT_COUNT", DbType.Int32, 4);
            parms.AddOut("V_RESULT", DbType.Int32, 4);

            IMapper map = DatabaseInstance.Instance();
            map.Execute("P_SO_CONTAINER_WEIGHT", parms, CommandType.StoredProcedure);

            int result = parms.Get<int>("V_RESULT");
            if (result == 1)
            {
                ctWeightIndex = parms.Get<int>("V_ORDER_INDEX");
                ctTotalCount = parms.Get<int>("V_CT_COUNT");
            }

            return result;
        }

        public DataTable GetWuLiuXiangWeight(string LPN)
        {
            string sql = "SELECT wcs.BILL_HEAD_ID,wcs.CT_CODE, wcs.GROSS_WEIGHT,wcs.NET_WEIGHT FROM wm_container_state wcs WHERE wcs.CT_CODE='{0}';";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(String.Format(sql, LPN));

        }
        #endregion

        #region zhangyj 物流箱查询
        /// <summary>
        /// 查询物流箱状态
        /// </summary>
        /// <param name="billNO"></param>
        /// <param name="containerCode"></param>
        /// <param name="containerState"></param>
        /// <returns></returns>
        public DataTable ListContainerState(string billNO, string containerCode, string containerState)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT WC.CT_CODE, S.CT_STATE, ST.ITEM_DESC STATE_DESC, S.BILL_HEAD_ID, H.BILL_NO, S.UNIQUE_CODE, C.C_NAME, S.LC_CODE "
                        + "FROM WM_CONTAINER WC "
                        + "LEFT JOIN WM_CONTAINER_STATE S ON S.CT_CODE = WC.CT_CODE "
                        + "LEFT JOIN WM_BASE_CODE ST ON ST.ITEM_VALUE = S.CT_STATE "
                        + "LEFT JOIN WM_SO_HEADER H ON S.BILL_HEAD_ID = H.BILL_ID "
                        + "LEFT JOIN CUSTOMERS C ON H.C_CODE = C.C_CODE "
                        + "WHERE WC.CT_TYPE = '51'";

            return map.LoadTable(sql);
            //if (!string.IsNullOrEmpty(containerState))
            //    sql = string.Format(sql + " AND ({0})", DBUtil.FormatParameter("S.CARD_STATE", containerState));
            //return map.LoadTable(sql, new { BillID = containerCode, BillNO = billNO, CardNO = containerState });
        }

        /// <summary>
        /// 查询物流箱当前记录
        /// </summary>
        /// <param name="containerCode"></param>
        /// <returns></returns>
        public DataTable GetContainerRecords(string containerCode)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = string.Format("SELECT US.SKU_BARCODE, US.SKU_CODE, SKU.SKU_NAME, UM.UM_NAME, R.PICK_QTY, R.USER_CODE " +
                "FROM WM_SO_PICK_RECORD R " +
                "INNER JOIN WM_UM_SKU US ON R.UM_SKU_ID = US.ID " +
                "INNER JOIN WM_SKU SKU ON US.SKU_CODE = SKU.SKU_CODE " +
                "INNER JOIN WM_UM UM ON US.UM_CODE = UM.UM_CODE " +
                "WHERE R.UNIQUE_CODE = '{0}'", containerCode);

            return map.LoadTable(sql);
        }

        #endregion

        #region zhangyj 装车单打印 PENGWEI-20150803修改
        public List<SOHeaderEntity> GetVhicleHeadersInfoByBillID(int vehicleID)
        {
            string sql = @"SELECT A.BILL_ID, A.BILL_NO, A.FROM_WH_CODE, A.BILL_TYPE, 
  C1.ITEM_DESC BILL_TYPE_NAME, A.BILL_STATE, C2.ITEM_DESC STATUS_NAME, A.OUTSTORE_TYPE, 
  C3.ITEM_DESC OUTSTORE_TYPE_NAME, A.SALES_MAN, A.CONTRACT_NO, A.C_CODE, S.C_NAME, 
  S.ADDRESS, S.CONTACT, S.PHONE, A.SHIP_NO, A.REMARK, A.WMS_REMARK, A.ROW_COLOR, 
  A.CREATE_DATE, A.CLOSE_DATE,A.CONFIRM_DATE, W.WH_NAME FROM_WH_NAME, A.PICK_ZN_TYPE, 
  C4.ITEM_DESC PICK_ZN_TYPE_NAME, A.RECEIVE_AMOUNT, A.REAL_AMOUNT,A.CRN_AMOUNT, 
  A.OTHER_AMOUNT,A.CONFIRM_FLAG ,ifnull(A.PAYED_AMOUNT,0) PAYED_AMOUNT, 
  A.DELAYMARK, A.PAY_METHOD,A.ORIGINAL_BILL_NO, u.MOBILE_PHONE, u.USER_NAME, D.IN_VH_SORT, 
  F_CALC_PIECES_BY_PICK(A.BILL_ID, 0) BOX_NUM,F_CALC_BULK_PIECES(A.BILL_ID)  CASE_BOX_NUM,
  F_CALC_PIECES_BY_PICK(A.BILL_ID, 0) BOX_NUM, F_CALC_BULK_PIECES(A.BILL_ID) CASE_BOX_NUM, 
  A.SYNC_STATE, A.CANCEL_FLAG 
  FROM WM_LOADING_HEADER H 
  INNER JOIN WM_LOADING_DETAIL D ON H.VH_TRAIN_NO = D.VH_TRAIN_NO AND D.FLAG = 0 
  LEFT JOIN WM_SO_HEADER A ON A.BILL_NO = D.BILL_NO
  LEFT JOIN CUSTOMERS S ON A.C_CODE = S.C_CODE 
  INNER JOIN WM_BASE_CODE C1 ON A.BILL_TYPE = C1.ITEM_VALUE 
  INNER JOIN WM_BASE_CODE C2 ON A.BILL_STATE = C2.ITEM_VALUE 
  INNER JOIN WM_BASE_CODE C3 ON A.OUTSTORE_TYPE = C3.ITEM_VALUE 
  INNER JOIN WM_BASE_CODE C4 ON A.PICK_ZN_TYPE = C4.ITEM_VALUE 
  LEFT JOIN wm_vehicle wv ON H.VH_ID = wv.ID 
  LEFT JOIN users u ON u.USER_CODE = wv.USER_CODE 
  LEFT JOIN WM_WAREHOUSE W ON A.FROM_WH_CODE = W.WH_CODE 
  WHERE H.VH_ID = @VehicleID AND (A.BILL_STATE ='66'OR A.BILL_STATE ='67' OR A.BILL_STATE ='691' OR A.BILL_STATE ='65')
  GROUP BY A.BILL_NO";
            IMapper map = DatabaseInstance.Instance();
            return map.Query<SOHeaderEntity>(sql, new { VehicleID = vehicleID });
        }
        public List<SOHeaderEntity> GetVhicleHeadersInfoByBillID(int vehicleID, string loadingNo)
        {
            string sql = @"SELECT A.BILL_ID, A.BILL_NO, A.FROM_WH_CODE, A.BILL_TYPE, 
  C1.ITEM_DESC BILL_TYPE_NAME, A.BILL_STATE, C2.ITEM_DESC STATUS_NAME, A.OUTSTORE_TYPE, 
  C3.ITEM_DESC OUTSTORE_TYPE_NAME, A.SALES_MAN, A.CONTRACT_NO, A.C_CODE, S.C_NAME, 
  S.ADDRESS, S.CONTACT, S.PHONE, A.SHIP_NO, A.REMARK, A.WMS_REMARK, A.ROW_COLOR, 
  A.CREATE_DATE, A.CLOSE_DATE,A.CONFIRM_DATE, W.WH_NAME FROM_WH_NAME, A.PICK_ZN_TYPE, 
  C4.ITEM_DESC PICK_ZN_TYPE_NAME, A.RECEIVE_AMOUNT, A.REAL_AMOUNT,A.CRN_AMOUNT, 
  A.OTHER_AMOUNT,A.CONFIRM_FLAG ,ifnull(A.PAYED_AMOUNT,0) PAYED_AMOUNT, 
  A.DELAYMARK, A.PAY_METHOD,A.ORIGINAL_BILL_NO, u.MOBILE_PHONE, u.USER_NAME, D.IN_VH_SORT, 
  F_CALC_PIECES_BY_PICK(A.BILL_ID, 0) BOX_NUM,F_CALC_BULK_PIECES(A.BILL_ID)  CASE_BOX_NUM,
  F_CALC_PIECES_BY_PICK(A.BILL_ID, 0) BOX_NUM, F_CALC_BULK_PIECES(A.BILL_ID) CASE_BOX_NUM, 
  A.SYNC_STATE, A.CANCEL_FLAG 
  FROM WM_LOADING_HEADER H 
  INNER JOIN WM_LOADING_DETAIL D ON H.VH_TRAIN_NO = D.VH_TRAIN_NO AND D.FLAG = 0 
  INNER JOIN WM_SO_HEADER A ON A.BILL_NO = D.BILL_NO AND A.BILL_STATE NOT IN ('68', '693', '692')
  LEFT JOIN CUSTOMERS S ON A.C_CODE = S.C_CODE 
  INNER JOIN WM_BASE_CODE C1 ON A.BILL_TYPE = C1.ITEM_VALUE 
  INNER JOIN WM_BASE_CODE C2 ON A.BILL_STATE = C2.ITEM_VALUE 
  INNER JOIN WM_BASE_CODE C3 ON A.OUTSTORE_TYPE = C3.ITEM_VALUE 
  INNER JOIN WM_BASE_CODE C4 ON A.PICK_ZN_TYPE = C4.ITEM_VALUE 
  LEFT JOIN wm_vehicle wv ON H.VH_ID = wv.ID 
  LEFT JOIN users u ON u.USER_CODE = wv.USER_CODE 
  LEFT JOIN WM_WAREHOUSE W ON A.FROM_WH_CODE = W.WH_CODE 
  WHERE H.VH_ID = @VehicleID AND H.VH_TRAIN_NO = @LoadingNo
  GROUP BY A.BILL_NO";
            IMapper map = DatabaseInstance.Instance();
            return map.Query<SOHeaderEntity>(sql, new { VehicleID = vehicleID, LoadingNo = loadingNo });
        }
        #endregion

        /// <summary>
        /// 根据车辆信息获取未做回款确认的出库单信息 Add by ZXQ 20150611
        /// </summary>
        /// <param name="vehicleID"></param>
        /// <returns></returns>
        public List<SOHeaderEntity> GetVhicleHeadersByVehicleID(int vehicleID)
        {
            string sql = @"SELECT A.BILL_ID, A.BILL_NO, S.C_NAME, S.CONTACT, S.ADDRESS, A.CONFIRM_FLAG, IFNULL(A.CLOSE_DATE, A.LAST_UPDATETIME) AS CLOSE_DATE, 
                            CAST(IFNULL(A.RECEIVE_AMOUNT, B.SO_AMOUNT) AS DECIMAL(18,2)) AS RECEIVE_AMOUNT, A.CREATE_DATE, 
                            CAST(IFNULL(A.CRN_AMOUNT, IFNULL(C.CRN_AMOUNT, 0)) AS DECIMAL(18,2)) CRN_AMOUNT, 
                            CAST(IFNULL(A.OTHER_AMOUNT, 0) AS DECIMAL(18,2)) OTHER_AMOUNT, A.OTHER_REMARK, 
                            CAST(IFNULL(A.REAL_AMOUNT, B.SO_AMOUNT - IFNULL(C.CRN_AMOUNT, 0)) AS DECIMAL(18,2)) AS REAL_AMOUNT,
                            PAYMENT_FLAG 
                            FROM WM_SO_HEADER A 
                            INNER JOIN (SELECT IFNULL(SUM(wsd.PICK_QTY * wsd.PRICE), 0) AS SO_AMOUNT, wsd.BILL_ID 
                                        FROM wm_so_detail wsd GROUP BY wsd.BILL_ID) B ON B.BILL_ID = A.BILL_ID
                            LEFT JOIN (SELECT wch.SENTORDER_NO, IFNULL(SUM(wch.CRN_AMOUNT), 0) AS CRN_AMOUNT 
                                        FROM wm_crn_header wch GROUP BY wch.SENTORDER_NO) C ON C.SENTORDER_NO = A.BILL_NO
                            LEFT JOIN CUSTOMERS S ON A.C_CODE = S.C_CODE ";
            if (vehicleID == -1)
                sql += " WHERE (A.BILL_STATE = '68' OR A.BILL_STATE = '693') and A.BILL_TYPE = '120' and A.CONFIRM_FLAG = 0 ";
            else
                sql += " WHERE (A.BILL_STATE = '68' OR A.BILL_STATE = '693') and A.BILL_TYPE = '120' and A.SHIP_NO = @VehicleID and A.CONFIRM_FLAG = 0 ";

            IMapper map = DatabaseInstance.Instance();
            return map.Query<SOHeaderEntity>(sql, new { VehicleID = vehicleID });
        }

        /// <summary>
        /// 根据车辆信息获取未做回款确认的出库单信息 Add by ZXQ 20150611
        /// </summary>
        /// <param name="vehicleID"></param>
        /// <returns></returns>
        public List<SOHeaderEntity> GetConfirmHistory(int vehicleID, DateTime dateBegin, DateTime dateEnd)
        {
            string sql = @"SELECT A.BILL_ID, A.BILL_NO, S.C_NAME, S.CONTACT, S.ADDRESS, A.CONFIRM_FLAG, IFNULL(A.CLOSE_DATE, A.LAST_UPDATETIME) AS CLOSE_DATE, 
                        CAST(IFNULL(A.RECEIVE_AMOUNT, 0) AS DECIMAL(18,2)) AS RECEIVE_AMOUNT, A.CREATE_DATE, 
                        CAST(IFNULL(A.CRN_AMOUNT, 0) AS DECIMAL(18,2)) CRN_AMOUNT, 
                        CAST(IFNULL(A.OTHER_AMOUNT, 0) AS DECIMAL(18,2)) OTHER_AMOUNT, A.OTHER_REMARK, 
                        CAST(IFNULL(A.REAL_AMOUNT, 0) AS DECIMAL(18,2)) AS REAL_AMOUNT,
                        PAYMENT_DATE, PAYMENT_BY, PAYMENT_FLAG  
                        FROM WM_SO_HEADER A 
                        LEFT JOIN CUSTOMERS S ON A.C_CODE = S.C_CODE ";
            if (vehicleID == -1)
                sql += @" WHERE A.BILL_STATE = '68' and A.CONFIRM_FLAG = 1 
                and IFNULL(A.PAYMENT_DATE, A.LAST_UPDATETIME) >= @DateBegin and IFNULL(A.PAYMENT_DATE, A.LAST_UPDATETIME) <= @DateEnd";
            else
                sql += @" WHERE A.BILL_STATE = '68' and A.SHIP_NO = @VehicleID and A.CONFIRM_FLAG = 1 
                and IFNULL(A.PAYMENT_DATE, A.LAST_UPDATETIME) >= @DateBegin and IFNULL(A.PAYMENT_DATE, A.LAST_UPDATETIME) <= @DateEnd";
            IMapper map = DatabaseInstance.Instance();
            return map.Query<SOHeaderEntity>(sql, new { VehicleID = vehicleID, DateBegin = dateBegin, DateEnd = dateEnd });
        }

        /// <summary>
        /// 保存出库单的各种金额 Add by ZXQ 20150611
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="receiveAmount"></param>
        /// <param name="realAmount"></param>
        /// <param name="crnAmount"></param>
        /// <param name="otherAmount"></param>
        /// <returns></returns>
        public int SaveAmount(int billID, decimal receiveAmount, decimal realAmount, decimal crnAmount, decimal otherAmount, string otherRemark, bool paymentFlag)
        {
            string sql = "";
            if (!paymentFlag)
            {
                sql = @"update wm_so_header set RECEIVE_AMOUNT = @ReceiveAmount, REAL_AMOUNT = @RealAmount, CRN_AMOUNT = @CrnAmount, 
                OTHER_AMOUNT = @OtherAmount, OTHER_REMARK = @OtherRemark where BILL_ID = @BillID ";
            }
            else
            {
                sql = @"update wm_so_header set RECEIVE_AMOUNT = @ReceiveAmount, REAL_AMOUNT = @RealAmount, CRN_AMOUNT = @CrnAmount, 
                OTHER_AMOUNT = @OtherAmount, OTHER_REMARK = @OtherRemark where BILL_ID = @BillID ";
            }
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new { ReceiveAmount = receiveAmount, RealAmount = realAmount, CrnAmount = crnAmount, OtherAmount = otherAmount, OtherRemark = otherRemark, BillID = billID });
        }

        //更新应收金额 Add by ZXQ 2015-06-25
        public int SaveReceiveAmount(int billID, decimal receiveAmount)
        {
            string sql = "update wm_so_header set RECEIVE_AMOUNT = @ReceiveAmount where BILL_ID = @BillID ";

            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new { ReceiveAmount = receiveAmount, BillID = billID });
        }

        /// <summary>
        /// 更新发货单的回款确认标记 Add by ZXQ 20150611
        /// </summary>
        /// <param name="lstHeader"></param>
        /// <returns></returns>
        public int UpdateConfirmFlag(List<SOHeaderEntity> lstHeader, string loginName)
        {
            int rtn = 0;
            string sql = @"update wm_so_header set PAYMENT_FLAG = 1, CONFIRM_FLAG = 1, SYNC_STATE = 6, RECEIVE_AMOUNT = @RecAmount, REAL_AMOUNT = @RealAmount,
                        CRN_AMOUNT = @CrnAmount, OTHER_AMOUNT = @OtherAmount, PAYMENT_DATE = NOW(), PAYMENT_BY = @PaymentBy
                        where BILL_ID = @BillID ";
            IMapper map = DatabaseInstance.Instance();
            IDbTransaction trans = map.BeginTransaction();
            try
            {
                foreach (SOHeaderEntity itm in lstHeader)
                {
                    rtn += map.Execute(sql, new
                    {
                        RecAmount = itm.ReceiveAmount,
                        RealAmount = itm.RealAmount,
                        CrnAmount = itm.CrnAmount,
                        OtherAmount = itm.OtherAmount,
                        PaymentBy = loginName,
                        BillID = itm.BillID
                    });
                }
                if (rtn > 0)
                {
                    trans.Commit();
                }
            }
            catch
            {
                rtn = 0;
                trans.Rollback();
            }
            return rtn;
        }

        #region 2015-05-23
        /// <summary>
        ///  打印显示托盘号
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public string GetVhicleNo(int billID)
        {
            string sql = "SELECT IFNULL(wv.VH_NO,'') FROM wm_so_header wsh "
                        + "JOIN wm_vehicle wv ON wsh.SHIP_NO=wv.ID "
                        + "WHERE wsh.BILL_ID=@BillID";
            IMapper map = DatabaseInstance.Instance();
            return map.ExecuteScalar<string>(sql, new { BillID = billID });

        }
        /// <summary>
        /// 释放订单相关托盘
        /// </summary>
        /// <param name="billID"></param>
        public void UpdateContainerState(int billID)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "UPDATE wm_container_state wcs SET wcs.BILL_HEAD_ID=NULL ,wcs.CT_STATE='80',wcs.UNIQUE_CODE=NULL , "
                      + "wcs.GROSS_WEIGHT=NULL,wcs.NET_WEIGHT=NULL,wcs.LC_CODE=NULL,wcs.LAST_UPDATETIME=NOW() "
                      + "WHERE wcs.BILL_HEAD_ID=@BillID";
            map.Execute(sql, new { BillID = billID });
        }
        /// <summary>
        /// 当前订单量
        /// </summary>
        /// <returns></returns>
        public DataTable GetBillPlans(EWarehouseType wType)
        {
            IMapper map = DatabaseInstance.Instance();
            //            string sql = @"SELECT wsd.SKU_CODE,ws.SKU_NAME,wsd.SPEC,wsd.UM_CODE,wu.UM_NAME,SUM(wsd.QTY) BillQty,IFNULL(A.TotalQty,0) TotalQty FROM wm_so_detail wsd
            //JOIN wm_so_header wsh ON wsd.BILL_ID=wsh.BILL_ID
            //  LEFT JOIN ( 
            //    SELECT ws.SKU_CODE,ROUND(SUM((ws.QTY-ws.PICKING_QTY)/wus.QTY),0) TotalQty FROM wm_stock ws
            //  JOIN wm_location wl ON ws.LC_CODE=wl.LC_CODE
            //      JOIN wm_um_sku wus ON ws.SKU_CODE =wus.SKU_CODE AND wus.S_UNIT=1
            //  WHERE ws.QTY>0 AND wl.FLOOR_CODE=1
            //  GROUP BY ws.SKU_CODE
            //  ) A ON  wsd.SKU_CODE=A.SKU_CODE 
            //  JOIN wm_sku ws ON wsd.SKU_CODE =ws.SKU_CODE
            //  JOIN wm_um wu ON wsd.UM_CODE=wu.UM_CODE
            //WHERE wsh.BILL_STATE in ('60','61') AND wsd.IS_CASE=1 
            //  GROUP BY  wsd.SKU_CODE;";
            string sql = @"SELECT 
  wsd.SKU_CODE,wsd.SPEC,ws.SKU_NAME,wsd.UM_CODE,wu.UM_NAME,wus.ID,
  IFNULL(ROUND(SUM(wsd.QTY),0),0) BillQty,
  IFNULL(ROUND(A.TotalQty/wus.QTY,2),0) TotalQty,
  IFNULL(ROUND(B.TotalQty/wus.QTY,2),0) StockTotalQty,
  IFNULL(ROUND(C.TotalQty/wus.QTY,2),0) BackupQty,
  wus.QTY, wsd.IS_CASE, vw.QTY ADVICE_QTY,vw.UM_NAME ADVICE_UM_NAME,CASE WHT.FLAG  WHEN 1 THEN 1 ELSE 0 END FLAG
 FROM wm_so_detail wsd
JOIN wm_so_header wsh ON wsd.BILL_ID=wsh.BILL_ID
  LEFT JOIN ( 
    SELECT ws.SKU_CODE,ROUND(SUM((ws.QTY-ws.PICKING_QTY)),2) TotalQty FROM wm_stock ws
  JOIN wm_location wl ON ws.LC_CODE=wl.LC_CODE
  JOIN wm_zone wz ON wl.zn_code =wz.zn_code 
  WHERE ws.QTY>0 AND wz.zt_code='70'
  GROUP BY ws.SKU_CODE
  ) A ON  wsd.SKU_CODE=A.SKU_CODE 
  LEFT JOIN ( 
    SELECT ws.SKU_CODE,ROUND(SUM((ws.QTY-ws.PICKING_QTY)),2) TotalQty FROM wm_stock ws
  JOIN wm_location wl ON ws.LC_CODE=wl.LC_CODE
  JOIN wm_zone wz ON wl.zn_code =wz.zn_code 
  WHERE ws.QTY>0 AND wz.zt_code='71'
  GROUP BY ws.SKU_CODE
  ) B ON  wsd.SKU_CODE=B.SKU_CODE 
LEFT JOIN ( 
    SELECT ws.SKU_CODE,ROUND(SUM((ws.QTY-ws.PICKING_QTY)),2) TotalQty FROM wm_stock ws
    JOIN wm_location wl ON ws.LC_CODE=wl.LC_CODE
    JOIN wm_zone wz ON wl.zn_code =wz.zn_code 
    WHERE ws.QTY>0 AND wz.zt_code='73'
    GROUP BY ws.SKU_CODE
  ) C ON  wsd.SKU_CODE=C.SKU_CODE 
LEFT JOIN(SELECT 1 flag,wtd.SKU_CODE FROM tasks t INNER JOIN wm_trans_header wth 
  ON t.BILL_ID = wth.ID
  INNER JOIN wm_trans_detail wtd ON
   wth.ID = wtd.BILL_ID GROUP BY wtd.SKU_CODE) wht ON wsd.SKU_CODE = wht.SKU_CODE
LEFT JOIN (SELECT vwsqw.SKU_CODE,vwsqw.QTY,wu.UM_NAME
  FROM v_wm_sales_qty_week vwsqw INNER JOIN wm_um wu ON vwsqw.UM_CODE = wu.UM_CODE) vw ON
  wsd.SKU_CODE = vw.SKU_CODE
  JOIN wm_um_sku wus ON wsd.SKU_CODE=wus.SKU_CODE AND wsd.UM_CODE=wus.UM_CODE
  JOIN wm_sku ws ON wsd.SKU_CODE =ws.SKU_CODE 
  JOIN wm_um wu ON wsd.UM_CODE=wu.UM_CODE
WHERE (wsh.BILL_STATE = '60' or wsh.BILL_STATE = '61') AND wsh.PICK_ZN_TYPE = '70'" + (wType == EWarehouseType.整货仓 ? " AND wsd.IS_CASE=1 " : string.Empty) +
  "GROUP BY  wsd.SKU_CODE;";
            return map.LoadTable(sql);
        }
        public DataTable GetBillPlansBySorted(EWarehouseType wType)
        {
            string sql = @"SELECT 
  wsd.SKU_CODE,wsd.SPEC,ws.SKU_NAME,wsd.UM_CODE,wu.UM_NAME,wus.ID,
  IFNULL(ROUND(SUM(wsd.QTY),0),0) BillQty,
  IFNULL(ROUND(A.TotalQty/wus.QTY,2),0) TotalQty,
  IFNULL(ROUND(B.TotalQty/wus.QTY,2),0) StockTotalQty,
  IFNULL(ROUND(C.TotalQty/wus.QTY,2),0) BackupQty,
  wus.QTY, wsd.IS_CASE, vw.QTY ADVICE_QTY,vw.UM_NAME ADVICE_UM_NAME, CASE WHT.FLAG  WHEN 1 THEN 1 ELSE 0 END FLAG
 FROM wm_so_detail wsd
JOIN wm_so_header wsh ON wsd.BILL_ID=wsh.BILL_ID
  LEFT JOIN ( 
    SELECT ws.SKU_CODE,ROUND(SUM((ws.QTY-ws.PICKING_QTY)),2) TotalQty FROM wm_stock ws
  JOIN wm_location wl ON ws.LC_CODE=wl.LC_CODE
  JOIN wm_zone wz ON wl.zn_code =wz.zn_code 
  WHERE ws.QTY>0 AND wz.zt_code='70'
  GROUP BY ws.SKU_CODE
  ) A ON  wsd.SKU_CODE=A.SKU_CODE 
  LEFT JOIN ( 
    SELECT ws.SKU_CODE,ROUND(SUM((ws.QTY-ws.PICKING_QTY)),2) TotalQty FROM wm_stock ws
  JOIN wm_location wl ON ws.LC_CODE=wl.LC_CODE
  JOIN wm_zone wz ON wl.zn_code =wz.zn_code 
  WHERE ws.QTY>0 AND wz.zt_code='71'
  GROUP BY ws.SKU_CODE
  ) B ON  wsd.SKU_CODE=B.SKU_CODE 
LEFT JOIN ( 
    SELECT ws.SKU_CODE,ROUND(SUM((ws.QTY-ws.PICKING_QTY)),2) TotalQty FROM wm_stock ws
    JOIN wm_location wl ON ws.LC_CODE=wl.LC_CODE
    JOIN wm_zone wz ON wl.zn_code =wz.zn_code 
    WHERE ws.QTY>0 AND wz.zt_code='73'
    GROUP BY ws.SKU_CODE
  ) C ON  wsd.SKU_CODE=C.SKU_CODE 
 LEFT JOIN(SELECT 1 flag,wtd.SKU_CODE FROM tasks t INNER JOIN wm_trans_header wth 
  ON t.BILL_ID = wth.ID
  INNER JOIN wm_trans_detail wtd ON
   wth.ID = wtd.BILL_ID GROUP BY wtd.SKU_CODE) wht ON wsd.SKU_CODE = wht.SKU_CODE
LEFT JOIN (SELECT vwsqw.SKU_CODE,vwsqw.QTY,wu.UM_NAME
  FROM v_wm_sales_qty_week vwsqw INNER JOIN wm_um wu ON vwsqw.UM_CODE = wu.UM_CODE) vw ON
  wsd.SKU_CODE = vw.SKU_CODE
  JOIN wm_um_sku wus ON wsd.SKU_CODE=wus.SKU_CODE AND wsd.UM_CODE=wus.UM_CODE
  JOIN wm_sku ws ON wsd.SKU_CODE =ws.SKU_CODE 
  JOIN wm_um wu ON wsd.UM_CODE=wu.UM_CODE
WHERE wsh.BILL_STATE = '61' AND wsh.PICK_ZN_TYPE = '70'" + (wType == EWarehouseType.整货仓 ? " AND wsd.IS_CASE=1 " : string.Empty) +
  "GROUP BY  wsd.SKU_CODE;";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql);
        }
        /// <summary>
        /// h
        /// </summary>
        /// <returns></returns>
        public DataTable GetSKULocation(string skuCode)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = @"SELECT  wz.ZN_NAME,ws.LC_CODE,ws1.SKU_NAME,ws.SKU_CODE,round(ws.QTY,0) STOCKQTY,wu1.UM_NAME STOCKUMNAME,  group_concat( Format( ws.QTY/wus.QTY,0),wu.UM_NAME) SALEUMNAME "
                     + "FROM wm_stock ws "
                     + "JOIN wm_sku ws1 ON ws.SKU_CODE =ws1.SKU_CODE "
                     + "JOIN wm_um_sku wus ON ws.SKU_CODE=wus.SKU_CODE AND wus.S_UNIT=1 "
                     + "JOIN wm_um_sku wus1 ON ws.SKU_CODE=wus1.SKU_CODE AND ws.UM_CODE=wus1.UM_CODE "
                     + "JOIN wm_location wl ON ws.LC_CODE=wl.LC_CODE "
                     + "JOIN wm_zone wz ON wl.ZN_CODE=wz.ZN_CODE "
                     + "JOIN wm_um wu ON wus.UM_CODE =wu.UM_CODE "
                     + "JOIN wm_um wu1 ON wus1.UM_CODE =wu1.UM_CODE "
                     + "WHERE ws.SKU_CODE ='{0}' AND ws.QTY>0 "
                     + "GROUP BY wz.ZN_NAME,ws.LC_CODE,ws1.SKU_NAME,ws.SKU_CODE;";
            return map.LoadTable(String.Format(sql, skuCode));
        }

        /// <summary>
        /// 获取一个单据的拣货记录
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public DataTable GetPickRecordsByBillID(int billID)
        {
            string sql = string.Format(
                @"SELECT R.CT_CODE, D.SKU_CODE, M.SKU_NAME, UM.UM_NAME, R.PICK_QTY, 
  (CASE WHEN S.LC_CODE IS NULL THEN SP.LC_CODE ELSE S.LC_CODE END) LC_CODE, 
  U.USER_NAME, R.PICK_DATE
  FROM WM_SO_PICK_RECORD R 
  INNER JOIN USERS U ON R.USER_CODE = U.USER_CODE 
  INNER JOIN WM_SO_PICK SP ON R.PICK_ID = SP.ID 
  LEFT JOIN WM_SO_DETAIL D ON D.ID = SP.DETAIL_ID
  LEFT JOIN WM_STOCK S ON SP.STOCK_ID = S.ID 
  LEFT JOIN WM_SKU M ON M.SKU_CODE = D.SKU_CODE 
  LEFT JOIN WM_UM_SKU WUS ON R.UM_SKU_ID = WUS.ID 
  LEFT JOIN WM_UM UM ON WUS.UM_CODE = UM.UM_CODE 
  WHERE R.BILL_ID = {0}", billID);

            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql);
        }
        /// <summary>
        /// 获取一个托盘的拣货记录
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public DataTable GetPickRecordsByCtCode(int billID, string ctCode)
        {
            string sql = string.Format("SELECT wsd.SKU_CODE, ws.SKU_NAME, ws.SPEC, " +
  "ROUND((SUM(wspr.PICK_QTY)/(wus1.QTY/wus.QTY)),0) SAILQTY, " +
  "wu1.UM_NAME SAILUMNAME, wus1.WEIGHT,  " +
  "ROUND((SUM(wspr.PICK_QTY)/(wus1.QTY/wus.QTY)),0)* wus1.WEIGHT TotalWeight, " +
  "u.USER_NAME, wspr.PICK_DATE " +
    "FROM wm_so_pick_record wspr " +
    "INNER JOIN wm_so_pick wsp ON wsp.ID = wspr.PICK_ID AND wsp.LC_CODE = wspr.LC_CODE " +
    "INNER JOIN wm_so_detail wsd ON wsd.ID = wsp.DETAIL_ID " +
    "LEFT JOIN wm_sku ws ON ws.SKU_CODE = wsd.SKU_CODE " +
    "LEFT JOIN wm_um_sku wus ON wus.SKU_CODE = ws.SKU_CODE AND wus.ID = wspr.UM_SKU_ID " +
    "LEFT JOIN wm_um_sku wus1 ON wus1.SKU_CODE = wsd.SKU_CODE AND wus1.UM_CODE = wsd.UM_CODE " +
    "LEFT JOIN wm_um wu ON wu.UM_CODE =wus.UM_CODE " +
    "LEFT JOIN wm_um wu1 ON wu1.UM_CODE =wus1.UM_CODE " +
    "LEFT JOIN users u ON u.USER_CODE = wspr.USER_CODE " +
    "WHERE wspr.BILL_ID = '{0}' and wspr.CT_CODE='{1}' " +
    "GROUP BY ws.SKU_CODE, ws.SPEC;", billID, ctCode);

            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql);
        }
        #endregion

        #region 2015-05-31
        public DataTable GetSKUSaleSort(string dateStart, string dateEnd)
        {
            //string sql = "SELECT  wus1.SKU_CODE,ws.SKU_NAME,ROUND (SUM(wspr.PICK_QTY/wus1.QTY),0) QTY,wsd.SPEC,wu.UM_NAME FROM wm_so_pick_record wspr "
            //              + "JOIN wm_um_sku wus ON wspr.UM_SKU_ID=wus.ID "
            //              + "JOIN wm_sku ws ON wus.SKU_CODE=ws.SKU_CODE "
            //              + "JOIN wm_um_sku wus1 ON wus.SKU_CODE=wus1.SKU_CODE AND wus1.S_UNIT=1 "
            //              + "JOIN wm_um wu ON wus1.UM_CODE=wu.UM_CODE "
            //              + "JOIN wm_so_pick wsp ON wspr.PICK_ID=wsp.ID "
            //              + "JOIN wm_so_detail wsd ON wsp.DETAIL_ID=wsd.ID "
            //              + "WHERE wspr.PICK_DATE>='{0}' and wspr.PICK_DATE<='{1}' "
            //              + "GROUP BY wus1.SKU_CODE ORDER BY QTY DESC";
            string sql = "SELECT  wus1.SKU_CODE,ws.SKU_NAME, ROUND (SUM(wspr.PICK_QTY/wus1.QTY),0) QTY,wsd.SPEC,wu.UM_NAME "
                          + "FROM wm_so_pick_record wspr "
                          + "JOIN wm_so_pick wsp ON wspr.PICK_ID=wsp.ID "
                          + "JOIN wm_so_detail wsd ON wsp.DETAIL_ID=wsd.ID "
                          + "JOIN wm_um_sku wus ON wspr.UM_SKU_ID=wus.ID "
                          + "JOIN wm_sku ws ON wus.SKU_CODE=ws.SKU_CODE "
                          + "JOIN wm_um_sku wus1 ON wus.SKU_CODE=wus1.SKU_CODE AND wsd.UM_CODE=wus1.UM_CODE "
                          + "JOIN wm_um wu ON wus1.UM_CODE=wu.UM_CODE "
                          + "WHERE wspr.PICK_DATE>='{0}' and wspr.PICK_DATE<='{1}' "
                          + "GROUP BY wus1.SKU_CODE ORDER BY QTY DESC";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(String.Format(sql, String.IsNullOrEmpty(dateStart) == true ? DateTime.MinValue.ToString() : dateStart, String.IsNullOrEmpty(dateEnd) == true ? DateTime.MaxValue.ToString() : dateEnd));
        }
        #endregion

        #region 2015-06-02
        public int SetBillStates(int billID, string state, int vehicleID)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("V_BILL_ID", billID);
            parms.Add("V_BILL_STATE", state);
            parms.Add("V_VEHICLE_ID", vehicleID);
            parms.AddOut("V_RESULT", DbType.Int32, 4);

            IMapper map = DatabaseInstance.Instance();
            map.Execute("P_SO_SET_BILL_STATE", parms, CommandType.StoredProcedure);

            int result = parms.Get<int>("V_RESULT");
            return result;
        }
        #endregion

        #region 2015-06-03
        public int UpdateOutstoreStype(int billID, string type)
        {
            string sql = "UPDATE wm_so_header wsh SET wsh.OUTSTORE_TYPE='{0}' WHERE wsh.BILL_ID={1}";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(String.Format(sql, type, billID));

        }
        #endregion

        #region 2015-06-11
        /// <summary>
        /// 获取订单的附件信息
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public List<SODetailAttributeEntity> GeDetailAttri(int billID)
        {
            string sql = "SELECT wsda.ID, BILL_ID,  BILL_NO,  TYPE,  wsda.SKU_CODE,  wsda.UM_CODE,  NUM, " +
                         "BUY_PRICE,  SELL_PRICE,  wsda.LAST_UPDATETIME, ws.SKU_NAME, wsda.YUFU_NAME " +
                         "FROM wm_so_detail_attribute wsda " +
                         "LEFT JOIN wm_sku ws ON wsda.SKU_CODE = ws.SKU_CODE " +
                         "WHERE wsda.BILL_ID=@BillID";
            IMapper map = DatabaseInstance.Instance();
            return map.Query<SODetailAttributeEntity>(sql, new { BillID = billID });
        }

        /// <summary>
        /// 判断是否是新客户
        /// </summary>
        /// <param name="cusCode"></param>
        /// <returns></returns>
        public string GetCustomerIsNew(string cusCode)
        {
            string sql = String.Format("SELECT IFNULL(COUNT(wsh.C_CODE),0) FROM wm_so_header wsh WHERE wsh.C_CODE='{0}' ;", cusCode);
            IMapper map = DatabaseInstance.Instance();
            DataTable dt = map.LoadTable(sql);
            return dt.Rows[0][0].ToString();
        }
        #endregion

        #region 2015-06-13 zhangyj
        public int? BillIsCase(int billID)
        {
            string sql = String.Format("SELECT  COUNT(wsd.BILL_ID) FROM wm_so_detail wsd WHERE wsd.BILL_ID={0} AND wsd.IS_CASE=2;", billID);
            IMapper map = DatabaseInstance.Instance();
            return map.ExecuteScalar<int?>(sql);
        }

        public DataTable GetBillWuLiuXiangState2(string billID)
        {
            string sql = "SELECT wcs.CT_CODE FROM wm_container_state wcs "
                      + "JOIN wm_container wc ON  wcs.CT_CODE=wc.CT_CODE "
                      + "WHERE wc.CT_TYPE='51' AND  wcs.BILL_HEAD_ID='{0}';";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(String.Format(sql, billID));
        }
        #endregion

        #region 2015-6-10 13:46:09 by wangjw
        /// <summary>
        /// 获取一个单据的称重记录 2015-6-10 13:46:09 by wangjw
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public DataTable GetWeighRecordsByBillID(int billID)
        {
            string sql = string.Format("SELECT wsw.CT_CODE, wsw.GROSS_WEIGHT, wsw.NET_WEIGHT, u.USER_NAME, wsw.CREATE_DATE, uu.USER_NAME AS AUTH_USER_NAME ,wv.VH_NO "
                + "FROM wm_so_weight wsw "
                + "INNER JOIN users u ON u.USER_CODE = wsw.USER_CODE "
                + "LEFT join users uu on uu.USER_CODE = wsw.AUTH_USER_CODE "
                + "LEFT JOIN wm_vehicle wv ON wv.VH_CODE = wsw.VH_CODE "
                + "WHERE wsw.BILL_ID = {0} "
                + "ORDER BY wsw.CREATE_DATE ASC ", billID);
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql);
        }

        /// <summary>
        /// 查询指定车辆的装车记录
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public DataTable GetLoadRecordsByWhCode(string whCode, DateTime dateBegin, DateTime dateEnd)
        {
            string sql = string.Format("SELECT wv.VH_NO, wv.RT_CODE, wsh.BILL_NO, c.C_NAME, wsh.CREATE_DATE, wsh.LAST_UPDATETIME,ROUND(SUM(wsd.PICK_QTY),0) QTY "
                + "FROM wm_so_header wsh "
                + "JOIN wm_so_detail wsd ON wsh.BILL_ID = wsd.BILL_ID "
                + "LEFT JOIN wm_vehicle wv ON wv.ID = wsh.SHIP_NO "
                + "LEFT JOIN customers c ON c.C_CODE = wsh.C_CODE "
                + "WHERE wsh.SHIP_NO = (SELECT wv1.ID FROM wm_vehicle wv1 WHERE wv1.VH_CODE = '{0}') "
                + "AND wsh.LAST_UPDATETIME BETWEEN '{1}' AND '{2}' "
                + "GROUP BY wsh.BILL_NO "
                + "ORDER BY wsh.LAST_UPDATETIME DESC", whCode, dateBegin, dateEnd);
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql);
        }
        #endregion

        #region zhangyj 根据SKU生成补货任务
        public int ReplenishBySku(string skuCode, decimal shortQty, string uniqueCode, string whCode, decimal umSkuID)
        {
            IMapper map = DatabaseInstance.Instance();
            DynamicParameters param = new DynamicParameters();
            param.Add("V_SKU_CODE", skuCode);
            param.Add("V_SHORT_QTY", shortQty);
            param.Add("V_G_ID", uniqueCode);
            param.Add("V_WH_CODE", whCode);
            param.Add("V_UM_SKU_ID", umSkuID);
            return map.Execute("P_REPLENISH_BY_SKU", param, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 获取补货结果
        /// </summary>
        /// <param name="uniqueCode"></param>
        /// <returns></returns>
        public DataTable GetReplenishBySku(string uniqueCode)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = String.Format("SELECT G_ID,lc_code,sku_code,from_stock_id,trans_qty FROM wm_replenish_temp WHERE G_ID='{0}';", uniqueCode);
            return map.LoadTable(sql);
        }
        #endregion

        #region 彭伟 2015-07-14
        /// <summary>
        /// 关联订单与车辆
        /// </summary>
        /// <returns></returns>
        public int JoinBillNOAndVehicle(string vehicleID, int billID)
        {
            string sql = string.Format(
                "UPDATE wm_so_header wsh SET wsh.SHIP_NO=@VehicleID, wsh.BILL_STATE='66', " +
                "wsh.LAST_UPDATETIME=NOW(), wsh.CLOSE_DATE = NOW() WHERE wsh.BILL_ID=@BillID");
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new { VehicleID = vehicleID, billID = billID });
        }
        public int JoinBillNOAndVehicle(string vehicleID, int billID, string billState)
        {
            string sql = string.Format(
                "UPDATE wm_so_header wsh SET wsh.SHIP_NO=@VehicleID, " +
                "wsh.LAST_UPDATETIME=NOW(), wsh.CLOSE_DATE = NOW() WHERE wsh.BILL_ID=@BillID");
            if (!String.IsNullOrEmpty(billState))
                sql += String.Format(", wsh.BILL_STATE='{0}'", billState);
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new { VehicleID = vehicleID, billID = billID });
        }
        #endregion

        public static string CreateTask(int billID, string taskType)
        {
            IMapper map = DatabaseInstance.Instance();
            DynamicParameters parm = new DynamicParameters();
            parm.Add("v_bill_id", billID);
            parm.Add("v_task_type", taskType);
            parm.AddOut("v_msg", DbType.String, 30);
            map.ExecuteScalar<string>("P_SO_AUTO_TASK_CREATE", parm, null, CommandType.StoredProcedure);
            return parm.Get<string>("v_msg");
        }

        #region 2015-08-24 彭伟
        /// <summary>
        /// 获取等待称重并且未生成装车任务的订单
        /// </summary>
        /// <returns></returns>
        public static List<SOHeaderEntity> GetUnLoadingBills()
        {
            string sql = @"SELECT A.BILL_ID, A.BILL_NO, OS.VEHICLE_NO, A.FROM_WH_CODE, A.BILL_TYPE, 
              C1.ITEM_DESC BILL_TYPE_NAME, A.BILL_STATE, C2.ITEM_DESC STATUS_NAME, A.OUTSTORE_TYPE, 
              C3.ITEM_DESC OUTSTORE_TYPE_NAME, A.SALES_MAN, A.CONTRACT_NO, A.C_CODE, S.C_NAME, S.ADDRESS, 
              S.CONTACT, S.PHONE,A.SHIP_NO, A.REMARK, A.WMS_REMARK, A.ROW_COLOR, A.CREATE_DATE, 
              A.CLOSE_DATE, W.WH_NAME FROM_WH_NAME, A.PICK_ZN_TYPE, C4.ITEM_DESC PICK_ZN_TYPE_NAME, 
              A.DELAYMARK, F_CALC_PIECES_BY_PICK(A.BILL_ID, 1) BOX_NUM, S.X_COOR, S.Y_COOR, OS.Attri1, 
              F_CALC_BULK_PIECES(A.BILL_ID) CASE_BOX_NUM, OS.IN_VEHICLE_SORT ORDER_SORT, A.SYNC_STATE 
              FROM WM_SO_HEADER A
              LEFT JOIN CUSTOMERS S ON A.C_CODE = S.C_CODE 
              INNER JOIN WM_BASE_CODE C1 ON A.BILL_TYPE = C1.ITEM_VALUE 
              INNER JOIN WM_BASE_CODE C2 ON A.BILL_STATE = C2.ITEM_VALUE 
              INNER JOIN WM_BASE_CODE C3 ON A.OUTSTORE_TYPE = C3.ITEM_VALUE 
              INNER JOIN WM_BASE_CODE C4 ON A.PICK_ZN_TYPE = C4.ITEM_VALUE 
              LEFT JOIN WM_WAREHOUSE W ON A.FROM_WH_CODE = W.WH_CODE 
              LEFT JOIN WM_ORDER_SORT OS ON OS.BILL_NO = A.BILL_NO 
              WHERE A.BILL_STATE IN('65', '66', '691') AND (A.SHIP_NO IS NULL OR A.SHIP_NO = '') 
              AND NOT EXISTS (
              SELECT D.BILL_NO FROM WM_LOADING_DETAIL D 
                LEFT JOIN WM_SO_HEADER H ON H.BILL_NO = D.BILL_NO 
                WHERE (H.DELAYMARK <> 1 OR H.DELAYMARK IS NULL) AND A.BILL_NO=D.BILL_NO)
              GROUP BY A.BILL_ID ";
            IMapper map = DatabaseInstance.Instance();
            return map.Query<SOHeaderEntity>(sql.ToString());
        }

        #endregion

        #region 生成车次信息
        public void CreateTrain(string whCode, string creator, string vehicleNo, string vehicleName, string vehiclePhone, StringBuilder strBuilder, List<SOHeaderEntity> list, List<UserEntity> listUsers, EWarehouseType warehouseType)
        {
            IMapper mapper = DatabaseInstance.Instance();
            string trainSO = String.Format("C{0}{1}{2}", whCode, DateTime.Now.ToString("yyyyMMddHHmmss"), new Random().Next(1000, 10000));
            IDbTransaction trans = mapper.BeginTransaction();
            int? isCase2 = 0;//散货件数
            int? isCase1 = 0;//整货件数
            string sql1 = "INSERT INTO wm_vehicle_train_header "
                        + "(WH_CODE,VH_TRAIN_NO,VH_NO,VEHICLE_NAME,USER_PHONE,BULK_CARGO_QTY,WHOLE_GOODS,STATE,SYNC_STATE,USER_NAME,UPDATE_DATE,RANDOM_CODE)"
                        + "VALUES('{0}','{1}','{2}','{3}','{4}',{5},{6},{7},{8},'{9}',NOW(),'{10}');";
            string sql2 = "INSERT INTO wm_vehicle_train_detail "
                        + "(VH_TRAIN_NO ,BILL_NO ,ORIGINAL_BILL_NO ,UPDATE_DATE) "
                        + " VALUES ('{0}','{1}','{2}',NOW())";

            string sql3 = "INSERT INTO wm_vehicle_train_users(VH_TRAIN_NO ,USER_NAME ,USER_CODE ,UPDATE_DATE) "
                          + "VALUES('{0}','{1}','{2}',NOW())";
            int ret = 0;
            string sql = "";
            //插入订单信息
            foreach (SOHeaderEntity entity in list)
            {
                //插入明细表
                sql = String.Format(sql2, trainSO, entity.BillNO, entity.OriginalBillNo);
                ret = mapper.Execute(sql, null, trans, CommandType.Text);
                if (ret <= 0)
                {
                    trans.Rollback();
                }
            }
            sql = "";
            //关联配送司机和助理
            foreach (UserEntity entity in listUsers)
            {
                sql = String.Format(sql3, trainSO, entity.UserName, entity.UserCode);
                ret = mapper.Execute(sql, null, trans, CommandType.Text);
                if (ret <= 0)
                {
                    trans.Rollback();
                }
            }
            GetIscaseQty(strBuilder, warehouseType, out isCase1, out isCase2);
            sql = String.Format(sql1, whCode, trainSO, vehicleNo, vehicleName, vehiclePhone, isCase2, isCase1, 1, 1, creator, new Random().Next(100000, 1000000));
            ret = mapper.Execute(sql, null, trans, CommandType.Text);
            if (ret <= 0)
            {
                trans.Rollback();
            }
            else
            {
                trans.Commit();
            }
        }

        public void GetIscaseQty(StringBuilder strBuilder, EWarehouseType warehouseType, out int? isCase1, out int? isCase2)
        {
            isCase1 = isCase2 = 0;
            string str = strBuilder.ToString();
            if (str.Length == 0)
            {
                isCase1 = isCase2 = 0;
            }
            str = str.Remove(str.Length - 1);
            IMapper mapper = DatabaseInstance.Instance();
            string sqlIsCase1 = String.Format("SELECT ROUND(SUM(A.PICK_QTY),0) FROM wm_so_detail A  WHERE A.IS_CASE = 1 AND  A.BILL_ID IN ({0}) ;", str);
            string sqlIsCase2 = string.Empty;
            if (warehouseType == EWarehouseType.混合仓)
                sqlIsCase2 = String.Format("SELECT COUNT(DISTINCT wc.CT_CODE) FROM wm_so_pick_record wspr INNER JOIN wm_container wc ON wc.CT_CODE = wspr.CT_CODE AND wc.CT_TYPE = '51' WHERE wspr.BILL_ID IN ({0});", str);
            else
                sqlIsCase2 = String.Format("SELECT COUNT(1) FROM WM_CONTAINER_RECORD A  WHERE A.BILL_HEAD_ID IN ({0});", str);
            object objIsCase1 = mapper.ExecuteScalar<object>(sqlIsCase1);
            object objIsCase2 = mapper.ExecuteScalar<object>(sqlIsCase2);
            isCase1 = ConvertUtil.ToInt(objIsCase1);
            isCase2 = ConvertUtil.ToInt(objIsCase2);
        }
        /// <summary>
        /// 车次关联司机  助理
        /// </summary>
        /// <returns></returns>
        public int CreateUsers(string trainNO, string userName, string userCode)
        {
            IMapper mapper = DatabaseInstance.Instance();
            string sql = String.Format("INSERT INTO wm_vehicle_train_users(VH_TRAIN_NO ,USER_NAME ,USER_CODE ,UPDATE_DATE) "
                                + "VALUES('{0}','{1}','{2}',NOW())", trainNO, userName, userCode);
            return mapper.Execute(sql);
        }
        /// <summary>
        /// 清除现有的人员数据
        /// </summary>
        /// <param name="trainNO"></param>
        /// <returns></returns>
        public int ClearUsers(string trainNO)
        {
            IMapper mapper = DatabaseInstance.Instance();
            string sql = String.Format("Delete from wm_vehicle_train_users where VH_TRAIN_NO='{0}';", trainNO);
            return mapper.Execute(sql);
        }


        /// <summary>
        /// 查询当前车次中车次信息的创建时间
        /// </summary>
        /// <param name="trainNO"></param>
        /// <returns></returns>
        public int GetVHCreateDate(string trainNO)
        {
            IMapper mapper = DatabaseInstance.Instance();
            string sql = String.Format("SELECT TIMESTAMPDIFF(HOUR,UPDATE_DATE,NOW())FROM WM_VEHICLE_TRAIN_HEADER WHERE VH_TRAIN_NO = '{0}'", trainNO);
            return ConvertUtil.ToInt(mapper.ExecuteScalar<Object>(sql));
        }

        /// <summary>
        /// 获取车次信息
        /// </summary>
        /// <param name="vehicleNO"></param>
        /// <returns></returns>
        public DataTable GetTrainSOMsg(string vehicleNO, DateTime beginDate, DateTime endDate)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = String.Format(
                "SELECT A.WH_CODE,A.VH_TRAIN_NO,A.VH_NO,A.VEHICLE_NAME,A.USER_PHONE,A.USER_NAME," +
                "A.BULK_CARGO_QTY,A.WHOLE_GOODS,A.STATE,case when A.SYNC_STATE=1 then '未上传' else " +
                "'已上传' end SYNC_STATE,A.UPDATE_DATE,A.RANDOM_CODE, A.CONFIRM_DATE " +
                "FROM wm_vehicle_train_header A " +
                "WHERE A.UPDATE_DATE BETWEEN @BeginDate AND @EndDate {0} " +
                "ORDER BY A.UPDATE_DATE DESC",
                vehicleNO == "ALL" ? string.Empty : "AND A.VH_NO='" + vehicleNO + "'");
            return map.LoadTable(sql, new { BeginDate = beginDate, EndDate = endDate });
        }
        /// <summary>
        /// 获取车次订单明细
        /// </summary>
        /// <param name="trainSO"></param>
        /// <returns></returns>
        public DataTable GetTrainSODetailMsg(string trainSO)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = String.Format(
                @"SELECT A.BILL_NO '订单号',D.IN_VH_SORT '装车顺序', A.ORIGINAL_BILL_NO '原始订单编号', 
    A.UPDATE_DATE '更新时间',B.SALES_MAN '业务员', B.CONTRACT_NO '联系电话', 
    c.C_NAME '客户名称',c.CONTACT '客户姓名',c.PHONE '客户电话' ,c.ADDRESS '客户地址' -- 
    FROM wm_vehicle_train_detail A 
    LEFT JOIN WM_VEHICLE_TRAIN_HEADER TH ON TH.VH_TRAIN_NO = A.VH_TRAIN_NO
    INNER JOIN wm_so_header B ON A.BILL_NO=B.BILL_NO 
    LEFT JOIN (SELECT DISTINCT D.BILL_NO, D.IN_VH_SORT, MAX(D.UPDATE_DATE)
                FROM WM_LOADING_DETAIL D 
                GROUP BY D.BILL_NO
                ORDER BY D.UPDATE_DATE DESC) D ON D.BILL_NO = B.BILL_NO
    LEFT JOIN customers c ON B.C_CODE=c.C_CODE 
    WHERE A.VH_TRAIN_NO='{0}' ORDER BY D.IN_VH_SORT ASC  ", trainSO);
            return map.LoadTable(sql);
        }
        public DataTable GetTrainSOUsersMsg(string trainSO)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = String.Format("SELECT  USER_NAME '用户名称',USER_CODE '用户编码',UPDATE_DATE '更新时间'  FROM wm_vehicle_train_users WHERE VH_TRAIN_NO='{0}';", trainSO);
            return map.LoadTable(sql);
        }
        public static List<UserEntity> GetTrainSOUserEntity(string trainSO)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = string.Format(
                "SELECT U.USER_ID, TU.USER_CODE, TU.USER_NAME, U.BRANCH_CODE, U.PWD, U.MOBILE_PHONE " +
                "FROM WM_VEHICLE_TRAIN_USERS TU " +
                "LEFT JOIN USERS U ON TU.USER_CODE = U.USER_CODE " +
                "WHERE TU.VH_TRAIN_NO = '{0}' ", trainSO);
            return map.Query<UserEntity>(sql);
        }
        #endregion

        /// <summary>
        /// 根据Detail主键ID修改订购数量
        /// </summary>
        /// <param name="detailID"></param>
        /// <returns></returns>
        public static int UpdateDetailQtyByID(int detailID, decimal qty)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "UPDATE WM_SO_DETAIL SET QTY = @Qty WHERE QTY = 0 AND ID = @DetailID";
            return map.Execute(sql, new { Qty = qty, DetailID = detailID });
        }

        /// <summary>
        /// 取消单据，并清除任务
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="userName"></param>
        public string CancelBill(int billID, string userName)
        {
            IMapper map = DatabaseInstance.Instance();
            DynamicParameters parm = new DynamicParameters();
            parm.Add("V_BILL_ID", billID);
            parm.Add("V_USER_NAME", userName);
            parm.AddOut("V_ERROR_MSG", DbType.String, 500);
            map.ExecuteScalar<string>("P_SO_CANCEL_BILL", parm, null, CommandType.StoredProcedure);
            return parm.Get<string>("V_ERROR_MSG");
        }

        #region zhangyj 打印装车单扣减库存
        public void PrintCutStock(int billID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("V_BILL_ID", billID);
            IMapper map = DatabaseInstance.Instance();
            map.Execute("P_SO_CUT_STOCK", param, CommandType.StoredProcedure);
        }
        #endregion

        #region 混合仓  2015-9-6 04:46:24
        public int SaveCheckWeightWLX(string ctCode, decimal weight, string userCode, string authUserCode,
            out int ctWeightIndex, out int ctTotalCount)
        {
            ctWeightIndex = ctTotalCount = 0;

            DynamicParameters parms = new DynamicParameters();
            parms.Add("V_WEIGHT", weight);
            parms.Add("V_CT_CODE", ctCode);
            parms.Add("V_USER_CODE", userCode);
            parms.Add("V_AUTH_USER_CODE", authUserCode);
            parms.AddOut("V_ORDER_INDEX", DbType.Int32, 4);
            parms.AddOut("V_CT_COUNT", DbType.Int32, 4);
            parms.AddOut("V_RESULT", DbType.Int32, 4);

            IMapper map = DatabaseInstance.Instance();
            map.Execute("P_SO_CONTAINER_WEIGHT_WLX", parms, CommandType.StoredProcedure);

            int result = parms.Get<int>("V_RESULT");
            if (result == 1)
            {
                ctWeightIndex = parms.Get<int>("V_ORDER_INDEX");
                ctTotalCount = parms.Get<int>("V_CT_COUNT");
            }

            return result;
        }

        public DataTable GetContainerWeightByBillID(int billID)
        {
            string sql = "SELECT S.BILL_HEAD_ID, S.CT_CODE, S.LAST_UPDATETIME, (S.GROSS_WEIGHT/1000) GROSS_WEIGHT, (IFNULL(SUM(R.PICK_QTY * WUS.WEIGHT/WUS.QTY), 0) + C.CT_WEIGHT)/1000 CALC_WEIGHT " +
                "FROM WM_CONTAINER_STATE S " +
                "INNER JOIN WM_CONTAINER C ON S.CT_CODE = C.CT_CODE " +
                "LEFT JOIN WM_SO_PICK_RECORD R ON S.BILL_HEAD_ID = R.BILL_ID AND S.CT_CODE = R.CT_CODE " +
                "LEFT JOIN WM_SO_PICK P ON P.ID = R.PICK_ID " +
                "LEFT JOIN WM_SO_DETAIL D ON P.DETAIL_ID = D.ID " +
                "LEFT JOIN WM_UM_SKU WUS ON D.SKU_CODE = WUS.SKU_CODE AND D.UM_CODE = WUS.UM_CODE " +
                "WHERE S.BILL_HEAD_ID = @BillID AND C.CT_TYPE = '51' " +
                "GROUP BY S.BILL_HEAD_ID, S.CT_CODE, S.LAST_UPDATETIME " +
                "ORDER BY S.LAST_UPDATETIME ASC ";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { BillID = billID });
        }

        /// <summary>
        /// 获取物流箱内商品信息
        /// </summary>
        /// <param name="billID">订单ID</param>
        /// <param name="ctCode">物流箱ID</param>
        /// <returns></returns>
        public DataTable GetContainerSKUMIX(int billID, string ctCode)
        {
            string sql = "SELECT WS.SKU_NAME, WUS.SKU_BARCODE, WSPR.PICK_QTY/WUS.QTY QTY, " +
                "WUS.WEIGHT, IFNULL(WSPR.PICK_QTY * WUS.WEIGHT/WUS.QTY, 0) AS LINE_WEIGHT, WU.UM_NAME " +
                "FROM WM_SO_PICK_RECORD WSPR " +
                "INNER JOIN WM_SO_PICK WSP ON WSP.ID = WSPR.PICK_ID " +
                "INNER JOIN WM_SO_DETAIL D ON WSP.DETAIL_ID = D.ID " +
                "INNER JOIN WM_UM_SKU WUS ON D.SKU_CODE = WUS.SKU_CODE AND D.UM_CODE = WUS.UM_CODE " +
                "INNER JOIN WM_SKU WS ON D.SKU_CODE = WS.SKU_CODE " +
                "INNER JOIN WM_UM WU ON D.UM_CODE = WU.UM_CODE " +
                "WHERE WSPR.BILL_ID = @BillID AND WSPR.CT_CODE = @CtCode";
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql, new { BillID = billID, CtCode = ctCode });
        }

        #endregion 混合仓

        /// <summary>
        /// 删除拣货计划临时数据
        /// </summary>
        /// <returns></returns>
        public static int DeletePickTemp()
        {
            string sql = "DELETE FROM WM_PICK_TEMP;DELETE FROM WM_PICK_TEMP_ERROR;";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql);
        }

        public int GetWeightRecordsCountByBillID(int billID, string ctCode)
        {
            string sql = @"SELECT COUNT(1) FROM WM_SO_WEIGHT W 
                            WHERE W.BILL_ID = @BillID AND W.CT_CODE <> @CTCode";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new { BillID = billID, CTCode = ctCode });
        }

        /// <summary>
        /// 获取一个单据的容器记录
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public DataTable GetContainerByBillID(int billID)
        {
            string sql = string.Format("  SELECT A.BILL_ID,A.BILL_NO, B.VH_TRAIN_NO, C.CT_CODE, e.ITEM_DESC CT_STATE, C.LC_CODE  " +
                                     " FROM wm_so_header A LEFT JOIN wm_vehicle_train_detail B ON A.BILL_NO = B.BILL_NO    " +
                                     "     LEFT JOIN wm_container_state C ON A.BILL_ID = C.BILL_HEAD_ID   " +
                                     "     LEFT JOIN wm_container D ON C.CT_CODE = D.CT_CODE   " +
                                     "     LEFT JOIN wm_base_code e ON C.CT_STATE = e.ITEM_VALUE   " +
                               "  WHERE A.BILL_ID = {0} AND D.CT_TYPE = '50' GROUP BY CT_CODE  " +
                                "       UNION ALL  " +
                                "       SELECT A.BILL_ID,A.BILL_NO, B.VH_TRAIN_NO, C1.CT_CODE, e.ITEM_DESC CT_STATE, C2.LC_CODE   " +
                                "      FROM wm_so_header A LEFT JOIN wm_vehicle_train_detail B ON A.BILL_NO = B.BILL_NO    " +
                                "          LEFT JOIN wm_container_state C1 ON A.BILL_ID = C1.BILL_HEAD_ID   " +
                                "          LEFT JOIN wm_container_state C2 ON c1.LC_CODE = C2.CT_CODE  " +
                                "          LEFT JOIN wm_container D ON C1.CT_CODE = D.CT_CODE   " +
                                "         LEFT JOIN wm_base_code e ON C1.CT_STATE = e.ITEM_VALUE   " +
                                " WHERE A.BILL_ID = {0} AND D.CT_TYPE = '51' GROUP BY CT_CODE  ", billID);
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql);
        }
        /// <summary>
        /// 获取活动状态的集合
        /// </summary>
        /// <param name="groupCode"></param>
        /// <returns></returns>
        public static List<BaseCodeEntity> GetStatusList()
        {
            IMapper map = DatabaseInstance.Instance();

            string sql = "SELECT GROUP_CODE, ITEM_VALUE, ITEM_DESC, IS_ACTIVE, REMARK FROM WM_BASE_CODE WHERE IS_ACTIVE = 'Y'";
            return map.Query<BaseCodeEntity>(sql);
        }
    }
}
