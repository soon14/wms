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
using Nodes.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;

namespace Nodes.Common
{
    /// <summary>
    /// 选择订单
    /// </summary>
    public partial class FrmChooseBill : DevExpress.XtraEditors.XtraForm
    {
        #region 变量
        private SODal _soDal = new SODal();

        private int _billState = 0;                 // 订单状态
        private string _vehicleNO = string.Empty;   // 车次号
        #endregion

        #region 构造函数

        public FrmChooseBill()
        {
            InitializeComponent();
        }
        public FrmChooseBill(string vehicleNO)
            : this()
        {
            this._vehicleNO = vehicleNO;
        }
        public FrmChooseBill(int billState)
            : this()
        {
            this._billState = billState;
        }

        #endregion

        #region 方法
        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            List<SOHeaderEntity> list = this._soDal.QueryBillsByStatusUnUse("'66', '691'");
            if (!string.IsNullOrEmpty(this._vehicleNO)) // 排序前面所选的车次
            {
                list = list.FindAll(u => u.ShipNO != this._vehicleNO);
            }
            this.bindingSource1.DataSource = list;
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
                    if (header.HasChecked)
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
            this.LoadData();
            this.LoadCheckBoxImage();
        }
        #endregion

        #region 事件

        /// <summary>
        /// 关闭
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        /// <summary>
        /// 确认
        /// </summary>
        private void btnEnter_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
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
                List<SOHeaderEntity> _data = bindingSource1.DataSource as List<SOHeaderEntity>;
                if (_data == null) return;

                int currentIndex = hitInfo.Column.ImageIndex;
                bool flag = currentIndex == 0;
                for (int i = 0; i < gvHeader.RowCount; i++)
                {
                    if (gvHeader.IsRowVisible(i) == RowVisibleState.Visible)
                    {
                        gvHeader.SetRowCellValue(i, "HasChecked", flag);
                    }
                }
                //_data.ForEach(d => d.HasChecked = flag);
                hitInfo.Column.ImageIndex = 4 - currentIndex;
            }
        }
        #endregion
    }
}