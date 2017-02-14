using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Nodes.Entities;
using Nodes.DBHelper;
using System.IO;
using Nodes.Shares;
using System.Collections.Generic;
using Nodes.Utils;
using Nodes.UI;
using System.Linq;
using System.Windows.Forms;

namespace Nodes.Outstore
{
    /// <summary>
    /// 2015-07-17 惠民确认的版本  （销售发货单）
    /// </summary>
    public partial class RepSO_New : DevExpress.XtraReports.UI.XtraReport
    {
        #region 变量

        public readonly string RepFileName = "RepSO.repx";
        public short copies = 1;
        public int BillID = -1;
        SODal soDal = new SODal();
        SOBody dataSource = null;
        string _module = string.Empty;

        private uint _skuIndex = 0;         // 物料序号
        #endregion

        #region 构造函数

        public RepSO_New()
        {
            InitializeComponent();
            //string reportFilePath = Path.Combine(GlobeSettings.AppPath, RepFileName);
            //if (File.Exists(reportFilePath)) this.LoadLayout(reportFilePath);

            this.PrintingSystem.StartPrint += new DevExpress.XtraPrinting.PrintDocumentEventHandler(PrintingSystem_StartPrint);
        }

        public RepSO_New(SOBody body, string module)
            : this()
        {
            this.dataSource = body;
            this._module = module;
            try
            {
                List<SOFooterReportEntity> footer2 = new List<SOFooterReportEntity>();
                // 应收金额 (如果应收金额为负数表示惠付通需返回余额)
                decimal amount = this.dataSource.AmountReceivable;

                this.dataSource.ReportFooter1.Add(new SOFooterReportEntity()
                {
                    Value1 = "下单金额：",
                    Value2 = this.dataSource.BillAmount.ToString("0.00"),
                    Value3 = "出货合计：",
                    Value4 = this.dataSource.TotalAmount.ToString("0.00")
                });
                if (this.dataSource.PhysicalBondList.Count > 0)
                {
                    this.dataSource.ReportFooter1.Add(new SOFooterReportEntity(
                        "已使用实物券：", Math.Abs(this.dataSource.PhysicalBondList.Sum(u => u.SellPrice)).ToString("f2")));
                }
                #region 有预付
                if (this.dataSource.PaymentUsedGroup.Count > 0)
                {
                    foreach (string item in this.dataSource.PaymentUsedGroup.Keys)
                    {
                        string[] keys = item.Split('_');
                        if (keys == null && keys.Length != 2) continue;
                        decimal num = this.dataSource.PaymentUsedGroup[item];
                        List<SODetailAttributeEntity> payList = this.dataSource.PaymentList.FindAll(
                            u => ConvertUtil.ToString(u.SkuCode) == keys[1] && u.BillID == ConvertUtil.ToInt(keys[0])
                        );
                        if (payList == null || payList.Count == 0) continue;
                        decimal payNum = payList.Sum(u => ConvertUtil.ToInt(u.Num));
                        this.dataSource.ReportFooter1.Add(new SOFooterReportEntity(
                            string.Format("已使用{0}：", payList[0].YuFuName),
                            Math.Abs(payNum * payList[0].SellPrice).ToString("0.00")));
                        SODetailAttributeEntity attr = this.dataSource.PaymentList.Find(
                            new Predicate<SODetailAttributeEntity>((at) =>
                            {
                                return at.BillID == ConvertUtil.ToInt(keys[0]) &&
                                    ConvertUtil.ToString(at.SkuCode) == keys[1];
                            }));
                        if (attr == null) continue;
                        decimal phy = attr.SellPrice * num;
                        if (num < 0)// 小于零表示需要退预付
                        {
                            footer2.Add(new SOFooterReportEntity(
                                string.Format("{0}应退回：", attr.YuFuName),
                                Math.Abs(phy).ToString("0.00")));
                            amount += ((payNum + num) * attr.SellPrice);
                        }
                        else
                        {
                            amount += payNum * attr.SellPrice;
                        }
                    }
                }
                #endregion
                if (this.dataSource.CashList.Count > 0)
                {
                    this.dataSource.ReportFooter1.Add(new SOFooterReportEntity(
                        "已使用代金券：", this.dataSource.CashAmountStr));
                }
                if (this.dataSource.Header.PayedAmount > 0)
                {
                    this.dataSource.ReportFooter1.Add(new SOFooterReportEntity(
                        string.Format("已支付金额({0})：", this.dataSource.Header.PayMethodStr),
                        this.dataSource.Header.PayedAmountStr));
                }
                if (this.dataSource.PhyReturnList.Count > 0)        // 有实物券返回
                {
                    footer2.Insert(0, new SOFooterReportEntity(
                        "实物券应退回：",
                        string.Format("{0} 张", this.dataSource.PhyReturnList.Count)));
                }
                if (amount < 0)     // 有惠付通或者代金券返回(无需再计算 惠付通 或者 代金券)
                {
                    footer2.Insert(0, new SOFooterReportEntity("应收合计：", "0.00"));
                    if (this.dataSource.CashList.Count > 0)
                    {
                        footer2.Add(new SOFooterReportEntity("代金券应退回：", this.dataSource.CashAmountStr));
                    }
                    footer2.Insert(footer2.Count - 1, (new SOFooterReportEntity(
                        string.Format("{0}余额应退回：", this.dataSource.Header.PayMethodStr),
                        Math.Abs(amount).ToString("0.00"))));
                    amount = 0;
                }
                else               // 表示剩下金额应从 惠付通 或者 代金券 里扣除
                {
                    if (this.dataSource.CashAmount <= amount)// 直接抵扣，剩下金额使用已支付金额抵扣
                    {
                        amount -= (this.dataSource.CashAmount + this.dataSource.Header.PayedAmount);
                        if (amount < 0)
                        {
                            footer2.Insert(0, new SOFooterReportEntity("应收合计：", "0.00"));
                            footer2.Add(new SOFooterReportEntity(
                                string.Format("{0}支付应退回：", this.dataSource.Header.PayMethodStr),
                                Math.Abs(amount).ToString("0.00")));
                            amount = 0;
                        }
                        else
                        {
                            footer2.Insert(0, new SOFooterReportEntity("应收合计：", Math.Abs(amount).ToString("0.00")));
                        }
                    }
                    else                                     // 计算在代金券里可使用的合理值(不能超过当前应收金额)
                    {
                        // 找出应退的[代金券]额度
                        decimal cashDiff = this.dataSource.CashAmount - amount;// 现金券差
                        List<decimal> cashList = new List<decimal>();
                        decimal cashValue = 0.00m;                             // 应退 代金券 额度
                        for (int i = 0; i < this.dataSource.CashList.Count; i++)
                        {
                            cashValue = this.dataSource.CashList[i].SellPrice;
                            for (int j = 0; j < this.dataSource.CashList.Count; j++)
                            {
                                if (this.dataSource.CashList[i] == this.dataSource.CashList[j])
                                    continue;
                                else if (cashValue >= cashDiff)
                                {
                                    if (!cashList.Contains(cashValue))
                                        cashList.Add(cashValue);
                                    cashValue = this.dataSource.CashList[j].SellPrice;
                                }
                                else
                                    cashValue += this.dataSource.CashList[j].SellPrice;
                            }
                        }
                        if (cashList.Count > 0)
                            cashValue = cashList.Min();
                        cashValue = Math.Abs(cashValue);
                        amount -= (this.dataSource.CashAmount - cashValue);
                        // 剩下的钱用 已支付 扣除
                        decimal result = this.dataSource.Header.PayedAmount - Math.Abs(amount);
                        if (result < 0)
                        {
                            footer2.Insert(0, new SOFooterReportEntity("应收合计：", Math.Abs(result).ToString("f2")));
                            footer2.Add(new SOFooterReportEntity("代金券应退回：", Math.Abs(cashValue).ToString("f2")));
                        }
                        else
                        {
                            footer2.Insert(0, new SOFooterReportEntity("应收合计：", "0.00"));
                            footer2.Add(new SOFooterReportEntity("代金券应退回：", Math.Abs(cashValue).ToString("f2")));
                            footer2.Add(new SOFooterReportEntity(
                                string.Format("{0}支付应退回：", this.dataSource.Header.PayMethodStr),
                                result.ToString("f2")));
                            amount = 0;
                        }
                    }
                }
                this.dataSource.ReportFooter2.AddRange(footer2);
                soDal.SaveReceiveAmount(body.Header.BillID, amount);
                //判断是否是新客户
                int ret = ConvertUtil.ToInt(soDal.GetCustomerIsNew(this.dataSource.Header.CustomerCode));
                if (ret <= 1)
                    xrLabel15.Visible = true;
                lblDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:dd");
                lblWarehouse.Text = GlobeSettings.LoginedUser.WarehouseName;
                decimal n = 0;
                foreach (SODetailReportEntity entity in this.dataSource.ReportDetails)
                {
                    decimal num = Math.Ceiling(ConvertUtil.ToDecimal(entity.SkuCombName.Length) / ConvertUtil.ToDecimal(12));
                    if (num > 2)
                        n += num;
                }
                //2015-7-18 彭伟
                decimal footHeight = (this.dataSource.ReportFooter2.Count + this.dataSource.ReportFooter1.Count) * 70;
                decimal bodyHeight = (this.dataSource.ReportDetails.Count + n) * 90;
                this.PageHeight = ConvertUtil.ToInt(1030 + footHeight + bodyHeight);//1021
                xrLabel2.Text = this.soDal.GetVhicleNo(this.dataSource.Header.BillID);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        #endregion

        void PrintingSystem_StartPrint(object sender, DevExpress.XtraPrinting.PrintDocumentEventArgs e)
        {
            e.PrintDocument.PrinterSettings.Collate = true;
            e.PrintDocument.PrinterSettings.Copies = this.copies;
        }

        private void XtraReport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.DataSource = dataSource;
            this.DataMember = "reportDetailCopy";
            // 初始化序号
            this._skuIndex = 0;
        }

        private void RepSO_AfterPrint(object sender, EventArgs e)
        {
            //记录打印张数和人
            int pageCount = this.Pages.Count * this.copies;
            //new BillLogDal().SavePrintLog(this.dataSource.Header.BillNO, pageCount, "打印销货单", GlobeSettings.LoginedUser.UserName);
            LogDal.Insert(ELogType.打印, GlobeSettings.LoginedUser.UserName, this.dataSource.Header.BillNO, "销售发货单", this._module + "-RepSO_New");
        }
        /// <summary>
        /// 打印[序号]
        /// </summary>
        private void tcMaterialIndex_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            if (cell == null) return;
            cell.Text = ConvertUtil.ToString(++this._skuIndex);
        }
        /// <summary>
        /// 打印 实物券 / 预付 使用情况
        /// </summary>
        private void xrTableCell16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            if (cell == null) return;
            int index = (int)(this._skuIndex - 1);
            if (index >= this.dataSource.ReportDetails.Count)
                return;
            SODetailReportEntity detail = this.dataSource.ReportDetails[index];
            // 当前商品的实物券(用券量)
            int phy_count = this.dataSource.GetAttrCountBySku(ConvertUtil.ToInt(detail.MaterialCode), 2).Count;
            if (phy_count == 0)
            {
                cell.Text = string.Empty;
            }
            else
            {
                decimal out_result = 0.00m;
                decimal.TryParse(detail.PickQtyReportStr, out out_result);
                decimal returnCount = phy_count - out_result;
                if (returnCount < 0)
                    returnCount = 0;
                cell.Text = string.Format("{0}\r\n{1:f0}",
                    phy_count, returnCount);
            }
        }
        /// <summary>
        /// 打印 预付名称
        /// </summary>
        private void xrTableCell2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            if (cell == null) return;
            int index = (int)(this._skuIndex - 1);
            if (index >= this.dataSource.ReportDetails.Count)
                return;
            SODetailReportEntity detail = this.dataSource.ReportDetails[index];
            if (this.dataSource.PaymentUsedGroup.Count > 0)     // 如果存在预付
            {
                // 根据当前商品找到对应的预付
                SODetailAttributeEntity attr = this.dataSource.PaymentList.Find(
                    new Predicate<SODetailAttributeEntity>((item) =>
                    {
                        return ConvertUtil.ToString(item.SkuCode) == detail.MaterialCode;
                    }));
                if (attr == null)
                {
                    this.xrTableCell2.Text = string.Empty;
                }
                else
                {
                    if (attr.YuFuName == null) return;
                    // 当前商品的预付量
                    int pay_count = this.dataSource.GetAttrCountBySku(ConvertUtil.ToInt(detail.MaterialCode), 4).Sum(u => ConvertUtil.ToInt(u.Num));
                    int lastIndex = attr.YuFuName.LastIndexOf("预付款");
                    if (lastIndex < 1) return;
                    this.xrTableCell2.Text = string.Format("{0}\r\n{1}", pay_count, attr.YuFuName.Substring(0, lastIndex));
                }
            }
        }
    }
}
