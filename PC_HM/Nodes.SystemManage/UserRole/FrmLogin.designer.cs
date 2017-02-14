namespace Nodes.SystemManage
{
    partial class FrmLogin
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtPwd = new DevExpress.XtraEditors.TextEdit();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.txtID = new DevExpress.XtraEditors.ButtonEdit();
            this.btnSettings = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem9 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem7 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem8 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem10 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem11 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.loadingCircle1 = new Nodes.UI.LoadingCircle();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(441, 187);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(59, 26);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.Click += new System.EventHandler(this.OnCancel);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.txtPwd);
            this.layoutControl1.Controls.Add(this.btnOK);
            this.layoutControl1.Controls.Add(this.loadingCircle1);
            this.layoutControl1.Controls.Add(this.txtID);
            this.layoutControl1.Controls.Add(this.btnSettings);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.ID = "84a49fef-e81f-4d9c-babe-52ea71fcc717";
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(844, 185, 250, 350);
            this.layoutControl1.OptionsFocus.EnableAutoTabOrder = false;
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(522, 287);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtPwd
            // 
            this.txtPwd.EditValue = "";
            this.txtPwd.Location = new System.Drawing.Point(290, 117);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Properties.MaxLength = 20;
            this.txtPwd.Properties.NullValuePrompt = "不区分大小写";
            this.txtPwd.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtPwd.Properties.PasswordChar = '*';
            this.txtPwd.Properties.UseSystemPasswordChar = true;
            this.txtPwd.Size = new System.Drawing.Size(210, 20);
            this.txtPwd.StyleController = this.layoutControl1;
            this.txtPwd.TabIndex = 1;
            this.txtPwd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnPasswordKeyDown);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(378, 187);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(59, 26);
            this.btnOK.StyleController = this.layoutControl1;
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "登录(&O)";
            this.btnOK.Click += new System.EventHandler(this.OnBtnOKClick);
            // 
            // txtID
            // 
            this.txtID.EditValue = "";
            this.txtID.Location = new System.Drawing.Point(290, 93);
            this.txtID.Name = "txtID";
            this.txtID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.txtID.Properties.MaxLength = 20;
            this.txtID.Properties.NullValuePrompt = "帐号或姓名";
            this.txtID.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtID.Size = new System.Drawing.Size(210, 20);
            this.txtID.StyleController = this.layoutControl1;
            this.txtID.TabIndex = 0;
            this.txtID.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtID_ButtonClick);
            this.txtID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnUserKeyDown);
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(12, 255);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(19, 20);
            this.btnSettings.StyleController = this.layoutControl1;
            this.btnSettings.TabIndex = 7;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem6,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.emptySpaceItem3,
            this.layoutControlItem5,
            this.emptySpaceItem4,
            this.emptySpaceItem6,
            this.emptySpaceItem9,
            this.emptySpaceItem5,
            this.emptySpaceItem7,
            this.emptySpaceItem8,
            this.emptySpaceItem10,
            this.emptySpaceItem11,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(522, 287);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtID;
            this.layoutControlItem1.CustomizationFormText = "帐号(&A)";
            this.layoutControlItem1.Location = new System.Drawing.Point(233, 81);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(259, 24);
            this.layoutControlItem1.Text = "帐号(&A)";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(42, 12);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtPwd;
            this.layoutControlItem2.CustomizationFormText = "密码(&P)";
            this.layoutControlItem2.Location = new System.Drawing.Point(233, 105);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(259, 24);
            this.layoutControlItem2.Text = "密码(&P)";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(42, 12);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnCancel;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(429, 175);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(63, 26);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(63, 30);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(233, 129);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(259, 12);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(233, 165);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(259, 10);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(233, 175);
            this.emptySpaceItem3.MaxSize = new System.Drawing.Size(0, 30);
            this.emptySpaceItem3.MinSize = new System.Drawing.Size(10, 30);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(133, 30);
            this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnOK;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(366, 175);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(63, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(63, 30);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.CustomizationFormText = "emptySpaceItem4";
            this.emptySpaceItem4.Location = new System.Drawing.Point(233, 141);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(145, 24);
            this.emptySpaceItem4.Text = "emptySpaceItem4";
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem6
            // 
            this.emptySpaceItem6.AllowHotTrack = false;
            this.emptySpaceItem6.CustomizationFormText = "emptySpaceItem6";
            this.emptySpaceItem6.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem6.Name = "emptySpaceItem6";
            this.emptySpaceItem6.Size = new System.Drawing.Size(233, 205);
            this.emptySpaceItem6.Text = "emptySpaceItem6";
            this.emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem9
            // 
            this.emptySpaceItem9.AllowHotTrack = false;
            this.emptySpaceItem9.CustomizationFormText = "emptySpaceItem9";
            this.emptySpaceItem9.Location = new System.Drawing.Point(233, 0);
            this.emptySpaceItem9.Name = "emptySpaceItem9";
            this.emptySpaceItem9.Size = new System.Drawing.Size(259, 81);
            this.emptySpaceItem9.Text = "emptySpaceItem9";
            this.emptySpaceItem9.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.CustomizationFormText = "emptySpaceItem5";
            this.emptySpaceItem5.Location = new System.Drawing.Point(492, 0);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(10, 205);
            this.emptySpaceItem5.Text = "emptySpaceItem5";
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem7
            // 
            this.emptySpaceItem7.AllowHotTrack = false;
            this.emptySpaceItem7.CustomizationFormText = "emptySpaceItem7";
            this.emptySpaceItem7.Location = new System.Drawing.Point(482, 141);
            this.emptySpaceItem7.Name = "emptySpaceItem7";
            this.emptySpaceItem7.Size = new System.Drawing.Size(10, 24);
            this.emptySpaceItem7.Text = "emptySpaceItem7";
            this.emptySpaceItem7.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem8
            // 
            this.emptySpaceItem8.AllowHotTrack = false;
            this.emptySpaceItem8.CustomizationFormText = "emptySpaceItem8";
            this.emptySpaceItem8.Location = new System.Drawing.Point(233, 205);
            this.emptySpaceItem8.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem8.Name = "emptySpaceItem8";
            this.emptySpaceItem8.Size = new System.Drawing.Size(269, 62);
            this.emptySpaceItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem8.Text = "emptySpaceItem8";
            this.emptySpaceItem8.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem10
            // 
            this.emptySpaceItem10.AllowHotTrack = false;
            this.emptySpaceItem10.CustomizationFormText = "emptySpaceItem10";
            this.emptySpaceItem10.Location = new System.Drawing.Point(0, 205);
            this.emptySpaceItem10.Name = "emptySpaceItem10";
            this.emptySpaceItem10.Size = new System.Drawing.Size(233, 38);
            this.emptySpaceItem10.Text = "emptySpaceItem10";
            this.emptySpaceItem10.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem11
            // 
            this.emptySpaceItem11.AllowHotTrack = false;
            this.emptySpaceItem11.CustomizationFormText = "emptySpaceItem11";
            this.emptySpaceItem11.Location = new System.Drawing.Point(23, 243);
            this.emptySpaceItem11.Name = "emptySpaceItem11";
            this.emptySpaceItem11.Size = new System.Drawing.Size(210, 24);
            this.emptySpaceItem11.Text = "emptySpaceItem11";
            this.emptySpaceItem11.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnSettings;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 243);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(23, 24);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // loadingCircle1
            // 
            this.loadingCircle1.Active = false;
            this.loadingCircle1.Color = System.Drawing.Color.Green;
            this.loadingCircle1.InnerCircleRadius = 6;
            this.loadingCircle1.Location = new System.Drawing.Point(390, 153);
            this.loadingCircle1.Name = "loadingCircle1";
            this.loadingCircle1.NumberSpoke = 9;
            this.loadingCircle1.OuterCircleRadius = 7;
            this.loadingCircle1.RotationSpeed = 100;
            this.loadingCircle1.Size = new System.Drawing.Size(100, 20);
            this.loadingCircle1.SpokeThickness = 4;
            this.loadingCircle1.TabIndex = 5;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.ContentVisible = false;
            this.layoutControlItem4.Control = this.loadingCircle1;
            this.layoutControlItem4.CustomizationFormText = "进度";
            this.layoutControlItem4.Location = new System.Drawing.Point(378, 141);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(104, 24);
            this.layoutControlItem4.Text = "进度";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // FrmLogin
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::Nodes.SystemManage.Properties.Resources.login;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(522, 287);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "欢迎使用WMS";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtPwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private Nodes.UI.LoadingCircle loadingCircle1;
        private DevExpress.XtraEditors.TextEdit txtPwd;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraEditors.ButtonEdit txtID;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem9;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem8;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem10;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem11;
        private DevExpress.XtraEditors.SimpleButton btnSettings;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;

    }
}

