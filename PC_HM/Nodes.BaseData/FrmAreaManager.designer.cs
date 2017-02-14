namespace Nodes.BaseData
{
    partial class FrmAreaManager
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
            this.toolAdd = new DevExpress.XtraBars.BarButtonItem();
            this.toolEdit = new DevExpress.XtraBars.BarButtonItem();
            this.toolDel = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.txtFilter = new DevExpress.XtraEditors.ButtonEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.textBoxDump = new System.Windows.Forms.TextBox();
            this.labDump = new System.Windows.Forms.Label();
            this.labYE = new System.Windows.Forms.Label();
            this.txtCountPage = new System.Windows.Forms.TextBox();
            this.labZG = new System.Windows.Forms.Label();
            this.txtCurePage = new System.Windows.Forms.TextBox();
            this.labCur = new System.Windows.Forms.Label();
            this.labLast = new System.Windows.Forms.Label();
            this.labNext = new System.Windows.Forms.Label();
            this.labPre = new System.Windows.Forms.Label();
            this.labFirst = new System.Windows.Forms.Label();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
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
            this.toolAdd,
            this.toolEdit,
            this.toolDel,
            this.barButtonItem1,
            this.barButtonItem2});
            this.barManager1.MaxItemId = 9;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemButtonEdit1});
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
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2)});
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
            this.toolAdd.Hint = "为选中分类添加下级分类";
            this.toolAdd.Id = 1;
            this.toolAdd.Name = "toolAdd";
            this.toolAdd.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.toolAdd.Tag = "新增";
            this.toolAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.toolAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // toolEdit
            // 
            this.toolEdit.Caption = "修改";
            this.toolEdit.Id = 2;
            this.toolEdit.Name = "toolEdit";
            this.toolEdit.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.toolEdit.Tag = "修改";
            this.toolEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.toolEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // toolDel
            // 
            this.toolDel.Caption = "删除";
            this.toolDel.Id = 3;
            this.toolDel.Name = "toolDel";
            this.toolDel.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.toolDel.Tag = "删除";
            this.toolDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.toolDel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "收缩";
            this.barButtonItem1.Id = 7;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem1.Tag = "收缩";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "展开";
            this.barButtonItem2.Id = 8;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem2.Tag = "展开";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1129, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 441);
            this.barDockControlBottom.Size = new System.Drawing.Size(1129, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 410);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1129, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 410);
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // treeList1
            // 
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn2});
            this.treeList1.DataSource = this.bindingSource1;
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Location = new System.Drawing.Point(0, 65);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.AllowIncrementalSearch = true;
            this.treeList1.OptionsBehavior.Editable = false;
            this.treeList1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.treeList1.OptionsView.AutoWidth = false;
            this.treeList1.OptionsView.ShowFocusedFrame = false;
            this.treeList1.OptionsView.ShowIndicator = false;
            this.treeList1.Size = new System.Drawing.Size(1129, 376);
            this.treeList1.TabIndex = 4;
            this.treeList1.DoubleClick += new System.EventHandler(this.OnTreelistDoubleClick);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "编码";
            this.treeListColumn1.FieldName = "Code";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            this.treeListColumn1.Width = 154;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "名称";
            this.treeListColumn2.FieldName = "Name";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 1;
            this.treeListColumn2.Width = 284;
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(5, 5);
            this.txtFilter.MenuManager = this.barManager1;
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.txtFilter.Properties.MaxLength = 50;
            this.txtFilter.Properties.NullValuePrompt = "输入查找内容，按下回车键模糊查找";
            this.txtFilter.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtFilter.Size = new System.Drawing.Size(366, 20);
            this.txtFilter.TabIndex = 16;
            this.txtFilter.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtFilter_ButtonClick);
            this.txtFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFilter_KeyDown);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.textBoxDump);
            this.panelControl1.Controls.Add(this.labDump);
            this.panelControl1.Controls.Add(this.labYE);
            this.panelControl1.Controls.Add(this.txtCountPage);
            this.panelControl1.Controls.Add(this.labZG);
            this.panelControl1.Controls.Add(this.txtCurePage);
            this.panelControl1.Controls.Add(this.labCur);
            this.panelControl1.Controls.Add(this.labLast);
            this.panelControl1.Controls.Add(this.labNext);
            this.panelControl1.Controls.Add(this.labPre);
            this.panelControl1.Controls.Add(this.labFirst);
            this.panelControl1.Controls.Add(this.btnSearch);
            this.panelControl1.Controls.Add(this.txtFilter);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 31);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1129, 34);
            this.panelControl1.TabIndex = 26;
            // 
            // textBoxDump
            // 
            this.textBoxDump.Location = new System.Drawing.Point(662, 8);
            this.textBoxDump.Name = "textBoxDump";
            this.textBoxDump.Size = new System.Drawing.Size(81, 21);
            this.textBoxDump.TabIndex = 45;
            this.textBoxDump.Visible = false;
            this.textBoxDump.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxDump_KeyDown);
            // 
            // labDump
            // 
            this.labDump.AutoSize = true;
            this.labDump.Location = new System.Drawing.Point(614, 13);
            this.labDump.Name = "labDump";
            this.labDump.Size = new System.Drawing.Size(41, 12);
            this.labDump.TabIndex = 44;
            this.labDump.Text = "跳转页";
            this.labDump.Visible = false;
            this.labDump.Click += new System.EventHandler(this.labDump_Click);
            // 
            // labYE
            // 
            this.labYE.AutoSize = true;
            this.labYE.Location = new System.Drawing.Point(1097, 13);
            this.labYE.Name = "labYE";
            this.labYE.Size = new System.Drawing.Size(17, 12);
            this.labYE.TabIndex = 43;
            this.labYE.Text = "页";
            this.labYE.Visible = false;
            // 
            // txtCountPage
            // 
            this.txtCountPage.Location = new System.Drawing.Point(993, 6);
            this.txtCountPage.Name = "txtCountPage";
            this.txtCountPage.ReadOnly = true;
            this.txtCountPage.Size = new System.Drawing.Size(100, 21);
            this.txtCountPage.TabIndex = 42;
            this.txtCountPage.Visible = false;
            // 
            // labZG
            // 
            this.labZG.AutoSize = true;
            this.labZG.Location = new System.Drawing.Point(960, 13);
            this.labZG.Name = "labZG";
            this.labZG.Size = new System.Drawing.Size(29, 12);
            this.labZG.TabIndex = 41;
            this.labZG.Text = "总共";
            this.labZG.Visible = false;
            // 
            // txtCurePage
            // 
            this.txtCurePage.Location = new System.Drawing.Point(525, 7);
            this.txtCurePage.Name = "txtCurePage";
            this.txtCurePage.ReadOnly = true;
            this.txtCurePage.Size = new System.Drawing.Size(81, 21);
            this.txtCurePage.TabIndex = 40;
            this.txtCurePage.Visible = false;
            // 
            // labCur
            // 
            this.labCur.AutoSize = true;
            this.labCur.Location = new System.Drawing.Point(480, 13);
            this.labCur.Name = "labCur";
            this.labCur.Size = new System.Drawing.Size(41, 12);
            this.labCur.TabIndex = 39;
            this.labCur.Text = "当前页";
            this.labCur.Visible = false;
            // 
            // labLast
            // 
            this.labLast.AutoSize = true;
            this.labLast.Location = new System.Drawing.Point(895, 13);
            this.labLast.Name = "labLast";
            this.labLast.Size = new System.Drawing.Size(41, 12);
            this.labLast.TabIndex = 38;
            this.labLast.Text = "最后页";
            this.labLast.Visible = false;
            this.labLast.Click += new System.EventHandler(this.labLast_Click);
            // 
            // labNext
            // 
            this.labNext.AutoSize = true;
            this.labNext.Location = new System.Drawing.Point(848, 13);
            this.labNext.Name = "labNext";
            this.labNext.Size = new System.Drawing.Size(41, 12);
            this.labNext.TabIndex = 37;
            this.labNext.Text = "下一页";
            this.labNext.Visible = false;
            this.labNext.Click += new System.EventHandler(this.labNext_Click);
            // 
            // labPre
            // 
            this.labPre.AutoSize = true;
            this.labPre.Location = new System.Drawing.Point(801, 13);
            this.labPre.Name = "labPre";
            this.labPre.Size = new System.Drawing.Size(41, 12);
            this.labPre.TabIndex = 36;
            this.labPre.Text = "上一页";
            this.labPre.Visible = false;
            this.labPre.Click += new System.EventHandler(this.labPre_Click);
            // 
            // labFirst
            // 
            this.labFirst.AutoSize = true;
            this.labFirst.Location = new System.Drawing.Point(754, 13);
            this.labFirst.Name = "labFirst";
            this.labFirst.Size = new System.Drawing.Size(41, 12);
            this.labFirst.TabIndex = 35;
            this.labFirst.Text = "第一页";
            this.labFirst.Visible = false;
            this.labFirst.Click += new System.EventHandler(this.labFirst_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(377, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(66, 23);
            this.btnSearch.TabIndex = 17;
            this.btnSearch.Text = "查找";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // FrmAreaManager
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1129, 441);
            this.Controls.Add(this.treeList1);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmAreaManager";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "区域信息";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem toolRefresh;
        private DevExpress.XtraBars.BarButtonItem toolAdd;
        private DevExpress.XtraBars.BarButtonItem toolEdit;
        private DevExpress.XtraBars.BarButtonItem toolDel;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.ButtonEdit txtFilter;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
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