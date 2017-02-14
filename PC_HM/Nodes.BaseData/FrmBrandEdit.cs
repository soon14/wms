using System;
using System.Windows.Forms;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.UI;
using System.Collections.Generic;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.BaseData;
using Nodes.Entities.HttpEntity.Stock;
using Newtonsoft.Json;

namespace Nodes.BaseData
{
    public partial class FrmBrandEdit : DevExpress.XtraEditors.XtraForm
    {
        private BrandEntity brandEntity = null;
        public event EventHandler DataSourceChanged = null;
        private bool isNew = true;
        //private BrandDal brandDal = null;

        public FrmBrandEdit()
        {
            InitializeComponent();
        }

        public FrmBrandEdit(BrandEntity brandEntity)
            : this()
        {
            this.brandEntity = brandEntity;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            //brandDal = new BrandDal();

            if (brandEntity != null)
            {
                this.Text = "品牌-修改";
                txtCode.Enabled = false;
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ShowEditInfo(brandEntity);
                isNew = false;
            }
        }

        #region 自定义方法

        private void ShowEditInfo(BrandEntity brandEntity)
        {
            txtCode.Text = brandEntity.BrandCode;
            txtName.Text = brandEntity.BrandName;
        }

        private void Continue()
        {
            txtName.Text = "";

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
                MsgBox.Warn("品牌编码不能为空。");
                return false;
            }

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MsgBox.Warn("品牌名称不能为空。");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 添加或编辑品牌
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isNew">添加或编辑</param>
        /// <returns></returns>
        public bool SaveUpdateBrandInfo(BrandEntity brands, bool isNew)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("brdName=").Append(brands.BrandName).Append("&");
                loStr.Append("brdCode=").Append(brands.BrandCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveUpdateBrandInfo);
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
                BrandEntity editEntity = PrepareSave();
                //int ret = brandDal.Save(editEntity, isNew);
                //if (ret == -1)
                //    MsgBox.Warn("品牌编号或名称已存在，请改为其他的品牌编号或名称。");
                //else if (ret == -2)
                //    MsgBox.Warn("更新失败，该行已经被其他人删除。");
                //else
                if (SaveUpdateBrandInfo(editEntity, isNew))
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
                MsgBox.Err(ex.Message);
            }
            return success;
        }
        #endregion

        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            if (Save())
                this.DialogResult = DialogResult.OK;
        }

        public BrandEntity PrepareSave()
        {
            BrandEntity editEntity = brandEntity;
            if (editEntity == null) editEntity = new BrandEntity();
            editEntity.BrandCode = txtCode.Text.Trim();
            editEntity.BrandName = txtName.Text.Trim();
            return editEntity;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                if (brandEntity == null)
                    Continue();
                else
                    this.DialogResult = DialogResult.OK;
            }
        }
    }
}
