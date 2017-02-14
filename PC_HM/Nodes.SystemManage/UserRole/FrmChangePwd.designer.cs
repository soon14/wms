namespace Nodes.SystemManage
{
    partial class FrmChangePwd
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.checkShow = new DevExpress.XtraEditors.CheckEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.txtPwdAgain = new DevExpress.XtraEditors.TextEdit();
            this.txtNewPwd = new DevExpress.XtraEditors.TextEdit();
            this.txtCurrentPwd = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkShow.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPwdAgain.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrentPwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.checkShow);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnOK);
            this.layoutControl1.Controls.Add(this.txtPwdAgain);
            this.layoutControl1.Controls.Add(this.txtNewPwd);
            this.layoutControl1.Controls.Add(this.txtCurrentPwd);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.ID = "d523cf5e-0c86-4aae-a195-2b6d0a51f316";
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(950, 151, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(517, 263);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // checkShow
            // 
            this.checkShow.Location = new System.Drawing.Point(12, 219);
            this.checkShow.Name = "checkShow";
            this.checkShow.Properties.Caption = "显示密码";
            this.checkShow.Size = new System.Drawing.Size(115, 19);
            this.checkShow.StyleController = this.layoutControl1;
            this.checkShow.TabIndex = 11;
            this.checkShow.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(409, 219);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 32);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "取消(&C)";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(309, 219);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(96, 32);
            this.btnOK.StyleController = this.layoutControl1;
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtPwdAgain
            // 
            this.txtPwdAgain.Location = new System.Drawing.Point(106, 86);
            this.txtPwdAgain.Name = "txtPwdAgain";
            this.txtPwdAgain.Properties.AutoHeight = false;
            this.txtPwdAgain.Properties.MaxLength = 20;
            this.txtPwdAgain.Properties.PasswordChar = '*';
            this.txtPwdAgain.Size = new System.Drawing.Size(399, 27);
            this.txtPwdAgain.StyleController = this.layoutControl1;
            this.txtPwdAgain.TabIndex = 6;
            // 
            // txtNewPwd
            // 
            this.txtNewPwd.Location = new System.Drawing.Point(106, 54);
            this.txtNewPwd.Name = "txtNewPwd";
            this.txtNewPwd.Properties.AutoHeight = false;
            this.txtNewPwd.Properties.MaxLength = 20;
            this.txtNewPwd.Properties.NullValuePrompt = "密码不能为空字符，并且不区分大小写。";
            this.txtNewPwd.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtNewPwd.Properties.PasswordChar = '*';
            this.txtNewPwd.Size = new System.Drawing.Size(399, 28);
            this.txtNewPwd.StyleController = this.layoutControl1;
            this.txtNewPwd.TabIndex = 5;
            // 
            // txtCurrentPwd
            // 
            this.txtCurrentPwd.Location = new System.Drawing.Point(106, 12);
            this.txtCurrentPwd.Name = "txtCurrentPwd";
            this.txtCurrentPwd.Properties.AutoHeight = false;
            this.txtCurrentPwd.Properties.MaxLength = 20;
            this.txtCurrentPwd.Properties.PasswordChar = '*';
            this.txtCurrentPwd.Size = new System.Drawing.Size(399, 28);
            this.txtCurrentPwd.StyleController = this.layoutControl1;
            this.txtCurrentPwd.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.emptySpaceItem3,
            this.layoutControlItem6,
            this.emptySpaceItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(517, 263);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtCurrentPwd;
            this.layoutControlItem1.CustomizationFormText = "输入当前密码(&P)";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 32);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(148, 32);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(497, 32);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "输入当前密码(&P)";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(90, 12);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 32);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(497, 10);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtNewPwd;
            this.layoutControlItem2.CustomizationFormText = "输入一个新密码";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 42);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(0, 32);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(148, 32);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(497, 32);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "输入一个新密码";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(90, 12);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtPwdAgain;
            this.layoutControlItem3.CustomizationFormText = "重新输入新密码";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 74);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(0, 31);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(148, 31);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(497, 31);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "重新输入新密码";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(90, 12);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnOK;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(297, 207);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(100, 36);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(100, 36);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(100, 36);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnCancel;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(397, 207);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(100, 36);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(100, 36);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(100, 36);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(119, 207);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(178, 36);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.checkShow;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 207);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(75, 23);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(119, 36);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 105);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(497, 102);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // FrmChangePwd
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(517, 263);
            this.Controls.Add(this.layoutControl1);
            this.MaximizeBox = false;
            this.Name = "FrmChangePwd";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改密码";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkShow.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPwdAgain.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrentPwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit txtPwdAgain;
        private DevExpress.XtraEditors.TextEdit txtNewPwd;
        private DevExpress.XtraEditors.TextEdit txtCurrentPwd;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraEditors.CheckEdit checkShow;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
    }
}