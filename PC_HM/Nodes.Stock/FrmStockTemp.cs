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
using Nodes.UI;
using Nodes.Entities.Inventory;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Stock;
using Newtonsoft.Json;

namespace Nodes.Stock
{
    public partial class FrmStockTemp : DevExpress.XtraEditors.XtraForm
    {
        List<StockSKUEntity> list = null;
        public FrmStockTemp()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 待称重集货区查询--按照SKU统计
        /// </summary>
        /// <returns></returns>
        public DataTable GetTempStockBySKU()
        {
            #region
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("商品名称", Type.GetType("System.String"));
            tblDatas.Columns.Add("商品编码", Type.GetType("System.String"));
            tblDatas.Columns.Add("商品规格", Type.GetType("System.String"));
            tblDatas.Columns.Add("数量", Type.GetType("System.String"));
            tblDatas.Columns.Add("单位名称", Type.GetType("System.String"));
            tblDatas.Columns.Add("单位编码", Type.GetType("System.String"));
            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("cardState=").Append(cardState);
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_GetTempStockBySKU);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetTempStockBySKU bill = JsonConvert.DeserializeObject<JsonGetTempStockBySKU>(jsonQuery);
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
                foreach (JsonGetTempStockBySKUResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["商品名称"] = tm.skuName;
                    newRow["商品编码"] = tm.skuCode;
                    newRow["商品规格"] = tm.spec;
                    newRow["数量"] = tm.qty;
                    newRow["单位名称"] = tm.umName;
                    newRow["单位编码"] = tm.umCode;
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

        /// <summary>
        /// 待称重集货区查询--按照托盘统计
        /// </summary>
        /// <returns></returns>
        public DataTable GetTempStockByCTCode()
        {
            #region
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("托盘编号", Type.GetType("System.String"));
            tblDatas.Columns.Add("托盘状态", Type.GetType("System.String"));
            tblDatas.Columns.Add("订单编号", Type.GetType("System.String"));
            tblDatas.Columns.Add("订单状态", Type.GetType("System.String"));
            tblDatas.Columns.Add("托盘位", Type.GetType("System.String"));
            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("cardState=").Append(cardState);
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_GetTempStockByCTCode);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetTempStockByCTCode bill = JsonConvert.DeserializeObject<JsonGetTempStockByCTCode>(jsonQuery);
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
                foreach (JsonGetTempStockByCTCodeResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["托盘编号"] = tm.ctCode;
                    newRow["托盘状态"] = tm.itemDesc;
                    newRow["订单编号"] = tm.billNo;
                    newRow["订单状态"] = tm.billDesc;
                    newRow["托盘位"] = tm.lcCode;
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

   

        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
                string tag = ConvertUtil.ToString(e.Item.Tag);
                switch (tag)
                {
                    case "按照商品统计":
                        LoadData(GetTempStockBySKU(), tag);
                        break;
                    case "按照托盘统计":
                        LoadData(GetTempStockByCTCode(), tag);
                        break;
                    case "按照订单统计":
                        LoadData(GetTempStockByBill(), tag);
                        break;
                }
        }

        /// <summary>
        /// 待称重集货区查询--按照订单统计
        /// </summary>
        /// <returns></returns>
        public DataTable GetTempStockByBill()
        {
            #region
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("订单编号", Type.GetType("System.String"));
            tblDatas.Columns.Add("订单状态", Type.GetType("System.String"));
            tblDatas.Columns.Add("托盘列表", Type.GetType("System.String"));
            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("cardState=").Append(cardState);
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_GetTempStockByBill);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetTempStockByBill bill = JsonConvert.DeserializeObject<JsonGetTempStockByBill>(jsonQuery);
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
                foreach (JsonGetTempStockByBillResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["订单编号"] = tm.billNo;
                    newRow["订单状态"] = tm.itemDesc;
                    newRow["托盘列表"] = tm.ctCode;
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

        private void FrmStockSKU_Load(object sender, EventArgs e)
        {
            LoadData(GetTempStockByBill(), "按照订单统计");
        }

        private void LoadData(DataTable ds, string tag)
        {
            try
            {
                using (WaitDialogForm form = new WaitDialogForm("正在统计", "请稍后"))
                {
                    gridView1.Columns.Clear();
                    gridControl1.DataSource = null;
                    bindingSource1.DataSource = ds;
                    gridControl1.DataSource = bindingSource1.DataSource;
                    gridControl1.RefreshDataSource();
                    gridView1.ViewCaption = tag;
                    if (gridView1.RowCount > 0)
                    {
                        gridView1.Columns[0].Width = 160;
                        gridView1.Columns[0].SummaryItem.SummaryType = SummaryItemType.Count;
                        gridView1.Columns[0].SummaryItem.FieldName = gridView1.Columns[0].FieldName;
                        gridView1.Columns[0].SummaryItem.DisplayFormat = "总计：{0}";
                        foreach (GridColumn c in gridView1.Columns)
                        {
                            if (c.FieldName == "订单编号" )
                            {
                                c.Width = 160;
                            }
                            else if (c.FieldName == "托盘列表")
                            {
                                c.Width = 220;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
    }
}