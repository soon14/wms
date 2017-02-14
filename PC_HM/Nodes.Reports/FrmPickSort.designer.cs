namespace Reports
{
    partial class FrmPickSort
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
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.textUserCode = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.dateStart = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.dateEnd = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemDateEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.buttonQuery = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridHeader = new DevExpress.XtraGrid.GridControl();
            this.gvHeader = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridDetails = new DevExpress.XtraGrid.GridControl();
            this.gvDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "整散标记";
            this.gridColumn19.FieldName = "IsCaseName";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 7;
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
            this.barButtonItem1,
            this.dateStart,
            this.dateEnd,
            this.buttonQuery,
            this.textUserCode});
            this.barManager1.MaxItemId = 7;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemDateEdit1,
            this.repositoryItemDateEdit2,
            this.repositoryItemTextEdit1});
            // 
            // bar1
            // 
            this.bar1.BarName = "工具";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.textUserCode, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.dateStart),
            new DevExpress.XtraBars.LinkPersistInfo(this.dateEnd),
            new DevExpress.XtraBars.LinkPersistInfo(this.buttonQuery)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableCustomization = true;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "工具";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "刷新";
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem1.Tag = "刷新";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonQuery_ItemClick);
            // 
            // textUserCode
            // 
            this.textUserCode.Caption = "账号";
            this.textUserCode.Edit = this.repositoryItemTextEdit1;
            this.textUserCode.Id = 6;
            this.textUserCode.Name = "textUserCode";
            this.textUserCode.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.Caption;
            this.textUserCode.Width = 107;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // dateStart
            // 
            this.dateStart.Caption = "开始日期：";
            this.dateStart.Edit = this.repositoryItemDateEdit1;
            this.dateStart.Id = 3;
            this.dateStart.Name = "dateStart";
            this.dateStart.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.dateStart.Tag = "开始日期";
            this.dateStart.Width = 170;
            // 
            // repositoryItemDateEdit1
            // 
            this.repositoryItemDateEdit1.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.repositoryItemDateEdit1.AutoHeight = false;
            this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.repositoryItemDateEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDateEdit1.EditFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.repositoryItemDateEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDateEdit1.Mask.EditMask = "yyyy-MM-dd HH:mm";
            this.repositoryItemDateEdit1.MaxValue = new System.DateTime(2100, 5, 1, 0, 0, 0, 0);
            this.repositoryItemDateEdit1.MinValue = new System.DateTime(2015, 5, 1, 0, 0, 0, 0);
            this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            this.repositoryItemDateEdit1.ShowWeekNumbers = true;
            this.repositoryItemDateEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.repositoryItemDateEdit1.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
            this.repositoryItemDateEdit1.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // dateEnd
            // 
            this.dateEnd.Caption = "结束日期：";
            this.dateEnd.Edit = this.repositoryItemDateEdit2;
            this.dateEnd.Id = 4;
            this.dateEnd.Name = "dateEnd";
            this.dateEnd.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.dateEnd.Tag = "结束日期";
            this.dateEnd.Width = 170;
            // 
            // repositoryItemDateEdit2
            // 
            this.repositoryItemDateEdit2.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.repositoryItemDateEdit2.AutoHeight = false;
            this.repositoryItemDateEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit2.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.repositoryItemDateEdit2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDateEdit2.EditFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.repositoryItemDateEdit2.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDateEdit2.Mask.EditMask = "yyyy-MM-dd HH:mm";
            this.repositoryItemDateEdit2.Name = "repositoryItemDateEdit2";
            this.repositoryItemDateEdit2.ShowWeekNumbers = true;
            this.repositoryItemDateEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.repositoryItemDateEdit2.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
            this.repositoryItemDateEdit2.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // buttonQuery
            // 
            this.buttonQuery.Caption = "查询";
            this.buttonQuery.Id = 5;
            this.buttonQuery.Name = "buttonQuery";
            this.buttonQuery.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.buttonQuery.Tag = "查询";
            this.buttonQuery.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonQuery_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(739, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 384);
            this.barDockControlBottom.Size = new System.Drawing.Size(739, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 353);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(739, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 353);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 31);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gridHeader);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gridDetails);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(739, 353);
            this.splitContainerControl1.SplitterPosition = 206;
            this.splitContainerControl1.TabIndex = 5;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // gridHeader
            // 
            this.gridHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridHeader.Location = new System.Drawing.Point(0, 0);
            this.gridHeader.MainView = this.gvHeader;
            this.gridHeader.MenuManager = this.barManager1;
            this.gridHeader.Name = "gridHeader";
            this.gridHeader.Size = new System.Drawing.Size(739, 142);
            this.gridHeader.TabIndex = 13;
            this.gridHeader.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvHeader});
            // 
            // gvHeader
            // 
            this.gvHeader.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn18});
            this.gvHeader.GridControl = this.gridHeader;
            this.gvHeader.GroupCount = 1;
            this.gvHeader.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "BILL_NO", null, "订单合计：{0:f0} 单"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "W_QTY", null, "整货件数：{0:f0} 箱"),
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "B_QTY", null, "物流箱数：{0:f0} 箱")});
            this.gvHeader.ID = "b3b22d56-7bb9-41ce-afd8-5a3497edf08c";
            this.gvHeader.Name = "gvHeader";
            this.gvHeader.OptionsSelection.MultiSelect = true;
            this.gvHeader.OptionsView.ColumnAutoWidth = false;
            this.gvHeader.OptionsView.EnableAppearanceOddRow = true;
            this.gvHeader.OptionsView.ShowAutoFilterRow = true;
            this.gvHeader.OptionsView.ShowFooter = true;
            this.gvHeader.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn2, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvHeader.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "单据ID";
            this.gridColumn3.FieldName = "BILL_ID";
            this.gridColumn3.Name = "gridColumn3";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "单据号";
            this.gridColumn1.FieldName = "BILL_NO";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 200;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "拣货员";
            this.gridColumn2.FieldName = "USER_NAME";
            this.gridColumn2.Name = "gridColumn2";
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "客户";
            this.gridColumn11.FieldName = "C_NAME";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 1;
            this.gridColumn11.Width = 150;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "客户地址";
            this.gridColumn12.FieldName = "ADDRESS";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 2;
            this.gridColumn12.Width = 220;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "联系人";
            this.gridColumn13.FieldName = "CONTACT";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 3;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "开始时间";
            this.gridColumn14.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.gridColumn14.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn14.FieldName = "BEGIN_DATE";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 4;
            this.gridColumn14.Width = 140;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "结束时间";
            this.gridColumn15.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.gridColumn15.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn15.FieldName = "END_DATE";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 5;
            this.gridColumn15.Width = 140;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "整货件数";
            this.gridColumn16.DisplayFormat.FormatString = "f0";
            this.gridColumn16.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn16.FieldName = "W_QTY";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "QTY", "总量：{0:f0}")});
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 6;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "物流箱数";
            this.gridColumn18.DisplayFormat.FormatString = "f0";
            this.gridColumn18.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn18.FieldName = "B_QTY";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 7;
            // 
            // gridDetails
            // 
            this.gridDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDetails.Location = new System.Drawing.Point(0, 0);
            this.gridDetails.MainView = this.gvDetails;
            this.gridDetails.Name = "gridDetails";
            this.gridDetails.Size = new System.Drawing.Size(739, 206);
            this.gridDetails.TabIndex = 18;
            this.gridDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDetails});
            // 
            // gvDetails
            // 
            this.gvDetails.Appearance.ViewCaption.Options.UseTextOptions = true;
            this.gvDetails.Appearance.ViewCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gvDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn23,
            this.gridColumn4,
            this.gridColumn7,
            this.gridColumn10,
            this.gridColumn8,
            this.gridColumn19,
            this.gridColumn17,
            this.gridColumn9});
            styleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.Red;
            styleFormatCondition1.Appearance.Options.UseForeColor = true;
            styleFormatCondition1.Column = this.gridColumn19;
            styleFormatCondition1.Expression = "[IsCaseName] == \'散\'";
            this.gvDetails.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1});
            this.gvDetails.GridControl = this.gridDetails;
            this.gvDetails.ID = "01afa348-ae50-4c5d-bfcf-2939db669391";
            this.gvDetails.Name = "gvDetails";
            this.gvDetails.OptionsView.ColumnAutoWidth = false;
            this.gvDetails.OptionsView.EnableAppearanceOddRow = true;
            this.gvDetails.OptionsView.ShowAutoFilterRow = true;
            this.gvDetails.OptionsView.ShowFooter = true;
            this.gvDetails.OptionsView.ShowViewCaption = true;
            this.gvDetails.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn6, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvDetails.ViewCaption = "未选择单据";
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "物料编码";
            this.gridColumn5.FieldName = "MaterialCode";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "MaterialCode", "{0:f0}")});
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 0;
            this.gridColumn5.Width = 80;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "名称";
            this.gridColumn6.FieldName = "MaterialName";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 1;
            this.gridColumn6.Width = 180;
            // 
            // gridColumn23
            // 
            this.gridColumn23.Caption = "套餐名称";
            this.gridColumn23.FieldName = "CombMaterial";
            this.gridColumn23.Name = "gridColumn23";
            this.gridColumn23.Visible = true;
            this.gridColumn23.VisibleIndex = 2;
            this.gridColumn23.Width = 80;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "条码";
            this.gridColumn4.FieldName = "SkuBarcode";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 120;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "计量单位";
            this.gridColumn7.FieldName = "UnitName";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            this.gridColumn7.Width = 57;
            // 
            // gridColumn10
            // 
            this.gridColumn10.AppearanceCell.ForeColor = System.Drawing.Color.Red;
            this.gridColumn10.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn10.AppearanceHeader.ForeColor = System.Drawing.Color.Red;
            this.gridColumn10.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn10.Caption = "订购数量";
            this.gridColumn10.DisplayFormat.FormatString = "f0";
            this.gridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn10.FieldName = "Qty";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Qty", "{0:f0}")});
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 5;
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceCell.ForeColor = System.Drawing.Color.Green;
            this.gridColumn8.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn8.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.gridColumn8.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn8.Caption = "拣货数量";
            this.gridColumn8.DisplayFormat.FormatString = "f0";
            this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn8.FieldName = "PickQty";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PickQty", "{0:f0}")});
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 4;
            this.gridColumn8.Width = 69;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "单价";
            this.gridColumn17.DisplayFormat.FormatString = "f";
            this.gridColumn17.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn17.FieldName = "Price";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 8;
            this.gridColumn17.Width = 56;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "金额";
            this.gridColumn9.DisplayFormat.FormatString = "f2";
            this.gridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn9.FieldName = "TotalAmount";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalAmount", "{0:f2}")});
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 9;
            this.gridColumn9.Width = 100;
            // 
            // FrmPickSort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 384);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmPickSort";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "拣货员任务统计";
            this.Load += new System.EventHandler(this.FrmPickSort_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarEditItem dateStart;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
        private DevExpress.XtraBars.BarEditItem dateEnd;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit2;
        private DevExpress.XtraBars.BarButtonItem buttonQuery;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraBars.BarEditItem textUserCode;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraGrid.GridControl gridHeader;
        private DevExpress.XtraGrid.Views.Grid.GridView gvHeader;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.GridControl gridDetails;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDetails;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn23;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
    }
}