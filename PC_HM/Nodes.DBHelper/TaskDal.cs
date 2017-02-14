using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Entities;
using Nodes.Dapper;
using System.Data;
using Nodes.Utils;
using Nodes.DBHelper;

namespace Nodes.DBHelper
{
    public class TaskDal
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="billID">单据编号</param>
        /// <param name="taskType">任务类型（要干什么）：盘点（140)、移库(141)、上架(142)、下架(143)、补货任务(144)</param>
        /// <returns>0:参数没设置对；1：成功；-1：尚未上班，无签到记录；-2：需要配置角色；-3：未查到单据；-4：单据状态不对</returns>
        public static string Schedule(int billID, string taskType)
        {
            IMapper map = DatabaseInstance.Instance();
            DynamicParameters p = new DynamicParameters();
            p.Add("@V_BILL_ID", billID);
            p.Add("@V_TASK_TYPE", taskType);
            p.AddOut("@V_RESULT", DbType.String);

            map.Execute("P_TASK_SCHEDULE", p, CommandType.StoredProcedure);
            return p.Get<string>("@V_RESULT");
        }
        /// <summary>
        /// 获取任务池当前状态信息
        /// </summary>
        /// <returns></returns>
        public static List<TaskEntity> GetCurrentTask()
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT A.ID, A.TASK_TYPE, C.ITEM_DESC, A.BILL_ID, A.USER_CODE, U.USER_NAME, A.QTY, A.CREATE_DATE, A.TASK_DESC " +
                "FROM TASKS A " +
                "LEFT JOIN USERS U ON A.USER_CODE = U.USER_CODE " +
                "INNER JOIN WM_BASE_CODE C ON A.TASK_TYPE = C.ITEM_VALUE ";
            return map.Query<TaskEntity>(sql);
        }
        /// <summary>
        /// 获取任务池当前状态信息
        /// </summary>
        /// <returns></returns>
        public static List<TaskEntity> GetCurrentTaskNew()
        {
            IMapper map = DatabaseInstance.Instance();
            //string sql = "SELECT A.ID, A.TASK_TYPE, C.ITEM_DESC, A.BILL_ID,wsh.BILL_NO,wbc.ITEM_DESC BILL_DESC, A.USER_CODE, U.USER_NAME, A.QTY, A.CREATE_DATE, A.TASK_DESC "
            //    + "FROM TASKS A "
            //    + "LEFT JOIN USERS U ON A.USER_CODE = U.USER_CODE "
            //    + "JOIN task_detail td ON A.ID=td.TASK_ID "
            //    + "JOIN wm_so_header wsh ON td.ACTION_ID=wsh.BILL_ID "
            //    + "JOIN wm_base_code wbc ON wsh.BILL_STATE=wbc.ITEM_VALUE "
            //    + "INNER JOIN WM_BASE_CODE C ON A.TASK_TYPE = C.ITEM_VALUE ";
            string sql = @"SELECT DISTINCT OL.USER_CODE,u.USER_NAME, TSK.ID, TSK.TASK_TYPE,
    CONCAT(C.ITEM_DESC,(CASE WHEN (TSK.IS_CASE=2) THEN '(散)' ELSE  '' END)) AS 'ITEM_DESC',
    C1.ITEM_DESC BILL_TYPE_DESC, TSK.BILL_ID,TSK.CONFIRM_DATE,
    (CASE WHEN TSK.TASK_TYPE = '143' THEN wsh.BILL_NO WHEN TSK.TASK_TYPE = '148' THEN wlh.VH_TRAIN_NO
      ELSE WAH.BILL_NO END) AS 'BILL_NO',
    wbc.ITEM_DESC BILL_DESC, TSK.QTY, TSK.CREATE_DATE, TSK.TASK_DESC, TSK.IS_CASE
  FROM USER_ONLINE OL 
  LEFT JOIN USER_ROLE UR ON OL.USER_CODE = UR.USER_CODE
  LEFT JOIN ROLES R ON UR.ROLE_ID = R.ROLE_ID
  LEFT JOIN TASKS TSK ON OL.USER_CODE = TSK.USER_CODE
  INNER JOIN users u ON OL.USER_CODE=u.USER_CODE
  LEFT JOIN wm_so_header wsh ON TSK.BILL_ID=wsh.BILL_ID
  LEFT JOIN wm_asn_header wah ON TSK.BILL_ID =wah.BILL_ID
  LEFT JOIN wm_loading_header wlh ON WLH.ID = TSK.BILL_ID
  LEFT JOIN wm_base_code wbc ON wsh.BILL_STATE=wbc.ITEM_VALUE 
  LEFT JOIN WM_BASE_CODE C ON TSK.TASK_TYPE = C.ITEM_VALUE
  LEFT JOIN WM_BASE_CODE C1 ON wsh.BILL_TYPE = C1.ITEM_VALUE
  WHERE OL.IS_ONLINE = 'Y' AND u.IS_ACTIVE='Y' ORDER BY TSK.CREATOR DESC";
            return map.Query<TaskEntity>(sql);
        }
        /// <summary>
        /// 获取任务详情
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public DataTable GetTaskDetail(int taskID)
        {
            IMapper map = DatabaseInstance.Instance();
            DynamicParameters p = new DynamicParameters();
            p.Add("@V_TASK_ID", taskID);
            p.AddOut("@V_RESULT_MSG", DbType.String);
            return map.LoadTable("P_TASK_GET_DETAIL", p, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取当前符合任务角色的所有用户
        /// </summary>
        /// <param name="rowName"></param>
        /// <returns></returns>
        public DataSet GetAllUsers(string roleName)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql1 = "SELECT OL.USER_CODE,u.USER_NAME , COUNT(TSK.QTY) TASKCOUNT,wbc.ITEM_DESC "
                        + "FROM USER_ONLINE OL "
                        + "LEFT JOIN USER_ROLE UR ON OL.USER_CODE = UR.USER_CODE "
                        + "LEFT JOIN ROLES R ON UR.ROLE_ID = R.ROLE_ID "
                        + "LEFT JOIN TASKS TSK ON OL.USER_CODE = TSK.USER_CODE "
                        + "LEFT JOIN wm_base_code wbc ON TSK.TASK_TYPE=wbc.ITEM_VALUE "
                        + "LEFT JOIN users u ON OL.USER_CODE=u.USER_CODE "
                        + "WHERE OL.IS_ONLINE = 'Y' AND R.ROLE_NAME = '{0}' "
                        + "GROUP BY wbc.ITEM_DESC";
            DataTable dt1 = map.LoadTable(String.Format(sql1, roleName));
            dt1.TableName = "CONG";

            string sql2 = "SELECT OL.USER_CODE,u.USER_NAME,''  QTY FROM USER_ONLINE OL "
                + "LEFT JOIN USER_ROLE UR ON OL.USER_CODE = UR.USER_CODE "
                + "LEFT JOIN ROLES R ON UR.ROLE_ID = R.ROLE_ID "
                + "left JOIN users u ON OL.USER_CODE=u.USER_CODE "
            + "WHERE OL.IS_ONLINE = 'Y' AND R.ROLE_NAME = '{0}'";
            DataSet ds = map.LoadDataSet(String.Format(sql2, roleName));
            ds.Tables[0].TableName = "ZHU";
            ds.Tables.Add(dt1.Copy());
            return ds;
        }
        /// <summary>
        /// 改变任务
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="dic"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string TaskChange(string userCode, Dictionary<string, int> dic, TaskEntity entity)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "";
            if (entity.TaskType == "143")//拣货任务 更改直接更新update就可以了
            {
                foreach (string str in dic.Keys)
                {
                    sql += String.Format("UPDATE tasks t SET t.USER_CODE='{0}' WHERE t.ID={1};", str, entity.TaskID);
                }
            }
            else
            {
                sql = String.Format("delete from tasks where ID={0};", entity.TaskID);
                string sql2 = "INSERT INTO tasks(TASK_TYPE ,BILL_ID ,USER_CODE ,QTY ,CREATE_DATE ,CREATOR ,TASK_DESC ) "
                            + "VALUES(  '{0}'  ,{1} ,'{2}' ,{3}  ,NOW()  ,'{4}'  ,'{5}'   );";
                foreach (string str in dic.Keys)
                {
                    sql += String.Format(sql2, entity.TaskType, entity.BillID, str, dic[str], userCode, entity.TaskDesc);
                }
            }
            string result = "";
            IDbTransaction trans = map.BeginTransaction();
            try
            {
                result = map.Execute(sql).ToString();
                trans.Commit();
                result = "Y";
            }
            catch (Exception ex)
            {
                trans.Rollback();
                result = ex.Message;
            }

            return result;

        }
        /// <summary>
        /// 获取任务对应的订单状态
        /// </summary>
        /// <param name="taskType"></param>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public string TaskState(string taskType, int taskID)
        {
            IMapper map = DatabaseInstance.Instance();
            DynamicParameters p = new DynamicParameters();
            p.Add("@V_TASK_TYPE", taskType);
            p.Add("@V_TASK_ID", taskID);
            p.AddOut("@V_RESULT_MSG", DbType.String);
            map.Execute("P_TASK_GET_STATE", p, CommandType.StoredProcedure);
            return p.Get<string>("@V_RESULT_MSG");
        }

        #region 2015-06-01
        public int CloseTask(TaskEntity entity)
        {
            IMapper map = DatabaseInstance.Instance();
            DynamicParameters p = new DynamicParameters();
            p.Add("@V_TASK_ID", entity.TaskID);
            p.AddOut("@V_RESULT", DbType.Int32);
            map.Execute("P_SO_CLOSE_PICK_TASK_HAND", p, CommandType.StoredProcedure);
            return p.Get<Int32>("@V_RESULT");
        }

        #endregion

        #region 2015-07-30 彭伟
        /// <summary>
        /// 根据任务ID 获取订单明细
        /// </summary>
        /// <param name="taskID">任务 ID</param>
        /// <returns></returns>
        public List<SODetailEntity> GetDetailsByTaskID(int taskID)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT D.ID, D.BILL_ID, D.ROW_NO, D.SKU_CODE, D.SPEC, WS.SKU_NAME, WUS.SKU_BARCODE, " +
                "IFNULL(D.SUIT_NUM,1) SUIT_NUM, D.COM_MATERIAL, D.QTY, D.UM_CODE, UM.UM_NAME, D.DUE_DATE, " +
                "D.BATCH_NO, D.PRICE, D.REMARK, D.PICK_QTY, IS_CASE " +
                "FROM TASK_DETAIL TD " +
                "INNER JOIN WM_SO_DETAIL D ON TD.ACTION_ID = D.BILL_ID " +
                "LEFT JOIN WM_SKU WS ON WS.SKU_CODE = D.SKU_CODE " +
                "INNER JOIN WM_UM UM ON UM.UM_CODE = D.UM_CODE " +
                "LEFT JOIN WM_UM_SKU WUS ON D.SKU_CODE = WUS.SKU_CODE AND D.UM_CODE = WUS.UM_CODE " +
                "WHERE TD.TASK_ID = " + taskID;
            return map.Query<SODetailEntity>(sql);
        }
        #endregion

        #region zhangyj 查询等待分配任务的单据
        public static DataTable GetTask62()
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = @"SELECT T.ID, t.TASK_TYPE,tl.TASK_LEVEL,tl.begin_time, CONCAT(wbc.ITEM_DESC,(CASE WHEN (IS_CASE=2) THEN '(散)' ELSE  '' END)) AS 'ITEM_DESC',
  (CASE WHEN T.TASK_TYPE = '143' THEN WSH.BILL_NO WHEN T.TASK_TYPE = '148' THEN WLH.VH_TRAIN_NO ELSE
    T.BILL_ID END) BILL_NO,
  t.CREATE_DATE, wbc.Attri1 ATTRI 
  FROM tasks t
  JOIN wm_base_code wbc ON t.TASK_TYPE=wbc.ITEM_VALUE
  JOIN task_level tl ON t.TASK_TYPE=tl.TASK_TYPE
  LEFT JOIN wm_so_header wsh ON t.BILL_ID=wsh.BILL_ID
  LEFT JOIN WM_LOADING_HEADER WLH ON WLH.ID = t.BILL_ID
  WHERE (t.USER_CODE IS NULL OR t.USER_CODE ='') AND ((TIME_TO_SEC(TL.BEGIN_TIME) < TIME_TO_SEC(CURTIME()) 
                          AND TIME_TO_SEC(CURTIME()) <= TIME_TO_SEC(tl.BEGIN_TIME) + tl.DIFF_VALUE * 60)
                        OR (TIME_TO_SEC(tl.BEGIN_TIME) > TIME_TO_SEC(CURTIME()) AND TIME_TO_SEC(tl.BEGIN_TIME) > TIME_TO_SEC(tl.END_TIME)
                          AND TIME_TO_SEC(CURTIME()) <= TIME_TO_SEC(TL.END_TIME)))
  ORDER BY tl.TASK_LEVEL DESC,t.STICK_DATE ASC";
            return map.LoadTable(sql);
        }

        public static DataTable GetBillCount()
        {
            IMapper map = DatabaseInstance.Instance();
            //            string sql = @"SELECT  wbc.ITEM_DESC,COUNT(wsh.BILL_ID) QTY,'查看' 'OPERATION','发货单状态' AS T FROM wm_base_code wbc
            //                      LEFT JOIN wm_so_header wsh ON wbc.ITEM_VALUE=wsh.BILL_STATE
            //                      WHERE wbc.REMARK='发货单状态' AND wbc.ITEM_VALUE < 67
            //                      GROUP BY wbc.ITEM_DESC
            //                      ORDER BY wbc.ITEM_VALUE ASC  ;  ";
            string sql = @"SELECT
  A.ITEM_DESC,
  A.QTY,
  A.OPERATION,
  A.T
FROM (SELECT
    wbc.ITEM_DESC,
    wbc.ITEM_VALUE,
    COUNT(wsh.BILL_ID) QTY,
    '' AS 'OPERATION',
    '发货单状态' AS T,
    '1' SORT
  FROM wm_base_code wbc
    LEFT JOIN wm_so_header wsh
      ON wbc.ITEM_VALUE = wsh.BILL_STATE
  WHERE wbc.REMARK = '发货单状态' AND wbc.ITEM_VALUE < 67
  GROUP BY wbc.ITEM_DESC
  UNION ALL

  SELECT
    wbc.ITEM_DESC,
    wbc.ITEM_VALUE,
    COUNT(wah.BILL_NO) QTY,
    '' AS 'OPERATION',
    '收货单状态' AS T,
    '2' SORT
  FROM wm_base_code wbc
    LEFT JOIN wm_asn_header wah
      ON wbc.ITEM_VALUE = wah.BILL_STATE
  WHERE wbc.REMARK = 'ASN状态' AND wbc.ITEM_VALUE IN ('20', '21', '22', '23', '24')
  GROUP BY wbc.ITEM_DESC
  UNION ALL
  SELECT
    wbc.ITEM_DESC,
    wbc.ITEM_VALUE,
    COUNT(wcs.CT_CODE) QTY,
    '' AS 'OPERATION',
    '容器状态' AS T,
    '3' SORT
  FROM wm_base_code wbc
    LEFT JOIN wm_container_state wcs
      ON wbc.ITEM_VALUE = wcs.CT_STATE
    LEFT JOIN wm_container wc
      ON wcs.CT_CODE = wc.CT_CODE
  WHERE wbc.REMARK = '容器状态'
  GROUP BY wbc.ITEM_DESC) A
ORDER BY A.SORT, A.ITEM_VALUE
";
            return map.LoadTable(sql);
        }
        public static DataTable GetContainerState()
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = @"SELECT wbc.ITEM_DESC,wbc.ITEM_VALUE ,COUNT(wcs.CT_CODE) QTY,'查看' 'OPERATION','容器状态' AS T FROM wm_base_code wbc 
                      LEFT JOIN wm_container_state wcs ON wbc.ITEM_VALUE=wcs.CT_STATE
                      LEFT JOIN wm_container wc ON wcs.CT_CODE=wc.CT_CODE
                      WHERE wbc.REMARK='容器状态'
                      GROUP BY wbc.ITEM_DESC
                      ORDER BY wbc.ITEM_VALUE ASC ; ";
            return map.LoadTable(sql);
        }
        public static DataTable GetASNCount()
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = @"SELECT wbc.ITEM_DESC,wbc.ITEM_VALUE,COUNT(wah.BILL_NO) QTY,'查看' 'OPERATION','收货单状态' AS T FROM wm_base_code wbc 
                      LEFT JOIN wm_asn_header wah ON wbc.ITEM_VALUE=wah.BILL_STATE
                      WHERE wbc.REMARK='ASN状态' AND wbc.ITEM_VALUE IN ('20','21','22','23','24')
                      GROUP BY wbc.ITEM_DESC
                      ORDER BY wbc.ITEM_VALUE ASC; ";
            return map.LoadTable(sql);
        }
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
        public static string CreateLoadingTask(string billIDs, string shipNo)
        {
            IMapper map = DatabaseInstance.Instance();
            DynamicParameters parm = new DynamicParameters();
            parm.Add("V_BILL_IDS", billIDs);
            parm.Add("V_SHIP_NO", shipNo);
            parm.AddOut("V_ERR_MSG", DbType.String, 500);
            map.ExecuteScalar<string>("P_SO_CREATE_LOADING_TASK", parm, null, CommandType.StoredProcedure);
            return parm.Get<string>("V_ERR_MSG");
        }
        #endregion

        #region 2015-08-20 彭伟
        /// <summary>
        /// 获取人员任务情况
        /// </summary>
        /// <returns></returns>
        public static List<TaskEntity> GetUserByTasks()
        {
            string sql = "SELECT U.USER_CODE, U.USER_NAME, U.ITEM_DESC , COUNT(TH.USER_CODE) TASK_COUNT, " +
                "MAX(TH.CREATE_DATE) MAX_DATE, TIMESTAMPDIFF(MINUTE, MAX(TH.CREATE_DATE), now()) DIFF_TIME " +
                "FROM (SELECT DISTINCT U.USER_CODE, U.USER_NAME, UO.UPDATE_DATE, C.ITEM_DESC FROM USER_ONLINE UO " +
                "LEFT JOIN USERS U ON UO.USER_CODE = U.USER_CODE " +
                "LEFT JOIN TASKS T ON T.USER_CODE = U.USER_CODE " +
                "LEFT JOIN WM_BASE_CODE C ON C.ITEM_VALUE = T.TASK_TYPE " +
                "WHERE UO.IS_ONLINE = 'Y' AND U.IS_ACTIVE = 'Y') U " +
                "LEFT JOIN TASK_HISTORY TH ON U.USER_CODE = TH.USER_CODE " +
                "WHERE U.UPDATE_DATE <= IFNULL(TH.CREATE_DATE, NOW()) OR " +
                "IFNULL(TH.CLOSE_DATE, NOW()) >= U.UPDATE_DATE " +
                "GROUP BY U.USER_CODE " +
                "ORDER BY U.ITEM_DESC DESC";
            IMapper map = DatabaseInstance.Instance();
            return map.Query<TaskEntity>(sql);
        }
        public static List<TaskEntity> GetUserByTasks(DateTime beginTime, DateTime endTime)
        {
            string sql = "SELECT U.USER_NAME, WBC.ITEM_DESC, COUNT(A.TASK_TYPE) TASK_COUNT, " +
                "IFNULL(UO.IS_ONLINE, 'N') IS_ONLINE, TIMESTAMPDIFF(MINUTE, MAX(A.CONFIRM_DATE), NOW()) DIFF_TIME " +
                "FROM USERS U LEFT JOIN " +
                "(SELECT T.TASK_TYPE, T.BILL_ID, T.USER_CODE, T.QTY, T.CREATE_DATE, T.CREATOR, T.CONFIRM_DATE " +
                "FROM TASKS T WHERE T.CREATE_DATE >= @BeginTime AND T.CREATE_DATE <= @EndTime UNION " +
                "SELECT TH.TASK_TYPE, TH.BILL_ID, TH.USER_CODE, TH.QTY, TH.CREATE_DATE, TH.CREATOR, TH.CONFIRM_DATE " +
                "FROM TASK_HISTORY TH WHERE TH.CREATE_DATE >= @BeginTime AND TH.CREATE_DATE <= @EndTime) A " +
                "ON U.USER_CODE = A.USER_CODE " +
                "LEFT JOIN WM_BASE_CODE WBC ON WBC.ITEM_VALUE = A.TASK_TYPE " +
                "LEFT JOIN USER_ONLINE UO ON UO.USER_CODE = U.USER_CODE " +
                "WHERE U.IS_DELETED IS NULL " +
                "GROUP BY U.USER_CODE ORDER BY UO.IS_ONLINE DESC";
            IMapper map = DatabaseInstance.Instance();
            return map.Query<TaskEntity>(sql, new { BeginTime = beginTime, EndTime = endTime });
        }
        /// <summary>
        /// 获取当前任务详细信息
        /// </summary>
        /// <returns></returns>
        public static List<TaskEntity> GetCurrentTaskDetailInfo()
        {
            List<TaskEntity> list = GetCurrentTaskNew();
            if (list != null && list.Count > 0)
            {
                foreach (TaskEntity entity in list)
                {
                    switch (entity.TaskType)
                    {
                        case "140":
                            entity.BeginDate = CycleCountDal.GetBeginDate(entity.TaskID);
                            break;
                        case "143":
                            entity.BeginDate = SOQueryDal.GetBeginDate(entity.TaskID);
                            break;
                        case "141":
                        case "144":
                            entity.BeginDate = StockTransferDal.GetBeginDate(entity.TaskID);
                            break;
                        case "142":
                            entity.BeginDate = SOQueryDal.GetBeginDate(entity.TaskID);
                            break;
                        case "146":
                            entity.BeginDate = SOQueryDal.GetBeginDate(entity.TaskID);
                            break;
                        case "147":
                            entity.BeginDate = SOQueryDal.GetBeginDate(entity.TaskID);
                            break;
                    }
                }
            }
            return list;
        }
        #endregion

        /// <summary>
        /// 根据任务类型，获取指定任务列表
        /// </summary>
        /// <param name="taskType"></param>
        /// <returns></returns>
        public static List<TaskEntity> GetTasksByType(ETaskType taskType)
        {
            string sql = "SELECT T.ID, T.TASK_TYPE, C.ITEM_DESC, T.BILL_ID, H.BILL_NO, " +
                "T.USER_CODE, T.QTY, T.CREATE_DATE, T.CREATOR, T.TASK_DESC, T.REMARK, T.CONFIRM_DATE " +
                "FROM TASKS T " +
                "LEFT JOIN WM_BASE_CODE C ON C.ITEM_VALUE = T.TASK_TYPE " +
                "LEFT JOIN WM_SO_HEADER H ON H.BILL_ID = T.BILL_ID";
            if (taskType != ETaskType.无)
            {
                sql = string.Format("{0} WHERE T.TASK_TYPE = '{1}'", sql, (int)taskType);
            }
            IMapper map = DatabaseInstance.Instance();
            return map.Query<TaskEntity>(sql);
        }
        /// <summary>
        /// 删除装车任务
        /// </summary>
        /// <param name="list"></param>
        /// <param name="userCode">操作人员</param>
        public static void DeleteLoadingTask(List<TaskEntity> list, string userCode)
        {
            if (list == null || list.Count == 0)
                return;
            IMapper map = DatabaseInstance.Instance();
            IDbTransaction trans = map.BeginTransaction();
            try
            {
                string delSql = "DELETE FROM TASKS WHERE ID = {0}";
                foreach (TaskEntity item in list)
                {
                    map.Execute(string.Format(delSql, item.TaskID), null, trans);
                    // 插入日志信息
                    LogDal.Insert(ELogType.操作任务, userCode, ConvertUtil.ToString(item.BillID), item.TaskName, "任务池管理(新)", item.UserCode);
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
        }

        /// <summary>
        /// 根据用户编号获取该用户可执行的任务,优先级等
        /// </summary>
        /// <param name="userCode">用户编号</param>
        /// <returns></returns>
        public static List<TaskEntity> GetTaskByUserCode(string userCode)
        {
            string sql = @"SELECT U.USER_CODE, U.USER_NAME, R.ROLE_NAME, R.ROLE_ID, UR.Attri1 U_ATTRI, C.ITEM_DESC, UR.Attri2 ROLE_ENABLED, R.Attri1 TaskTypeNo
FROM USERS U
  LEFT JOIN USER_ONLINE UO ON UO.USER_CODE = U.USER_CODE
  LEFT JOIN USER_ROLE UR ON UR.USER_CODE = U.USER_CODE
  LEFT JOIN ROLES R ON UR.ROLE_ID = R.ROLE_ID
  LEFT JOIN WM_BASE_CODE C ON C.GROUP_CODE = '114' AND C.ITEM_VALUE = R.Attri1
  WHERE R.Attri1 IN (SELECT C.ITEM_VALUE FROM WM_BASE_CODE C WHERE C.GROUP_CODE = '114' AND C.IS_ACTIVE = 'Y') AND U.USER_CODE = @UserCode
  ORDER BY U.USER_CODE, UR.Attri1 DESC";
            IMapper map = DatabaseInstance.Instance();
            return map.Query<TaskEntity>(sql, new { UserCode = userCode });
        }

        public static List<TaskEntity> GetReport(DateTime beginDate, DateTime endDate, string userCode)
        {
            string sql = @"SELECT H.ID, WBC.ITEM_DESC, H.BILL_ID, H.USER_CODE, U.USER_NAME, H.CREATE_DATE, 
  H.CLOSE_DATE, H.CONFIRM_DATE, H.IS_CASE, 
  (CASE H.TASK_TYPE 
      WHEN '143' THEN ROUND(SUM(D.PICK_QTY), 0)
      WHEN '140' THEN ROUND(COUNT(C.LC_CODE), 0)
      WHEN '144' THEN ROUND(SUM(T.QTY / IFNULL(T.UM_QTY, 0)), 0) END) TASK_QTY
  FROM TASK_HISTORY H
  LEFT JOIN WM_SO_DETAIL D ON D.BILL_ID = H.BILL_ID
  LEFT JOIN WM_COUNT_RECORD C ON C.BILL_ID = H.BILL_ID
  LEFT JOIN WM_TRANS_RECORD T ON T.BILL_ID = H.BILL_ID
  LEFT JOIN USERS U ON U.USER_CODE = H.USER_CODE
  LEFT JOIN WM_BASE_CODE WBC ON WBC.ITEM_VALUE = H.TASK_TYPE
  WHERE H.CREATE_DATE BETWEEN @BeginDate AND @EndDate 
    AND H.USER_CODE = @UserCode
  GROUP BY H.BILL_ID";
            IMapper map = DatabaseInstance.Instance();
            return map.Query<TaskEntity>(sql, new { BeginDate = beginDate, EndDate = endDate, UserCode = userCode });
        }
        public static int StickTask(int taskID)
        {
            string sql = @"UPDATE TASKS T
  INNER JOIN (SELECT T.ID, T.STICK_DATE FROM TASKS T ORDER BY IFNULL(T.STICK_DATE, NOW()) LIMIT 1) A ON A.ID <> 0
  set T.STICK_DATE = SUBDATE(IFNULL(A.STICK_DATE, NOW()), 1)
  WHERE T.ID = @TaskID ";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new { TaskID = taskID });
        }
        public static int RemoveLoadingTask(List<LoadingDetailEntity> list)
        {
            int result = 0;
            if (list != null && list.Count > 0)
            {
                foreach (LoadingDetailEntity item in list)
                {
                    result += RemoveLoadingTask(item);
                }
            }
            return result;
        }
        public static int RemoveLoadingTask(LoadingDetailEntity entity)
        {
            string sql = @"DELETE FROM TASKS WHERE BILL_ID = @BillID ";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new { BillID = entity.BillID });
        }
        public static bool CanAdd(string userCode, int billID, string taskType)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = string.Format("SELECT COUNT(1) FROM tasks t " +
                                        "WHERE t.BILL_ID ='{1}'AND t.USER_CODE ='{0}' AND t.TASK_TYPE ='{2}'", userCode, billID, taskType);
            long ret = map.ExecuteScalar<long>(sql);
            if (ret <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void AddInstoreTaskPerson(int taskID, string userCode, string taskType)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = string.Format(
            @"INSERT INTO TASKS(TASK_TYPE, BILL_ID, USER_CODE, QTY, CREATE_DATE, CREATOR, CONFIRM_DATE, TASK_DESC )
  	        SELECT TASK_TYPE, BILL_ID, '{1}', QTY, NOW(), CREATOR, NOW(),'个单据'
  	        FROM TASKS WHERE ID = {0}", taskID, userCode);
            map.Execute(sql);
            sql = string.Format("INSERT INTO task_detail ( TASK_ID , ACTION_ID ) " +
                                 "SELECT MAX(T1.ID) ,T1.BILL_ID   FROM tasks T1  " +
                                 " WHERE T1.BILL_ID =(SELECT t.BILL_ID  FROM tasks t  " +
                                 " WHERE t.ID= '{0}') AND T1.USER_CODE ='{1}'AND T1.TASK_TYPE ='{2}' ", taskID, userCode,taskType);
            map.Execute(sql);
        }

        public static void ChangeInstoreTask(int taskID, string userCode)
        {
            IMapper map = DatabaseInstance.Instance();
            IDbTransaction trans = map.BeginTransaction();
            try
            {
                string sql = string.Format(
    @"INSERT INTO TASK_HISTORY(ID, TASK_TYPE, BILL_ID, USER_CODE, QTY, CREATE_DATE, CREATOR, CLOSE_DATE, CONFIRM_DATE)
  	SELECT ID, TASK_TYPE, BILL_ID, USER_CODE, QTY, CREATE_DATE, CREATOR, NOW(), CONFIRM_DATE
  	FROM TASKS WHERE ID = {0}", taskID);
                map.Execute(sql);
                sql = string.Format(
    @"INSERT INTO TASKS(TASK_TYPE, BILL_ID, USER_CODE, QTY, CREATE_DATE, CREATOR, CONFIRM_DATE)
  	SELECT TASK_TYPE, BILL_ID, '{1}', QTY, NOW(), CREATOR, NOW()
  	FROM TASKS WHERE ID = {0}", taskID, userCode);
                map.Execute(sql);
                sql = string.Format(
    @"DELETE FROM TASKS WHERE ID = {0} ", taskID);
                map.Execute(sql);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
        }
    }
}

