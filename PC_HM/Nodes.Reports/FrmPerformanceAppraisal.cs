using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.Utils;
using Nodes.UI;
using Nodes.DBHelper;
using DevExpress.Utils;
using Nodes.Entities.Report;

namespace Reports
{
    /// <summary>
    /// KPI
    /// </summary>
    public partial class FrmPerformanceAppraisal : DevExpress.XtraEditors.XtraForm
    {
        #region 构造函数

        public FrmPerformanceAppraisal()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void bar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DateTime dateStart = ConvertUtil.ToDatetime(this.deStartTime.EditValue);
            DateTime dateEnd = ConvertUtil.ToDatetime(this.deEndTime.EditValue);
            this.gridView1.Columns.Clear();
            this.gridControl1.RefreshDataSource();
            using (WaitDialogForm dialog = new WaitDialogForm())
            {
                switch (e.Item.Tag.ToString())
                {
                    case "配货率":
                        this.bindingSource1.DataSource = ReportDal.GetDistribution(dateStart.Date, dateEnd.Date);
                        this.groupControl1.Text = string.Format("{0} - {1} 配货率",
                            dateStart.ToString("yyyy-MM-dd"), dateEnd.ToString("yyyy-MM-dd"));
                        break;
                    case "发货率":
                        this.bindingSource1.DataSource = ReportDal.GetDelivery(dateStart.Date, dateEnd.Date);
                        this.groupControl1.Text = string.Format("{0} - {1} 发货率",
                            dateStart.ToString("yyyy-MM-dd"), dateEnd.ToString("yyyy-MM-dd"));
                        break;
                    case "回款率":
                        this.bindingSource1.DataSource = ReportDal.GetReturnedMoney(dateStart.Date, dateEnd.Date);
                        this.groupControl1.Text = string.Format("{0} - {1} 回款率",
                            dateStart.ToString("yyyy-MM-dd"), dateEnd.ToString("yyyy-MM-dd"));
                        break;
                    case "销售单配货单量":
                        this.bindingSource1.DataSource = ReportDal.GetAllocationCargo(dateStart, dateEnd, 1);
                        this.groupControl1.Text = string.Format("{0} - {1} 销售单 配货单量", dateStart, dateEnd);
                        break;
                    case "调拨单配货单量":
                        this.bindingSource1.DataSource = ReportDal.GetAllocationCargo(dateStart, dateEnd, 2);
                        this.groupControl1.Text = string.Format("{0} - {1} 调拨单 配货单量", dateStart, dateEnd);
                        break;
                    case "配货件数":
                        this.bindingSource1.DataSource = ReportDal.GetPickedCount(dateStart, dateEnd);
                        this.groupControl1.Text = string.Format("{0} - {1} 配货件数", dateStart, dateEnd);
                        break;
                    case "配货金额":
                        this.bindingSource1.DataSource = ReportDal.GetPickedAmount(dateStart, dateEnd);
                        this.groupControl1.Text = string.Format("{0} - {1} 配货金额", dateStart, dateEnd);
                        break;
                    case "配货平均件数":
                        this.bindingSource1.DataSource = ReportDal.GetCountAvg(dateStart, dateEnd);
                        this.groupControl1.Text = string.Format("{0} - {1} 配货平均件数", dateStart, dateEnd);
                        break;
                    case "销售单验货单量":
                        this.bindingSource1.DataSource = ReportDal.GetExamineCargoCrop(dateStart, dateEnd, 1);
                        this.groupControl1.Text = string.Format("{0} - {1} 销售单 验货单量",
                            dateStart, dateEnd);
                        break;
                    case "调拨单验货单量":
                        this.bindingSource1.DataSource = ReportDal.GetExamineCargoCrop(dateStart, dateEnd, 2);
                        this.groupControl1.Text = string.Format("{0} - {1} 调拨单 验货单量",
                            dateStart, dateEnd);
                        break;
                    case "销售单装车单量":
                        this.bindingSource1.DataSource = ReportDal.GetTruckCropD(dateStart, dateEnd, 1);
                        this.groupControl1.Text = string.Format("{0} - {1} 销售单 装车单量",
                            dateStart, dateEnd);
                        break;
                    case "调拨单装车单量":
                        this.bindingSource1.DataSource = ReportDal.GetTruckCropD(dateStart, dateEnd, 2);
                        this.groupControl1.Text = string.Format("{0} - {1} 调拨单 装车单量",
                            dateStart, dateEnd);
                        break;
                    case "销售单装车金额":
                        this.bindingSource1.DataSource = ReportDal.GetTruckPriceD(dateStart, dateEnd, 1);
                        this.groupControl1.Text = string.Format("{0} - {1} 销售单 装车金额",
                            dateStart.ToString("yyyy-MM-dd"), dateEnd.ToString("yyyy-MM-dd"));
                        break;
                    case "调拨单装车金额":
                        this.bindingSource1.DataSource = ReportDal.GetTruckPriceD(dateStart, dateEnd, 2);
                        this.groupControl1.Text = string.Format("{0} - {1} 调拨单 装车金额",
                            dateStart, dateEnd);
                        break;
                    case "送货总金额":
                        this.bindingSource1.DataSource = ReportDal.GetDeliverGoodsPrice(dateStart.Date, dateEnd.Date);
                        this.groupControl1.Text = string.Format("{0} - {1} 送货总金额",
                            dateStart.ToString("yyyy-MM-dd"), dateEnd.ToString("yyyy-MM-dd"));
                        break;
                    case "配送完成单量":
                        this.bindingSource1.DataSource = ReportDal.GetTruckCrop(dateStart, dateEnd);
                        this.groupControl1.Text = string.Format("{0} - {1} 配送完成单量",
                            dateStart.ToString("yyyy-MM-dd"), dateEnd.ToString("yyyy-MM-dd"));
                        break;
                    case "配送完成件数":
                        this.bindingSource1.DataSource = ReportDal.GetTruckCount(dateStart, dateEnd);
                        this.groupControl1.Text = string.Format("{0} - {1} 配送完成件数",
                            dateStart.ToString("yyyy-MM-dd"), dateEnd.ToString("yyyy-MM-dd"));
                        break;
                    case "验收单量":
                        this.bindingSource1.DataSource = ReportDal.GetCheckCrop(dateStart.Date, dateEnd.Date);
                        this.groupControl1.Text = string.Format("{0} - {1} 验收单量",
                            dateStart.ToString("yyyy-MM-dd"), dateEnd.ToString("yyyy-MM-dd"));
                        break;
                    case "验收金额":
                        this.bindingSource1.DataSource = ReportDal.GetCheckPrice(dateStart.Date, dateEnd.Date);
                        this.groupControl1.Text = string.Format("{0} - {1} 验收金额",
                            dateStart.ToString("yyyy-MM-dd"), dateEnd.ToString("yyyy-MM-dd"));
                        break;
                    case "打印发货单量":
                        this.bindingSource1.DataSource = ReportDal.GetPrintDeliverGoodsCrop(dateStart.Date, dateEnd.Date);
                        this.groupControl1.Text = string.Format("{0} - {1} 打印发货单量",
                            dateStart.ToString("yyyy-MM-dd"), dateEnd.ToString("yyyy-MM-dd"));
                        break;
                    case "收货单量与件数":
                        DataTable table = ReportDal.GetAsnBillCountAndTotalPiece(dateStart, dateEnd);
                        this.bindingSource1.DataSource = table;
                        this.groupControl1.Text = string.Format("{0} - {1} 收货单量与件数",
                            dateStart.ToString("yyyy-MM-dd"), dateEnd.ToString("yyyy-MM-dd"));
                        this.gridView1.Columns[0].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
                        this.gridView1.Columns[0].SummaryItem.DisplayFormat = "共计:{0:f0} 单";
                        this.gridView1.Columns[0].SummaryItem.FieldName = "订单编号";
                        this.gridView1.Columns[1].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        this.gridView1.Columns[1].SummaryItem.DisplayFormat = "共计: {0:f0} 件";
                        this.gridView1.Columns[1].SummaryItem.FieldName = "总件数";
                        break;
                    case "退货统计":
                        table = ReportDal.GetCrnReport(dateStart, dateEnd);
                        this.bindingSource1.DataSource = table;
                        this.groupControl1.Text = string.Format("{0} - {1} 退货单统计",
                            dateStart.ToString("yyyy-MM-dd"), dateEnd.ToString("yyyy-MM-dd"));
                        this.gridView1.Columns[0].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
                        this.gridView1.Columns[0].SummaryItem.DisplayFormat = "共计:{0:f0} 单";
                        this.gridView1.Columns[0].SummaryItem.FieldName = "订单编号";
                        this.gridView1.Columns[2].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        this.gridView1.Columns[2].SummaryItem.DisplayFormat = "共计:{0:f0}";
                        this.gridView1.Columns[2].SummaryItem.FieldName = "件数";
                        this.gridView1.Columns[3].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        this.gridView1.Columns[3].SummaryItem.DisplayFormat = "共计:{0:f0}";
                        this.gridView1.Columns[3].SummaryItem.FieldName = "上架数";
                        break;
                }
            }
        }

