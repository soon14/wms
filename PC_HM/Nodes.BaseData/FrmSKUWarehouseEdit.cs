using System;
using System.Collections.Generic;
using System.Windows.Forms;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.UI;
using DevExpress.XtraEditors;
using Nodes.Shares;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.BaseData;
using Nodes.Entities.HttpEntity.Stock;
using Newtonsoft.Json;

namespace Nodes.BaseData
{
    public partial class FrmSKUWarehouseEdit : DevExpress.XtraEditors.XtraForm
    {
        private SkuWarehouseEntity Entity = null;
        public event EventHandler DataSourceChanged = null;
        //private SkuWarehouseDal skuWarehouseDal = null;
        //private MaterialDal materialDal = new MaterialDal();
        private List<MaterialEntity> ListMaterial = new List<MaterialEntity>();


        public FrmSKUWarehouseEdit()
        {
            InitializeComponent();
        }

        public FrmSKUWarehouseEdit(SkuWarehouseEntity entity)
            : this()
        {
            this.Entity = entity;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            //skuWarehouseDal = new SkuWarehouseDal();
            BindingCombox();
            if (this.Entity != null)
            {
                this.Text = "修改货位容量";
                txtCode.Enabled = false;
                listSku.Enabled = false;

                //layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ShowEditInfo(this.Entity);
            }
        }

        /// <summary>
        /// string转换decimal 得到2位小数点 
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private decimal GetTwoDecimal(string num)
        {
            string ret = num;
            if (ret.Contains("."))
                ret = ret.Insert(ret.Length, "00");
            else
                ret = ret.Insert(ret.Length, ".00");
            return Math.Round(Convert.ToDecimal(ret), 2);
        }

