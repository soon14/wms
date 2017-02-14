using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Entities;
using Nodes.Dapper;
using System.Data;
using Nodes.Utils;

namespace Nodes.DBHelper
{
    public class CycleCountDal
    {
        /// <summary>
        /// 保存盘点单
        /// </summary>
        /// <param name="remark"></param>
        /// <param name="userName"></param>
        /// <param name="locations"></param>
        /// <returns></returns>
        public int SaveCountBill(string remark, string userName, string warehouse, List<LocationEntity> locations, string tagCode)
        {
            IMapper map = DatabaseInstance.Instance();

            //保存表头
            string sql = string.Format("INSERT INTO WM_COUNT_HEADER(BILL_STATE, REMARK, CREATOR, CREATE_DATE, WH_CODE, TAG) " +
                "VALUES(@BillState, @Remark, @Creator, {0}, @Warehouse, @Tag)", map.GetSysDateString());

            map.Execute(sql, new
            {
                BillState = BaseCodeConstant.COUNT_STATE_NOT_START,
                Remark = remark,
                Creator = userName,
                Warehouse = warehouse,
                Tag = tagCode
            });

            int id = map.GetAutoIncreasementID("WM_COUNT_HEADER", "BILL_ID");
            sql = "INSERT INTO WM_COUNT_LOCATION(BILL_ID, LC_CODE, LC_STATE) VALUES(@BillID, @Location, @LocationState)";
            foreach (LocationEntity location in locations)
                map.Execute(sql, new { BillID = id, Location = location.LocationCode, LocationState = "未完成" });

            return id;
        }

        public void SaveCountTask(string userName, int billID, string userCode, List<CountDetailEntity> locations)
        {
            IMapper map = DatabaseInstance.Instance();

            //先删除该盘点员在此盘点单已分派的货位，然后重新分派
            //string sql = "SELECT ID FROM TASKS WHERE BILL_ID = @BillID AND USER_CODE = @UserCode";
            //object objTaskID = map.ExecuteScalar<object>(sql, new { BillID = billID, UserCode = userCode });
            //if (objTaskID != null && objTaskID != DBNull.Value)
            //{
            //    map.Execute(string.Format("DELETE FROM TASK_DETAIL WHERE TASK_ID = {0}", ConvertUtil.ObjectToNull(objTaskID)));
            //    map.Execute(string.Format("DELETE FROM TASKS WHERE ID = {0}", ConvertUtil.ObjectToNull(objTaskID)));
            //}
            #region #差异# 自动调度(D01/D02/D05) 将 userCode 改为 null
            //重新写入
            map.Execute(string.Format("INSERT INTO TASKS(TASK_TYPE, BILL_ID, USER_CODE, QTY, CREATE_DATE, CREATOR, TASK_DESC) " +
                "VALUES('140', {0}, '{1}', {2}, NOW(), '{3}', '个货位')", billID, null, locations.Count, userName));
            #endregion

            //得到最新的任务编号
            int taskID = map.GetAutoIncreasementID("TASKS", "TASK_ID");

            foreach (CountDetailEntity loc in locations)
            {
                map.Execute(string.Format("INSERT INTO TASK_DETAIL(TASK_ID, ACTION_ID) VALUES({0}, {1})",
                    taskID, loc.ID));
            }
            new SODal().AutoAssignTask();
        }

        /// <summary>
        /// 获取一个盘点单的信息
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public CountHeaderEntity GetBillInfo(int billID)
        {
            IMapper map = DatabaseInstance.Instance();

            string sql = "SELECT BILL_ID, BILL_STATE, REMARK, CREATOR, CREATE_DATE, COMPLETE_DATE, WH_CODE FROM WM_COUNT_HEADER WHERE BILL_ID = @BillID";
            return map.QuerySingle<CountHeaderEntity>(sql, new { BillID = billID });
        }