        #endregion

        #region 方法

        ///// <summary>
        ///// 打印发货单量
        ///// </summary>
        //private void PrintDeliverGoodsCrop()
        //{
        //    string printCount = dal.GetPrintDeliverGoodsCrop(sTime, eTime);
        //    MessageBox.Show(string.Format("打印出库单数为：{0}", printCount), "打印发货单量");
        //}

        ///// <summary>
        ///// 送货总金额
        ///// </summary>
        //private void DeliverGoodsPrice()
        //{
        //    string price = dal.GetDeliverGoodsPrice(sTime, eTime).ToString();
        //    MessageBox.Show(string.Format("应收总金额为：{0}", price), "送货总金额");
        //}

        ///// <summary>
        ///// 验收金额
        ///// </summary>
        //private void CheckPrice()
        //{
        //    string price = dal.GetCheckPrice(sTime, eTime).ToString();
        //    MessageBox.Show(string.Format("入库金额为：{0}", price), "验收金额");
        //}

        ///// <summary>
        ///// 验收单量
        ///// </summary>
        //private void CheckCrop()
        //{
        //    re = dal.GetCheckCrop(startTime, endTime);
        //    MessageBox.Show(string.Format("入库完成单数为：{0} 出库完成单数为：{1}", re.InList.ToString(), re.OutList.ToString()), "验收单量");
        //}

