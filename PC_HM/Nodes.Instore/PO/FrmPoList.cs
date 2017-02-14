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
using Nodes.Shares;
using Nodes.Controls;
using Nodes.Utils;

namespace Nodes.Instore
{
    public partial class FrmPoList : DevExpress.XtraEditors.XtraForm
    {
        POQueryDal poQueryDal = new POQueryDal();

        public FrmPoList()
        {
            InitializeComponent();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            dateEditFrom.DateTime = DateTime.Now.AddMonths(-1);
            dateEditTo.DateTime = DateTime.Now;

            try
            {
                CustomFields.AppendMaterialFields(gvHeader);

                //绑定供应商
                listSupplier.Properties.DataSource = new SupplierDal().ListActiveSupplierByPriority();

                //绑定采购业务员
                listSales.Properties.DataSource = new UserDal().ListUsersByRoleAndOrgCode(GlobeSettings.LoginedUser.OrgCode, GlobeSettings.POSalesRoleName);

                //绑定业务类型
                listBillType.Properties.DataSource = BaseCodeDal.GetItemList(BaseCodeConstant.PO_TYPE);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void OnLookUpEditButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            BaseEdit editor = sender as BaseEdit;
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
                editor.EditValue = null;
        }

        private void OnQueryClick(object sender, EventArgs e)
        {
            try
            {
                string billID = ConvertUtil.StringToNull(txtBillID.Text);
                string contractNO = ConvertUtil.StringToNull(txtContractNO.Text);
                string billTypeCode = ConvertUtil.ObjectToNull(listBillType.EditValue);
                string supplierCode = ConvertUtil.ObjectToNull(listSupplier.EditValue);
                string materialCode = ConvertUtil.StringToNull(txtMaterial.Text);
                string sales = ConvertUtil.StringToNull(listSales.Text);
                DateTime dateFrom = dateEditFrom.DateTime.Date;
                DateTime dateTo = dateEditTo.DateTime.AddDays(1).Date;
                if (dateFrom >= dateTo)
                {
                    MsgBox.Warn("起始日期不能大于截止日期，查不到任何数据。");
                    return;
                }

                gdHeader.DataSource = poQueryDal.PoList(GlobeSettings.LoginedUser.OrgCode, billID, contractNO, 
                    supplierCode, billTypeCode, materialCode, sales, dateFrom, dateTo);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
    }
}