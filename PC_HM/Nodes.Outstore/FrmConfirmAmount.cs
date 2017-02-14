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
using Nodes.Resources;
using Nodes.Utils;

namespace Nodes.Outstore
{
    public partial class FrmConfirmAmount : DevExpress.XtraEditors.XtraForm
    {
        private SOHeaderEntity headerEntity;
        private SODal soDal = null;
        public string VehicleNo { get; set; }

        public FrmConfirmAmount(SOHeaderEntity header)
        {
            InitializeComponent();
            soDal = new SODal();
            headerEntity = header;
        }

        private void FrmConfirmAmount_Load(object sender, EventArgs e)
        {
            lblBillNo.Text = headerEntity.BillNO;
            lblContract.Text = headerEntity.Consignee;
            lblCustomer.Text = headerEntity.CustomerName;
            lblVehicle.Text = VehicleNo;

            txtRealAmount.Text = headerEntity.RealAmount.ToString();
            txtReceiveAmount.Text = headerEntity.ReceiveAmount.ToString();
            txtCrnAmount.Text = headerEntity.CrnAmount.ToString();
            txtOtherAmount.Text = headerEntity.OtherAmount.ToString();
            txtOtherRemark.Text = headerEntity.OtherRemark;
            chkPaymentFlag.Checked = headerEntity.PaymentFlagDesc;
            txtReceiveAmount.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!IsValidate()) return;
            if (MsgBox.AskOK("确定保存该出库单的数据吗？") != DialogResult.OK) return;

            int rtn = SaveAmount();
            if (rtn > 0)
            {
                MsgBox.Warn("保存成功。");
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MsgBox.Warn("保存失败。");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private int SaveAmount()
        {
            try
            {
                return soDal.SaveAmount(headerEntity.BillID, ConvertUtil.ToDecimal(txtReceiveAmount.Text.Trim()), ConvertUtil.ToDecimal(txtRealAmount.Text.Trim()),
                    ConvertUtil.ToDecimal(txtCrnAmount.Text.Trim()), ConvertUtil.ToDecimal(txtOtherAmount.Text.Trim()), txtOtherRemark.Text.Trim(), chkPaymentFlag.Checked);
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
                return -1;
            }
        }

        private bool IsValidate()
        {
            decimal recAmount, realAmount, crnAmount, otherAmount;
            if (!ConvertUtil.IsDecimal(this.txtReceiveAmount.Text.Trim()))
            {
                MsgBox.Warn("应收金额必须是数字！");
                txtReceiveAmount.Focus();
                return false;
            }
            recAmount = ConvertUtil.ToDecimal(txtReceiveAmount.Text.Trim());
            //if (recAmount < 0)
            //{
            //    MsgBox.Warn("应收金额不能小于0！");
            //    txtReceiveAmount.Focus();
            //    return false;
            //}
            if (!ConvertUtil.IsDecimal(this.txtRealAmount.Text.Trim()))
            {
                MsgBox.Warn("实收现金必须是数字！");
                txtRealAmount.Focus();
                return false;
            }
            realAmount = ConvertUtil.ToDecimal(txtRealAmount.Text.Trim());
            //if (realAmount < 0)
            //{
            //    MsgBox.Warn("实收现金不能小于0！");
            //    txtRealAmount.Focus();
            //    return false;
            //}
            if (!ConvertUtil.IsDecimal(this.txtCrnAmount.Text.Trim()))
            {
                MsgBox.Warn("退货金额必须是数字！");
                txtCrnAmount.Focus();
                return false;
            }
            crnAmount = ConvertUtil.ToDecimal(txtCrnAmount.Text.Trim());
            //if (crnAmount < 0)
            //{
            //    MsgBox.Warn("退货金额不能小于0！");
            //    txtCrnAmount.Focus();
            //    return false;
            //}
            if (!ConvertUtil.IsDecimal(this.txtOtherAmount.Text.Trim()))
            {
                MsgBox.Warn("它项金额必须是数字！");
                txtOtherAmount.Focus();
                return false;
            }
            otherAmount = ConvertUtil.ToDecimal(txtOtherAmount.Text.Trim());
            //if (otherAmount < 0)
            //{
            //    MsgBox.Warn("它项金额不能小于0！");
            //    txtOtherAmount.Focus();
            //    return false;
            //}
            if (realAmount != recAmount - crnAmount + otherAmount)
            {
                MsgBox.Warn("<实收现金>应该等于<应收金额>减掉<退货金额>加上<它项金额>！");
                txtRealAmount.Focus();
                return false;
            }
            return true;
        }

       
    }
}