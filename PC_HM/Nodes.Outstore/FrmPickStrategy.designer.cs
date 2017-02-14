namespace Nodes.Outstore
{
    partial class FrmPickStrategy
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.listPickType = new DevExpress.XtraEditors.LookUpEdit();
            this.txtBillNO = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.listZnType = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.listPickType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBillNO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listZnType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(51, 19);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 12);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "单据编号";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(51, 51);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 12);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "拣货方式";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(157, 138);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(85, 26);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(246, 138);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 26);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消(&C)";
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "货位";
            this.gridColumn11.FieldName = "LocationCode";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 0;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "货区";
            this.gridColumn13.FieldName = "ZoneName";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 1;
            // 
            // listPickType
            // 
            this.listPickType.Location = new System.Drawing.Point(111, 48);
            this.listPickType.Name = "listPickType";
            this.listPickType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.listPickType.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ItemDesc", "拣货方式")});
            this.listPickType.Properties.DisplayMember = "ItemDesc";
            this.listPickType.Properties.DropDownRows = 4;
            this.listPickType.Properties.NullText = "";
            this.listPickType.Properties.ShowFooter = false;
            this.listPickType.Properties.ShowHeader = false;
            this.listPickType.Properties.ValueMember = "ItemValue";
            this.listPickType.Size = new System.Drawing.Size(220, 20);
            this.listPickType.TabIndex = 3;
            this.listPickType.EditValueChanged += new System.EventHandler(this.listPickType_EditValueChanged);
            // 
            // txtBillNO
            // 
            this.txtBillNO.Enabled = false;
            this.txtBillNO.Location = new System.Drawing.Point(111, 16);
            this.txtBillNO.Name = "txtBillNO";
            this.txtBillNO.Size = new System.Drawing.Size(220, 20);
            this.txtBillNO.TabIndex = 1;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(51, 81);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 12);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "拣货区域";
            // 
            // listZnType
            // 
            this.listZnType.Location = new System.Drawing.Point(111, 78);
            this.listZnType.Name = "listZnType";
            this.listZnType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.listZnType.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ItemDesc", "拣货区域")});
            this.listZnType.Properties.DisplayMember = "ItemDesc";
            this.listZnType.Properties.DropDownRows = 4;
            this.listZnType.Properties.NullText = "";
            this.listZnType.Properties.ShowFooter = false;
            this.listZnType.Properties.ShowHeader = false;
            this.listZnType.Properties.ValueMember = "ItemValue";
            this.listZnType.Size = new System.Drawing.Size(220, 20);
            this.listZnType.TabIndex = 5;
            this.listZnType.EditValueChanged += new System.EventHandler(this.listPickType_EditValueChanged);
            // 
            // FrmPickStrategy
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(368, 188);
            this.Controls.Add(this.txtBillNO);
            this.Controls.Add(this.listZnType);
            this.Controls.Add(this.listPickType);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Name = "FrmPickStrategy";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "修改拣货方式";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.listPickType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBillNO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listZnType.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LookUpEdit listPickType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraEditors.TextEdit txtBillNO;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LookUpEdit listZnType;
    }
}