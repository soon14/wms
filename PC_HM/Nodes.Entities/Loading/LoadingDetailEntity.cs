using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;
using Nodes.Utils;

namespace Nodes.Entities
{
    [Serializable]
    public class LoadingDetailEntity
    {
        bool hasChecked = false;
        /// <summary>
        /// 选中，用于表格操作中的选中，不绑定到数据库
        /// </summary>
        public bool HasChecked { get { return hasChecked; } set { hasChecked = value; } }
        [ColumnName("ID")]
        public int ID { get; set; }
        [ColumnName("VH_TRAIN_NO")]
        public string LoadingNO { get; set; }
        [ColumnName("BILL_NO")]
        public string BillNO { get; set; }
        [ColumnName("BILL_ID")]
        public int BillID { get; set; }
        [ColumnName("BILL_STATE")]
        public string BillState { get; set; }
        [ColumnName("IN_VH_SORT")]
        public int InVehicleSort { get; set; }
        [ColumnName("UPDATE_DATE")]
        public DateTime UpdateDate { get; set; }
        [ColumnName("BULK_COUNT")]
        public int BulkBoxCount { get; set; }
        [ColumnName("BULK_COUNT2")]
        public int BulkBoxCount2 { get; set; }
        [ColumnName("WHOLE_COUNT")]
        public int WholeBoxCount { get; set; }
        [ColumnName("SALES_MAN")]
        public string SalesMan { get; set; }
        [ColumnName("CONTRACT_NO")]
        public string ContractNo { get; set; }
        [ColumnName("C_NAME")]
        public string CustomerName { get; set; }
        [ColumnName("CONTACT")]
        public string CustomerContact { get; set; }
        [ColumnName("PHONE")]
        public string CustomerPhone { get; set; }
        [ColumnName("ADDRESS")]
        public string CustomerAddress { get; set; }
        [ColumnName("VEHICLE_NO")]
        public string MapLoadingNO { get; set; }
        [ColumnName("SYNC_STATE")]
        public int SyncState { get; set; }
        [ColumnName("DELAYMARK")]
        public int DelayMark { get; set; }
        [ColumnName("IN_VEHICLE_SORT")]
        public int Location { get; set; }
        public int VehicleID { get; set; }
        [ColumnName("BILL_TYPE")]
        public string BillType { get; set; }
        [ColumnName("OUTSTORE_TYPE")]
        public string OutStoreType { get; set; }
        [ColumnName("PICK_ZN_TYPE")]
        public string PickZnType { get; set; }
        [ColumnName("FROM_WH_CODE")]
        public string FromWhCode { get; set; }
        [ColumnName("WH_NAME")]
        public string FromWhName { get; set; }
        /// <summary>
        /// 建单日期-SAP中的凭证日期
        /// </summary>
        [ColumnName("CREATE_DATE")]
        public DateTime CreateDate { get; set; }

        public string DelayMarkStr
        {
            get
            {
                if (this.DelayMark == 1)
                    return "是";
                else
                    return "否";
            }
        }
        /// <summary>
        /// 托盘编号
        /// </summary>
        //[ColumnName("CT_CODE")]
        //public string TrayListStr { get; set; }
        [ColumnName("CT_STATE")]
        public string CtState { get; set; }
        public List<ContainerEntity> TrayList { get; set; }

        public string TrayListStr
        {
            get
            {
                if (this.TrayList != null && this.TrayList.Count > 0)
                {
                    return StringUtil.JoinBySign(this.TrayList, "ContainerCode", "、");
                }
                return string.Empty;
            }
        }
        [ColumnName("WMS_REMARK")]
        public string WMSRemark { get; set; }
    }
}
