using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.Entities
{
    public class ReturnHeaderEntity
    {
        bool hasChecked = false;
        /// <summary>
        /// 选中，用于表格操作中的选中，不绑定到数据库
        /// </summary>
        public bool HasChecked { get { return hasChecked; } set { hasChecked = value; } }

        [ColumnName("BILL_ID")]
        public int BillID { get; set; }

        [ColumnName("BILL_NO")]
        public string BillNo { get; set; }

        [ColumnName("BILL_STATE")]
        public string BillState { get; set; }

        [ColumnName("BILL_TYPE_NAME")]
        public string BillTypeName { get; set; }

        [ColumnName("STATUS_NAME")]
        public string StatusName { get; set; }

        [ColumnName("WH_CODE")]
        public string WHCode { get; set; }

        [ColumnName("FROM_WH_NAME")]
        public string WhCodeName { get; set; }

        [ColumnName("BILL_TYPE")]
        public string BillType { get; set; }

        [ColumnName("INSTORE_TYPE")]
        public string InstoreType { get; set; }

        [ColumnName("SALES_MAN")]
        public string SaleMan { get; set; }

        [ColumnName("CONTRACT_NO")]
        public string ContractNo { get; set; }

        [ColumnName("SHIP_NO")]
        public string ShipNo { get; set; }

        [ColumnName("S_CODE")]
        public string SCode { get; set; }

        [ColumnName("ARRIVE_DATE")]
        public DateTime ArriveDate { get; set; }

        [ColumnName("CHECK_BEGIN_DATE")]
        public DateTime CheckBeginDate { get; set; }

        [ColumnName("CHECK_COMPLETE_DATE")]
        public DateTime CheckEndDate { get; set; }

        [ColumnName("PUTAWAY_DATE")]
        public DateTime PutAwayDate { get; set; }

        [ColumnName("CLOSE_DATE")]
        public DateTime CloseDate { get; set; }

        [ColumnName("CREATE_DATE")]
        public DateTime CreateDate { get; set; }

        [ColumnName("ORIGINAL_BILL_NO")]
        public string OriginalBillNo { get; set; }

        [ColumnName("PRINTED")]
        public int Printed { get; set; }

        public bool HasPrinted
        {
            get
            {
                return Printed > 0;
            }
        }

        [ColumnName("PRINTED_TIME")]
        public DateTime PrintedDate { get; set; }

        [ColumnName("REMARK")]
        public string Remark { get; set; }

        [ColumnName("WMS_REMARK")]
        public string WmsRemark { get; set; }

        [ColumnName("ROW_COLOR")]
        public int? RowColor { get; set; }

        [ColumnName("CREATOR")]
        public string Creator { get; set; }

        [ColumnName("USER_NAME")]
        public string CreatorName { get; set; }

        [ColumnName("LAST_UPDATETIME")]
        public DateTime LastUpdateDate { get; set; }

        [ColumnName("IS_DELETED")]
        public int IsDelete { get; set; }

        [ColumnName("SYNC_STATE")]
        public string SyncState { get; set; }

        /// <summary>
        /// 它项金额
        /// </summary>
        [ColumnName("RETURN_AMOUNT")]
        public decimal ReturnAmount { get; set; }

        /// <summary>
        /// 本次退货金额
        /// </summary>
        [ColumnName("CRN_AMOUNT")]
        public decimal CrnAmount { get; set; }

        [ColumnName("RETURN_DRIVER")]
        public string ReturnDriver { get; set; }

        [ColumnName("HANDING_PERSON")]
        public string HandPerson { get; set; }

        [ColumnName("RETURN_DATE")]
        public DateTime ReturnDate { get; set; }

        [ColumnName("C_NAME")]
        public string CustomerName { get; set; }

        [ColumnName("RETURN_REASON")]
        public string ReturnReasonCode { get; set; }

        [ColumnName("REASON_DESC")]
        public string ReturnReasonDesc { get; set; }

        [ColumnName("RETURN_REMARK")]
        public string ReturnRemark { get; set; }

        [ColumnName("SENTORDER_NO")]
        public string SentOrderNo { get; set; }
    }
}
