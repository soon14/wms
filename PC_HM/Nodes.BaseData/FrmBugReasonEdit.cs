using System;
using System.Collections.Generic;
using System.Windows.Forms;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.UI;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.BaseData;
using Newtonsoft.Json;

namespace Nodes.BaseData
{
    public partial class FrmBugReasonEdit : DevExpress.XtraEditors.XtraForm
    {
        private BusReasonEntity bugEntity = null;
        public event EventHandler DataSourceChanged = null;
        private bool isNew = true;
        //private BugReasonDal bugDal = null;

        public FrmBugReasonEdit()
        {
            InitializeComponent();
        }

        public FrmBugReasonEdit(BusReasonEntity bugEntity)
            : this()
        {
            this.bugEntity = bugEntity;
        }

        private void FrmUnitEdit_Load(object sender, EventArgs e)
        {
            //bugDal = new BugReasonDal();

            if (bugEntity != null)
            {
                this.Text = "不合格原因-修改";
                txtCode.Enabled = false;
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ShowEditInfo(bugEntity);
                isNew = false;
            }
        }

        #region 自定义方法

        private void ShowEditInfo(BusReasonEntity bugEntity)
        {
            txtCode.Text = bugEntity.BugCode;
            txtName.Text = bugEntity.BugName;
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
                MsgBox.Warn("编码不能为空。");
                return false;
            }

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MsgBox.Warn("名称不能为空。");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 添加或编辑
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operatorFlag">添加或编辑</param>
        /// <returns></returns>
        public bool SaveAddBugReason(BusReasonEntity entity, bool isNew)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("bugCode=").Append(entity.BugCode).Append("&");
                loStr.Append("bugName=").Append(entity.BugName);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveAddBugReason);
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

        /// <summary>
        /// 基础管理（不合格原因-编辑不合格原因）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isNew"></param>
        /// <returns></returns>
        public bool SaveUpdateBugReason(BusReasonEntity entity, bool isNew)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("bugCode=").Append(entity.BugCode).Append("&");
                loStr.Append("bugName=").Append(entity.BugName);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveUpdateBugReason);
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
                BusReasonEntity editEntity = PrepareSave();
                //int ret = bugDal.Save(editEntity, isNew);
                //if (ret == -1)
                //    MsgBox.Warn("编号已存在，请改为其他的编号。");
                //else if (ret == -2)
                //    MsgBox.Warn("更新失败，该行已经被其他人删除。");
                //else
                bool ret;
                if (isNew)
                    ret = SaveAddBugReason(editEntity, isNew);
                else
                    ret = SaveUpdateBugReason(editEntity, isNew);
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
                MsgBox.Warn(ex.Message);
            }
            return success;
        }
        #endregion

        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            if (Save())
                this.DialogResult = DialogResult.OK;
        }

        public BusReasonEntity PrepareSave()
        {
            BusReasonEntity editEntity = bugEntity;
            if (editEntity == null) editEntity = new BusReasonEntity();
            editEntity.BugCode = txtCode.Text.Trim();
            editEntity.BugName = txtName.Text.Trim();
            return editEntity;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                if (bugEntity == null)
                    Continue();
                else
                    this.DialogResult = DialogResult.OK;
            }
        }
    }
}