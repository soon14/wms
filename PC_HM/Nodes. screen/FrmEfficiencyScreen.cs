using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.UI;
using System.Linq;
using Nodes.Utils;

namespace Nodes.screen
{
    public partial class FrmEfficiencyScreen : DevExpress.XtraEditors.XtraForm
    {
        ScreenDal screenDal = new ScreenDal();
        List<EfficiencyEntity> list = null;
        List<EfficiencyEntity> ListActiveDAY = null;
        List<EfficiencyEntity> ListActiveMonth = null;
        private string DayViewCaption = "当 日 排 名";
        private string MonthViewCaption = "当 月 排 名";
        //private int DefaultShowRowsDay = 15;
        //private int DefaultSHowRowsMonth = 20;
        public FrmEfficiencyScreen()
        {
            InitializeComponent();
        }

        private void FrmLoad(object sender, EventArgs e)
        {
            BindingGrid();
            BindUserAll();
            labelControl1.Text = String.Format("最后刷新时间：{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            timeRefresh.Enabled = true;
            timeNextPage.Enabled = true;
        }

        // 绑定左上表格
        public void BindingGrid()
        {
            try
            {
                list = screenDal.SummaryByPersonnelEfficiency(1);
                ListActiveDAY = new List<EfficiencyEntity>(list);
                //实体个人成绩倒序排列
                var listActiveVar=from l in ListActiveDAY
                              orderby l.UserAllGrade descending
                              select l;
                ListActiveDAY=listActiveVar.ToList<EfficiencyEntity>();
                //绑定第一页
                if (ListActiveDAY.Count > GlobalSettings.DefaultShowRowsDay)
                {
                    DayPageCount = ConvertUtil.ToInt(Math.Ceiling(ConvertUtil.ToDecimal(ListActiveDAY.Count) / ConvertUtil.ToDecimal(GlobalSettings.DefaultShowRowsDay)));
                    gridView1.ViewCaption =DayViewCaption + String.Format("（第{0}/{1}页）", 1, DayPageCount);
                    gridControl1.DataSource = ListActiveDAY.GetRange(0, GlobalSettings.DefaultShowRowsDay);
                    DayIndex++;
                }
                else
                {
                    gridView1.ViewCaption = DayViewCaption + String.Format("（第{0}/{1}页）", 1, 1);
                    gridControl1.DataSource = ListActiveDAY;
                }
                List<EfficiencyEntity> copyList = new List<EfficiencyEntity>(ListActiveDAY);

                //被踢次数前三名
                List<EfficiencyEntity> listTimeout = list.FindAll(u => u.TimeoutQty > 0);
                var listTimeoutVar = from l in listTimeout
                              orderby l.TimeoutQty descending
                              select l;
                listTimeout = listTimeoutVar.ToList<EfficiencyEntity>();
                // copyList.Sort((x, y) => x.CompareTo(y));
                if (listTimeout.Count >= 3)
                    gridControl2.DataSource = listTimeout.GetRange(0,3);
                else
                    gridControl2.DataSource = listTimeout;

                //等待时间最长后三名
                List<EfficiencyEntity> listWaitTime = list.FindAll(u => u.PickConfirmDate > 0);
                var listWaitTimeVar = from l in listWaitTime
                                      orderby l.PickConfirmDate descending
                                      select l;
                listWaitTime = listWaitTimeVar.ToList<EfficiencyEntity>();
                if (listWaitTime.Count >= 3)
                    gridControl4.DataSource = listWaitTime.GetRange(0, 3);
                else
                    gridControl4.DataSource = listWaitTime;

                //完成任务数
                List<EfficiencyEntity> listTaskCount = list.FindAll(u => u.TaskCount > 0);
                var listTaskCountVar = from l in listTaskCount
                                       orderby l.TaskCount ascending
                                       select l;
                listTaskCount = listTaskCountVar.ToList<EfficiencyEntity>();
                if (listTaskCount.Count >= 3)
                    gridControl5.DataSource = listTaskCount.GetRange(0, 3);
                else
                    gridControl5.DataSource = listTaskCount;
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
            }
        }

        public void BindUserAll()
        {
            try
            {
                // 绑定左上
                ListActiveMonth = screenDal.SummaryByPersonnelEfficiency(2);
                //var ListActiveMonthVar=from l in ListActiveMonth
                //                       orderby l.UserAllGrade descending
                //                       select l;
                //ListActiveMonth = ListActiveMonthVar.ToList<EfficiencyEntity>();
                if (ListActiveMonth.Count > GlobalSettings.DefaultSHowRowsMonth)
                {
                    MonthPageCount = ConvertUtil.ToInt(Math.Ceiling(ConvertUtil.ToDecimal(ListActiveMonth.Count) / ConvertUtil.ToDecimal(GlobalSettings.DefaultSHowRowsMonth)));
                    gridView3.ViewCaption = MonthViewCaption + String.Format("（第{0}/{1}页）", 1, MonthPageCount);
                    gridControl3.DataSource = ListActiveMonth.GetRange(0, GlobalSettings.DefaultSHowRowsMonth);
                    MonthIndex++;
                }
                else
                {
                    gridView3.ViewCaption = MonthViewCaption + String.Format("（第{0}/{1}页）", 1, 1);
                    gridControl3.DataSource = ListActiveMonth;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
            }
        }

        private void timeRefresh_Tick(object sender, EventArgs e)
        {
            BindingGrid();
            labelControl1.Text = String.Format("最后刷新时间：{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        int DayIndex  = 0;
        int MonthIndex = 0;
        int DayPageCount = 0;
        int MonthPageCount = 0;
        private void timeNextPage_Tick(object sender, EventArgs e)
        {
            try
            {
                int DayRowTotal = ListActiveDAY.Count;
                DayPageCount = ConvertUtil.ToInt(Math.Ceiling(ConvertUtil.ToDecimal(DayRowTotal) / ConvertUtil.ToDecimal(GlobalSettings.DefaultShowRowsDay)));
                int DayLastPageRowCount = DayRowTotal % GlobalSettings.DefaultShowRowsDay;

                int MonthRowTotal = ListActiveMonth.Count;
                MonthPageCount = ConvertUtil.ToInt(Math.Ceiling(ConvertUtil.ToDecimal(MonthRowTotal) / ConvertUtil.ToDecimal(GlobalSettings.DefaultSHowRowsMonth)));
                int MonthLastPageRowCount = MonthRowTotal % GlobalSettings.DefaultSHowRowsMonth;

                if (DayIndex < (DayPageCount - 1))
                {
                    gridView1.ViewCaption = DayViewCaption + String.Format("（第{0}/{1}页）", DayIndex + 1, DayPageCount);
                    gridControl1.DataSource = ListActiveDAY.GetRange(DayIndex * GlobalSettings.DefaultShowRowsDay, GlobalSettings.DefaultShowRowsDay);
                    DayIndex++;
                }
                else if (DayIndex == (DayPageCount - 1))
                {
                    gridView1.ViewCaption = DayViewCaption + String.Format("（第{0}/{1}页）", DayPageCount, DayPageCount);
                    gridControl1.DataSource = ListActiveDAY.GetRange(DayIndex * GlobalSettings.DefaultShowRowsDay, DayLastPageRowCount);
                    DayIndex = 0;
                }


                if (MonthIndex < (MonthPageCount - 1))
                {
                    gridView3.ViewCaption = MonthViewCaption + String.Format("（第{0}/{1}页）", MonthIndex + 1, MonthPageCount);
                    gridControl3.DataSource = ListActiveMonth.GetRange(MonthIndex * GlobalSettings.DefaultSHowRowsMonth, GlobalSettings.DefaultSHowRowsMonth);
                    MonthIndex++;
                }
                else if (MonthIndex == (MonthPageCount - 1))
                {
                    gridView3.ViewCaption = MonthViewCaption + String.Format("（第{0}/{1}页）", MonthPageCount, MonthPageCount);
                    gridControl3.DataSource = ListActiveMonth.GetRange(MonthIndex * GlobalSettings.DefaultSHowRowsMonth, MonthLastPageRowCount);
                    MonthIndex = 0;
                }
                
            }
            catch (Exception ex)
            { }
        }
    }
}