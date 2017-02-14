namespace Nodes.BaseData
{
    partial class FrmContainerEdit
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnSaveAndClose = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtNum = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.listContainerType = new DevExpress.XtraEditors.LookUpEdit();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.txtWeight = new DevExpress.XtraEditors.ButtonEdit();
            this.checkMultIncrement = new System.Windows.Forms.CheckBox();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItemCtWeight = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemCtType = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemCtCode = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemCtName = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemAutoNum = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.checkSetBeginNum = new System.Windows.Forms.CheckBox();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listContainerType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCtWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCtType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCtCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCtName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAutoNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnSaveAndClose;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(239, 154);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(0, 26);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(99, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(99, 26);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Location = new System.Drawing.Point(251, 166);
            this.btnSaveAndClose.Name = "btnSaveAndClose";
            this.btnSaveAndClose.Size = new System.Drawing.Size(95, 22);
            this.btnSaveAndClose.StyleController = this.layoutControl1;
            this.btnSaveAndClose.TabIndex = 7;
            this.btnSaveAndClose.Text = "保存并关闭(&S)";
            this.btnSaveAndClose.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.checkSetBeginNum);
            this.layoutControl1.Controls.Add(this.txtNum);
            this.layoutControl1.Controls.Add(this.txtName);
            this.layoutControl1.Controls.Add(this.listContainerType);
            this.layoutControl1.Controls.Add(this.txtCode);
            this.layoutControl1.Controls.Add(this.txtWeight);
            this.layoutControl1.Controls.Add(this.checkMultIncrement);
            this.layoutControl1.Controls.Add(this.btnSaveAndClose);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.ID = "4afb84c7-0e20-4771-ba0b-14981aac406f";
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(722, 117, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(462, 200);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtNum
            // 
            this.txtNum.EditValue = "1";
            this.txtNum.Location = new System.Drawing.Point(64, 60);
            this.txtNum.Name = "txtNum";
            this.txtNum.Properties.EditFormat.FormatString = "d";
            this.txtNum.Properties.Mask.EditMask = "d";
            this.txtNum.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtNum.Properties.MaxLength = 4;
            this.txtNum.Properties.NullText = "1";
            this.txtNum.Size = new System.Drawing.Size(150, 20);
            this.txtNum.StyleController = this.layoutControl1;
            this.txtNum.TabIndex = 13;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(270, 36);
            this.txtName.Name = "txtName";
            this.txtName.Properties.MaxLength = 5;
            this.txtName.Properties.NullText = "00001";
            this.txtName.Size = new System.Drawing.Size(180, 20);
            this.txtName.StyleController = this.layoutControl1;
            this.txtName.TabIndex = 7;
            // 
            // listContainerType
            // 
            this.listContainerType.Location = new System.Drawing.Point(64, 12);
            this.listContainerType.Name = "listContainerType";
            this.listContainerType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.listContainerType.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ItemDesc", "分类")});
            this.listContainerType.Properties.DisplayMember = "ItemDesc";
            this.listContainerType.Properties.NullText = "";
            this.listContainerType.Properties.ShowFooter = false;
            this.listContainerType.Properties.ShowHeader = false;
            this.listContainerType.Properties.ValueMember = "ItemValue";
            this.listContainerType.Size = new System.Drawing.Size(150, 20);
            this.listContainerType.StyleController = this.layoutControl1;
            this.listContainerType.TabIndex = 11;
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(64, 36);
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.MaxLength = 10;
            this.txtCode.Size = new System.Drawing.Size(150, 20);
            this.txtCode.StyleController = this.layoutControl1;
            this.txtCode.TabIndex = 6;
            // 
            // txtWeight
            // 
            this.txtWeight.Location = new System.Drawing.Point(270, 60);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "克", -1, false, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
            this.txtWeight.Properties.DisplayFormat.FormatString = "f2";
            this.txtWeight.Properties.Mask.EditMask = "f2";
            this.txtWeight.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtWeight.Properties.MaxLength = 9;
            this.txtWeight.Size = new System.Drawing.Size(180, 20);
            this.txtWeight.StyleController = this.layoutControl1;
            this.txtWeight.TabIndex = 12;
            // 
            // checkMultIncrement
            // 
            this.checkMultIncrement.Location = new System.Drawing.Point(218, 12);
            this.checkMultIncrement.Name = "checkMultIncrement";
            this.checkMultIncrement.Size = new System.Drawing.Size(114, 20);
            this.checkMultIncrement.TabIndex = 10;
            this.checkMultIncrement.Text = "批量新增";
            this.checkMultIncrement.UseVisualStyleBackColor = true;
            this.checkMultIncrement.CheckedChanged += new System.EventHandler(this.checkMultIncrement_CheckedChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.emptySpaceItem2,
            this.layoutControlItemCtWeight,
            this.layoutControlItemCtType,
            this.layoutControlItemCtCode,
            this.layoutControlItem6,
            this.layoutControlItemCtName,
            this.layoutControlItemAutoNum,
            this.emptySpaceItem3,
            this.emptySpaceItem1,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(462, 200);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 72);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(442, 82);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItemCtWeight
            // 
            this.layoutControlItemCtWeight.Control = this.txtWeight;
            this.layoutControlItemCtWeight.CustomizationFormText = "容器重量";
            this.layoutControlItemCtWeight.Location = new System.Drawing.Point(206, 48);
            this.layoutControlItemCtWeight.Name = "layoutControlItemCtWeight";
            this.layoutControlItemCtWeight.Size = new System.Drawing.Size(236, 24);
            this.layoutControlItemCtWeight.Text = "容器重量";
            this.layoutControlItemCtWeight.TextSize = new System.Drawing.Size(48, 12);
            // 
            // layoutControlItemCtType
            // 
            this.layoutControlItemCtType.Control = this.listContainerType;
            this.layoutControlItemCtType.CustomizationFormText = "分类";
            this.layoutControlItemCtType.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemCtType.Name = "layoutControlItemCtType";
            this.layoutControlItemCtType.Size = new System.Drawing.Size(206, 24);
            this.layoutControlItemCtType.Text = "容器类别";
            this.layoutControlItemCtType.TextSize = new System.Drawing.Size(48, 12);
            // 
            // layoutControlItemCtCode
            // 
            this.layoutControlItemCtCode.Control = this.txtCode;
            this.layoutControlItemCtCode.CustomizationFormText = "编号";
            this.layoutControlItemCtCode.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItemCtCode.Name = "layoutControlItemCtCode";
            this.layoutControlItemCtCode.Size = new System.Drawing.Size(206, 24);
            this.layoutControlItemCtCode.Text = "容器编号";
            this.layoutControlItemCtCode.TextSize = new System.Drawing.Size(48, 12);
            this.layoutControlItemCtCode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.checkMultIncrement;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(206, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(118, 24);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItemCtName
            // 
            this.layoutControlItemCtName.Control = this.txtName;
            this.layoutControlItemCtName.CustomizationFormText = "简称";
            this.layoutControlItemCtName.Location = new System.Drawing.Point(206, 24);
            this.layoutControlItemCtName.Name = "layoutControlItemCtName";
            this.layoutControlItemCtName.Size = new System.Drawing.Size(236, 24);
            this.layoutControlItemCtName.Text = "容器简称";
            this.layoutControlItemCtName.TextSize = new System.Drawing.Size(48, 12);
            this.layoutControlItemCtName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItemAutoNum
            // 
            this.layoutControlItemAutoNum.Control = this.txtNum;
            this.layoutControlItemAutoNum.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItemAutoNum.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItemAutoNum.Name = "layoutControlItemAutoNum";
            this.layoutControlItemAutoNum.Size = new System.Drawing.Size(206, 24);
            this.layoutControlItemAutoNum.Text = "新增数量";
            this.layoutControlItemAutoNum.TextSize = new System.Drawing.Size(48, 12);
            this.layoutControlItemAutoNum.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 154);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(239, 26);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(338, 154);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(104, 26);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // checkSetBeginNum
            // 
            this.checkSetBeginNum.Location = new System.Drawing.Point(336, 12);
            this.checkSetBeginNum.Name = "checkSetBeginNum";
            this.checkSetBeginNum.Size = new System.Drawing.Size(114, 20);
            this.checkSetBeginNum.TabIndex = 14;
            this.checkSetBeginNum.Text = "指定初值(简称)";
            this.checkSetBeginNum.UseVisualStyleBackColor = true;
            this.checkSetBeginNum.CheckedChanged += new System.EventHandler(this.checkSetBeginNum_CheckedChanged);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.checkSetBeginNum;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(324, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(118, 24);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // FrmContainerEdit
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(462, 200);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmContainerEdit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "容器-新增";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listContainerType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCtWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCtType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCtCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCtName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemAutoNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.SimpleButton btnSaveAndClose;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private System.Windows.Forms.CheckBox checkMultIncrement;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCtCode;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCtName;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.LookUpEdit listContainerType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCtType;
        private DevExpress.XtraEditors.ButtonEdit txtWeight;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCtWeight;
        private DevExpress.XtraEditors.TextEdit txtNum;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemAutoNum;
        private System.Windows.Forms.CheckBox checkSetBeginNum;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;

    }
}