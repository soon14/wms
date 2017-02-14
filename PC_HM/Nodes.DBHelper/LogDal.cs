using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;
using Nodes.Entities;

namespace Nodes.DBHelper
{
    public class LogDal
    {
        #region Insert

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="type">日志类型</param>
        /// <param name="creator">当前操作人</param>
        /// <param name="billNo">订单编号</param>
        /// <param name="description">操作描述</param>
        /// <param name="module">模块</param>
        /// <param name="createTime">创建时间</param>
        /// <param name="remark">备注信息</param>
        /// <returns></returns>
        public static int Insert(ELogType type, string creator, string billNo, string description, 
            string module, DateTime createTime, string remark)
        {
            string sql = "INSERT INTO WM_LOG(TYPE, OPERATOR, BILL_NO, DESCRIPTION, MODULAR, CREATE_TIME, REMARK) " +
                "VALUES(@Type, @Operator, @BillNo, @Description, @Modular, NOW(), @Remark)";
            DynamicParameters parms = new DynamicParameters();
            parms.Add("Type", type.ToString());
            parms.Add("Operator", creator);
            parms.Add("BillNo", billNo);
            parms.Add("Description", description);
            parms.Add("Modular", module);
            parms.Add("Remark", remark);
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, parms);
        }
        public static int Insert(ELogType type, string creator, string billNo, string description,
            string module, string remark)
        {
            return Insert(type, creator, billNo, description, module, DateTime.Now, remark);
        }
        public static int Insert(ELogType type, string creator, string billNo, string description,
            string module)
        {
            return Insert(type, creator, billNo, description, module, DateTime.Now, null);
        }
        public static int Insert(ELogType type, string creator, string billNo, string module)
        {
            return Insert(type, creator, billNo, string.Empty, module, DateTime.Now, null);
        }
        /// <summary>
        /// 生成SO的操作日志信息
        /// </summary>
        /// <param name="billID">订单ID</param>
        /// <param name="content">内容:ESOOperationType枚举下的类型String格式</param>
        /// <param name="userName">操作人</param>
        /// <returns></returns>
        public static int InsertSOLog(int billID, string content, string userName)
        {
            string sql = "INSERT INTO WM_SO_LOG(BILL_ID, EVT, CREATE_DATE, CREATOR)" +
                        "VALUES(@billID,@content, NOW(), @userName);";
            DynamicParameters parms = new DynamicParameters();
            parms.Add("billID", billID);
            parms.Add("content", content);
            parms.Add("userName", userName);
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, parms);
        }

        #endregion
    }
}
