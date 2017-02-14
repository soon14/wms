namespace Nodes.SystemManage
{
    partial class FrmOptions
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
            this.optionsControl = new DevExpress.XtraBars.Ribbon.BackstageViewControl();
            this.backstageViewClientControl1 = new DevExpress.XtraBars.Ribbon.BackstageViewClientControl();
            this.backstageViewTabItem1 = new DevExpress.XtraBars.Ribbon.BackstageViewTabItem();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.optionsControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // optionsControl
            // 
            this.optionsControl.ColorScheme = DevExpress.XtraBars.Ribbon.RibbonControlColorScheme.Yellow;
            this.optionsControl.Controls.Add(this.backstageViewClientControl1);
            this.optionsControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.optionsControl.Items.Add(this.backstageViewTabItem1);
            this.optionsControl.Location = new System.Drawing.Point(0, 0);
            this.optionsControl.Name = "optionsControl";
            this.optionsControl.SelectedTab = null;
            this.optionsControl.Size = new System.Drawing.Size(677, 315);
            this.optionsControl.TabIndex = 0;
            this.optionsControl.Text = "backstageViewControl1";
            this.optionsControl.SelectedTabChanged += new DevExpress.XtraBars.Ribbon.BackstageViewItemEventHandler(this.backstageViewControl1_SelectedTabChanged);
            // 
            // backstageViewClientControl1
            // 
            this.backstageViewClientControl1.Name = "backstageViewClientControl1";
            this.backstageViewClientControl1.TabIndex = 0;
            // 
            // backstageViewTabItem1
            // 
            this.backstageViewTabItem1.Caption = "外观设置";
            this.backstageViewTabItem1.ContentControl = this.backstageViewClientControl1;
            this.backstageViewTabItem1.Name = "backstageViewTabItem1";
            this.backstageViewTabItem1.Selected = false;
            this.backstageViewTabItem1.Tag = "外观设置";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(563, 337);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 29);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消(&C)";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(294, 337);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(87, 29);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.Click += new System.EventHandler(this.OnOKClick);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.Location = new System.Drawing.Point(470, 337);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(87, 29);
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.Text = "应用(&A)";
            this.simpleButton1.Click += new System.EventHandler(this.OnApplyClick);
            // 
            // FrmOptions
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(677, 378);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.optionsControl);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.btnOK);
            this.MaximizeBox = false;
            this.Name = "FrmOptions";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统设置";
            this.Load += new System.EventHandler(this.FrmOptions_Load);
            this.optionsControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.BackstageViewControl optionsControl;
        private DevExpress.XtraBars.Ribbon.BackstageViewClientControl backstageViewClientControl1;
        private DevExpress.XtraBars.Ribbon.BackstageViewTabItem backstageViewTabItem1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;


    }
}