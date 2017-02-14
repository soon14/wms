namespace Nodes.WMS.Stock
{
    partial class FrmStockOccupyManager
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
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition2 = new DevExpress.XtraGrid.StyleFormatCondition();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dateEditTo = new DevExpress.XtraEditors.DateEdit();
            this.dateEditFrom = new DevExpress.XtraEditors.DateEdit();
            this.btnInventory = new DevExpress.XtraEditors.SimpleButton();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.txtCreator = new DevExpress.XtraEditors.TextEdit();
            this.rdStatus = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreator.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.dateEditTo);
            this.groupControl1.Controls.Add(this.dateEditFrom);
            this.groupControl1.Controls.Add(this.btnInventory);
            this.groupControl1.Controls.Add(this.btnQuery);
            this.groupControl1.Controls.Add(this.txtCreator);
            this.groupControl1.Controls.Add(this.rdStatus);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(972, 85);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "查询条件";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(375, 61);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(12, 12);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "--";
            // 
            // dateEditTo
            // 
            this.dateEditTo.EditValue = null;
            this.dateEditTo.Location = new System.Drawing.Point(393, 57);
            this.dateEditTo.Name = "dateEditTo";
            this.dateEditTo.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dateEditTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditTo.Properties.Mask.EditMask = "";
            this.dateEditTo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dateEditTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditTo.Size = new System.Drawing.Size(127, 20);
            this.dateEditTo.TabIndex = 13;
            // 
            // dateEditFrom
            // 
            this.dateEditFrom.EditValue = null;
            this.dateEditFrom.Location = new System.Drawing.Point(393, 31);
            this.dateEditFrom.Name = "dateEditFrom";
            this.dateEditFrom.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dateEditFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditFrom.Properties.Mask.EditMask = "";
            this.dateEditFrom.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dateEditFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditFrom.Size = new System.Drawing.Size(127, 20);
            this.dateEditFrom.TabIndex = 12;
            // 
            // btnInventory
            // 
            this.btnInventory.Location = new System.Drawing.Point(671, 32);
            this.btnInventory.Name = "btnInventory";
            this.btnInventory.Size = new System.Drawing.Size(78, 24);
            this.btnInventory.TabIndex = 5;
            this.btnInventory.Text = "解除占用";
            this.btnInventory.Click += new System.EventHandler(this.btnInventory_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(587, 31);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(78, 24);
            this.btnQuery.TabIndex = 3;
            this.btnQuery.Text = "查询";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtCreator
            // 
            this.txtCreator.Location = new System.Drawing.Point(83, 30);
            this.txtCreator.Name = "txtCreator";
            this.txtCreator.Properties.MaxLength = 20;
            this.txtCreator.Size = new System.Drawing.Size(226, 20);
            this.txtCreator.TabIndex = 2;
            // 
            // rdStatus
            // 
            this.rdStatus.EditValue = 2;
            this.rdStatus.Location = new System.Drawing.Point(83, 56);
            this.rdStatus.Name = "rdStatus";
            this.rdStatus.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "锁定中"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "已释放"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "不限制")});
            this.rdStatus.Size = new System.Drawing.Size(226, 24);
            this.rdStatus.TabIndex = 1;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(53, 63);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(24, 12);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "状态";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(363, 34);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(24, 12);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "日期";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(41, 33);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 12);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "创建人";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 85);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(972, 318);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn7,
            this.gridColumn11,
            this.gridColumn1});
            styleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.Blue;
            styleFormatCondition1.Appearance.Options.UseForeColor = true;
            styleFormatCondition1.ApplyToRow = true;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
            styleFormatCondition1.Expression = "[DueDays] < 90 And [DueDays] > 0";
            styleFormatCondition2.Appearance.ForeColor = System.Drawing.Color.Red;
            styleFormatCondition2.Appearance.Options.UseForeColor = true;
            styleFormatCondition2.ApplyToRow = true;
            styleFormatCondition2.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
            styleFormatCondition2.Expression = "!IsNullOrEmpty([DueDays]) And [DueDays] <= 0";
            this.gridView1.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1,
            styleFormatCondition2});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupCount = 1;
            this.gridView1.ID = "f45ec8ee-3646-478e-a282-202f4e6d9a97";
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn2, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "货位";
            this.gridColumn2.FieldName = "Location";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Width = 107;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "物料编码";
            this.gridColumn3.FieldName = "Material";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 112;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "物料名称";
            this.gridColumn4.FieldName = "MaterialName";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            this.gridColumn4.Width = 216;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "组分料";
            this.gridColumn5.FieldName = "ComMaterial";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "创建人";
            this.gridColumn6.FieldName = "Creator";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 3;
            this.gridColumn6.Width = 85;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "效期";
            this.gridColumn9.FieldName = "DueDate";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 4;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "批号";
            this.gridColumn10.FieldName = "BatchNO";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 5;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "占用数量";
            this.gridColumn7.FieldName = "OccupyQty";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "状态";
            this.gridColumn11.FieldName = "StatusName";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 7;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "备注";
            this.gridColumn1.FieldName = "Remark";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 8;
            // 
            // FrmStockOccupyManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 403);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.groupControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmStockOccupyManager";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "库存占用管理";
            this.Load += new System.EventHandler(this.FrmStockOccupyManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreator.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnInventory;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraEditors.TextEdit txtCreator;
        private DevExpress.XtraEditors.RadioGroup rdStatus;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dateEditFrom;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit dateEditTo;
    }
}