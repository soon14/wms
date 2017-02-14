using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nodes.UI;
using Nodes.DBHelper;
using Nodes.Shares;
using Nodes.Utils;
using Nodes.Entities;
using DevExpress.XtraGrid.Columns;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Stock;
using Newtonsoft.Json;

namespace Nodes.Stock
{
    /// <summary>
    /// 单品补货
    /// </summary>
    public partial class FrmSingleReplenish : DevExpress.XtraEditors.XtraForm
    {
        #region 变量
        private ReplenishDal replenishDal = new ReplenishDal();
        private List<BillSKUNum> _list = null;
        #endregion

        #region 构造函数

        public FrmSingleReplenish()
        {
            InitializeComponent();
        }
        public FrmSingleReplenish(List<BillSKUNum> list)
            : this()
        {
            this._list = list;
        }

        #endregion

        /// <summary>
        /// 当前订单量（查询补货库存）
        /// </summary>
        /// <returns></returns>
        public DataTable QueryReplenishStock()
        {
            #region
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("物料编码", Type.GetType("System.String"));
            tblDatas.Columns.Add("商品名称", Type.GetType("System.String"));
            tblDatas.Columns.Add("备货区库存", Type.GetType("System.String"));
            tblDatas.Columns.Add("单位", Type.GetType("System.String"));
            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("cardState=").Append(cardState);
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_QueryReplenishStock);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonQueryReplenishStock bill = JsonConvert.DeserializeObject<JsonQueryReplenishStock>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return tblDatas;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return tblDatas;
                }
                #endregion

                #region 赋值
                foreach (JsonQueryReplenishStockResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["物料编码"] = tm.skuCode;
                    newRow["商品名称"] = tm.skuName;
                    newRow["备货区库存"] = tm.stockQty;
                    newRow["单位"] = tm.umName;
                    tblDatas.Rows.Add(newRow);
                }
                return tblDatas;
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return tblDatas;
        }

        #region 方法
        private void LoadData()
        {
            this.txtQty.Text = "0";
            this.lblUnit.Text = string.Empty;
            this.searchLookUpEdit1.Properties.DataSource = QueryReplenishStock();
        }
        #endregion

        #region 事件

        /// <summary>
        /// 通过商品编码审计
        /// </summary>
        /// <param name="skuCode"></param>
        /// <param name="shortQty"></param>
        /// <param name="gID"></param>
        /// <param name="isCase"></param>
        /// <returns></returns>
        public bool InquiryBySku(string skuCode, decimal shortQty, string gID, int isCase)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("skuCode=").Append(skuCode).Append("&");
                loStr.Append("shortQty=").Append(shortQty).Append("&");
                if (GlobeSettings.LoginedUser.WarehouseType == EWarehouseType.混合仓)
                    loStr.Append("isCase=").Append(isCase).Append("&");
                loStr.Append("gId=").Append(gID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_InquiryBySku);
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
        /// 当前订单量（获取结果集）
        /// </summary>
        /// <param name="gID"></param>
        /// <returns></returns>
        public List<StockTransEntity> GetResultByGID(string gID)
        {
            List<StockTransEntity> list = new List<StockTransEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("gId=").Append(gID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetResultByGID);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetResultByGID bill = JsonConvert.DeserializeObject<JsonGetResultByGID>(jsonQuery);
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
                #region 赋值

                foreach (JsonGetResultByGIDResult tm in bill.result)
                {
                    StockTransEntity sku = new StockTransEntity();
                    //tm.fromStockId;
                    sku.IsCase = Convert.ToInt32(tm.isCase);
                    sku.Location = tm.Location;
                    sku.Material = tm.Material;
                    sku.TransferQty = Convert.ToDecimal(tm.TransferQty);
                    sku.MaterialName = tm.skuName;
                    sku.Spec = tm.spec;
                    sku.TargetLocation = tm.TargetLocation;
                    sku.UnitName = tm.umName;
                    list.Add(sku);
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
        /// 生成补货任务
        /// </summary>
        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView row = this.searchLookUpEdit1.EditValue as DataRowView;
                if (row == null)
                {
                    MsgBox.Warn("请选择商品！");
                    return;
                }
                string skuCode = row["物料编码"].ToString();
                decimal stockQty = ConvertUtil.ToDecimal(row["备货区库存"]);
                decimal qty = ConvertUtil.ToInt(this.txtQty.Text);
                if (qty <= 0 || qty > stockQty)
                {
                    MsgBox.Warn("请输入正确的补货数量！");
                    return;
                }
                string gid = Guid.NewGuid().ToString().Replace("-", "");
                InquiryBySku(skuCode, stockQty, gid, 2);

                List<StockTransEntity> results = GetResultByGID(gid);
                FrmCreateReplenishBill frm = new FrmCreateReplenishBill(results, false);
                this.DialogResult = frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
        private void cboSkuList_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataRowView row = this.searchLookUpEdit1.EditValue as DataRowView;
                if (row == null) return;
                this.lblUnit.Text = row["单位"].ToString();
                if (this._list == null || this._list.Count == 0)
                    return;
                BillSKUNum skuEntity = this._list.Find(u => u.SKUCode == row["物料编码"].ToString());
                if (skuEntity != null)
                {
                    decimal totalQty = ConvertUtil.ToDecimal(row["备货区库存"]);
                    decimal resultQty = (skuEntity.BillQty - skuEntity.TotalQty) * skuEntity.Qty;
                    this.txtQty.Text = (totalQty < resultQty ? totalQty : resultQty).ToString("f0");
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
        #endregion

        #region Override Methods
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            try
            {
                this.LoadData();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
        #endregion
    }
}
