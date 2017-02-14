using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Nodes.Outstore
{
    public partial class FrmLoadingCtQtyInput : DevExpress.XtraEditors.XtraForm
    {
        public FrmLoadingCtQtyInput()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (txtCtQty.Text.Trim() == null)
                return;
            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
