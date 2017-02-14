namespace Nodes.Common
{
    partial class FrmChoosePersonnel
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
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnEnter = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cboPersonnel = new DevExpress.XtraEditors.LookUpEdit();
            this.bindingSource1 = new System.Windows.Forms.BindingSource();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.chkCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.cboPersonnel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(240, 143);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 32);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "关闭";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnEnter
            // 
            this.btnEnter.Location = new System.Drawing.Point(142, 143);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(92, 32);
            this.btnEnter.TabIndex = 2;
            this.btnEnter.Text = "确认";
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("宋体", 12F);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(12, 43);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(321, 25);
            this.labelControl1.TabIndex = 13;
            this.labelControl1.Text = "请选择人员";
            // 
            // cboPersonnel
            // 
            this.cboPersonnel.Location = new System.Drawing.Point(11, 74);
            this.cboPersonnel.Name = "cboPersonnel";
            this.cboPersonnel.Properties.Appearance.Font = new System.Drawing.Font("宋体", 12F);
            this.cboPersonnel.Properties.Appearance.Options.UseFont = true;
            this.cboPersonnel.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPersonnel.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("UserName", 100, "人员姓名")});
            this.cboPersonnel.Properties.DataSource = this.bindingSource1;
            this.cboPersonnel.Properties.DisplayMember = "UserName";
            this.cboPersonnel.Properties.NullText = "";
            this.cboPersonnel.Properties.NullValuePrompt = "选择人员";
            this.cboPersonnel.Properties.NullValuePromptShowForEmptyValue = true;
            this.cboPersonnel.Size = new System.Drawing.Size(321, 25);
            this.cboPersonnel.TabIndex = 1;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bindingSource1;
            this.gridControl1.Location = new System.Drawing.Point(11, 219);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(321, 128);
            this.gridControl1.TabIndex = 15;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1,
            this.gridView2});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.chkCheck,
            this.gridColumn2});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never;
            this.gridView1.ID = "b1e38fdd-cf58-4f0d-9435-d54bf500df4a";
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = true;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            // 
            // chkCheck
            // 
            this.chkCheck.Caption = "选中";
            this.chkCheck.FieldName = "HasChecked";
            this.chkCheck.Name = "chkCheck";
            this.chkCheck.OptionsColumn.AllowEdit = true;
            this.chkCheck.OptionsColumn.AllowMove = false;
            this.chkCheck.OptionsColumn.AllowShowHide = false;
            this.chkCheck.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.chkCheck.OptionsColumn.FixedWidth = true;
            this.chkCheck.Visible = true;
            this.chkCheck.VisibleIndex = 0;
            this.chkCheck.Width = 70;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "用户名称";
            this.gridColumn2.FieldName = "UserName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 200;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridControl1;
            this.gridView2.ID = "1b650d2d-2917-429f-a3c2-6be2d22ab8aa";
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsView.EnableAppearanceOddRow = true;
            // 
            // FrmChoosePersonnel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(354, 359);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.cboPersonnel);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmChoosePersonnel";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择人员";
            ((System.ComponentModel.ISupportInitialize)(this.cboPersonnel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnEnter;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit cboPersonnel;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn chkCheck;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
    }
}