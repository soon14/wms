using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Shares;
using Nodes.Utils;
using Nodes.UI;

namespace Nodes.WMS.Outbound
{
    public partial class FrmDeletedSO : DevExpress.XtraEditors.XtraForm
    {
        private DeletedSODal soDal = new DeletedSODal();

        public FrmDeletedSO()
        {
            InitializeComponent();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            ListDeletedBills();
        }

        private void ListDeletedBills()
        {
            try
            {
                List<DeletedSOHeaderEntity> asnHeaderEntitys = soDal.QueryBillsQuickly(GlobeSettings.LoginedUser.WarehouseCode);
                bindingSource1.DataSource = asnHeaderEntitys;

                ShowFocusedBillDetail();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DoClickEvent(ConvertUtil.ToString(e.Item.Tag));
        }

        private void DoClickEvent(string itemTag)
        {
            switch (itemTag)
            {
                case "刷新":
                    ListDeletedBills();
                    break;
                case "还原":
                    RestoreFocusedBill();
                    break;
            }
        }

        private DeletedSOHeaderEntity SelectHeader
        {
            get
            {
                if (gvHeader.GetFocusedRowCellValue("BillNO") == null)
                    return null;
                else
                    return gvHeader.GetFocusedRow() as DeletedSOHeaderEntity;
            }
        }

        private void OnHeaderFocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ShowFocusedBillDetail();
        }

        void ShowFocusedBillDetail()
        {
            DeletedSOHeaderEntity selectedHeader = SelectHeader;
            if (selectedHeader == null)
            {
                gridDetails.DataSource = null;
            }
            else
            {
                List<DeletedSODetailEntity> plans = soDal.GetDetailsByBillID(selectedHeader.BillID);
                gridDetails.DataSource = plans;
            }
        }

        private void RestoreFocusedBill()
        {
            DeletedSOHeaderEntity soHeaderEntity = SelectHeader;
            if (soHeaderEntity == null)
            {
                MsgBox.Warn("请选中要还原的单据。");
                return;
            }

            if (MsgBox.AskOK(string.Format("确认要还原单据“{0}”吗？", soHeaderEntity.BillNO)) != DialogResult.OK)
                return;

            try
            {
                //先看单据编号是否已存在
                SOHeaderEntity header = new SODal().GetHeaderInfoByBillNO(soHeaderEntity.BillNO, GlobeSettings.LoginedUser.WarehouseCode);
                if (header != null)
                {
                    MsgBox.Warn("单据编号已存在，无法还原。");
                }
                else
                {
                    soDal.RestoreBill(soHeaderEntity.BillID, soHeaderEntity.BillNO);
                    gvHeader.DeleteSelectedRows();
                    bindingSource1.ResetBindings(false);
                    ShowFocusedBillDetail();
                    MsgBox.OK("还原成功。");
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
    }
}