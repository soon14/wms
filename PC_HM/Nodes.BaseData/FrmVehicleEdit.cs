using System;
using System.Collections.Generic;
using System.Windows.Forms;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.UI;
using Nodes.Shares;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;
using Nodes.Entities.HttpEntity.Instore;
using Nodes.Entities.HttpEntity.BaseData;
using Newtonsoft.Json;

namespace Nodes.BaseData
{
    public partial class FrmVehicleEdit : DevExpress.XtraEditors.XtraForm
    {
        private VehicleEntity vehicleEntity = null;
        public event EventHandler DataSourceChanged = null;
        private bool isNew = true;
        //private VehicleDal vehicleDal = null;

        public FrmVehicleEdit()
        {
            InitializeComponent();
        }

        public FrmVehicleEdit(VehicleEntity vehicleEntity)
            : this()
        {
            this.vehicleEntity = vehicleEntity;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            //vehicleDal = new VehicleDal();
            BindList();

            if (vehicleEntity != null)
            {
                this.Text = "车辆信息-修改";
                txtCode.Enabled = false;
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ShowEditInfo(vehicleEntity);
                isNew = false;
            }
        }

        /// <summary>
        /// 列出某个组织下面的某个角色的成员，例如保税库的发货员，状态必须是启用的
        /// </summary>
        /// <param name="warehouseCode"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public List<UserEntity> ListUsersByRoleAndWarehouseCode(string warehouseCode, string roleName)
        {
            List<UserEntity> list = new List<UserEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("warehouseCode=").Append(warehouseCode).Append("&");
                loStr.Append("roleName=").Append(roleName);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_ListUsersByRoleAndWarehouseCode);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonListUsersByRoleAndWarehouseCode bill = JsonConvert.DeserializeObject<JsonListUsersByRoleAndWarehouseCode>(jsonQuery);
                if (bill == null)
                {
                    //MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return list;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return list;
                }
                #endregion

                #region 赋值数据
                foreach (JsonListUsersByRoleAndWarehouseCodeResult jbr in bill.result)
                {
                    UserEntity asnEntity = new UserEntity();
                    asnEntity.AllowEdit = jbr.allowEdit;
                    asnEntity.IsActive = jbr.isActive;
                    asnEntity.IsOnline = jbr.isOnline;
                    asnEntity.MobilePhone = jbr.mobilePhone;
                    asnEntity.Remark = jbr.remark;
                    asnEntity.ROLE_ID = Convert.ToInt32(jbr.roleId);
                    asnEntity.UserCode = jbr.userCode;
                    asnEntity.UserName = jbr.userName;
                    asnEntity.WarehouseCode = jbr.whCode;
                    asnEntity.WarehouseName = jbr.whName;
                    asnEntity.UserPwd = jbr.pwd;
                    try
                    {
                        //if (!string.IsNullOrEmpty(jbr.updateDate))
                        //    asnEntity.UpdateDate = Convert.ToDateTime(jbr.updateDate);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    }
                    list.Add(asnEntity);
                }
                return list;
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return list;
        }

        /// <summary>
        /// 收货单据管理， baseCode信息查询(用于业务类型和单据状态筛选条件)
        /// 获取活动状态的集合
        /// </summary>
        /// <param name="groupCode"></param>
        /// <returns></returns>
        public  List<BaseCodeEntity> GetItemList(string groupCode)
        {
            List<BaseCodeEntity> list = new List<BaseCodeEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("groupCode=").Append(groupCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetItemList);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonBaseCodeInfo bill = JsonConvert.DeserializeObject<JsonBaseCodeInfo>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return list;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return list;
                }
                #endregion

                #region 赋值数据
                foreach (JsonBaseCodeInfoResult jbr in bill.result)
                {
                    BaseCodeEntity asnEntity = new BaseCodeEntity();
                    asnEntity.GroupCode = jbr.groupCode;
                    asnEntity.IsActive = jbr.isActive;
                    asnEntity.ItemDesc = jbr.itemDesc;
                    asnEntity.ItemValue = jbr.itemValue;
                    asnEntity.Remark = jbr.remark;
                    list.Add(asnEntity);
                }
                return list;
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return list;
        }

