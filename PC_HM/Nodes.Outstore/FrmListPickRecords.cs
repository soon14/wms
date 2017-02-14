using System;
using System.Data;
//using Nodes.DBHelper;
using Nodes.Utils;
using Nodes.UI;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;
using Newtonsoft.Json;

namespace Nodes.Outstore
{
    public partial class FrmListPickRecords : DevExpress.XtraEditors.XtraForm
    {
        private int billID;
        public FrmListPickRecords(int billID, string billNO)
        {
            InitializeComponent();

            this.billID = billID;
            this.Text = this.Text + string.Format("(单据：{0})", billNO);
        }

        /// <summary>
        /// 出库单管理：拣货记录
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public DataTable GetPickRecordsByBillID(int billID)
        {
            #region
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("SKU_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("SKU_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("CT_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("PICK_QTY", Type.GetType("System.String"));
            tblDatas.Columns.Add("UM_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("LC_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("USER_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("PICK_DATE", Type.GetType("System.String"));
            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billID=").Append(billID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetPickRecordsByBillID);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetPickRecordsByBillID bill = JsonConvert.DeserializeObject<JsonGetPickRecordsByBillID>(jsonQuery);
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
                foreach (JsonGetPickRecordsByBillIDResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["SKU_CODE"] = tm.skuCode;
                    newRow["SKU_NAME"] = tm.skuName;
                    newRow["CT_CODE"] = tm.ctCode;
                    newRow["PICK_QTY"] = tm.pickQty;
                    newRow["UM_NAME"] = tm.umName;
                    newRow["LC_CODE"] = tm.lcCode;
                    newRow["USER_NAME"] = tm.userName;
                    newRow["PICK_DATE"] = tm.pickDate;
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
                DataTable data = GetPickRecordsByBillID(billID);
                gridControl1.DataSource = data;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
    }
}