        /// <summary>
        /// 根据条件查询盘点单
        /// </summary>
        /// <param name="warehouse"></param>
        /// <param name="billNO"></param>
        /// <param name="billStatus"></param>
        /// <param name="Creator"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public List<CountHeaderEntity> QueryBills(string warehouse, string billNO,
          string billStatus, bool showNotComplete, DateTime? dateFrom, DateTime? dateTo)
        {
            string whereCondition = string.Format("WHERE A.WH_CODE = '{0}' ", warehouse);
            if (!string.IsNullOrEmpty(billNO))
                whereCondition += string.Format(" AND A.BILL_ID = '{0}'", billNO);

            if (showNotComplete)
                whereCondition += string.Format(" AND A.BILL_STATE < '{0}'", BaseCodeConstant.COUNT_STATE_CLOSE);
            else if (!string.IsNullOrEmpty(billStatus))
                whereCondition += string.Format(" AND ({0})", DBUtil.FormatParameter("A.BILL_STATE", billStatus));

            if (dateFrom != null)
                whereCondition += string.Format(" AND A.CREATE_DATE >= '{0}'", dateFrom);
            if (dateTo != null)
                whereCondition += string.Format(" AND A.CREATE_DATE <= '{0}'", dateTo);

            string sql = @"SELECT A.BILL_ID, A.BILL_STATE, A.REMARK, A.CREATOR, A.CREATE_DATE, A.WH_CODE, 
  (CASE WHEN C.ITEM_DESC IS NULL THEN A.BILL_STATE ELSE C.ITEM_DESC END) STATE_NAME, 
  BC.ITEM_DESC TAG_DESC  
FROM WM_COUNT_HEADER A 
LEFT JOIN WM_BASE_CODE C ON C.ITEM_VALUE = A.BILL_STATE 
LEFT JOIN WM_BASE_CODE BC ON BC.ITEM_VALUE = A.TAG " + whereCondition;
            IMapper map = DatabaseInstance.Instance();
            return map.Query<CountHeaderEntity>(sql);
        }

        public List<CountHeaderEntity> GetBills(string warehouse, string billStatus)
        {
            string whereCondition = string.Format("WHERE A.WH_CODE = '{0}' ", warehouse);

            if (!string.IsNullOrEmpty(billStatus))
                whereCondition += string.Format(" AND ({0})", DBUtil.FormatParameter("A.BILL_STATE", billStatus));

            string sql = "SELECT A.BILL_ID, A.BILL_STATE, A.REMARK, A.CREATOR, A.CREATE_DATE, A.WH_CODE " +
                "FROM WM_COUNT_HEADER A " + whereCondition;
            IMapper map = DatabaseInstance.Instance();
            return map.Query<CountHeaderEntity>(sql);
        }

        public List<CountDetailEntity> GetCountLocation(int billID)
        {
            IMapper map = DatabaseInstance.Instance();

            string sql = @"SELECT D.ID, D.BILL_ID, D.LC_CODE, D.LC_STATE, Z.ZN_CODE, Z.ZN_NAME, 
  L.PASSAGE_CODE, L.FLOOR_CODE, L.CELL_CODE, L.SHELF_CODE 
  FROM WM_COUNT_LOCATION D INNER JOIN WM_LOCATION L ON D.LC_CODE = L.LC_CODE 
  INNER JOIN WM_ZONE Z ON L.ZN_CODE = Z.ZN_CODE 
  WHERE D.BILL_ID = @BillID ORDER BY D.LC_CODE ;";
            return map.Query<CountDetailEntity>(sql, new { BillID = billID });
        }

        public List<CountDetailEntity> GetCountLocations(int billID)
        {
            IMapper map = DatabaseInstance.Instance();

            string sql = @"SELECT D.ID, D.BILL_ID, D.LC_CODE, D.LC_STATE, Z.ZN_CODE, Z.ZN_NAME, 
  L.PASSAGE_CODE, L.FLOOR_CODE, L.CELL_CODE, L.SHELF_CODE 
  FROM WM_COUNT_LOCATION D INNER JOIN WM_LOCATION L ON D.LC_CODE = L.LC_CODE 
  INNER JOIN WM_ZONE Z ON L.ZN_CODE = Z.ZN_CODE 
  WHERE D.BILL_ID = @BillID AND D.LC_CODE NOT IN(
SELECT DISTINCT L.LC_CODE FROM (
  SELECT T.ID, T.TASK_TYPE, T.BILL_ID, T.USER_CODE, T.QTY FROM TASKS T 
  UNION
  SELECT H.ID, H.TASK_TYPE, H.BILL_ID, H.USER_CODE, H.QTY FROM TASK_HISTORY H) T
  LEFT JOIN TASK_DETAIL D ON T.ID = D.TASK_ID
  LEFT JOIN WM_COUNT_LOCATION L ON D.ACTION_ID = L.ID
  WHERE T.TASK_TYPE = '140' AND T.BILL_ID = @BillID);";
            return map.Query<CountDetailEntity>(sql, new { BillID = billID });
        }

