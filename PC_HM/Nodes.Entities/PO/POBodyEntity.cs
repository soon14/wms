using System;
using System.Collections.Generic;
using Nodes.Dapper;
using Nodes.Utils;

namespace Nodes.Entities
{
    public class POBodyEntity : OrgEntity
    {
        public List<PODetailEntity> Details
        {
            get;
            set;
        }

        /// <summary>
        /// 采购单编号
        /// </summary>
        [ColumnName("BILL_ID")]
        public string BillID { get; set; }

        /// <summary>
        /// 原始单号，在从其他系统导入时比较有用，指的是存在于别的系统时的单据编号
        /// </summary>
        [ColumnName("SOURCE_BILL_ID")]
        public string SourceBillID { get; set; }

        /// <summary>
        /// 业务员
        /// </summary>
        [ColumnName("SALES")]
        public string Sales { get; set; }

        /// <summary>
        /// 供应商编码
        /// </summary>
        [ColumnName("SUPPLIER")]
        public string Supplier { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        [ColumnName("SUP_NAME")]
        public string SupplierName { get; set; }

        /// <summary>
        /// 供应商名称简称
        /// </summary>
        [ColumnName("NAME_S")]
        public string SupplierNameS { get; set; }

        public string SupplierPY
        {
            get
            {
                return PinYin.GetQuanPin(SupplierName);
            }
        }

        /// <summary>
        /// 业务类型
        /// </summary>
        [ColumnName("BILL_TYPE")]
        public string BillType { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        [ColumnName("BILL_TYPE_DESC")]
        public string BillTypeDesc { get; set; }

        /// <summary>
        /// 单据行字体颜色，ARGB
        /// </summary>
        [ColumnName("ROW_COLOR")]
        public int? RowForeColor { get; set; }

        /// <summary>
        /// 合同号
        /// </summary>
        [ColumnName("CONTRACT_NO")]
        public string ContractNO { get; set; }

        [ColumnName("CREATE_DATE")]
        public DateTime CreateDate { get; set; }

        [ColumnName("CREATOR")]
        public string Creator { get; set; }

        [ColumnName("BILL_STATE")]
        public string BillState { get; set; }

        [ColumnName("BILL_STATE_DESC")]
        public string BillStateDesc { get; set; }

        /// <summary>
        /// 备用字符1
        /// </summary>
        [ColumnName("PO_STR1")]
        public string PO_STR1
        {
            set;
            get;
        }

        /// <summary>
        /// 备用字符2
        /// </summary>
        [ColumnName("PO_STR2")]
        public string PO_STR2
        {
            set;
            get;
        }

        /// <summary>
        /// 备用字符3
        /// </summary>
        [ColumnName("PO_STR3")]
        public string PO_STR3
        {
            set;
            get;
        }

        /// <summary>
        /// 备用字符4
        /// </summary>
        [ColumnName("PO_STR4")]
        public string PO_STR4
        {
            set;
            get;
        }

        /// <summary>
        /// 备用数值1
        /// </summary>
        [ColumnName("PO_NUM1")]
        public decimal? PO_NUM1
        {
            set;
            get;
        }

        /// <summary>
        /// 备用数值2
        /// </summary>
        [ColumnName("PO_NUM2")]
        public decimal? PO_NUM2
        {
            set;
            get;
        }

        /// <summary>
        /// 备用日期1
        /// </summary>
        [ColumnName("PO_DATE1")]
        public DateTime? PO_DATE1
        {
            set;
            get;
        }

        /// <summary>
        /// 备用日期2
        /// </summary>
        [ColumnName("PO_DATE2")]
        public DateTime? PO_DATE2
        {
            set;
            get;
        }
    }
}
