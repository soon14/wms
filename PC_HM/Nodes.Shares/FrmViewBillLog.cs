using System;
using System.Data;
using Nodes.UI;
//using Nodes.DBHelper;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Shares;
using Newtonsoft.Json;

namespace Nodes.Shares
{
    public partial class FrmViewBillLog : DevExpress.XtraEditors.XtraForm
    {
        private int BillID;
        private string StateCode = String.Empty;
        private string BillNO = String.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="stateCode">102：采购单状态编码；</param>
        public FrmViewBillLog(int billID,string billNO, string stateCode)
        {
            InitializeComponent();

            this.BillID = billID;
            this.BillNO = billNO;
            this.StateCode = stateCode;
            this.Text = this.Text + string.Format("(单据：{0})", billNO);
        }

        /// <summary>
        /// 收货单据管理，查询出库单据日志（收货管理未使用）
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public DataTable SOGetBillLog(int billID)
        {//(@rowNO := @rowNo+1)
            #region 创建DataTable

            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("CREATE_DATE", Type.GetType("System.DateTime"));
            tblDatas.Columns.Add("EVT", Type.GetType("System.String"));
            tblDatas.Columns.Add("CREATOR", Type.GetType("System.String"));
            #endregion
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SOGetBillLog);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonSOGetBill bill = JsonConvert.DeserializeObject<JsonSOGetBill>(jsonQuery);
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
                foreach (JsonSOGetBillResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    try
                    {
                        if (!string.IsNullOrEmpty(tm.createDate))
                            newRow["CREATE_DATE"] = Convert.ToDateTime(tm.createDate);
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Warn(ex.Message);
                        //LogHelper.errorLog("FrmViewBillLog+SOGetBillLog", ex);
                    }
                    newRow["EVT"] = tm.evt;
                    newRow["CREATOR"] = tm.creator;
                    tblDatas.Rows.Add(newRow);
                }
                return tblDatas;
                #endregion
            }
            catch (Exception exa)
            {
                MsgBox.Err(exa.Message);
            }
            return tblDatas;
        }

        /// <summary>
        /// 收货单据管理，查询入库单据日志
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public DataTable ASNGetBillLog(int billID)
        {//(@rowNO := @rowNo+1)
            #region 创建DataTable

            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("CREATE_DATE", Type.GetType("System.DateTime"));
            tblDatas.Columns.Add("EVT", Type.GetType("System.String"));
            tblDatas.Columns.Add("CREATOR", Type.GetType("System.String"));
            #endregion
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_ASNGetBillLog);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonASNGetBill bill = JsonConvert.DeserializeObject<JsonASNGetBill>(jsonQuery);
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
                foreach (JsonASNGetBillResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    try
                    {
                        if (!string.IsNullOrEmpty(tm.createDate))
                            newRow["CREATE_DATE"] = Convert.ToDateTime(tm.createDate);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmViewBillLog+ASNGetBillLog", msg);
                    }
                    newRow["EVT"] = tm.evt;
                    newRow["CREATOR"] = tm.creator;
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
                DataTable data = null;
                if (StateCode == "出库单据")
                    data = SOGetBillLog(this.BillID);
                else
                    data = ASNGetBillLog(this.BillID);

                gridControl1.DataSource = data;

                DataView _chartView = data.DefaultView;
                this.chartControl1.Series[0].ArgumentDataMember = "EVT";
                this.chartControl1.Series[0].ValueDataMembersSerializable = "CREATE_DATE";
                this.chartControl1.DataSource = _chartView;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
    }
}