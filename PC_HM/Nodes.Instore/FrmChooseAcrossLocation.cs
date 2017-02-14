using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Shares;
using Nodes.Utils;

namespace Nodes.WMS.Inbound
{
    public partial class FrmChooseAcrossLocation : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 获取选中的货位
        /// </summary>
        public string SelectedLocation
        {
            get
            {
                return ConvertUtil.ToString(lookUpEdit1.EditValue);
            }
        }

        public FrmChooseAcrossLocation()
        {
            InitializeComponent();
        }

        private void FrmChooseAcrossLocation_Load(object sender, EventArgs e)
        {
            LocationDal locDal = new LocationDal();
            List<LocationEntity> locations = locDal.GetStockLocationByWarehouse(GlobeSettings.LoginedUser.WarehouseCode, SysCodeConstant.ZONE_TYPE_TEMP);
            lookUpEdit1.Properties.DataSource = locations;

            //默认选中第一行
            if (locations.Count > 0)
                lookUpEdit1.EditValue = locations[0].LocationCode;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lookUpEdit1.EditValue == null)
            {
                MsgBox.Warn("请选择货位。");
                return;
            }

            this.DialogResult = DialogResult.OK;
        }
    }
}