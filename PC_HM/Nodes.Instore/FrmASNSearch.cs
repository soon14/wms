using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Icons;
using Nodes.Utils;

namespace Nodes.WMS.Inbound
{
    public partial class FrmASNSearch : DevExpress.XtraEditors.XtraForm, IAsnManage
    {
        private PAsnManage myPre;

        private AsnDal asnDal;
        private CodeItemDal codeItemDal;
        private IBindDataSouce bindDataSouce;

        public FrmASNSearch()
        {
            InitializeComponent();

            myPre = new PAsnManage(this);
            asnDal = new AsnDal();
            codeItemDal = new CodeItemDal();
            bindDataSouce = new BindLookUpEditDataSouce();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            ImageCollection ic = IconHelper.LoadToolImages();
            barManager1.Images = ic;
            toolAsynFromSAP.ImageIndex = (int)IconHelper.Images.download;
            toolRefresh.ImageIndex = (int)IconHelper.Images.refresh;
            barStaticItem2.ImageIndex = (int)IconHelper.Images.log;
            barStaticItem1.ImageIndex = (int)IconHelper.Images.search;
            barStaticItem3.ImageIndex = (int)IconHelper.Images.ok;
            barStaticItem4.ImageIndex = (int)IconHelper.Images.print;

            InitDate();
            BindLookUpControl();
            BindingSuppliers();
            myPre.Query(1, DateTime.Now, DateTime.Now);
        }

        #region 绑定界面列表控件，初始化数据
        /// <summary>
        /// 默认系统加载一月内的单据
        /// </summary>
        private void InitDate()
        {
            dateEditFrom.DateTime = System.DateTime.Now.AddMonths(-1);
            dateEditTo.DateTime = System.DateTime.Now;
        }

        void BindingSuppliers()
        {
            List<SupplierEntity> suppliers = new SupplierDal().ListActiveSupplierByPriority();
            listSupplers.Properties.DataSource = suppliers;
        }

        /// <summary>
        /// 绑定页面下拉列表
        /// </summary>
        private void BindLookUpControl()
        {
            //单据状态
            List<CodeItemEntity> entityLists = codeItemDal.GetCodeItemByCodeSetCode(SysCodeConstant.ASN_STATUS);
            listBillState.Properties.DisplayMember = "Name";
            listBillState.Properties.ValueMember = "Code";
            listBillState.Properties.DataSource = entityLists;

            //单据类型
            bindDataSouce.BindCommonLookUpEdit(lookUpEditBillType, SysCodeConstant.INBOUND_ASN_TYPE);

            //收货策略
            bindDataSouce.BindCommonLookUpEdit(lookUpEditStrategeType, SysCodeConstant.INBOUND_ASN_STRATEGY);
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
                    refresh();
                    break;
                case "修改入库方式":
                    myPre.ChangeInboundStyle();
                    break;
                case "所有未完成":
                    myPre.Query(1, DateTime.Now, DateTime.Now);
                    break;
                case "近一周单据":
                    myPre.Query(2, DateTime.Now.AddDays(-7).Date, DateTime.Now.AddDays(1).Date);
                    break;
                case "打印标签":
                    myPre.PrintLabel();
                    break;
                case "到货确认":
                    myPre.DoASNArrived();
                    break;
                case "删除单据":
                    myPre.DeleteSelectBills();
                    break;
                case "完成收货":
                    myPre.DoCloseBill();
                    break;
                case "状态日志":
                    myPre.ShowBillLog();
                    break;
                case "入库明细":
                    myPre.ShowInboundDetails();
                    break;
                case "入库汇总":
                    myPre.ShowInboundSummary();
                    break;
                case "打印通知单":
                    myPre.PrintAsn();
                    break;
                case "修改备注":
                    myPre.WriteWMSRemark();                    
                    break;
                case "设计入库单报表":
                    myPre.OpenAsnReport();
                    break;
                case "原始单据":
                    myPre.ShowSapBill();
                    break;
            }
        }

        #region 接口实现
        public List<AsnHeaderEntity> GetFocusedBills()
        {
            List<AsnHeaderEntity> checkedBills = new List<AsnHeaderEntity>();
            int[] focusedHandles = gvHeader.GetSelectedRows();
            foreach (int handle in focusedHandles)
            {
                if (handle >= 0)
                    checkedBills.Add(gvHeader.GetRow(handle) as AsnHeaderEntity);
            }

            return checkedBills;
        }

