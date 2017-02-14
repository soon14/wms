using System.Collections.Generic;
using System.Data;
using Nodes.Dapper;
using Nodes.Entities;

namespace Nodes.DBHelper
{
    /// <summary>
    /// 回收站：到货通知单数据访问类
    /// </summary>
    public class DeletedAsnDal
    {
        private const string SELECT_ASN_BODY = "SELECT A.BILL_ID, A.BILL_NO, A.ASN_TYPE, C1.NAM ASN_TYPE_NAME, A.BILL_STATE, C2.NAM STATUS_NAME, " +
            "A.INBOUND_TYPE, C3.NAM INBOUND_TYPE_NAME, A.SALES_MAN, A.CONTRACT_NO, A.ARRIVE_DATE, A.SUPPLIER, " +
            "S.NAM SUPPLIER_NAME, A.CLOSE_DATE, A.ORIGINAL_BILL_NO, A.REMARK, A.CREATE_DATE, A.PRINTED, A.WAREHOUSE, A.DELETED_TIME, A.DELETED_USER " +
            "FROM ExtDB.dbo.DEL_ASN_HEADER A " +
            "INNER JOIN SUPPLIER S ON A.SUPPLIER = S.COD " +
            "INNER JOIN CODEITEM C1 ON A.ASN_TYPE = C1.COD " +
            "INNER JOIN CODEITEM C2 ON A.BILL_STATE = C2.COD " +
            "INNER JOIN CODEITEM C3 ON A.INBOUND_TYPE = C3.COD ";

        /// <summary>
        /// 按照库房列出所有回收站的单据
        /// </summary>
        /// <param name="warehouse"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public List<DeletedAsnHeaderEntity> QueryBillsQuickly(string warehouse)
        {
            string whereCondition = "WHERE WAREHOUSE = @Warehouse";
            string sql = SELECT_ASN_BODY + whereCondition;
            IMapper map = DatabaseInstance.Instance();
            return map.Query<DeletedAsnHeaderEntity>(sql,
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
        public List<DeletedAsnDetailEntity> GetDetailsByBillID(int billID)
        {
            string sql = "SELECT WD.BILL_ID, WD.DETAIL_ID, H.BILL_NO, WD.ROW_NO, WD.MATERIAL, M.NAM AS MATERIAL_NAME, WD.COM_MATERIAL, WD.UNIT, WD.QTY, WD.CHECK_QTY, " +
                "WD.PUT_QTY, WD.DUE_DATE, WD.BATCH_NO, WD.PRICE, SD.QUAL_QTY, SD.UNQUAL_QTY, SD.FO_NUM, SD.CONTRACT_NO " +
                "FROM ExtDB.dbo.DEL_ASN_DETAIL WD " +
                "INNER JOIN MATERIAL M ON WD.MATERIAL = M.COD " +
                "INNER JOIN ExtDB.dbo.DEL_ASN_HEADER H ON WD.BILL_ID = H.Bill_ID " +
                "INNER JOIN ExtDB.dbo.DEL_SAP_ASN_DETAIL SD ON H.BILL_NO = SD.BILL_NO AND WD.ROW_NO = SD.ROW_NO WHERE WD.BILL_ID = @BillID";
            IMapper map = DatabaseInstance.Instance();
            return map.Query<DeletedAsnDetailEntity>(sql, new { BillID = billID });
        }

        public int RestoreBill(int billID, string billNO)
        {
            DynamicParameters parms = new DynamicParameters();
            parms.Add("BILL_ID", billID);
            parms.Add("BILL_NO", billNO);
            parms.AddOut("RET_VAL", DbType.Int32);

            IMapper map = DatabaseInstance.Instance();
            map.Execute("P_ASN_RESTORE", parms, CommandType.StoredProcedure);
            return parms.Get<int>("RET_VAL");
        }
    }
}
