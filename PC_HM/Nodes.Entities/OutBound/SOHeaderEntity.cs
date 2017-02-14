using System;
using Nodes.Dapper;

namespace Nodes.Entities
{
    /// <summary>
    /// 到货通知单表头
    /// </summary>
    public class SOHeaderEntity
    {
        bool hasChecked = false;
        /// <summary>
        /// 选中，用于表格操作中的选中，不绑定到数据库
        /// </summary>
        public bool HasChecked { get { return hasChecked; } set { hasChecked = value; } }

        /// <summary>
        /// 主键编号
        /// </summary>
        [ColumnName("CASE_STR")]
        public string CaseStr { get; set; }

        /// <summary>
        /// 主键编号
        /// </summary>
        [ColumnName("BILL_ID")]
        public int BillID { get; set; }

        /// <summary>
        /// 单据编号
        /// </summary>
        [ColumnName("BILL_NO")]
        public string BillNO { get; set; }

        [ColumnName("WH_CODE")]
        public string Warehouse { get; set; }

        [ColumnName("WH_NAME")]
        public string WarehouseName { get; set; }

        /// <summary>
        /// 单据来源仓库编号
        /// </summary>
        [ColumnName("FROM_WH_CODE")]
        public string FromWarehouse { get; set; }

        [ColumnName("FROM_WH_NAME")]
        public string FromWarehouseName { get; set; }

        /// <summary>
        /// 拣货区
        /// </summary>
        [ColumnName("PICK_ZN_TYPE")]
        public string PickZnType { get; set; }

        /// <summary>
        /// 拣货区名称
        /// </summary>
        [ColumnName("PICK_ZN_TYPE_NAME")]
        public string PickZnTypeName { get; set; }

        /// <summary>
        /// 单据状态
        /// </summary>
        [ColumnName("BILL_STATE")]
        public string Status { get; set; }

        /// <summary>
        /// 单据状态名称
        /// </summary>
        [ColumnName("STATUS_NAME")]
        public string StatusName
        {
            get;
            set;
        }

        /// <summary>
        /// 单据类型代码
        /// </summary>
        [ColumnName("BILL_TYPE")]
        public string BillType { get; set; }

        /// <summary>
        /// 单据类型名称（正常采购单、退货单、越库收货单、虚拟入库单）
        /// </summary>
        [ColumnName("BILL_TYPE_NAME")]
        public string BillTypeName { get; set; }

        /// <summary>
        /// 出库方式
        /// </summary>
        [ColumnName("OUTSTORE_TYPE")]
        public string OutstoreType { get; set; }

        /// <summary>
        /// 收货策略名称（正常单据、越库收货单、虚拟入库单）
        /// </summary>
        [ColumnName("OUTSTORE_TYPE_NAME")]
        public string OutstoreTypeName { get; set; }

        /// <summary>
        /// 业务员
        /// </summary>
        [ColumnName("SALES_MAN")]
        public string SalesMan { get; set; }

        /// <summary>
        /// 业务电话号
        /// </summary>
        [ColumnName("CONTRACT_NO")]
        public string ContractNO { get; set; }

        /// <summary>
        /// 交货日期
        /// </summary>
        [ColumnName("DELIVERY_DATE")]
        public DateTime? DeliveryDate { get; set; }

