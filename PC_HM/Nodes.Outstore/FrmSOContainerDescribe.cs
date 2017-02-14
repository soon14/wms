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
using Nodes.Entities.HttpEntity.Outstore;
using Newtonsoft.Json;

namespace Nodes.Outstore
{
    public partial class FrmSOContainerDescribe : DevExpress.XtraEditors.XtraForm
    {
        private int billID;

        public FrmSOContainerDescribe(int billID, string billNO)
        {
            InitializeComponent();

            this.billID = billID;
            this.Text = this.Text + string.Format("(单据：{0})", billNO);
        }

        public DataTable GetContainerByBillID(int billID)
        {
            #region
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("BILL_NO", Type.GetType("System.String"));
            tblDatas.Columns.Add("VH_TRAIN_NO", Type.GetType("System.String"));
            tblDatas.Columns.Add("CT_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("CT_STATE", Type.GetType("System.String"));
            tblDatas.Columns.Add("LC_CODE", Type.GetType("System.String"));
            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billID=").Append(billID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetContainerByBillID);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetContainerByBillID bill = JsonConvert.DeserializeObject<JsonGetContainerByBillID>(jsonQuery);
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
                foreach (JsonGetContainerByBillIDResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["BILL_NO"] = tm.billNo;
                    newRow["VH_TRAIN_NO"] = tm.vhTrainNo;
                    newRow["CT_CODE"] = tm.ctCode;
                    newRow["CT_STATE"] = tm.ctState;
                    newRow["LC_CODE"] = tm.lcCode;
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

        private void FrmSOContainerDescribe_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable data = GetContainerByBillID(billID);
                gridControl1.DataSource = data;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
    }
}