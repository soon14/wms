using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.DBHelper;
using Nodes.Utils;
using DevExpress.Utils;
using Nodes.Icons;

namespace Nodes.WMS.Stock
{
    public partial class FrmSequenceQuery : DevExpress.XtraEditors.XtraForm
    {
        StockDal stockDal = new StockDal();

        public FrmSequenceQuery()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }

        private void Query()
        {
            try
            {
                string seq1 = null, seq2 = null;
                if (!string.IsNullOrEmpty(txtSeq1.Text.Trim()))
                    seq1 = txtSeq1.Text.Trim();

                if (!string.IsNullOrEmpty(txtSeq2.Text.Trim()))
                    seq2 = txtSeq2.Text.Trim();

                gridControl1.DataSource = stockDal.QuerySequence(seq1, seq2);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ShowFocusedHeaderDetails(FocusedHeaderSeq);
        }

        string FocusedHeaderSeq
        {
            get
            {
                if (gridView1.GetFocusedRowCellValue("SequenceNO") == null)
                    return null;
                else
                    return ConvertUtil.ToString(gridView1.GetFocusedRowCellValue("SequenceNO"));
            }
        }

        private void ShowFocusedHeaderDetails(string seq)
        {
            if (string.IsNullOrEmpty(seq))
            {
                gridView2.ViewCaption = "未选中流水号";
                gridControl2.DataSource = null;
                return;
            }

            try
            {
                gridView2.ViewCaption = string.Format("库存信息-流水号: {0}", seq);
                gridControl2.DataSource = stockDal.QueryStockBySequence(seq);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void FrmSequenceQuery_Load(object sender, EventArgs e)
        {
            ImageCollection ic = IconHelper.LoadToolImages();
            barManager1.Images = ic;
            barButtonItem1.ImageIndex = (int)IconHelper.Images.ok;
            barButtonItem2.ImageIndex = (int)IconHelper.Images.search;
        }
    }
}