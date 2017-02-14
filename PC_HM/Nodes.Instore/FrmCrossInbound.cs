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
using System.Linq;

namespace Nodes.WMS.Inbound
{
    public partial class FrmCrossInbound : DevExpress.XtraEditors.XtraForm
    {
        private AsnDal asnDal;
        private PagerDal pagerDal;
        private string warehouseCode = string.Empty;
        private IBindDataSouce bindDataSouce;

        public FrmCrossInbound()
        {
            InitializeComponent();

            asnDal = new AsnDal();
            pagerDal = new PagerDal();
            bindDataSouce = new BindLookUpEditDataSouce();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            ImageCollection ic = IconHelper.LoadToolImages();
            barManager1.Images = ic;
            toolConfirm.ImageIndex = (int)IconHelper.Images.add;
            toolRefresh.ImageIndex = (int)IconHelper.Images.refresh;

            warehouseCode = GlobeSettings.LoginedUser.WarehouseCode;
            LocationDal locDal = new LocationDal();
            List<LocationEntity> locations = locDal.GetStockLocationByWarehouse(warehouseCode, SysCodeConstant.ZONE_TYPE_TEMP);
            listLocations.Properties.DataSource = locations;

            //默认选中第一行
            if (locations.Count > 0)
                listLocations.EditValue = locations[0].LocationCode;

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
                List<AsnHeaderEntity> asnHeaderEntitys = asnDal.QueryBillsQuickly(warehouseCode, SysCodeConstant.INBOUND_TYPE_ACROSS, null, null);
                gridControl1.DataSource = asnHeaderEntitys;
                lblQueryContent.Text = "过滤条件：所有未完成收货的越库单据。";
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        AsnHeaderEntity SelectedHeader
        {
            get
            {
                if (gvHeader.GetFocusedRowCellValue("BillNO") == null)
                    return null;
                else
                    return gvHeader.GetFocusedRow() as AsnHeaderEntity;
            }
        }

        void ShowFocusDetail()
        {
            AsnHeaderEntity selectedHeader = SelectedHeader;
            if (selectedHeader == null)
            {
                gridDetails.DataSource = null;
                lblBillNO.Text = "未选择单据";
            }
            else
            {
                gridDetails.DataSource = asnDal.GetDetailsByBillID(selectedHeader.BillID);
                lblBillNO.Text = selectedHeader.BillNO;
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
                case "确认入库":
                    ConfirmInbound();
                    break;
            }
        }

        bool CanSave()
        {
            gvDetails.PostEditor();

            List<AsnDetailEntity> details = gvDetails.DataSource as List<AsnDetailEntity>;
            if (details.Find(d => d.PutawayQty <= 0) != null)
            {
                MsgBox.Warn("实收数量必须为大于0的值，请重新填写数量后再保存。");
                return false;
            }

            if (details.Find(d => d.PutawayQty > d.Qty) != null)
            {
                MsgBox.Warn("实收数量不能大于订购量，请重新填写数量后再保存。");
                return false;
            }

            if (listLocations.EditValue == null)
            {
                MsgBox.Warn("请选择要收货的货位。");
                return false;
            }

            return true;
        }

        private void ConfirmInbound()
        {
            AsnHeaderEntity asnHeaderEntity = SelectedHeader;
            if (asnHeaderEntity == null)
            {
                MsgBox.Warn("请选择要确认入库的单据。");
                return;
            }

            if (asnHeaderEntity.Status != SysCodeConstant.ASN_STATUS_AWAIT_CHECK)
            {
                MsgBox.Warn("只有待验收状态的【越库单据】才能执行【确认入库】操作。");
                return;
            }

            if (!CanSave())
                return;

            //FrmChooseAcrossLocation frmLocation = new FrmChooseAcrossLocation();
            //if (DialogResult.OK == frmLocation.ShowDialog())
            //{
                try
                {
                    List<AsnDetailEntity> details = gvDetails.DataSource as List<AsnDetailEntity>;
                    int qty = details.Sum(d => d.Qty);
                    int putQty = details.Sum(d => d.PutawayQty.Value);
                    if (MsgBox.AskOK(string.Format("应收数量为“{0}”，实收数量为“{1}”，确定要入库吗？", qty, putQty)) != DialogResult.OK)
                        return;

                    string errMsg = asnDal.ExecuteCrossInstore(asnHeaderEntity.BillID, listLocations.Text, details, GlobeSettings.LoginedUser.UserName);
                    if (string.IsNullOrEmpty(errMsg))
                    {
                        MsgBox.OK("越库收货成功，单据将从下面的表格中移除。");
                        gvHeader.DeleteSelectedRows();
                        ShowFocusDetail();
                    }
                    else
                    {
                        MsgBox.Warn(errMsg);
                    }
                }
                catch (Exception ex)
                {
                    MsgBox.Err(ex.Message);
                }
            //}
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ShowFocusDetail();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            List<AsnDetailEntity> details = gvDetails.DataSource as List<AsnDetailEntity>;
            if (details != null)
            {
                details.ForEach(d => d.PutawayQty = d.Qty);
                gvDetails.RefreshData();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            ConfirmInbound();
        }
    }
}