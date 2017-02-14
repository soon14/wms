namespace Nodes.Outstore
{
    partial class FrmConfirmAmount
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
            this.txtReceiveAmount = new DevExpress.XtraEditors.TextEdit();
            this.lblCustomer = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lblBillNo = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.txtRealAmount = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtCrnAmount = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtOtherAmount = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.lblContract = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.lblVehicle = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtOtherRemark = new DevExpress.XtraEditors.TextEdit();
            this.chkPaymentFlag = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiveAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRealAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCrnAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOtherAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOtherRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPaymentFlag.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtReceiveAmount
            // 
            this.txtReceiveAmount.Enabled = false;
            this.txtReceiveAmount.Location = new System.Drawing.Point(71, 36);
            this.txtReceiveAmount.Name = "txtReceiveAmount";
            this.txtReceiveAmount.Size = new System.Drawing.Size(137, 20);
            this.txtReceiveAmount.TabIndex = 1;
            // 
            // lblCustomer
            // 
            this.lblCustomer.Location = new System.Drawing.Point(80, 47);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(0, 12);
            this.lblCustomer.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(14, 46);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 12);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "客户名称：";
            // 
            // lblBillNo
            // 
            this.lblBillNo.Location = new System.Drawing.Point(80, 13);
            this.lblBillNo.Name = "lblBillNo";
            this.lblBillNo.Size = new System.Drawing.Size(0, 12);
            this.lblBillNo.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(14, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 12);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "单据编号：";
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(5, 39);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(60, 12);
            this.labelControl10.TabIndex = 0;
            this.labelControl10.Text = "应收金额：";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(306, 266);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(79, 25);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(391, 266);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(79, 25);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtRealAmount
            // 
            this.txtRealAmount.Location = new System.Drawing.Point(321, 36);
            this.txtRealAmount.Name = "txtRealAmount";
            this.txtRealAmount.Size = new System.Drawing.Size(137, 20);
            this.txtRealAmount.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(254, 39);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 12);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "实收现金：";
            // 
            // txtCrnAmount
            // 
            this.txtCrnAmount.Location = new System.Drawing.Point(71, 79);
            this.txtCrnAmount.Name = "txtCrnAmount";
            this.txtCrnAmount.Size = new System.Drawing.Size(137, 20);
            this.txtCrnAmount.TabIndex = 5;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(5, 82);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 12);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "退货金额：";
            // 
            // txtOtherAmount
            // 
            this.txtOtherAmount.Location = new System.Drawing.Point(321, 79);
            this.txtOtherAmount.Name = "txtOtherAmount";
            this.txtOtherAmount.Size = new System.Drawing.Size(137, 20);
            this.txtOtherAmount.TabIndex = 7;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(250, 82);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(60, 12);
            this.labelControl5.TabIndex = 6;
            this.labelControl5.Text = "它项金额：";
            // 
            // lblContract
            // 
            this.lblContract.Location = new System.Drawing.Point(328, 79);
            this.lblContract.Name = "lblContract";
            this.lblContract.Size = new System.Drawing.Size(0, 12);
            this.lblContract.TabIndex = 7;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(275, 79);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(48, 12);
            this.labelControl7.TabIndex = 6;
            this.labelControl7.Text = "收货人：";
            // 
            // lblVehicle
            // 
            this.lblVehicle.Location = new System.Drawing.Point(79, 79);
            this.lblVehicle.Name = "lblVehicle";
            this.lblVehicle.Size = new System.Drawing.Size(0, 12);
            this.lblVehicle.TabIndex = 5;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(26, 79);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(48, 12);
            this.labelControl8.TabIndex = 4;
            this.labelControl8.Text = "车牌号：";
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl10);
            this.groupControl1.Controls.Add(this.txtReceiveAmount);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtRealAmount);
            this.groupControl1.Controls.Add(this.txtOtherAmount);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.txtOtherRemark);
            this.groupControl1.Controls.Add(this.txtCrnAmount);
            this.groupControl1.Location = new System.Drawing.Point(9, 107);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(466, 153);
            this.groupControl1.TabIndex = 9;
            this.groupControl1.Text = "输入项";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(5, 125);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(60, 12);
            this.labelControl6.TabIndex = 8;
            this.labelControl6.Text = "它项备注：";
            // 
            // txtOtherRemark
            // 
            this.txtOtherRemark.Location = new System.Drawing.Point(70, 122);
            this.txtOtherRemark.Name = "txtOtherRemark";
            this.txtOtherRemark.Size = new System.Drawing.Size(388, 20);
            this.txtOtherRemark.TabIndex = 9;
            // 
            // chkPaymentFlag
            // 
            this.chkPaymentFlag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkPaymentFlag.Location = new System.Drawing.Point(12, 272);
            this.chkPaymentFlag.Name = "chkPaymentFlag";
            this.chkPaymentFlag.Properties.Caption = "未收款标记";
            this.chkPaymentFlag.Size = new System.Drawing.Size(102, 19);
            this.chkPaymentFlag.TabIndex = 11;
            // 
            // FrmConfirmAmount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 303);
            this.Controls.Add(this.chkPaymentFlag);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.lblVehicle);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.lblContract);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.lblCustomer);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.lblBillNo);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmConfirmAmount";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "回款确认";
            this.Load += new System.EventHandler(this.FrmConfirmAmount_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiveAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRealAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCrnAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOtherAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOtherRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPaymentFlag.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtReceiveAmount;
        private DevExpress.XtraEditors.LabelControl lblCustomer;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl lblBillNo;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.TextEdit txtRealAmount;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtCrnAmount;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtOtherAmount;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl lblContract;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl lblVehicle;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtOtherRemark;
        private DevExpress.XtraEditors.CheckEdit chkPaymentFlag;
    }
}