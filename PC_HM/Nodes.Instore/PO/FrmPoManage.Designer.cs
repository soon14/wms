namespace Nodes.Instore
{
    partial class FrmPoManage
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
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.toolRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.toolEdit = new DevExpress.XtraBars.BarButtonItem();
            this.toolDel = new DevExpress.XtraBars.BarButtonItem();
            this.toolCancelCommit = new DevExpress.XtraBars.BarButtonItem();
            this.toolCommit = new DevExpress.XtraBars.BarButtonItem();
            this.toolToday = new DevExpress.XtraBars.BarButtonItem();
            this.toolWeek = new DevExpress.XtraBars.BarButtonItem();
            this.toolSearch = new DevExpress.XtraBars.BarButtonItem();
            this.popupControlContainer1 = new DevExpress.XtraBars.PopupControlContainer(this.components);
            this.ucPoQueryConditionPanel1 = new Nodes.Instore.UcPoQueryEngine();
            this.toolView = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.toolCopy = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.toolPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.ucPoBody1 = new Nodes.Instore.UcPoBody();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupControlContainer1)).BeginInit();
            this.popupControlContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.AllowCustomization = false;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.toolRefresh,
            this.toolEdit,
            this.toolDel,
            this.toolToday,
            this.toolWeek,
            this.toolSearch,
            this.toolCopy,
            this.barButtonItem1,
            this.barButtonItem2,
            this.barButtonItem6,
            this.toolPrint,
            this.toolCommit,
            this.toolCancelCommit,
            this.toolView,
            this.barButtonItem3,
            this.barButtonItem4,
            this.barButtonItem5});
            this.barManager1.MaxItemId = 29;
            // 
            // bar1
            // 
            this.bar1.BarName = "工具";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.toolRefresh),
            new DevExpress.XtraBars.LinkPersistInfo(this.toolEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.toolDel),
            new DevExpress.XtraBars.LinkPersistInfo(this.toolCommit),
            new DevExpress.XtraBars.LinkPersistInfo(this.toolCancelCommit),
            new DevExpress.XtraBars.LinkPersistInfo(this.toolToday, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.toolWeek),
            new DevExpress.XtraBars.LinkPersistInfo(this.toolSearch),
            new DevExpress.XtraBars.LinkPersistInfo(this.toolView, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.toolCopy),
            new DevExpress.XtraBars.LinkPersistInfo(this.toolPrint)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "工具";
            // 
            // toolRefresh
            // 
            this.toolRefresh.Caption = "刷新";
            this.toolRefresh.Hint = "可以使用快捷键F5";
            this.toolRefresh.Id = 0;
            this.toolRefresh.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5);
            this.toolRefresh.Name = "toolRefresh";
            this.toolRefresh.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.toolRefresh.Tag = "刷新";
            this.toolRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // toolEdit
            // 
            this.toolEdit.Caption = "编辑选中...";
            this.toolEdit.Id = 2;
            this.toolEdit.Name = "toolEdit";
            this.toolEdit.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.toolEdit.Tag = "编辑";
            this.toolEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // toolDel
            // 
            this.toolDel.Caption = "删除选中...";
            this.toolDel.Hint = "可以使用快捷键Delete";
            this.toolDel.Id = 3;
            this.toolDel.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Delete);
            this.toolDel.Name = "toolDel";
            this.toolDel.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.toolDel.Tag = "删除";
            this.toolDel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // toolCancelCommit
            // 
            this.toolCancelCommit.Caption = "取消提交";
            this.toolCancelCommit.Hint = "将单据状态变回“草稿”状态。";
            this.toolCancelCommit.Id = 24;
            this.toolCancelCommit.Name = "toolCancelCommit";
            this.toolCancelCommit.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.toolCancelCommit.Tag = "取消提交";
            this.toolCancelCommit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // toolCommit
            // 
            this.toolCommit.Caption = "提交选中";
            this.toolCommit.Hint = "提交后将不允许再次修改。";
            this.toolCommit.Id = 23;
            this.toolCommit.Name = "toolCommit";
            this.toolCommit.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.toolCommit.Tag = "提交";
            this.toolCommit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // toolToday
            // 
            this.toolToday.Caption = "进行中单据";
            this.toolToday.Id = 4;
            this.toolToday.Name = "toolToday";
            this.toolToday.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.toolToday.Tag = "进行中单据";
            this.toolToday.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // toolWeek
            // 
            this.toolWeek.Caption = "近一周单据";
            this.toolWeek.Id = 5;
            this.toolWeek.Name = "toolWeek";
            this.toolWeek.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.toolWeek.Tag = "近一周单据";
            this.toolWeek.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // toolSearch
            // 
            this.toolSearch.ActAsDropDown = true;
            this.toolSearch.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.toolSearch.Caption = "自定义查询";
            this.toolSearch.DropDownControl = this.popupControlContainer1;
            this.toolSearch.Hint = "可以使用快捷键F3";
            this.toolSearch.Id = 6;
            this.toolSearch.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F3);
            this.toolSearch.Name = "toolSearch";
            this.toolSearch.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.toolSearch.ShortcutKeyDisplayString = "F3";
            // 
            // popupControlContainer1
            // 
            this.popupControlContainer1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.popupControlContainer1.CloseOnLostFocus = false;
            this.popupControlContainer1.CloseOnOuterMouseClick = false;
            this.popupControlContainer1.Controls.Add(this.ucPoQueryConditionPanel1);
            this.popupControlContainer1.Location = new System.Drawing.Point(521, 44);
            this.popupControlContainer1.Manager = this.barManager1;
            this.popupControlContainer1.Name = "popupControlContainer1";
            this.popupControlContainer1.Size = new System.Drawing.Size(273, 264);
            this.popupControlContainer1.TabIndex = 1;
            this.popupControlContainer1.Visible = false;
            this.popupControlContainer1.Popup += new System.EventHandler(this.popupControlContainer1_Popup);
            // 
            // ucPoQueryConditionPanel1
            // 
            this.ucPoQueryConditionPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPoQueryConditionPanel1.Location = new System.Drawing.Point(0, 0);
            this.ucPoQueryConditionPanel1.Name = "ucPoQueryConditionPanel1";
            this.ucPoQueryConditionPanel1.Size = new System.Drawing.Size(273, 264);
            this.ucPoQueryConditionPanel1.TabIndex = 0;
            this.ucPoQueryConditionPanel1.QueryCompleted += new Nodes.Instore.UcPoQueryEngine.QueryComplete(this.ucPoQueryConditionPanel1_QueryCompleted);
            // 
            // toolView
            // 
            this.toolView.Caption = "查看";
            this.toolView.Id = 25;
            this.toolView.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem3),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem4),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem5)});
            this.toolView.Name = "toolView";
            this.toolView.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.toolView.Tag = "";
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "单据日志";
            this.barButtonItem3.Id = 26;
            this.barButtonItem3.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F1);
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.ShortcutKeyDisplayString = "F3";
            this.barButtonItem3.Tag = "单据日志";
            this.barButtonItem3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "关联的到货通知单";
            this.barButtonItem4.Id = 27;
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Caption = "入库清单";
            this.barButtonItem5.Id = 28;
            this.barButtonItem5.Name = "barButtonItem5";
            // 
            // toolCopy
            // 
            this.toolCopy.Caption = "复制为";
            this.toolCopy.Id = 12;
            this.toolCopy.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem6),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2)});
            this.toolCopy.Name = "toolCopy";
            this.toolCopy.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Caption = "新采购订单";
            this.barButtonItem6.Id = 19;
            this.barButtonItem6.Name = "barButtonItem6";
            this.barButtonItem6.Tag = "新采购订单";
            this.barButtonItem6.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "采购退货单";
            this.barButtonItem1.Id = 13;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "入库通知单";
            this.barButtonItem2.Id = 14;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.Tag = "复制为入库通知单";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // toolPrint
            // 
            this.toolPrint.Caption = "打印入库单";
            this.toolPrint.Hint = "可以使用快捷键Ctrl+P";
            this.toolPrint.Id = 22;
            this.toolPrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P));
            this.toolPrint.Name = "toolPrint";
            this.toolPrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1016, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 518);
            this.barDockControlBottom.Size = new System.Drawing.Size(1016, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 487);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1016, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 487);
            // 
            // ucPoBody1
            // 
            this.ucPoBody1.DataSource = null;
            this.ucPoBody1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPoBody1.Location = new System.Drawing.Point(0, 31);
            this.ucPoBody1.Name = "ucPoBody1";
            this.ucPoBody1.Size = new System.Drawing.Size(1016, 487);
            this.ucPoBody1.TabIndex = 26;
            // 
            // FrmPoManage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1016, 518);
            this.Controls.Add(this.popupControlContainer1);
            this.Controls.Add(this.ucPoBody1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmPoManage";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "采购单管理";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupControlContainer1)).EndInit();
            this.popupControlContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem toolRefresh;
        private DevExpress.XtraBars.BarButtonItem toolEdit;
        private DevExpress.XtraBars.BarButtonItem toolDel;
        private DevExpress.XtraBars.BarButtonItem toolToday;
        private DevExpress.XtraBars.BarButtonItem toolWeek;
        private DevExpress.XtraBars.BarButtonItem toolSearch;
        private DevExpress.XtraBars.BarSubItem toolCopy;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.XtraBars.BarButtonItem toolPrint;
        private DevExpress.XtraBars.BarButtonItem toolCancelCommit;
        private DevExpress.XtraBars.BarButtonItem toolCommit;
        private DevExpress.XtraBars.PopupControlContainer popupControlContainer1;
        private UcPoQueryEngine ucPoQueryConditionPanel1;
        private DevExpress.XtraBars.BarSubItem toolView;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private UcPoBody ucPoBody1;
    }
}