        public List<CountDetailEntity> GetCountRecords(int billID)
        {
            IMapper map = DatabaseInstance.Instance();

            string sql = "SELECT D.ID, D.BILL_ID, D.LC_CODE, D.SKU_BARCODE, D.SKU_CODE, SKU.SKU_NAME, D.QTY, D.CREATE_DATE, Z.ZN_CODE, Z.ZN_NAME, D.USER_CODE, U.USER_NAME " +
                "FROM WM_COUNT_RECORD D " +
                "INNER JOIN WM_LOCATION L ON D.LC_CODE = L.LC_CODE " +
                "INNER JOIN WM_SKU SKU ON D.SKU_CODE = SKU.SKU_CODE " +
                "INNER JOIN WM_ZONE Z ON L.ZN_CODE = Z.ZN_CODE " +
                "INNER JOIN USERS U ON D.USER_CODE = U.USER_CODE " +
                "WHERE D.BILL_ID = @BillID";
            return map.Query<CountDetailEntity>(sql, new { BillID = billID });
        }

        /// <summary>
        /// 跟库存实时比对，显示报告
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public DataTable GetReportVsStock(int billID)
        {
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable("P_COUNT_REPORT", new { V_BILL_ID = billID }, CommandType.StoredProcedure);
        }

        public DataTable GetReportOnlyDiff(int billID)
        {
            string sql = string.Format("SELECT R.ID, R.BILL_ID, R.LC_CODE, Z.ZN_NAME, R.SKU_CODE, UM.UM_NAME, SKU.SKU_NAME, SKU.SPEC, IFNULL(R.COUNT_QTY,0) COUNT_QTY,IFNULL(R.STOCK_QTY,0) STOCK_QTY, R.REMARK, R.UPLOADED, R.STOCK_EXP_DATE, R.EXP_DATE, " +
                "CASE R.UPLOADED WHEN 1 THEN '通过' ELSE '未通过' END IS_EFFECT " +
                "FROM WM_COUNT_REPORT R " +
                "INNER JOIN WM_LOCATION L ON R.LC_CODE = L.LC_CODE " +
                "INNER JOIN WM_ZONE Z ON L.ZN_CODE = Z.ZN_CODE " +
                "INNER JOIN WM_SKU SKU ON SKU.SKU_CODE = R.SKU_CODE " +
                "LEFT JOIN WM_UM_SKU U ON U.SKU_CODE = R.SKU_CODE AND U.SKU_LEVEL = 1 " +
                "LEFT JOIN WM_UM UM ON UM.UM_CODE = U.UM_CODE " +
                "WHERE BILL_ID = {0} and (R.STOCK_QTY <> R.COUNT_QTY or R.STOCK_EXP_DATE <> R.EXP_DATE OR R.STOCK_EXP_DATE IS NULL); ", billID);
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(sql);
        }

        public int SaveReportDetail(DataRow row)
        {
            string sql = "UPDATE WM_COUNT_REPORT SET REMARK = @Remark, UPLOADED = @Uploaded WHERE ID = @Id";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new
            {
                Remark = ConvertUtil.ToString(row["REMARK"]),
                Uploaded = ConvertUtil.ToBool(row["UPLOADED"]),
                Id = ConvertUtil.ToInt(row["ID"])
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="warehouse"></param>
        /// <returns>1表示成功，-1表示未找到最小单位</returns>
        public int ExecuteStock(int id, string warehouse)
        {
            IMapper map = DatabaseInstance.Instance();
            DynamicParameters dyna = new DynamicParameters();
            dyna.Add("V_ID", id);
            dyna.Add("V_WAREHOUSE", warehouse);
            dyna.AddOut("V_RESULT", DbType.Int32);
            map.Execute("P_COUNT_STOCK", dyna, CommandType.StoredProcedure);
            return dyna.Get<int>("V_RESULT");
        }

        public int UpdateBillState(int billID, string billState)
        {
            string sql = "UPDATE WM_COUNT_HEADER SET BILL_STATE = @BillState, LAST_UPDATETIME = @LastUpdateTime " +
                "WHERE BILL_ID = @BillID";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new { BillState = billState, LastUpdateTime = DateTime.Now, BillID = billID });
        }

        public int UpdateBillSyncState(int billID, string syncState)
        {
            string sql = string.Format("UPDATE WM_COUNT_HEADER SET SYNC_STATE = '{0}' WHERE BILL_ID = {1}", syncState, billID);
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql);
        }

