namespace Nodes.Outstore
{
    partial class FrmShowNeedSKU
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
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colmChecked = new DevExpress.XtraGrid.Columns.GridColumn();
            this.check = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.IsFlag = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.toolRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.toolLog = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem10 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.check)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "状态";
            this.gridColumn6.FieldName = "State";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 9;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 31);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.check});
            this.gridControl1.Size = new System.Drawing.Size(885, 418);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colmChecked,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn7,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn8,
            this.gridColumn10,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn9,
            this.IsFlag,
            this.gridColumn11});
            styleFormatCondition1.Appearance.BackColor = System.Drawing.Color.Yellow;
            styleFormatCondition1.Appearance.BackColor2 = System.Drawing.Color.Red;
            styleFormatCondition1.Appearance.Options.UseBackColor = true;
            styleFormatCondition1.ApplyToRow = true;
            styleFormatCondition1.Column = this.gridColumn6;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            styleFormatCondition1.Value1 = "N";
            this.gridView1.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1,
            styleFormatCondition2});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.ID = "889729f5-a665-48fc-8e31-fc58aa757588";
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = true;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.RowDoubleClick += new System.EventHandler(this.gridView1_RowDoubleClick);
            this.gridView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnViewMouseUp);
            // 
            // colmChecked
            // 
            this.colmChecked.AppearanceHeader.ForeColor = System.Drawing.Color.Green;
            this.colmChecked.AppearanceHeader.Options.UseForeColor = true;
            this.colmChecked.Caption = "选中";
            this.colmChecked.ColumnEdit = this.check;
            this.colmChecked.FieldName = "HasChecked";
            this.colmChecked.Name = "colmChecked";
            this.colmChecked.OptionsColumn.AllowEdit = true;
            this.colmChecked.OptionsColumn.AllowMove = false;
            this.colmChecked.OptionsColumn.AllowShowHide = false;
            this.colmChecked.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colmChecked.OptionsColumn.FixedWidth = true;
            this.colmChecked.Visible = true;
            this.colmChecked.VisibleIndex = 0;
            // 
            // check
            // 
            this.check.AutoHeight = false;
            this.check.Name = "check";
            this.check.CheckStateChanged += new System.EventHandler(this.check_CheckStateChanged);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "商品编码";
            this.gridColumn1.FieldName = "SKUCode";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 90;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "商品名称";
            this.gridColumn2.FieldName = "SKUName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 300;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "订单商品规格";
            this.gridColumn7.FieldName = "Spec";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 3;
            this.gridColumn7.Width = 100;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "订单总量";
            this.gridColumn3.FieldName = "BillQty";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 4;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "拣货区库存量";
            this.gridColumn4.FieldName = "TotalQty";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 5;
            this.gridColumn4.Width = 88;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "备货区库存量";
            this.gridColumn8.FieldName = "StockTotalQty";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 6;
            this.gridColumn8.Width = 88;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "越库区库存量";
            this.gridColumn10.FieldName = "BackupQty";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 7;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "计量单位";
            this.gridColumn5.FieldName = "UmName";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 8;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "整散标记";
            this.gridColumn9.FieldName = "IsCaseName";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 10;
            // 
            // IsFlag
            // 
            this.IsFlag.Caption = "是否补货";
            this.IsFlag.FieldName = "IsFlag";
            this.IsFlag.Name = "IsFlag";
            this.IsFlag.Visible = true;
            this.IsFlag.VisibleIndex = 11;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "建议补货量";
            this.gridColumn11.FieldName = "AdiceQtyUmName";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 12;
            this.gridColumn11.Width = 88;
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
            this.barButtonItem4,
            this.toolLog,
            this.barButtonItem10,
            this.barButtonItem7,
            this.barButtonItem1,
            this.barButtonItem2,
            this.barButtonItem3,
            this.barButtonItem5,
            this.barButtonItem6});
            this.barManager1.MaxItemId = 43;
            // 
            // bar1
            // 
            this.bar1.BarName = "工具";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.toolRefresh, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem5, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem3),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem6)});
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
            // barButtonItem5
            // 
            this.barButtonItem5.Caption = "未排序订单";
            this.barButtonItem5.Id = 40;
            this.barButtonItem5.Name = "barButtonItem5";
            this.barButtonItem5.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem5.Tag = "未排序订单";
            this.barButtonItem5.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "已排序订单";
            this.barButtonItem3.Id = 39;
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem3.Tag = "已排序订单";
            this.barButtonItem3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "生成补货任务";
            this.barButtonItem1.Id = 36;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem1.Tag = "生成补货任务";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "单品补货";
            this.barButtonItem2.Id = 38;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem2.Tag = "单品补货";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Caption = "安全库存触发补货";
            this.barButtonItem6.Id = 42;
            this.barButtonItem6.Name = "barButtonItem6";
            this.barButtonItem6.Tag = "安全库存触发补货";
            this.barButtonItem6.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(885, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 449);
            this.barDockControlBottom.Size = new System.Drawing.Size(885, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 418);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(885, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 418);
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "原始单据";
            this.barButtonItem4.Id = 25;
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // toolLog
            // 
            this.toolLog.Caption = "操作日志";
            this.toolLog.Id = 26;
            this.toolLog.Name = "toolLog";
            this.toolLog.Tag = "操作日志";
            // 
            // barButtonItem10
            // 
            this.barButtonItem10.Caption = "拣货计划";
            this.barButtonItem10.Id = 28;
            this.barButtonItem10.Name = "barButtonItem10";
            this.barButtonItem10.Tag = "拣货计划";
            // 
            // barButtonItem7
            // 
            this.barButtonItem7.Caption = "拣货记录";
            this.barButtonItem7.Id = 34;
            this.barButtonItem7.Name = "barButtonItem7";
            this.barButtonItem7.Tag = "拣货记录";
            // 
            // FrmShowNeedSKU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 449);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmShowNeedSKU";
            this.Text = "当前订单量";
            this.Load += new System.EventHandler(this.FrmShowNeedSKU_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.check)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem toolRefresh;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem toolLog;
        private DevExpress.XtraBars.BarButtonItem barButtonItem10;
        private DevExpress.XtraBars.BarButtonItem barButtonItem7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraGrid.Columns.GridColumn IsFlag;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn colmChecked;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit check;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
    }
}