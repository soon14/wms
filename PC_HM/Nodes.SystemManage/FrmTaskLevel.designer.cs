namespace Nodes.SystemManage
{
    partial class FrmTaskLevel
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
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.editBeginTime = new DevExpress.XtraEditors.TimeEdit();
            this.editEndTime = new DevExpress.XtraEditors.TimeEdit();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.bindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editBeginTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editEndTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(15, 24);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 12);
            this.labelControl3.TabIndex = 1;
            this.labelControl3.Text = "开始时段：";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(210, 24);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 12);
            this.labelControl4.TabIndex = 1;
            this.labelControl4.Text = "结束时段：";
            // 
            // editBeginTime
            // 
            this.editBeginTime.EditValue = new System.DateTime(2015, 8, 19, 0, 0, 0, 0);
            this.editBeginTime.Location = new System.Drawing.Point(74, 20);
            this.editBeginTime.Name = "editBeginTime";
            this.editBeginTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.editBeginTime.Properties.DisplayFormat.FormatString = "HH:mm";
            this.editBeginTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.editBeginTime.Properties.EditFormat.FormatString = "HH:mm";
            this.editBeginTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.editBeginTime.Size = new System.Drawing.Size(126, 20);
            this.editBeginTime.TabIndex = 6;
            // 
            // editEndTime
            // 
            this.editEndTime.EditValue = new System.DateTime(2015, 8, 19, 0, 0, 0, 0);
            this.editEndTime.Location = new System.Drawing.Point(269, 20);
            this.editEndTime.Name = "editEndTime";
            this.editEndTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.editEndTime.Properties.DisplayFormat.FormatString = "HH:mm";
            this.editEndTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.editEndTime.Properties.EditFormat.FormatString = "HH:mm";
            this.editEndTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.editEndTime.Size = new System.Drawing.Size(126, 20);
            this.editEndTime.TabIndex = 6;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(515, 19);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 4;
            this.btnClear.Tag = "清空";
            this.btnClear.Text = "清空";
            this.btnClear.Click += new System.EventHandler(this.button_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(417, 19);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Tag = "保存";
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.button_Click);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(15, 59);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gridControl2);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gridControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(727, 271);
            this.splitContainerControl1.SplitterPosition = 161;
            this.splitContainerControl1.TabIndex = 9;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // gridControl2
            // 
            this.gridControl2.DataSource = this.bindingSource2;
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(0, 0);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBox1});
            this.gridControl2.Size = new System.Drawing.Size(727, 161);
            this.gridControl2.TabIndex = 10;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn6,
            this.gridColumn7});
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.ID = "d9e4b9bd-556b-4381-8c0a-b68c4f72388c";
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = true;
            this.gridView2.OptionsView.EnableAppearanceOddRow = true;
            this.gridView2.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn6, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "任务类型";
            this.gridColumn6.FieldName = "ItemDesc";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 0;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "任务优先级";
            this.gridColumn7.ColumnEdit = this.repositoryItemComboBox1;
            this.gridColumn7.FieldName = "Level";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = true;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 1;
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            this.repositoryItemComboBox1.NullValuePromptShowForEmptyValue = true;
            this.repositoryItemComboBox1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bindingSource1;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(727, 105);
            this.gridControl1.TabIndex = 8;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn5,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.ID = "3d41fd63-8922-4697-acdf-0b2d2f902cc5";
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn3, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "主键ID";
            this.gridColumn5.FieldName = "ID";
            this.gridColumn5.Name = "gridColumn5";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "任务类型";
            this.gridColumn1.FieldName = "TaskTypeDesc";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "优先级";
            this.gridColumn2.FieldName = "TaskLevel";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "开始时段";
            this.gridColumn3.DisplayFormat.FormatString = "HH:mm";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn3.FieldName = "BeginTime";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "结束时段";
            this.gridColumn4.DisplayFormat.FormatString = "HH:mm";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn4.FieldName = "EndTime";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl1.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(15, 337);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(727, 26);
            this.labelControl1.TabIndex = 10;
            this.labelControl1.Text = "注：优先级数值越大，代表优先级越高！";
            // 
            // FrmTaskLevel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(754, 368);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.editEndTime);
            this.Controls.Add(this.editBeginTime);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.btnClear);
            this.Name = "FrmTaskLevel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "任务优先级";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editBeginTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editEndTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.TimeEdit editBeginTime;
        private DevExpress.XtraEditors.TimeEdit editEndTime;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private System.Windows.Forms.BindingSource bindingSource2;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}