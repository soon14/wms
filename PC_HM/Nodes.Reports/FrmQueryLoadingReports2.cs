using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nodes.Shares;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.UI;
using DevExpress.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Reports;
using Newtonsoft.Json;

namespace Reports
{
    /// <summary>
    /// 装车绩效考核（平均分）
    /// </summary>
    public partial class FrmQueryLoadingReports2 : Form
    {
        #region 构造函数

        public FrmQueryLoadingReports2()
        {
            InitializeComponent();
        }

        #endregion

        /// <summary>
        /// 查询统计（装车绩效考核）
        /// </summary>
        /// <param name="dateBegin"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public  DataTable GetLoadingReport2(DateTime dateBegin, DateTime dateEnd)
        {
            #region DataTable
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("人员编号", Type.GetType("System.String"));
            tblDatas.Columns.Add("人员姓名", Type.GetType("System.String"));
            tblDatas.Columns.Add("所属", Type.GetType("System.String"));
            tblDatas.Columns.Add("整货件数", Type.GetType("System.Decimal"));
            tblDatas.Columns.Add("散货件数", Type.GetType("System.Decimal"));
            
            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("beginDate=").Append(dateBegin).Append("&");
                loStr.Append("endDate=").Append(dateEnd);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetLoadingReport2);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetLoadingReport2 bill = JsonConvert.DeserializeObject<JsonGetLoadingReport2>(jsonQuery);
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
                foreach (JsonGetLoadingReport2Result tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["人员编号"] = tm.userCode;
                    newRow["人员姓名"] = tm.userName;
                    newRow["所属"] = tm.itemDesc;
                    newRow["整货件数"] = Convert.ToDecimal(tm.fullCount);
                    newRow["散货件数"] = Convert.ToDecimal(tm.singleCount);
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

        #region 方法
        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            using (WaitDialogForm dialog = new WaitDialogForm())
            {
                DateTime dateBegin = this.dateBegin.EditValue == null ? DateTime.Now : ConvertUtil.ToDatetime(this.dateBegin.EditValue);
                DateTime dateEnd = this.dateEnd.EditValue == null ? DateTime.Now : ConvertUtil.ToDatetime(this.dateEnd.EditValue);
                this.gridControl1.DataSource = GetLoadingReport2(dateBegin, dateEnd);
            }
        }
        #endregion

        #region Override Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.dateBegin.EditValue = DateTime.Now.AddMonths(-1);
            this.dateEnd.EditValue = DateTime.Now;
            //this.LoadData();
        }
        #endregion

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.LoadData();
        }
    }
}
