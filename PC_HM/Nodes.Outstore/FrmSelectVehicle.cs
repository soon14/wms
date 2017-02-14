using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.DBHelper;
using Nodes.UI;
using Nodes.Utils;

namespace Nodes.Outstore
{
    public partial class FrmSelectVehicle : DevExpress.XtraEditors.XtraForm
    {
        private VehicleDal vehicleDal = new VehicleDal();
        private int vehicleID = 0;
        public FrmSelectVehicle()
        {
            InitializeComponent();
        }

        public int VehcileID
        {
            get { return vehicleID; }
            set { vehicleID = value; }
        }
        public void LoadDataAndBindGrid()
        {
            try
            {
                bindingSource1.DataSource = this.vehicleDal.GetAll();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        

        private void FrmSelectVehicle_Load(object sender, EventArgs e)
        {
            LoadDataAndBindGrid();
        }

        private void btnOKOnClick(object sender, EventArgs e)
        {
            vehicleID = ConvertUtil.ToInt(lookUpEdit1.EditValue);
            this.DialogResult = DialogResult.OK;
        }
    }
}