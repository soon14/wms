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
using Nodes.Entities.HttpEntity.Stock;
using Newtonsoft.Json;

namespace Nodes.Stock
{
    public partial class FrmStockTransferQuery : DevExpress.XtraEditors.XtraForm
    {
        //private StockTransferDal transferDal = null;
        public FrmStockTransferQuery()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 移库记录表--查询移库记录
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="skuName"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public DataTable QueryTransRecords(DateTime dateFrom, DateTime dateTo, string skuName, string location)
        {
            #region
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("FROM_LC_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("TO_LC_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("SKU_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("QTY_TRANS", Type.GetType("System.String"));
            tblDatas.Columns.Add("ITEM_DESC", Type.GetType("System.String"));
            tblDatas.Columns.Add("CREATE_DATE", Type.GetType("System.String"));
            tblDatas.Columns.Add("USER_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("AUTH_USER_NAME", Type.GetType("System.String"));
            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("dateFrom=").Append(dateFrom).Append("&");
                loStr.Append("dateTo=").Append(dateTo).Append("&");
                loStr.Append("skuName=").Append(skuName).Append("&");
                loStr.Append("location=").Append(location);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_QueryTransRecords);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonQueryTransRecords bill = JsonConvert.DeserializeObject<JsonQueryTransRecords>(jsonQuery);
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
                foreach (JsonQueryTransRecordsResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["FROM_LC_CODE"] = tm.fromLcCode;
                    newRow["TO_LC_CODE"] = tm.toLcCode;
                    newRow["SKU_NAME"] = tm.skuName;
                    newRow["QTY_TRANS"] = tm.qtyTrans;
                    newRow["ITEM_DESC"] = tm.itemDEsc;
                    newRow["CREATE_DATE"] = tm.createDate;
                    newRow["USER_NAME"] = tm.userName;
                    newRow["AUTH_USER_NAME"] = tm.authUserName;
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

        private void OnbtnQueryClick(object sender, EventArgs e)
        {
            DateTime dateFrom = dateStart.DateTime.Date;
            DateTime dateTo = dateEnd.DateTime.Date.AddDays(1);
            string location = txtLocation.Text.Trim();
            string skuName = txtSkuName.Text.Trim();

            try
            {
                bindingSource1.DataSource = QueryTransRecords(dateFrom, dateTo, skuName, location);
            }
            catch(Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void OnFrmLoad(object sender, EventArgs e)
        {
            dateStart.DateTime = DateTime.Now.AddDays(-7).Date;
            dateEnd.DateTime = DateTime.Now.Date;

            //this.transferDal = new StockTransferDal();
        }
    }
}