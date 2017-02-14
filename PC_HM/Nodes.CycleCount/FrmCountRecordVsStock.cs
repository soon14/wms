using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
//using Nodes.DBHelper;
using Nodes.UI;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.CycleCount;
using Newtonsoft.Json;

namespace Nodes.CycleCount
{
    public partial class FrmCountRecordVsStock : DevExpress.XtraEditors.XtraForm
    {
       
        private int billID;

        public FrmCountRecordVsStock(int billID)
        {
            InitializeComponent();

            this.billID = billID;
        }

        /// <summary>
        /// 盘点单管理---跟库存实时比对，显示报告
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public DataTable GetReportVsStock(int billID)
        {
            #region DataTable
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("ZN_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("LC_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("SKU_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("COUNT_QTY", Type.GetType("System.String"));
            tblDatas.Columns.Add("STOCK_QTY", Type.GetType("System.String"));
            tblDatas.Columns.Add("DIFF_QTY", Type.GetType("System.String"));
            tblDatas.Columns.Add("STOCK_EXP_DATE", Type.GetType("System.DateTime"));
            tblDatas.Columns.Add("EXP_DATE", Type.GetType("System.DateTime"));
            tblDatas.Columns.Add("IS_SYNC", Type.GetType("System.String"));
            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetReportVsStock);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetReportVsStock bill = JsonConvert.DeserializeObject<JsonGetReportVsStock>(jsonQuery);
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
                foreach (JsonGetReportVsStockResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["ZN_NAME"] = tm.znName;
                    newRow["LC_CODE"] = tm.lcCode;
                    newRow["SKU_NAME"] = tm.skuName;
                    newRow["COUNT_QTY"] = tm.countQty;
                    newRow["STOCK_QTY"] = tm.stockQty;
                    //newRow["DIFF_QTY"] = tm.;
                    if (!string.IsNullOrEmpty(tm.stockExpDate))
                        newRow["STOCK_EXP_DATE"] = tm.stockExpDate;
                    if (!string.IsNullOrEmpty(tm.expDate))
                        newRow["EXP_DATE"] = tm.expDate;
                    //newRow["IS_SYNC"] = tm.is;
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

        private void OnFormLoad(object sender, EventArgs e)
        {
            try
            {
                DataView v = GetReportVsStock(this.billID).DefaultView;
                bindingSource1.DataSource = v;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            DataView data = bindingSource1.DataSource as DataView;
            if (checkEdit1.Checked)
                data.RowFilter = "COUNT_QTY <> STOCK_QTY";
            else
                data.RowFilter = null;

            bindingSource1.ResetBindings(false);
        }
    }
}