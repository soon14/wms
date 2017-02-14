using System.Collections.Generic;
using System.Windows.Forms;
using Nodes.Entities;
using Nodes.Shares;

namespace Nodes.BaseData
{
    public partial class UcMaterialGrid : UserControl
    {
        public delegate void FocusedRowChanged(MaterialEntity focusedHeader);
        public event FocusedRowChanged GridFocusedRowChanged;

        public delegate void RowDoubleClick();
        public event RowDoubleClick GridRowDoubleClick;

        public UcMaterialGrid()
        {
            InitializeComponent();
        }

        public MaterialEntity FocusedHeader
        {
            get
            {
                if (gridView1.FocusedRowHandle < 0)
                    return null;
                else
                    return gridView1.GetFocusedRow() as MaterialEntity;
            }
        }

        #region 绑定数据源
        public object DataSource
        {
            set
            {
                gridControl1.DataSource = value;
            }
            get
            {
                return gridControl1.DataSource;
            }
        }

        #endregion

        private void OnFocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (GridFocusedRowChanged != null)
                GridFocusedRowChanged(FocusedHeader);
        }

        private void OnRowDoubleClick(object sender, System.EventArgs e)
        {
            if (GridRowDoubleClick != null)
                GridRowDoubleClick();
        }
    }
}