        AsnHeaderEntity SelectedHeader
        {
            get
            {
                if (gvHeader.FocusedRowHandle < 0)
                    return null;
                else
                    return gvHeader.GetFocusedRow() as AsnHeaderEntity;
            }
        }

        /// <summary>
        /// 从列表中移除
        /// </summary>
        /// <param name="header"></param>
        public void RemoveBill(AsnHeaderEntity header)
        {
            bindingSource1.Remove(header);
        }

        public void RefreshHeaderGrid()
        {
            bindingSource1.ResetBindings(false);
        }

        public AsnHeaderEntity GetFocusedBill()
        {
            return SelectedHeader;
        }

        public void BindingGrid(List<AsnHeaderEntity> headers)
        {
            bindingSource1.DataSource = headers;
        }

        public void ShowQueryCondition(int queryType, string billNO, string supplier, string salesMan, string billType, string billStatus,
            string inboundType, DateTime dateFrom, DateTime dateTo)
        {
            string displayContent = string.Empty;
            string separator = " && ";
            if (queryType == 1)
                displayContent = "查询条件：“所有收货尚未完成”的到货通知单。";
            else if (queryType == 2)
                displayContent = "查询条件：“最近 7 天收货未完成”的到货通知单。";
            else
            {
                displayContent = string.Format(@"查询条件: {0}{1}{2}{3}{4}{5}{6}",
                     string.IsNullOrEmpty(billNO) ? "" : "单据号=" + billNO + separator,
                    string.IsNullOrEmpty(supplier) ? "" : "供应商=" + listSupplers.Text + separator,
                    string.IsNullOrEmpty(salesMan) ? "" : "业务员=" + salesMan + separator,
                    string.IsNullOrEmpty(billType) ? "" : "单据类型=" + lookUpEditBillType.Text + separator,
                    string.IsNullOrEmpty(billStatus) ? "" : "单据状态=" + listBillState.Text + separator,
                    string.IsNullOrEmpty(inboundType) ? "" : "收货方式=" + lookUpEditStrategeType.Text + separator,
                  "时间范围 从 " + dateFrom.ToShortDateString() + " 至 " + dateTo.AddDays(-1).ToShortDateString());
            }

            lblQueryContent.Text = displayContent.TrimEnd('&');
            ClosePopup();
        }
        #endregion

        #region 表头选中事件处理
        private void OnGridHeaderFocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ShowFocusDetail();
        }

        public void ShowFocusDetail()
        {
            AsnHeaderEntity selectedHeader = SelectedHeader;
            if (selectedHeader == null)
            {
                gridDetails.DataSource = null;
                gvDetails.ViewCaption = "未选择单据";
            }
            else
            {
                gridDetails.DataSource = asnDal.GetDetailsByBillID(selectedHeader.BillID);
                gvDetails.ViewCaption = string.Format("单据号: {0}", selectedHeader.BillNO);
            }
        }

        #endregion

        #region 处理查询       

        private void btnQuery_Click(object sender, EventArgs e)
        {
            QueryCustom();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClosePopup();
        }

        private void ClosePopup()
        {
            popupControlContainer1.HidePopup();
        }

        private void QueryCustom()
        {
            string billNO = txtBillNo.Text.Trim();
            string supplier = ConvertUtil.ToString(listSupplers.EditValue);
            string salesMan = txtSalesMan.Text.Trim();
            string billType = ConvertUtil.ToString(lookUpEditBillType.EditValue);
            string billState = ConvertUtil.ToString(listBillState.EditValue);
            string inboundType = ConvertUtil.ToString(lookUpEditStrategeType.EditValue);

            myPre.BindQueryResult(3, billNO, supplier, salesMan, billType, billState, inboundType,
                        dateEditFrom.DateTime.Date,
                        dateEditTo.DateTime.AddDays(1).Date);
        }

        /// <summary>
        /// 刷新
        /// </summary>
        private void refresh()
        {
            myPre.Requery();
        }
        #endregion

        private void OnLookupEditDeleteButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            LookUpEdit contrl = sender as LookUpEdit;
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
                contrl.EditValue = null;
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
                AsnHeaderEntity header = vw.GetRow(e.RowHandle) as AsnHeaderEntity;
                if (header != null)
                {
                    if (header.RowBackgroundColor != null)
                    {
                        e.Appearance.ForeColor = Color.FromArgb(header.RowBackgroundColor.Value);
                    }
                }
            }
            catch (Exception ex) { }
        }
    }
}