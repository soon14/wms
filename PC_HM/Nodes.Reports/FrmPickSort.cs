using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nodes.DBHelper;
using Nodes.Utils;
using Nodes.UI;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Nodes.Entities;
using DevExpress.XtraGrid.Views.Grid;
using Nodes.Shares;

namespace Reports
{
    /// <summary>
    /// 拣货员任务统计
    /// </summary>
    public partial class FrmPickSort : DevExpress.XtraEditors.XtraForm
    {
        #region 变量
        private SOQueryDal soQueryDal = new SOQueryDal();
        private SODal soDal = new SODal();
        #endregion

        #region 构造函数
        public FrmPickSort()
        {
            InitializeComponent();
        }

        #endregion

        #region 方法
        private void LoadData()
        {
            using (WaitDialogForm frm = new WaitDialogForm("查询中...", "请稍等"))
            {
                try
                {
                    DateTime dtStart = ConvertUtil.ToDatetime(dateStart.EditValue.ToString());
                    DateTime dtEnd = ConvertUtil.ToDatetime(dateEnd.EditValue.ToString());
                    string userCode = ConvertUtil.ToString(this.textUserCode.EditValue).Trim();

                    this.gridHeader.DataSource = soQueryDal.QueryBills(userCode, dtStart, dtEnd);
                    this.gridHeader.RefreshDataSource();
                }
                catch (Exception ex)
                {
                    MsgBox.Err(ex.Message);
                }
            }
        }
        #endregion

        #region Override Methods
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            this.dateStart.EditValue = DateTime.Now.AddDays(-1);
            this.dateEnd.EditValue = DateTime.Now;

            this.LoadData();
        }
        #endregion

        #region 事件
        /// <summary>
        /// 查询显示选择单据的详细商品信息
        /// </summary>
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRowView dataRow = this.gvHeader.GetFocusedRow() as DataRowView;
            if (dataRow == null)
            {
                gridDetails.DataSource = null;
                gvDetails.ViewCaption = "未选择单据";
            }
            else
            {

                if (GlobeSettings.LoginedUser.WarehouseType == EWarehouseType.混合仓)
                    gridDetails.DataSource = soDal.GetDetails(ConvertUtil.ToInt(dataRow.Row["BILL_ID"]), 0);
                else if(GlobeSettings.LoginedUser.WarehouseType == EWarehouseType.整货仓)
                    gridDetails.DataSource = soDal.GetDetails(ConvertUtil.ToInt(dataRow.Row["BILL_ID"]), 1);
                gvDetails.ViewCaption = string.Format("单据号: {0}", ConvertUtil.ToString(dataRow.Row["BILL_NO"]));
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        private void buttonQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.LoadData();
        }
        #endregion

        private void FrmPickSort_Load(object sender, EventArgs e)
        {

        }
    }
}
