using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.UI;
using DevExpress.XtraEditors;
using Nodes.Shares;
using Nodes.SystemManage;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.BaseData;
using Nodes.Entities.HttpEntity.Stock;
using Newtonsoft.Json;

namespace Nodes.BaseData
{
    public partial class FrmUnitGroupEditType_C : DevExpress.XtraEditors.XtraForm
    {
        private UnitGroupEntity unitGrpEntity = null;
        public event EventHandler DataSourceChanged = null;
        private UnitGroupDal ugDal = null;
        private MaterialDal materialDal = new MaterialDal();
        private List<MaterialEntity> ListMaterial = new List<MaterialEntity>();

        public FrmUnitGroupEditType_C()
        {
            InitializeComponent();
        }

        public FrmUnitGroupEditType_C(UnitGroupEntity unitGrpEntity)
            : this()
        {
            this.unitGrpEntity = unitGrpEntity;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            ugDal = new UnitGroupDal();
            BindingCombox();
            if (unitGrpEntity != null)
            {
                this.Text = "包装关系-修改";
                txtCode.Enabled = false;
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ShowEditInfo(unitGrpEntity);
            }
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
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetAll, 20000);
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
            List<UnitEntity> units = GetAllUnit();
            listUnit1.Properties.DataSource = units;

            this.ListMaterial = GetAll();
            listSku.Properties.DataSource = this.ListMaterial;
        }

        private void ShowEditInfo(UnitGroupEntity grpEntity)
        {
            //txtCode.Text = grpEntity.GrpCode;
            listSku.EditValue = grpEntity.SkuCode;
            txtBarcode.Text = grpEntity.SkuBarcode;
            listUnit1.EditValue = grpEntity.UnitCode;
            txtCount.Text = grpEntity.Qty.ToString();
            txtWeight.Text = grpEntity.Weight.ToString();
            txtChang.Text = grpEntity.Length.ToString();
            txtKuan.Text = grpEntity.Width.ToString();
            txtGao.Text = grpEntity.Height.ToString();
            //txtDesc.Text = grpEntity.GrpDesc;
            listUnit1.EditValue = grpEntity.UnitCode;
            comboIsActive.Text = grpEntity.IsActive;
            txtSkuLevel.Text = grpEntity.SkuLevel.ToString();
        }

        private bool IsFieldValueValid()
        {
            //if (string.IsNullOrEmpty(txtCode.Text))
            //{
            //    MsgBox.Warn("编号不能为空。");
            //    return false;
            //}

            if (string.IsNullOrEmpty(txtBarcode.Text))
            {
                MsgBox.Warn("条码不能为空。");
                return false;
            }


            if (listUnit1.EditValue == null)
            {
                MsgBox.Warn("最小计量单位必须填写。");
                return false;
            }

            if (String.IsNullOrEmpty(txtCount.Text.Trim()))
            {
                MsgBox.Warn("转换率不能为空。");
                return false;
            }


            return true;
        }

        private void Continue()
        {
            listSku.EditValue = null;
            listUnit1.EditValue = null;
            txtBarcode.Text = txtCount.Text = txtWeight.Text = txtChang.Text = txtKuan.Text = txtGao.Text = String.Empty;
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
                if (unitGrpEntity == null)
                    Continue();
                else
                    this.DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// 基础管理（包装关系-编辑包装关系）
        /// </summary>
        /// <param name="UmCode"></param>
        /// <param name="Qty"></param>
        /// <param name="SkuCode"></param>
        /// <param name="Barcode"></param>
        /// <param name="Weight"></param>
        /// <param name="Length"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="IsActive"></param>
        /// <param name="ID"></param>
        /// <param name="isCreateNew"></param>
        /// <returns></returns>
        public bool SaveUpdateUmSku(string UmCode, int Qty, string SkuCode, string Barcode,
           decimal Weight, decimal Length, decimal Width, decimal Height, string IsActive, int ID, bool isCreateNew)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                #region
                loStr.Append("umCode=").Append(UmCode).Append("&");
                loStr.Append("qty=").Append(Qty).Append("&");
                loStr.Append("skuCode=").Append(SkuCode).Append("&");
                loStr.Append("weight=").Append(Weight).Append("&");
                loStr.Append("length=").Append(Length).Append("&");
                loStr.Append("width=").Append(Width).Append("&");
                loStr.Append("height=").Append(Height).Append("&");
                int sUnit = 0; //销售单位与库存单位，转换倍数大于1时为销售单位，否则为库存单位
                if (Qty > 1)
                    sUnit = 1;
                loStr.Append("sUnit=").Append(sUnit).Append("&");
                loStr.Append("id=").Append(ID).Append("&");
                loStr.Append("isActive=").Append(IsActive);
                #endregion
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveUpdateUmSku);
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

