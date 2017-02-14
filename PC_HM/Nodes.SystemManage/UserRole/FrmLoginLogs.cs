using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using Nodes.Utils;
using Nodes.Entities;
using Nodes.DBHelper;
using Nodes.Resources;
using Nodes.UI;

namespace Nodes.SystemManage
{
    public partial class FrmLoginLogs : DevExpress.XtraEditors.XtraForm
    {       
        public FrmLoginLogs()
        {
            InitializeComponent();
        } 

        bool isOneWeek = true;
        string labCode, labName, labIp= null;
        DateTime labDateBegin, labDateEnd;
        UserDal userDal = new UserDal();

        private void BindingDate()
        {
            dateBeginDate.DateTime = System.DateTime.Now.AddDays(-7);
            dateEndDate.DateTime = System.DateTime.Now;
        }

        private void FrmLoginLogs_Load(object sender, EventArgs e)
        {
            ImageCollection ic = AppResource.LoadToolImages();
            barManager1.Images = ic;
            barButtonItem1.ImageIndex = (int)AppResource.EIcons.refresh;
            barButtonItem2.ImageIndex = (int)AppResource.EIcons.week;

            BindingDate();
            ReloadOperateHistories(labCode, labName, labIp, DateTime.Now.AddDays(-7), DateTime.Now);
        }

        private void ReloadOperateHistories(string code, string name, string ip,DateTime dateBegin, DateTime dateEnd)
        {
            BindDetail(code, name, ip,dateBegin, dateEnd);
        }

        private void BindDetail(string code, string name, string ip, DateTime dateBegin, DateTime dateEnd)
        {
            try
            {
                if (dateBeginDate.DateTime > dateEndDate.DateTime)
                {
                    MsgBox.Warn("开始时间不能大于结束时间。");
                    return;
                }

                LoginLogEntiy logs = new LoginLogEntiy();
                labCode = logs.UserCode = string.IsNullOrEmpty(code) ? null : code;
                labName = logs.UserName = string.IsNullOrEmpty(name) ? null : name;
                labIp = logs.IP = string.IsNullOrEmpty(ip) ? null : ip;
                labDateBegin = dateBegin;
                labDateEnd = dateEnd;

                List<LoginLogEntiy> loginlogs = userDal.ListLoginLogs(logs, dateBegin, dateEnd.AddDays(1));
                gridOperateHistoryEntities.DataSource = loginlogs;
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
                DisplayText = string.Format(@"{0} {1} {2} {3} {4}",
                    "查询条件: ",
                     string.IsNullOrEmpty(labCode) ? null : "用户帐号=" + labCode + separator,
                    string.IsNullOrEmpty(labName) ? null : "姓名=" + labName + separator,
                    string.IsNullOrEmpty(labIp) ? null : "IP=" + labIp + separator,
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

            BindDetail(txtCode.Text, txtName.Text, txtIP.Text,
                dateBeginDate.DateTime.Date,
                    dateEndDate.DateTime.Date);
            ClearInfo();

        }
        private void ClearInfo()
        {
            popupControlContainer1.HidePopup();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearInfo();
        }
        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string tag = ConvertUtil.ToString(e.Item.Tag);
            switch (tag)
            {
                case "刷新":
                    ReloadOperateHistories(labCode, labName, labIp, labDateBegin, labDateEnd);
                    break;
            }
        }
    }
}