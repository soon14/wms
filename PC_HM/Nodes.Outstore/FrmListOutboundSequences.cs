using System;
using System.Data;
using Nodes.DBHelper;
using Nodes.Utils;
using Nodes.UI;

namespace Nodes.WMS.Outbound
{
    public partial class FrmListOutboundSequences : DevExpress.XtraEditors.XtraForm
    {
        private int billID;
        public FrmListOutboundSequences(int billID, string billNO)
        {
            InitializeComponent();

            this.billID = billID;
            this.Text = this.Text + string.Format("(单据：{0})", billNO);
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            try
            {
                DataTable data = new SODal().ListOutboundSequence(billID);
                gridControl1.DataSource = data;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
    }
}