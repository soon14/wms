using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;

namespace Nodes.WMS.Stock
{
    public partial class FrmComMaterialSplit : DevExpress.XtraEditors.XtraForm
    {
        public event EventHandler DataSourceChanged = null;
        private StockRecordEntity stockEntity;
        public FrmComMaterialSplit(StockRecordEntity stockEntity)
        {
            InitializeComponent();
            this.stockEntity = stockEntity;
            this.Text = this.Text + string.Format("({0})", stockEntity.Material);
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            spinEdit1.Properties.MaxValue = stockEntity.Qty - stockEntity.OccupyQty;
            spinEdit1.Properties.MinValue = 1;
            spinEdit1.Value = stockEntity.Qty - stockEntity.OccupyQty;
            spinEdit1.Properties.IsFloatValue = false;

            List<MaterialEntity> materials = new CombMaterialDal().ListMaterialsByCombCode(stockEntity.Material);
            gridControl1.DataSource = materials;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int qty = (int)spinEdit1.Value;
            if (MsgBox.AskOK(string.Format("确认要拆分吗？拆分数量是“{0}”。", qty)) != DialogResult.OK)
                return;

            try
            {
                List<MaterialEntity> materials = gridControl1.DataSource as List<MaterialEntity>;
                new StockDal().SplitComMaterial(stockEntity.StockID, qty, materials);

                if (DataSourceChanged != null)
                    DataSourceChanged(null, null);

                MsgBox.OK("拆分完成。");
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
    }
}