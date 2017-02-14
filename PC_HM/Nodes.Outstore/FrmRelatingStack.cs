using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.Utils;
using Nodes.Entities;
using Nodes.DBHelper;
using Nodes.UI;


namespace Nodes.Outstore
{
    public partial class FrmRelatingStack : DevExpress.XtraEditors.XtraForm
    {
        ReturnHeaderEntity headerEntity = null;
        private ReturnManageDal rtnDal;

        public FrmRelatingStack(ReturnHeaderEntity header)
        {
            InitializeComponent();
            headerEntity = header;
            rtnDal = new ReturnManageDal();
        }

        private void FrmRelatingStack_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = "关联托盘记录：客户<" + headerEntity.CustomerName + ">,退货单<" + headerEntity.BillID + ">";
                this.gridHeader.DataSource = rtnDal.GetRelatingStackInfo(headerEntity.BillID);
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
                return;
            }
        }
    }
}