using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nodes.Shares;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.UI;

namespace Reports
{
    /// <summary>
    /// 装车绩效考核
    /// </summary>
    public partial class FrmQueryLoadingReports : Form
    {
        #region 构造函数

        public FrmQueryLoadingReports()
        {
            InitializeComponent();
        }

        #endregion

        #region 方法
        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            List<UserEntity> userList = new UserDal().ListUsersByRoleAndWarehouseCode(
                GlobeSettings.LoginedUser.WarehouseCode, "装车员");
            this.gridControl3.DataSource = userList;
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
                DateTime dateBegin = this.dateBegin.EditValue == null ? DateTime.Now : ConvertUtil.ToDatetime(this.dateBegin.EditValue);
                DateTime dateEnd = this.dateEnd.EditValue == null ? DateTime.Now : ConvertUtil.ToDatetime(this.dateEnd.EditValue);
                this.gridControl1.DataSource = LoadingDal.GetLoadingReport(dateBegin, dateEnd, user.UserCode);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
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
    }
}
