namespace Nodes.WMSClient
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.treeMenu = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.teButtons = new DevExpress.XtraEditors.ButtonEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barLoginInfo = new DevExpress.XtraBars.BarStaticItem();
            this.barIPAddress = new DevExpress.XtraBars.BarStaticItem();
            this.barCopyRight = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.documentManager1 = new DevExpress.XtraBars.Docking2010.DocumentManager(this.components);
            this.tabbedView1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(this.components);
            this.txtMenu = new DevExpress.XtraEditors.ButtonEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            ((System.ComponentModel.ISupportInitialize)(this.treeMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teButtons.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.documentManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMenu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeMenu
            // 
            this.treeMenu.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.treeMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeMenu.KeyFieldName = "ModuleID";
            this.treeMenu.Location = new System.Drawing.Point(2, 22);
            this.treeMenu.Name = "treeMenu";
            this.treeMenu.OptionsBehavior.Editable = false;
            this.treeMenu.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.treeMenu.OptionsView.ShowColumns = false;
            this.treeMenu.OptionsView.ShowFocusedFrame = false;
            this.treeMenu.OptionsView.ShowIndicator = false;
            this.treeMenu.RowHeight = 21;
            this.treeMenu.Size = new System.Drawing.Size(155, 349);
            this.treeMenu.TabIndex = 18;
            this.treeMenu.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnTreeMenuMouseClick);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "导航菜单";
            this.treeListColumn1.FieldName = "MenuName";
            this.treeListColumn1.MinWidth = 49;
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::Nodes.WMSClient.Properties.Resources.title;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(383, 62);
            this.panel1.TabIndex = 24;
            // 
            // teButtons
            // 
            this.teButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.teButtons.Location = new System.Drawing.Point(376, 38);
            this.teButtons.Name = "teButtons";
            this.teButtons.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "帮助文档", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, "", "帮助文档", null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "修改密码", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject6, "", "修改密码", null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "本机设置", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject7, "", "本机设置", null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "退出系统", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject8, "", "退出系统", null, true)});
            this.teButtons.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.teButtons.Size = new System.Drawing.Size(220, 19);
            this.teButtons.TabIndex = 0;
            this.teButtons.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.OnSystemMenuButtonClick);
            // 
            // barManager1
            // 
            this.barManager1.AllowCustomization = false;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barLoginInfo,
            this.barCopyRight,
            this.barIPAddress});
            this.barManager1.MaxItemId = 3;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar3
            // 
            this.bar3.BarName = "状态栏";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barLoginInfo),
            new DevExpress.XtraBars.LinkPersistInfo(this.barIPAddress),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCopyRight)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "状态栏";
            // 
            // barLoginInfo
            // 
            this.barLoginInfo.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            this.barLoginInfo.Caption = "当前用户";
            this.barLoginInfo.Id = 1;
            this.barLoginInfo.Name = "barLoginInfo";
            this.barLoginInfo.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barIPAddress
            // 
            this.barIPAddress.Caption = "IP: 127.0.0.1";
            this.barIPAddress.Id = 0;
            this.barIPAddress.Name = "barIPAddress";
            this.barIPAddress.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // barCopyRight
            // 
            this.barCopyRight.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barCopyRight.Caption = "版权所有：中商惠民(北京)电子商务有限公司";
            this.barCopyRight.Id = 2;
            this.barCopyRight.Name = "barCopyRight";
            this.barCopyRight.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(982, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 435);
            this.barDockControlBottom.Size = new System.Drawing.Size(982, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 435);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(982, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 435);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(982, 62);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.teButtons);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(383, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(599, 62);
            this.panel3.TabIndex = 25;
            // 
            // documentManager1
            // 
            this.documentManager1.MdiParent = this;
            this.documentManager1.MenuManager = this.barManager1;
            this.documentManager1.View = this.tabbedView1;
            this.documentManager1.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] {
            this.tabbedView1});
            // 
            // txtMenu
            // 
            this.txtMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtMenu.Location = new System.Drawing.Point(2, 2);
            this.txtMenu.MenuManager = this.barManager1;
            this.txtMenu.Name = "txtMenu";
            this.txtMenu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.txtMenu.Properties.MaxLength = 20;
            this.txtMenu.Properties.NullValuePrompt = "查找";
            this.txtMenu.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtMenu.Size = new System.Drawing.Size(155, 20);
            this.txtMenu.TabIndex = 28;
            this.txtMenu.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtMenu_ButtonClick);
            this.txtMenu.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnSearchMenu_KeyPress);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.treeMenu);
            this.panelControl1.Controls.Add(this.txtMenu);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl1.Location = new System.Drawing.Point(0, 62);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(159, 373);
            this.panelControl1.TabIndex = 29;
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(159, 62);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(5, 373);
            this.splitterControl1.TabIndex = 34;
            this.splitterControl1.TabStop = false;
            // 
            // FrmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(982, 462);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "中商惠民无线仓储管理系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.treeMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teButtons.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.documentManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMenu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList treeMenu;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.ButtonEdit teButtons;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarStaticItem barCopyRight;
        private DevExpress.XtraBars.BarStaticItem barLoginInfo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private DevExpress.XtraBars.Docking2010.DocumentManager documentManager1;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView tabbedView1;
        private DevExpress.XtraBars.BarStaticItem barIPAddress;
        private DevExpress.XtraEditors.ButtonEdit txtMenu;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
    }
}