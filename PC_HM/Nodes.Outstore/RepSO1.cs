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
using Nodes.Entities.Outbound;
using System.Linq;

namespace Nodes.Outstore
{
    /// <summary>
    /// 最初第一个版本
    /// </summary>
    public partial class RepSO1 : DevExpress.XtraReports.UI.XtraReport
    {
        public readonly string RepFileName = "RepSO.repx";
        public short copies = 1;
        public int BillID = -1;
        SODal soDal = new SODal();
        SOBody dataSource = null;
        SOHeaderEntity header = null;

        public RepSO1()
        {
            InitializeComponent();
            //string reportFilePath = Path.Combine(GlobeSettings.AppPath, RepFileName);
            //if (File.Exists(reportFilePath)) this.LoadLayout(reportFilePath);

            this.PrintingSystem.StartPrint += new DevExpress.XtraPrinting.PrintDocumentEventHandler(PrintingSystem_StartPrint);
        }

        public RepSO1(int billID, short copies)
            : this()
        {
            BillID = billID;
            this.copies = copies;

            //获取数据
            try
            {
                header = soDal.GetHeaderInfoByBillID(BillID);

                List<SODetailReportEntity> reportDetails = soDal.GetDetailsForPrint(BillID);
                List<SODetailAttributeEntity> detailAttri = soDal.GeDetailAttri(BillID);        // 该订单所有优惠券
                List<SODetailAttributeEntity> tempList = new List<SODetailAttributeEntity>();   // 满足条件的优惠券 2015-07-08 彭伟[添加]
                dataSource = new SOBody();
                dataSource.CompanyInfo = new CompanyDal().GetCompanys()[0];
                dataSource.Header = header;
                dataSource.ReportDetails = reportDetails;
                dataSource.ReportDetailAttri = tempList;
                List<SODetailReportEntity> reportDetailCopy = new List<SODetailReportEntity>();
                if (reportDetailCopy.Count > 0)
                    reportDetailCopy.Add(reportDetails[0]);
                dataSource.ReportDetailCopy = reportDetailCopy;
                int numRow = 0;// 记录应该加几行空白
                decimal nomalTotal = 0;
                decimal nomalAmount = reportDetails.Sum(u => u.PickQtyReport);
                decimal attriAmount = 0.00M;
                if (detailAttri.Count == 0)
                {
                    GroupHeader1.Visible = false;
                    Detail2.Visible = false;
                    numRow = 10;
                }
                else
                {
                    #region 2015-07-08 彭伟[添加/修改]
                    // 筛选符合条件的优惠券
                    foreach (SODetailAttributeEntity item in detailAttri)
                    {
                        // 找出有优惠券的商品
                        SODetailReportEntity detail = reportDetails.Find(new Predicate<SODetailReportEntity>((d) =>
                        {
                            return d.MaterialCode == ConvertUtil.ToString(item.SkuCode);
                        }));
                        if (detail != null && detail.PickQty > 0)
                        {
                            // 计算使用的优惠券是否已经达到拣货量
                            decimal count = tempList.Count(new Func<SODetailAttributeEntity, bool>((d) =>
                            {
                                return ConvertUtil.ToString(d.SkuCode) == detail.MaterialCode;
                            }));
                            if (count < detail.PickQtyReport)
                                tempList.Add(item);
                        }
                    }
                    if (tempList.Count > 0)
                    {
                        attriAmount = tempList.Sum(u => u.SellPrice);
                        //attriAmount = detailAttri.Sum(u => u.SellPrice);
                        numRow = 13;
                    }
                    else
                    {
                        GroupHeader1.Visible = false;
                        Detail2.Visible = false;
                        numRow = 10;
                    }
                    #endregion
                }
                nomalTotal = nomalAmount + attriAmount - header.PayedAmount;//实物金额-实物劵（负数）- 已支付金额；
                lblNormal.Text = nomalAmount.ToString();
                lblAttriTotal.Text = attriAmount.ToString();
                lblPayedAmount.Text = header.PayedAmount.ToString();
                if (nomalTotal < 0)
                {
                    this.xrLabel14.Text = "0.00";
                    this.xrLabel26.Visible = this.xrLabel27.Visible = true;
                    this.xrLabel27.Text = Math.Abs(nomalTotal).ToString();
                }
                else
                {
                    this.xrLabel26.Visible = this.xrLabel27.Visible = false;
                    xrLabel14.Text = nomalTotal.ToString();
                }
                soDal.SaveReceiveAmount(BillID, nomalTotal);
                //判断是否是新客户
                int ret = ConvertUtil.ToInt(soDal.GetCustomerIsNew(header.CustomerCode));
                if (ret <= 1)
                    xrLabel15.Visible = true;
                //dataSource.Customer = new CustomerDal().GetByCode(header.Customer);
                lblDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:dd");
                lblWarehouse.Text = GlobeSettings.LoginedUser.WarehouseName;
                decimal n = 0;
                foreach (SODetailReportEntity entity in reportDetails)
                {
                    decimal num = Math.Ceiling(ConvertUtil.ToDecimal(entity.SkuCombName.Length) / ConvertUtil.ToDecimal(12));
                    if (num > 1)
                    {
                        n += num - 1;
                    }
                }
                this.PageHeight = (reportDetails.Count + ConvertUtil.ToInt(n + numRow) + tempList.Count) * 63 + 800;
                xrLabel2.Text = this.soDal.GetVhicleNo(header.BillID);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        void PrintingSystem_StartPrint(object sender, DevExpress.XtraPrinting.PrintDocumentEventArgs e)
        {
            e.PrintDocument.PrinterSettings.Collate = true;
            e.PrintDocument.PrinterSettings.Copies = this.copies;
        }

        private void XtraReport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.DataSource = dataSource;
            this.DataMember = "reportDetailCopy";
        }

        private void RepSO_AfterPrint(object sender, EventArgs e)
        {
            //记录打印张数和人
            int pageCount = this.Pages.Count * this.copies;
            new BillLogDal().SavePrintLog(header.BillNO, pageCount, "打印销货单", GlobeSettings.LoginedUser.UserName);
        }
    }
}
