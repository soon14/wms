namespace Nodes.Outstore
{
    partial class FrmListPickPlan
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
            this.gridPlans = new DevExpress.XtraGrid.GridControl();
            this.gvPlans = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridPlans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPlans)).BeginInit();
            this.SuspendLayout();
            // 
            // gridPlans
            // 
            this.gridPlans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPlans.Location = new System.Drawing.Point(0, 0);
            this.gridPlans.MainView = this.gvPlans;
            this.gridPlans.Name = "gridPlans";
            this.gridPlans.Size = new System.Drawing.Size(892, 421);
            this.gridPlans.TabIndex = 18;
            this.gridPlans.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPlans});
            // 
            // gvPlans
            // 
            this.gvPlans.Appearance.ViewCaption.Options.UseTextOptions = true;
            this.gvPlans.Appearance.ViewCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gvPlans.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn1,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn18,
            this.gridColumn22,
            this.gridColumn3});
            this.gvPlans.GridControl = this.gridPlans;
            this.gvPlans.ID = "665c7f62-7757-4a44-a595-2921d977955c";
            this.gvPlans.Name = "gvPlans";
            this.gvPlans.OptionsView.EnableAppearanceOddRow = true;
            this.gvPlans.OptionsView.ShowAutoFilterRow = true;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "商品编码";
            this.gridColumn2.FieldName = "Material";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "商品条码";
            this.gridColumn10.FieldName = "SkuBarcode";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 1;
            this.gridColumn10.Width = 123;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "商品名称";
            this.gridColumn11.FieldName = "MaterialName";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 2;
            this.gridColumn11.Width = 145;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "套装";
            this.gridColumn1.FieldName = "ComMaterial";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 3;
            this.gridColumn1.Width = 84;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "拣货数量(库存单位)";
            this.gridColumn13.FieldName = "StockUnitQty";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 4;
            this.gridColumn13.Width = 128;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "拣货数量(销售单位)";
            this.gridColumn14.FieldName = "SaleUnitQty";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 5;
            this.gridColumn14.Width = 128;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "有效期";
            this.gridColumn18.FieldName = "ExpDate";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 6;
            this.gridColumn18.Width = 99;
            // 
            // gridColumn22
            // 
            this.gridColumn22.Caption = "拣货货位";
            this.gridColumn22.FieldName = "Location";
            this.gridColumn22.Name = "gridColumn22";
            this.gridColumn22.Visible = true;
            this.gridColumn22.VisibleIndex = 7;
            this.gridColumn22.Width = 130;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "创建时间";
            this.gridColumn3.FieldName = "CreateData";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 8;
            // 
            // FrmListPickPlan
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(892, 421);
            this.Controls.Add(this.gridPlans);
            this.Name = "FrmListPickPlan";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "拣货计划";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.gridPlans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPlans)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridPlans;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPlans;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;

    }
}