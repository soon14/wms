using System;
using Nodes.DBHelper;
using Nodes.Utils;

namespace Nodes.WMS.Outbound
{
    public partial class FrmListPackDetail : DevExpress.XtraEditors.XtraForm
    {
        SODal soDal = new SODal();
        private int billID;

        public FrmListPackDetail(int billID, string billNO)
        {
            InitializeComponent();

            this.billID = billID;
            this.Text = this.Text + string.Format("(单据：{0})", billNO);
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            gridControl1.DataSource = soDal.ListPackDetail(this.billID);
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            object id = gridView1.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                gridControl2.DataSource = null;
                gridControl3.DataSource = null;
            }
            else
            {
                int packID = ConvertUtil.ToInt(id);
                gridControl2.DataSource = soDal.ListPackSequence(packID);
                gridControl3.DataSource = soDal.ListPackItem(packID);
            }
        }
    }
}