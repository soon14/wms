using System;
using System.Collections.Generic;
using System.Windows.Forms;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.Shares;
using Nodes.UI;
using DevExpress.XtraEditors;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.BaseData;
using Nodes.Entities.HttpEntity.Instore;
using Newtonsoft.Json;

namespace Nodes.BaseData
{
    public partial class FrmMaterialEdit : DevExpress.XtraEditors.XtraForm
    {
        private MaterialEntity materialEntity = null;
        public event EventHandler DataSourceChanged = null;
        private bool isNew = true;
        //private MaterialDal materialDal = null;

        public FrmMaterialEdit()
        {
            InitializeComponent();
        }

        public FrmMaterialEdit(MaterialEntity materialEntity, bool isNew)
            : this()
        {
            this.materialEntity = materialEntity;
            this.isNew = isNew;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            //materialDal = new MaterialDal();
            BindingLookupEdit();

            if (materialEntity != null)
            {
                ShowEditInfo(materialEntity);
                //layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            if (!isNew)
            {
                this.Text = "物料-修改";
                txtCode.Enabled = false;
                if (materialEntity.IsOwn == "N")
                {
                    //txtName.Enabled = txtNamePY.Enabled = txtSpec.Enabled = txtStr3.Enabled = false;
                    //非自有物料不允许修改名称等信息，只能修改WMS自有字段
                }
            }
            else
            {
                txtCode.Text = "";
            }
        }
        //private void CustomGridCaption()
        //{
        //    List<CustomFieldEntity> customFields = CustomFields.MaterialCustomFields;
        //    foreach (CustomFieldEntity field in customFields)
        //    {
        //        switch (field.FieldName)
        //        {
        //            case "MTL_STR1":
        //                if (field.IsActive == "N")
        //                    itemStr1.Control.Enabled = false;
        //                itemStr1.Text = field.FieldDesc;
        //                break;
        //            case "MTL_STR2":
        //                if (field.IsActive == "N")
        //                    itemStr2.Control.Enabled = false;
        //                itemStr2.Text = field.FieldDesc;
        //                break;
        //            case "MTL_STR3":
        //                if (field.IsActive == "N")
        //                    itemStr3.Control.Enabled = false;
        //                itemStr3.Text = field.FieldDesc;
        //                break;
        //            case "MTL_STR4":
        //                if (field.IsActive == "N")
        //                    itemStr4.Control.Enabled = false;
        //                itemStr4.Text = field.FieldDesc;
        //                break;
        //            case "MTL_NUM1":
        //                if (field.IsActive == "N")
        //                    itemNum1.Control.Enabled = false;
        //                itemNum1.Text = field.FieldDesc;
        //                break;
        //            case "MTL_NUM2":
        //                if (field.IsActive == "N")
        //                    itemNum2.Control.Enabled = false;
        //                itemNum2.Text = field.FieldDesc;
        //                break;
        //            case "MTL_DATE1":
        //                if (field.IsActive == "N")
        //                    itemDate1.Control.Enabled = false;
        //                itemDate1.Text = field.FieldDesc;
        //                break;
        //            case "MTL_DATE2":
        //                if (field.IsActive == "N")
        //                    itemDate2.Control.Enabled = false;
        //                itemDate2.Text = field.FieldDesc;
        //                break;
        //        }
        //    }
        //}
        #region 自定义方法

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

        /// <summary>
        /// 物料信息编辑
        /// </summary>
        /// <param name="sku_code"></param>
        /// <returns></returns>
        public List<UnitGroupEntity> GetUmName(string sku_code)
        {
            List<UnitGroupEntity> list = new List<UnitGroupEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("skuCode=").Append(sku_code);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetUmName);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetUmName bill = JsonConvert.DeserializeObject<JsonGetUmName>(jsonQuery);
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
                foreach (JsonGetUmNameResult jbr in bill.result)
                {
                    UnitGroupEntity asnEntity = new UnitGroupEntity();
                    #region 0-10
                    asnEntity.Height = StringToDecimal.GetTwoDecimal(jbr.height);
                    asnEntity.ID = Convert.ToInt32(jbr.id);
                    asnEntity.IsActive = jbr.isActive;
                    asnEntity.Length = StringToDecimal.GetTwoDecimal(jbr.length);
                    //jbr.namePy;
                    asnEntity.Qty = Convert.ToInt32(jbr.qty);
                    asnEntity.SkuBarcode = jbr.skuBarCode;
                    asnEntity.SkuCode = jbr.skuCode;
                    asnEntity.SkuLevel = Convert.ToInt32(jbr.skuLevel);
                    asnEntity.SkuName = jbr.skuName;
                    asnEntity.Spec = jbr.spec;
                    #endregion
                    #region 11-15
                    asnEntity.UnitCode = jbr.umCode;
                    asnEntity.UnitName = jbr.umName;
                    asnEntity.Weight = StringToDecimal.GetTwoDecimal(jbr.weight);
                    asnEntity.Width = StringToDecimal.GetTwoDecimal(jbr.width);
                    #endregion

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

        private void BindingLookupEdit()
        {

            lookUpEditTemperature.Properties.DataSource = GetAllTemperature();
            lookUpEdit1.Properties.DataSource = GetItemList(BaseCodeConstant.SKU_TYPE);
            lookUpUmName.Properties.DataSource = GetUmName(materialEntity.MaterialCode);
        }

        /// <summary>
        /// 显示要编辑的物料信息
        /// </summary>        
        private void ShowEditInfo(MaterialEntity material)
        {
            txtCode.Text = material.MaterialCode;     //物料编码
            txtName.Text = material.MaterialName;     //物料名称
            txtSpec.Text = material.Spec;            //物料规格

            if (material.SecurityQty.HasValue)        //单货位安全库存
                spinEditSecurityQty.Value = material.SecurityQty.Value;

            lookUpEditTemperature.EditValue = material.TemperatureCode;//存储条件

            if (material.MinStockQty.HasValue)        //低储
                spinEditMin.Value = material.MinStockQty.Value;

            if (material.MaxStockQty.HasValue)        //高储
                spinEditMax.Value = material.MaxStockQty.Value;

            lookUpEdit1.Text = material.SkuTypeDesc;

        }

        private void Continue()
        {

            spinEditMax.Value = 0;
            spinEditMin.Value = 0;

        }

        private bool IsFieldValueValid()
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                MsgBox.Warn("物料编码不能为空。");
                return false;
            }

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MsgBox.Warn("物料名称不能为空。");
                return false;
            }

