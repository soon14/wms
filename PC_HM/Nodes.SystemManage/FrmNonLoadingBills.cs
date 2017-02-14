using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nodes.DBHelper;
using Nodes.Entities;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Nodes.Utils;
using Nodes.UI;
using Nodes.Shares;

namespace Nodes.SystemManage
{
    /// <summary>
    /// 待装车订单
    /// </summary>
    public partial class FrmNonLoadingBills : DevExpress.XtraEditors.XtraForm
    {
        #region 变量
        private SODal _soDal = new SODal();
        private VehicleDal _vehicleDal = new VehicleDal();
        #endregion

        #region 构造函数

        public FrmNonLoadingBills()
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
            List<SOHeaderEntity> list = SODal.GetForLoadingBills();
            this.gridHeader.DataSource = list;
            // 获取车辆信息
            this.bindingSource1.DataSource = this._vehicleDal.GetAll();
            this.listPersonnel.DataSource = new UserDal().ListUsersByRoleAndWarehouseCode(
                GlobeSettings.LoginedUser.WarehouseCode, "装车员");
            this.listPersonnel.DisplayMember = "UserName";
        }

        #endregion

        #region 属性
        /// <summary>
        /// 获取选择的订单
        /// </summary>
        public List<SOHeaderEntity> SelectedBills
        {
            get
            {
                gvHeader.PostEditor();

                List<SOHeaderEntity> headers = new List<SOHeaderEntity>();
                //获取选中的单据，只处理显示出来的，不考虑由于过滤导致的未显示单据
                for (int i = 0; i < gvHeader.RowCount; i++)
                {
                    SOHeaderEntity header = gvHeader.GetRow(i) as SOHeaderEntity;
                    if (header != null && header.HasChecked)
                    {
                        headers.Add(header);
                    }
                }
                return headers;
            }
        }
        #endregion

        #region Override Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.LoadCheckBoxImage();
            this.LoadData();
        }
        #endregion

        #region "选中与复选框"
        private void LoadCheckBoxImage()
        {
            gvHeader.Images = GridUtil.GetCheckBoxImages();
            colCheck.ImageIndex = 0;
        }

        private void OnViewMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                CheckOneGridColumn(gvHeader, "HasChecked", MousePosition);
            }
        }

        private void OnViewCellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName != "HasChecked") return;
            SOHeaderEntity selectedHeader = gvHeader.GetFocusedRow() as SOHeaderEntity;
            if (selectedHeader == null) return;

            selectedHeader.HasChecked = ConvertUtil.ToBool(e.Value);
            gvHeader.CloseEditor();
        }

        private void CheckOneGridColumn(GridView view, string checkedField, Point mousePosition)
        {
            Point p = view.GridControl.PointToClient(mousePosition);
            GridHitInfo hitInfo = view.CalcHitInfo(p);
            if (hitInfo.HitTest == GridHitTest.Column && hitInfo.Column.FieldName == checkedField)
            {
                List<SOHeaderEntity> _data = gridHeader.DataSource as List<SOHeaderEntity>;
                if (_data == null) return;

                int currentIndex = hitInfo.Column.ImageIndex;
                bool flag = currentIndex == 0;
                for (int i = 0; i < gvHeader.RowCount; i++)
                {
                    //if (gvHeader.IsRowVisible(i) == RowVisibleState.Visible)
                    //{
                    gvHeader.SetRowCellValue(i, "HasChecked", flag);
                    //}
                }
                //_data.ForEach(d => d.HasChecked = flag);
                hitInfo.Column.ImageIndex = 4 - currentIndex;
            }
        }
        #endregion

        #region 事件

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnCreateTask_Click(object sender, EventArgs e)
        {
            try
            {
                List<UserEntity> userList = new List<UserEntity>();
                foreach (var obj in this.listPersonnel.CheckedItems)
                {
                    if (obj is UserEntity)
                    {
                        UserEntity user = obj as UserEntity;
                        userList.Add(user);
                    }
                }
                if (userList.Count == 0)
                {
                    MsgBox.Warn("请选择装车人员！");
                    return;
                }
                if (this.SelectedBills.Count == 0)
                {
                    MsgBox.Warn("请选择订单！");
                    return;
                }
                VehicleEntity vehicle = this.cboVehicle.EditValue as VehicleEntity;
                if (vehicle == null)
                {
                    MsgBox.Warn("请选择车辆！");
                    return;
                }
                using (FrmSOSortMap frmSOSortMap = new FrmSOSortMap(this.SelectedBills, vehicle))
                {
                    if (frmSOSortMap.ShowDialog() == DialogResult.OK)
                    {
                        this.LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err("错误：" + ex.Message);
            }
        }
        #endregion
    }
}
