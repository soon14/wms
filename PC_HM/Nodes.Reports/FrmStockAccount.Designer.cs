namespace Nodes.Reports
{
    partial class FrmStockAccount
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn26 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn27 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtMaterialCode = new DevExpress.XtraEditors.TextEdit();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.dateEditTo = new DevExpress.XtraEditors.DateEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.dateEditFrom = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaterialCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 24);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1370, 454);
            this.gridControl1.TabIndex = 2;
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
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn17,
            this.gridColumn18,
            this.gridColumn19,
            this.gridColumn20,
            this.gridColumn21,
            this.gridColumn22,
            this.gridColumn23,
            this.gridColumn24,
            this.gridColumn25,
            this.gridColumn26,
            this.gridColumn27});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.ID = "c50f1eb0-738c-4f1e-9254-bc201b63a7ed";
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = true;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "时间";
            this.gridColumn1.FieldName = "chekDate";
            this.gridColumn1.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "商品ID";
            this.gridColumn2.FieldName = "skuCode";
            this.gridColumn2.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "商品名称";
            this.gridColumn3.FieldName = "skuName";
            this.gridColumn3.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 150;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "单位";
            this.gridColumn4.FieldName = "umName";
            this.gridColumn4.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "期初库存";
            this.gridColumn5.FieldName = "beginQty";
            this.gridColumn5.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "期末库存";
            this.gridColumn6.FieldName = "endQty";
            this.gridColumn6.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "采购入库";
            this.gridColumn7.FieldName = "enterAsnQty";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "销售退货入库";
            this.gridColumn8.FieldName = "enterCrnQty";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 7;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "调拨入库";
            this.gridColumn9.FieldName = "enterTransQty";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 8;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "采购换货入库";
            this.gridColumn10.FieldName = "enterExchangeQty";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 9;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "采购退货入库";
            this.gridColumn11.FieldName = "enterReturnQty";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 10;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "盘盈入库";
            this.gridColumn12.FieldName = "enterCountPlusQty";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 11;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "其他入库";
            this.gridColumn13.FieldName = "enterOtherQty";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 12;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "销售出库";
            this.gridColumn14.FieldName = "outSalesQty";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 13;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "退货调拨出库";
            this.gridColumn15.FieldName = "outReturnTransQty";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 14;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "正常调拨出库";
            this.gridColumn16.FieldName = "outTransQty";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 15;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "销售调拨出库";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 16;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "采购退货出库";
            this.gridColumn18.FieldName = "outAsnReturnQty";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 17;
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "报损出库";
            this.gridColumn19.FieldName = "outBadQty";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 18;
            // 
            // gridColumn20
            // 
            this.gridColumn20.Caption = "内卖出库";
            this.gridColumn20.FieldName = "outInsideSaleQty";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 19;
            // 
            // gridColumn21
            // 
            this.gridColumn21.Caption = "折扣出库";
            this.gridColumn21.FieldName = "outDiscount";
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.Visible = true;
            this.gridColumn21.VisibleIndex = 20;
            // 
            // gridColumn22
            // 
            this.gridColumn22.Caption = "采购换货出库";
            this.gridColumn22.FieldName = "outExchangeQty";
            this.gridColumn22.Name = "gridColumn22";
            this.gridColumn22.Visible = true;
            this.gridColumn22.VisibleIndex = 21;
            // 
            // gridColumn23
            // 
            this.gridColumn23.Caption = "内领出库";
            this.gridColumn23.FieldName = "outUseQty";
            this.gridColumn23.Name = "gridColumn23";
            this.gridColumn23.Visible = true;
            this.gridColumn23.VisibleIndex = 22;
            // 
            // gridColumn24
            // 
            this.gridColumn24.Caption = "取消订单调拨出库";
            this.gridColumn24.FieldName = "outCancelQty";
            this.gridColumn24.Name = "gridColumn24";
            this.gridColumn24.Visible = true;
            this.gridColumn24.VisibleIndex = 23;
            // 
            // gridColumn25
            // 
            this.gridColumn25.Caption = "盘亏出库";
            this.gridColumn25.FieldName = "outCountMinusQty";
            this.gridColumn25.Name = "gridColumn25";
            this.gridColumn25.Visible = true;
            this.gridColumn25.VisibleIndex = 24;
            // 
            // gridColumn26
            // 
            this.gridColumn26.Caption = "其他出库";
            this.gridColumn26.FieldName = "outOtherQty";
            this.gridColumn26.Name = "gridColumn26";
            this.gridColumn26.Visible = true;
            this.gridColumn26.VisibleIndex = 25;
            // 
            // gridColumn27
            // 
            this.gridColumn27.AppearanceCell.ForeColor = System.Drawing.Color.Red;
            this.gridColumn27.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn27.Caption = "差异";
            this.gridColumn27.FieldName = "diff";
            this.gridColumn27.Name = "gridColumn27";
            this.gridColumn27.Visible = true;
            this.gridColumn27.VisibleIndex = 26;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(394, 6);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 12);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "商品ID";
            // 
            // txtMaterialCode
            // 
            this.txtMaterialCode.Location = new System.Drawing.Point(438, 2);
            this.txtMaterialCode.Name = "txtMaterialCode";
            this.txtMaterialCode.Properties.MaxLength = 30;
            this.txtMaterialCode.Size = new System.Drawing.Size(81, 20);
            this.txtMaterialCode.TabIndex = 1;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(541, 0);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 14;
            this.btnQuery.Text = "查询";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(15, 5);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(48, 12);
            this.labelControl7.TabIndex = 10;
            this.labelControl7.Text = "开始日期";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(210, 5);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(48, 12);
            this.labelControl6.TabIndex = 12;
            this.labelControl6.Text = "结束日期";
            // 
            // dateEditTo
            // 
            this.dateEditTo.EditValue = null;
            this.dateEditTo.Location = new System.Drawing.Point(268, 2);
            this.dateEditTo.Name = "dateEditTo";
            this.dateEditTo.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dateEditTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditTo.Properties.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.dateEditTo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditTo.Properties.EditFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.dateEditTo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditTo.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm";
            this.dateEditTo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dateEditTo.Properties.ShowWeekNumbers = true;
            this.dateEditTo.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
            this.dateEditTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditTo.Size = new System.Drawing.Size(115, 20);
            this.dateEditTo.TabIndex = 13;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.dateEditFrom);
            this.panelControl1.Controls.Add(this.dateEditTo);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.labelControl7);
            this.panelControl1.Controls.Add(this.btnQuery);
            this.panelControl1.Controls.Add(this.txtMaterialCode);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1370, 24);
            this.panelControl1.TabIndex = 1;
            // 
            // dateEditFrom
            // 
            this.dateEditFrom.EditValue = null;
            this.dateEditFrom.Location = new System.Drawing.Point(73, 2);
            this.dateEditFrom.Name = "dateEditFrom";
            this.dateEditFrom.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dateEditFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditFrom.Properties.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.dateEditFrom.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditFrom.Properties.EditFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.dateEditFrom.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEditFrom.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm";
            this.dateEditFrom.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dateEditFrom.Properties.ShowWeekNumbers = true;
            this.dateEditFrom.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
            this.dateEditFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditFrom.Size = new System.Drawing.Size(117, 20);
            this.dateEditFrom.TabIndex = 11;
            // 
            // FrmStockAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 478);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panelControl1);
            this.Name = "FrmStockAccount";
            this.Text = "库存对账查询";
            this.Load += new System.EventHandler(this.FrmStockAccount_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaterialCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtMaterialCode;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.DateEdit dateEditTo;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.DateEdit dateEditFrom;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn23;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn24;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn25;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn26;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn27;
        private System.Windows.Forms.BindingSource bindingSource;
    }
}