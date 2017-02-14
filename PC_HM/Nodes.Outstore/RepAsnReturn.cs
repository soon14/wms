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

namespace Nodes.Outstore
{
    /// <summary>
    /// 采购退货单 报表
    /// </summary>
    public partial class RepAsnReturn : DevExpress.XtraReports.UI.XtraReport
    {
        public readonly string RepFileName = "RepReturn.repx";
        public short copies = 1;
        public int BillID = -1;
        SODal soDal = new SODal();
        SOBody dataSource = null;

        public RepAsnReturn()
        {
            InitializeComponent();
            string reportFilePath = Path.Combine(GlobeSettings.AppPath, RepFileName);
            if (File.Exists(reportFilePath))
            {
                this.LoadLayout(reportFilePath);
            }
            this.PrintingSystem.StartPrint += new DevExpress.XtraPrinting.PrintDocumentEventHandler(PrintingSystem_StartPrint);
        }

        public RepAsnReturn(SOBody body)
            : this()
        {
            this.dataSource = body;

            //获取数据
            try
            {
                dataSource.CompanyInfo = new CompanyDal().GetCompanys()[0];
                lblReturnAmount.Text = body.TotalAmount.ToString("f2");

                lblDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                lblWarehouse.Text = GlobeSettings.LoginedUser.WarehouseName;
                this.PageHeight = body.ReportDetails.Count * 63 + 1150;
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
            this.DataMember = "Details";
        }

        private void RepSO_AfterPrint(object sender, EventArgs e)
        {
            //记录打印张数和人
            int pageCount = this.Pages.Count * this.copies;
            //new BillLogDal().SavePrintLog(header.BillNO, pageCount, "打印销货单", GlobeSettings.LoginedUser.UserName); 
            LogDal.Insert(ELogType.打印, GlobeSettings.LoginedUser.UserName, dataSource.Header.BillNO, "采购退货单", "RepAsnReturn");
        }
    }
}
