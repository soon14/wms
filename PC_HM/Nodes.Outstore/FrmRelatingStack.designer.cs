namespace Nodes.Outstore
{
    partial class FrmRelatingStack
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
            this.gridHeader = new DevExpress.XtraGrid.GridControl();
            this.gvHeader = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn40 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn44 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn53 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHeader)).BeginInit();
            this.SuspendLayout();
            // 
            // gridHeader
            // 
            this.gridHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridHeader.Location = new System.Drawing.Point(0, 0);
            this.gridHeader.MainView = this.gvHeader;
            this.gridHeader.Name = "gridHeader";
            this.gridHeader.Size = new System.Drawing.Size(751, 383);
            this.gridHeader.TabIndex = 5;
            this.gridHeader.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvHeader});
            // 
            // gvHeader
            // 
            this.gvHeader.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn24,
            this.gridColumn40,
            this.gridColumn44,
            this.gridColumn7,
            this.gridColumn53,
            this.gridColumn3,
            this.gridColumn1,
            this.gridColumn2});
            this.gvHeader.GridControl = this.gridHeader;
            this.gvHeader.GroupCount = 1;
            this.gvHeader.ID = "33679cea-d538-46c1-9954-e4e6bd4e3e85";
            this.gvHeader.Name = "gvHeader";
            this.gvHeader.OptionsSelection.MultiSelect = true;
            this.gvHeader.OptionsView.ColumnAutoWidth = false;
            this.gvHeader.OptionsView.EnableAppearanceOddRow = true;
            this.gvHeader.OptionsView.ShowAutoFilterRow = true;
            this.gvHeader.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn24, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn40, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumn24
            // 
            this.gridColumn24.Caption = "托盘号";
            this.gridColumn24.FieldName = "CT_CODE";
            this.gridColumn24.Name = "gridColumn24";
            this.gridColumn24.Width = 70;
            // 
            // gridColumn40
            // 
            this.gridColumn40.Caption = "托盘状态";
            this.gridColumn40.FieldName = "ITEM_DESC";
            this.gridColumn40.Name = "gridColumn40";
            this.gridColumn40.Visible = true;
            this.gridColumn40.VisibleIndex = 0;
            this.gridColumn40.Width = 100;
            // 
            // gridColumn44
            // 
            this.gridColumn44.Caption = "产品名称";
            this.gridColumn44.FieldName = "SKU_NAME";
            this.gridColumn44.Name = "gridColumn44";
            this.gridColumn44.Visible = true;
            this.gridColumn44.VisibleIndex = 1;
            this.gridColumn44.Width = 180;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "单位";
            this.gridColumn7.FieldName = "UM_NAME";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 2;
            // 
            // gridColumn53
            // 
            this.gridColumn53.Caption = "清点数";
            this.gridColumn53.FieldName = "QTY";
            this.gridColumn53.Name = "gridColumn53";
            this.gridColumn53.Visible = true;
            this.gridColumn53.VisibleIndex = 3;
            this.gridColumn53.Width = 80;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "生产日期";
            this.gridColumn3.FieldName = "PRODUCT_DATE";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 4;
            this.gridColumn3.Width = 120;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "清点人员";
            this.gridColumn1.FieldName = "CREATOR";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 5;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "复核人员";
            this.gridColumn2.FieldName = "CHECK_NAME";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 6;
            // 
            // FrmRelatingStack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 383);
            this.Controls.Add(this.gridHeader);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRelatingStack";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "关联托盘记录";
            this.Load += new System.EventHandler(this.FrmRelatingStack_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHeader)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridHeader;
        private DevExpress.XtraGrid.Views.Grid.GridView gvHeader;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn24;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn40;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn44;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn53;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
    }
}