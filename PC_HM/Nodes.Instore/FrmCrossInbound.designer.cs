namespace Nodes.WMS.Inbound
{
    partial class FrmCrossInbound
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
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.toolRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.toolConfirm = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gvHeader = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn40 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn44 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn49 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn50 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn51 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn53 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn54 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn57 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn58 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblQueryContent = new DevExpress.XtraEditors.LabelControl();
            this.gridDetails = new DevExpress.XtraGrid.GridControl();
            this.gvDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.listLocations = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lblBillNO = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listLocations.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.toolRefresh,
            this.toolConfirm});
            this.barManager1.MaxItemId = 30;
            // 
            // bar1
            // 
            this.bar1.BarName = "工具";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.toolRefresh),
            new DevExpress.XtraBars.LinkPersistInfo(this.toolConfirm)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "工具";
            // 
            // toolRefresh
            // 
            this.toolRefresh.Caption = "刷新(&R)";
            this.toolRefresh.Id = 0;
            this.toolRefresh.Name = "toolRefresh";
            this.toolRefresh.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.toolRefresh.Tag = "刷新";
            this.toolRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // toolConfirm
            // 
            this.toolConfirm.Caption = "确认入库";
            this.toolConfirm.Id = 1;
            this.toolConfirm.Name = "toolConfirm";
            this.toolConfirm.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.toolConfirm.Tag = "确认入库";
            this.toolConfirm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(990, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 518);
            this.barDockControlBottom.Size = new System.Drawing.Size(990, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 487);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(990, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 487);
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bindingSource1;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 2);
            this.gridControl1.MainView = this.gvHeader;
            this.gridControl1.MenuManager = this.barManager1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(986, 254);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvHeader});
            // 
            // gvHeader
            // 
            this.gvHeader.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn40,
            this.gridColumn44,
            this.gridColumn49,
            this.gridColumn50,
            this.gridColumn51,
            this.gridColumn53,
            this.gridColumn54,
            this.gridColumn57,
            this.gridColumn58});
            this.gvHeader.GridControl = this.gridControl1;
            this.gvHeader.ID = "16cfff43-417c-4334-8a2e-f4d511518d31";
            this.gvHeader.Name = "gvHeader";
            this.gvHeader.OptionsSelection.MultiSelect = true;
            this.gvHeader.OptionsView.ColumnAutoWidth = false;
            this.gvHeader.OptionsView.EnableAppearanceOddRow = true;
            this.gvHeader.OptionsView.ShowAutoFilterRow = true;
            this.gvHeader.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            // 
            // gridColumn40
            // 
            this.gridColumn40.Caption = "单据号";
            this.gridColumn40.FieldName = "BillNO";
            this.gridColumn40.Name = "gridColumn40";
            this.gridColumn40.Visible = true;
            this.gridColumn40.VisibleIndex = 0;
            // 
            // gridColumn44
            // 
            this.gridColumn44.Caption = "单据类型";
            this.gridColumn44.FieldName = "AsnTypeName";
            this.gridColumn44.Name = "gridColumn44";
            this.gridColumn44.Visible = true;
            this.gridColumn44.VisibleIndex = 1;
            this.gridColumn44.Width = 81;
            // 
            // gridColumn49
            // 
            this.gridColumn49.Caption = "业务员";
            this.gridColumn49.FieldName = "SalesMan";
            this.gridColumn49.Name = "gridColumn49";
            this.gridColumn49.Visible = true;
            this.gridColumn49.VisibleIndex = 5;
            // 
            // gridColumn50
            // 
            this.gridColumn50.Caption = "合同号";
            this.gridColumn50.FieldName = "ContractNO";
            this.gridColumn50.Name = "gridColumn50";
            this.gridColumn50.Visible = true;
            this.gridColumn50.VisibleIndex = 3;
            // 
            // gridColumn51
            // 
            this.gridColumn51.Caption = "到货日期";
            this.gridColumn51.FieldName = "ArriveDate";
            this.gridColumn51.Name = "gridColumn51";
            this.gridColumn51.Visible = true;
            this.gridColumn51.VisibleIndex = 7;
            this.gridColumn51.Width = 99;
            // 
            // gridColumn53
            // 
            this.gridColumn53.Caption = "供应商";
            this.gridColumn53.FieldName = "SupplierName";
            this.gridColumn53.Name = "gridColumn53";
            this.gridColumn53.Visible = true;
            this.gridColumn53.VisibleIndex = 4;
            this.gridColumn53.Width = 113;
            // 
            // gridColumn54
            // 
            this.gridColumn54.Caption = "运单号";
            this.gridColumn54.FieldName = "ShipNO";
            this.gridColumn54.Name = "gridColumn54";
            this.gridColumn54.Visible = true;
            this.gridColumn54.VisibleIndex = 6;
            // 
            // gridColumn57
            // 
            this.gridColumn57.Caption = "原始单号";
            this.gridColumn57.FieldName = "OriginalBillNO";
            this.gridColumn57.Name = "gridColumn57";
            this.gridColumn57.Visible = true;
            this.gridColumn57.VisibleIndex = 2;
            // 
            // gridColumn58
            // 
            this.gridColumn58.Caption = "备注";
            this.gridColumn58.FieldName = "Remark";
            this.gridColumn58.Name = "gridColumn58";
            this.gridColumn58.Visible = true;
            this.gridColumn58.VisibleIndex = 8;
            this.gridColumn58.Width = 162;
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterControl1.Location = new System.Drawing.Point(0, 330);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(990, 5);
            this.splitterControl1.TabIndex = 5;
            this.splitterControl1.TabStop = false;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblQueryContent);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 31);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(990, 41);
            this.panelControl1.TabIndex = 11;
            // 
            // lblQueryContent
            // 
            this.lblQueryContent.Location = new System.Drawing.Point(18, 14);
            this.lblQueryContent.Name = "lblQueryContent";
            this.lblQueryContent.Size = new System.Drawing.Size(48, 12);
            this.lblQueryContent.TabIndex = 0;
            this.lblQueryContent.Text = "查询条件";
            // 
            // gridDetails
            // 
            this.gridDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDetails.Location = new System.Drawing.Point(0, 384);
            this.gridDetails.MainView = this.gvDetails;
            this.gridDetails.Name = "gridDetails";
            this.gridDetails.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.gridDetails.Size = new System.Drawing.Size(990, 134);
            this.gridDetails.TabIndex = 16;
            this.gridDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDetails});
            // 
            // gvDetails
            // 
            this.gvDetails.Appearance.ViewCaption.Options.UseTextOptions = true;
            this.gvDetails.Appearance.ViewCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gvDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn1,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn18,
            this.gridColumn20});
            this.gvDetails.GridControl = this.gridDetails;
            this.gvDetails.ID = "516ab106-35d4-4fb9-b1b1-ca9b026c07b7";
            this.gvDetails.Name = "gvDetails";
            this.gvDetails.OptionsBehavior.Editable = true;
            this.gvDetails.OptionsView.ColumnAutoWidth = false;
            this.gvDetails.OptionsView.EnableAppearanceOddRow = true;
            this.gvDetails.OptionsView.ShowAutoFilterRow = true;
            this.gvDetails.OptionsView.ShowFooter = true;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "物料编码";
            this.gridColumn10.FieldName = "MaterialCode";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 0;
            this.gridColumn10.Width = 129;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "物料名称";
            this.gridColumn11.FieldName = "MaterialName";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 1;
            this.gridColumn11.Width = 157;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "计量单位";
            this.gridColumn13.FieldName = "Unit";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 2;
            this.gridColumn13.Width = 57;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "数量";
            this.gridColumn14.FieldName = "Qty";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 3;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceHeader.ForeColor = System.Drawing.Color.Green;
            this.gridColumn1.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn1.Caption = "实收数量";
            this.gridColumn1.FieldName = "PutawayQty";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = true;
            this.gridColumn1.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 4;
            // 
            // gridColumn15
            // 
            this.gridColumn15.AppearanceHeader.ForeColor = System.Drawing.Color.Green;
            this.gridColumn15.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn15.Caption = "效期";
            this.gridColumn15.ColumnEdit = this.repositoryItemTextEdit1;
            this.gridColumn15.FieldName = "DueDate";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsColumn.AllowEdit = true;
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 5;
            this.gridColumn15.Width = 109;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Mask.EditMask = "\\d{4}((0([1-9]{1}))|(1[1|2]))(([0-2]([1-9]{1}))|(3[0|1]))";
            this.repositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.repositoryItemTextEdit1.Mask.ShowPlaceHolders = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // gridColumn16
            // 
            this.gridColumn16.AppearanceHeader.ForeColor = System.Drawing.Color.Green;
            this.gridColumn16.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn16.Caption = "批号";
            this.gridColumn16.FieldName = "BatchNO";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.OptionsColumn.AllowEdit = true;
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 6;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "单价";
            this.gridColumn18.FieldName = "Price";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 7;
            // 
            // gridColumn20
            // 
            this.gridColumn20.Caption = "备注";
            this.gridColumn20.FieldName = "Remark";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 8;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.gridControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 72);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(990, 258);
            this.panelControl2.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 5;
            this.layoutControlGroup1.Size = new System.Drawing.Size(386, 221);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.listLocations);
            this.panelControl3.Controls.Add(this.labelControl2);
            this.panelControl3.Controls.Add(this.lblBillNO);
            this.panelControl3.Controls.Add(this.labelControl1);
            this.panelControl3.Controls.Add(this.simpleButton1);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(0, 335);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(990, 49);
            this.panelControl3.TabIndex = 21;
            // 
            // listLocations
            // 
            this.listLocations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listLocations.Location = new System.Drawing.Point(685, 12);
            this.listLocations.Name = "listLocations";
            this.listLocations.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.listLocations.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("LocationCode", "名称")});
            this.listLocations.Properties.DisplayMember = "LocationCode";
            this.listLocations.Properties.DropDownRows = 4;
            this.listLocations.Properties.NullText = "";
            this.listLocations.Properties.ShowFooter = false;
            this.listLocations.Properties.ShowHeader = false;
            this.listLocations.Properties.ValueMember = "LocationCode";
            this.listLocations.Size = new System.Drawing.Size(176, 20);
            this.listLocations.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl2.Location = new System.Drawing.Point(605, 15);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(72, 12);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "可选暂存货位";
            // 
            // lblBillNO
            // 
            this.lblBillNO.Location = new System.Drawing.Point(102, 15);
            this.lblBillNO.Name = "lblBillNO";
            this.lblBillNO.Size = new System.Drawing.Size(12, 12);
            this.lblBillNO.TabIndex = 1;
            this.lblBillNO.Text = "空";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(84, 12);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "选中单据编号：";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(457, 20);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(79, 23);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "批量收货";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // FrmCrossInbound
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(990, 518);
            this.Controls.Add(this.gridDetails);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmCrossInbound";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "越库收货";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listLocations.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gvHeader;
        private DevExpress.XtraBars.BarButtonItem toolRefresh;
        private DevExpress.XtraBars.BarButtonItem toolConfirm;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn49;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn57;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn58;
        private DevExpress.XtraGrid.GridControl gridDetails;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDetails;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl lblQueryContent;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn40;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn44;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn50;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn51;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn53;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn54;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl lblBillNO;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit listLocations;
    }
}