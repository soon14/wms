namespace Nodes.Outstore
{
    partial class FrmLoadingSortMap
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.clearLine = new DevExpress.XtraEditors.SimpleButton();
            this.lineWay = new DevExpress.XtraEditors.SimpleButton();
            this.btnCreateTask = new DevExpress.XtraEditors.SimpleButton();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.clearLine);
            this.panelControl1.Controls.Add(this.lineWay);
            this.panelControl1.Controls.Add(this.btnCreateTask);
            this.panelControl1.Controls.Add(this.btnRefresh);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(761, 52);
            this.panelControl1.TabIndex = 0;
            // 
            // clearLine
            // 
            this.clearLine.Location = new System.Drawing.Point(249, 9);
            this.clearLine.Name = "clearLine";
            this.clearLine.Size = new System.Drawing.Size(83, 34);
            this.clearLine.TabIndex = 2;
            this.clearLine.Text = "清除路线";
            this.clearLine.Click += new System.EventHandler(this.clearLine_Click);
            // 
            // lineWay
            // 
            this.lineWay.Location = new System.Drawing.Point(131, 9);
            this.lineWay.Name = "lineWay";
            this.lineWay.Size = new System.Drawing.Size(83, 34);
            this.lineWay.TabIndex = 1;
            this.lineWay.Text = "推荐路线";
            this.lineWay.Click += new System.EventHandler(this.lineWay_Click);
            // 
            // btnCreateTask
            // 
            this.btnCreateTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateTask.Location = new System.Drawing.Point(625, 9);
            this.btnCreateTask.Name = "btnCreateTask";
            this.btnCreateTask.Size = new System.Drawing.Size(124, 34);
            this.btnCreateTask.TabIndex = 0;
            this.btnCreateTask.Text = "分派装车信息";
            this.btnCreateTask.Click += new System.EventHandler(this.btnCreateTask_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 9);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(83, 34);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "重新加载";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.AllowWebBrowserDrop = false;
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser1.Location = new System.Drawing.Point(0, 52);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(761, 339);
            this.webBrowser1.TabIndex = 2;
            this.webBrowser1.WebBrowserShortcutsEnabled = false;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // FrmLoadingSortMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 391);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.panelControl1);
            this.Name = "FrmLoadingSortMap";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "订单排序";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnCreateTask;
        private DevExpress.XtraEditors.SimpleButton btnRefresh;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private DevExpress.XtraEditors.SimpleButton clearLine;
        private DevExpress.XtraEditors.SimpleButton lineWay;

    }
}