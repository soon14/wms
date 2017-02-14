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
    /// 报损出库单
    /// </summary>
    public partial class RepSOBreakage : DevExpress.XtraReports.UI.XtraReport
    {
        #region 变量

        public readonly string RepFileName = "RepSOTransfer.repx";
        public short copies = 1;
        public int BillID = -1;
        SODal soDal = new SODal();
        SOBody dataSource = null;
        string _module = string.Empty;

        #endregion

        #region 构造函数

        public RepSOBreakage()
        {
            InitializeComponent();
            //string reportFilePath = Path.Combine(GlobeSettings.AppPath, RepFileName);
            //if (File.Exists(reportFilePath)) this.LoadLayout(reportFilePath);

            this.PrintingSystem.StartPrint += new DevExpress.XtraPrinting.PrintDocumentEventHandler(PrintingSystem_StartPrint);
        }

        public RepSOBreakage(SOBody body, string module)
            : this()
        {
            this.dataSource = body;
            this._module = module;
            try
            {
                //判断是否是新客户
                int ret = ConvertUtil.ToInt(soDal.GetCustomerIsNew(this.dataSource.Header.CustomerCode));
                lblDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:dd");
                lblWarehouse.Text = GlobeSettings.LoginedUser.WarehouseName;
                decimal n = 0;
                foreach (SODetailReportEntity entity in this.dataSource.ReportDetails)
                {
                    decimal num = Math.Ceiling(ConvertUtil.ToDecimal(entity.SkuCombName.Length) / ConvertUtil.ToDecimal(14.5));
                    if (num >= 2)
                        n += num;
                }
                //2015-7-18 彭伟
                decimal bodyHeight = (this.dataSource.ReportDetails.Count) * 63.5m + n * 18.5m;
                this.PageHeight = ConvertUtil.ToInt(760 + bodyHeight);//1021
                xrLabel2.Text = this.soDal.GetVhicleNo(this.dataSource.Header.BillID);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
                throw ex;
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
            this.DataMember = "ReportDetails";
        }

        private void RepSO_AfterPrint(object sender, EventArgs e)
        {
            //记录打印张数和人
            int pageCount = this.Pages.Count * this.copies;
            //new BillLogDal().SavePrintLog(this.dataSource.Header.BillNO, pageCount, "打印调拨单", GlobeSettings.LoginedUser.UserName);
            LogDal.Insert(ELogType.打印, GlobeSettings.LoginedUser.UserName, dataSource.Header.BillNO, "调拨出库单", this._module + "-RepSOTransfer");
        }
    }
}
