using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.Utils;
using Nodes.UI;
using Nodes.DBHelper;

namespace Nodes.Outstore
{
    public partial class FrmSaleSort : DevExpress.XtraEditors.XtraForm
    {
        private SODal soDal = new SODal();
        public FrmSaleSort()
        {
            InitializeComponent();
        }

        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DoClickEvent(ConvertUtil.ToString(e.Item.Tag));
        }

        private void DoClickEvent(string tag)
        {
            switch (tag)
            {
                case "刷新":
                    LoadData();
                    break;
                case "查询":
                    LoadData();
                   
                    break;
            }
        }

        private void LoadData()
        {
            try
            {
                gridView1.ViewCaption = String.Format("{0} 至 {1} 商品销售排行", ConvertUtil.ToString(dateStart.EditValue == null ? dateStart.EditValue : ConvertUtil.ToDatetime(dateStart.EditValue.ToString()).ToString("yyyy-MM-dd")),
                        ConvertUtil.ToString(dateEnd.EditValue == null ? dateEnd.EditValue : ConvertUtil.ToDatetime(dateEnd.EditValue.ToString()).ToString("yyyy-MM-dd")));

                DataTable dtResult = this.soDal.GetSKUSaleSort(ConvertUtil.ToString(dateStart.EditValue == null ? dateStart.EditValue : ConvertUtil.ToDatetime(dateStart.EditValue.ToString()).Date),
                    ConvertUtil.ToString(dateEnd.EditValue == null ? dateEnd.EditValue : ConvertUtil.ToDatetime(dateEnd.EditValue.ToString()).AddDays(1).Date));
                gridControl1.DataSource = dtResult;
                gridControl1.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void FrmLoad(object sender, EventArgs e)
        {
            try
            {
                dateStart.EditValue = DateTime.Now.AddDays(-7);
                dateEnd.EditValue = DateTime.Now;

                LoadData();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void gridView1_RowDoubleClick(object sender, EventArgs e)
        {
            try
            {
                string skuCode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SKU_CODE").ToString();
                if (skuCode == "")
                {
                    return;
                }
                FrmSKULocation frmSKULocation = new FrmSKULocation(skuCode);
                frmSKULocation.ShowDialog();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
    }
}