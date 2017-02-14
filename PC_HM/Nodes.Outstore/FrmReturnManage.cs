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
using Nodes.Resources;
using Nodes.Shares;
using Nodes.UI;
using System.Linq;
using DevExpress.XtraGrid.Views.Grid;
using Newtonsoft.Json;

namespace Nodes.Outstore
{
    public partial class FrmReturnManage : DevExpress.XtraEditors.XtraForm, IReturnManage
    {
        private PReturnManage myPre;
        private ReturnManageDal crnDal;
        private ReturnDal returnDal = null;
        private string warehouseCode = string.Empty;
        private List<ReturnDetailsEntity> lstDetail;

        public FrmReturnManage()
        {
            InitializeComponent();

            myPre = new PReturnManage(this);
            crnDal = new ReturnManageDal();
            returnDal = new ReturnDal();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            ImageCollection ic = AppResource.LoadToolImages();
            barManager1.Images = ic;
            toolEdit.ImageIndex = (int)AppResource.EIcons.download;
            toolRefresh.ImageIndex = (int)AppResource.EIcons.refresh;
            toolToday.ImageIndex = (int)AppResource.EIcons.today;
            toolWeek.ImageIndex = (int)AppResource.EIcons.week;
            toolSearch.ImageIndex = (int)AppResource.EIcons.search;
            toolPrint.ImageIndex = (int)AppResource.EIcons.print;
            barSubItem1.ImageIndex = (int)AppResource.EIcons.log;
            toolDeleteBill.ImageIndex = (int)AppResource.EIcons.edit;
            toolCreateCRNBill.ImageIndex = (int)AppResource.EIcons.copy;
            toolModifyAmount.ImageIndex = (int)AppResource.EIcons.design;
            toolRelatingStack.ImageIndex = (int)AppResource.EIcons.approved;
            InitDate();
            BindLookUpControl();

            //默认显示所有未完成单据
            myPre.Query(1, DateTime.Now, DateTime.Now);
        }

        /// <summary>
        /// 默认系统加载一月内的单据
        /// </summary>
        private void InitDate()
        {
            dateEditFrom.DateTime = System.DateTime.Now.AddMonths(-1);
            dateEditTo.DateTime = System.DateTime.Now;
        }

        /// <summary>
        /// 绑定页面下拉列表
        /// </summary>
        private void BindLookUpControl()
        {
            //单据状态
            listBillState.Properties.DataSource = BaseCodeDal.GetItemList(BaseCodeConstant.ASN_STATE);
        }

        #region 接口
        public void BindingGrid(List<ReturnHeaderEntity> headers)
        {
            bindingSource1.DataSource = headers;
        }

        public ReturnHeaderEntity GetFocusedBill()
        {
            return SelectedHeader;
        }

        public void ShowQueryCondition(int queryType, string billNO, string customer, string salesMan, string material, string billStatus,
            string returnDriver, string shipNo, DateTime dateFrom, DateTime dateTo)
        {
            string displayContent = string.Empty;
            string separator = " && ";
            if (queryType == 1)
                displayContent = "查询条件：“所有退货尚未完成”的出库单。";
            else if (queryType == 2)
                displayContent = "查询条件：“最近 7 天退货未完成”的出库单。";
            else
            {
                displayContent = string.Format(@"查询条件: {0}{1}{2}{3}{4}{5}{6}",
                     string.IsNullOrEmpty(billNO) ? "" : "单据号=" + billNO + separator,
                    string.IsNullOrEmpty(customer) ? "" : "客户包含'" + txtCustomer.Text.Trim() + "'" + separator,
                    string.IsNullOrEmpty(salesMan) ? "" : "业务员=" + salesMan + separator,
                    string.IsNullOrEmpty(material) ? "" : "物料包含'" + material + "'" + separator,
                    string.IsNullOrEmpty(billStatus) ? "" : "单据状态=" + listBillState.Text + separator,
                    string.IsNullOrEmpty(returnDriver) ? "" : "退货司机=" + txtDriver.Text + separator,
                  "时间范围 从 " + dateFrom.ToShortDateString() + " 至 " + dateTo.AddDays(-1).ToShortDateString());
            }

            lblQueryContent.Text = displayContent.TrimEnd('&');
            ClosePopup();
        }

