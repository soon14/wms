using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.Entities;
using Nodes.DBHelper;
using Nodes.UI;
using Nodes.Utils;
using System.Linq;

namespace Nodes.screen
{
    public partial class FrmVehicleAndPickTask : DevExpress.XtraEditors.XtraForm
    {
        public FrmVehicleAndPickTask()
        {
            InitializeComponent();
        }
        //测试
        ScreenDal screenDal = new ScreenDal();
        List<ScreenAchievementEntity> list = null;
        List<ScreenAchievementEntity> ListActiveDAY = null;
        List<ScreenAchievementEntity> ListActiveMonth = null;
        private string DayViewCaption = "当 日 排 名";
        private string MonthViewCaption = "当 月 排 名";
        //private int DefaultShowRowsDay = 15;
        //private int DefaultSHowRowsMonth = 20;
        private void FrmVehicleAndPickTask_Load(object sender, EventArgs e)
        {
            try
            {
                BindingGrid();
                BindUserAll();
                labelControl1.Text = String.Format("最后一次更新时间：{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                timerRefresh.Enabled = true;
                timeNextPage.Enabled = true;
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
            }
            
        }

        // 绑定左上表格
        public void BindingGrid()
        {
            try
            {
                list = screenDal.SummaryByPersonnel(1);
                //获取所有计算有成绩的人员数据
                ListActiveDAY = new List<ScreenAchievementEntity>(list.FindAll(u => u.userAllGrade > 0));
                //按照个人总分排序
                var ListActiveDAYVar = from l in ListActiveDAY
                                     orderby l.userAllGrade descending
                                     select l;
                ListActiveDAY = ListActiveDAYVar.ToList<ScreenAchievementEntity>();
                if (ListActiveDAY.Count > GlobalSettings.DefaultShowRowsDay)
                {
                    DayPageCount = ConvertUtil.ToInt(Math.Ceiling(ConvertUtil.ToDecimal(ListActiveDAY.Count) / ConvertUtil.ToDecimal(GlobalSettings.DefaultShowRowsDay)));
                    gridView1.ViewCaption = DayViewCaption + String.Format("（第{0}/{1}页）", 1, DayPageCount);
                    gridControl1.DataSource = ListActiveDAY.GetRange(0, GlobalSettings.DefaultShowRowsDay);
                    DayIndex++;
                }
                else
                {
                    gridView1.ViewCaption = DayViewCaption + String.Format("（第{0}/{1}页）", 1, 1);
                    gridControl1.DataSource = ListActiveDAY;
                }
                //绑定后三名
                List<ScreenAchievementEntity> listLast = new List<ScreenAchievementEntity>(ListActiveDAY);
                var listLastVar = from l in ListActiveDAY
                                       orderby l.userAllGrade ascending
                                       select l;
                listLast = listLastVar.ToList<ScreenAchievementEntity>();
                if (listLast.Count >= 3)
                    gridControl2.DataSource = listLast.GetRange(0, 3);
                else
                    gridControl2.DataSource = listLast;

            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
            }
        }

        // 绑定右边表格
        public void BindUserAll()
        {
            try
            {
                ListActiveMonth = screenDal.SummaryByPersonnel(2);
                var ListActiveMonthVar = from l in ListActiveMonth
                                         orderby l.userAllGrade descending
                                         select l;
                ListActiveMonth = ListActiveMonthVar.ToList<ScreenAchievementEntity>();
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

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            BindingGrid();
            labelControl1.Text = String.Format("最后刷新时间：{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        int DayIndex = 0;
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
                else 
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
                else 
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