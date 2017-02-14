using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Nodes.Dapper;

namespace Nodes.DBHelper
{
    public class BillLogDal
    {
        /// <summary>
        /// 写单据状态变化日志
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="eventCode"></param>
        /// <param name="status"></param>
        /// <param name="creator"></param>
        /// <returns></returns>
        public static int WriteStatusUpdate(int billID, string eventCode, string status, string creator)
        {
            string sql = "INSERT INTO TRANS_LOG(BILLID, EVENT, CREATEDATE, CREATOR, BILLSTATE) " +
                "VALUES(@BILLID, @EVENT, GETDATE(), @CREATOR, @BILLSTATE)";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new { BILLID = billID, EVENT = eventCode, CREATOR = creator, BILLSTATE = status });
        }

        public static int Write(int billID, string eventCode, string remark, string creator)
        {
            string sql = "INSERT INTO TRANS_LOG(BILLID, EVENT, CREATEDATE, CREATOR, REMARK) " +
                "VALUES(@BILLID, @EVENT, GETDATE(), @CREATOR, @REMARK)";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new { BILLID = billID, EVENT = eventCode, CREATOR = creator, REMARK = remark });
        }

        /// <summary>
        /// 保存打印日志
        /// </summary>
        /// <param name="qty"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public int SavePrintLog(string startSeq, int qty, string typ,  string user)
        {
            return 1;
            //string sql = "insert into PRINT_LOGS(START_SEQ, QTY, PRINT_USER, PRINT_DATE, TYP) values(@startSeq, @printQty, @printUser, getdate(), @typ)";

            //IMapper map = DatabaseInstance.Instance();
            //return map.Execute(sql, new { startSeq = startSeq, printQty = qty, printUser = user, typ = typ });
        }

        public static DataTable Query(string billID, string stateCode)
        {
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable(
                string.Format(
                "SELECT L.LOG_ID, L.BILL_ID, L.EV_CODE, E.EV_DESC, L.CREATE_DATE, L.CREATOR, " +
                "L.BILL_STATE, C.ITEM_DESC BILL_STATE_DESC, L.REMARK FROM BILL_LOG L " +
                "INNER JOIN BILL_EVENT E ON L.EV_CODE = E.EV_CODE " +
                "INNER JOIN WM_BASE_CODE C ON C.GROUP_CODE = '{0}' AND L.BILL_STATE = C.ITEM_VALUE " +
                "WHERE L.BILL_ID = @BillID ORDER BY L.CREATE_DATE ASC", stateCode), 
                new { BillID = billID });
        }
    }
}
