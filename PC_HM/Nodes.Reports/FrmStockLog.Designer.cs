namespace Nodes.Reports
{
    partial class FrmStockLog
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
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.sbFlash = new DevExpress.XtraEditors.SimpleButton();
            this.dateEnd = new DevExpress.XtraEditors.DateEdit();
            this.dateBegin = new DevExpress.XtraEditors.DateEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gdDetails = new DevExpress.XtraGrid.GridControl();
            this.gvDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gdHeader = new DevExpress.XtraGrid.GridControl();
            this.gvHeader = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBegin.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBegin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHeader)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.sbFlash);
            this.panelControl1.Controls.Add(this.dateEnd);
            this.panelControl1.Controls.Add(this.dateBegin);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(953, 35);
            this.panelControl1.TabIndex = 6;
            // 
            // sbFlash
            // 
            this.sbFlash.Location = new System.Drawing.Point(38, 5);
            this.sbFlash.Name = "sbFlash";
            this.sbFlash.Size = new System.Drawing.Size(89, 25);
            this.sbFlash.TabIndex = 14;
            this.sbFlash.Text = "刷新";
            this.sbFlash.Click += new System.EventHandler(this.sbFlash_Click);
            // 
            // dateEnd
            // 
            this.dateEnd.EditValue = null;
            this.dateEnd.Location = new System.Drawing.Point(468, 8);
            this.dateEnd.Name = "dateEnd";
            this.dateEnd.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dateEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEnd.Properties.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.dateEnd.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEnd.Properties.EditFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.dateEnd.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEnd.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm";
            this.dateEnd.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dateEnd.Properties.ShowWeekNumbers = true;
            this.dateEnd.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
            this.dateEnd.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEnd.Size = new System.Drawing.Size(170, 20);
            this.dateEnd.TabIndex = 13;
            // 
            // dateBegin
            // 
            this.dateBegin.EditValue = null;
            this.dateBegin.Location = new System.Drawing.Point(202, 9);
            this.dateBegin.Name = "dateBegin";
            this.dateBegin.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dateBegin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateBegin.Properties.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.dateBegin.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateBegin.Properties.EditFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.dateBegin.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateBegin.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm";
            this.dateBegin.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dateBegin.Properties.ShowWeekNumbers = true;
            this.dateBegin.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
            this.dateBegin.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateBegin.Size = new System.Drawing.Size(170, 20);
            this.dateBegin.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(409, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "结束日期";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(143, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "开始日期";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.CustomizationFormText = "开始日期";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(252, 24);
            this.layoutControlItem1.Text = "开始日期";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(48, 12);
            this.layoutControlItem1.TextToControlDistance = 5;
            // 
            // gdDetails
            // 
            this.gdDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gdDetails.Location = new System.Drawing.Point(0, 202);
            this.gdDetails.MainView = this.gvDetails;
            this.gdDetails.Name = "gdDetails";
            this.gdDetails.Size = new System.Drawing.Size(953, 271);
            this.gdDetails.TabIndex = 28;
            this.gdDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDetails});
            // 
            // gvDetails
            // 
            this.gvDetails.Appearance.ViewCaption.Options.UseTextOptions = true;
            this.gvDetails.Appearance.ViewCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gvDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn18,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn20,
            this.gridColumn17,
            this.gridColumn19});
            styleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.Red;
            styleFormatCondition1.Appearance.Options.UseForeColor = true;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
            styleFormatCondition1.Expression = "[PlanQty] != [RealQty]";
            this.gvDetails.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1});
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
            // gridColumn10
            // 
            this.gridColumn10.Caption = "订单号";
            this.gridColumn10.FieldName = "bill_id";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 0;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "商品条码";
            this.gridColumn11.FieldName = "sku_code";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 1;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "商品名称";
            this.gridColumn18.FieldName = "sku_name";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 2;
            this.gridColumn18.Width = 150;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "商品规格";
            this.gridColumn12.FieldName = "um_name";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 3;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "入库数量";
            this.gridColumn13.FieldName = "in_sku_qty";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 4;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "出库数量";
            this.gridColumn14.FieldName = "out_sku_qty";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 5;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "现库存数量";
            this.gridColumn15.FieldName = "new_sku_qty";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 6;
            this.gridColumn15.Width = 100;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "操作类型";
            this.gridColumn16.FieldName = "action_type";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 7;
            // 
            // gridColumn20
            // 
            this.gridColumn20.Caption = "商品备注";
            this.gridColumn20.FieldName = "sku_desc";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 8;
            this.gridColumn20.Width = 90;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "创建时间";
            this.gridColumn17.FieldName = "create_time";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 9;
            this.gridColumn17.Width = 125;
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "默认修改时间";
            this.gridColumn19.FieldName = "updateTime";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 10;
            this.gridColumn19.Width = 125;
            // 
            // gdHeader
            // 
            this.gdHeader.DataSource = this.bindingSource1;
            this.gdHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.gdHeader.Location = new System.Drawing.Point(0, 35);
            this.gdHeader.MainView = this.gvHeader;
            this.gdHeader.Name = "gdHeader";
            this.gdHeader.Size = new System.Drawing.Size(953, 167);
            this.gdHeader.TabIndex = 27;
            this.gdHeader.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvHeader});
            // 
            // gvHeader
            // 
            this.gvHeader.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9});
            this.gvHeader.GridControl = this.gdHeader;
            this.gvHeader.GroupCount = 1;
            this.gvHeader.ID = "4e1bbad9-8fa9-41e1-bab0-2ea207b59597";
            this.gvHeader.Name = "gvHeader";
            this.gvHeader.OptionsDetail.EnableMasterViewMode = false;
            this.gvHeader.OptionsSelection.MultiSelect = true;
            this.gvHeader.OptionsView.ColumnAutoWidth = false;
            this.gvHeader.OptionsView.EnableAppearanceOddRow = true;
            this.gvHeader.OptionsView.ShowAutoFilterRow = true;
            this.gvHeader.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn9, DevExpress.Data.ColumnSortOrder.Descending)});
            this.gvHeader.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvHeader_FocusedRowChanged);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "商品条码";
            this.gridColumn1.FieldName = "sku_code";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "商品名称";
            this.gridColumn2.FieldName = "sku_name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 150;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "商品规格";
            this.gridColumn3.FieldName = "um_name";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "商品库存总计";
            this.gridColumn4.FieldName = "sku_qty_total";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 100;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "库房号";
            this.gridColumn5.FieldName = "wh_code";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "第一次插入时间";
            this.gridColumn6.FieldName = "latestIn";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 125;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "默认修改时间";
            this.gridColumn7.FieldName = "updateTime";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            this.gridColumn7.Width = 125;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "总库存备注";
            this.gridColumn8.FieldName = "sku_desc";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 7;
            this.gridColumn8.Width = 100;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "分区";
            this.gridColumn9.FieldName = "znName";
            this.gridColumn9.Name = "gridColumn9";
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterControl1.Location = new System.Drawing.Point(0, 202);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(953, 5);
            this.splitterControl1.TabIndex = 29;
            this.splitterControl1.TabStop = false;
            // 
            // FrmStockLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(953, 473);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.gdDetails);
            this.Controls.Add(this.gdHeader);
            this.Controls.Add(this.panelControl1);
            this.Name = "FrmStockLog";
            this.Text = "库存流水查询";
            this.Load += new System.EventHandler(this.FrmStockLog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBegin.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBegin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHeader)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraGrid.GridControl gdDetails;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDetails;
        private DevExpress.XtraGrid.GridControl gdHeader;
        private DevExpress.XtraGrid.Views.Grid.GridView gvHeader;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
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
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraEditors.DateEdit dateBegin;
        private DevExpress.XtraEditors.DateEdit dateEnd;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraEditors.SimpleButton sbFlash;
    }
}