        ///// <summary>
        ///// 配送完成单量
        ///// </summary>
        //private void TruckCrop()
        //{
        //    string count = dal.GetTruckCrop(sTime, eTime);
        //    MessageBox.Show(string.Format("配送完成单数为：{0}", count), "配送完成单量");
        //}

        ///// <summary>
        ///// 配送完成件数
        ///// </summary>
        //private void TruckCount()
        //{
        //    string count = ConvertUtil.ToInt(dal.GetTruckCount(sTime, eTime)).ToString();
        //    MessageBox.Show(string.Format("配送完成件数为：{0}", count), "配送完成件数");
        //}

        ///// <summary>
        ///// 装车金额(C类库)
        ///// </summary>
        //private void TruckPriceC()
        //{
        //    string price = dal.GetTruckPriceC(sTime, eTime).ToString();
        //    MessageBox.Show(string.Format("装车金额为：{0}" , price), "装车金额(C类库)");
        //}

        ///// <summary>
        ///// 装车单量
        ///// </summary>
        //private void TruckCropC()
        //{
        //    string truckCrop = dal.GetTruckCropC(sTime, eTime);
        //    MessageBox.Show(string.Format("装车单量为：{0}" , truckCrop), "装车单量(C类库)");
        //}

        ///// <summary>
        ///// 装车金额(D类库)
        ///// </summary>
        //private void TruckPriceD()
        //{
        //    try
        //    {
        //        re = dal.GetTruckPriceD(startTime, endTime);
        //        MessageBox.Show(string.Format("实际出库数量为：{0} 实际出库金额为：{1}", ConvertUtil.ToInt(re.OutCount), re.OutPrice), "装车金额(D类库)");
        //    }
        //    catch (Exception ex)
        //    {
        //        MsgBox.Err(ex.Message);
        //    }
        //}

