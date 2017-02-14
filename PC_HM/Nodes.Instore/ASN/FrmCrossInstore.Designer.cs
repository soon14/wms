namespace Nodes.Instore
{
    partial class FrmCrossInstore
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition2 = new DevExpress.XtraGrid.StyleFormatCondition();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.toolRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.toolAdd = new DevExpress.XtraBars.BarButtonItem();
            this.toolEdit = new DevExpress.XtraBars.BarButtonItem();
            this.toolDel = new DevExpress.XtraBars.BarButtonItem();
            this.toolSave = new DevExpress.XtraBars.BarButtonItem();
            this.toolSearch = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.gdHeader = new DevExpress.XtraGrid.GridControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gvHeader = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.gdDetails = new DevExpress.XtraGrid.GridControl();
            this.gvDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.AllowCustomization = false;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.toolRefresh,
            this.toolAdd,
            this.toolEdit,
            this.toolDel,
            this.toolSearch,
            this.barButtonItem1,
            this.barButtonItem2,
            this.toolSave});
            this.barManager1.MaxItemId = 11;
            // 
            // bar1
            // 
            this.bar1.BarName = "工具";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.toolRefresh),
            new DevExpress.XtraBars.LinkPersistInfo(this.toolAdd, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.toolEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.toolDel),
            new DevExpress.XtraBars.LinkPersistInfo(this.toolSave),
            new DevExpress.XtraBars.LinkPersistInfo(this.toolSearch, true)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableCustomization = true;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "工具";
            // 
            // toolRefresh
            // 
            this.toolRefresh.Caption = "刷新";
            this.toolRefresh.Id = 0;
            this.toolRefresh.Name = "toolRefresh";
            this.toolRefresh.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.toolRefresh.Tag = "刷新";
            this.toolRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // toolAdd
            // 
            this.toolAdd.Caption = "新增";
            this.toolAdd.Id = 1;
            this.toolAdd.Name = "toolAdd";
            this.toolAdd.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.toolAdd.Tag = "新增";
            this.toolAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // toolEdit
            // 
            this.toolEdit.Caption = "修改";
            this.toolEdit.Id = 2;
            this.toolEdit.Name = "toolEdit";
            this.toolEdit.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.toolEdit.Tag = "修改";
            this.toolEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // toolDel
            // 
            this.toolDel.Caption = "删除";
            this.toolDel.Id = 3;
            this.toolDel.Name = "toolDel";
            this.toolDel.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.toolDel.Tag = "删除";
            this.toolDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // toolSave
            // 
            this.toolSave.Caption = "确认越库";
            this.toolSave.Id = 10;
            this.toolSave.Name = "toolSave";
            this.toolSave.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.toolSave.Tag = "确认越库";
            this.toolSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // toolSearch
            // 
            this.toolSearch.Caption = "快速查找";
            this.toolSearch.Id = 4;
            this.toolSearch.Name = "toolSearch";
            this.toolSearch.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.toolSearch.Tag = "快速查找";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(774, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 419);
            this.barDockControlBottom.Size = new System.Drawing.Size(774, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 388);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(774, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 388);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Id = 9;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "修改";
            this.barButtonItem2.Id = 8;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // gdHeader
            // 
            this.gdHeader.DataSource = this.bindingSource1;
            this.gdHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.gdHeader.Location = new System.Drawing.Point(0, 31);
            this.gdHeader.MainView = this.gvHeader;
            this.gdHeader.Name = "gdHeader";
            this.gdHeader.Size = new System.Drawing.Size(774, 167);
            this.gdHeader.TabIndex = 23;
            this.gdHeader.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvHeader});
            // 
            // gvHeader
            // 
            this.gvHeader.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn7,
            this.gridColumn4,
            this.gridColumn2,
            this.gridColumn6,
            this.gridColumn3,
            this.gridColumn5,
            this.gridColumn21,
            this.gridColumn20,
            this.gridColumn19,
            this.gridColumn1,
            this.gridColumn10});
            this.gvHeader.GridControl = this.gdHeader;
            this.gvHeader.ID = "4e1bbad9-8fa9-41e1-bab0-2ea207b59597";
            this.gvHeader.Name = "gvHeader";
            this.gvHeader.OptionsDetail.EnableMasterViewMode = false;
            this.gvHeader.OptionsSelection.MultiSelect = true;
            this.gvHeader.OptionsView.ColumnAutoWidth = false;
            this.gvHeader.OptionsView.EnableAppearanceOddRow = true;
            this.gvHeader.OptionsView.ShowAutoFilterRow = true;
            this.gvHeader.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.OnHeadFocusedRowChanged);
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "入库单号";
            this.gridColumn7.FieldName = "BillNO";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 0;
            this.gridColumn7.Width = 99;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "供应商";
            this.gridColumn4.FieldName = "SupplierName";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            this.gridColumn4.Width = 152;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "单据状态";
            this.gridColumn2.FieldName = "BillStateDesc";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 80;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "业务类型";
            this.gridColumn6.FieldName = "BillTypeDesc";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 3;
            this.gridColumn6.Width = 78;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "业务员";
            this.gridColumn3.FieldName = "Sales";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 5;
            this.gridColumn3.Width = 61;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "原始单号";
            this.gridColumn5.FieldName = "OriginalBillNO";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 6;
            this.gridColumn5.Width = 110;
            // 
            // gridColumn21
            // 
            this.gridColumn21.Caption = "打印";
            this.gridColumn21.FieldName = "HasPrinted";
            this.gridColumn21.Name = "gridColumn21";
            // 
            // gridColumn20
            // 
            this.gridColumn20.Caption = "建单日期";
            this.gridColumn20.DisplayFormat.FormatString = "g";
            this.gridColumn20.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn20.FieldName = "CreateDate";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 7;
            this.gridColumn20.Width = 110;
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "建单人";
            this.gridColumn19.FieldName = "Creator";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 8;
            this.gridColumn19.Width = 73;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "WMS备注";
            this.gridColumn1.FieldName = "WmsRemark";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 9;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "入库方式";
            this.gridColumn10.FieldName = "InstoreTypeDesc";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 4;
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterControl1.Location = new System.Drawing.Point(0, 198);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(774, 5);
            this.splitterControl1.TabIndex = 24;
            this.splitterControl1.TabStop = false;
            // 
            // gdDetails
            // 
            this.gdDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gdDetails.Location = new System.Drawing.Point(0, 203);
            this.gdDetails.MainView = this.gvDetails;
            this.gdDetails.Name = "gdDetails";
            this.gdDetails.Size = new System.Drawing.Size(774, 216);
            this.gdDetails.TabIndex = 26;
            this.gdDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDetails});
            // 
            // gvDetails
            // 
            this.gvDetails.Appearance.ViewCaption.Options.UseTextOptions = true;
            this.gvDetails.Appearance.ViewCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gvDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn8,
            this.gridColumn13,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn17,
            this.gridColumn18,
            this.gridColumn9});
            styleFormatCondition2.Appearance.ForeColor = System.Drawing.Color.Red;
            styleFormatCondition2.Appearance.Options.UseForeColor = true;
            styleFormatCondition2.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
            styleFormatCondition2.Expression = "[PlanQty] != [RealQty]";
            this.gvDetails.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition2});
            this.gvDetails.GridControl = this.gdDetails;
            this.gvDetails.ID = "b677e7e9-b1be-49c5-9150-0b570f778b86";
            this.gvDetails.Name = "gvDetails";
            this.gvDetails.OptionsBehavior.Editable = true;
            this.gvDetails.OptionsSelection.MultiSelect = true;
            this.gvDetails.OptionsView.ColumnAutoWidth = false;
            this.gvDetails.OptionsView.EnableAppearanceOddRow = true;
            this.gvDetails.OptionsView.ShowFooter = true;
            this.gvDetails.OptionsView.ShowViewCaption = true;
            this.gvDetails.ViewCaption = "未选择单据";
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "物料编码";
            this.gridColumn11.FieldName = "MaterialCode";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 0;
            this.gridColumn11.Width = 72;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "物料名称";
            this.gridColumn12.FieldName = "MaterialName";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 1;
            this.gridColumn12.Width = 209;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "商品条码";
            this.gridColumn8.FieldName = "Barcode1";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 2;
            this.gridColumn8.Width = 120;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "规格型号";
            this.gridColumn13.FieldName = "Spec";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Width = 93;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "销售单位";
            this.gridColumn15.FieldName = "UnitName";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Width = 84;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "计划量";
            this.gridColumn16.FieldName = "PlanQty";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 3;
            this.gridColumn16.Width = 85;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "单价";
            this.gridColumn17.DisplayFormat.FormatString = "0.00";
            this.gridColumn17.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn17.FieldName = "Price";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 4;
            this.gridColumn17.Width = 70;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "金额合计";
            this.gridColumn18.DisplayFormat.FormatString = "0.00";
            this.gridColumn18.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn18.FieldName = "PlanAmount";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PlanAmount", "{0:N2}")});
            this.gridColumn18.Width = 85;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "越库数量";
            this.gridColumn9.FieldName = "PutQty";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = true;
            this.gridColumn9.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 5;
            // 
            // FrmCrossInstore
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(774, 419);
            this.Controls.Add(this.gdDetails);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.gdHeader);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmCrossInstore";
            this.Text = "越库收货";
            this.Load += new System.EventHandler(this.OnFrmLoad);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem toolRefresh;
        private DevExpress.XtraBars.BarButtonItem toolAdd;
        private DevExpress.XtraBars.BarButtonItem toolEdit;
        private DevExpress.XtraBars.BarButtonItem toolDel;
        private DevExpress.XtraBars.BarButtonItem toolSearch;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraGrid.GridControl gdHeader;
        private DevExpress.XtraGrid.Views.Grid.GridView gvHeader;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.GridControl gdDetails;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDetails;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraBars.BarButtonItem toolSave;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
    }
}