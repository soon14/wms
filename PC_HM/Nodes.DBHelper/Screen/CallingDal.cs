using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;
using System.Data;

namespace Nodes.DBHelper
{
    public class CallingDal
    {
        /// <summary>
        /// 生成叫号信息
        /// </summary>
        /// <param name="callType"></param>
        /// <param name="billNO"></param>
        /// <param name="description"></param>
        /// <param name="taskID">没有写-1</param>
        /// <returns></returns>
        public string CreateCalling(string callType, string billNO, string description,string userCode,int taskID)
        {
            IMapper map = DatabaseInstance.Instance();
            DynamicParameters parms = new DynamicParameters();
            parms.Add("V_CALL_TYPE", callType);
            parms.Add("V_CALL_BILL_NO", billNO);
            parms.Add("V_DESCRIPTION", description);
            parms.Add("V_USER_CODE", userCode);
            parms.Add("V_TASK_ID", taskID);
            parms.AddOut("V_MSG", DbType.String);
            map.Execute("P_SCREEN_CALLING_INSERT", parms, CommandType.StoredProcedure);
            return parms.Get<string>("V_MSG");
        }
        /// <summary>
        /// 获取叫号内同
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCallingData()
        {
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable("P_SCREEN_CALL_GET", null, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 更新叫号状态
        /// </summary>
        /// <param name="callID"></param>
        /// <returns></returns>
        public static int UpdateCallState(int callID)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = String.Format("UPDATE wm_system_call set CALL_STATE='2',CALL_NUM=CALL_NUM+1,LAST_UPDATE_TIME=NOW() WHERE ID={0}", callID);
            return map.Execute(sql);
        }
        /// <summary>
        /// 重复叫号
        /// </summary>
        /// <param name="callID"></param>
        /// <returns></returns>
        public static int ReCall(int callID)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = String.Format("UPDATE wm_system_call set CALL_STATE='1',LAST_UPDATE_TIME=NOW() WHERE ID={0}", callID);
            return map.Execute(sql);
        }
    }
}
