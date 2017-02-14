using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Nodes.UI;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Resources;
using Nodes.Utils;
using System.Threading;

namespace Nodes.Outstore
{
    public partial class FrmBackConfirmReport : DevExpress.XtraEditors.XtraForm
    {
        private SODal soDal = null;
        private VehicleDal vehicleDal = null;
        List<SOHeaderEntity> List = null;
        public FrmBackConfirmReport()
        {
            InitializeComponent();
        }

        private void OnFrmLoad(object sender, EventArgs e)
        {
            try
            {
                this.soDal = new SODal();
                this.vehicleDal = new VehicleDal();
                dtBegin.DateTime = DateTime.Now.AddDays(-1);
                dtEnd.DateTime = DateTime.Now;
                LoadDataAndBindGrid();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        public SOHeaderEntity GetFocusedBill()
        {
            return SelectedHeader;
        }

        SOHeaderEntity SelectedHeader
        {
            get
            {
                if (this.gridView1.FocusedRowHandle < 0)
                    return null;
                else
                    return gridView1.GetFocusedRow() as SOHeaderEntity;
            }
        }

        public void LoadDataAndBindGrid()
        {
            try
            {
                List<VehicleEntity> list = this.vehicleDal.GetAll();
                VehicleEntity itm = new VehicleEntity();
                itm.ID = -1;
                itm.VehicleNO = "ALL";
                list.Insert(0, itm);
                bindingSource1.DataSource = list;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        } 

         private void OnbtnQueryClick(object sender, EventArgs e)
         {
             try
             {
                 int vehicleID = ConvertUtil.ToInt(lstVehicle.EditValue);
                 //if (vehicleID <= 0)
                 //{
                 //    MsgBox.Warn("请选择车辆信息。");
                 //    return;
                 //}
                 DateTime begin = ConvertUtil.ToDatetime(dtBegin.DateTime.ToShortDateString() + " 00:00:00");
                 DateTime end = ConvertUtil.ToDatetime(dtEnd.DateTime.ToShortDateString() + " 23:59:59");
                 List = this.soDal.GetConfirmHistory(vehicleID, begin, end);
                 gridControl1.DataSource = List;
             }
             catch (Exception ex)
             {
                 MsgBox.Err(ex.Message);
             }
         }
        
         public List<SOHeaderEntity> GetFocusedBills()
         {
             List<SOHeaderEntity> checkedBills = new List<SOHeaderEntity>();
             int[] focusedHandles = this.gridView1.GetSelectedRows();
             foreach (int handle in focusedHandles)
             {
                 if (handle >= 0)
                     checkedBills.Add(gridView1.GetRow(handle) as SOHeaderEntity);
             }

             return checkedBills;
         }
    }
}