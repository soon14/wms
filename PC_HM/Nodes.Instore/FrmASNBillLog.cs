using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.UI;

namespace Nodes.Instore
{
    public partial class FrmASNBillLog : DevExpress.XtraEditors.XtraForm
    {
        private int billID;

        public FrmASNBillLog(int billID)
        {
            InitializeComponent();

            this.billID = billID;
        }

        private void FrmASNBillLog_Load(object sender, EventArgs e)
        {
            try
            {
                //DataTable data = BillLogDal.Query(billID);
                //gridControl1.DataSource = data;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
    }
}