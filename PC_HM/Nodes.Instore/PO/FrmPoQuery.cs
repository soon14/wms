using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Nodes.Controls;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Shares;

namespace Nodes.Instore
{
    public partial class FrmPoQuery : DevExpress.XtraEditors.XtraForm
    {
        PODal poDal = null;
        POQueryDal poQueryDal = null;

        public FrmPoQuery()
        {
            InitializeComponent();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            poDal = new PODal();
            poQueryDal = new POQueryDal();

            ucQueryCondition.LoadDataSource();
            CustomFields.AppendMaterialFields(gvDetails);
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

        public POBodyEntity FocusedHeader
        {
            get
            {
                if (gvHeader.FocusedRowHandle < 0)
                    return null;

                return gvHeader.GetRow(gvHeader.FocusedRowHandle) as POBodyEntity;
            }
        }

        void ShowFocusedDetail()
        {
            POBodyEntity po = FocusedHeader;
            if (po != null && po.Details == null)
            {
                po.Details = poQueryDal.GetDetailByBillID(po.BillID);
                gvHeader.RefreshData();
            }
        }

        private void OnOKClick(object sender, EventArgs e)
        {
            POBodyEntity po = FocusedHeader;
            if (po == null)
            {
                MsgBox.Warn("未选中任何订单行。");
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void OnQueryCompleted(List<POBodyEntity> dataSource)
        {
            gdHeader.DataSource = dataSource;
        }
    }
}