        /// <summary>
        /// 完成盘点，关闭
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int CompleteBill(int billID, string userName)
        {
            IMapper map = DatabaseInstance.Instance();
            DynamicParameters P = new DynamicParameters();
            P.Add("V_BILL_ID", billID);
            P.Add("V_USER_NAME", userName);
            P.AddOut("V_RESULT", DbType.Int32);

            map.Execute("P_COUNT_COMPLETE", P, CommandType.StoredProcedure);
            return P.Get<int>("V_RESULT");
        }

        /// <summary>
        /// 列出今天库存发生变动的货位
        /// </summary>
        /// <param name="warehouse"></param>
        /// <returns></returns>
        public List<LocationEntity> ListChangedLocations(DateTime fromDate, DateTime toDate)
        {
            IMapper map = DatabaseInstance.Instance();
            //string sql = "SELECT DISTINCT S.LC_CODE, Z.ZN_NAME FROM WM_STOCK S " +
            //    "INNER JOIN WM_LOCATION L ON L.LC_CODE = S.LC_CODE " +
            //    "INNER JOIN WM_ZONE Z ON L.ZN_CODE = Z.ZN_CODE " +
            //    "WHERE (S.LATEST_IN >= @FromDate AND S.LATEST_IN <= @ToDate) OR (S.LATEST_OUT >= @FromDate AND S.LATEST_OUT <= @ToDate)";
            string sql = @"SELECT DISTINCT S.LC_CODE, Z.ZN_NAME, A.BILL_ID, A.BILL_STATE FROM WM_STOCK S 
  INNER JOIN WM_LOCATION L ON L.LC_CODE = S.LC_CODE 
  INNER JOIN WM_ZONE Z ON L.ZN_CODE = Z.ZN_CODE 
  LEFT JOIN (SELECT H.BILL_ID, (CASE WHEN C.ITEM_DESC IS NULL THEN H.BILL_STATE ELSE C.ITEM_DESC END) BILL_STATE, L.LC_CODE 
              FROM WM_COUNT_HEADER H
              LEFT JOIN WM_COUNT_LOCATION L ON L.BILL_ID = H.BILL_ID
              LEFT JOIN WM_BASE_CODE C ON C.ITEM_VALUE = H.BILL_STATE
              WHERE H.BILL_STATE IN ('130', '131', '等待差异调整')) A ON A.LC_CODE = L.LC_CODE
  WHERE (S.LATEST_IN >= @FromDate AND S.LATEST_IN <= @ToDate) OR (S.LATEST_OUT >= @FromDate AND S.LATEST_OUT <= @ToDate)
  ORDER BY A.BILL_STATE";
            return map.Query<LocationEntity>(sql, new { FromDate = fromDate, ToDate = toDate });
        }
        #region 彭伟 2015-08-20

        public static DateTime? GetBeginDate(int taskID)
        {
            string sql = "SELECT MIN(R.CREATE_DATE) FROM TASK_DETAIL D " +
                "LEFT JOIN WM_COUNT_RECORD R ON R.BILL_ID = D.ACTION_ID " +
                "WHERE D.TASK_ID = " + taskID;
            IMapper map = DatabaseInstance.Instance();
            return map.ExecuteScalar<DateTime>(sql);
        }

        #endregion

        #region 张忠平 2016-03-09

        /// <summary>
        /// 获取当前盘点差异单据的明细
        /// </summary>
        /// <param name="billNo"></param>
        /// <returns></returns>
        public List<LocationEntity> ListGetLocations(int billNo) 
        {
            string sql = string.Format("SELECT LC_CODE, COUNT_QTY, STOCK_QTY, EXP_DATE, STOCK_EXP_DATE FROM WM_COUNT_REPORT WHERE BILL_ID = {0} GROUP BY LC_CODE ", billNo);

            IMapper map = DatabaseInstance.Instance();
            return map.Query<LocationEntity>(sql);
        }
        #endregion
    }
}

