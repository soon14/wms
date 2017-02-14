using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.UI;
using Nodes.DBHelper;
using Nodes.Entities;

namespace Nodes.Outstore
{
    public partial class FrmOutstoreTypeModify : DevExpress.XtraEditors.XtraForm
    {
        private string itemValue = "";
        private SOHeaderEntity Entity = null;
        public FrmOutstoreTypeModify(SOHeaderEntity entity)
        {
            InitializeComponent();
            this.Entity = entity;
        }

        public string ItemValue
        {
            get { return itemValue; }
            set { }
        }

        private void OnFrmLoad(object sender, EventArgs e)
        {
            try
            {
                listInstoreType.Properties.DataSource = BaseCodeDal.GetItemList(BaseCodeConstant.OUTSTORE_TYPE);
                lblBillNO.Text = this.Entity.BillNO;
                listInstoreType.EditValue = this.Entity.OutstoreType;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (listInstoreType.Text.Trim() == "")
            {
                MsgBox.Warn("请选择出库方式！");
                return;
            }

            this.itemValue = listInstoreType.EditValue.ToString();
            this.DialogResult = DialogResult.OK;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}