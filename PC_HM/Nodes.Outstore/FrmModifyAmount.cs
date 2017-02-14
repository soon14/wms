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
    public partial class FrmModifyAmount : DevExpress.XtraEditors.XtraForm
    {
        ReturnHeaderEntity headerEntity = null;
        private ReturnManageDal rtnDal;
        private decimal _crnAmount = 0.00m;

        public FrmModifyAmount(ReturnHeaderEntity header)
        {
            InitializeComponent();

            headerEntity = header;
            rtnDal = new ReturnManageDal();
        }

        private void FrmModifyAmount_Load(object sender, EventArgs e)
        {
            lblBillNo.Text = headerEntity.BillNo;
            lblDriver.Text = headerEntity.ReturnDriver;
            txtCrnAmount.Text = headerEntity.CrnAmount.ToString();
            txtCrnAmount.Focus();
        }

        public decimal CrnAmount
        {
            get { return this._crnAmount; }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ConvertUtil.IsDecimal(txtCrnAmount.Text.Trim()))
            {
                MsgBox.Warn("退货金额必须是数字！");
                txtCrnAmount.Focus();
                return;
            }
            if (ConvertUtil.ToDecimal(txtCrnAmount.Text.Trim()) < 0)
            {
                MsgBox.Warn("退货金额不能小于0！");
                txtCrnAmount.Focus();
                return;
            }
            try
            {
                this._crnAmount = ConvertUtil.ToDecimal(txtCrnAmount.Text.Trim());
                int rtn = rtnDal.ModifyReturnAmount(headerEntity.BillID, this._crnAmount);
                if (rtn > 0)
                {
                    MsgBox.OK("保存成功。");
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MsgBox.Warn("保存失败。");
                    txtCrnAmount.Focus();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
    }
}