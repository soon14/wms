using System;
using System.Data;
using Nodes.DBHelper;
using Nodes.Utils;
using Nodes.Controls;

namespace Nodes.Instore
{
    public partial class FrmListInboundDetails : DevExpress.XtraEditors.XtraForm
    {
        private int billID;

        public FrmListInboundDetails(int billID, string billNO)
        {
            InitializeComponent();

            this.billID = billID;
            this.Text = this.Text + string.Format("(单据：{0})", billNO);
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            try
            {
                DataTable data = new AsnDal().ListInboundDetails(billID);
                gridControl1.DataSource = data;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
    }
}