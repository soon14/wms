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

namespace Nodes.Outstore
{
    public partial class RepReturn : DevExpress.XtraReports.UI.XtraReport
    {
        public readonly string RepFileName = "RepReturn.repx";
        public short copies = 1;
        public int BillID = -1;
        SODal soDal = new SODal();
        ReturnManageDal rtnDal = new ReturnManageDal();
        ReturnBody dataSource = null;
        ReturnHeaderEntity header = null;
        string _module = string.Empty;

        public RepReturn()
        {
            InitializeComponent();
            string reportFilePath = Path.Combine(GlobeSettings.AppPath, RepFileName);
            if (File.Exists(reportFilePath))
            {
                this.LoadLayout(reportFilePath);
            }
            this.PrintingSystem.StartPrint += new DevExpress.XtraPrinting.PrintDocumentEventHandler(PrintingSystem_StartPrint);
        }

        public RepReturn(int billID, short copies, string module)
            : this()
        {
            BillID = billID;
            this.copies = copies;
            this._module = module;
            //获取数据
            try
            {
                header = rtnDal.GetHeaderInfoByBillID(BillID);
                List<ReturnDetailsEntity> details = rtnDal.GetReturnDetails(BillID);

                dataSource = new ReturnBody();
                dataSource.CompanyInfo = new CompanyDal().GetCompanys()[0];
                dataSource.Header = header;
                dataSource.Details = details;
                //dataSource.Customer = new CustomerDal().GetByCode(header.Customer);
                lblDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                lblWarehouse.Text = GlobeSettings.LoginedUser.WarehouseName;
                lblSoNo.Text = header.OriginalBillNo;
                this.PageHeight = details.Count * 63 + 1150;
                //decimal totalAmount = header.CrnAmount;
                ////foreach (ReturnDetailsEntity itm in details)
                ////{
                ////    totalAmount += itm.ReturnAmount;
                ////}
                //totalAmount += header.ReturnAmount;
                //lblTotalAmount.Text = totalAmount.ToString();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        public RepReturn(int billID, short copies)
            : this()
        {
            BillID = billID;
            this.copies = copies;

            //获取数据
            try
            {
                header = rtnDal.GetHeaderInfoByBillID(BillID);
                List<ReturnDetailsEntity> details = rtnDal.GetReturnDetails(BillID);

                dataSource = new ReturnBody();
                dataSource.CompanyInfo = new CompanyDal().GetCompanys()[0];
                dataSource.Header = header;
                dataSource.Details = details;
                //dataSource.Customer = new CustomerDal().GetByCode(header.Customer);
                lblDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                lblWarehouse.Text = GlobeSettings.LoginedUser.WarehouseName;
                lblSoNo.Text = header.OriginalBillNo;
                this.PageHeight = details.Count * 63 + 1150;
                //decimal totalAmount = header.CrnAmount;
                ////foreach (ReturnDetailsEntity itm in details)
                ////{
                ////    totalAmount += itm.ReturnAmount;
                ////}
                //totalAmount += header.ReturnAmount;
                //lblTotalAmount.Text = totalAmount.ToString();
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
            LogDal.Insert(ELogType.打印, GlobeSettings.LoginedUser.UserName, dataSource.Header.BillNo, "销售退货单", this._module + "-RepReturn");
        }
    }
}
