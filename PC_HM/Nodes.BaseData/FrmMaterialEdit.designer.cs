namespace Nodes.BaseData
{
    partial class FrmMaterialEdit
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lookUpUmName = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditTemperature = new DevExpress.XtraEditors.LookUpEdit();
            this.txtSpec = new DevExpress.XtraEditors.TextEdit();
            this.spinEditSecurityQty = new DevExpress.XtraEditors.SpinEdit();
            this.spinEditMin = new DevExpress.XtraEditors.SpinEdit();
            this.btnSaveClose = new DevExpress.XtraEditors.SimpleButton();
            this.spinEditMax = new DevExpress.XtraEditors.SpinEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpUmName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditTemperature.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpec.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditSecurityQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditMin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditMax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lookUpUmName);
            this.layoutControl1.Controls.Add(this.lookUpEdit1);
            this.layoutControl1.Controls.Add(this.lookUpEditTemperature);
            this.layoutControl1.Controls.Add(this.txtSpec);
            this.layoutControl1.Controls.Add(this.spinEditSecurityQty);
            this.layoutControl1.Controls.Add(this.spinEditMin);
            this.layoutControl1.Controls.Add(this.btnSaveClose);
            this.layoutControl1.Controls.Add(this.spinEditMax);
            this.layoutControl1.Controls.Add(this.txtName);
            this.layoutControl1.Controls.Add(this.txtCode);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.ID = "fffbd862-5da5-443c-8cfc-156a8520799c";
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(929, 66, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(468, 263);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lookUpUmName
            // 
            this.lookUpUmName.Location = new System.Drawing.Point(295, 132);
            this.lookUpUmName.Name = "lookUpUmName";
            this.lookUpUmName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpUmName.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("UnitCode", "单位编码"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("UnitName", "单位")});
            this.lookUpUmName.Properties.DisplayMember = "UnitName";
            this.lookUpUmName.Properties.NullText = "";
            this.lookUpUmName.Properties.SortColumnIndex = 1;
            this.lookUpUmName.Properties.ValueMember = "UnitCode";
            this.lookUpUmName.Size = new System.Drawing.Size(161, 20);
            this.lookUpUmName.StyleController = this.layoutControl1;
            this.lookUpUmName.TabIndex = 41;
            // 
            // lookUpEdit1
            // 
            this.lookUpEdit1.Location = new System.Drawing.Point(99, 108);
            this.lookUpEdit1.Name = "lookUpEdit1";
            this.lookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit1.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ItemDesc", "类型")});
            this.lookUpEdit1.Properties.DisplayMember = "ItemDesc";
            this.lookUpEdit1.Properties.NullText = "";
            this.lookUpEdit1.Properties.ValueMember = "ItemValue";
            this.lookUpEdit1.Size = new System.Drawing.Size(103, 20);
            this.lookUpEdit1.StyleController = this.layoutControl1;
            this.lookUpEdit1.TabIndex = 40;
            // 
            // lookUpEditTemperature
            // 
            this.lookUpEditTemperature.AllowDrop = true;
            this.lookUpEditTemperature.Location = new System.Drawing.Point(293, 108);
            this.lookUpEditTemperature.Name = "lookUpEditTemperature";
            this.lookUpEditTemperature.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.lookUpEditTemperature.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditTemperature.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TemperatureName", 10, "条件名"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("LowerLimit", 10, "低温"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("UpperLimit", 10, "高温")});
            this.lookUpEditTemperature.Properties.DisplayMember = "TemperatureName";
            this.lookUpEditTemperature.Properties.NullText = "";
            this.lookUpEditTemperature.Properties.ValueMember = "TemperatureCode";
            this.lookUpEditTemperature.Size = new System.Drawing.Size(163, 20);
            this.lookUpEditTemperature.StyleController = this.layoutControl1;
            this.lookUpEditTemperature.TabIndex = 36;
            // 
            // txtSpec
            // 
            this.txtSpec.Enabled = false;
            this.txtSpec.Location = new System.Drawing.Point(99, 60);
            this.txtSpec.Name = "txtSpec";
            this.txtSpec.Size = new System.Drawing.Size(357, 20);
            this.txtSpec.StyleController = this.layoutControl1;
            this.txtSpec.TabIndex = 35;
            // 
            // spinEditSecurityQty
            // 
            this.spinEditSecurityQty.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditSecurityQty.Location = new System.Drawing.Point(99, 132);
            this.spinEditSecurityQty.Name = "spinEditSecurityQty";
            this.spinEditSecurityQty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditSecurityQty.Properties.Mask.EditMask = "d";
            this.spinEditSecurityQty.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.spinEditSecurityQty.Properties.NullText = "0";
            this.spinEditSecurityQty.Size = new System.Drawing.Size(105, 20);
            this.spinEditSecurityQty.StyleController = this.layoutControl1;
            this.spinEditSecurityQty.TabIndex = 34;
            // 
            // spinEditMin
            // 
            this.spinEditMin.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditMin.Location = new System.Drawing.Point(99, 84);
            this.spinEditMin.Name = "spinEditMin";
            this.spinEditMin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditMin.Properties.Mask.EditMask = "d";
            this.spinEditMin.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.spinEditMin.Properties.NullText = "0";
            this.spinEditMin.Size = new System.Drawing.Size(103, 20);
            this.spinEditMin.StyleController = this.layoutControl1;
            this.spinEditMin.TabIndex = 22;
            // 
            // btnSaveClose
            // 
            this.btnSaveClose.Location = new System.Drawing.Point(300, 156);
            this.btnSaveClose.Name = "btnSaveClose";
            this.btnSaveClose.Size = new System.Drawing.Size(156, 22);
            this.btnSaveClose.StyleController = this.layoutControl1;
            this.btnSaveClose.TabIndex = 38;
            this.btnSaveClose.Text = "保存并关闭";
            this.btnSaveClose.Click += new System.EventHandler(this.btnSaveClose_Click);
            // 
            // spinEditMax
            // 
            this.spinEditMax.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditMax.Location = new System.Drawing.Point(293, 84);
            this.spinEditMax.Name = "spinEditMax";
            this.spinEditMax.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditMax.Properties.Mask.EditMask = "d";
            this.spinEditMax.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.spinEditMax.Properties.NullText = "0";
            this.spinEditMax.Size = new System.Drawing.Size(163, 20);
            this.spinEditMax.StyleController = this.layoutControl1;
            this.spinEditMax.TabIndex = 21;
            // 
            // txtName
            // 
            this.txtName.Enabled = false;
            this.txtName.Location = new System.Drawing.Point(99, 36);
            this.txtName.Name = "txtName";
            this.txtName.Properties.MaxLength = 200;
            this.txtName.Size = new System.Drawing.Size(357, 20);
            this.txtName.StyleController = this.layoutControl1;
            this.txtName.TabIndex = 7;
            // 
            // txtCode
            // 
            this.txtCode.Enabled = false;
            this.txtCode.Location = new System.Drawing.Point(99, 12);
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCode.Properties.MaxLength = 50;
            this.txtCode.Size = new System.Drawing.Size(357, 20);
            this.txtCode.StyleController = this.layoutControl1;
            this.txtCode.TabIndex = 6;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem1,
            this.layoutControlItem8,
            this.layoutControlItem7,
            this.layoutControlItem9,
            this.emptySpaceItem1,
            this.layoutControlItem3,
            this.emptySpaceItem3,
            this.layoutControlItem12,
            this.layoutControlItem4,
            this.layoutControlItem10,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(468, 263);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtName;
            this.layoutControlItem2.CustomizationFormText = "客户名称";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(448, 24);
            this.layoutControlItem2.Text = "物料名称";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(84, 12);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtCode;
            this.layoutControlItem1.CustomizationFormText = "客户编码";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(448, 24);
            this.layoutControlItem1.Text = "物料编码";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(84, 12);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.spinEditMin;
            this.layoutControlItem8.CustomizationFormText = "低储";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(194, 24);
            this.layoutControlItem8.Text = "低储";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(84, 12);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.spinEditMax;
            this.layoutControlItem7.CustomizationFormText = "高储";
            this.layoutControlItem7.Location = new System.Drawing.Point(194, 72);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(254, 24);
            this.layoutControlItem7.Text = "高储";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(84, 12);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.txtSpec;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(448, 24);
            this.layoutControlItem9.Text = "规格";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(84, 12);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 144);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(288, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnSaveClose;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(288, 144);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(160, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 170);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(448, 73);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.spinEditSecurityQty;
            this.layoutControlItem12.CustomizationFormText = "单货位安全库存";
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 120);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(196, 24);
            this.layoutControlItem12.Text = "单货位安全库存";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(84, 12);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lookUpEditTemperature;
            this.layoutControlItem4.CustomizationFormText = "存储条件";
            this.layoutControlItem4.Location = new System.Drawing.Point(194, 96);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(254, 24);
            this.layoutControlItem4.Text = "存储条件";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(84, 12);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.lookUpEdit1;
            this.layoutControlItem10.CustomizationFormText = "商品类型";
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(194, 24);
            this.layoutControlItem10.Text = "商品类型";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(84, 12);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.lookUpUmName;
            this.layoutControlItem5.CustomizationFormText = "单位";
            this.layoutControlItem5.Location = new System.Drawing.Point(196, 120);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(252, 24);
            this.layoutControlItem5.Text = "单位";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(84, 12);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.spinEditMin;
            this.layoutControlItem6.CustomizationFormText = "低储";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem6.Name = "layoutControlItem8";
            this.layoutControlItem6.Size = new System.Drawing.Size(284, 24);
            this.layoutControlItem6.Text = "低储";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(60, 12);
            this.layoutControlItem6.TextToControlDistance = 5;
            // 
            // FrmMaterialEdit
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(468, 263);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmMaterialEdit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "物料编辑";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lookUpUmName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditTemperature.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpec.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditSecurityQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditMin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditMax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SpinEdit spinEditMin;
        private DevExpress.XtraEditors.SpinEdit spinEditMax;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.SpinEdit spinEditSecurityQty;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.TextEdit txtSpec;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditTemperature;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.SimpleButton btnSaveClose;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraEditors.LookUpEdit lookUpUmName;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}