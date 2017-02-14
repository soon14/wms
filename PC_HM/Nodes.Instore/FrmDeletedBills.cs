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
using Nodes.Utils;
using DevExpress.Utils;
using Nodes.Icons;
using Nodes.Shares;

namespace Nodes.WMS.Inbound
{
    public partial class FrmDeletedBills : DevExpress.XtraEditors.XtraForm
    {
        private DeletedAsnDal asnDeletedDal;
        private string warehouseCode = string.Empty;

        public FrmDeletedBills()
        {
            InitializeComponent();

            asnDeletedDal = new DeletedAsnDal();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            ImageCollection ic = IconHelper.LoadToolImages();
            barManager1.Images = ic;
            toolConfirm.ImageIndex = (int)IconHelper.Images.add;
            toolRefresh.ImageIndex = (int)IconHelper.Images.refresh;

            warehouseCode = GlobeSettings.LoginedUser.WarehouseCode;
            BindQueryResult();
        }

        /// <summary>
        /// 绑定查询结果信息
        /// </summary>
        /// <param name="billNo">单据号</param>
        /// <param name="billStatus">单据状态</param>
        /// <param name="dateBegin">开始时间</param>
        /// <param name="dateEnd">结束时间</param>
        private void BindQueryResult()
        {
            try
            {
                List<DeletedAsnHeaderEntity> asnHeaderEntitys = asnDeletedDal.QueryBillsQuickly(warehouseCode);
                gridControl1.DataSource = asnHeaderEntitys;
                gvHeader.BestFitColumns();
                ShowFocusDetail();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        DeletedAsnHeaderEntity SelectedHeader
        {
            get
            {
                if (gvHeader.GetFocusedRowCellValue("BillNO") == null)
                    return null;
                else
                    return gvHeader.GetFocusedRow() as DeletedAsnHeaderEntity;
            }
        }

        void ShowFocusDetail()
        {
            DeletedAsnHeaderEntity selectedHeader = SelectedHeader;
            if (selectedHeader == null)
            {
                gridDetails.DataSource = null;
                gvDetails.ViewCaption = "未选择单据";
            }
            else
            {
                gridDetails.DataSource = asnDeletedDal.GetDetailsByBillID(selectedHeader.BillID);
                gvDetails.BestFitColumns(); //自动匹配列宽
                gvDetails.ViewCaption = string.Format("单据号: {0}", selectedHeader.BillNO);
            }
        }

        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DoClickEvent(ConvertUtil.ToString(e.Item.Tag));
        }

        void DoClickEvent(string tag)
        {
            switch (tag)
            {
                case "刷新":
                    BindQueryResult();
                    break;
                case "还原":
                    RestoreFocusedBill();
                    break;
            }
        }

        private void RestoreFocusedBill()
        {
            DeletedAsnHeaderEntity asnHeaderEntity = SelectedHeader;
            if (asnHeaderEntity == null)
            {
                MsgBox.Warn("请选中要还原的单据。");
                return;
            }

            if (MsgBox.AskOK(string.Format("确认要还原单据“{0}”吗？", asnHeaderEntity.BillNO)) != DialogResult.OK)
                return;

            try
            {
                //先看单据编号是否已存在
                AsnHeaderEntity header = new AsnDal().GetHeaderInfoByBillNO(GlobeSettings.LoginedUser.WarehouseCode, asnHeaderEntity.BillNO);
                if (header != null)
                {
                    MsgBox.Warn("单据编号已存在，无法还原。");
                }
                else
                {
                    asnDeletedDal.RestoreBill(asnHeaderEntity.BillID, asnHeaderEntity.BillNO);
                    gvHeader.DeleteSelectedRows();
                    bindingSource1.ResetBindings(false);
                    ShowFocusDetail();
                    MsgBox.OK("还原成功。");
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ShowFocusDetail();
        }
    }
}