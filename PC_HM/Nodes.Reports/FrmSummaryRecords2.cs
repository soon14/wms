using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nodes.Entities;
using Nodes.DBHelper;
using Nodes.Shares;
using Nodes.UI;
using Nodes.Utils;
using System.Diagnostics;
using System.Threading;
using DevExpress.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Reports;
using Newtonsoft.Json;


namespace Reports
{
    /// <summary>
    /// 库房人员绩效汇总
    /// </summary>
    public partial class FrmSummaryRecords2 : Form
    {
        #region 构造函数

        public FrmSummaryRecords2()
        {
            InitializeComponent();
        }

        #endregion

        #region Override Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.itemDateBegin.EditValue = DateTime.Now.AddMonths(-1);
            this.itemDateEnd.EditValue = DateTime.Now;
        }
        #endregion

        /// <summary>
        /// 查询统计（库房人员绩效汇总）
        /// </summary>
        /// <param name="dateBegin"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public  DataTable SummaryByPersonnel(DateTime dateBegin, DateTime dateEnd)
        {
            #region DataTable
            DataTable tblDatas = new DataTable("Datas");

            #region 整散区分
            if (GlobeSettings.LoginedUser.WarehouseType == EWarehouseType.整货仓)
            {
                #region 0-10
                tblDatas.Columns.Add("USER_CODE", Type.GetType("System.String"));
                tblDatas.Columns.Add("人员姓名", Type.GetType("System.String"));
                tblDatas.Columns.Add("所属", Type.GetType("System.String"));
                tblDatas.Columns.Add("拣货单量", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("拣货量(整)", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("批市拣货量", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("补货量", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("移货次数", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("上架件数", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("上架托数", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("收货单量", Type.GetType("System.Decimal"));
                #endregion

                #region 11-20
                tblDatas.Columns.Add("收货件数", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("退货单量", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("退货总数", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("配送单量", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("配送整货", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("配送散货", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("装车整货", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("装车散货", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("二批拣货量(整)", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("批市拣货量(整)", Type.GetType("System.Decimal"));
                #endregion

                #region 21-30
                tblDatas.Columns.Add("调拨拣货量(整)", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("盘点货位数", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("称重件数(整)", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("称重次数", Type.GetType("System.Decimal"));
                #endregion
            }
            else if (GlobeSettings.LoginedUser.WarehouseType == EWarehouseType.混合仓)
            {
                #region 0-10
                tblDatas.Columns.Add("USER_CODE", Type.GetType("System.String"));
                tblDatas.Columns.Add("人员姓名", Type.GetType("System.String"));
                tblDatas.Columns.Add("所属", Type.GetType("System.String"));
                tblDatas.Columns.Add("拣货单量", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("拣货量(整)", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("拣货量(散)", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("批市拣货量", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("补货量", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("移货次数", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("上架件数", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("上架托数", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("收货单量", Type.GetType("System.Decimal"));
                #endregion

                #region 11-20
                tblDatas.Columns.Add("收货件数", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("退货单量", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("退货总数", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("配送单量", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("配送整货", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("配送散货", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("装车整货", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("装车散货", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("二批拣货量(整)", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("二批拣货量(散)", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("批市拣货量(整)", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("批市拣货量(散)", Type.GetType("System.Decimal"));
                #endregion

                #region 21-30
                tblDatas.Columns.Add("调拨拣货量(整)", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("调拨拣货量(散)", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("盘点货位数", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("称重件数(整)", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("称重件数(散)", Type.GetType("System.Decimal"));
                tblDatas.Columns.Add("称重次数", Type.GetType("System.Decimal"));
                #endregion
            }
            #endregion

            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("beginDate=").Append(dateBegin).Append("&");
                loStr.Append("endDate=").Append(dateEnd).Append("&");
                loStr.Append("warehouseType=").Append(EUtilStroreType.WarehouseTypeToInt(GlobeSettings.LoginedUser.WarehouseType));
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SummaryByPersonnel,20000);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 赋值
                if (GlobeSettings.LoginedUser.WarehouseType == EWarehouseType.整货仓)
                {
                    #region 000 整货仓
                    #region 正常错误处理

                    JsonSummaryByPersonnel bill = JsonConvert.DeserializeObject<JsonSummaryByPersonnel>(jsonQuery);
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

                    foreach (JsonSummaryByPersonnelResult tm in bill.result)
                    {
                        DataRow newRow;
                        newRow = tblDatas.NewRow();
                        #region 0-10
                        newRow["人员姓名"] = tm.userName;
                        newRow["所属"] = tm.itemDesc;
                        newRow["拣货单量"] = Convert.ToDecimal(tm.abCount);
                        newRow["拣货量(整)"] = Convert.ToDecimal(tm.bQty);
                        newRow["批市拣货量"] = Convert.ToDecimal(tm.cQty);
                        newRow["补货量"] = Convert.ToDecimal(tm.dQty);
                        newRow["移货次数"] = Convert.ToDecimal(tm.eCnt);
                        newRow["上架件数"] = Convert.ToDecimal(tm.fQty);
                        newRow["上架托数"] = Convert.ToDecimal(tm.gQty);
                        newRow["收货单量"] = Convert.ToDecimal(tm.hbCount);
                        #endregion

                        #region 11-20
                        newRow["收货件数"] = Convert.ToDecimal(tm.hQty);
                        newRow["退货单量"] = Convert.ToDecimal(tm.ibCount);
                        newRow["退货总数"] = Convert.ToDecimal(tm.iQty);
                        newRow["配送单量"] = Convert.ToDecimal(tm.dispatchOne);
                        newRow["配送整货"] = Convert.ToDecimal(tm.dispatchWhole);
                        newRow["配送散货"] = Convert.ToDecimal(tm.dispatchSanhuo);
                        newRow["装车整货"] = Convert.ToDecimal(tm.loadingWhole);
                        newRow["装车散货"] = Convert.ToDecimal(tm.loadingSanhuo);
                        newRow["二批拣货量(整)"] = Convert.ToDecimal(tm.lQty);
                        newRow["批市拣货量(整)"] = Convert.ToDecimal(tm.mQty);
                        #endregion

                        #region 21-30
                        newRow["调拨拣货量(整)"] = Convert.ToDecimal(tm.nQty);
                        newRow["USER_CODE"] = tm.userCode;
                        newRow["盘点货位数"] = Convert.ToDecimal(tm.oQty);
                        newRow["称重件数(整)"] = Convert.ToDecimal(tm.pQty);
                        newRow["称重次数"] = Convert.ToDecimal(tm.qctQty);
                        #endregion

                        tblDatas.Rows.Add(newRow);
                    }
                    #endregion   
                }
                else if (GlobeSettings.LoginedUser.WarehouseType == EWarehouseType.混合仓)
                {
                    #region 000  混合仓
                    #region 正常错误处理

                    JsonSummaryByPersonnelSanhuo bill = JsonConvert.DeserializeObject<JsonSummaryByPersonnelSanhuo>(jsonQuery);
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

                    foreach (JsonSummaryByPersonnelResultSanhuo tm in bill.result)
                    {
                        DataRow newRow;
                        newRow = tblDatas.NewRow();
                        #region 0-10
                        newRow["人员姓名"] = tm.userName;
                        newRow["所属"] = tm.itemDesc;
                        newRow["拣货单量"] = Convert.ToDecimal(tm.abCount);
                        newRow["拣货量(整)"] = Convert.ToDecimal(tm.bQty);
                        newRow["拣货量(散)"] = Convert.ToDecimal(tm.cQty);
                        newRow["批市拣货量"] = Convert.ToDecimal(tm.dQty);
                        newRow["补货量"] = Convert.ToDecimal(tm.eCnt);
                        newRow["移货次数"] = Convert.ToDecimal(tm.fQty);
                        newRow["上架件数"] = Convert.ToDecimal(tm.gQty);
                        newRow["上架托数"] = Convert.ToDecimal(tm.hQty);
                        newRow["收货单量"] = Convert.ToDecimal(tm.ibCount);
                        #endregion

                        #region 11-20
                        newRow["收货件数"] = Convert.ToDecimal(tm.iQty);
                        newRow["退货单量"] = Convert.ToDecimal(tm.jbQty);
                        newRow["退货总数"] = Convert.ToDecimal(tm.jQty);
                        newRow["配送单量"] = Convert.ToDecimal(tm.dispatchOne);
                        newRow["配送整货"] = Convert.ToDecimal(tm.dispatchWhole);
                        newRow["配送散货"] = Convert.ToDecimal(tm.dispatchSanhuo);
                        newRow["装车整货"] = Convert.ToDecimal(tm.loadingWhole);
                        newRow["装车散货"] = Convert.ToDecimal(tm.loadingSanhuo);
                        newRow["二批拣货量(整)"] = Convert.ToDecimal(tm.erCiWhole);
                        newRow["二批拣货量(散)"] = Convert.ToDecimal(tm.erCiSanHuo);
                        newRow["批市拣货量(整)"] = Convert.ToDecimal(tm.oQty);
                        newRow["批市拣货量(散)"] = Convert.ToDecimal(tm.pQty);
                        #endregion

                        #region 21-30
                        newRow["调拨拣货量(整)"] = Convert.ToDecimal(tm.qctQty);
                        newRow["调拨拣货量(散)"] = Convert.ToDecimal(tm.rQty);
                        newRow["USER_CODE"] = tm.userCode;
                        newRow["盘点货位数"] = Convert.ToDecimal(tm.sQty);
                        newRow["称重件数(整)"] = Convert.ToDecimal(tm.tQty);
                        newRow["称重件数(散)"] = Convert.ToDecimal(tm.vQty);
                        newRow["称重次数"] = Convert.ToDecimal(tm.wQty);
                        #endregion

                        tblDatas.Rows.Add(newRow);
                    }
                    #endregion
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

        #region 事件

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                DateTime dateBegin = this.itemDateBegin.EditValue == null ? DateTime.Now : ConvertUtil.ToDatetime(this.itemDateBegin.EditValue);
                DateTime dateEnd = this.itemDateEnd.EditValue == null ? DateTime.Now : ConvertUtil.ToDatetime(this.itemDateEnd.EditValue);
                using (WaitDialogForm form = new WaitDialogForm("查询时间可能需要几分钟，请耐心等待。"))
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    this.gridControl1.DataSource = ReportDal.SummaryByPersonnel(dateBegin, dateEnd);//SummaryByPersonnel(dateBegin, dateEnd);
                    sw.Stop();
                    Console.WriteLine(sw.Elapsed.TotalSeconds);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        #endregion
    }
}
