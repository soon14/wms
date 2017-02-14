using System.IO;
using Nodes.Shares;

namespace Nodes.WMS.Inbound
{
    public partial class RepSequenceLabel : DevExpress.XtraReports.UI.XtraReport
    {
        public readonly string RepFileName = "RepSequenceLabel.repx";
        public string PrintedData = null;
        public short copies = 1;

        public RepSequenceLabel()
        {
            InitializeComponent();

            string reportFilePath = Path.Combine(GlobeSettings.AppPath, RepFileName);
            if (File.Exists(reportFilePath)) this.LoadLayout(reportFilePath);

            this.PrintingSystem.StartPrint += new DevExpress.XtraPrinting.PrintDocumentEventHandler(PrintingSystem_StartPrint);
        }

        public RepSequenceLabel(string PrintedContent, short copies)
            : this()
        {
            PrintedData = PrintedContent;
            this.copies = copies;
        }

        void PrintingSystem_StartPrint(object sender, DevExpress.XtraPrinting.PrintDocumentEventArgs e)
        {
            e.PrintDocument.PrinterSettings.Collate = true;
            e.PrintDocument.PrinterSettings.Copies = this.copies;
        }

        private void XtraReport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.xrBarCode1.Text = PrintedData;
        }
    }
}
