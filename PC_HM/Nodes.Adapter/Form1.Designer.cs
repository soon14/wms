namespace Nodes.Adapter
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiFuntion = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiStartInterface = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiStopInterface = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTools = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.账号ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsmiLocalIP = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsmiState = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsmiServerState = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvOnLineUsers = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFuntion,
            this.tsmiTools,
            this.账号ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(703, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiFuntion
            // 
            this.tsmiFuntion.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiStartInterface,
            this.tsmiStopInterface});
            this.tsmiFuntion.Name = "tsmiFuntion";
            this.tsmiFuntion.Size = new System.Drawing.Size(44, 21);
            this.tsmiFuntion.Text = "功能";
            // 
            // tsmiStartInterface
            // 
            this.tsmiStartInterface.Name = "tsmiStartInterface";
            this.tsmiStartInterface.Size = new System.Drawing.Size(124, 22);
            this.tsmiStartInterface.Text = "启用接口";
            // 
            // tsmiStopInterface
            // 
            this.tsmiStopInterface.Name = "tsmiStopInterface";
            this.tsmiStopInterface.Size = new System.Drawing.Size(124, 22);
            this.tsmiStopInterface.Text = "停止接口";
            // 
            // tsmiTools
            // 
            this.tsmiTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOptions});
            this.tsmiTools.Name = "tsmiTools";
            this.tsmiTools.Size = new System.Drawing.Size(44, 21);
            this.tsmiTools.Text = "工具";
            // 
            // tsmiOptions
            // 
            this.tsmiOptions.Name = "tsmiOptions";
            this.tsmiOptions.Size = new System.Drawing.Size(100, 22);
            this.tsmiOptions.Text = "选项";
            // 
            // 账号ToolStripMenuItem
            // 
            this.账号ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiLogin,
            this.tsmiLogout});
            this.账号ToolStripMenuItem.Name = "账号ToolStripMenuItem";
            this.账号ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.账号ToolStripMenuItem.Text = "账号";
            // 
            // tsmiLogin
            // 
            this.tsmiLogin.Name = "tsmiLogin";
            this.tsmiLogin.Size = new System.Drawing.Size(124, 22);
            this.tsmiLogin.Text = "登录";
            // 
            // tsmiLogout
            // 
            this.tsmiLogout.Name = "tsmiLogout";
            this.tsmiLogout.Size = new System.Drawing.Size(124, 22);
            this.tsmiLogout.Text = "退出登录";
            this.tsmiLogout.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiLocalIP,
            this.tsmiState,
            this.tsmiServerState});
            this.statusStrip1.Location = new System.Drawing.Point(0, 403);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(703, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsmiLocalIP
            // 
            this.tsmiLocalIP.Name = "tsmiLocalIP";
            this.tsmiLocalIP.Size = new System.Drawing.Size(79, 17);
            this.tsmiLocalIP.Text = "本机IP地址：";
            // 
            // tsmiState
            // 
            this.tsmiState.Margin = new System.Windows.Forms.Padding(50, 3, 0, 2);
            this.tsmiState.Name = "tsmiState";
            this.tsmiState.Size = new System.Drawing.Size(101, 17);
            this.tsmiState.Text = "状态：正在启动...";
            // 
            // tsmiServerState
            // 
            this.tsmiServerState.Margin = new System.Windows.Forms.Padding(50, 3, 0, 2);
            this.tsmiServerState.Name = "tsmiServerState";
            this.tsmiServerState.Size = new System.Drawing.Size(56, 17);
            this.tsmiServerState.Text = "服务器：";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(703, 378);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(695, 352);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "本地";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(689, 346);
            this.splitContainer1.SplitterDistance = 229;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvOnLineUsers);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(229, 346);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "在线用户";
            // 
            // lvOnLineUsers
            // 
            this.lvOnLineUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader5,
            this.columnHeader3,
            this.columnHeader4});
            this.lvOnLineUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvOnLineUsers.Location = new System.Drawing.Point(3, 17);
            this.lvOnLineUsers.Name = "lvOnLineUsers";
            this.lvOnLineUsers.Size = new System.Drawing.Size(223, 326);
            this.lvOnLineUsers.TabIndex = 0;
            this.lvOnLineUsers.UseCompatibleStateImageBehavior = false;
            this.lvOnLineUsers.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "账号";
            this.columnHeader1.Width = 59;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "姓名";
            this.columnHeader2.Width = 75;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "平台";
            this.columnHeader5.Width = 45;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "登录时间";
            this.columnHeader3.Width = 74;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "上次访问时间";
            this.columnHeader4.Width = 72;
            // 
            // groupBox2
            // 
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(456, 346);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "命令";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tabControl2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(695, 352);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "接口";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(3, 3);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(689, 346);
            this.tabControl2.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(681, 320);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "运行状态";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(681, 320);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "运行状态：正常";
            this.notifyIcon1.Visible = true;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 425);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(719, 464);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "惠民网仓储管理系统-适配器";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsmiState;
        private System.Windows.Forms.ToolStripStatusLabel tsmiServerState;
        private System.Windows.Forms.ToolStripStatusLabel tsmiLocalIP;
        private System.Windows.Forms.ToolStripMenuItem tsmiTools;
        private System.Windows.Forms.ToolStripMenuItem tsmiOptions;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripMenuItem tsmiFuntion;
        private System.Windows.Forms.ToolStripMenuItem tsmiStartInterface;
        private System.Windows.Forms.ToolStripMenuItem tsmiStopInterface;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lvOnLineUsers;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStripMenuItem 账号ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiLogin;
        private System.Windows.Forms.ToolStripMenuItem tsmiLogout;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ColumnHeader columnHeader5;
    }
}

