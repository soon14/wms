namespace Nodes.Outstore
{
    partial class FrmModifyAmount
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
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.txtCrnAmount = new DevExpress.XtraEditors.TextEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lblBillNo = new DevExpress.XtraEditors.LabelControl();
            this.lblDriver = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtCrnAmount.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(126, 171);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(79, 25);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(218, 171);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(79, 25);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtCrnAmount
            // 
            this.txtCrnAmount.Location = new System.Drawing.Point(126, 107);
            this.txtCrnAmount.Name = "txtCrnAmount";
            this.txtCrnAmount.Size = new System.Drawing.Size(137, 20);
            this.txtCrnAmount.TabIndex = 5;
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(36, 110);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(84, 12);
            this.labelControl10.TabIndex = 4;
            this.labelControl10.Text = "本次退货金额：";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(60, 28);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 12);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "单据编号：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(60, 69);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 12);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "退货司机：";
            // 
            // lblBillNo
            // 
            this.lblBillNo.Location = new System.Drawing.Point(126, 28);
            this.lblBillNo.Name = "lblBillNo";
            this.lblBillNo.Size = new System.Drawing.Size(0, 12);
            this.lblBillNo.TabIndex = 1;
            // 
            // lblDriver
            // 
            this.lblDriver.Location = new System.Drawing.Point(126, 69);
            this.lblDriver.Name = "lblDriver";
            this.lblDriver.Size = new System.Drawing.Size(0, 12);
            this.lblDriver.TabIndex = 3;
            // 
            // FrmModifyAmount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 208);
            this.Controls.Add(this.txtCrnAmount);
            this.Controls.Add(this.lblDriver);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.lblBillNo);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl10);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmModifyAmount";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改退货金额";
            this.Load += new System.EventHandler(this.FrmModifyAmount_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtCrnAmount.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.TextEdit txtCrnAmount;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl lblBillNo;
        private DevExpress.XtraEditors.LabelControl lblDriver;
    }
}