        private void QueryCustom()
        {
            string billNO = txtBillNO.Text.Trim();
            string customer = ConvertUtil.StringToNull(txtCustomer.Text.Trim());
            string material = ConvertUtil.StringToNull(txtMaterial.Text.Trim());
            string salesMan = txtSalesMan.Text.Trim();
            string rtnDriver = ConvertUtil.ToString(txtDriver.EditValue);
            string billState = ConvertUtil.ToString(listBillState.EditValue);
            string shipNo = "";

            myPre.BindQueryResult(3, billNO, customer, salesMan, material, billState, rtnDriver, shipNo,
                        dateEditFrom.DateTime.Date, dateEditTo.DateTime.AddDays(1).Date);
        }

        private void ClosePopup()
        {
            popupControlContainer1.HidePopup();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ShowFocusDetail();
        }

        public void ShowFocusDetail()
        {
            ReturnHeaderEntity selectedHeader = SelectedHeader;
            if (selectedHeader == null)
            {
                gridDetails.DataSource = null;
                gvDetails.ViewCaption = "未选择单据！";
            }
            else
            {
                lstDetail = new List<ReturnDetailsEntity>();
                lstDetail = crnDal.GetReturnDetails(selectedHeader.BillID);
                gridDetails.DataSource = lstDetail;
                gvDetails.ViewCaption = string.Format("单据号: {0};  客户名称：{1}", selectedHeader.BillNo, selectedHeader.CustomerName);
            }
        }
        #endregion

        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DoClickEvent(ConvertUtil.ToString(e.Item.Tag));
        }

        private void DoClickEvent(string tag)
        {
            switch (tag)
            {
                case "刷新":
                    myPre.Requery();
                    break;
                case "所有未完成":
                    myPre.Query(1, DateTime.Now, DateTime.Now);
                    break;
                case "近一周单据":
                    myPre.Query(2, DateTime.Now.AddDays(-7).Date, DateTime.Now.AddDays(1).Date);
                    break;
                case "删除单据":
                    myPre.DeleteReturnBill();
                    break;
                case "打印退货单":
                    myPre.PrintReturnBill();
                    break;
                case "修改退货金额":
                    myPre.ModifyReturnAmount();
                    break;
                case "关联托盘记录":
                    myPre.RelatingStackInfo();
                    break;
                case "退货完成":
                    myPre.ReturnComplete();
                    break;
            }
        }

        public List<ReturnHeaderEntity> GetFocusedBills()
        {
            List<ReturnHeaderEntity> checkedBills = new List<ReturnHeaderEntity>();
            int[] focusedHandles = gvHeader.GetSelectedRows();
            foreach (int handle in focusedHandles)
            {
                if (handle >= 0)
                    checkedBills.Add(gvHeader.GetRow(handle) as ReturnHeaderEntity);
            }

            return checkedBills;
        }

        public void RefreshHeaderGrid()
        {
            bindingSource1.ResetBindings(false);
        }

        ReturnHeaderEntity SelectedHeader
        {
            get
            {
                if (gvHeader.FocusedRowHandle < 0)
                    return null;
                else
                    return gvHeader.GetFocusedRow() as ReturnHeaderEntity;
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            QueryCustom();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClosePopup();
        }

        private void OnQueryPanelKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                QueryCustom();
        }

