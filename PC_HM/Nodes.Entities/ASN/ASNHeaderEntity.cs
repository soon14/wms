using System;
using Nodes.Dapper;
using Nodes.Utils;

namespace Nodes.Entities
{
    /// <summary>
    /// 收货单表头
    /// </summary>
    public class AsnHeaderEntity : OrgEntity
    {
        /// <summary>
        /// 入库单编号
        /// </summary>
        [ColumnName("BILL_ID")]
        public int BillID { get; set; }

        [ColumnName("BILL_NO")]
        public string BillNO { get; set; }

        /// <summary>
        /// 原始单号，在从其他系统导入时比较有用，指的是存在于别的系统时的单据编号
        /// </summary>
        [ColumnName("ORIGINAL_BILL_NO")]
        public string OriginalBillNO { get; set; }

        /// <summary>
        /// 退货时，需要记录发货单号
        /// </summary>
        [ColumnName("SO_BILL_NO")]
        public string SOBillNO { get; set; }

        /// <summary>
        /// 业务员
        /// </summary>
        [ColumnName("SALES_MAN")]
        public string Sales { get; set; }

        /// <summary>
        /// 供应商编码
        /// </summary>
        [ColumnName("C_CODE")]
        public string SupplierCode { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        [ColumnName("C_NAME")]
        public string SupplierName { get; set; }

        public string SupplierNamePY {
            get
            {
                return PinYin.GetCapital(SupplierName);
            }
        }
        public string DisplayName
        {
            get
            {
                return string.Format("{0} | {1}", BillNO, SupplierName);
            }
        }

        /// <summary>
        /// 单据类型
        /// </summary>
        [ColumnName("BILL_TYPE")]
        public string BillType { get; set; }

        /// <summary>
        /// 单据类型
        /// </summary>
        [ColumnName("BILL_TYPE_DESC")]
        public string BillTypeDesc { get; set; }

        /// <summary>
        /// 入库类型
        /// </summary>
        [ColumnName("INSTORE_TYPE")]
        public string InstoreType { get; set; }

        /// <summary>
        /// 入库类型
        /// </summary>
        [ColumnName("INSTORE_TYPE_DESC")]
        public string InstoreTypeDesc { get; set; }

        /// <summary>
        /// 合同号
        /// </summary>
        [ColumnName("CONTRACT_NO")]
        public string ContractNO { get; set; }

        /// <summary>
        /// 计划收货仓库
        /// </summary>
        [ColumnName("WH_CODE")]
        public string WarehouseCode
        {
            get;
            set;
        }

        [ColumnName("CREATE_DATE")]
        public DateTime CreateDate { get; set; }

        [ColumnName("CREATOR")]
        public string Creator { get; set; }

        [ColumnName("BILL_STATE")]
        public string BillState { get; set; }

        [ColumnName("BILL_STATE_DESC")]
        public string BillStateDesc { get; set; }

        /// <summary>
        /// 到货日期
        /// </summary>
        [ColumnName("ARRIVE_DATE")]
        public DateTime? ArriveDate { get; set; }

        /// <summary>
        /// 开始验收/清点时间
        /// </summary>
        [ColumnName("CHECK_DATE")]
        public DateTime? CheckDate { get; set; }

        /// <summary>
        /// 清点完成时间
        /// </summary>
        [ColumnName("CHECK_COMPLETE_DATE")]
        public DateTime? CheckCompleteDate { get; set; }

        /// <summary>
        /// 开始上架时间
        /// </summary>
        [ColumnName("PUTAWAY_DATE")]
        public DateTime? PutawayDate { get; set; }

        /// <summary>
        /// 收货完成时间
        /// </summary>
        [ColumnName("CLOSE_DATE")]
        public DateTime? CloseDate { get; set; }

        /// <summary>
        /// 打印次数
        /// </summary>
        [ColumnName("PRINTED")]
        public int Printed { get; set; }

        [ColumnName("PRINTED_TIME")]
        public DateTime? PrintedTime { get; set; }

        public bool HasPrinted
        {
            get
            {
                return Printed > 0;
            }
        }

        /// <summary>
        /// WMS自己的备注
        /// </summary>
        [ColumnName("WMS_REMARK")]
        public string WmsRemark { get; set; }

        /// <summary>
        /// 单据行背景色，ARGB
        /// </summary>
        [ColumnName("ROW_COLOR")]
        public int? RowForeColor { get; set; }

        public void Copy(AsnHeaderEntity header)
        {
            this.WarehouseCode = header.WarehouseCode;
            this.WarehouseName = header.WarehouseName;
            this.BillID = header.BillID;
            this.BillType = header.BillType;
            this.BillTypeDesc = header.BillTypeDesc;
            this.InstoreType = header.InstoreType;
            this.InstoreTypeDesc = header.InstoreTypeDesc;
            this.BillState = header.BillState;
            this.BillStateDesc = header.BillStateDesc;

            this.OriginalBillNO = header.OriginalBillNO;
            this.Sales = header.Sales;
            this.SupplierCode = header.SupplierCode;
            this.SupplierName = header.SupplierName;
            this.ContractNO = header.ContractNO;

            this.ArriveDate = header.ArriveDate;
            this.CheckDate = header.CheckDate;
            this.PutawayDate = header.PutawayDate;
            this.CloseDate = header.CloseDate;
            this.CreateDate = header.CreateDate;

            this.Remark = header.Remark;
            this.RowForeColor = header.RowForeColor;
            this.Creator = header.Creator;
            this.Printed = header.Printed;
            this.PrintedTime = header.PrintedTime;

            this.LastUpdateBy = header.LastUpdateBy;
            this.LastUpdateDate = header.LastUpdateDate;
        }
    }
}