        /// <summary>
        /// 客户代码
        /// </summary>
        [ColumnName("CUSTOMER")]
        public string Customer { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        [ColumnName("C_NAME")]
        public string CustomerName { get; set; }

        /// <summary>
        /// 客户编号
        /// </summary>
        [ColumnName("C_CODE")]
        public string CustomerCode { get; set; }
        /// <summary>
        /// 客户编码-客户名称
        /// </summary>
        public string CustomerCodeAndName
        {
            get { return string.Format("{0}-{1}", this.CustomerCode, this.CustomerName); }
        }
        /// <summary>
        /// 运单号
        /// </summary>
        [ColumnName("SHIP_NO")]
        public string ShipNO { get; set; }

        [ColumnName("SAP_SO_NO")]
        public string SapSoNO { get; set; }

        /// <summary>
        /// 收货完成时间
        /// </summary>
        [ColumnName("OUTBOUND_COMPLETE_DATE")]
        public DateTime? OutboundCompleteDate { get; set; }

        /// <summary>
        /// QC合同号
        /// </summary>
        [ColumnName("QC_NUMBER")]
        public string QcNumber { get; set; }

        /// <summary>
        /// 收货人员
        /// </summary>
        [ColumnName("OUTBOUND_MAN")]
        public string OutboundMan { get; set; }

        /// <summary>
        /// 承运商-SAP传递过来的字段
        /// </summary>
        [ColumnName("CARRIER")]
        public string Carrier { get; set; }

        /// <summary>
        /// 收货人地址-SAP传递过来的字段
        /// </summary>
        [ColumnName("ADDRESS")]
        public string Address { get; set; }

        /// <summary>
        /// 收货人电话-SAP传递过来的字段
        /// </summary>
        [ColumnName("PHONE")]
        public string ShTel { get; set; }

        /// <summary>
        /// 收货人-SAP传递过来的字段
        /// 指的是发给谁
        /// </summary>
        [ColumnName("CONTACT")]
        public string Consignee { get; set; }

        /// <summary>
        /// 收货单位-SAP传递过来的字段
        /// </summary>
        [ColumnName("SH_COMPANY")]
        public string ShCompany { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [ColumnName("REMARK")]
        public string Remark { get; set; }

        /// <summary>
        /// 建单日期-SAP中的凭证日期
        /// </summary>
        [ColumnName("CREATE_DATE")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// WMS自有备注
        /// </summary>
        [ColumnName("WMS_REMARK")]
        public string WmsRemark { get; set; }

        /// <summary>
        /// 单据行字体颜色，ARGB
        /// </summary>
        [ColumnName("ROW_COLOR")]
        public int? RowForeColor { get; set; }

        /// <summary>
        /// 是否已打印发货单（0：未打印；1：已打印）
        /// </summary>
        [ColumnName("PRINTED")]
        public int Printed { get; set; }

        public bool HasPrinted
        {
            get
            {
                return Printed >= 1;
            }
        }

        /// <summary>
        /// 创建人
        /// </summary>
        [ColumnName("CREATOR")]
        public string Creator { get; set; }

        //public string PDA { get; set; }

        /// <summary>
        /// 只更新可能会发生变化的属性
        /// </summary>
        /// <param name="header"></param>
        public void Update(SOHeaderEntity header)
        {
            this.BillNO = header.BillNO;
            this.Printed = header.Printed;
            this.OutstoreType = header.OutstoreType;
            this.OutstoreTypeName = header.OutstoreTypeName;
            this.Status = header.Status;
            this.StatusName = header.StatusName;
        }

        public void UpdateRemark(string reamrk, int? color)
        {
            this.WmsRemark = reamrk;
            this.RowForeColor = color;
        }
        /// <summary>
        /// 确认时间
        /// </summary>
        [ColumnName("CONFIRM_DATE")]
        public DateTime ConfirmDate { get; set; }

        /// <summary>
        /// 发货完成时间
        /// </summary>
        [ColumnName("CLOSE_DATE")]
        public DateTime? CloseDate { get; set; }

        /// <summary>
        /// 应收金额
        /// </summary>
        [ColumnName("RECEIVE_AMOUNT")]
        public decimal ReceiveAmount { get; set; }

        /// <summary>
        /// 实收现金
        /// </summary>
        [ColumnName("REAL_AMOUNT")]
        public decimal RealAmount { get; set; }

        /// <summary>
        /// 退货金额
        /// </summary>
        [ColumnName("CRN_AMOUNT")]
        public decimal CrnAmount { get; set; }

        /// <summary>
        /// 它项金额
        /// </summary>
        [ColumnName("OTHER_AMOUNT")]
        public decimal OtherAmount { get; set; }

        /// <summary>
        /// 回车确认状态，默认0，确认后1
        /// </summary>
        [ColumnName("CONFIRM_FLAG")]
        public int ConfirmFlag { get; set; }

        /// <summary>
        /// 它项金额备注
        /// </summary>
        [ColumnName("OTHER_REMARK")]
        public string OtherRemark { get; set; }

        /// <summary>
        /// 已支付金额
        /// </summary>
        [ColumnName("PAYED_AMOUNT")]
        public decimal PayedAmount { get; set; }

        /// <summary>
        /// 已支付金额字符串表示形式
        /// </summary>
        public string PayedAmountStr
        {
            get
            {
                return this.PayedAmount.ToString("0.00");
            }
        }

        /// <summary>
        /// 回款确认时间
        /// </summary>
        [ColumnName("PAYMENT_DATE")]
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// 回款确认人
        /// </summary>
        [ColumnName("PAYMENT_BY")]
        public string PaymentBy { get; set; }

        /// <summary>
        /// 回款标记
        /// </summary>
        [ColumnName("PAYMENT_FLAG")]
        public int PaymentFlag { get; set; }

        /// <summary>
        /// 延时标记
        /// </summary>
        [ColumnName("DELAYMARK")]
        public int DelayMark { get; set; }

        /// <summary>
        /// 回款标记描述
        /// </summary>
        public bool PaymentFlagDesc 
        {
            get 
            { 
                return PaymentFlag == 1; 
            }
        }
        /// <summary>
        /// 送货人员电话
        /// </summary>
        [ColumnName("MOBILE_PHONE")]
        public string DeliverymanMobile
        {
            get;
            set;
        }
        /// <summary>
        /// 支付方式
        /// </summary>
        [ColumnName("PAY_METHOD")]
        public int PayMethod { get; set; }
        /// <summary>
        /// 支付方式 字符串表示形式
        /// </summary>
        public string PayMethodStr
        {
            get
            {
                switch (this.PayMethod)
                {
                    case 10:
                        return "现金";
                    case 20:
                        return "银联";
                    case 30:
                        return "惠付通";
                    case 40:
                        return "微信";
                    case 50:
                        return "支付宝";
                    default:
                        return "其它";
                }
            }
        }
        /// <summary>
        /// 车次编号
        /// </summary>
        [ColumnName("VEHICLE_NO")]
        public string VehicleNO { get; set; }

        /// <summary>
        /// 同步状态
        /// </summary>
        [ColumnName("SYNC_STATE")]
        public int SyncState { get; set; }
        /// <summary>
        /// 整货件数
        /// </summary>
        [ColumnName("BOX_NUM")]
        public int BoxNum { get; set; }
        /// <summary>
        /// 预估散货件数
        /// </summary>
        [ColumnName("CASE_BOX_NUM")]
        public int CaseBoxNum { get; set; }
        /// <summary>
        /// 排序顺序
        /// </summary>
        [ColumnName("ORDER_SORT")]
        public int OrderSort { get; set; }
        [ColumnName("X_COOR")]
        public decimal XCoor { get; set; }
        [ColumnName("Y_COOR")]
        public decimal YCoor { get; set; }
        /// <summary>
        /// 排序附加属性（1：订单排序生成；10：生成装车任务生成）
        /// </summary>
        [ColumnName("Attri1")]
        public int Attri1 { get; set; }
        /// <summary>
        /// erp原始订单编号
        /// </summary>
        [ColumnName("ORIGINAL_BILL_NO")]
        public string OriginalBillNo { get; set; }

        [ColumnName("CANCEL_FLAG")]
        public int CancelFlag { get; set; }

        [ColumnName("CT_CODE")]
        public string TrayListStr { get; set; }

        [ColumnName("CT_STATE")]
        public string CtState { get; set; }
    }
}
