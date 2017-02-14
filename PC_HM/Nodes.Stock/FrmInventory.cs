using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.Entities;
using Nodes.DBHelper;
using Nodes.Utils;
using Nodes.Shares;
using Nodes.UI;

namespace Nodes.Stock
{
    public partial class FrmInventory : DevExpress.XtraEditors.XtraForm
    {
        public event EventHandler DataSourceChanged = null;
        StockRecordEntity StockRecord = new StockRecordEntity();
        OccupyRecordDal occupyrecorddal = new OccupyRecordDal();
        StockDal stockdal = new StockDal();
        public FrmInventory()
        {
            InitializeComponent();
        }
     
        public FrmInventory(StockRecordEntity Entity)
            : this()
        {
            this.StockRecord = Entity;
        }


        private void FrmInventory_Load(object sender, EventArgs e)
        {
            bindTextData();
        }

        void bindTextData()
        {
            this.txtLocation.Text = this.StockRecord.Location;
            this.txtMaterial.Text = this.StockRecord.Material;
            this.txtMaterialName.Text = this.StockRecord.MaterialName;
            this.txtComMaterial.Text = this.StockRecord.ComMaterial;
            this.txtDueDate.Text = this.StockRecord.ExpDate.ToString();
            this.txtBatchNo.Text = this.StockRecord.BatchNO;
            this.txtStockQty.Text = this.StockRecord.Qty.ToString();
            this.txtOccupyQty.Text = this.StockRecord.OccupyQty.ToString();
            this.txtCanOccupyQty.Text = (this.StockRecord.Qty - this.StockRecord.OccupyQty).ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            int OccupyQty = Convert.ToInt32(this.txtInventoryQty.Text) + Convert.ToInt32(this.txtOccupyQty.Text);
            if (OccupyQty > Convert.ToInt32(this.txtCanOccupyQty.Text))
            {
                MsgBox.Err("占用数量大于可占用的数量。");
                return;
            }
            int result = stockdal.UpdateOccupyQty(OccupyQty, StockRecord.StockID);
            if (result > 0)
            {
                OccupyRecordEntity OccupyRecord = new OccupyRecordEntity();
                OccupyRecord.StockID = StockRecord.StockID;
                OccupyRecord.Creator = GlobeSettings.LoginedUser.UserName;
                OccupyRecord.CreateDate = DateTime.Now;
                OccupyRecord.OccupyQty = Convert.ToInt32(this.txtInventoryQty.Text);
                OccupyRecord.Remark =txtRemark.Text ;
                OccupyRecord.Status = SysCodeConstant.OCCUPY_STATUS_OK;
                result = occupyrecorddal.OccupyRecordAdd(OccupyRecord);
                if (DataSourceChanged != null && result>0)
                {
                    StockRecord.OccupyQty = OccupyQty;
                    DataSourceChanged(StockRecord, null);
                    this.Close();
                }
            }
            else
            {
                MsgBox.Err("占用失败。");
            }
        }


    }
}