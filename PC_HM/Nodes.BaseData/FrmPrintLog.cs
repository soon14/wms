using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using Nodes.Resources;
using Nodes.DBHelper;
using Nodes.Utils;
using Nodes.Entities;
using Nodes.UI;

namespace Nodes.BaseData
{
    public partial class FrmPrintLog : DevExpress.XtraEditors.XtraForm
    {
        bool isOneWeek = true;
        string  labName = null;
        DateTime labDateBegin, labDateEnd;
        PrintLogDal printlog = new PrintLogDal();
        public FrmPrintLog()
        {
            InitializeComponent();
        }
        private void BindingDate()
        {
            dateBeginDate.DateTime = System.DateTime.Now.AddDays(-7);
            dateEndDate.DateTime = System.DateTime.Now;
        }
        private void FrmPrintLog_Load(object sender, EventArgs e)
        {
            ImageCollection ic = AppResource.LoadToolImages();
            barManager1.Images = ic;
            barButtonItem1.ImageIndex = (int)AppResource.EIcons.refresh;
            barButtonItem2.ImageIndex = (int)AppResource.EIcons.week;
            BindingDate();
            ReloadOperatePrintLogs(labName,DateTime.Now.AddDays(-7), DateTime.Now);
        }

        private void ReloadOperatePrintLogs( string name, DateTime dateBegin, DateTime dateEnd)
        {
            BindDetail( name, dateBegin, dateEnd);
        }

        private void BindDetail(string name, DateTime dateBegin, DateTime dateEnd)
        {
            try
            {
                if (dateBeginDate.DateTime > dateEndDate.DateTime)
                {
                    MsgBox.Warn("开始时间不能大于结束时间。");
                    return;
                }

                PrintLogEntity logs = new PrintLogEntity();
                labName = logs.PRINT_USER = string.IsNullOrEmpty(name) ? null : name;
                labDateBegin = dateBegin;
                labDateEnd = dateEnd;

                List<PrintLogEntity> printlogs = printlog.ListPrintLogs(logs, dateBegin, dateEnd.AddDays(1));
                gridOperateHistoryEntities.DataSource = printlogs;
                labelTextDisplay();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
        private void labelTextDisplay()
        {
            string DisplayText = string.Empty;
            string separator = "  &&";
            if (isOneWeek)
            {
                DisplayText = "查询条件：最近7天的登录日志。";
            }
            else
            {
                DisplayText = string.Format(@"{0} {1} {2} ",
                    "查询条件: ",
                    string.IsNullOrEmpty(labName) ? null : "姓名=" + labName + separator,
                  "时间范围 从 " + labDateBegin.ToShortDateString() + " 至 " + labDateEnd.ToShortDateString());
            }

            labelControl1.Text = DisplayText.TrimEnd('&');
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            QueryBind();
        }
        private void QueryBind()
        {
            if (dateBeginDate.EditValue == null || dateEndDate.EditValue == null)
            {
                MsgBox.Warn("请选择开始日期和结束日期");
                return;
            }
            isOneWeek = false;

            BindDetail( txtName.Text,
                dateBeginDate.DateTime.Date,
                    dateEndDate.DateTime.Date);
            ClearInfo();

        }
        private void ClearInfo()
        {
            popupControlContainer1.HidePopup();
        }

        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string tag = ConvertUtil.ToString(e.Item.Tag);
            switch (tag)
            {
                case "刷新":
                    ReloadOperatePrintLogs(labName, labDateBegin, labDateEnd);
                    break;
            }
        }
    }
}