        /// <summary>
        /// 利用RowCellStyle给特殊的行标记字体颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnHeaderRowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView vw = (sender as GridView);
            try
            {
                ReturnHeaderEntity header = vw.GetRow(e.RowHandle) as ReturnHeaderEntity;
                if (header != null)
                {
                    if (header.RowColor != null)
                    {
                        e.Appearance.ForeColor = Color.FromArgb(header.RowColor.Value);
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void btnCommit_Click(object sender, EventArgs e)
        {
            ReturnHeaderEntity selectedHeader = SelectedHeader;
            if (selectedHeader.StatusName != "等待清点")
            {
                MsgBox.Warn("只有<等待清点>状态的单据可以修改！");
                return;
            }
            ReturnDetailsEntity selectDetail = SelectedDetail;
            if (selectDetail == null)
            {
                MsgBox.Warn("没有选中的行！");
                return;
            }
            if (string.IsNullOrEmpty(txtReturnQty.Text.Trim()))
            {
                MsgBox.Warn("退货数量不能为空！");
                txtReturnQty.Focus();
                return;
            }
            if (!ConvertUtil.IsDecimal(txtReturnQty.Text.Trim()))
            {
                MsgBox.Warn("退货数量必须是数字！");
                txtReturnQty.Focus();
                return;
            }
            if (ConvertUtil.ToDecimal(txtReturnQty.Text.Trim()) < 0)
            {
                MsgBox.Warn("退货数量不能小于0！");
                txtReturnQty.Focus();
                return;
            }
            if (ConvertUtil.ToString(listReturnUnit.EditValue) == string.Empty)
            {
                MsgBox.Warn("退货单位不能为空！");
                listReturnUnit.Focus();
                return;
            }
            try
            {
                decimal minReturnQty = 0;
                if (selectDetail.ReturnUnitCode == ConvertUtil.ToString(listReturnUnit.EditValue))
                {
                    minReturnQty = ConvertUtil.ToDecimal(txtReturnQty.Text.Trim());
                }
                else
                {
                    minReturnQty = selectDetail.CastRate * ConvertUtil.ToDecimal(txtReturnQty.Text.Trim());
                }

                if (minReturnQty > selectDetail.MinPickQty - selectDetail.ReturnedQty)
                {
                    MsgBox.Warn("退货数量超出！");
                    txtReturnQty.Focus();
                    return;
                }

                selectDetail.ReturnQty = minReturnQty;

                gvDetails.RefreshData();
                btnClear_Click(null, null);
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ReturnHeaderEntity header = SelectedHeader;
                if (header == null)
                {
                    MsgBox.Warn("没有选中单据！");
                    return;
                }
                if (header.StatusName != "等待清点")
                {
                    MsgBox.Warn("只有<等待清点>状态的单据可以修改！");
                    return;
                }
                if (lstDetail.Count == 0)
                {
                    MsgBox.Warn("没有需要保存的数据！");
                    return;
                }

                if (MsgBox.AskOK("确定保存该退货明细吗？") != DialogResult.OK) return;
                int rtn = returnDal.UpdateReturnDetails(lstDetail);
                if (rtn >= 0)
                {
                    string detailStr = JsonConvert.SerializeObject(lstDetail);
                    LogDal.Insert(ELogType.退货单, GlobeSettings.LoginedUser.UserName, header.BillNo, detailStr, "退货单管理");
                    MsgBox.Warn("保存成功！");
                }
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
                return;
            }
        }

        private void gvDetails_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.RowHandle < 0) return;
            ShowFocusReturnDetails();
        }

        ReturnDetailsEntity SelectedDetail
        {
            get
            {
                if (gvDetails.FocusedRowHandle < 0)
                {
                    return null;
                }
                else
                {
                    return gvDetails.GetFocusedRow() as ReturnDetailsEntity;
                }
            }
        }

        private void GetReturnUnit(string skuCode)
        {
            try
            {
                listReturnUnit.Properties.DataSource = returnDal.GetAllUnitBySku(skuCode);
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
                return;
            }
        }

        private void ShowFocusReturnDetails()
        {
            try
            {
                ReturnDetailsEntity selectDetail = SelectedDetail;
                GetReturnUnit(selectDetail.SkuCode);

                txtReturnQty.Text = selectDetail.ReturnQty.ToString();
                listReturnUnit.EditValue = selectDetail.ReturnUnitCode;
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
                return;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtReturnQty.Text = "";
            listReturnUnit.EditValue = null;
            txtReturnQty.Focus();
        }

        private void OnLookUpEditButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            BaseEdit editor = sender as BaseEdit;
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
                editor.EditValue = null;
        }

        
    }
}