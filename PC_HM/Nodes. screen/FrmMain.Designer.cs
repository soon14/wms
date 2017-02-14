namespace Nodes.screen
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.timeChange = new System.Windows.Forms.Timer(this.components);
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtShowRowsMonth = new DevExpress.XtraEditors.TextEdit();
            this.txtShowRowsDay = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtShowRowsMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShowRowsDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.AppearancePage.Header.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.xtraTabControl1.AppearancePage.Header.Options.UseFont = true;
            this.xtraTabControl1.AppearancePage.Header.Options.UseTextOptions = true;
            this.xtraTabControl1.AppearancePage.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.xtraTabControl1.HeaderAutoFill = DevExpress.Utils.DefaultBoolean.True;
            this.xtraTabControl1.Location = new System.Drawing.Point(12, 36);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(776, 360);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(770, 326);
            this.xtraTabPage1.Text = "绩效排行";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(770, 326);
            this.xtraTabPage2.Text = "效率排行";
            // 
            // timeChange
            // 
            this.timeChange.Interval = 300000;
            this.timeChange.Tick += new System.EventHandler(this.timeChange_Tick);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtShowRowsMonth);
            this.layoutControl1.Controls.Add(this.txtShowRowsDay);
            this.layoutControl1.Controls.Add(this.xtraTabControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.ID = "506b0d6c-1854-4ed3-ba67-bef1af3b0388";
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(800, 408);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtShowRowsMonth
            // 
            this.txtShowRowsMonth.Location = new System.Drawing.Point(550, 12);
            this.txtShowRowsMonth.Name = "txtShowRowsMonth";
            this.txtShowRowsMonth.Properties.Mask.EditMask = "[1-9]\\d*";
            this.txtShowRowsMonth.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtShowRowsMonth.Size = new System.Drawing.Size(238, 20);
            this.txtShowRowsMonth.StyleController = this.layoutControl1;
            this.txtShowRowsMonth.TabIndex = 5;
            this.txtShowRowsMonth.Tag = "Month";
            this.txtShowRowsMonth.EditValueChanged += new System.EventHandler(this.TxtEditValueChange);
            // 
            // txtShowRowsDay
            // 
            this.txtShowRowsDay.Location = new System.Drawing.Point(160, 12);
            this.txtShowRowsDay.Name = "txtShowRowsDay";
            this.txtShowRowsDay.Properties.Mask.EditMask = "[1-9]\\d*";
            this.txtShowRowsDay.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtShowRowsDay.Size = new System.Drawing.Size(238, 20);
            this.txtShowRowsDay.StyleController = this.layoutControl1;
            this.txtShowRowsDay.TabIndex = 4;
            this.txtShowRowsDay.Tag = "Day";
            this.txtShowRowsDay.EditValueChanged += new System.EventHandler(this.TxtEditValueChange);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(800, 408);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.xtraTabControl1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(780, 364);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtShowRowsDay;
            this.layoutControlItem2.CustomizationFormText = "”当日排名“每页显示行数";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(390, 24);
            this.layoutControlItem2.Text = "”当日排名“每页显示行数";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(144, 12);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtShowRowsMonth;
            this.layoutControlItem3.CustomizationFormText = "”当月排名“每页显示行数";
            this.layoutControlItem3.Location = new System.Drawing.Point(390, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(390, 24);
            this.layoutControlItem3.Text = "”当月排名“每页显示行数";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(144, 12);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 408);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmMain";
            this.ShowIcon = false;
            this.Text = "大屏幕展示";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmLoad);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmClosing);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtShowRowsMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShowRowsDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private System.Windows.Forms.Timer timeChange;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit txtShowRowsMonth;
        private DevExpress.XtraEditors.TextEdit txtShowRowsDay;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}