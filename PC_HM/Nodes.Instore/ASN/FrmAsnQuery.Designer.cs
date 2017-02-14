namespace Nodes.Instore
{
    partial class FrmAsnQuery
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.gvDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn28 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gdHeader = new DevExpress.XtraGrid.GridControl();
            this.gvHeader = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.ucQueryCondition = new Nodes.Instore.UcAsnQueryEngine();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gvDetails
            // 
            this.gvDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn7,
            this.gridColumn28,
            this.gridColumn10,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15});
            this.gvDetails.GridControl = this.gdHeader;
            this.gvDetails.ID = "7e0fd096-bf85-4d6e-bb93-6c650fed1ee4";
            this.gvDetails.Name = "gvDetails";
            this.gvDetails.OptionsMenu.EnableIndicatorMenu = false;
            this.gvDetails.OptionsView.ColumnAutoWidth = false;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "物料编码";
            this.gridColumn7.FieldName = "MaterialCode";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 0;
            this.gridColumn7.Width = 82;
            // 
            // gridColumn28
            // 
            this.gridColumn28.Caption = "名称";
            this.gridColumn28.FieldName = "MaterialName";
            this.gridColumn28.Name = "gridColumn28";
            this.gridColumn28.Visible = true;
            this.gridColumn28.VisibleIndex = 1;
            this.gridColumn28.Width = 120;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "规格型号";
            this.gridColumn10.FieldName = "Spec";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 2;
            this.gridColumn10.Width = 60;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "数量";
            this.gridColumn13.FieldName = "PlanQty";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 3;
            this.gridColumn13.Width = 50;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "单价";
            this.gridColumn14.DisplayFormat.FormatString = "0.00";
            this.gridColumn14.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn14.FieldName = "Price";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 4;
            this.gridColumn14.Width = 50;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "金额";
            this.gridColumn15.DisplayFormat.FormatString = "0.00";
            this.gridColumn15.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn15.FieldName = "PlanAmount";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 5;
            this.gridColumn15.Width = 50;
            // 
            // gdHeader
            // 
            this.gdHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.LevelTemplate = this.gvDetails;
            gridLevelNode1.RelationName = "Details";
            this.gdHeader.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gdHeader.Location = new System.Drawing.Point(282, 0);
            this.gdHeader.MainView = this.gvHeader;
            this.gdHeader.Name = "gdHeader";
            this.gdHeader.Size = new System.Drawing.Size(620, 363);
            this.gdHeader.TabIndex = 1;
            this.gdHeader.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvHeader,
            this.gvDetails});
            // 
            // gvHeader
            // 
            this.gvHeader.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn6,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn11,
            this.gridColumn5,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn19,
            this.gridColumn20});
            this.gvHeader.GridControl = this.gdHeader;
            this.gvHeader.GroupCount = 1;
            this.gvHeader.ID = "85e5a684-5964-40e9-97bb-9c31440d1422";
            this.gvHeader.Name = "gvHeader";
            this.gvHeader.OptionsDetail.AllowOnlyOneMasterRowExpanded = true;
            this.gvHeader.OptionsDetail.AllowZoomDetail = false;
            this.gvHeader.OptionsDetail.ShowDetailTabs = false;
            this.gvHeader.OptionsPrint.ExpandAllDetails = true;
            this.gvHeader.OptionsPrint.PrintDetails = true;
            this.gvHeader.OptionsView.ColumnAutoWidth = false;
            this.gvHeader.OptionsView.ShowAutoFilterRow = true;
            this.gvHeader.OptionsView.ShowGroupedColumns = true;
            this.gvHeader.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn20, DevExpress.Data.ColumnSortOrder.Descending)});
            this.gvHeader.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.OnGvHeaderFocusedRowChanged);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "收货单号";
            this.gridColumn1.FieldName = "BillID";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 141;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "业务类型";
            this.gridColumn6.FieldName = "BillTypeDesc";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 1;
            this.gridColumn6.Width = 78;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "单据状态";
            this.gridColumn2.FieldName = "BillStateDesc";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 69;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "业务员";
            this.gridColumn3.FieldName = "Sales";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            this.gridColumn3.Width = 61;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "供应商";
            this.gridColumn4.FieldName = "SupplierName";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 4;
            this.gridColumn4.Width = 152;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "交货日期";
            this.gridColumn11.FieldName = "DeliveryDate";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 5;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "原始采购单号";
            this.gridColumn5.FieldName = "PoNO";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 6;
            this.gridColumn5.Width = 85;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "合同号";
            this.gridColumn8.FieldName = "ContractNO";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 7;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "备注";
            this.gridColumn9.FieldName = "Remark";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 8;
            this.gridColumn9.Width = 107;
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "建单人";
            this.gridColumn19.FieldName = "Creator";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 9;
            this.gridColumn19.Width = 66;
            // 
            // gridColumn20
            // 
            this.gridColumn20.Caption = "建单日期";
            this.gridColumn20.DisplayFormat.FormatString = "g";
            this.gridColumn20.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn20.FieldName = "CreateDate";
            this.gridColumn20.GroupInterval = DevExpress.XtraGrid.ColumnGroupInterval.DateRange;
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 10;
            this.gridColumn20.Width = 111;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.ucQueryCondition);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(277, 422);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "查找条件";
            // 
            // ucQueryCondition
            // 
            this.ucQueryCondition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucQueryCondition.Location = new System.Drawing.Point(2, 22);
            this.ucQueryCondition.Name = "ucQueryCondition";
            this.ucQueryCondition.Size = new System.Drawing.Size(273, 398);
            this.ucQueryCondition.TabIndex = 0;
            this.ucQueryCondition.QueryCompleted += new Nodes.Instore.UcAsnQueryEngine.QueryComplete(this.OnQueryCompleted);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(496, 17);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 26);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消(&C)";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnOK);
            this.panelControl1.Controls.Add(this.btnCancel);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(282, 363);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(620, 59);
            this.panelControl1.TabIndex = 2;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(404, 17);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(86, 26);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.OnOKClick);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(277, 0);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(5, 422);
            this.splitterControl1.TabIndex = 3;
            this.splitterControl1.TabStop = false;
            // 
            // FrmAsnQuery
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(902, 422);
            this.Controls.Add(this.gdHeader);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.groupControl1);
            this.Name = "FrmAsnQuery";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查找收货单";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.gvDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gdHeader;
        private DevExpress.XtraGrid.Views.Grid.GridView gvHeader;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDetails;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn28;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private UcAsnQueryEngine ucQueryCondition;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
    }
}