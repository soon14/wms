using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.UI;
using Nodes.Shares;

namespace Nodes.BaseData
{
    public partial class FrmSupplierEdit : DevExpress.XtraEditors.XtraForm
    {
        private SupplierEntity supplierEntity = null;
        public event EventHandler DataSourceChanged = null;
        private bool isNew = true;
        private SupplierDal supplierDal = null;
        private ProvinceDal provinceDal = null;

        public FrmSupplierEdit()
        {
            InitializeComponent();
        }

        public FrmSupplierEdit(SupplierEntity supplierEntity)
            : this()
        {
            this.supplierEntity = supplierEntity;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            supplierDal = new SupplierDal();
            provinceDal = new ProvinceDal();
            BindingCombox();
            if (supplierEntity != null)
            {
                this.Text = "供应商-修改";
                txtCode.Enabled = false;
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ShowEditInfo(supplierEntity);
                isNew = false;

                //非自有数据不允许编辑
                if (supplierEntity.IsOwn == "N")
                    btnSave.Enabled = false;
            }
        }

        private void BindingCombox()
        {
            listProvince.Properties.DataSource = provinceDal.GetAllProvince();
        }

        private void ShowEditInfo(SupplierEntity supplier)
        {
            //txtCode.Text = supplier.SupplierCode;
            //txtName.Text = supplier.SupplierName;
            //txtNameS.Text = supplier.SupplierNameS;
            //txtNamePY.Text = supplier.SupplierNamePY;
            //listProvince.Text = supplier.Province;
            //txtContact.Text = supplier.ContactName;
            //txtPhone.Text = supplier.Phone;
            //txtAddress.Text = supplier.Address;
            //txtPostcode.Text = supplier.Postcode;
            //spinPriority.Value = supplier.SortOrder;
            //cbIsActive.Text = supplier.IsActive;
        }

        private void Continue()
        {
            txtName.Text = "";
            txtNameS.Text = "";
            txtNamePY.Text = "";
            txtContact.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            txtPostcode.Text = "";

            if (checkAutoIncrement.Checked)
            {
                txtCode.Text = AutoIncrement.NextCode(txtCode.Text.Trim());
                txtName.Focus();
            }
            else
            {
                txtCode.Text = "";
                txtCode.Focus();
            }
        }

        private bool IsFieldValueValid()
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                MsgBox.Warn("供应商编码不能为空。");
                return false;
            }

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MsgBox.Warn("供应商名称不能为空。");
                return false;
            }

            return true;
        }

        private bool Save()
        {
            if (!IsFieldValueValid()) return false;
            bool success = false;
            try
            {
                SupplierEntity editEntity = PrepareSave();
                int ret = supplierDal.Save(editEntity, isNew);
                if (ret == -1)
                    MsgBox.Warn("供应商编号已存在，请改为其他的编号。");
                else if (ret == -2)
                    MsgBox.Warn("更新失败，该行已经被其他人删除。");
                else
                {
                    success = true;
                    if (DataSourceChanged != null)
                    {
                        DataSourceChanged(editEntity, null);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
            }
            return success;
        }

        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            if (Save())
                this.DialogResult = DialogResult.OK;
        }

        public SupplierEntity PrepareSave()
        {
            SupplierEntity editEntity = supplierEntity;
            if (editEntity == null)
            {
                editEntity = new SupplierEntity();

                editEntity.IsOwn = "Y";
                editEntity.LastUpdateBy = GlobeSettings.LoginedUser.UserName;
                editEntity.LastUpdateDate = DateTime.Now;
            }

            editEntity.SupplierCode = txtCode.Text.Trim();
            editEntity.SupplierName = txtName.Text.Trim();
            editEntity.SupplierNameS = txtNameS.Text.Trim();
            editEntity.SupplierNamePY = txtNamePY.Text.Trim();
           // editEntity.Province = listProvince.Text;
            editEntity.ContactName = txtContact.Text.Trim();
            editEntity.Phone = txtPhone.Text.Trim();
            editEntity.Address = txtAddress.Text.Trim();
            editEntity.Postcode = txtPostcode.Text.Trim();
            editEntity.SortOrder = (int)spinPriority.Value;
            editEntity.IsActive = cbIsActive.Text;

            return editEntity;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                if (supplierEntity == null)
                    Continue();
                else
                    this.DialogResult = DialogResult.OK;
            }
        }

        private void lookUpEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
            {
                listProvince.EditValue = null;
                listProvince.Text = string.Empty;
                listProvince.ClosePopup();
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            txtNamePY.Text = PinYin.GetCapital(txtName.Text.Trim());
            txtNameS.Text = txtName.Text.Trim();
        }
    }
}