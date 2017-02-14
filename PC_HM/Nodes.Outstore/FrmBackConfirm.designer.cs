namespace Nodes.Outstore
{
    partial class FrmBackConfirm
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
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.toolRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.toolBookIn = new DevExpress.XtraBars.BarButtonItem();
            this.toolConfirm = new DevExpress.XtraBars.BarButtonItem();
            this.toolQueryHistory = new DevExpress.XtraBars.BarButtonItem();
            this.toolMarkDelayedOrder = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.lstVehicle = new DevExpress.XtraEditors.LookUpEdit();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstVehicle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "单据号";
            this.gridColumn1.FieldName = "BillNO";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 170;
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
            this.toolConfirm,
            this.toolBookIn,
            this.toolQueryHistory,
            this.toolMarkDelayedOrder});
            this.barManager1.MaxItemId = 5;
            // 
            // bar1
            // 
            this.bar1.BarName = "工具";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.toolRefresh),
            new DevExpress.XtraBars.LinkPersistInfo(this.toolBookIn, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.toolConfirm),
            new DevExpress.XtraBars.LinkPersistInfo(this.toolQueryHistory),
            new DevExpress.XtraBars.LinkPersistInfo(this.toolMarkDelayedOrder)});
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
            // toolBookIn
            // 
            this.toolBookIn.Caption = "金额录入";
            this.toolBookIn.Id = 2;
            this.toolBookIn.Name = "toolBookIn";
            this.toolBookIn.Tag = "金额录入";
            this.toolBookIn.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.toolBookIn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // toolConfirm
            // 
            this.toolConfirm.Caption = "回款确认";
            this.toolConfirm.Id = 1;
            this.toolConfirm.Name = "toolConfirm";
            this.toolConfirm.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.toolConfirm.Tag = "回款确认";
            this.toolConfirm.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.toolConfirm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // toolQueryHistory
            // 
            this.toolQueryHistory.Caption = "确认历史查询";
            this.toolQueryHistory.Id = 3;
            this.toolQueryHistory.Name = "toolQueryHistory";
            this.toolQueryHistory.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.toolQueryHistory.Tag = "确认历史查询";
            this.toolQueryHistory.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.toolQueryHistory.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // toolMarkDelayedOrder
            // 
            this.toolMarkDelayedOrder.Caption = "二次发货";
            this.toolMarkDelayedOrder.Id = 4;
            this.toolMarkDelayedOrder.Name = "toolMarkDelayedOrder";
            this.toolMarkDelayedOrder.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.toolMarkDelayedOrder.Tag = "标记延交订单";
            this.toolMarkDelayedOrder.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(922, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 456);
            this.barDockControlBottom.Size = new System.Drawing.Size(922, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 425);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(922, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 425);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnQuery);
            this.layoutControl1.Controls.Add(this.lstVehicle);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.ID = "72df686b-83a4-41fe-8cf1-3f73a48014b4";
            this.layoutControl1.Location = new System.Drawing.Point(0, 31);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(667, 228, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(922, 425);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(313, 12);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(100, 22);
            this.btnQuery.StyleController = this.layoutControl1;
            this.btnQuery.TabIndex = 6;
            this.btnQuery.Text = "查询";
            this.btnQuery.Click += new System.EventHandler(this.OnbtnQueryClick);
            // 
            // lstVehicle
            // 
            this.lstVehicle.Location = new System.Drawing.Point(63, 12);
            this.lstVehicle.MenuManager = this.barManager1;
            this.lstVehicle.Name = "lstVehicle";
            this.lstVehicle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lstVehicle.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("VehicleNO", "车牌号"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("RouteName", "线路名称"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("UserName", "司机姓名")});
            this.lstVehicle.Properties.DataSource = this.bindingSource1;
            this.lstVehicle.Properties.DisplayMember = "JianCheng";
            this.lstVehicle.Properties.NullText = "";
            this.lstVehicle.Properties.ValueMember = "ID";
            this.lstVehicle.Size = new System.Drawing.Size(246, 20);
            this.lstVehicle.StyleController = this.layoutControl1;
            this.lstVehicle.TabIndex = 5;
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 38);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.MenuManager = this.barManager1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(898, 375);
            this.gridControl1.TabIndex = 7;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn16,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn13,
            this.gridColumn15,
            this.gridColumn17,
            this.gridColumn14,
            this.gridColumn4,
            this.gridColumn3,
            this.gridColumn2,
            this.gridColumn5});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.ID = "a67823b5-e306-42c2-8e70-1bc452ad6bd6";
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowViewCaption = true;
            this.gridView1.ViewCaption = " ";
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "客户姓名";
            this.gridColumn16.FieldName = "CustomerName";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 1;
            this.gridColumn16.Width = 170;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "收货人";
            this.gridColumn8.FieldName = "Consignee";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 2;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "客户地址";
            this.gridColumn9.FieldName = "Address";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 3;
            this.gridColumn9.Width = 150;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "应收金额";
            this.gridColumn13.DisplayFormat.FormatString = "f2";
            this.gridColumn13.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn13.FieldName = "ReceiveAmount";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ReceiveAmount", "{0:f2}")});
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 4;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "退货金额";
            this.gridColumn15.DisplayFormat.FormatString = "f2";
            this.gridColumn15.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn15.FieldName = "CrnAmount";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "CrnAmount", "{0:f2}")});
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 5;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "它项金额";
            this.gridColumn17.DisplayFormat.FormatString = "f2";
            this.gridColumn17.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn17.FieldName = "OtherAmount";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "OtherAmount", "{0:f2}")});
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 6;
            this.gridColumn17.Width = 90;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "实收现金";
            this.gridColumn14.DisplayFormat.FormatString = "f2";
            this.gridColumn14.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn14.FieldName = "RealAmount";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "RealAmount", "{0:f2}")});
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 7;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "它项金额备注";
            this.gridColumn4.FieldName = "OtherRemark";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 8;
            this.gridColumn4.Width = 90;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "建单时间";
            this.gridColumn3.FieldName = "CreateDate";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 9;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "发货时间";
            this.gridColumn2.FieldName = "CloseDate";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 10;
            this.gridColumn2.Width = 90;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "未收款标记";
            this.gridColumn5.FieldName = "PaymentFlagDesc";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 11;
            this.gridColumn5.Width = 80;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(922, 425);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.lstVehicle;
            this.layoutControlItem2.CustomizationFormText = "车辆信息";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(301, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(301, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(301, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "车辆信息";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(48, 12);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnQuery;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(301, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(104, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(104, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(104, 26);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(405, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(497, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridControl1;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(902, 379);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // FrmBackConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 456);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmBackConfirm";
            this.Text = "回款确认";
            this.Load += new System.EventHandler(this.OnFrmLoad);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lstVehicle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem toolRefresh;
        private DevExpress.XtraBars.BarButtonItem toolConfirm;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraEditors.LookUpEdit lstVehicle;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraBars.BarButtonItem toolBookIn;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraBars.BarButtonItem toolQueryHistory;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraBars.BarButtonItem toolMarkDelayedOrder;

    }
}