using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using DevExpress.Utils;
using Nodes.UI;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Resources;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;
using Nodes.Entities.HttpEntity.SystemManage;
using Newtonsoft.Json;

namespace Nodes.SystemManage
{
    /// <summary>
    /// 组织架构之公司信息：目前只支持一个公司
    /// </summary>
    public partial class FrmCompanyManager : DevExpress.XtraEditors.XtraForm
    {
        List<CompanyEntity> listCompany = null;
       // CompanyDal comDal = new CompanyDal();

        public FrmCompanyManager()
        {
            InitializeComponent();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            //为工具栏设置图标
            ImageCollection ic = AppResource.LoadToolImages();
            barManager1.Images = ic;
            toolRefresh.ImageIndex = (int)AppResource.EIcons.refresh;
            toolAdd.ImageIndex = (int)AppResource.EIcons.add;
            toolEdit.ImageIndex = (int)AppResource.EIcons.edit;
            toolDelete.ImageIndex = (int)AppResource.EIcons.delete;

            //显示公司信息
            LoadCompanyInfo();
        }

        /// <summary>
        /// 退货单管理, 获取公司信息
        /// </summary>
        /// <returns></returns>
        public List<CompanyEntity> GetCompanys()
        {
            List<CompanyEntity> temp = new List<CompanyEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                string jsons = string.Empty;
                string jsonQuery = WebWork.SendRequest(jsons, WebWork.URL_GetCompanys);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return temp;
                }
                #endregion

                #region 正常错误处理

                JsonGetCompanys bill = JsonConvert.DeserializeObject<JsonGetCompanys>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return temp;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return temp;
                }
                #endregion
                
                #region 赋值数据
                foreach (JsonGetCompanysResult jbr in bill.result)
                {
                    CompanyEntity asnEntity = new CompanyEntity();
                    asnEntity.Address = jbr.addr;
                    asnEntity.CompanyCode = jbr.companyCode;
                    asnEntity.CompanyName = jbr.companyName;
                    asnEntity.Email = jbr.email;
                    asnEntity.Fax = jbr.fax;
                    asnEntity.Phone = jbr.phone;
                    asnEntity.Postcode = jbr.postCode;
                    asnEntity.Remark = jbr.remark;
                    temp.Add(asnEntity);
                }

                return temp;
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return temp;
        }

        private void LoadCompanyInfo()
        {
            listCompany = GetCompanys();
            bindingSource1.DataSource = listCompany;
        }

        CompanyEntity FocusedHeader
        {
            get
            {
                if (gvHeader.FocusedRowHandle < 0)
                    return null;
                else
                    return gvHeader.GetFocusedRow() as CompanyEntity;
            }
        }

        void EditFocusedCompany()
        {
            //if (!GlobeSettings.HasRight(2001))
            //    return;

            CompanyEntity company = FocusedHeader;
            if (company == null)
            {
                MsgBox.Warn("请选中要修改的行。");
                return;
            }

            FrmCompanyEdit frmCompanyEdit = new FrmCompanyEdit(company);
            frmCompanyEdit.DataSourceChanged += OnDataChanged;
            frmCompanyEdit.ShowDialog();
        }

        void CreateCompany()
        {
            FrmCompanyEdit frmCompanyEdit = new FrmCompanyEdit(null);
            frmCompanyEdit.DataSourceChanged += OnDataChanged;
            frmCompanyEdit.ShowDialog();
        }

        /// <summary>
        /// 删除公司信息
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns>0：成功；1：被用户引用，无法删除</returns>
        public bool DeleteCompany(string CompanyCode)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                #region
                loStr.Append("CompanyCode=").Append(CompanyCode);
                #endregion
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_DeleteCompany);
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

        void DoDelete()
        {
            CompanyEntity company = FocusedHeader;
            if (company == null)
            {
                MsgBox.Warn("请选中要删除的公司。");
                return;
            }

            if (MsgBox.AskOK(string.Format("确定要删除公司“{0}”吗？", company.CompanyName)) != DialogResult.OK)
                return;

            //开始存入数据库
            bool result = DeleteCompany(company.CompanyCode);
            if (!result)
            {
                //MsgBox.Warn("无法删除该公司信息，因为有部门信息在引用它。");
                return;
            }

            bindingSource1.Remove(company);
        }

        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string tag = ConvertUtil.ToString(e.Item.Tag);
            DoEvent(tag);
        }

        private void DoEvent(string tag)
        {
            switch (tag)
            {
                case "刷新":
                    LoadCompanyInfo();
                    break;
                case "添加":
                    CreateCompany();
                    break;
                case "修改":
                    EditFocusedCompany();
                    break;
                case "删除":
                    DoDelete();
                    break;
            }
        }

        private void OnDataChanged(object sender, EventArgs e)
        {
            CompanyEntity newEntity = (CompanyEntity)sender;
            CompanyEntity oldEntity = listCompany.Find(item => item.CompanyCode == newEntity.CompanyCode);

            if (oldEntity == null)
                bindingSource1.Add(newEntity);
            else
                bindingSource1.ResetBindings(false);
        }

        private void OnRowDoubleClick(object sender, EventArgs e)
        {
            EditFocusedCompany();
        }
    }
}