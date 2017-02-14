using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.Shares;
using Nodes.UI;
using DevExpress.Utils;
using Nodes.Resources;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Reports;
using Newtonsoft.Json;

namespace Reports
{
    public partial class FrmQuerySoDetails : DevExpress.XtraEditors.XtraForm
    {
        //SOQueryDal soQueryDal = new SOQueryDal();

        public FrmQuerySoDetails()
        {
            InitializeComponent();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            dateEditFrom.DateTime = System.DateTime.Now;
            dateEditTo.DateTime = System.DateTime.Now;
        }

        /// <summary>
        /// 查询统计（销货明细）
        /// </summary>
        /// <param name="billNO"></param>
        /// <param name="materialCode"></param>
        /// <param name="materialName"></param>
        /// <param name="customerName"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public DataTable QuerySoDetails(string billNO, string materialCode, string materialName,
            string customerName, DateTime dateFrom, DateTime dateTo)
        {
            #region DataTable
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("BILL_NO", Type.GetType("System.String"));
            tblDatas.Columns.Add("C_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("CREATE_DATE", Type.GetType("System.DateTime"));
            tblDatas.Columns.Add("CONFIRM_DATE", Type.GetType("System.DateTime"));
            tblDatas.Columns.Add("SKU_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("SKU_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("SPEC", Type.GetType("System.String"));
            tblDatas.Columns.Add("COM_MATERIAL", Type.GetType("System.String"));
            tblDatas.Columns.Add("UM_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("QTY", Type.GetType("System.Decimal"));
            tblDatas.Columns.Add("PICK_QTY", Type.GetType("System.Decimal"));
            tblDatas.Columns.Add("PRICE", Type.GetType("System.Decimal"));
            tblDatas.Columns.Add("AMOUNT", Type.GetType("System.Decimal"));
            
            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                #region 参数
                loStr.Append("beginDate=").Append(dateFrom).Append("&");
                loStr.Append("endDate=").Append(dateTo).Append("&");
                loStr.Append("billNo=").Append(billNO).Append("&");
                loStr.Append("skuCode=").Append(materialCode).Append("&");
                loStr.Append("skuName=").Append(materialName).Append("&");
                loStr.Append("cName=").Append(customerName);
                #endregion

                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_QuerySoDetails);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonQuerySoDetails bill = JsonConvert.DeserializeObject<JsonQuerySoDetails>(jsonQuery);
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
                foreach (JsonQuerySoDetailsResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["BILL_NO"] = tm.billNo;
                    newRow["C_NAME"] = tm.cName;
                    newRow["CREATE_DATE"] = Convert.ToDateTime(tm.createDate);
                    newRow["CONFIRM_DATE"] = Convert.ToDateTime(tm.confirmDate);
                    newRow["SKU_CODE"] = tm.skuCode;
                    newRow["SKU_NAME"] = tm.skuName;
                    newRow["SPEC"] = tm.spec;
                    newRow["COM_MATERIAL"] = tm.comMaterial;
                    newRow["UM_NAME"] = tm.umName;
                    newRow["QTY"] = Convert.ToDecimal(tm.qty);
                    newRow["PICK_QTY"] = Convert.ToDecimal(tm.pickQty);
                    newRow["PRICE"] = Convert.ToDecimal(tm.price);
                    newRow["AMOUNT"] = Convert.ToDecimal(tm.amount);
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

        private void btnQuery_Click(object sender, EventArgs e)
        {
            using (WaitDialogForm frm = new WaitDialogForm("查询中...", "请稍等"))
            {
                try
                {
                    DateTime dateFrom = dateEditFrom.DateTime;
                    DateTime dateTo = dateEditTo.DateTime;

                    string billNO = txtBillNO.Text.Trim();
                    string customerName = txtCustomerName.Text.Trim();
                    string material = txtMaterialCode.Text.Trim();
                    string materialName = txtMaterialName.Text.Trim();
                    bindingSource1.DataSource = QuerySoDetails(billNO, material, materialName, customerName, dateFrom, dateTo);
                }
                catch (Exception ex)
                {
                    MsgBox.Err(ex.Message);
                }
            }
        }
    }
}