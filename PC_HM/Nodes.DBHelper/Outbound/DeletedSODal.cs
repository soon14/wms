using System.Collections.Generic;
using System.Data;
using Nodes.Dapper;
using Nodes.Entities;

namespace Nodes.DBHelper
{
    /// <summary>
    /// 回收站：发货单数据访问类
    /// </summary>
    public class DeletedSODal
    {
        private const string SELECT_BILL_BODY = 
            "SELECT A.BILL_ID, A.BILL_NO, A.WAREHOUSE, W.NAM WAREHOUSE_NAME, A.BILL_TYPE, " +
                "C1.NAM BILL_TYPE_NAME, A.BILL_STATE, C2.NAM STATUS_NAME, A.OUTBOUND_TYPE, C3.NAM OUTBOUND_TYPE_NAME, " +
                "A.SALES_MAN, A.CONTRACT_NO, A.DELIVERY_DATE, A.CUSTOMER, S.NAM CUSTOMER_NAME, " +
                "A.SHIP_NO, A.OUTBOUND_COMPLETE_DATE, A.OUTBOUND_MAN, A.ORIGINAL_BILL_NO, A.REMARK, A.CREATE_DATE, A.DELETE_TIME, A.DELETE_USER " +
                "FROM ExtDB.dbo.DEL_SO_HEADER A " +
                "INNER JOIN WAREHOUSE W ON A.WAREHOUSE = W.COD " +
                "INNER JOIN CUSTOMER S ON A.CUSTOMER = S.COD " +
                "INNER JOIN CODEITEM C1 ON A.BILL_TYPE = C1.COD " +
                "INNER JOIN CODEITEM C2 ON A.BILL_STATE = C2.COD " +
                "INNER JOIN CODEITEM C3 ON A.OUTBOUND_TYPE = C3.COD ";

        /// <summary>
        /// 按照库房列出所有回收站的单据
        /// </summary>
        /// <param name="warehouse"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public List<DeletedSOHeaderEntity> QueryBillsQuickly(string warehouse)
        {
            string whereCondition = "WHERE WAREHOUSE = @Warehouse";
            string sql = SELECT_BILL_BODY + whereCondition;
            IMapper map = DatabaseInstance.Instance();
            return map.Query<DeletedSOHeaderEntity>(sql,
                new
                {
                    Warehouse = warehouse
                });
        }

        /// <summary>
        /// 获取到货通知单明细信息
        /// </summary>
        /// <param name="billID">单据号</param>
        /// <returns>到货通知单明细信息集合</returns>
        public List<DeletedSODetailEntity> GetDetailsByBillID(int billID)
        {
            string sql = "SELECT D.BILL_ID, D.DETAIL_ID, D.ROW_NO, D.MATERIAL, M.NAM MATERIAL_NAME, " +
                "D.COM_MATERIAL, D.UNIT, D.QTY, D.DUE_DATE, D.BATCH_NO, D.PRICE, D.REMARK " +
                "FROM ExtDB.dbo.DEL_SO_DETAIL D " +
                "INNER JOIN MATERIAL M ON D.MATERIAL = M.COD " +
                "INNER JOIN ExtDB.dbo.DEL_SO_HEADER H ON D.BILL_ID = H.Bill_ID " +
                //"INNER JOIN ExtDB.dbo.DEL_SAP_SO_DETAIL SD ON H.BILL_NO = SD.BILL_NO AND D.ROW_NO = SD.ROW_NO " +
                "WHERE D.BILL_ID = @BillID";
            IMapper map = DatabaseInstance.Instance();
            return map.Query<DeletedSODetailEntity>(sql, new { BillID = billID });
        }

        public int RestoreBill(int billID, string billNO)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("BILL_ID", billID);
            parms.Add("BILL_NO", billNO);
            parms.AddOut("RET_VAL", DbType.Int32);

            IMapper map = DatabaseInstance.Instance();
            map.Execute("P_SO_RESTORE", parms, CommandType.StoredProcedure);
            return parms.Get<int>("RET_VAL");
        }
    }
}
