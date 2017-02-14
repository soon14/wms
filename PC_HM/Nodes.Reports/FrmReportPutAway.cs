using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.Entities;
using Nodes.Shares;
using Nodes.DBHelper;
using Nodes.Utils;
using DevExpress.Utils;

namespace Reports
{
    public partial class FrmReportPutAway : DevExpress.XtraEditors.XtraForm
    {
        private List<MaterialEntity> ListMaterial = new List<MaterialEntity>();
        private ReportDal report = new ReportDal();
        public FrmReportPutAway()
        {
            InitializeComponent();
        }

        private void FrmLoad(object sender, EventArgs e)
        {
            BindingCombox();
        }

        private void BindingCombox()
        {
            try
            {
                dateBegin.DateTime = DateTime.Now.Date;
                dateEnd.DateTime = DateTime.Now.Date;
                this.ListMaterial = new MaterialDal().GetAll();
                this.listMaterials.Properties.DataSource = this.ListMaterial;

                listDriver.Properties.DataSource = new UserDal().ListUsersByRoleAndWarehouseCode(GlobeSettings.LoginedUser.WarehouseCode, GlobeSettings.PutAwayRoleName);
            }
            catch
            { }
        }

        private void btnClick(object sender, EventArgs e)
        {
            using (WaitDialogForm frm = new WaitDialogForm("查询中...", "请稍等"))
            {
                try
                {
                    gridControl1.DataSource = this.report.GetPutAwayRecords(listMaterials.Text == "" ? 0 : ConvertUtil.ToInt(listMaterials.EditValue), listDriver.Text == "" ? "" : ConvertUtil.ToString(listDriver.EditValue), dateBegin.DateTime.Date, dateEnd.DateTime.AddDays(1).Date);
                }
                catch{ }
            }
        }
    }
}