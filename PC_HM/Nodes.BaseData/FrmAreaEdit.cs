using System;
using System.Windows.Forms;
using Nodes.UI;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using System.Collections.Generic;
using Nodes.Shares;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.BaseData;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nodes.BaseData
{
    public partial class FrmAreaEdit : DevExpress.XtraEditors.XtraForm
    {
        private AreaEntity editEntity = null;
        public event EventHandler DataSourceChanged = null;
        private bool isNew = true;
        private AreaDal dal = null;

        public FrmAreaEdit()
        {
            InitializeComponent();
        }

        public FrmAreaEdit(AreaEntity editEntity, bool isNew)
            : this()
        {
            this.editEntity = editEntity;
            this.isNew = isNew;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            this.dal = new AreaDal();

            if (!isNew)
            {
                this.Text = "区域信息-修改";
                layoutControlItem3.Visibility = layoutControlItem11.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                txtCode.Enabled = false;
                txtCode.Properties.Buttons[0].Visible = false;
                ShowEditInfo();
            }
            else
            {
                BindCategories();
            }
        }

        void BindCategories()
        {
            treeParent.ParentFieldName = "ParentID";
            treeParent.KeyFieldName = "ID";
            treeParent.DisplayFieldName = "DisplayName";
            treeParent.ValueFieldName = "Code";
            treeParent.BindFields = "Code,编码;Name,名称";
            treeParent.AnySelect = true;
            treeParent.DataSource = this.dal.GetAll();
            treeParent.ExpandTree();

            treeParent.SetFocusedByID(editEntity.ID);
        }

        private void ShowEditInfo()
        {
            txtName.Text = editEntity.Name;
            txtCode.Text = editEntity.Code;
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
            if (string.IsNullOrEmpty(txtCode.Text.Trim()))
            {
                MsgBox.Warn("编码不能为空。");
                return false;
            }

            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                MsgBox.Warn("名称不能为空。");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 添加或编辑区域
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isNew">添加或编辑</param>
        /// <returns></returns>
        public bool SaveUpdateArea(AreaEntity entity, bool isNew)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("arCode=").Append(entity.Code).Append("&");
                loStr.Append("arName=").Append(entity.Name).Append("&");
                loStr.Append("id=").Append(entity.ID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveUpdateArea);
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
                AreaEntity editEntity = PrepareSave();
                //int ret = dal.Save(editEntity, isNew);
                //if (ret == -1)
                //    MsgBox.Warn("编号已存在，请改为其他的编号。");
                //else if (ret == -2)
                //    MsgBox.Warn("更新失败，该行已经被其他人删除。");
                //else
                bool ret = SaveUpdateArea(editEntity, isNew);
                if(ret)
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

        public AreaEntity PrepareSave()
        {
            AreaEntity entity = editEntity;
            if (this.isNew)
            {
                entity = new AreaEntity();
                entity.ParentID = ConvertUtil.ToInt(treeParent.GetFieldValue("ID"));
            }

            entity.Name = txtName.Text.Trim();
            entity.Code = txtCode.Properties.Buttons[0].Caption + txtCode.Text.Trim();

            return entity;
        }

        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            if (Save())
                this.DialogResult = DialogResult.OK;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                if (this.isNew)
                    Continue();
                else
                    this.DialogResult = DialogResult.OK;
            }
        }

        private void treeParent_EditValueChanged(object sender, EventArgs e)
        {
            //把选中的根节点排除掉
            ShowParentCategoryCode();
        }

        private void ShowParentCategoryCode()
        {
            if (treeParent.InnerValue == "0")
                txtCode.Properties.Buttons[0].Caption = string.Empty;
            else
                txtCode.Properties.Buttons[0].Caption = treeParent.InnerValue;
        }
    }
}