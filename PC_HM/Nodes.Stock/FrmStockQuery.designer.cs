namespace Nodes.Stock
{
    partial class FrmStockQuery
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
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition2 = new DevExpress.XtraGrid.StyleFormatCondition();
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition3 = new DevExpress.XtraGrid.StyleFormatCondition();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.checkWithZero = new DevExpress.XtraEditors.CheckEdit();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.txtMC = new DevExpress.XtraEditors.TextEdit();
            this.txtLocation = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnSkuFlag = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkWithZero.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLocation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "保质期状态";
            this.gridColumn15.FieldName = "IsPassEXP";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 11;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.checkWithZero);
            this.groupControl1.Controls.Add(this.btnQuery);
            this.groupControl1.Controls.Add(this.txtMC);
            this.groupControl1.Controls.Add(this.txtLocation);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 31);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1114, 82);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "查询条件";
            // 
            // checkWithZero
            // 
            this.checkWithZero.Location = new System.Drawing.Point(543, 34);
            this.checkWithZero.Name = "checkWithZero";
            this.checkWithZero.Properties.Caption = "包含 0 库存";
            this.checkWithZero.Size = new System.Drawing.Size(111, 19);
            this.checkWithZero.TabIndex = 4;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(688, 30);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(91, 25);
            this.btnQuery.TabIndex = 5;
            this.btnQuery.Text = "查询";
            this.btnQuery.Click += new System.EventHandler(this.OnQueryClick);
            // 
            // txtMC
            // 
            this.txtMC.Location = new System.Drawing.Point(319, 35);
            this.txtMC.Name = "txtMC";
            this.txtMC.Properties.MaxLength = 50;
            this.txtMC.Properties.NullValuePrompt = "支持模糊查询";
            this.txtMC.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtMC.Size = new System.Drawing.Size(178, 20);
            this.txtMC.TabIndex = 3;
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(63, 35);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Properties.MaxLength = 20;
            this.txtLocation.Size = new System.Drawing.Size(178, 20);
            this.txtLocation.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(263, 38);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 12);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "物料名称";
            // 
            // labelControl3
            // 
            this.labelControl3.AllowHtmlString = true;
            this.labelControl3.Location = new System.Drawing.Point(5, 64);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(258, 14);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "【提示：<color=red>红色字体</color>表示剩余过期天数小于7天。】";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(27, 38);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 12);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "货位";
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bindingSource1;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 113);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1114, 383);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn10,
            this.gridColumn6,
            this.gridColumn17,
            this.gridColumn9,
            this.gridColumn12,
            this.gridColumn5,
            this.gridColumn11,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn18});
            styleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.Red;
            styleFormatCondition1.Appearance.Options.UseForeColor = true;
            styleFormatCondition1.ApplyToRow = true;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
            styleFormatCondition1.Expression = "[IsPassEXP] <= 0.5";
            styleFormatCondition2.Appearance.ForeColor = System.Drawing.Color.Red;
            styleFormatCondition2.Appearance.Options.UseForeColor = true;
            styleFormatCondition2.Column = this.gridColumn15;
            styleFormatCondition2.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            styleFormatCondition2.Value1 = "临期";
            styleFormatCondition3.Appearance.BackColor = System.Drawing.Color.Gray;
            styleFormatCondition3.Appearance.Options.UseBackColor = true;
            styleFormatCondition3.ApplyToRow = true;
            styleFormatCondition3.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
            styleFormatCondition3.Expression = "[LocationIsActive] == \'N\'";
            this.gridView1.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1,
            styleFormatCondition2,
            styleFormatCondition3});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.ID = "f45ec8ee-3646-478e-a282-202f4e6d9a97";
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn15, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "所属货区";
            this.gridColumn1.FieldName = "ZoneName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 12;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "货位";
            this.gridColumn2.FieldName = "Location";
            this.gridColumn2.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 110;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "物料编码";
            this.gridColumn3.FieldName = "Material";
            this.gridColumn3.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 80;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "物料名称";
            this.gridColumn4.FieldName = "MaterialName";
            this.gridColumn4.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 216;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "总库存";
            this.gridColumn7.DisplayFormat.FormatString = "f0";
            this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn7.FieldName = "Qty";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Qty", "{0:f0}")});
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "拣货中";
            this.gridColumn8.DisplayFormat.FormatString = "f0";
            this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn8.FieldName = "PickingQty";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 4;
            this.gridColumn8.Width = 60;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "占用中";
            this.gridColumn10.DisplayFormat.FormatString = "f0";
            this.gridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn10.FieldName = "OccupyQty";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 5;
            this.gridColumn10.Width = 60;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "库存单位";
            this.gridColumn6.FieldName = "UnitName";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 6;
            this.gridColumn6.Width = 60;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "生产日期";
            this.gridColumn17.FieldName = "CreateDate";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 7;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "有效期至";
            this.gridColumn9.FieldName = "ExpDate";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 8;
            this.gridColumn9.Width = 80;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "剩余过期天数";
            this.gridColumn12.FieldName = "ExpDiffDays";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 9;
            this.gridColumn12.Width = 90;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "最近一次入位";
            this.gridColumn5.DisplayFormat.FormatString = "g";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn5.FieldName = "LastInDate";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 14;
            this.gridColumn5.Width = 120;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "最近一次出位";
            this.gridColumn11.DisplayFormat.FormatString = "g";
            this.gridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn11.FieldName = "LastOutDate";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 15;
            this.gridColumn11.Width = 120;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "规格";
            this.gridColumn13.FieldName = "Spec";
            this.gridColumn13.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 3;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "保质期天数";
            this.gridColumn14.FieldName = "ExpDays";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 10;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "货位可用状态";
            this.gridColumn16.FieldName = "LocationIsActive";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 13;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "商品质量";
            this.gridColumn18.FieldName = "SkuQuality";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 16;
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
            this.barButtonItem2,
            this.barButtonItem3,
            this.barButtonItem4,
            this.barButtonItem5,
            this.barBtnSkuFlag});
            this.barManager1.MaxItemId = 6;
            // 
            // bar1
            // 
            this.bar1.BarName = "工具";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem3),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem4),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem5),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBtnSkuFlag)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableCustomization = true;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "工具";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "查看关联拣货单";
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem1.Tag = "";
            this.barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "查看占用记录";
            this.barButtonItem2.Id = 1;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem2.Tag = "占用记录";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "查看台账记录";
            this.barButtonItem3.Id = 2;
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem3.Tag = "台账记录";
            this.barButtonItem3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "查看商品库存信息";
            this.barButtonItem4.Id = 3;
            this.barButtonItem4.Name = "barButtonItem4";
            this.barButtonItem4.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem4.Tag = "查看商品库存信息";
            this.barButtonItem4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Caption = "删除0库存行";
            this.barButtonItem5.Id = 4;
            this.barButtonItem5.Name = "barButtonItem5";
            this.barButtonItem5.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem5.Tag = "删除0库存行";
            this.barButtonItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barButtonItem5.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // barBtnSkuFlag
            // 
            this.barBtnSkuFlag.Caption = "标记";
            this.barBtnSkuFlag.Id = 5;
            this.barBtnSkuFlag.Name = "barBtnSkuFlag";
            this.barBtnSkuFlag.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barBtnSkuFlag.Tag = "标记";
            this.barBtnSkuFlag.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnSkuFlag_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1114, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 496);
            this.barDockControlBottom.Size = new System.Drawing.Size(1114, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 465);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1114, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 465);
            // 
            // FrmStockQuery
            // 
            this.AcceptButton = this.btnQuery;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1114, 496);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmStockQuery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "库存查询";
            this.Load += new System.EventHandler(this.FrmStockQuery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkWithZero.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLocation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtMC;
        private DevExpress.XtraEditors.TextEdit txtLocation;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.CheckEdit checkWithZero;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraBars.BarButtonItem barBtnSkuFlag;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
    }
}