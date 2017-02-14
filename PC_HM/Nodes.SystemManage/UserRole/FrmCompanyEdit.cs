using System;
using System.Collections.Generic;
using System.Windows.Forms;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.UI;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;
using Nodes.Entities.HttpEntity.SystemManage;
using Newtonsoft.Json;

namespace Nodes.SystemManage
{
    public partial class FrmCompanyEdit : DevExpress.XtraEditors.XtraForm
    {
        public event EventHandler DataSourceChanged = null;
       // private CompanyDal companyDal = null;
        private CompanyEntity m_Company = null;
        private bool isNew = false;

        public FrmCompanyEdit()
        {
            InitializeComponent();
        }

        public FrmCompanyEdit(CompanyEntity Company)
            : this()
        {
            //companyDal = new CompanyDal();

            if (Company != null)
            {
                txtCode.Enabled = false;
                layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                m_Company = Company;
                isNew = false;
            }
            else
            {
                isNew = true;
                this.Text = "添加公司信息";
            }
        }

        private void ShowEditCompany()
        {
            txtCode.Text = m_Company.CompanyCode;
            txtName.Text = m_Company.CompanyName;
            txtAddress.Text = m_Company.Address;
            txtPhone.Text = m_Company.Phone;
            txtFax.Text = m_Company.Fax;
            txtEmail.Text = m_Company.Email;
            txtRemark.Text = m_Company.Remark;
            txtPostcode.Text = m_Company.Postcode;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            if (m_Company != null)
            {
                ShowEditCompany();
            }
        }

        public CompanyEntity ReturnMain()
        {
            if (isNew)
                m_Company = new CompanyEntity();

            m_Company.CompanyCode = txtCode.Text.Trim();
            m_Company.CompanyName = txtName.Text.Trim();
            m_Company.Address = txtAddress.Text.Trim();
            m_Company.Phone = txtPhone.Text.Trim();
            m_Company.Fax = txtFax.Text.Trim();
            m_Company.Email = txtEmail.Text.Trim();
            m_Company.Remark = txtRemark.Text.Trim();
            m_Company.Postcode = txtPostcode.Text.Trim();

            return m_Company;
        }


        /// <summary>
        /// 新建或者修改公司信息
        /// </summary>
        /// <param name="Company"></param>
        /// <returns></returns>
        public bool CreateOrUpdateCompany(CompanyEntity Company, bool isCreateNew)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                #region 
                loStr.Append("id=").Append(Company.CompanyCode).Append("&");
                loStr.Append("name=").Append(Company.CompanyName).Append("&");
                loStr.Append("address=").Append(Company.Address).Append("&");
                loStr.Append("phone=").Append(Company.Phone).Append("&");
                loStr.Append("fax=").Append(Company.Fax).Append("&");
                loStr.Append("email=").Append(Company.Email).Append("&");
                loStr.Append("postCode=").Append(Company.Postcode).Append("&");
                loStr.Append("remark=").Append(Company.Remark).Append("&");
                loStr.Append("isCreateNew=").Append(isCreateNew);
                #endregion
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_CreateOrUpdateCompany);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return false;
                }
                #endregion

                #region 正常错误处理

                Sucess bill = JsonConvert.DeserializeObject<Sucess>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return false;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return false;
                }
                #endregion

                return true;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return false;
        }

        

        private bool Save()
        {
            if (!IsFieldValueValid()) return false;

            bool success = false;
            try
            {
                CompanyEntity detailEntity = ReturnMain();
                //int result = companyDal.CreateOrUpdateCompany(detailEntity, isNew);
                //if (result == -1)
                //    MsgBox.Warn("公司编号已存在，请修改为未被使用的编号。");
                //else if (result == -2)
                //    MsgBox.Warn("该公司信息可能已经被其他人删除，保存失败。。");
                //else
                if (CreateOrUpdateCompany(detailEntity, isNew))
                {
                    success = true;
                    if (DataSourceChanged != null)
                        DataSourceChanged(detailEntity, null);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return success;
        }

        private bool IsFieldValueValid()
        {
            if (string.IsNullOrEmpty(this.txtCode.Text.Trim()))
            {
                MsgBox.Warn("编号不能为空。");
                return false;
            }

            if (string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                MsgBox.Warn("名称不能为空。");
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                if (isNew)
                    Continue();
                else
                    this.DialogResult = DialogResult.OK;
            }
        }

        void Continue()
        {
            txtCode.Text = txtName.Text = txtAddress.Text = txtEmail.Text =
                txtFax.Text = txtPhone.Text = txtRemark.Text = txtPostcode.Text = "";
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            if (Save()) this.DialogResult = DialogResult.OK;
        }
    }
}