        ///<summary>
        ///查询所有计量单位
        ///</summary>
        ///<returns></returns>
        public List<UnitEntity> GetAllUnit()
        {
            List<UnitEntity> list = new List<UnitEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billState=").Append(BillStateConst.ASN_STATE_CODE_COMPLETE).Append("&");
                //loStr.Append("wareHouseCode=").Append(warehouseCode);
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_GetAllUnit);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetAllUnit bill = JsonConvert.DeserializeObject<JsonGetAllUnit>(jsonQuery);
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
                foreach (JsonGetAllUnitResult jbr in bill.result)
                {
                    UnitEntity asnEntity = new UnitEntity();
                    asnEntity.UnitCode = jbr.umCode;
                    asnEntity.UnitName = jbr.umName;
                    try
                    {
                        //if (!string.IsNullOrEmpty(jbr.closeDate))
                        //    asnEntity.CloseDate = Convert.ToDateTime(jbr.closeDate);
                        //if (!string.IsNullOrEmpty(jbr.printedTime))
                        //    asnEntity.PrintedTime = Convert.ToDateTime(jbr.printedTime);
                        //if (!string.IsNullOrEmpty(jbr.createDate))
                        //    asnEntity.CreateDate = Convert.ToDateTime(jbr.createDate);
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
        /// 查询所有物料，用于物料维护，如果是填充其他界面，请调用GetActiveMaterials()函数
        /// </summary>
        /// <returns></returns>
        public List<MaterialEntity> GetAll()
        {
            List<MaterialEntity> list = new List<MaterialEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("beginRow=").Append("&");
                loStr.Append("rows=");
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetAll,20000);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetAllSku bill = JsonConvert.DeserializeObject<JsonGetAllSku>(jsonQuery);
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
                foreach (JsonGetAllSkuResult jbr in bill.result)
                {
                    MaterialEntity asnEntity = new MaterialEntity();
                    #region 0-10
                    asnEntity.ExpDays = Convert.ToInt32(jbr.expDays);
                    asnEntity.BrandName = jbr.brdName;
                    asnEntity.MaterialCode = jbr.skuCode;
                    asnEntity.MaterialName = jbr.skuName;
                    asnEntity.MaxStockQty = Convert.ToInt32(jbr.maxStockQty);
                    asnEntity.MinStockQty = Convert.ToInt32(jbr.minStockQty);
                    asnEntity.SecurityQty = Convert.ToInt32(jbr.securityQty);
                    asnEntity.SkuType = jbr.skuType;
                    asnEntity.SkuTypeDesc = jbr.itemDesc;
                    asnEntity.Spec = jbr.spec;
                    #endregion

                    #region 11-20
                    asnEntity.TemperatureName = jbr.tempName;
                    asnEntity.TemperatureCode = jbr.tempCode;
                    asnEntity.TotalStockQty = StringToDecimal.GetTwoDecimal(jbr.totalStockQty);
                    asnEntity.MaterialTypeName = jbr.typName;
                    //asnEntity.UnitGrpCode
                    #endregion
                    try
                    {
                        //if (!string.IsNullOrEmpty(jbr.closeDate))
                        //    asnEntity.CloseDate = Convert.ToDateTime(jbr.closeDate);
                        //if (!string.IsNullOrEmpty(jbr.printedTime))
                        //    asnEntity.PrintedTime = Convert.ToDateTime(jbr.printedTime);
                        //if (!string.IsNullOrEmpty(jbr.createDate))
                        //    asnEntity.CreateDate = Convert.ToDateTime(jbr.createDate);
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

        private void BindingCombox()
        {
            #region 只有修改暂时不用
            /*List<UnitEntity> units = GetAllUnit();
            listUnit1.Properties.DataSource = units;
            */
            List<MaterialEntity> list = new List<MaterialEntity>();
            MaterialEntity tm = new MaterialEntity();
            tm.MaterialName = this.Entity.SkuName;
            tm.MaterialCode = this.Entity.SkuCode;
            list.Add(tm);
            //this.ListMaterial = GetAll();
            this.ListMaterial = list;
            listSku.Properties.DataSource = this.ListMaterial;
            
            #endregion
        }

        private void ShowEditInfo(SkuWarehouseEntity entity)
        {
            txtCode.Text = entity.SkuWarehouseID.ToString();
            listSku.EditValue = entity.SkuCode;
            txtSpec.Text = entity.Spec;
            txtMinStockQty.Text = entity.MinStockQty.ToString();
            txtMaxStockQty.Text = entity.MaxStockQty.ToString();
            txtLocSafe.Text = entity.SecurityQty.ToString();
        }

        private bool IsFieldValueValid()
        {

            if (string.IsNullOrEmpty(txtSpec.Text))
            {
                MsgBox.Warn("规格不能为空。");
                return false;
            }
            return true;
        }

        private void Continue()
        {
            listSku.EditValue = null;
            listUnit1.EditValue = null;
            txtSpec.Text = txtMinStockQty.Text = txtMaxStockQty.Text = txtLocSafe.Text =  String.Empty;
            #region 
            //if (checkAutoIncrement.Checked)
            //{
            //    //txtCode.Text = AutoIncrement.NextCode(txtCode.Text.Trim());
            //    //txtBarcode.Focus();
            //}
            //else
            //{
                //txtCode.Text = "";
                //txtCode.Focus();
            //}
            #endregion


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                if (this.Entity == null)
                    Continue();
                else
                    this.DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// 基础管理（本库物料-更新本库物料）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isCreateNew"></param>
        /// <returns></returns>
        public bool SaveUpdateSkuWarehouse(SkuWarehouseEntity entity, bool isCreateNew)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                #region
                loStr.Append("minStockQty=").Append(entity.MinStockQty).Append("&");
                loStr.Append("maxStockQty=").Append(entity.MaxStockQty).Append("&");
                loStr.Append("securityQty=").Append(entity.SecurityQty).Append("&");
                loStr.Append("id=").Append(entity.SkuWarehouseID);
                #endregion
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveUpdateSkuWarehouse);
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

            try
            {
                SkuWarehouseEntity entity = PrepareSave();
                bool isCreateNew = (this.Entity == null);
                //int result = skuWarehouseDal.Save(entity, isCreateNew);
                //if (result == -1)
                //{
                //    MsgBox.Warn("编号已经存在，请改为其他的编号。");
                //    return false;
                //}
                if (SaveUpdateSkuWarehouse(entity,isCreateNew))
                {
                    if (DataSourceChanged != null)
                        DataSourceChanged(null, null);

                    return true;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
                return false;
            }
            return false;
        }

        public SkuWarehouseEntity PrepareSave()
        {
            SkuWarehouseEntity editEntity = this.Entity;
            if (editEntity == null)
            {
                editEntity = new SkuWarehouseEntity();
            }

            editEntity.Spec = txtSpec.Text.Trim();
            editEntity.MinStockQty = ConvertUtil.ToInt(txtMinStockQty.Text.Trim());
            editEntity.MaxStockQty = ConvertUtil.ToInt(txtMaxStockQty.Text.Trim());
            editEntity.SecurityQty = ConvertUtil.ToInt(txtLocSafe.Text.Trim());

            return editEntity;
        }

        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            if (Save())
                this.DialogResult = DialogResult.OK;
        }
    }
}