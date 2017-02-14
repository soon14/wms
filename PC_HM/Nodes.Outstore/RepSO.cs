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
    /// 2015-07-14 惠民确认的版本
    /// </summary>
    public partial class RepSO : DevExpress.XtraReports.UI.XtraReport
    {
        #region 变量

        public readonly string RepFileName = "RepSO.repx";
        public short copies = 1;
        public int BillID = -1;
        SODal soDal = new SODal();
        SOBody dataSource = null;

        private uint _skuIndex = 0;         // 物料序号
        #endregion

        #region 构造函数

        public RepSO()
        {
            InitializeComponent();
            //string reportFilePath = Path.Combine(GlobeSettings.AppPath, RepFileName);
            //if (File.Exists(reportFilePath)) this.LoadLayout(reportFilePath);

            this.PrintingSystem.StartPrint += new DevExpress.XtraPrinting.PrintDocumentEventHandler(PrintingSystem_StartPrint);
        }

        public RepSO(int billID, short copies)
            : this()
        {
            //BillID = billID;
            //this.copies = copies;

            ////获取数据
            //try
            //{
            //    dataSource = new SOBody();
            //    header = soDal.GetHeaderInfoByBillID(BillID);

            //    List<SODetailReportEntity> reportDetails = soDal.GetDetailsForPrint(BillID);
            //    List<SODetailAttributeEntity> detailAttri = soDal.GeDetailAttri(BillID);        // 该订单所有优惠券
            //    List<SODetailAttributeEntity> tempList = new List<SODetailAttributeEntity>();   // 满足条件的优惠券 2015-07-08 彭伟[添加]

            //    dataSource.CompanyInfo = new CompanyDal().GetCompanys()[0];
            //    dataSource.Header = header;
            //    dataSource.ReportDetails = reportDetails;
            //    dataSource.ReportDetailAttri = tempList;
            //    List<SODetailReportEntity> reportDetailCopy = new List<SODetailReportEntity>();
            //    if (reportDetailCopy.Count > 0)
            //        reportDetailCopy.Add(reportDetails[0]);
            //    dataSource.ReportDetailCopy = reportDetailCopy;
            //    int numRow = 0;// 记录应该加几行空白
            //    decimal nomalTotal = 0;
            //    decimal nomalAmount = reportDetails.Sum(u => u.PickQtyReport);
            //    decimal attriAmount = 0.00M;
            //    if (detailAttri.Count == 0)
            //    {
            //        GroupHeader1.Visible = false;
            //        Detail2.Visible = false;
            //        numRow = 10;
            //    }
            //    else
            //    {
            //        #region 2015-07-08 彭伟[添加/修改]
            //        // 筛选符合条件的优惠券
            //        foreach (SODetailAttributeEntity item in detailAttri)
            //        {
            //            // 找出有优惠券的商品
            //            SODetailReportEntity detail = reportDetails.Find(new Predicate<SODetailReportEntity>((d) =>
            //            {
            //                return d.MaterialCode == ConvertUtil.ToString(item.SkuCode);
            //            }));
            //            if (detail != null && detail.PickQty > 0)
            //            {
            //                // 计算使用的优惠券是否已经达到拣货量
            //                decimal count = tempList.Count(new Func<SODetailAttributeEntity, bool>((d) =>
            //                {
            //                    return ConvertUtil.ToString(d.SkuCode) == detail.MaterialCode;
            //                }));
            //                if (count < detail.PickQtyReport)
            //                    tempList.Add(item);
            //            }
            //        }
            //        if (tempList.Count > 0)
            //        {
            //            attriAmount = tempList.Sum(u => u.SellPrice);
            //            //attriAmount = detailAttri.Sum(u => u.SellPrice);
            //            numRow = 13;
            //        }
            //        else
            //        {
            //            GroupHeader1.Visible = false;
            //            Detail2.Visible = false;
            //            numRow = 10;
            //        }
            //        #endregion
            //    }
            //    nomalTotal = nomalAmount + attriAmount - header.PayedAmount;//实物金额-实物劵（负数）- 已支付金额；
            //    lblNormal.Text = nomalAmount.ToString();
            //    lblAttriTotal.Text = attriAmount.ToString();
            //    lblPayedAmount.Text = header.PayedAmount.ToString();
            //    xrLabel14.Text = nomalTotal.ToString();
            //    soDal.SaveReceiveAmount(BillID, nomalTotal);
            //    //判断是否是新客户
            //    int ret = ConvertUtil.ToInt(soDal.GetCustomerIsNew(header.CustomerCode));
            //    if (ret <= 1)
            //        xrLabel15.Visible = true;
            //    //dataSource.Customer = new CustomerDal().GetByCode(header.Customer);
            //    lblDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:dd");
            //    lblWarehouse.Text = GlobeSettings.LoginedUser.WarehouseName;
            //    decimal n = 0;
            //    foreach (SODetailReportEntity entity in reportDetails)
            //    {
            //        decimal num = Math.Ceiling(ConvertUtil.ToDecimal(entity.SkuCombName.Length) / ConvertUtil.ToDecimal(12));
            //        if (num > 1)
            //        {
            //            n += num - 1;
            //        }
            //    }
            //    //2015-7-6 10:35:55  by  Wjw    63→66
            //    this.PageHeight = (reportDetails.Count + ConvertUtil.ToInt(n + numRow) + tempList.Count) * 66 + 800;
            //    xrLabel2.Text = this.soDal.GetVhicleNo(header.BillID);
            //}
            //catch (Exception ex)
            //{
            //    MsgBox.Err(ex.Message);
            //}
        }

        public RepSO(SOBody body)
            : this()
        {
            this.dataSource = body;
            try
            {
                List<SOFooterReportEntity> footer2 = new List<SOFooterReportEntity>();
                //int numRow = 0;// 记录应该加几行空白
                this.dataSource.ReportFooter1.Add(new SOFooterReportEntity()
                {
                    Value1 = "下单金额：",
                    Value2 = this.dataSource.BillAmount.ToString("0.00"),
                    Value3 = "出货合计：",
                    Value4 = this.dataSource.TotalAmount.ToString("0.00")
                });
                if (this.dataSource.PhysicalBondList.Count > 0)         //有实物券
                {
                    this.dataSource.ReportFooter1.Add(new SOFooterReportEntity(
                        "已使用实物券：", Math.Abs(this.dataSource.PhysicalBondList.Sum(u => u.SellPrice)).ToString("0.00")));
                }
                // 应收金额 (如果应收金额为负数表示惠付通需返回余额)
                decimal amount = this.dataSource.AmountReceivable;
                #region 有预付
                if (this.dataSource.PaymentUsedGroup.Count > 0)
                {
                    foreach (string item in this.dataSource.PaymentUsedGroup.Keys)
                    {
                        string[] keys = item.Split('_');
                        if (keys == null && keys.Length != 2) continue;
                        decimal num = this.dataSource.PaymentUsedGroup[item];
                        List<SODetailAttributeEntity> payList = this.dataSource.PaymentList.FindAll(
                            u=>ConvertUtil.ToString(u.SkuCode) == keys[1] && u.BillID == ConvertUtil.ToInt(keys[0])
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
                                string.Format("{0}已退回：", attr.YuFuName),
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
                if (this.dataSource.Header.PayedAmount > 0)
                {
                    this.dataSource.ReportFooter1.Add(new SOFooterReportEntity(
                        "已支付金额(惠付通)：", this.dataSource.Header.PayedAmountStr));
                }
                if (amount < 0)     // 有惠付通余额返回
                {
                    footer2.Insert(0, new SOFooterReportEntity("应收合计：", "0.00"));
                    if (this.dataSource.PhyReturnList.Count > 0)        // 有实物券返回
                    {
                        footer2.Insert(1, new SOFooterReportEntity(
                            "实物券已退回：",
                            string.Format("{0} 张", this.dataSource.PhyReturnList.Count)));
                    }
                    footer2.Add(new SOFooterReportEntity("惠付通余额已退回：", Math.Abs(amount).ToString("0.00")));
                    amount = 0;
                }
                else
                {
                    footer2.Insert(0, new SOFooterReportEntity("应收合计：", amount.ToString("0.00")));
                    if (this.dataSource.PhyReturnList.Count > 0)        // 有实物券返回
                    {
                        footer2.Insert(1, new SOFooterReportEntity(
                            "实物券已退回：",
                            string.Format("{0} 张", this.dataSource.PhyReturnList.Count)));
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
                    //Size size = TextRenderer.MeasureText(entity.SkuCombName, this.xrTableCell13.Font);
                    //n += (int)(size.Width * 3.5 / this.xrTableCell13.WidthF);
                    decimal num = Math.Ceiling(ConvertUtil.ToDecimal(entity.SkuCombName.Length) / ConvertUtil.ToDecimal(12));
                    if (num > 2)
                    {
                        n += num;
                    }
                }
                //2015-7-18 彭伟
                decimal footHeight = (this.dataSource.ReportFooter2.Count + this.dataSource.ReportFooter1.Count) * 70;
                decimal bodyHeight = (this.dataSource.ReportDetails.Count + n) * 90;
                this.PageHeight = ConvertUtil.ToInt(1030 + footHeight + bodyHeight);//1021
                //this.PageHeight = (this.dataSource.ReportDetails.Count + ConvertUtil.ToInt(n + numRow) + this.dataSource.ReportDetailAttri.Count) * 63 + 800;
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
            new BillLogDal().SavePrintLog(this.dataSource.Header.BillNO, pageCount, "打印销货单", GlobeSettings.LoginedUser.UserName);
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
            int phy_count = this.dataSource.GetAttrCountBySku(ConvertUtil.ToInt(detail.MaterialCode), 2).Count;// 实物券
            int pay_count = this.dataSource.GetAttrCountBySku(ConvertUtil.ToInt(detail.MaterialCode), 4).Sum(u => ConvertUtil.ToInt(u.Num));// 预付
            if (phy_count == 0 && pay_count == 0)
                cell.Text = string.Empty;
            else
                cell.Text = string.Format("{0}\r\n{1}", phy_count, pay_count);
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
                    int lastIndex = attr.YuFuName.LastIndexOf("预付款");
                    if (lastIndex < 1) return;
                    this.xrTableCell2.Text = attr.YuFuName.Substring(0, lastIndex);
                }
            }
        }
    }
}
