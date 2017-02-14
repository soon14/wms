using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Nodes.Instore
{
    public partial class FrmPoReportByMonth : DevExpress.XtraEditors.XtraForm
    {
        RepPoByMonth report = null;
        public FrmPoReportByMonth()
        {
            InitializeComponent();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            report = new RepPoByMonth();
            printControl1.PrintingSystem = report.PrintingSystem;
            report.CreateDocument();
        }
    }
}