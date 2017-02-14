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

namespace Reports
{
    /// <summary>
    /// 库房人员绩效汇总
    /// </summary>
    public partial class FrmSummaryRecords : Form
    {
        #region 构造函数

        public FrmSummaryRecords()
        {
            InitializeComponent();
        }

        #endregion

        #region 方法
        private void LoadData()
        {
            List<UserEntity> userList = UserDal.ListUsersByWarehouseCode(GlobeSettings.LoginedUser.WarehouseCode);
            this.gridControl3.DataSource = userList;
        }
        #endregion

        #region Override Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.dateBegin.EditValue = DateTime.Now.AddMonths(-1);
            this.dateEnd.EditValue = DateTime.Now;
            this.LoadData();
        }
        #endregion

        #region 事件

        private void gridView3_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                this.gridControl1.DataSource = null;
                UserEntity user = this.gridView3.GetFocusedRow() as UserEntity;
                if (user == null)
                    return;
                DateTime dateBegin = this.dateBegin.EditValue == null ? DateTime.Now.Date : ConvertUtil.ToDatetime(this.dateBegin.EditValue).Date;
                DateTime dateEnd = this.dateEnd.EditValue == null ? DateTime.Now.AddDays(1).Date.AddSeconds(-1) : ConvertUtil.ToDatetime(this.dateEnd.EditValue).AddDays(1).Date.AddSeconds(-1);
                Stopwatch sw = new Stopwatch();
                sw.Start();
                //DataTable dt = ReportDal.SummaryByPersonnel(dateBegin, dateEnd, user.UserCode);
                //sw.Stop();
                //MessageBox.Show(sw.ElapsedMilliseconds + "");
                //this.gridControl1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        #endregion
    }
}
