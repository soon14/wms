namespace Nodes.WMS.Inbound
{
    partial class FrmEditAsn
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
            this.colorBack = new DevExpress.XtraEditors.ColorEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.colorBack.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // colorBack
            // 
            this.colorBack.EditValue = System.Drawing.Color.Empty;
            this.colorBack.Location = new System.Drawing.Point(90, 20);
            this.colorBack.Name = "colorBack";
            this.colorBack.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.colorBack.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.colorBack_Properties_ButtonClick);
            this.colorBack.Size = new System.Drawing.Size(294, 20);
            this.colorBack.TabIndex = 1;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(202, 162);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(88, 27);
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.Text = "确定";
            this.simpleButton1.Click += new System.EventHandler(this.OnOKClick);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(296, 162);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 27);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(29, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 12);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "字体颜色";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(29, 51);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 12);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "备注";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(90, 49);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Properties.MaxLength = 200;
            this.txtRemark.Size = new System.Drawing.Size(294, 96);
            this.txtRemark.TabIndex = 3;
            // 
            // FrmEditAsn
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(408, 201);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.colorBack);
            this.Name = "FrmEditAsn";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "修改单据信息";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.colorBack.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ColorEdit colorBack;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.MemoEdit txtRemark;
    }
}