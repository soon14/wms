using System.Collections.Generic;
using System.IO;
using Nodes.Entities;
using Nodes.Shares;
using Nodes.Utils;

namespace Nodes.SystemManage
{
    public partial class RepUserCard : DevExpress.XtraReports.UI.XtraReport
    {
        public readonly string RepFileName = "RepUserCard.repx";
        public List<UserEntity> PrintedData = null;
        public short copies = 1;

        public RepUserCard()
        {
            InitializeComponent();
        }

        public RepUserCard(List<UserEntity> PrintedData, short copies)
            : this()
        {
            this.PrintedData = PrintedData;

            this.copies = copies;

            string reportFilePath = Path.Combine(PathUtil.ApplicationStartupPath, RepFileName);
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
    }
}
