namespace Nodes.CycleCount
{
    partial class FrmCountExecute
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
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition3 = new DevExpress.XtraGrid.StyleFormatCondition();
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition4 = new DevExpress.XtraGrid.StyleFormatCondition();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnCreate = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.listBillNO = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.sbFlash = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listBillNO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "有效期至";
            this.gridColumn1.DisplayFormat.FormatString = "d";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn1.FieldName = "EXP_DATE";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 7;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.sbFlash);
            this.panelControl1.Controls.Add(this.btnCreate);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.listBillNO);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.btnCancel);
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(883, 92);
            this.panelControl1.TabIndex = 0;
            // 
            // btnCreate
            // 
            this.btnCreate.AccessibleDescription = "eng";
            this.btnCreate.Location = new System.Drawing.Point(664, 15);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(89, 31);
            this.btnCreate.TabIndex = 5;
            this.btnCreate.Text = "生成盘点单";
            this.btnCreate.Visible = false;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.labelControl4.Location = new System.Drawing.Point(558, 74);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(318, 12);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "*注：有效期显示红色表示当前盘点的效期与库存效期不一致";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(124, 62);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 12);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "2015-06-06";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(59, 61);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(54, 12);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "下发时间:";
            // 
            // listBillNO
            // 
            this.listBillNO.Location = new System.Drawing.Point(124, 21);
            this.listBillNO.Name = "listBillNO";
            this.listBillNO.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.listBillNO.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("BillID", "编号")});
            this.listBillNO.Properties.DisplayMember = "BillID";
            this.listBillNO.Properties.NullText = "";
            this.listBillNO.Properties.ShowFooter = false;
            this.listBillNO.Properties.ShowHeader = false;
            this.listBillNO.Properties.ValueMember = "BillID";
            this.listBillNO.Size = new System.Drawing.Size(194, 20);
            this.listBillNO.TabIndex = 2;
            this.listBillNO.EditValueChanged += new System.EventHandler(this.listBillNO_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(23, 24);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(90, 12);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "选择差异调整单:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(551, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(89, 31);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消差异调整";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(443, 15);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(89, 31);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "确定执行";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(0, 92);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(883, 387);
            this.gridControl2.TabIndex = 3;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn7,
            this.gridColumn3,
            this.gridColumn8,
            this.gridColumn10,
            this.gridColumn4,
            this.gridColumn11,
            this.gridColumn2,
            this.gridColumn12,
            this.gridColumn1,
            this.gridColumn5,
            this.gridColumn6});
            styleFormatCondition3.Appearance.ForeColor = System.Drawing.Color.Red;
            styleFormatCondition3.Appearance.Options.UseForeColor = true;
            styleFormatCondition3.ApplyToRow = true;
            styleFormatCondition3.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
            styleFormatCondition3.Expression = "[IS_EFFECT] == \'未通过\'";
            styleFormatCondition4.Appearance.ForeColor = System.Drawing.Color.Red;
            styleFormatCondition4.Appearance.Options.UseForeColor = true;
            styleFormatCondition4.Column = this.gridColumn1;
            styleFormatCondition4.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
            styleFormatCondition4.Expression = "[EXP_DATE] != [STOCK_EXP_DATE]";
            this.gridView2.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition3,
            styleFormatCondition4});
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.GroupCount = 1;
            this.gridView2.ID = "e5e4d0d7-3f5b-40e1-b8b3-30d6f0bf5f09";
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsView.ColumnAutoWidth = false;
            this.gridView2.OptionsView.EnableAppearanceOddRow = true;
            this.gridView2.OptionsView.ShowFooter = true;
            this.gridView2.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn4, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "货位";
            this.gridColumn7.FieldName = "LC_CODE";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 0;
            this.gridColumn7.Width = 123;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "物料编码";
            this.gridColumn3.FieldName = "SKU_CODE";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 55;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "物料";
            this.gridColumn8.FieldName = "SKU_NAME";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 2;
            this.gridColumn8.Width = 196;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "实盘数量";
            this.gridColumn10.FieldName = "COUNT_QTY";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 3;
            this.gridColumn10.Width = 85;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "货区";
            this.gridColumn4.FieldName = "ZN_NAME";
            this.gridColumn4.Name = "gridColumn4";
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "当前库存";
            this.gridColumn11.FieldName = "STOCK_QTY";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 4;
            this.gridColumn11.Width = 89;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "单位";
            this.gridColumn2.FieldName = "UM_NAME";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 5;
            this.gridColumn2.Width = 50;
            // 
            // gridColumn12
            // 
            this.gridColumn12.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.gridColumn12.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn12.Caption = "差异";
            this.gridColumn12.FieldName = "DIFF_QTY";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gridColumn12.UnboundExpression = "[COUNT_QTY] - [STOCK_QTY]";
            this.gridColumn12.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 6;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "审核状态";
            this.gridColumn5.FieldName = "IS_EFFECT";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 8;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "库存效期";
            this.gridColumn6.DisplayFormat.FormatString = "d";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn6.FieldName = "STOCK_EXP_DATE";
            this.gridColumn6.Name = "gridColumn6";
            // 
            // sbFlash
            // 
            this.sbFlash.Location = new System.Drawing.Point(340, 15);
            this.sbFlash.Name = "sbFlash";
            this.sbFlash.Size = new System.Drawing.Size(89, 31);
            this.sbFlash.TabIndex = 6;
            this.sbFlash.Text = "刷新";
            this.sbFlash.Click += new System.EventHandler(this.sbFlash_Click);
            // 
            // FrmCountExecute
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(883, 479);
            this.Controls.Add(this.gridControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "FrmCountExecute";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "盘点差异调整";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listBillNO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LookUpEdit listBillNO;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnCreate;
        private DevExpress.XtraEditors.SimpleButton sbFlash;
    }
}