        ///// <summary>
        ///// 装车单量(D类库)
        ///// </summary>
        //private void TruckCropD()
        //{
        //    try
        //    {
        //        re = dal.GetTruckCropD(startTime, endTime);
        //        MessageBox.Show(string.Format("打印订单数量为：{0} 实际发货金额为：{1}", re.PrintCount.ToString(), re.DeliverPrice.ToString()), "装车单量(D类库)");
        //    }
        //    catch (Exception ex)
        //    {
        //        MsgBox.Err(ex.Message);
        //    }
        //}

        ///// <summary>
        ///// 验货单量
        ///// </summary>
        //private void ExamineCargoCrop()
        //{
        //    using (new WaitDialogForm("正在查询...", "请稍等"))
        //    {
        //        try
        //        {
        //            groupControl2.Visible = false;
        //            groupControl3.Visible = true;
        //            gridControl2.DataSource = dal.GetExamineCargoCrop(startTime, endTime);
        //        }
        //        catch (Exception ex)
        //        {
        //            MsgBox.Err(ex.Message);
        //        }
        //    }
        //}

        ///// <summary>
        ///// 配货单量
        ///// </summary>
        //private void AllocationCargo()
        //{

        //    using (new WaitDialogForm("正在查询...", "请稍等"))
        //    try
        //    {
        //        groupControl3.Visible = false;
        //        groupControl2.Visible = true;
        //        gridControl1.DataSource = dal.GetAllocationCargo(startTime, endTime);
        //    }
        //    catch (Exception ex)
        //    {
        //        MsgBox.Err(ex.Message);
        //    }
        //}

        ///// <summary>
        ///// 配货件数平均值(D类库)
        ///// </summary>
        //private void GetCountAvgD()
        //{
        //    try
        //    {
        //        MessageBox.Show(string.Format("配货件数平均值：{0}",Math.Round(dal.GetCountAvgD(startTime, endTime),2).ToString()), "配货件数平均值(D类库)");
        //    }
        //    catch (Exception ex)
        //    {
        //        MsgBox.Err(ex.Message);
        //    }
        //}

        ///// <summary>
        ///// 记录时间
        ///// </summary>
        //private void SaveTime()
        //{
        //    startTime = ConvertUtil.ToDatetime(deStartTime.EditValue.ToString());
        //    endTime = ConvertUtil.ToDatetime(deEndTime.EditValue.ToString());
        //    sTime = ConvertUtil.ToDatetime(startTime.ToString("d"));
        //    eTime = ConvertUtil.ToDatetime(endTime.ToString("d"));
        //}

        //private void FrmPerformanceAppraisal_Load(object sender, EventArgs e)
        //{
        //    deStartTime.EditValue = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
        //    deEndTime.EditValue = DateTime.Now;
        //    SaveTime();
        //    AllocationCargo();
        //}

        //private void SaveTime_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    SaveTime();
        //}

        #endregion

        #region Override Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.deStartTime.EditValue = DateTime.Now.AddMonths(-1);
            this.deEndTime.EditValue = DateTime.Now;
        }
        #endregion
    }
}