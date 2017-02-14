using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.Utils;
using Nodes.DBHelper;
using Nodes.Controls;

namespace Nodes.Instore
{
    public partial class FrmListInboundSummary : DevExpress.XtraEditors.XtraForm
    {
        private int billID;
        public FrmListInboundSummary(int billID, string billNO)
        {
            InitializeComponent();

            this.billID = billID;
            this.Text = this.Text + string.Format("(单据：{0})", billNO);
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            try
            {
                DataTable data = new AsnDal().ListInboundSummary(billID);
                gridControl1.DataSource = data;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
    }
}