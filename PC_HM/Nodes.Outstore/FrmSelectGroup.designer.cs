namespace Nodes.Outstore
{
    partial class FrmSelectGroup
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
            this.btnCommit = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cboGroupNo = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.cboGroupNo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCommit
            // 
            this.btnCommit.Location = new System.Drawing.Point(33, 135);
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Size = new System.Drawing.Size(78, 26);
            this.btnCommit.TabIndex = 1;
            this.btnCommit.Text = "确定";
            this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(157, 135);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(78, 26);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(33, 68);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(96, 12);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "请选择分组编码：";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.labelControl2.Location = new System.Drawing.Point(33, 12);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(162, 12);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "注：目前系统最大支持8个分组";
            // 
            // cboGroupNo
            // 
            this.cboGroupNo.Location = new System.Drawing.Point(135, 65);
            this.cboGroupNo.Name = "cboGroupNo";
            this.cboGroupNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboGroupNo.Properties.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H"});
            this.cboGroupNo.Size = new System.Drawing.Size(100, 20);
            this.cboGroupNo.TabIndex = 3;
            // 
            // FrmSelectGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 179);
            this.Controls.Add(this.cboGroupNo);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCommit);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSelectGroup";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择订单分组";
            this.Load += new System.EventHandler(this.FrmSelectGroup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cboGroupNo.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCommit;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit cboGroupNo;
    }
}