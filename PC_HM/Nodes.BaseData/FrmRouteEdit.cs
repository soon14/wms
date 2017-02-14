using System;
using System.Windows.Forms;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.UI;
using Nodes.Entities.HttpEntity;
using Newtonsoft.Json;

namespace Nodes.BaseData
{
    public partial class FrmRouteEdit : DevExpress.XtraEditors.XtraForm
    {
        private RouteEntity routeEntity = null;
        public event EventHandler DataSourceChanged = null;
        private bool isNew = true;
        private RouteDal routeDal = null;

        public FrmRouteEdit()
        {
            InitializeComponent();
        }

        public FrmRouteEdit(RouteEntity routeEntity)
            : this()
        {
            this.routeEntity = routeEntity;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            routeDal = new RouteDal();

            if (routeEntity != null)
            {
                this.Text = "送货路线-修改";
                txtCode.Enabled = false;
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ShowEditInfo(routeEntity);
                isNew = false;
            }
        }

        #region 自定义方法

        private void ShowEditInfo(RouteEntity unitEntity)
        {
            txtCode.Text = unitEntity.RouteCode;
            txtName.Text = unitEntity.RouteName;
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
        /// 基础管理（送货路线-添加）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isNew"></param>
        /// <returns></returns>
        public bool InsertSaveRoute(RouteEntity entity, bool isNew)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("rtCode=").Append(entity.RouteCode).Append("&");
                loStr.Append("rtName=").Append(entity.RouteName);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_InsertSaveRoute);
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
        /// 基础管理（送货路线-更改）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isNew"></param>
        /// <returns></returns>
        public bool UpdateSaveRoute(RouteEntity entity, bool isNew)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("rtCode=").Append(entity.RouteCode).Append("&");
                loStr.Append("rtName=").Append(entity.RouteName);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_UpdateSaveRoute);
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
                RouteEntity editEntity = PrepareSave();
                //int ret = routeDal.Save(editEntity, isNew);
                //if (ret == -1)
                //    MsgBox.Warn("编号或名称已存在，请改为其他的编号或名称。");
                //else if (ret == -2)
                //    MsgBox.Warn("更新失败，该行已经被其他人删除。");
                //else
                bool ret;
                if (isNew)
                {
                    ret = InsertSaveRoute(editEntity, isNew);
                }
                else
                {
                    ret = UpdateSaveRoute(editEntity, isNew);
                }

                if (ret)
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

        public RouteEntity PrepareSave()
        {
            RouteEntity editEntity = routeEntity;
            if (editEntity == null) editEntity = new RouteEntity();
            editEntity.RouteCode = txtCode.Text.Trim();
            editEntity.RouteName = txtName.Text.Trim();
            return editEntity;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                if (routeEntity == null)
                    Continue();
                else
                    this.DialogResult = DialogResult.OK;
            }
        }
    }
}