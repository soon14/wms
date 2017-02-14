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
    public partial class FrmListWeighRecords : DevExpress.XtraEditors.XtraForm
    {
        private int billID;
        public FrmListWeighRecords(int billID, string billNO)
        {
            InitializeComponent();

            this.billID = billID;
            this.Text = this.Text + string.Format("(单据：{0})", billNO);
        }

        /// <summary>
        /// 出库单管理：称重记录
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public DataTable GetWeighRecordsByBillID(int billID)
        {
            #region
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("CT_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("GROSS_WEIGHT", Type.GetType("System.String"));
            tblDatas.Columns.Add("NET_WEIGHT", Type.GetType("System.String"));
            tblDatas.Columns.Add("USER_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("AUTH_USER_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("CREATE_DATE", Type.GetType("System.String"));
            tblDatas.Columns.Add("VH_NO", Type.GetType("System.String"));
            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billID=").Append(billID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetWeighRecordsByBillID);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetWeighRecordsByBillID bill = JsonConvert.DeserializeObject<JsonGetWeighRecordsByBillID>(jsonQuery);
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
                foreach (JsonGetWeighRecordsByBillIDResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["CT_CODE"] = tm.ctCode;
                    newRow["GROSS_WEIGHT"] = tm.crossWeight;
                    newRow["NET_WEIGHT"] = tm.netWeight;
                    newRow["USER_NAME"] = tm.userName;
                    newRow["AUTH_USER_NAME"] = tm.authUserName;
                    newRow["CREATE_DATE"] = tm.createDate;
                    newRow["VH_NO"] = tm.vhNo;
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
                DataTable data = GetWeighRecordsByBillID(billID);
                gridControl1.DataSource = data;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
    }
}