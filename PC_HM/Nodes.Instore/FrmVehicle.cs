using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.DBHelper;
using Nodes.Controls;
using Nodes.Utils;
using Nodes.Shares;

namespace Nodes.Instore
{
    public partial class FrmVehicle : DevExpress.XtraEditors.XtraForm
    {
        private AsnQueryDal asnQueryDal = null;
        public FrmVehicle()
        {
            InitializeComponent();
        }

        private void frmLoad(object sender, EventArgs e)
        {
            try
            {
                asnQueryDal = new AsnQueryDal();

                BindingData();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void BindingData()
        {
            txtBills.Properties.DataSource = asnQueryDal.QueryBills("20");//绑定入库单列表
            gridControl1.DataSource = asnQueryDal.GetVehicles();//统计当前库内车辆
        }

        private void btnSave(object sender, EventArgs e)
        {
            if (txtBills.Text.Trim() == "")
            {
                MsgBox.Warn("请选择入库单！");
                return;
            }
            if (txtBarcode.Text.Trim() == "")
            {
                MsgBox.Warn("请扫描送货牌条码！");
                return;
            }
            int? result = asnQueryDal.CreateVechile(ConvertUtil.ToInt(txtBills.EditValue), txtBarcode.Text.Trim(), txtDeriver.Text.Trim(), txtContactPhone.Text.Trim(), txtVehicleNo.Text.Trim(), GlobeSettings.LoginedUser.UserName);
            if (result > 0)
            {
                MsgBox.OK("登记成功！");
                BindingData();
                txtBills.EditValue = null;
                txtBarcode.Text = "";
                txtContactPhone.Text = "";
                txtDeriver.Text = "";
                txtVehicleNo.Text = "";
            }
            else if (result == -1)
            {
                MsgBox.Warn("该送货牌正在使用，请使用其他送货牌！");
            }
        }
    }
}