using System;
using System.Data;
using Nodes.DBHelper;
using Nodes.Utils;
using Nodes.UI;
using Nodes.Entities;

namespace Nodes.Outstore
{
    public partial class FrmLoadRecords : DevExpress.XtraEditors.XtraForm
    {
        private SODal soDal = new SODal();
        public FrmLoadRecords()
        {
            InitializeComponent();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            try
            {
                this.dateStart.DateTime = DateTime.Now.AddMonths(-1);
                this.dateOver.DateTime = DateTime.Now;

                Reload();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        public void Reload()
        {
            try 
            {
                bindingSource1.DataSource = new VehicleDal().GetAll();
            }
            catch
            { }
        }

        /// <summary>
        /// 获得选中数据
        /// </summary>
        VehicleEntity SelectedUnitRow
        {
            get
            {
                if (gridView2.FocusedRowHandle < 0)
                    return null;

                return gridView2.GetFocusedRow() as VehicleEntity;
            }
        }


        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (SelectedUnitRow == null)
            {
                MsgBox.Warn("请选择一行进行查询。");
                return;
            }

            DataTable data = this.soDal.GetLoadRecordsByWhCode(SelectedUnitRow.VehicleCode, this.dateStart.DateTime.Date, this.dateOver.DateTime.AddDays(1).Date);//
            gridView1.ViewCaption = string.Format("车辆：{0} 单据信息", SelectedUnitRow.VehicleNO);
            gridControl1.DataSource = data;

        }
    }
}