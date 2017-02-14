using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.Utils;
//using Nodes.DBHelper;
using Nodes.Shares;
using Nodes.UI;
using DevExpress.XtraBars;
using Nodes.Entities;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Stock;
using Newtonsoft.Json;

namespace Nodes.Stock
{
    public partial class FrmSkuLog : DevExpress.XtraEditors.XtraForm
    {
        #region 变量


        private int _stockID = 0;
        private DateTime _beginDate = DateTime.Now;
        private DateTime _endDate = DateTime.Now;

        #endregion

        #region 构造函数
        public FrmSkuLog()
        {
            InitializeComponent();
        }

        public FrmSkuLog(int stockID)
        {
            InitializeComponent();
            this._stockID = stockID;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 台账记录
        /// </summary>
        /// <param name="stockID"></param>
        /// <param name="skuCode"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataTable QuerySkuLog(int stockID, string skuCode, DateTime beginDate, DateTime endDate)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("stockId=").Append(stockID).Append("&");
                loStr.Append("skuCode=").Append(skuCode).Append("&");
                loStr.Append("beginDate=").Append(beginDate).Append("&");
                loStr.Append("endDate=").Append(endDate);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_QuerySkuLog);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return null;
                }
                #endregion

                if (!string.IsNullOrEmpty(skuCode))
                {
                    #region
                    DataTable tblDatas = new DataTable("Datas");
                    tblDatas.Columns.Add("EVT", Type.GetType("System.String"));
                    tblDatas.Columns.Add("BILL_NO", Type.GetType("System.String"));
                    tblDatas.Columns.Add("EVT_DATE", Type.GetType("System.String"));
                    tblDatas.Columns.Add("USER_NAME", Type.GetType("System.String"));
                    tblDatas.Columns.Add("QTY", Type.GetType("System.String"));
                    tblDatas.Columns.Add("LC_CODE", Type.GetType("System.String"));
                    #endregion

                    #region 正常错误处理

                    JsonQuerySkuLog bill = JsonConvert.DeserializeObject<JsonQuerySkuLog>(jsonQuery);
                    if (bill == null)
                    {
                        //MsgBox.Warn(WebWork.JSON_DATA_NULL);
                        return tblDatas;
                    }
                    if (bill.flag != 0)
                    {
                        MsgBox.Warn(bill.error);
                        return tblDatas;
                    }
                    #endregion

                    #region 赋值
                    foreach (JsonQuerySkuLogResult tm in bill.result)
                    {
                        DataRow newRow;
                        newRow = tblDatas.NewRow();
                        newRow["EVT"] = tm.evt;
                        newRow["BILL_NO"] = tm.billNo;
                        newRow["EVT_DATE"] = tm.evtDate;
                        newRow["USER_NAME"] = tm.userName;
                        newRow["QTY"] = tm.qty;
                        newRow["LC_CODE"] = tm.lcCode;
                        tblDatas.Rows.Add(newRow);
                    }
                    return tblDatas;
                    #endregion
                }
                else
                {
                    #region 正常错误处理

                    JsonQuerySkuLogNoLC bill = JsonConvert.DeserializeObject<JsonQuerySkuLogNoLC>(jsonQuery);
                    if (bill == null)
                    {
                        //MsgBox.Warn(WebWork.JSON_DATA_NULL);
                        return null;
                    }
                    if (bill.flag != 0)
                    {
                        MsgBox.Warn(bill.error);
                        return null;
                    }
                    #endregion
                    DataTable tblDatas = new DataTable("Datas");
                    tblDatas.Columns.Add("EVT", Type.GetType("System.String"));
                    tblDatas.Columns.Add("BILL_NO", Type.GetType("System.String"));
                    tblDatas.Columns.Add("EVT_DATE", Type.GetType("System.String"));
                    tblDatas.Columns.Add("USER_NAME", Type.GetType("System.String"));
                    tblDatas.Columns.Add("QTY", Type.GetType("System.String"));
                    //tblDatas.Columns.Add("LC_CODE", Type.GetType("System.String"));
                    #region 赋值
                    foreach (JsonQuerySkuLogNoLCResult tm in bill.result)
                    {
                        DataRow newRow;
                        newRow = tblDatas.NewRow();
                        newRow["EVT"] = tm.evt;
                        newRow["BILL_NO"] = tm.billNo;
                        newRow["EVT_DATE"] = tm.evtDate;
                        newRow["USER_NAME"] = tm.userName;
                        newRow["QTY"] = tm.qty;
                        //newRow["LC_CODE"] = tm.lcCode;
                        tblDatas.Rows.Add(newRow);
                    }
                    return tblDatas;
                    #endregion
                }

            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return null;
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

        private void LoadData()
        {
            try
            {
                this.listMaterial.DataSource = GetAll();

                DateTime beginDate = this.itemBeginDate.EditValue == null ? DateTime.Now.AddMonths(-1) : ConvertUtil.ToDatetime(this.itemBeginDate.EditValue);
                DateTime endDate = this.itemEndDate.EditValue == null ? DateTime.Now : ConvertUtil.ToDatetime(this.itemEndDate.EditValue);

                DataTable dataTable = QuerySkuLog(
                    this._stockID, this.barEditItem1.EditValue == null ? "" : this.barEditItem1.EditValue.ToString(),
                    beginDate.Date, 
                    endDate.Date.AddDays(1).AddSeconds(-1));
                this.gridControl1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        #endregion

        #region 事件

        private void itemQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BarItem item = e.Item;
            if (item.Tag == null) return;
            switch (item.Tag.ToString())
            {
                case "查询":
                    this.LoadData();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Override Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.itemBeginDate.EditValue = DateTime.Now.AddMonths(-1);
            this.itemEndDate.EditValue = DateTime.Now;
            this.LoadData();
        }
        #endregion
    }
}