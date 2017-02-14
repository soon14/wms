using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.DBHelper;
using Nodes.UI;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Stock;
using Newtonsoft.Json;

namespace Nodes.Stock
{
    public partial class FrmPickingScan : DevExpress.XtraEditors.XtraForm
    {
        private int StockID = 0;
        public FrmPickingScan(int stockID)
        {
            InitializeComponent();
            this.StockID = stockID;
        }

        /// <summary>
        /// 查看库存占用货主
        /// </summary>
        /// <param name="stockID"></param>
        /// <returns></returns>
        public DataTable GetPickingScan(int stockID)
        {
            #region
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("BILL_NO", Type.GetType("System.String"));
            tblDatas.Columns.Add("ITEM_DESC", Type.GetType("System.String"));
            tblDatas.Columns.Add("SKU_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("SKU_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("SPEC", Type.GetType("System.String"));
            tblDatas.Columns.Add("QTY", Type.GetType("System.String"));
            tblDatas.Columns.Add("UM_NAME", Type.GetType("System.String"));
            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("stockId=").Append(stockID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetPickingScan);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetPickingScan bill = JsonConvert.DeserializeObject<JsonGetPickingScan>(jsonQuery);
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
                foreach (JsonGetPickingScanResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["BILL_NO"] = tm.billNo;
                    newRow["ITEM_DESC"] = tm.itemDesc;
                    newRow["SKU_CODE"] = tm.skuCode;
                    newRow["SKU_NAME"] = tm.skuName;
                    newRow["SPEC"] = tm.spec;
                    newRow["QTY"] = tm.qty;
                    newRow["UM_NAME"] = tm.umName;
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

        private void frmLoad(object sender, EventArgs e)
        {
            try
            {
                gridControl1.DataSource = GetPickingScan(this.StockID);
            }
            catch
            { }
        }
    }
}