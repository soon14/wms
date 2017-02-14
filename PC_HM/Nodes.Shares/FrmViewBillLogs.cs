using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
//using Nodes.WMS.DBHelper;

namespace Nodes.WMS.Shares
{
    public partial class FrmViewBillLogs : DevExpress.XtraEditors.XtraForm
    {
        public FrmViewBillLogs()
        {
            InitializeComponent();
        }

        public FrmViewBillLogs(string billID, int billType)
        {
            //BillLogDal logDal = new BillLogDal();

        }
    }
}