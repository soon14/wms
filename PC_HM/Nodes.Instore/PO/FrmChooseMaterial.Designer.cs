namespace Nodes.Instore
{
    partial class FrmChooseMaterial
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
            this.listUnits = new DevExpress.XtraEditors.LookUpEdit();
            this.txtQty = new DevExpress.XtraEditors.TextEdit();
            this.txtRemark = new DevExpress.XtraEditors.TextEdit();
            this.txtMaterial = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControl3 = new DevExpress.XtraGrid.GridControl();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridMaterial = new Nodes.BaseData.UcMaterialGrid();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.lblInfo = new Nodes.Controls.SplashLabel();
            this.queryConditionPanel = new Nodes.BaseData.UcQueryMaterial();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.listUnits.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaterial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.SuspendLayout();
            // 
            // listUnits
            // 
            this.listUnits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listUnits.Location = new System.Drawing.Point(62, 36);
            this.listUnits.Name = "listUnits";
            this.listUnits.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.listUnits.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("UnitName", "名称")});
            this.listUnits.Properties.DisplayMember = "UnitName";
            this.listUnits.Properties.DropDownRows = 4;
            this.listUnits.Properties.NullText = "";
            this.listUnits.Properties.ShowFooter = false;
            this.listUnits.Properties.ShowHeader = false;
            this.listUnits.Properties.ValueMember = "UnitCode";
            this.listUnits.Size = new System.Drawing.Size(184, 20);
            this.listUnits.TabIndex = 3;
            // 
            // txtQty
            // 
            this.txtQty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQty.Location = new System.Drawing.Point(62, 62);
            this.txtQty.Name = "txtQty";
            this.txtQty.Properties.Mask.EditMask = "[0-9]{1,8}([.]{1}[0-9]{1,2})?";
            this.txtQty.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtQty.Size = new System.Drawing.Size(184, 20);
            this.txtQty.TabIndex = 5;
            // 
            // txtRemark
            // 
            this.txtRemark.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRemark.Location = new System.Drawing.Point(62, 89);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Properties.MaxLength = 100;
            this.txtRemark.Size = new System.Drawing.Size(184, 20);
            this.txtRemark.TabIndex = 7;
            // 
            // txtMaterial
            // 
            this.txtMaterial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaterial.Enabled = false;
            this.txtMaterial.Location = new System.Drawing.Point(62, 10);
            this.txtMaterial.Name = "txtMaterial";
            this.txtMaterial.Size = new System.Drawing.Size(184, 20);
            this.txtMaterial.TabIndex = 1;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(6, 66);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(24, 12);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "数量";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(6, 93);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 12);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "备注";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(6, 40);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 12);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "计量单位";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(6, 14);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 12);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "物料";
            // 
            // gridControl2
            // 
            this.gridControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl2.Location = new System.Drawing.Point(4, 144);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(249, 107);
            this.gridControl2.TabIndex = 10;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.ID = "3a98ec71-1fe5-44ca-84c5-60a0368cf834";
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsView.ShowViewCaption = true;
            this.gridView2.ViewCaption = "库存列表";
            // 
            // gridControl3
            // 
            this.gridControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl3.Location = new System.Drawing.Point(4, 257);
            this.gridControl3.MainView = this.gridView3;
            this.gridControl3.Name = "gridControl3";
            this.gridControl3.Size = new System.Drawing.Size(249, 123);
            this.gridControl3.TabIndex = 11;
            this.gridControl3.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
            // 
            // gridView3
            // 
            this.gridView3.GridControl = this.gridControl3;
            this.gridView3.ID = "1fbd0a97-5e5a-4fa2-a3be-6827e37b9fef";
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsView.ShowViewCaption = true;
            this.gridView3.ViewCaption = "采购历史";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 105);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gridMaterial);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.simpleButton2);
            this.splitContainerControl1.Panel2.Controls.Add(this.lblInfo);
            this.splitContainerControl1.Panel2.Controls.Add(this.txtMaterial);
            this.splitContainerControl1.Panel2.Controls.Add(this.gridControl3);
            this.splitContainerControl1.Panel2.Controls.Add(this.labelControl2);
            this.splitContainerControl1.Panel2.Controls.Add(this.gridControl2);
            this.splitContainerControl1.Panel2.Controls.Add(this.listUnits);
            this.splitContainerControl1.Panel2.Controls.Add(this.labelControl3);
            this.splitContainerControl1.Panel2.Controls.Add(this.labelControl4);
            this.splitContainerControl1.Panel2.Controls.Add(this.txtQty);
            this.splitContainerControl1.Panel2.Controls.Add(this.labelControl1);
            this.splitContainerControl1.Panel2.Controls.Add(this.txtRemark);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(914, 407);
            this.splitContainerControl1.SplitterPosition = 256;
            this.splitContainerControl1.TabIndex = 22;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // gridMaterial
            // 
            this.gridMaterial.DataSource = null;
            this.gridMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMaterial.Location = new System.Drawing.Point(0, 0);
            this.gridMaterial.Name = "gridMaterial";
            this.gridMaterial.Size = new System.Drawing.Size(653, 407);
            this.gridMaterial.TabIndex = 0;
            this.gridMaterial.GridFocusedRowChanged += new Nodes.BaseData.UcMaterialGrid.FocusedRowChanged(this.OnFocusedHandlerChanged);
            this.gridMaterial.GridRowDoubleClick += new Nodes.BaseData.UcMaterialGrid.RowDoubleClick(this.OnGridRowDoubleClick);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton2.Location = new System.Drawing.Point(171, 115);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 9;
            this.simpleButton2.Text = "添加";
            this.simpleButton2.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.Location = new System.Drawing.Point(6, 121);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(48, 12);
            this.lblInfo.TabIndex = 8;
            this.lblInfo.Text = "保存成功";
            this.lblInfo.Visible = false;
            // 
            // queryConditionPanel
            // 
            this.queryConditionPanel.Barcode = null;
            this.queryConditionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.queryConditionPanel.Location = new System.Drawing.Point(2, 2);
            this.queryConditionPanel.MaterialCode = null;
            this.queryConditionPanel.MaterialName = null;
            this.queryConditionPanel.MaterialTypeCode = null;
            this.queryConditionPanel.Name = "queryConditionPanel";
            this.queryConditionPanel.Size = new System.Drawing.Size(662, 101);
            this.queryConditionPanel.Spec = null;
            this.queryConditionPanel.MTL_STR1 = null;
            this.queryConditionPanel.SupplierCode = null;
            this.queryConditionPanel.TabIndex = 0;
            this.queryConditionPanel.QueryComplete += new System.EventHandler(this.OnQueryComplete);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.queryConditionPanel);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(914, 105);
            this.panelControl1.TabIndex = 0;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl2.Location = new System.Drawing.Point(664, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(248, 101);
            this.panelControl2.TabIndex = 1;
            // 
            // FrmChooseMaterial
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(914, 512);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.panelControl1);
            this.MaximizeBox = false;
            this.Name = "FrmChooseMaterial";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "填写物料";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.listUnits.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaterial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtMaterial;
        private DevExpress.XtraEditors.TextEdit txtQty;
        private DevExpress.XtraEditors.LookUpEdit listUnits;
        private DevExpress.XtraEditors.TextEdit txtRemark;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.GridControl gridControl3;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private Nodes.Controls.SplashLabel lblInfo;
        private Nodes.BaseData.UcQueryMaterial queryConditionPanel;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Nodes.BaseData.UcMaterialGrid gridMaterial;
        private DevExpress.XtraEditors.PanelControl panelControl2;
    }
}