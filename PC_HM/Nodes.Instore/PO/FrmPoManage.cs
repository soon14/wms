using System;
using System.Collections.Generic;
using DevExpress.Utils;
using Nodes.Controls;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Icons;
using Nodes.Utils;

namespace Nodes.Instore
{
    public partial class FrmPoManage : DevExpress.XtraEditors.XtraForm, IPOManager
    {
        PODal receiveDal = new PODal();
        PrePOManager prePOManager = null;

        public FrmPoManage()
        {
            InitializeComponent();
            prePOManager = new PrePOManager(this);
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            ImageCollection ic = IconHelper.LoadToolImages();
            barManager1.Images = ic;
            toolEdit.ImageIndex = (int)IconHelper.Images.edit;
            toolDel.ImageIndex = (int)IconHelper.Images.delete;
            toolRefresh.ImageIndex = (int)IconHelper.Images.refresh;
            toolToday.ImageIndex = (int)IconHelper.Images.today;
            toolWeek.ImageIndex = (int)IconHelper.Images.week;
            toolView.ImageIndex = (int)IconHelper.Images.log;
            toolSearch.ImageIndex = (int)IconHelper.Images.search;
            toolPrint.ImageIndex = (int)IconHelper.Images.print;
            toolCopy.ImageIndex = (int)IconHelper.Images.copy;
            toolCancelCommit.ImageIndex = (int)IconHelper.Images.remove;
            toolCommit.ImageIndex = (int)IconHelper.Images.basedata;

            ucPoBody1.CustomGridCaption();
            ucPoQueryConditionPanel1.DoQueryNotCompleteBill("所有进行中(未完成收货)的单据");
        }

        public void ReloadPO()
        {
            ucPoQueryConditionPanel1.Reload();
        }

        public void RefreshState()
        {
            ucPoBody1.RefreshMeMemory();
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
                    ReloadPO();
                    break;
                case "进行中单据":
                    ucPoQueryConditionPanel1.DoQueryNotCompleteBill("所有进行中(未完成收货)的单据");
                    break;
                case "近一周单据":
                    ucPoQueryConditionPanel1.DoQuery(DateTime.Now.AddDays(-6).Date, DateTime.Now.AddDays(1).Date, 
                        string.Format("最近一周(【{0}】-【{1}】)创建的单据", 
                        DateTime.Now.AddDays(-6).Date.ToShortDateString(), 
                        DateTime.Now.Date.ToShortDateString()));
                    break;
                case "新建":
                    using (FrmPoEdit frmNewBill = new FrmPoEdit())
                    {
                        frmNewBill.MdiParent = this.MdiParent;
                        frmNewBill.Show();
                    }
                    break;
                case "编辑":
                    DoEditOne();                    
                    break;
                case "删除":
                    prePOManager.DeleteSelectedBill(ucPoBody1.FocusedHeaders);
                    break;
                case "取消提交":
                    prePOManager.CancelCommitBill(ucPoBody1.FocusedHeaders);
                    break;
                case "提交":
                    prePOManager.CommitBill(ucPoBody1.FocusedHeaders);
                    break;
                case "新采购订单":
                    DoCopyOne();
                    break;
                case "单据日志":
                    prePOManager.ViewLog(ucPoBody1.FocusedHeader);
                    break;
                default:
                    MsgBox.OK("正在实现");
                    break;
            }
        }

        #region 查看日志、删除、提交等操作
        private void DoEditOne()
        {
            if (ucPoBody1.FocusedRowCount == 0)
            {
                MsgBox.Warn("请选中要编辑的行。");
                return;
            }

            if (ucPoBody1.FocusedRowCount > 1)
            {
                MsgBox.Warn("不支持多行操作，请选择其中一行。");
                return;
            }

            POBodyEntity focusedHeader = ucPoBody1.FocusedHeader;
            if (focusedHeader.BillState != BillStateConst.PO_STATE_CODE_DRAFT)
            {
                MsgBox.Warn(string.Format("单据“{0}”的状态不是草稿，不允许编辑。", focusedHeader.BillID));
                return;
            }

            FrmPoEdit frmEditBill = new FrmPoEdit(focusedHeader.BillID, false);
            frmEditBill.MdiParent = this.MdiParent;
            frmEditBill.Show();
        }

        public List<POBodyEntity> GetFocusedHeaders()
        {
            return ucPoBody1.FocusedHeaders;
        }

        private void DoCopyOne()
        {
            if (ucPoBody1.FocusedRowCount == 0)
            {
                MsgBox.Warn("请选中要复制的行。");
                return;
            }

            if (ucPoBody1.FocusedRowCount > 1)
            {
                MsgBox.Warn("不支持多行操作，请选择其中一行。");
                return;
            }

            FrmPoEdit frmEditBill = new FrmPoEdit(ucPoBody1.FocusedHeader.BillID, true);
            frmEditBill.MdiParent = this.MdiParent;
            frmEditBill.Show();
        }

        #endregion

        private void ucPoQueryConditionPanel1_QueryCompleted(List<POBodyEntity> dataSource)
        {
            ucPoBody1.DataSource = dataSource;
            ucPoBody1.ShowCondition(ucPoQueryConditionPanel1.QueryCondition);
            ucPoBody1.ShowTimeMsg(ucPoQueryConditionPanel1.ElapsedTime);

            popupControlContainer1.HidePopup();
        }

        private void popupControlContainer1_Popup(object sender, EventArgs e)
        {
            ucPoQueryConditionPanel1.LoadDataSource();
        }
    }
}