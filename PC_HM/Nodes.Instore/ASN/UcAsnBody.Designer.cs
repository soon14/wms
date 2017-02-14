namespace Nodes.Instore
{
    partial class UcAsnBody
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gdHeader = new DevExpress.XtraGrid.GridControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource();
            this.gvHeader = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblMsg = new Nodes.UI.SplashLabel();
            this.lblCondition = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.gdDetails = new DevExpress.XtraGrid.GridControl();
            this.gvDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gdHeader);
            this.splitContainerControl1.Panel1.Controls.Add(this.panelControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gdDetails);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1081, 448);
            this.splitContainerControl1.SplitterPosition = 243;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // gdHeader
            // 
            this.gdHeader.DataSource = this.bindingSource1;
            this.gdHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gdHeader.Location = new System.Drawing.Point(0, 39);
            this.gdHeader.MainView = this.gvHeader;
            this.gdHeader.Name = "gdHeader";
            this.gdHeader.Size = new System.Drawing.Size(1081, 161);
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
            this.gridColumn23,
            this.gridColumn20,
            this.gridColumn22,
            this.gridColumn19,
            this.gridColumn1,
            this.gridColumn9});
            this.gvHeader.GridControl = this.gdHeader;
            this.gvHeader.ID = "4e1bbad9-8fa9-41e1-bab0-2ea207b59597";
            this.gvHeader.Name = "gvHeader";
            this.gvHeader.OptionsDetail.EnableMasterViewMode = false;
            this.gvHeader.OptionsSelection.MultiSelect = true;
            this.gvHeader.OptionsView.ColumnAutoWidth = false;
            this.gvHeader.OptionsView.EnableAppearanceOddRow = true;
            this.gvHeader.OptionsView.ShowAutoFilterRow = true;
            this.gvHeader.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn20, DevExpress.Data.ColumnSortOrder.Descending)});
            this.gvHeader.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.OnHeaderRowCellStyle);
            this.gvHeader.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.OnHeaderFocusedRowChanged);
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "入库单号";
            this.gridColumn7.FieldName = "BillNO";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 0;
            this.gridColumn7.Width = 160;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "供应商";
            this.gridColumn4.FieldName = "SupplierName";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            this.gridColumn4.Width = 180;
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
            this.gridColumn21.Visible = true;
            this.gridColumn21.VisibleIndex = 7;
            // 
            // gridColumn23
            // 
            this.gridColumn23.Caption = "打印时间";
            this.gridColumn23.DisplayFormat.FormatString = "g";
            this.gridColumn23.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn23.FieldName = "PrintedTime";
            this.gridColumn23.Name = "gridColumn23";
            this.gridColumn23.Tag = "";
            this.gridColumn23.Visible = true;
            this.gridColumn23.VisibleIndex = 8;
            // 
            // gridColumn20
            // 
            this.gridColumn20.Caption = "建单日期";
            this.gridColumn20.DisplayFormat.FormatString = "g";
            this.gridColumn20.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn20.FieldName = "CreateDate";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 9;
            this.gridColumn20.Width = 110;
            // 
            // gridColumn22
            // 
            this.gridColumn22.Caption = "完成日期";
            this.gridColumn22.DisplayFormat.FormatString = "g";
            this.gridColumn22.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn22.FieldName = "CloseDate";
            this.gridColumn22.Name = "gridColumn22";
            this.gridColumn22.Tag = "";
            this.gridColumn22.Visible = true;
            this.gridColumn22.VisibleIndex = 10;
            this.gridColumn22.Width = 110;
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "建单人";
            this.gridColumn19.FieldName = "Creator";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 11;
            this.gridColumn19.Width = 73;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "WMS备注";
            this.gridColumn1.FieldName = "WmsRemark";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 12;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "入库方式";
            this.gridColumn9.FieldName = "InstoreTypeDesc";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 4;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblMsg);
            this.panelControl1.Controls.Add(this.lblCondition);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1081, 39);
            this.panelControl1.TabIndex = 21;
            // 
            // lblMsg
            // 
            this.lblMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMsg.Location = new System.Drawing.Point(910, 13);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(48, 12);
            this.lblMsg.TabIndex = 2;
            this.lblMsg.Text = "查询完成";
            this.lblMsg.Visible = false;
            // 
            // lblCondition
            // 
            this.lblCondition.Location = new System.Drawing.Point(91, 13);
            this.lblCondition.Name = "lblCondition";
            this.lblCondition.Size = new System.Drawing.Size(72, 12);
            this.lblCondition.TabIndex = 1;
            this.lblCondition.Text = "显示过滤条件";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(24, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 12);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "筛选条件：";
            // 
            // gdDetails
            // 
            this.gdDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gdDetails.Location = new System.Drawing.Point(0, 0);
            this.gdDetails.MainView = this.gvDetails;
            this.gdDetails.Name = "gdDetails";
            this.gdDetails.Size = new System.Drawing.Size(1081, 243);
            this.gdDetails.TabIndex = 18;
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
            this.gridColumn10,
            this.gridColumn17,
            this.gridColumn18,
            this.gridColumn14});
            styleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.Red;
            styleFormatCondition1.Appearance.Options.UseForeColor = true;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
            styleFormatCondition1.Expression = "[PlanQty] != [RealQty]";
            this.gvDetails.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1});
            this.gvDetails.GridControl = this.gdDetails;
            this.gvDetails.ID = "b677e7e9-b1be-49c5-9150-0b570f778b86";
            this.gvDetails.Name = "gvDetails";
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
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 3;
            this.gridColumn13.Width = 93;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "销售单位";
            this.gridColumn15.FieldName = "UnitName";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 4;
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
            this.gridColumn16.VisibleIndex = 5;
            this.gridColumn16.Width = 85;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "已收量";
            this.gridColumn10.FieldName = "PutQty";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 6;
            this.gridColumn10.Width = 85;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "单价";
            this.gridColumn17.DisplayFormat.FormatString = "0.0000";
            this.gridColumn17.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn17.FieldName = "Price";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 7;
            this.gridColumn17.Width = 70;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "订单金额";
            this.gridColumn18.DisplayFormat.FormatString = "0.0000";
            this.gridColumn18.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn18.FieldName = "PlanAmount";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PlanAmount", "{0:N2}")});
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 8;
            this.gridColumn18.Width = 85;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "实收现金";
            this.gridColumn14.FieldName = "PutAmount";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 9;
            // 
            // UcAsnBody
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "UcAsnBody";
            this.Size = new System.Drawing.Size(1081, 448);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gdHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Nodes.UI.SplashLabel lblMsg;
        private DevExpress.XtraEditors.LabelControl lblCondition;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.GridControl gdDetails;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDetails;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.GridControl gdHeader;
        private DevExpress.XtraGrid.Views.Grid.GridView gvHeader;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn23;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
    }
}
