namespace Nodes.BaseData
{
    partial class FrmCustomerManager
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
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.toolRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.toolAdd = new DevExpress.XtraBars.BarButtonItem();
            this.toolEdit = new DevExpress.XtraBars.BarButtonItem();
            this.toolDel = new DevExpress.XtraBars.BarButtonItem();
            this.toolSearch = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.labDump = new System.Windows.Forms.Label();
            this.textBoxDump = new System.Windows.Forms.TextBox();
            this.labFirst = new System.Windows.Forms.Label();
            this.labPre = new System.Windows.Forms.Label();
            this.labYE = new System.Windows.Forms.Label();
            this.labNext = new System.Windows.Forms.Label();
            this.txtCountPage = new System.Windows.Forms.TextBox();
            this.labLast = new System.Windows.Forms.Label();
            this.labZG = new System.Windows.Forms.Label();
            this.labCur = new System.Windows.Forms.Label();
            this.txtCurePage = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "启用状态";
            this.gridColumn9.FieldName = "IsActive";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 8;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "WMS自有";
            this.gridColumn12.FieldName = "IsOwn";
            this.gridColumn12.Name = "gridColumn12";
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
            this.toolSearch,
            this.toolAdd,
            this.toolEdit,
            this.toolDel});
            this.barManager1.MaxItemId = 8;
            // 
            // bar1
            // 
            this.bar1.BarName = "工具";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.toolRefresh),
            new DevExpress.XtraBars.LinkPersistInfo(this.toolAdd, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.toolEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.toolDel),
            new DevExpress.XtraBars.LinkPersistInfo(this.toolSearch)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableCustomization = true;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "工具";
            // 
            // toolRefresh
            // 
            this.toolRefresh.Caption = "刷新";
            this.toolRefresh.Id = 0;
            this.toolRefresh.Name = "toolRefresh";
            this.toolRefresh.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.toolRefresh.Tag = "刷新";
            this.toolRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // toolAdd
            // 
            this.toolAdd.Caption = "新增";
            this.toolAdd.Id = 5;
            this.toolAdd.Name = "toolAdd";
            this.toolAdd.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.toolAdd.Tag = "新增";
            this.toolAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.toolAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // toolEdit
            // 
            this.toolEdit.Caption = "修改";
            this.toolEdit.Id = 6;
            this.toolEdit.Name = "toolEdit";
            this.toolEdit.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.toolEdit.Tag = "修改";
            this.toolEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.toolEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // toolDel
            // 
            this.toolDel.Caption = "删除";
            this.toolDel.Id = 7;
            this.toolDel.Name = "toolDel";
            this.toolDel.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.toolDel.Tag = "删除";
            this.toolDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.toolDel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // toolSearch
            // 
            this.toolSearch.Caption = "快速查找";
            this.toolSearch.Id = 4;
            this.toolSearch.Name = "toolSearch";
            this.toolSearch.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.toolSearch.Tag = "快速查找";
            this.toolSearch.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1174, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 448);
            this.barDockControlBottom.Size = new System.Drawing.Size(1174, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 417);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1174, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 417);
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bindingSource1;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 31);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.MenuManager = this.barManager1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1174, 257);
            this.gridControl1.TabIndex = 6;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn14,
            this.gridColumn10,
            this.gridColumn3,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn13,
            this.gridColumn7,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn8,
            this.gridColumn17,
            this.gridColumn9,
            this.gridColumn4,
            this.gridColumn11,
            this.gridColumn12});
            styleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.Red;
            styleFormatCondition1.Appearance.Options.UseForeColor = true;
            styleFormatCondition1.Column = this.gridColumn9;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
            styleFormatCondition1.Expression = "IsActive == \'N\'";
            this.gridView1.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.ID = "a65c8380-2631-4644-92cb-7645f55e97d7";
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn1, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView1.RowDoubleClick += new System.EventHandler(this.gridView1_RowDoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "编号";
            this.gridColumn1.FieldName = "CustomerCode";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 70;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "名称";
            this.gridColumn2.FieldName = "CustomerName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 150;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "简称";
            this.gridColumn14.FieldName = "CustomerNameS";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 2;
            this.gridColumn14.Width = 100;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "拼音简写";
            this.gridColumn10.FieldName = "CustomerNamePY";
            this.gridColumn10.Name = "gridColumn10";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "区域";
            this.gridColumn3.FieldName = "AreaName";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            this.gridColumn3.Width = 53;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "联系人";
            this.gridColumn5.FieldName = "Contact";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 120;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "联系电话";
            this.gridColumn6.FieldName = "Phone";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 100;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "邮编";
            this.gridColumn13.FieldName = "PostCode";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 6;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "地址";
            this.gridColumn7.FieldName = "Address";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 7;
            this.gridColumn7.Width = 149;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "线路编号";
            this.gridColumn15.FieldName = "RouteCode";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 9;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "线路名称";
            this.gridColumn16.FieldName = "RouteName";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 10;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "优先级";
            this.gridColumn8.FieldName = "SortOrder";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 14;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "距离";
            this.gridColumn17.FieldName = "Distance";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 11;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "最后更新时间";
            this.gridColumn4.DisplayFormat.FormatString = "g";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn4.FieldName = "UpdateDate";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 12;
            this.gridColumn4.Width = 121;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "最后更新人";
            this.gridColumn11.FieldName = "UpdateBy";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 13;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.gridControl2);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 288);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1174, 160);
            this.panelControl1.TabIndex = 16;
            this.panelControl1.Visible = false;
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(2, 2);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.MenuManager = this.barManager1;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(1054, 156);
            this.gridControl2.TabIndex = 1;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            this.gridControl2.Visible = false;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.ID = "c206a258-598a-4326-bd1d-6390ecd103fb";
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsView.EnableAppearanceOddRow = true;
            this.gridView2.OptionsView.ShowViewCaption = true;
            this.gridView2.ViewCaption = "客户地址信息";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.simpleButton3);
            this.panelControl2.Controls.Add(this.simpleButton2);
            this.panelControl2.Controls.Add(this.simpleButton1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl2.Location = new System.Drawing.Point(1056, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(116, 156);
            this.panelControl2.TabIndex = 0;
            this.panelControl2.Visible = false;
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(20, 88);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(75, 25);
            this.simpleButton3.TabIndex = 1;
            this.simpleButton3.Text = "删除";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(20, 54);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 25);
            this.simpleButton2.TabIndex = 1;
            this.simpleButton2.Text = "修改";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(20, 20);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 25);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "添加";
            // 
            // labDump
            // 
            this.labDump.AutoSize = true;
            this.labDump.Location = new System.Drawing.Point(448, 9);
            this.labDump.Name = "labDump";
            this.labDump.Size = new System.Drawing.Size(41, 12);
            this.labDump.TabIndex = 55;
            this.labDump.Text = "跳转页";
            this.labDump.Visible = false;
            this.labDump.Click += new System.EventHandler(this.labDump_Click);
            // 
            // textBoxDump
            // 
            this.textBoxDump.Location = new System.Drawing.Point(496, 4);
            this.textBoxDump.Name = "textBoxDump";
            this.textBoxDump.Size = new System.Drawing.Size(81, 21);
            this.textBoxDump.TabIndex = 56;
            this.textBoxDump.Visible = false;
            this.textBoxDump.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxDump_KeyDown);
            // 
            // labFirst
            // 
            this.labFirst.AutoSize = true;
            this.labFirst.Location = new System.Drawing.Point(588, 9);
            this.labFirst.Name = "labFirst";
            this.labFirst.Size = new System.Drawing.Size(41, 12);
            this.labFirst.TabIndex = 46;
            this.labFirst.Text = "第一页";
            this.labFirst.Visible = false;
            this.labFirst.Click += new System.EventHandler(this.labFirst_Click);
            // 
            // labPre
            // 
            this.labPre.AutoSize = true;
            this.labPre.Location = new System.Drawing.Point(635, 9);
            this.labPre.Name = "labPre";
            this.labPre.Size = new System.Drawing.Size(41, 12);
            this.labPre.TabIndex = 47;
            this.labPre.Text = "上一页";
            this.labPre.Visible = false;
            this.labPre.Click += new System.EventHandler(this.labPre_Click);
            // 
            // labYE
            // 
            this.labYE.AutoSize = true;
            this.labYE.Location = new System.Drawing.Point(931, 9);
            this.labYE.Name = "labYE";
            this.labYE.Size = new System.Drawing.Size(17, 12);
            this.labYE.TabIndex = 54;
            this.labYE.Text = "页";
            this.labYE.Visible = false;
            // 
            // labNext
            // 
            this.labNext.AutoSize = true;
            this.labNext.Location = new System.Drawing.Point(682, 9);
            this.labNext.Name = "labNext";
            this.labNext.Size = new System.Drawing.Size(41, 12);
            this.labNext.TabIndex = 48;
            this.labNext.Text = "下一页";
            this.labNext.Visible = false;
            this.labNext.Click += new System.EventHandler(this.labNext_Click);
            // 
            // txtCountPage
            // 
            this.txtCountPage.Location = new System.Drawing.Point(827, 2);
            this.txtCountPage.Name = "txtCountPage";
            this.txtCountPage.ReadOnly = true;
            this.txtCountPage.Size = new System.Drawing.Size(100, 21);
            this.txtCountPage.TabIndex = 53;
            this.txtCountPage.Visible = false;
            // 
            // labLast
            // 
            this.labLast.AutoSize = true;
            this.labLast.Location = new System.Drawing.Point(729, 9);
            this.labLast.Name = "labLast";
            this.labLast.Size = new System.Drawing.Size(41, 12);
            this.labLast.TabIndex = 49;
            this.labLast.Text = "最后页";
            this.labLast.Visible = false;
            this.labLast.Click += new System.EventHandler(this.labLast_Click);
            // 
            // labZG
            // 
            this.labZG.AutoSize = true;
            this.labZG.Location = new System.Drawing.Point(794, 9);
            this.labZG.Name = "labZG";
            this.labZG.Size = new System.Drawing.Size(29, 12);
            this.labZG.TabIndex = 52;
            this.labZG.Text = "总共";
            this.labZG.Visible = false;
            // 
            // labCur
            // 
            this.labCur.AutoSize = true;
            this.labCur.Location = new System.Drawing.Point(314, 9);
            this.labCur.Name = "labCur";
            this.labCur.Size = new System.Drawing.Size(41, 12);
            this.labCur.TabIndex = 50;
            this.labCur.Text = "当前页";
            this.labCur.Visible = false;
            // 
            // txtCurePage
            // 
            this.txtCurePage.Location = new System.Drawing.Point(359, 3);
            this.txtCurePage.Name = "txtCurePage";
            this.txtCurePage.ReadOnly = true;
            this.txtCurePage.Size = new System.Drawing.Size(81, 21);
            this.txtCurePage.TabIndex = 51;
            this.txtCurePage.Visible = false;
            // 
            // FrmCustomerManager
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1174, 448);
            this.Controls.Add(this.labDump);
            this.Controls.Add(this.textBoxDump);
            this.Controls.Add(this.labFirst);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.labPre);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.labYE);
            this.Controls.Add(this.txtCurePage);
            this.Controls.Add(this.labNext);
            this.Controls.Add(this.labCur);
            this.Controls.Add(this.txtCountPage);
            this.Controls.Add(this.labZG);
            this.Controls.Add(this.labLast);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmCustomerManager";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "客户信息";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem toolRefresh;
        private DevExpress.XtraBars.BarButtonItem toolSearch;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraBars.BarButtonItem toolAdd;
        private DevExpress.XtraBars.BarButtonItem toolEdit;
        private DevExpress.XtraBars.BarButtonItem toolDel;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private System.Windows.Forms.TextBox textBoxDump;
        private System.Windows.Forms.Label labDump;
        private System.Windows.Forms.Label labYE;
        private System.Windows.Forms.TextBox txtCountPage;
        private System.Windows.Forms.Label labZG;
        private System.Windows.Forms.TextBox txtCurePage;
        private System.Windows.Forms.Label labCur;
        private System.Windows.Forms.Label labLast;
        private System.Windows.Forms.Label labNext;
        private System.Windows.Forms.Label labPre;
        private System.Windows.Forms.Label labFirst;
    }
}