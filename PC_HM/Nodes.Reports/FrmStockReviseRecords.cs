using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using Nodes.DBHelper;
using DevExpress.Utils;
using Nodes.UI;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Reports;
using Newtonsoft.Json;

namespace Nodes.Reports
{
    public partial class FrmStockReviseRecords : Form
    {
        //private ReportDal report = new ReportDal();
        public FrmStockReviseRecords()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 查询库存修正记录
        /// </summary>
        /// <param name="lc_code"></param>
        /// <param name="dateBegin"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public DataTable GetStockReviseRecords(DateTime? dateBegin, DateTime? dateEnd, int nums, int begin, out int total)
        {
            total = 0;

            #region
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("lcCode", Type.GetType("System.String"));
            tblDatas.Columns.Add("skuCode", Type.GetType("System.String"));
            tblDatas.Columns.Add("oldStockQty", Type.GetType("System.Decimal"));
            tblDatas.Columns.Add("newStockQty", Type.GetType("System.Decimal"));
            tblDatas.Columns.Add("CZ_USER", Type.GetType("System.String"));
            tblDatas.Columns.Add("FH_USER", Type.GetType("System.String"));
            tblDatas.Columns.Add("createtime", Type.GetType("System.DateTime"));
            tblDatas.Columns.Add("UM_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("SKU_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("DIFFQTY", Type.GetType("System.Decimal"));
            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("dateBegin=").Append(dateBegin).Append("&");
                if (begin == 0)
                {
                    loStr.Append("beginRow=").Append("&");
                    loStr.Append("rows=").Append("&");
                }
                else
                {
                    loStr.Append("beginRow=").Append(begin).Append("&");
                    loStr.Append("rows=").Append(nums).Append("&");
                }
                loStr.Append("dateEnd=").Append(dateEnd);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetStockReviseRecords);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetStockReviseRecords bill = JsonConvert.DeserializeObject<JsonGetStockReviseRecords>(jsonQuery);
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
                foreach (JsonGetStockReviseRecordsResult tm in bill.result)
                {
                    DataRow newRow;
                    #region
                    newRow = tblDatas.NewRow();
                    newRow["lcCode"] = tm.lcCode;
                    newRow["skuCode"] = tm.skuCode;
                    newRow["oldStockQty"] = StringToDecimal.GetTwoDecimal(tm.oldStockQty);
                    newRow["newStockQty"] = StringToDecimal.GetTwoDecimal(tm.newStockQty);
                    newRow["CZ_USER"] = tm.czUser;
                    newRow["FH_USER"] = tm.fhUser;
                    newRow["createtime"] = Convert.ToDateTime(tm.createtime);
                    newRow["UM_NAME"] = tm.umName;
                    newRow["SKU_NAME"] = tm.skuName;
                    newRow["DIFFQTY"] = StringToDecimal.GetTwoDecimal(tm.diffqty);
                    #endregion

                    tblDatas.Rows.Add(newRow);
                }

                total = bill.total;

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
                    int total;
                    DataTable dt = GetStockReviseRecords(dateBegin.DateTime, dateEnd.DateTime.AddDays(1), 0, 0, out total);
                    if (dt.Rows.Count <= 0)
                    {
                        MsgBox.Warn("没有查询出数据，请重新选择查询日期范围！");
                    }
                    else
                    {
                        gridControl1.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {

                    MsgBox.Warn(ex.Message);
                }

            }
        }

        private void FrmStockReviseRecords_Load(object sender, EventArgs e)
        {
            dateBegin.DateTime = dateEnd.DateTime = DateTime.Now;
        }
    }
}
