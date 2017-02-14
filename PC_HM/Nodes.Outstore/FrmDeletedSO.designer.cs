namespace Nodes.WMS.Outbound
{
    partial class FrmDeletedSO
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
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridHeader = new DevExpress.XtraGrid.GridControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gvHeader = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn40 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn44 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn48 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn49 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn50 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn51 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn53 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn54 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn57 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn55 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn58 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridDetails = new DevExpress.XtraGrid.GridControl();
            this.gvDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblQueryCondition = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
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
            this.barButtonItem1,
            this.barButtonItem2});
            this.barManager1.MaxItemId = 2;
            // 
            // bar1
            // 
            this.bar1.BarName = "工具";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
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
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "还原";
            this.barButtonItem2.Id = 1;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem2.Tag = "还原";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(771, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 534);
            this.barDockControlBottom.Size = new System.Drawing.Size(771, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 503);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(771, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 503);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 70);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gridHeader);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gridDetails);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(771, 464);
            this.splitContainerControl1.SplitterPosition = 218;
            this.splitContainerControl1.TabIndex = 4;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // gridHeader
            // 
            this.gridHeader.DataSource = this.bindingSource1;
            this.gridHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridHeader.Location = new System.Drawing.Point(0, 0);
            this.gridHeader.MainView = this.gvHeader;
            this.gridHeader.MenuManager = this.barManager1;
            this.gridHeader.Name = "gridHeader";
            this.gridHeader.Size = new System.Drawing.Size(771, 241);
            this.gridHeader.TabIndex = 5;
            this.gridHeader.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvHeader});
            // 
            // gvHeader
            // 
            this.gvHeader.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn40,
            this.gridColumn44,
            this.gridColumn48,
            this.gridColumn49,
            this.gridColumn50,
            this.gridColumn51,
            this.gridColumn53,
            this.gridColumn54,
            this.gridColumn57,
            this.gridColumn55,
            this.gridColumn58,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.gvHeader.GridControl = this.gridHeader;
            this.gvHeader.ID = "755be4b7-fe7c-4b2c-8ae7-0fb6f6cc5095";
            this.gvHeader.Name = "gvHeader";
            this.gvHeader.OptionsSelection.MultiSelect = true;
            this.gvHeader.OptionsView.EnableAppearanceOddRow = true;
            this.gvHeader.OptionsView.ShowAutoFilterRow = true;
            this.gvHeader.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.OnHeaderFocusedRowChanged);
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
            this.gridColumn44.FieldName = "BillTypeName";
            this.gridColumn44.Name = "gridColumn44";
            this.gridColumn44.Visible = true;
            this.gridColumn44.VisibleIndex = 1;
            // 
            // gridColumn48
            // 
            this.gridColumn48.Caption = "收货方式";
            this.gridColumn48.FieldName = "OutboundTypeName";
            this.gridColumn48.Name = "gridColumn48";
            this.gridColumn48.Visible = true;
            this.gridColumn48.VisibleIndex = 2;
            // 
            // gridColumn49
            // 
            this.gridColumn49.Caption = "业务员";
            this.gridColumn49.FieldName = "SalesMan";
            this.gridColumn49.Name = "gridColumn49";
            this.gridColumn49.Visible = true;
            this.gridColumn49.VisibleIndex = 4;
            // 
            // gridColumn50
            // 
            this.gridColumn50.Caption = "合同号";
            this.gridColumn50.FieldName = "ContractNO";
            this.gridColumn50.Name = "gridColumn50";
            this.gridColumn50.Visible = true;
            this.gridColumn50.VisibleIndex = 5;
            // 
            // gridColumn51
            // 
            this.gridColumn51.Caption = "交货日期";
            this.gridColumn51.FieldName = "DeliveryDate";
            this.gridColumn51.Name = "gridColumn51";
            this.gridColumn51.Visible = true;
            this.gridColumn51.VisibleIndex = 6;
            // 
            // gridColumn53
            // 
            this.gridColumn53.Caption = "客户";
            this.gridColumn53.FieldName = "CustomerName";
            this.gridColumn53.Name = "gridColumn53";
            this.gridColumn53.Visible = true;
            this.gridColumn53.VisibleIndex = 3;
            // 
            // gridColumn54
            // 
            this.gridColumn54.Caption = "运单号";
            this.gridColumn54.FieldName = "WayBillNumber";
            this.gridColumn54.Name = "gridColumn54";
            this.gridColumn54.Visible = true;
            this.gridColumn54.VisibleIndex = 7;
            // 
            // gridColumn57
            // 
            this.gridColumn57.Caption = "原始单号";
            this.gridColumn57.FieldName = "OriginalBillNO";
            this.gridColumn57.Name = "gridColumn57";
            this.gridColumn57.Visible = true;
            this.gridColumn57.VisibleIndex = 8;
            // 
            // gridColumn55
            // 
            this.gridColumn55.Caption = "发货完成时间";
            this.gridColumn55.DisplayFormat.FormatString = "g";
            this.gridColumn55.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn55.FieldName = "OutboundCompleteDate";
            this.gridColumn55.Name = "gridColumn55";
            this.gridColumn55.Visible = true;
            this.gridColumn55.VisibleIndex = 9;
            // 
            // gridColumn58
            // 
            this.gridColumn58.Caption = "备注";
            this.gridColumn58.FieldName = "Remark";
            this.gridColumn58.Name = "gridColumn58";
            this.gridColumn58.Visible = true;
            this.gridColumn58.VisibleIndex = 10;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "单据生成日期";
            this.gridColumn2.FieldName = "CreateDate";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 11;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "删除日期";
            this.gridColumn3.DisplayFormat.FormatString = "g";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn3.FieldName = "DeletedTime";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 12;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "删除人";
            this.gridColumn4.FieldName = "DeletedUser";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 13;
            // 
            // gridDetails
            // 
            this.gridDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDetails.Location = new System.Drawing.Point(0, 0);
            this.gridDetails.MainView = this.gvDetails;
            this.gridDetails.Name = "gridDetails";
            this.gridDetails.Size = new System.Drawing.Size(771, 218);
            this.gridDetails.TabIndex = 17;
            this.gridDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDetails});
            // 
            // gvDetails
            // 
            this.gvDetails.Appearance.ViewCaption.Options.UseTextOptions = true;
            this.gvDetails.Appearance.ViewCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gvDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn18,
            this.gridColumn19,
            this.gridColumn21,
            this.gridColumn22});
            this.gvDetails.GridControl = this.gridDetails;
            this.gvDetails.ID = "3df6f7fe-fa0e-4e58-b4c7-a0b48f2a143c";
            this.gvDetails.Name = "gvDetails";
            this.gvDetails.OptionsView.EnableAppearanceOddRow = true;
            this.gvDetails.OptionsView.ShowAutoFilterRow = true;
            this.gvDetails.OptionsView.ShowFooter = true;
            this.gvDetails.OptionsView.ShowViewCaption = true;
            this.gvDetails.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn1, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvDetails.ViewCaption = "未选择单据";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "行号";
            this.gridColumn1.FieldName = "RowNO";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 40;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "物料编码";
            this.gridColumn10.FieldName = "MaterialCode";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 1;
            this.gridColumn10.Width = 84;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "名称";
            this.gridColumn11.FieldName = "MaterialName";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 2;
            this.gridColumn11.Width = 84;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "计量单位";
            this.gridColumn13.FieldName = "Unit";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 3;
            this.gridColumn13.Width = 84;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "订购量";
            this.gridColumn14.FieldName = "Qty";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 4;
            this.gridColumn14.Width = 84;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "效期";
            this.gridColumn18.FieldName = "DueDate";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 5;
            this.gridColumn18.Width = 84;
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "批号";
            this.gridColumn19.FieldName = "BatchNO";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 6;
            this.gridColumn19.Width = 84;
            // 
            // gridColumn21
            // 
            this.gridColumn21.Caption = "单价";
            this.gridColumn21.FieldName = "Price";
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.Visible = true;
            this.gridColumn21.VisibleIndex = 7;
            this.gridColumn21.Width = 84;
            // 
            // gridColumn22
            // 
            this.gridColumn22.Caption = "备注";
            this.gridColumn22.FieldName = "Remark";
            this.gridColumn22.Name = "gridColumn22";
            this.gridColumn22.Visible = true;
            this.gridColumn22.VisibleIndex = 8;
            this.gridColumn22.Width = 102;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblQueryCondition);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 31);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(771, 39);
            this.panelControl1.TabIndex = 14;
            // 
            // lblQueryCondition
            // 
            this.lblQueryCondition.AllowHtmlString = true;
            this.lblQueryCondition.Location = new System.Drawing.Point(12, 11);
            this.lblQueryCondition.Name = "lblQueryCondition";
            this.lblQueryCondition.Size = new System.Drawing.Size(48, 14);
            this.lblQueryCondition.TabIndex = 1;
            this.lblQueryCondition.Text = "过滤条件";
            // 
            // FrmDeletedSO
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(771, 534);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmDeletedSO";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "发货单回收站";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl lblQueryCondition;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraGrid.GridControl gridHeader;
        private DevExpress.XtraGrid.Views.Grid.GridView gvHeader;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn40;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn44;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn48;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn49;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn50;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn51;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn53;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn54;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn57;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn55;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn58;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.GridControl gridDetails;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDetails;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
    }
}