using System;
using System.Collections.Generic;
using System.Windows.Forms;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.UI;
using DevExpress.XtraEditors;
using Nodes.Shares;
using Nodes.DBHelper.BaseData;
using Nodes.Entities.BaseData;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.BaseData;
using Nodes.Entities.HttpEntity.Stock;
using Newtonsoft.Json;

namespace Nodes.BaseData
{
    public partial class FrmRecLocationEdit : DevExpress.XtraEditors.XtraForm
    {
        private RecLocationEntity recLocationEntity = null;
        public event EventHandler DataSourceChanged = null;
        private RecLocationDal recLocationDal = null;
        //private MaterialDal materialDal = new MaterialDal();        
        private List<MaterialEntity> ListMaterial = new List<MaterialEntity>();

        public FrmRecLocationEdit()
        {
            InitializeComponent();
        }

        public FrmRecLocationEdit(RecLocationEntity entity)
            : this()
        {
            this.recLocationEntity = entity;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            recLocationDal = new RecLocationDal();
            BindingCombox();
            if (recLocationEntity != null)
            {
                this.Text = "设置推荐货位商品。";
                txtCode.Enabled = false;
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ShowEditInfo(recLocationEntity);
            }
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
                    asnEntity.TotalStockQty = Convert.ToDecimal(jbr.totalStockQty);
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
            this.ListMaterial = GetAll();
            listSku.Properties.DataSource = this.ListMaterial;
        }

        private void ShowEditInfo(RecLocationEntity entity)
        {
            //txtCode.Text = grpEntity.GrpCode;
            listSku.EditValue = entity.SkuCode;
            txtlocCode.Text = entity.Location;
        }

        private bool IsFieldValueValid()
        {
            //if (string.IsNullOrEmpty(txtCode.Text))
            //{
            //    MsgBox.Warn("编号不能为空。");
            //    return false;
            //}

            if (string.IsNullOrEmpty(ConvertUtil.ToString(listSku.EditValue)))
            {
                MsgBox.Warn("商品名称不能为空。");
                return false;
            }


            return true;
        }

        private void Continue()
        {
            listSku.EditValue = null;
            txtlocCode.Text = String.Empty;
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
                if (recLocationEntity == null)
                    Continue();
                else
                    this.DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// 基础管理（推荐货位-添加或编辑库存货位）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="IsCreateNew"></param>
        /// <returns></returns>
        public bool SaveUpdate(RecLocationEntity entity, bool IsCreateNew)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("lcCode=").Append(entity.Location).Append("&");
                loStr.Append("skuCode=").Append(entity.SkuCode).Append("&");
                loStr.Append("recLoc=").Append(entity.RecLoc);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_Save);
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
                RecLocationEntity entity=PrepareSave();
                bool isCreateNew = (this.recLocationEntity == null);
                //int result = recLocationDal.Save(entity, isCreateNew);
                //if (result == -1)
                //{
                //    MsgBox.Warn("编号已经存在，请改为其他的编号。");
                //    return false;
                //}
                if (SaveUpdate(entity, isCreateNew))
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

        public RecLocationEntity PrepareSave()
        {
            RecLocationEntity editEntity = this.recLocationEntity;
            if (editEntity == null)
            {
                editEntity = new RecLocationEntity();
            }

            MaterialEntity entity = listSku.Properties.View.GetFocusedRow() as MaterialEntity;
            if (entity == null)
                return null;
            editEntity.SkuCode = entity.MaterialCode;
            editEntity.SkuName = entity.MaterialName;
            editEntity.Spec = entity.Spec;

            return editEntity;
        }

        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            if (Save())
                this.DialogResult = DialogResult.OK;
        }
    }
}