        ///<summary>
        ///查询所有路线
        ///</summary>
        ///<returns></returns>
        public List<RouteEntity> GetAll()
        {
            List<RouteEntity> list = new List<RouteEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("groupCode=").Append(groupCode);
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_GetAllRoute);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetAllRoute bill = JsonConvert.DeserializeObject<JsonGetAllRoute>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return list;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return list;
                }
                #endregion

                #region 赋值数据
                foreach (JsonGetAllRouteResult jbr in bill.result)
                {
                    RouteEntity asnEntity = new RouteEntity();
                    asnEntity.RouteCode = jbr.rtCode;
                    asnEntity.RouteName = jbr.rtName;
                    list.Add(asnEntity);
                }
                return list;
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return list;
        }

        #region 自定义方法
        private void BindList()
        {
            listDriver.Properties.DataSource = ListUsersByRoleAndWarehouseCode(GlobeSettings.LoginedUser.WarehouseCode, GlobeSettings.DriverRoleName);
            listRoute.Properties.DataSource = GetAll();
            listVHType.Properties.DataSource = GetItemList("119");
            this.cboVehicleType.Properties.DataSource = GetItemList("122");
        }

        private void ShowEditInfo(VehicleEntity vehicleEntity)
        {
            txtCode.Text = vehicleEntity.VehicleCode;
            txtVehicleNO.Text = vehicleEntity.VehicleNO;
            listDriver.EditValue = vehicleEntity.UserCode;
            txtVolume.Text = vehicleEntity.VehicleVolume.ToString();
            listRoute.EditValue = vehicleEntity.RouteCode;
            listIsActive.Text = vehicleEntity.IsActive;
            listVHType.EditValue = vehicleEntity.VhType;
            this.cboVehicleType.EditValue = vehicleEntity.VhAttri;
        }

        private void Continue()
        {
            txtVehicleNO.Text = "";

            if (checkAutoIncrement.Checked)
            {
                txtCode.Text = AutoIncrement.NextCode(txtCode.Text.Trim());
                txtVehicleNO.Focus();
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
                MsgBox.Warn("请填写编码。");
                return false;
            }

            if (string.IsNullOrEmpty(txtVehicleNO.Text))
            {
                MsgBox.Warn("请填写车牌号。");
                return false;
            }

            if (string.IsNullOrEmpty(txtVolume.Text))
            {
                MsgBox.Warn("请填写车辆容积。");
                return false;
            }

            if (listRoute.EditValue == null)
            {
                MsgBox.Warn("请选择最主要的送货路线。");
                return false;
            }
            if (listVHType.EditValue == null)
            {
                MsgBox.Warn("请选择车辆所属！");
                return false;
            }
            if (this.cboVehicleType.EditValue == null)
            {
                MsgBox.Warn("请选择车辆属性！");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isNew">添加或编辑</param>
        /// <returns></returns>
        public bool InsertSave(VehicleEntity entity, bool isNew)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("vhCode=").Append(entity.VehicleCode).Append("&");
                loStr.Append("vhNo=").Append(entity.VehicleNO).Append("&");
                loStr.Append("vhVolume=").Append(entity.VehicleVolume).Append("&");
                loStr.Append("userCode=").Append(entity.UserCode).Append("&");
                loStr.Append("rtCode=").Append(entity.RouteCode).Append("&");
                loStr.Append("vhType=").Append(entity.VhType).Append("&");
                loStr.Append("vhAttri=").Append(entity.VhAttri).Append("&");
                if(isNew)
                    loStr.Append("isActive=").Append("Y");
                else
                    loStr.Append("isActive=").Append(entity.IsActive);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_InsertSave);
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
        /// 基础管理（车辆信息-更改）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isNew"></param>
        /// <returns></returns>
        public bool UpdateSave(VehicleEntity entity, bool isNew)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("vhCode=").Append(entity.VehicleCode).Append("&");
                loStr.Append("vhNo=").Append(entity.VehicleNO).Append("&");
                loStr.Append("vhVolume=").Append(entity.VehicleVolume).Append("&");
                loStr.Append("userCode=").Append(entity.UserCode).Append("&");
                loStr.Append("rtCode=").Append(entity.RouteCode).Append("&");
                loStr.Append("vhType=").Append(entity.VhType).Append("&");
                loStr.Append("vhAttri=").Append(entity.VhAttri).Append("&");
                if (isNew)
                    loStr.Append("isActive=").Append("Y");
                else
                    loStr.Append("isActive=").Append(entity.IsActive);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_UpdateSave);
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
                VehicleEntity editEntity = PrepareSave();
                bool ret;
                if (isNew)
                {
                   ret = InsertSave(editEntity, isNew);
                }else{
                   ret = UpdateSave(editEntity, isNew);
                }
                //int ret = vehicleDal.Save(editEntity, isNew);
                //if (ret == -1)
                //    MsgBox.Warn("编号已存在，请改为其他的编号。");
                //else if (ret == -2)
                //    MsgBox.Warn("更新失败，该行已经被其他人删除。");
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

        public VehicleEntity PrepareSave()
        {
            VehicleEntity editEntity = vehicleEntity;
            if (editEntity == null) editEntity = new VehicleEntity();

            editEntity.VehicleCode = txtCode.Text.Trim();
            editEntity.VehicleNO = txtVehicleNO.Text.Trim();
            editEntity.UserCode = ConvertUtil.ToString(listDriver.EditValue);
            editEntity.UserName = listDriver.Text;
            editEntity.RouteCode = ConvertUtil.ToString(listRoute.EditValue);
            editEntity.RouteName = listRoute.Text;
            editEntity.VehicleVolume = ConvertUtil.ToDecimal(txtVolume.Text.Trim());
            editEntity.IsActive = listIsActive.Text;
            editEntity.VhType = ConvertUtil.ToString(listVHType.EditValue);
            editEntity.VhTypeStr = listVHType.Text;
            editEntity.VhAttri = ConvertUtil.ToString(cboVehicleType.EditValue);
            editEntity.VhAttriStr = cboVehicleType.Text;
            return editEntity;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                if (vehicleEntity == null)
                    Continue();
                else
                    this.DialogResult = DialogResult.OK;
            }
        }
    }
}