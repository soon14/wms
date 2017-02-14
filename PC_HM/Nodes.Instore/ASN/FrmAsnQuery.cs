using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Nodes.UI;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Shares;

namespace Nodes.Instore
{
    public partial class FrmAsnQuery : DevExpress.XtraEditors.XtraForm
    {
        AsnDal asnDal = null;
        AsnQueryDal asnQueryDal = null;

        public FrmAsnQuery()
        {
            InitializeComponent();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            asnDal = new AsnDal();
            asnQueryDal = new AsnQueryDal();

            ucQueryCondition.LoadDataSource();
        }

        public void LockThisState(string stateCode)
        {
            ucQueryCondition.LoadDataSource();
            ucQueryCondition.LockThisState(stateCode);
        }

        private void OnGvHeaderFocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ShowFocusedDetail();
            gvHeader.ExpandMasterRow(e.FocusedRowHandle);
        }

        public AsnBodyEntity FocusedHeader
        {
            get
            {
                if (gvHeader.FocusedRowHandle < 0)
                    return null;

                return gvHeader.GetRow(gvHeader.FocusedRowHandle) as AsnBodyEntity;
            }
        }

        void ShowFocusedDetail()
        {
            AsnBodyEntity asn = FocusedHeader;
            if (asn != null && asn.Details == null)
            {
                asn.Details = asnQueryDal.GetDetailByBillID(asn.BillID);
                gvHeader.RefreshData();
            }
        }

        private void OnOKClick(object sender, EventArgs e)
        {
            AsnBodyEntity asn = FocusedHeader;
            if (asn == null)
            {
                MsgBox.Warn("未选中任何订单行。");
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void OnQueryCompleted(List<AsnBodyEntity> dataSource)
        {
            gdHeader.DataSource = dataSource;
        }
    }
}