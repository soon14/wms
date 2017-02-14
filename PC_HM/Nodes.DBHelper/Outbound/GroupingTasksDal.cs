using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Nodes.Dapper;
using Nodes.Entities;


namespace Nodes.DBHelper
{
    public class GroupingTasksDal
    {



        /// <summary>
        /// 获取当前签到拣货人
        /// </summary>
        /// <returns></returns>
        public DataTable GetUserOnline()
        {
            IMapper map = DatabaseInstance.Instance();
            //DynamicParameters parms = new DynamicParameters();
            //parms.Add("V_STOCK_ID", stockID);

            return map.LoadTable("P_FIND_PICKER_ONLINE", null, CommandType.StoredProcedure);
        }

        public DataTable GetGronpingTask(string user_code)
        { 
            string  sql="";
            IMapper map = DatabaseInstance.Instance();

            DynamicParameters parms = new DynamicParameters();
            parms.Add("V_USER_CODE", user_code);

            return map.LoadTable("P_FIlTER_ORDER_WHILE_GROUPING_TASKS", parms, CommandType.StoredProcedure);
        
        }
        /// <summary>
        /// 判断选择人员是否有任务 整货任务、散货任务（根据有无车次）
        /// </summary>
        /// <param name="user_code"></param>
        /// <returns></returns>
        public int GetUserTasksNum(string user_code)
        {
            string sql = "SELECT COUNT(*) FROM tasks WHERE USER_CODE='" + user_code + "'and (IS_CASE= 1  OR (IS_CASE =2 and TASK_LOCK=2))";
            IMapper map = DatabaseInstance.Instance();

            int num = Convert.ToInt32(map.ExecuteScalar<object>(sql));

            if (num != 0)
            {
                return 1;
            }

            sql = "SELECT COUNT(*) FROM grouping_tasks WHERE USER_CODE='" + user_code + "'";

            num = Convert.ToInt32(map.ExecuteScalar<object>(sql));

            return num;

        }


        /// <summary>
        /// 增加一条数据
        ///// </summary>
        public void GronpingTaskAdd(DataTable dt)
        {
            IMapper map = DatabaseInstance.Instance();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strSql = "";
                strSql += "insert into grouping_tasks(";
                strSql += "PV_CODE,CT_CODE,Tasks_ID,USER_CODE,CREATE_USER";
                strSql += ")";
                strSql += " values (";
                strSql += "'" + dt.Rows[i]["PV_CODE"].ToString () + "',";
                strSql += "'" + dt.Rows[i]["CT_CODE"].ToString() + "',";
                strSql += "" + Convert.ToUInt32(dt.Rows[i]["Tasks_ID"]) + ",";
                strSql += "'" + dt.Rows[i]["USER_CODE"].ToString() + "',";
                strSql += "'" + dt.Rows[i]["CREATE_USER"].ToString() + "'";
                strSql += ")";            
                map.Execute(strSql);
            }
        }

        /// <summary>
        /// 增加一条数据
        ///// </summary>
        public void GronpingTaskAdd1(DataTable dt)
        {
            IMapper map = DatabaseInstance.Instance();
        
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DynamicParameters parms = new DynamicParameters();

                parms.Add("V_PV_CODE", dt.Rows[i]["PV_CODE"]);
                parms.Add("V_CT_CODE", dt.Rows[i]["CT_CODE"]);
                parms.Add("V_Tasks_ID", dt.Rows[i]["Tasks_ID"]);
                parms.Add("V_CREATE_USER", dt.Rows[i]["CREATE_USER"]);
                parms.Add("V_BILL_ID", dt.Rows[i]["BILL_ID"]);
                parms.Add("V_USER_CODE", dt.Rows[i]["USER_CODE"]);
                map.Execute("P_WMSX_WMS_GRONPINGTASK_ADD", parms, CommandType.StoredProcedure);

            }
        }

        public bool IFC_CODE(string V_CT_CODE)
        {
            IMapper map = DatabaseInstance.Instance();
            string strSql = "";
            strSql += "SELECT COUNT(*) ";
            strSql += "FROM WM_CONTAINER WC ";
            strSql += "INNER JOIN WM_CONTAINER_STATE WCS ON WC.CT_CODE = WCS.CT_CODE ";
            strSql += "WHERE WC.CT_CODE = '" + V_CT_CODE + "'";

            if ( Convert .ToInt32 ( map.ExecuteScalar<object>(strSql) )!= 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 判断小车是否被占用
        /// </summary>
        /// <param name="PV_CODE">小车条码</param>
        /// <returns></returns>
        public bool IFPV_CODE(string PV_CODE)
        {
            IMapper map = DatabaseInstance.Instance();
            string strSql = "";
            strSql += "SELECT COUNT(*) ";
            strSql += "FROM grouping_tasks ";
            strSql += "WHERE PV_CODE = '" + PV_CODE + "'";

            if (Convert.ToInt32(map.ExecuteScalar<object>(strSql)) != 0)
            {
                return true;
            }
            return false;
        }




    //SELECT WC.CT_CODE, WCS.BILL_HEAD_ID, WCS.UNIQUE_CODE  
    //FROM WM_CONTAINER WC 
    //INNER JOIN WM_CONTAINER_STATE WCS ON WC.CT_CODE = WCS.CT_CODE WHERE WC.CT_CODE = V_CT_CODE;

    }
}