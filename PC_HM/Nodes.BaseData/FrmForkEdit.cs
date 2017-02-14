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
    public partial class FrmForkEdit : DevExpress.XtraEditors.XtraForm
    {
        private ForkEntity forkEntity = null;
        public event EventHandler DataSourceChanged = null;
        private bool isNew = true;
        //private ForkDal forkDal = null;

        public FrmForkEdit()
        {
            InitializeComponent();
        }

        public FrmForkEdit(ForkEntity forkEntity)
            : this()
        {
            this.forkEntity = forkEntity;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            //forkDal = new ForkDal();

            if (forkEntity != null)
            {
                this.Text = "叉车-修改";
                txtCode.Enabled = false;
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ShowEditInfo(forkEntity);
                isNew = false;
            }
        }

        #region 自定义方法

        private void ShowEditInfo(ForkEntity forkliftEntity)
        {
            txtCode.Text = forkliftEntity.ForkliftCode;
            txtName.Text = forkliftEntity.ForkliftName;
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
                MsgBox.Warn("叉车编码不能为空。");
                return false;
            }

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MsgBox.Warn("叉车名称不能为空。");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 添加或编辑叉车
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isNew">添加或编辑</param>
        /// <returns></returns>
        public bool SaveAddForkInfo(ForkEntity fork, bool isNew)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("forkCode=").Append(fork.ForkliftCode).Append("&");
                loStr.Append("forkName=").Append(fork.ForkliftName);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveAddForkInfo);
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
        /// 基础管理（叉车信息-编辑叉车信息）
        /// </summary>
        /// <param name="fork"></param>
        /// <param name="isNew"></param>
        /// <returns></returns>
        public bool SaveUpdateForkInfo(ForkEntity fork, bool isNew)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("forkCode=").Append(fork.ForkliftCode).Append("&");
                loStr.Append("forkName=").Append(fork.ForkliftName);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveUpdateForkInfo);
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
                ForkEntity editEntity = PrepareSave();
                    //int ret = forkDal.Save(editEntity, isNew);
                    //if (ret == -1)
                    //    MsgBox.Warn("叉车编号或名称已存在，请改为其他的叉车编号或名称。");
                    //else if (ret == -2)
                    //    MsgBox.Warn("更新失败，该行已经被其他人删除。");
                    //else
                bool ret;
                if (isNew)
                    ret = SaveAddForkInfo(editEntity, isNew);
                else
                    ret = SaveUpdateForkInfo(editEntity, isNew);

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

        public ForkEntity PrepareSave()
        {
            ForkEntity editEntity = forkEntity;
            if (editEntity == null) editEntity = new ForkEntity();
            editEntity.ForkliftCode = txtCode.Text.Trim();
            editEntity.ForkliftName = txtName.Text.Trim();
            return editEntity;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                if (forkEntity == null)
                    Continue();
                else
                    this.DialogResult = DialogResult.OK;
            }
        }
    }
}
