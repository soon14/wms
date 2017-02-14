namespace Nodes.Common
{
    partial class FrmInputNumeral
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
            this.txtValue = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtValue.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(236, 135);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(93, 34);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnEnter
            // 
            this.btnEnter.Location = new System.Drawing.Point(137, 135);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(93, 34);
            this.btnEnter.TabIndex = 1;
            this.btnEnter.Text = "确认";
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // txtValue
            // 
            this.txtValue.EditValue = "0";
            this.txtValue.Location = new System.Drawing.Point(12, 85);
            this.txtValue.Name = "txtValue";
            this.txtValue.Properties.Appearance.Font = new System.Drawing.Font("宋体", 12F);
            this.txtValue.Properties.Appearance.Options.UseFont = true;
            this.txtValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtValue.Size = new System.Drawing.Size(317, 25);
            this.txtValue.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("宋体", 12F);
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(12, 57);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(317, 22);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "请输入数量";
            // 
            // FrmInputNumeral
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(341, 204);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmInputNumeral";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "输入窗口";
            ((System.ComponentModel.ISupportInitialize)(this.txtValue.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnEnter;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtValue;
    }
}