namespace Nodes.Outstore
{
    partial class FrmLoadRecords
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.BILL_NO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.C_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CREATE_DATE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.LAST_UPDATETIME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.dateOver = new DevExpress.XtraEditors.DateEdit();
            this.dateStart = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateOver.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateOver.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateStart.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(409, 431);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.BILL_NO,
            this.C_NAME,
            this.CREATE_DATE,
            this.LAST_UPDATETIME});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.ID = "ac97ae79-8145-436b-8c6b-76e1794bf671";
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowViewCaption = true;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.C_NAME, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView1.ViewCaption = " ";
            // 
            // BILL_NO
            // 
            this.BILL_NO.Caption = "单据编号";
            this.BILL_NO.FieldName = "BILL_NO";
            this.BILL_NO.Name = "BILL_NO";
            this.BILL_NO.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "BILL_NO", "总计：{0:F0} 单")});
            this.BILL_NO.Visible = true;
            this.BILL_NO.VisibleIndex = 0;
            this.BILL_NO.Width = 160;
            // 
            // C_NAME
            // 
            this.C_NAME.Caption = "客户名称";
            this.C_NAME.FieldName = "C_NAME";
            this.C_NAME.Name = "C_NAME";
            this.C_NAME.Visible = true;
            this.C_NAME.VisibleIndex = 1;
            this.C_NAME.Width = 135;
            // 
            // CREATE_DATE
            // 
            this.CREATE_DATE.Caption = "建单时间";
            this.CREATE_DATE.DisplayFormat.FormatString = "g";
            this.CREATE_DATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.CREATE_DATE.FieldName = "CREATE_DATE";
            this.CREATE_DATE.Name = "CREATE_DATE";
            this.CREATE_DATE.Visible = true;
            this.CREATE_DATE.VisibleIndex = 2;
            this.CREATE_DATE.Width = 135;
            // 
            // LAST_UPDATETIME
            // 
            this.LAST_UPDATETIME.Caption = "完成时间";
            this.LAST_UPDATETIME.DisplayFormat.FormatString = "g";
            this.LAST_UPDATETIME.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.LAST_UPDATETIME.FieldName = "LAST_UPDATETIME";
            this.LAST_UPDATETIME.Name = "LAST_UPDATETIME";
            this.LAST_UPDATETIME.Visible = true;
            this.LAST_UPDATETIME.VisibleIndex = 3;
            this.LAST_UPDATETIME.Width = 139;
            // 
            // gridControl2
            // 
            this.gridControl2.DataSource = this.bindingSource1;
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(0, 88);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(506, 343);
            this.gridControl2.TabIndex = 7;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn5,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn6});
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.ID = "754cf566-fb42-4ecb-af00-18ce69492a1e";
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.MultiSelect = true;
            this.gridView2.OptionsView.ColumnAutoWidth = false;
            this.gridView2.OptionsView.EnableAppearanceOddRow = true;
            this.gridView2.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn1, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView2.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView2_FocusedRowChanged);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "内部编号";
            this.gridColumn1.FieldName = "VehicleCode";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 3;
            this.gridColumn1.Width = 80;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "车牌号";
            this.gridColumn2.FieldName = "VehicleNO";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 100;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "容积(立方)";
            this.gridColumn5.FieldName = "VehicleVolume";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "当值司机";
            this.gridColumn3.FieldName = "UserName";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 112;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "送货路线";
            this.gridColumn4.FieldName = "RouteName";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            this.gridColumn4.Width = 210;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "是否启用";
            this.gridColumn6.FieldName = "IsActive";
            this.gridColumn6.Name = "gridColumn6";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gridControl2);
            this.splitContainerControl1.Panel1.Controls.Add(this.groupControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gridControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(920, 431);
            this.splitContainerControl1.SplitterPosition = 506;
            this.splitContainerControl1.TabIndex = 8;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.layoutControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(506, 88);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "查询日期";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.dateOver);
            this.layoutControl1.Controls.Add(this.dateStart);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.ID = "c8662340-4883-42cd-aa56-7ea1e6060fb7";
            this.layoutControl1.Location = new System.Drawing.Point(2, 22);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(502, 64);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // dateOver
            // 
            this.dateOver.EditValue = null;
            this.dateOver.Location = new System.Drawing.Point(56, 28);
            this.dateOver.Name = "dateOver";
            this.dateOver.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateOver.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateOver.Size = new System.Drawing.Size(442, 20);
            this.dateOver.StyleController = this.layoutControl1;
            this.dateOver.TabIndex = 5;
            // 
            // dateStart
            // 
            this.dateStart.EditValue = null;
            this.dateStart.Location = new System.Drawing.Point(56, 4);
            this.dateStart.Name = "dateStart";
            this.dateStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateStart.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateStart.Size = new System.Drawing.Size(442, 20);
            this.dateStart.StyleController = this.layoutControl1;
            this.dateStart.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutControlGroup1.Size = new System.Drawing.Size(502, 64);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.dateStart;
            this.layoutControlItem1.CustomizationFormText = "开始日期";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(498, 24);
            this.layoutControlItem1.Text = "开始日期";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(48, 12);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.dateOver;
            this.layoutControlItem2.CustomizationFormText = "结束日期";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(498, 36);
            this.layoutControlItem2.Text = "结束日期";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(48, 12);
            // 
            // FrmLoadRecords
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(920, 431);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "FrmLoadRecords";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "装车记录查询";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateOver.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateOver.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateStart.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn BILL_NO;
        private DevExpress.XtraGrid.Columns.GridColumn C_NAME;
        private DevExpress.XtraGrid.Columns.GridColumn CREATE_DATE;
        private DevExpress.XtraGrid.Columns.GridColumn LAST_UPDATETIME;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.DateEdit dateOver;
        private DevExpress.XtraEditors.DateEdit dateStart;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}