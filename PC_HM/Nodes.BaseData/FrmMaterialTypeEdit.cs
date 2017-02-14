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
    public partial class FrmMaterialTypeEdit : DevExpress.XtraEditors.XtraForm
    {
        private MaterialTypeEntity mtlTypeEntity = null;
        public event EventHandler DataSourceChanged = null;
        private bool isNew = true;
        //private MaterialTypeDal mtlTypeDal = null;

        public FrmMaterialTypeEdit()
        {
            InitializeComponent();
        }

        public FrmMaterialTypeEdit(MaterialTypeEntity mtlTypeEntity)
            : this()
        {
            this.mtlTypeEntity = mtlTypeEntity;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            //mtlTypeDal = new MaterialTypeDal();
            BindingCombox();

            if (mtlTypeEntity != null)
            {
                this.Text = "物料分类-修改";
                txtCode.Enabled = false;
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ShowEditInfo(mtlTypeEntity);
                isNew = false;
            }
        }

        #region 自定义方法

        ///<summary>
        ///查询所有货区
        ///</summary>
        ///<returns></returns>
        public List<ZoneEntity> GetAllZone()
        {
            List<ZoneEntity> list = new List<ZoneEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("vhNo=").Append(vehicleNO);
                string jsons = string.Empty;
                string jsonQuery = WebWork.SendRequest(jsons, WebWork.URL_GetAllZone);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetAllZone bill = JsonConvert.DeserializeObject<JsonGetAllZone>(jsonQuery);
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
                foreach (JsonGetAllZoneResult jbr in bill.result)
                {
                    ZoneEntity asnEntity = new ZoneEntity();
                    asnEntity.IsActive = jbr.isActive;
                    asnEntity.TemperatureCode = jbr.tempCode;
                    asnEntity.TemperatureName = jbr.tempName;
                    asnEntity.WarehouseCode = jbr.whCode;
                    asnEntity.WarehouseName = jbr.whName;
                    asnEntity.ZoneCode = jbr.znCode;
                    asnEntity.ZoneName = jbr.znName;
                    asnEntity.ZoneTypeCode = jbr.ztCode;
                    asnEntity.ZoneTypeName = jbr.ztName;
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
            listZones.Properties.DataSource = GetAllZone();
        }

        private void ShowEditInfo(MaterialTypeEntity mtlTypeEntity)
        {
            txtCode.Text = mtlTypeEntity.MaterialTypeCode;
            txtName.Text = mtlTypeEntity.MaterialTypeName;
            listZones.EditValue = mtlTypeEntity.ZoneCode;
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
                MsgBox.Warn("分类编码不能为空。");
                return false;
            }

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MsgBox.Warn("分类名称不能为空。");
                return false;
            }

            if (listZones.EditValue == null)
            {
                MsgBox.Warn("请填写存放货区。");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 添加或编辑分类
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isNew">添加或编辑</param>
        /// <returns></returns>
        public bool SaveAddSkuType(MaterialTypeEntity entity, bool isNew)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("typCode=").Append(entity.MaterialTypeCode).Append("&");
                loStr.Append("typName=").Append(entity.MaterialTypeName).Append("&");
                loStr.Append("znCode=").Append(entity.ZoneCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveAddSkuType);
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
        /// 添加或编辑分类
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isNew">添加或编辑</param>
        /// <returns></returns>
        public bool SaveUpdateSkuType(MaterialTypeEntity entity, bool isNew)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("typCode=").Append(entity.MaterialTypeCode).Append("&");
                loStr.Append("typName=").Append(entity.MaterialTypeName).Append("&");
                loStr.Append("znCode=").Append(entity.ZoneCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveUpdateSkuType);
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
                MaterialTypeEntity editEntity = PrepareSave();
                //int ret = mtlTypeDal.Save(editEntity, isNew);
                //if (ret == -1)
                //    MsgBox.Warn("分类编号已存在，请改为其他的编号。");
                //else if (ret == -2)
                //    MsgBox.Warn("更新失败，该行已经被其他人删除。");
                //else
                bool ret;
                if (isNew)
                    ret = SaveAddSkuType(editEntity, isNew);
                else
                    ret = SaveUpdateSkuType(editEntity, isNew);

                if(ret)
                {
                    success = true;
                    if (DataSourceChanged != null)
                        DataSourceChanged(editEntity, null);
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

        public MaterialTypeEntity PrepareSave()
        {
            MaterialTypeEntity editEntity = mtlTypeEntity;
            if (editEntity == null) editEntity = new MaterialTypeEntity();

            editEntity.MaterialTypeCode = txtCode.Text.Trim();
            editEntity.MaterialTypeName = txtName.Text.Trim();
            editEntity.ZoneCode = ConvertUtil.ToString(listZones.EditValue);
            editEntity.ZoneName = listZones.Text;

            return editEntity;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                if (mtlTypeEntity == null)
                    Continue();
                else
                    this.DialogResult = DialogResult.OK;
            }
        }
    }
}