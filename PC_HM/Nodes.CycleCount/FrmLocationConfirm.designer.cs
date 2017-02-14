namespace Nodes.CycleCount
{
    partial class FrmLocationConfirm
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cboCountTag = new DevExpress.XtraEditors.LookUpEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.rdoDiffDate = new DevExpress.XtraEditors.CheckEdit();
            this.rdoDiffQty = new DevExpress.XtraEditors.CheckEdit();
            this.rdoAll = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboCountTag.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdoDiffDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoDiffQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoAll.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 61);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(578, 237);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupCount = 1;
            this.gridView1.ID = "87dc28ac-167f-4a06-9048-4b2529cc89e5";
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn1, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "货区";
            this.gridColumn1.FieldName = "ZoneName";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "货位";
            this.gridColumn2.FieldName = "LocationCode";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 131;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.cboCountTag);
            this.panelControl1.Controls.Add(this.btnCancel);
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 298);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(578, 61);
            this.panelControl1.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl1.Appearance.Font = new System.Drawing.Font("宋体", 12F);
            this.labelControl1.Location = new System.Drawing.Point(11, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(32, 16);
            this.labelControl1.TabIndex = 15;
            this.labelControl1.Text = "标签";
            // 
            // cboCountTag
            // 
            this.cboCountTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboCountTag.Location = new System.Drawing.Point(49, 20);
            this.cboCountTag.Name = "cboCountTag";
            this.cboCountTag.Properties.Appearance.Font = new System.Drawing.Font("宋体", 12F);
            this.cboCountTag.Properties.Appearance.Options.UseFont = true;
            this.cboCountTag.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboCountTag.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ItemDesc", "盘点标签")});
            this.cboCountTag.Properties.DisplayMember = "ItemDesc";
            this.cboCountTag.Properties.NullText = "";
            this.cboCountTag.Properties.NullValuePrompt = "选择盘点标签";
            this.cboCountTag.Properties.NullValuePromptShowForEmptyValue = true;
            this.cboCountTag.Size = new System.Drawing.Size(262, 25);
            this.cboCountTag.TabIndex = 14;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(456, 16);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 29);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(355, 16);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 29);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "确定保存";
            this.btnSave.Click += new System.EventHandler(this.OnSaveClick);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.rdoDiffDate);
            this.panelControl2.Controls.Add(this.rdoDiffQty);
            this.panelControl2.Controls.Add(this.rdoAll);
            this.panelControl2.Controls.Add(this.labelControl2);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(578, 61);
            this.panelControl2.TabIndex = 3;
            // 
            // rdoDiffDate
            // 
            this.rdoDiffDate.Location = new System.Drawing.Point(231, 24);
            this.rdoDiffDate.Name = "rdoDiffDate";
            this.rdoDiffDate.Properties.Caption = "效期差异";
            this.rdoDiffDate.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.rdoDiffDate.Properties.RadioGroupIndex = 0;
            this.rdoDiffDate.Size = new System.Drawing.Size(74, 19);
            this.rdoDiffDate.TabIndex = 1;
            this.rdoDiffDate.CheckedChanged += new System.EventHandler(this.checkEdit_CheckedChanged);
            // 
            // rdoDiffQty
            // 
            this.rdoDiffQty.Location = new System.Drawing.Point(143, 24);
            this.rdoDiffQty.Name = "rdoDiffQty";
            this.rdoDiffQty.Properties.Caption = "数量差异";
            this.rdoDiffQty.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.rdoDiffQty.Properties.RadioGroupIndex = 0;
            this.rdoDiffQty.Size = new System.Drawing.Size(77, 19);
            this.rdoDiffQty.TabIndex = 1;
            this.rdoDiffQty.CheckedChanged += new System.EventHandler(this.checkEdit_CheckedChanged);
            // 
            // rdoAll
            // 
            this.rdoAll.EditValue = true;
            this.rdoAll.Location = new System.Drawing.Point(76, 24);
            this.rdoAll.Name = "rdoAll";
            this.rdoAll.Properties.Caption = "全部";
            this.rdoAll.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.rdoAll.Properties.RadioGroupIndex = 0;
            this.rdoAll.Size = new System.Drawing.Size(56, 19);
            this.rdoAll.TabIndex = 1;
            this.rdoAll.CheckedChanged += new System.EventHandler(this.checkEdit_CheckedChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(13, 27);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 12);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "筛选条件";
            // 
            // FrmLocationConfirm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(578, 359);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "FrmLocationConfirm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "货位确认";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboCountTag.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdoDiffDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoDiffQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoAll.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit cboCountTag;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.CheckEdit rdoDiffDate;
        private DevExpress.XtraEditors.CheckEdit rdoDiffQty;
        private DevExpress.XtraEditors.CheckEdit rdoAll;
    }
}