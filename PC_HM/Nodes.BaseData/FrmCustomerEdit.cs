using System;
using System.Windows.Forms;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.UI;
using Nodes.Shares;

namespace Nodes.BaseData
{
    public partial class FrmCustomerEdit : DevExpress.XtraEditors.XtraForm
    {
        private CustomerEntity customerEntity = null;
        public event EventHandler DataSourceChanged = null;
        private bool isNew = true;
        //private CustomerDal customerDal = null;

        public FrmCustomerEdit()
        {
            InitializeComponent();
        }

        public FrmCustomerEdit(CustomerEntity customerEntity)
            : this()
        {
            this.customerEntity = customerEntity;
        }

        private void FrmCustomerEdit_Load(object sender, EventArgs e)
        {
            //customerDal = new CustomerDal();
            BindingCombox();

            if (customerEntity != null)
            {
                this.Text = "客户-修改";
                txtCode.Enabled = false;
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ShowEditInfo(customerEntity);
                isNew = false;

                //非自有数据不允许编辑
                if (customerEntity.IsOwn == "N")
                    btnSave.Enabled = false;
            }
        }

        private void BindingCombox()
        {
            listRoutes.Properties.DataSource = new RouteDal().GetAll();
        }

        private void ShowEditInfo(CustomerEntity customer)
        {
            txtCode.Text = customer.CustomerCode;
            txtName.Text = customer.CustomerName;
            txtNameS.Text = customer.CustomerNameS;
            //lookUpEdit1.Text = customer.Province;
            txtPhone.Text = customer.Phone;
            txtAddress.Text = customer.Address;
            spinPriority.Value = customer.SortOrder;
            cbIsActive.Text = customer.IsActive;
            listRoutes.EditValue = customer.RouteCode;
            txtDistance.Text = ConvertUtil.ToString( customer.Distance);
        }

        private void Continue()
        {
            txtName.Text = "";
            txtNameS.Text = "";
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
                MsgBox.Warn("编号不能为空。");
                return false;
            }

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MsgBox.Warn("名称不能为空。");
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
                CustomerEntity editEntity = PrepareSave();
                //int ret = customerDal.CustomerAddAndUpdate(editEntity, isNew);
                //if (ret == -1)
                //    MsgBox.Warn("客户编号已存在，请改为其他的编号。");
                //else if (ret == -2)
                //    MsgBox.Warn("更新失败，该行已经被其他人删除。");
                //else
                //{
                //    success = true;
                //    if (DataSourceChanged != null)
                //    {
                //        DataSourceChanged(editEntity, null);
                //    }
                //}
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return success;
        }

        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            if (Save())
                this.DialogResult = DialogResult.OK;
        }

        public CustomerEntity PrepareSave()
        {
            CustomerEntity editEntity = customerEntity;
            if (editEntity == null)
            {
                editEntity = new CustomerEntity();
                editEntity.IsOwn = "Y";
                editEntity.LastUpdateBy = GlobeSettings.LoginedUser.UserName;
                editEntity.LastUpdateDate = DateTime.Now;
            }

            editEntity.CustomerCode = txtCode.Text.Trim();
            //editEntity.CustomerName = txtName.Text.Trim();
            //editEntity.CustomerNameS = txtNameS.Text.Trim();
            //editEntity.CustomerNamePY = txtNamePY.Text.Trim();
            //editEntity.Province = lookUpEdit1.Text;
            //editEntity.ContactName = txtContact.Text.Trim();
            //editEntity.Phone = txtPhone.Text.Trim();
            //editEntity.Address = txtAddress.Text.Trim();
            //editEntity.Postcode = txtPostcode.Text.Trim();
            //editEntity.SortOrder = (int)spinPriority.Value;
            //editEntity.IsActive = cbIsActive.Text;
            editEntity.RouteCode = ConvertUtil.ToString(listRoutes.EditValue);
            editEntity.RouteName = listRoutes.Text;
            editEntity.Distance = ConvertUtil.ToDecimal(txtDistance.Text.Trim());

            return editEntity;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                if (customerEntity == null)
                    Continue();
                else
                    this.DialogResult = DialogResult.OK;
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            txtNameS.Text = txtName.Text.Trim();
        }
    }
}