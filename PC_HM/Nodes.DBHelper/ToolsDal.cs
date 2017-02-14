using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Nodes.Dapper;

namespace Nodes.DBHelper
{
    public class ToolsDal
    {
        /// <summary>
        /// 退货单位及其数量维护
        /// </summary>
        /// <param name="billID">退货单ID</param>
        /// <returns></returns>
        public static string Tools_Return_Modify(int billID)
        {
            DynamicParameters pram = new DynamicParameters();
            pram.Add("V_BILL_ID", billID);
            pram.AddOut("V_MSG", DbType.String, 100);
            IMapper map = DatabaseInstance.Instance();
            map.Execute("P_TOOL_MODIFY_RETURN_BILL",pram,CommandType.StoredProcedure);
            return pram.Get<string>("V_MSG");
        }
    }
}
