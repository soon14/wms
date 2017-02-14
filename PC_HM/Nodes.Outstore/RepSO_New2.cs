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
    public partial class RepSO_New2 : DevExpress.XtraReports.UI.XtraReport
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

        public RepSO_New2()
        {
            InitializeComponent();
            //string reportFilePath = Path.Combine(GlobeSettings.AppPath, RepFileName);
            //if (File.Exists(reportFilePath)) this.LoadLayout(reportFilePath);

            this.PrintingSystem.StartPrint += new DevExpress.XtraPrinting.PrintDocumentEventHandler(PrintingSystem_StartPrint);
        }

        public RepSO_New2(SOBody body, string module)
            : this()
        {
            this.dataSource = body;
            this._module = module;
            try
            {
                List<SOFooterReportEntity> footer2 = new List<SOFooterReportEntity>();
                // 应收金额 (如果应收金额为负数表示惠付通需返回余额)
                decimal amount = this.dataSource.AmountReceivable;
                this.xrTableCell12.Text = this.dataSource.BillAmount.ToString("f2");
                this.xrTableCell18.Text = this.dataSource.TotalAmount.ToString("f2");
                if (this.dataSource.PhysicalBondList.Count > 0) // 实物券
                {
                    this.dataSource.ReportFooter1.Add(new SOFooterReportEntity(
                        "已使用实物券：", Math.Abs(this.dataSource.PhysicalBondList.Sum(u => u.SellPrice)).ToString("f2")));
                }
                if (this.dataSource.PaymentList.Count > 0)  // 预付
                {
                    foreach (SODetailAttributeEntity item in this.dataSource.PaymentList)
                    {
                        decimal value = Math.Abs(item.SellPrice);
                        this.dataSource.ReportFooter1.Add(new SOFooterReportEntity(
                            string.Format("已使用{0}预付款：", item.YuFuName), value.ToString("f2")));
                        amount -= value;
                    }
                }
                if (this.dataSource.CashList.Count > 0)     // 现金券
                {
                    this.dataSource.ReportFooter1.Add(new SOFooterReportEntity(
                        "已使用代金券：", this.dataSource.CashAmountStr));
                    amount -= this.dataSource.CashAmount;
                }
                if (this.dataSource.SuitList.Count > 0)
                {
                    foreach (SODetailAttributeEntity entity in this.dataSource.SuitList)
                    {
                        decimal value = Math.Abs(ConvertUtil.ToInt(entity.Num) * entity.SellPrice);
                        amount -= value;
                        this.dataSource.ReportFooter1.Insert(0, new SOFooterReportEntity(
                            entity.YuFuName + "（套餐）优惠：", value.ToString("f2")));
                    }
                }
                if (this.dataSource.HuiFuList.Count > 0)    // 惠付通
                {
                    foreach (SODetailAttributeEntity item in this.dataSource.HuiFuList)
                    {
                        decimal value = Math.Abs(item.SellPrice);
                        amount -= value;
                        this.dataSource.ReportFooter1.Add(new SOFooterReportEntity(
                            "已支付金额(惠付通)：", value.ToString("f2")));
                    }
                }
                else
                {
                    if (this.dataSource.Header.PayedAmount > 0)
                    {
                        amount -= this.dataSource.Header.PayedAmount;
                        this.dataSource.ReportFooter1.Add(new SOFooterReportEntity(
                            "已支付金额(惠付通)：", this.dataSource.Header.PayedAmountStr));
                    }
                }
                if (this.dataSource.CardList.Count > 0)
                {
                    foreach (SODetailAttributeEntity item in this.dataSource.CardList)
                    {
                        decimal value = Math.Abs(item.SellPrice);
                        amount -= value;
                        this.dataSource.ReportFooter1.Add(new SOFooterReportEntity(
                            "已支付金额(刷卡)：", value.ToString("f2")));
                    }
                }
                if (this.dataSource.AlipayList.Count > 0)
                {
                    foreach (SODetailAttributeEntity item in this.dataSource.AlipayList)
                    {
                        decimal value = Math.Abs(item.SellPrice);
                        amount -= value;
                        this.dataSource.ReportFooter1.Add(new SOFooterReportEntity(
                            "已支付金额(支付宝)：", value.ToString("f2")));
                    }
                }
                if (this.dataSource.BianMinList.Count > 0)
                {
                    foreach (SODetailAttributeEntity item in this.dataSource.BianMinList)
                    {
                        decimal value = Math.Abs(item.SellPrice);
                        amount -= value;
                        this.dataSource.ReportFooter1.Add(new SOFooterReportEntity(
                            "已支付金额(便民账户)：", value.ToString("f2")));
                    }
                }
                if (amount < 0)     
                {
                    footer2.Insert(0, new SOFooterReportEntity("应收合计：", "0.00"));
                    footer2.Insert(footer2.Count - 1, (new SOFooterReportEntity(
                        "惠付通余额应退回：", Math.Abs(amount).ToString("0.00"))));
                    amount = 0;
                }
                else               
                {
                    footer2.Insert(0, new SOFooterReportEntity("应收合计：", Math.Abs(amount).ToString("f2")));
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
                decimal footHeight = (this.dataSource.ReportFooter2.Count + this.dataSource.ReportFooter1.Count) * 70;
                decimal bodyHeight = (this.dataSource.ReportDetails.Count + n) * 90;
                this.PageHeight = ConvertUtil.ToInt(ConvertUtil.ToDecimal(this.TopMargin.HeightF + 
                    this.BottomMargin.HeightF + this.ReportHeader.HeightF + this.ReportHeader1.HeightF + 
                    this.ReportHeader2.HeightF + this.ReportFooter.HeightF) + footHeight + bodyHeight) + 100;
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
            //new BillLogDal().SavePrintLog(this.dataSource.Header.BillNO, pageCount, "打印销售发货单", GlobeSettings.LoginedUser.UserName);
            //LogDal.Insert(ELogType.打印, GlobeSettings.LoginedUser.UserName, this.dataSource.Header.BillNO, "销售发货单", this._module + "-RepSO_New2");
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
    }
}
