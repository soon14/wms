using System;
using System.Windows.Forms;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.UI;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.BaseData;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nodes.BaseData
{
    public partial class FrmUnitEdit : DevExpress.XtraEditors.XtraForm
    {
        private UnitEntity unitEntity = null;
        public event EventHandler DataSourceChanged = null;
        private bool isNew = true;
        //private UnitDal unitDal = null;

        public FrmUnitEdit()
        {
            InitializeComponent();
        }

        public FrmUnitEdit(UnitEntity unitEntity)
            : this()
        {
            this.unitEntity = unitEntity;
        }

        private void FrmUnitEdit_Load(object sender, EventArgs e)
        {
            //unitDal = new UnitDal();

            if (unitEntity != null)
            {
                this.Text = "计量单位-修改";
                txtCode.Enabled = false;
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ShowEditInfo(unitEntity);
                isNew = false;
            }
        }

        #region 自定义方法

        private void ShowEditInfo(UnitEntity unitEntity)
        {
            txtCode.Text = unitEntity.UnitCode;
            txtName.Text = unitEntity.UnitName;
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
                MsgBox.Warn("计量单位编码不能为空。");
                return false;
            }

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MsgBox.Warn("计量单位名称不能为空。");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 添加或编辑计量单位
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isNew">添加或编辑</param>
        /// <returns></returns>
        public bool SaveAddWmUm(UnitEntity entity, bool isNew)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("umCode=").Append(entity.UnitCode).Append("&");
                loStr.Append("umName=").Append(entity.UnitName);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveAddWmUm);
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
        /// 添加或编辑计量单位
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isNew">添加或编辑</param>
        /// <returns></returns>
        public bool SaveUpdateWmUm(UnitEntity entity, bool isNew)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("umCode=").Append(entity.UnitCode).Append("&");
                loStr.Append("umName=").Append(entity.UnitName);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveUpdateWmUm);
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
                UnitEntity editEntity = PrepareSave();
                //int ret = unitDal.Save(editEntity, isNew);
                //if (ret == -1)
                //    MsgBox.Warn("计量单位编号或名称已存在，请改为其他的计量单位编号或名称。");
                //else if (ret == -2)
                //    MsgBox.Warn("更新失败，该行已经被其他人删除。");
                //else
                bool ret;
                if(isNew)
                    ret = SaveAddWmUm(editEntity, isNew);
                else
                    ret = SaveUpdateWmUm(editEntity, isNew);

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

        public UnitEntity PrepareSave()
        {
            UnitEntity editEntity = unitEntity;
            if (editEntity == null) editEntity = new UnitEntity();
            editEntity.UnitCode = txtCode.Text.Trim();
            editEntity.UnitName = txtName.Text.Trim();
            return editEntity;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                if (unitEntity == null)
                    Continue();
                else
                    this.DialogResult = DialogResult.OK;
            }
        }
    }
}