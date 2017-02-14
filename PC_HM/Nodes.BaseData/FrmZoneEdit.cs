using System;
using System.Collections.Generic;
using System.Windows.Forms;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.UI;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.BaseData;
using Nodes.Entities.HttpEntity.Instore;
using Newtonsoft.Json;

namespace Nodes.BaseData
{
    public partial class FrmZoneEdit : DevExpress.XtraEditors.XtraForm
    {
        private ZoneEntity zoneEntity = null;
        public event EventHandler DataSourceChanged = null;
        private bool isNew = true;
        //private ZoneDal zoneDal = null;
        //private WarehouseDal wareHouseDal = null;
        //private TemperatureDal temperatureDal = null;

        public FrmZoneEdit()
        {
            InitializeComponent();
        }

        public FrmZoneEdit(ZoneEntity zoneEntity)
            : this()
        {
            this.zoneEntity = zoneEntity;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            //zoneDal = new ZoneDal();
            //temperatureDal = new TemperatureDal();
            //wareHouseDal = new WarehouseDal();

            BindingCombox();
            listZoneType.EditValue = "CCQ"; //默认为存储区
            listTemprature.EditValue = "00"; //默认为无限制
            if (zoneEntity != null)
            {
                this.Text = "货区-修改";
                txtCode.Enabled = false;
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ShowEditInfo(zoneEntity);
                isNew = false;
            }
        }

        #region 自定义方法

        ///<summary>
        ///查询所有仓库
        ///</summary>
        ///<returns></returns>
        public List<WarehouseEntity> GetAllWarehouse()
        {
            List<WarehouseEntity> list = new List<WarehouseEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("vhNo=").Append(vehicleNO);
                string jsons = string.Empty;
                string jsonQuery = WebWork.SendRequest(jsons, WebWork.URL_GetAllWarehouse);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetAllWarehouse bill = JsonConvert.DeserializeObject<JsonGetAllWarehouse>(jsonQuery);
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
                foreach (JsonGetAllWarehouseResult jbr in bill.result)
                {
                    WarehouseEntity asnEntity = new WarehouseEntity();
                    asnEntity.OrgCode = jbr.orgCode;
                    asnEntity.OrgName = jbr.orgName;
                    asnEntity.WarehouseCode = jbr.whCode;
                    asnEntity.WarehouseName = jbr.whName;
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
        ///基础管理（货区信息-查询所有温控信息）
        ///</summary>
        ///<returns></returns>
        public List<TemperatureEntity> GetAllTemperature()
        {
            List<TemperatureEntity> list = new List<TemperatureEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("groupCode=").Append(groupCode);
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_GetAllTemperature);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetAllTemperature bill = JsonConvert.DeserializeObject<JsonGetAllTemperature>(jsonQuery);
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
                foreach (JsonGetAllTemperatureResult jbr in bill.result)
                {
                    TemperatureEntity asnEntity = new TemperatureEntity();
                    asnEntity.AllowEdit = jbr.allowEdit;
                    asnEntity.LowerLimit = Convert.ToInt32(jbr.lowerLimit);
                    asnEntity.TemperatureCode = jbr.tempCode;
                    asnEntity.TemperatureName = jbr.tempName;
                    asnEntity.UpperLimit = Convert.ToInt32(jbr.upperLimit);
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

        private void BindingCombox()
        {
            listWarehouse.Properties.DataSource = GetAllWarehouse();
            listZoneType.Properties.DataSource = GetItemList(BaseCodeConstant.ZONE_TYPE);
            listTemprature.Properties.DataSource = GetAllTemperature();
        }

        private void ShowEditInfo(ZoneEntity zoneEntity)
        {
            txtCode.Text = zoneEntity.ZoneCode;
            txtName.Text = zoneEntity.ZoneName;
            listWarehouse.EditValue = zoneEntity.WarehouseCode;
            listZoneType.EditValue = zoneEntity.ZoneTypeCode;
            listTemprature.EditValue = zoneEntity.TemperatureCode;
            comboIsActive.Text = zoneEntity.IsActive;
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
                MsgBox.Warn("货区编码不能为空。");
                return false;
            }

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MsgBox.Warn("货区名称不能为空。");
                return false;
            }
            if (listWarehouse.EditValue == null)
            {
                MsgBox.Warn("请选择所属仓库。");
                return false;
            }
            if (listTemprature.EditValue == null)
            {
                MsgBox.Warn("请选择温控属性。");
                return false;
            }

            if (listZoneType.EditValue == null)
            {
                MsgBox.Warn("请选择功能区分类。");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 基础管理（货区信息-添加货区）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isNew"></param>
        /// <returns></returns>
        public bool SaveAddZone(ZoneEntity entity, bool isNew)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("znCode=").Append(entity.ZoneCode).Append("&");
                loStr.Append("znName=").Append(entity.ZoneName).Append("&");
                loStr.Append("ztCode=").Append(entity.ZoneTypeCode).Append("&");
                loStr.Append("whCode=").Append(entity.WarehouseCode).Append("&");
                loStr.Append("tempCode=").Append(entity.TemperatureCode).Append("&");
                loStr.Append("isActive=").Append(entity.IsActive);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveAddZone);
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
        /// 基础管理（货区信息-更改货区信息）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isNew"></param>
        /// <returns></returns>
        public bool SaveUpdateZone(ZoneEntity entity, bool isNew)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("znCode=").Append(entity.ZoneCode).Append("&");
                loStr.Append("znName=").Append(entity.ZoneName).Append("&");
                loStr.Append("ztCode=").Append(entity.ZoneTypeCode).Append("&");
                loStr.Append("whCode=").Append(entity.WarehouseCode).Append("&");
                loStr.Append("tempCode=").Append(entity.TemperatureCode).Append("&");
                loStr.Append("isActive=").Append(entity.IsActive);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveUpdateZone);
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
                ZoneEntity editEntity = PrepareSave();
                //int ret = zoneDal.Save(editEntity, isNew);
                //if (ret == -1)
                //    MsgBox.Warn("货区编号已存在，请改为其他的编号。");
                //else if (ret == -2)
                //    MsgBox.Warn("更新失败，该行已经被其他人删除。");
                //else
                bool ret;
                if (isNew)
                    ret = SaveAddZone(editEntity, isNew);
                else
                    ret = SaveUpdateZone(editEntity, isNew);

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
        #endregion

        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            if (Save())
                this.DialogResult = DialogResult.OK;
        }

        public ZoneEntity PrepareSave()
        {
            ZoneEntity editEntity = zoneEntity;
            if (editEntity == null) editEntity = new ZoneEntity();

            editEntity.ZoneCode = txtCode.Text.Trim();
            editEntity.ZoneName = txtName.Text.Trim();
            editEntity.WarehouseCode = ConvertUtil.ToString(listWarehouse.EditValue);
            editEntity.WarehouseName = ConvertUtil.ToString(listWarehouse.Text);
            editEntity.ZoneTypeCode = ConvertUtil.ToString(listZoneType.EditValue);
            editEntity.ZoneTypeName = ConvertUtil.ToString(listZoneType.Text);
            editEntity.TemperatureCode = ConvertUtil.ToString(listTemprature.EditValue);
            editEntity.TemperatureName = ConvertUtil.ToString(listTemprature.Text);
            editEntity.IsActive = comboIsActive.Text;
            return editEntity;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                if (zoneEntity == null)
                    Continue();
                else
                    this.DialogResult = DialogResult.OK;
            }
        }

        private void lookUpEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
            {
                listWarehouse.EditValue = null;
                listWarehouse.ClosePopup();
            }
        }
    }
}