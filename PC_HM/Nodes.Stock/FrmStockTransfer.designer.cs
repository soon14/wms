namespace Nodes.Stock
{
    partial class FrmStockTransfer
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
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.txtMC = new DevExpress.XtraEditors.TextEdit();
            this.txtLocation = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.bindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSearchLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.repositoryItemSearchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLocation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // gridColumn16
            // 
            this.gridColumn16.AppearanceCell.ForeColor = System.Drawing.Color.Green;
            this.gridColumn16.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn16.AppearanceHeader.ForeColor = System.Drawing.Color.Green;
            this.gridColumn16.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn16.Caption = "移出数量";
            this.gridColumn16.FieldName = "TransferQty";
            this.gridColumn16.MaxWidth = 80;
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.OptionsColumn.AllowEdit = true;
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 4;
            this.gridColumn16.Width = 73;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnSave);
            this.groupControl1.Controls.Add(this.btnQuery);
            this.groupControl1.Controls.Add(this.txtMC);
            this.groupControl1.Controls.Add(this.txtLocation);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1204, 70);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "查询条件";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(1081, 33);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(111, 26);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.OnbtnSaveClick);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(469, 33);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(111, 26);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.Text = "查询";
            this.btnQuery.Click += new System.EventHandler(this.OnQueryClick);
            // 
            // txtMC
            // 
            this.txtMC.Location = new System.Drawing.Point(265, 35);
            this.txtMC.Name = "txtMC";
            this.txtMC.Properties.MaxLength = 50;
            this.txtMC.Properties.NullValuePrompt = "支持模糊查询";
            this.txtMC.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtMC.Size = new System.Drawing.Size(178, 20);
            this.txtMC.TabIndex = 3;
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(43, 35);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Properties.MaxLength = 20;
            this.txtLocation.Size = new System.Drawing.Size(151, 20);
            this.txtLocation.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(208, 38);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 12);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "物料名称";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 38);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 12);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "货位";
            // 
            // gridControl1
            // 
            this.gridControl1.AllowDrop = true;
            this.gridControl1.DataSource = this.bindingSource1;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(580, 426);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.DragOver += new System.Windows.Forms.DragEventHandler(this.gridControl2_DragOver);
            this.gridControl1.DragDrop += new System.Windows.Forms.DragEventHandler(this.gridControl1_DragDrop);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn4,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn10,
            this.gridColumn9,
            this.gridColumn12});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.ID = "837b1794-8119-4c1a-9fba-9ab8b6050ed0";
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.Tag = "source";
            this.gridView1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnGridViewMouseMove);
            this.gridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridViewMouseDown);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "所属货区";
            this.gridColumn1.FieldName = "ZoneName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "货位";
            this.gridColumn2.FieldName = "Location";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 100;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "物料名称";
            this.gridColumn4.FieldName = "MaterialName";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 183;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "单位";
            this.gridColumn6.FieldName = "UnitName";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 39;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "总库存";
            this.gridColumn7.DisplayFormat.FormatString = "f0";
            this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn7.FieldName = "Qty";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 3;
            this.gridColumn7.Width = 59;
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
            this.gridColumn8.Width = 59;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "占用中";
            this.gridColumn10.DisplayFormat.FormatString = "f0";
            this.gridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn10.FieldName = "OccupyQty";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 6;
            this.gridColumn10.Width = 60;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "有效期";
            this.gridColumn9.FieldName = "ExpDate";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 7;
            this.gridColumn9.Width = 80;
            // 
            // gridColumn12
            // 
            this.gridColumn12.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn12.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn12.Caption = "剩余过期天数";
            this.gridColumn12.FieldName = "ExpDays";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 8;
            this.gridColumn12.Width = 90;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 70);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gridControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gridControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1204, 426);
            this.splitContainerControl1.SplitterPosition = 580;
            this.splitContainerControl1.TabIndex = 2;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // gridControl2
            // 
            this.gridControl2.AllowDrop = true;
            this.gridControl2.DataSource = this.bindingSource2;
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(0, 0);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSearchLookUpEdit1});
            this.gridControl2.Size = new System.Drawing.Size(619, 426);
            this.gridControl2.TabIndex = 0;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            this.gridControl2.DragOver += new System.Windows.Forms.DragEventHandler(this.gridControl2_DragOver);
            this.gridControl2.DragDrop += new System.Windows.Forms.DragEventHandler(this.gridControl2_DragDrop);
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn14,
            this.gridColumn11,
            this.gridColumn5,
            this.gridColumn16,
            this.gridColumn13});
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.ID = "0adfa9a0-2d52-4f75-9ebe-756ccc946145";
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = true;
            this.gridView2.OptionsSelection.MultiSelect = true;
            this.gridView2.OptionsView.ColumnAutoWidth = false;
            this.gridView2.OptionsView.EnableAppearanceOddRow = true;
            this.gridView2.Tag = "dest";
            this.gridView2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnGridViewMouseMove);
            this.gridView2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridViewMouseDown);
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "从货位";
            this.gridColumn3.FieldName = "Location";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 80;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "物料名称";
            this.gridColumn14.FieldName = "MaterialName";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 1;
            this.gridColumn14.Width = 151;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "单位";
            this.gridColumn11.FieldName = "UnitName";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 2;
            this.gridColumn11.Width = 38;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "可移出量";
            this.gridColumn5.DisplayFormat.FormatString = "f0";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "IdleQty";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
            this.gridColumn5.Width = 65;
            // 
            // gridColumn13
            // 
            this.gridColumn13.AppearanceCell.ForeColor = System.Drawing.Color.Green;
            this.gridColumn13.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn13.AppearanceHeader.ForeColor = System.Drawing.Color.Green;
            this.gridColumn13.AppearanceHeader.Options.UseForeColor = true;
            this.gridColumn13.Caption = "移至";
            this.gridColumn13.ColumnEdit = this.repositoryItemSearchLookUpEdit1;
            this.gridColumn13.FieldName = "TargetLocation";
            this.gridColumn13.MaxWidth = 180;
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = true;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 5;
            this.gridColumn13.Width = 150;
            // 
            // repositoryItemSearchLookUpEdit1
            // 
            this.repositoryItemSearchLookUpEdit1.AutoHeight = false;
            this.repositoryItemSearchLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSearchLookUpEdit1.DisplayMember = "LocationCode";
            this.repositoryItemSearchLookUpEdit1.Name = "repositoryItemSearchLookUpEdit1";
            this.repositoryItemSearchLookUpEdit1.NullText = "请选择目的货位";
            this.repositoryItemSearchLookUpEdit1.PopupFormSize = new System.Drawing.Size(120, 0);
            this.repositoryItemSearchLookUpEdit1.ShowClearButton = false;
            this.repositoryItemSearchLookUpEdit1.ShowFooter = false;
            this.repositoryItemSearchLookUpEdit1.ValueMember = "LocationCode";
            this.repositoryItemSearchLookUpEdit1.View = this.repositoryItemSearchLookUpEdit1View;
            // 
            // repositoryItemSearchLookUpEdit1View
            // 
            this.repositoryItemSearchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn15,
            this.gridColumn17});
            this.repositoryItemSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemSearchLookUpEdit1View.GroupCount = 1;
            this.repositoryItemSearchLookUpEdit1View.ID = "d4424f14-5134-4b6d-b2d8-790debb9ece1";
            this.repositoryItemSearchLookUpEdit1View.Name = "repositoryItemSearchLookUpEdit1View";
            this.repositoryItemSearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = true;
            this.repositoryItemSearchLookUpEdit1View.OptionsView.ShowIndicator = false;
            this.repositoryItemSearchLookUpEdit1View.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn17, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "货位";
            this.gridColumn15.FieldName = "LocationCode";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 0;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "货区";
            this.gridColumn17.FieldName = "ZoneName";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 1;
            // 
            // FrmStockTransfer
            // 
            this.AcceptButton = this.btnQuery;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1204, 496);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.groupControl1);
            this.Name = "FrmStockTransfer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "库存转移";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLocation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).EndInit();
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
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private System.Windows.Forms.BindingSource bindingSource2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repositoryItemSearchLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
    }
}