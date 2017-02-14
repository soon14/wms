namespace Nodes.Outstore
{
    partial class FrmScanContainer
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblMsg = new Nodes.UI.SplashLabel();
            this.btnReload = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtBarcode = new DevExpress.XtraEditors.TextEdit();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gridHeader = new DevExpress.XtraGrid.GridControl();
            this.gvHeader = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn40 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn49 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn53 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBarcode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHeader)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblMsg);
            this.panelControl1.Controls.Add(this.btnReload);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.txtBarcode);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(869, 100);
            this.panelControl1.TabIndex = 0;
            // 
            // lblMsg
            // 
            this.lblMsg.Appearance.Font = new System.Drawing.Font("宋体", 14F);
            this.lblMsg.Location = new System.Drawing.Point(89, 70);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(76, 19);
            this.lblMsg.TabIndex = 3;
            this.lblMsg.Text = "关联成功";
            this.lblMsg.Visible = false;
            // 
            // btnReload
            // 
            this.btnReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReload.Location = new System.Drawing.Point(693, 14);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(122, 48);
            this.btnReload.TabIndex = 2;
            this.btnReload.Text = "刷新单据列表";
            this.btnReload.Click += new System.EventHandler(this.OnReloadClick);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 30);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 12);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "物流箱条码";
            // 
            // txtBarcode
            // 
            this.txtBarcode.Location = new System.Drawing.Point(89, 12);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Properties.Appearance.Font = new System.Drawing.Font("宋体", 30F);
            this.txtBarcode.Properties.Appearance.Options.UseFont = true;
            this.txtBarcode.Properties.MaxLength = 30;
            this.txtBarcode.Size = new System.Drawing.Size(345, 52);
            this.txtBarcode.TabIndex = 1;
            this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarcode_KeyDown);
            // 
            // gridHeader
            // 
            this.gridHeader.DataSource = this.bindingSource1;
            this.gridHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridHeader.Location = new System.Drawing.Point(0, 100);
            this.gridHeader.MainView = this.gvHeader;
            this.gridHeader.Name = "gridHeader";
            this.gridHeader.Size = new System.Drawing.Size(869, 358);
            this.gridHeader.TabIndex = 1;
            this.gridHeader.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvHeader});
            // 
            // gvHeader
            // 
            this.gvHeader.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn40,
            this.gridColumn49,
            this.gridColumn53,
            this.gridColumn7,
            this.gridColumn6,
            this.gridColumn5,
            this.gridColumn1});
            this.gvHeader.GridControl = this.gridHeader;
            this.gvHeader.GroupRowHeight = 30;
            this.gvHeader.ID = "b469d842-3a2b-42a9-b638-961f99831a39";
            this.gvHeader.Name = "gvHeader";
            this.gvHeader.OptionsCustomization.AllowSort = false;
            this.gvHeader.OptionsView.ColumnAutoWidth = false;
            this.gvHeader.OptionsView.ShowIndicator = false;
            this.gvHeader.RowHeight = 30;
            // 
            // gridColumn40
            // 
            this.gridColumn40.Caption = "单据号";
            this.gridColumn40.FieldName = "BillNO";
            this.gridColumn40.Name = "gridColumn40";
            this.gridColumn40.Visible = true;
            this.gridColumn40.VisibleIndex = 0;
            this.gridColumn40.Width = 160;
            // 
            // gridColumn49
            // 
            this.gridColumn49.Caption = "业务员";
            this.gridColumn49.FieldName = "SalesMan";
            this.gridColumn49.Name = "gridColumn49";
            this.gridColumn49.Visible = true;
            this.gridColumn49.VisibleIndex = 2;
            // 
            // gridColumn53
            // 
            this.gridColumn53.Caption = "客户";
            this.gridColumn53.FieldName = "CustomerName";
            this.gridColumn53.Name = "gridColumn53";
            this.gridColumn53.Visible = true;
            this.gridColumn53.VisibleIndex = 1;
            this.gridColumn53.Width = 158;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "收货人";
            this.gridColumn7.FieldName = "Consignee";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 3;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "收货地址";
            this.gridColumn6.FieldName = "Address";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 4;
            this.gridColumn6.Width = 216;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "收货人电话";
            this.gridColumn5.FieldName = "ShTel";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            this.gridColumn5.Width = 94;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "下单日期";
            this.gridColumn1.DisplayFormat.FormatString = "g";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn1.FieldName = "CreateDate";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 6;
            this.gridColumn1.Width = 120;
            // 
            // FrmScanContainer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(869, 458);
            this.Controls.Add(this.gridHeader);
            this.Controls.Add(this.panelControl1);
            this.Name = "FrmScanContainer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "关联物流箱";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBarcode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHeader)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtBarcode;
        private DevExpress.XtraEditors.SimpleButton btnReload;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraGrid.GridControl gridHeader;
        private DevExpress.XtraGrid.Views.Grid.GridView gvHeader;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn40;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn49;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn53;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private Nodes.UI.SplashLabel lblMsg;
    }
}