        #region 插入日志记录
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="type">日志类型</param>
        /// <param name="creator">当前操作人</param>
        /// <param name="billNo">订单编号</param>
        /// <param name="description">操作描述</param>
        /// <param name="module">模块</param>
        /// <param name="createTime">创建时间</param>
        /// <param name="remark">备注信息</param>
        /// <returns></returns>
        public  bool Insert(ELogType type, string creator, string billNo, string description,
            string module, DateTime createTime, string remark)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("type=").Append(type).Append("&");
                loStr.Append("creator=").Append(creator).Append("&");
                loStr.Append("billNo=").Append(billNo).Append("&");
                loStr.Append("description=").Append(description).Append("&");
                loStr.Append("module=").Append(module).Append("&");
                loStr.Append("remark=").Append(remark);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_Insert);
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
        public  bool Insert(ELogType type, string creator, string billNo, string description,
            string module, string remark)
        {
            return Insert(type, creator, billNo, description, module, DateTime.Now, remark);
        }
        public  bool Insert(ELogType type, string creator, string billNo, string description,
            string module)
        {
            return Insert(type, creator, billNo, description, module, DateTime.Now, null);
        }
        public  bool Insert(ELogType type, string creator, string billNo, string module)
        {
            return Insert(type, creator, billNo, string.Empty, module, DateTime.Now, null);
        }
        #endregion

        private bool Save()
        {
            FrmTempAuthorize frmAuthorize = new FrmTempAuthorize("称重复核员");
            if (frmAuthorize.ShowDialog() != DialogResult.OK)
                return false;

            if (!IsFieldValueValid())
                return false;

            try
            {
                bool isCreateNew = (unitGrpEntity == null);
                string skuCode = listSku.EditValue.ToString();
                string skuLevel = txtSkuLevel.Text.Trim();
                //int result = ugDal.Save(listUnit1.EditValue.ToString(), ConvertUtil.ToInt(txtCount.Text.Trim()), skuCode, txtBarcode.Text.Trim(), ConvertUtil.ToDecimal(txtWeight.Text.Trim()),
                //    ConvertUtil.ToDecimal(txtChang.Text.Trim()), ConvertUtil.ToDecimal(txtKuan.Text.Trim()), ConvertUtil.ToDecimal(txtGao.Text.Trim()), comboIsActive.Text, this.unitGrpEntity == null ? 0 : this.unitGrpEntity.ID, isCreateNew);
                //if (result == 0)
                //{
                //    string warnMsg = string.Format("未成功更新物料 {0} 层级为 {1} 的包装关系信息，\r\n最可能的原因：未维护当前物料层级的包装关系基础信息或者该包装关系没启用，\r\n请联系技术人员进行维护。", skuCode, skuLevel);
                //    MsgBox.Warn(warnMsg);
                //    return false;
                //}
                //if (result == -1)
                //{
                //    MsgBox.Warn("编号已经存在，请改为其他的编号。");
                //    return false;
                //}
                //else
                bool ret = SaveUpdateUmSku(listUnit1.EditValue.ToString(), ConvertUtil.ToInt(txtCount.Text.Trim()), skuCode, txtBarcode.Text.Trim(), ConvertUtil.ToDecimal(txtWeight.Text.Trim()),
                    ConvertUtil.ToDecimal(txtChang.Text.Trim()), ConvertUtil.ToDecimal(txtKuan.Text.Trim()), ConvertUtil.ToDecimal(txtGao.Text.Trim()), comboIsActive.Text, this.unitGrpEntity == null ? 0 : this.unitGrpEntity.ID, isCreateNew);
                if (ret)
                {
                    Insert(ELogType.修改包装关系, String.Format("操作人:{0};授权人:{1}", GlobeSettings.LoginedUser.UserName, frmAuthorize.AuthUserCode), skuCode, "包装关系");
                     
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

        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            if (Save())
                this.DialogResult = DialogResult.OK;
        }
    }
}