            if (spinEditSecurityQty.Value < 0 || spinEditMin.Value < 0 || spinEditMax.Value < 0)
            {
                MsgBox.Warn("要保存的项中不能有小于零的值。");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 添加或编辑物料
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isNew">添加或编辑</param>
        /// <returns>1:成功；-1：物料编号已存在；-2：已关联其它供应商，无法置为空；-3：该关联已存在</returns>
        public bool UpdateSkuInfo(MaterialEntity entity, bool isNew)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                #region
                loStr.Append("securityQty=").Append(entity.SecurityQty).Append("&");
                loStr.Append("tempCode=").Append(entity.TemperatureCode).Append("&");
                loStr.Append("minStockQty=").Append(entity.MinStockQty).Append("&");
                loStr.Append("maxStockQty=").Append(entity.MaxStockQty).Append("&");
                loStr.Append("updateTy=").Append(entity.LastUpdateBy).Append("&");
                loStr.Append("updateDate=").Append(entity.LastUpdateDate).Append("&");
                loStr.Append("skuType=").Append(entity.SkuType).Append("&");
                loStr.Append("skuCode=").Append(entity.MaterialCode);
                #endregion
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_UpdateSkuInfo);
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
                MaterialEntity editEntity = PrepareSave();
                //int ret = materialDal.Save(editEntity,lookUpUmName.EditValue.ToString(),Convert.ToInt32(spinEditSecurityQty.Text), isNew);
                //if (ret == -1)
                //    MsgBox.Warn("物料编号已存在，请改为其他的物料编号。");
                //else if (ret == -2)
                //    MsgBox.Warn("供应商不允许置为空，因为该物料已关联了其它供应商。");
                //else if (ret == -3)
                //    MsgBox.Warn(string.Format("该物料已经与供应商“{0}”存在关联，请重新选择默认供应商。", editEntity.SupplierName));
                //else
                //{
                //    success = true;
                //    if (DataSourceChanged != null)
                //    {
                //        DataSourceChanged(editEntity, null);
                //    }
                //}
                bool ret;
                if (!isNew)
                    ret = UpdateSkuInfo(editEntity, isNew);
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
        public MaterialEntity PrepareSave()
        {
            MaterialEntity editEntity = materialEntity;
            if (editEntity == null)
            {
                editEntity = new MaterialEntity();
                //新增的物料，标记为WMS自有
                editEntity.IsOwn = "Y";
            }

            //editEntity.MaterialCode = txtCode.Text.Trim();
            //editEntity.MaterialName = txtName.Text.Trim();
            //editEntity.SupplierCode = null;
            editEntity.SecurityQty = ConvertUtil.ToInt(spinEditSecurityQty.Value);
            editEntity.TemperatureCode = ConvertUtil.ToString(lookUpEditTemperature.EditValue);
            editEntity.MinStockQty = ConvertUtil.ToInt(spinEditMin.Value);
            editEntity.MaxStockQty = ConvertUtil.ToInt(spinEditMax.Value);

            editEntity.LastUpdateDate = DateTime.Now;
            editEntity.LastUpdateBy = GlobeSettings.LoginedUser.UserName;
            editEntity.SkuType = ConvertUtil.ToString(lookUpEdit1.EditValue);
            return editEntity;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                if (isNew)
                    Continue();
                else
                    this.DialogResult = DialogResult.OK;
            }
        }
        private void OnLookUpEditButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            LookUpEdit editor = sender as LookUpEdit;
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
            {
                editor.EditValue = null;
            }
        }

        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            if (Save())
                this.DialogResult = DialogResult.OK;
        }

    }
}