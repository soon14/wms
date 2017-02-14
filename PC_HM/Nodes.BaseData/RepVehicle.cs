using System.Collections.Generic;
using System.IO;
using Nodes.Entities;
using Nodes.Shares;
using Nodes.DBHelper;

namespace Nodes.BaseData
{
    public partial class RepVehicle : DevExpress.XtraReports.UI.XtraReport
    {
        public readonly string RepFileName = "RepVehicle.repx";
        public List<VehicleEntity> PrintedData = null;
        public short copies = 1;

        public RepVehicle()
        {
            InitializeComponent();
        }

        public RepVehicle(List<VehicleEntity> PrintedData, short copies)
            : this()
        {
            this.PrintedData = PrintedData;

            this.copies = copies;

            string reportFilePath = Path.Combine(GlobeSettings.AppPath, RepFileName);
            if (File.Exists(reportFilePath)) this.LoadLayout(reportFilePath);

            this.PrintingSystem.StartPrint += new DevExpress.XtraPrinting.PrintDocumentEventHandler(PrintingSystem_StartPrint);
        }

        void PrintingSystem_StartPrint(object sender, DevExpress.XtraPrinting.PrintDocumentEventArgs e)
        {
            e.PrintDocument.PrinterSettings.Collate = true;
            e.PrintDocument.PrinterSettings.Copies = this.copies;
        }

        private void XtraReport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.PrintedData != null)
            {
                this.DataSource = PrintedData;
            }
        }

        private void RepLocation_AfterPrint(object sender, System.EventArgs e)
        {
            //记录打印张数和人
            int pageCount = this.Pages.Count * this.copies;
            new BillLogDal().SavePrintLog(this.PrintedData[0].VehicleCode, pageCount, "打印车辆标签", GlobeSettings.LoginedUser.UserName); 
        }
    }
}
