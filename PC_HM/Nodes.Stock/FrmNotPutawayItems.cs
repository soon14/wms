using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.DBHelper;
using Nodes.Shares;
using Nodes.Utils;

namespace Nodes.WMS.Stock
{
    public partial class FrmNotPutawayItems : DevExpress.XtraEditors.XtraForm
    {
        private StockDal stockDal = new StockDal();

        public FrmNotPutawayItems()
        {
            InitializeComponent();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                DataTable data = stockDal.ListNotPutawayItems(GlobeSettings.LoginedUser.WarehouseCode);
                gridControl1.DataSource = data;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void OnHeaderFocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0)
                gridControl2.DataSource = null;
            else
                ShowFocusedSequence();
        }

        void ShowFocusedSequence()
        {
            object detailID = gridView1.GetFocusedRowCellValue("ID");
            DataTable data = new AsnDal().ListNotPutawaySeqs(ConvertUtil.ToInt(detailID));
            gridControl2.DataSource = data;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            LoadData();
            ShowFocusedSequence();
        }
    }
}