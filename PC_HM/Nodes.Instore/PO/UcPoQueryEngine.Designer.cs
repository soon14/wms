namespace Nodes.Instore
{
    partial class UcPoQueryEngine
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.dateEditTo = new DevExpress.XtraEditors.DateEdit();
            this.dateEditFrom = new DevExpress.XtraEditors.DateEdit();
            this.listBillTypes = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.listSales = new DevExpress.XtraEditors.LookUpEdit();
            this.txtMaterial = new DevExpress.XtraEditors.TextEdit();
            this.listSuppliers = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn26 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn27 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtBillID = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnCleanTextField = new DevExpress.XtraEditors.SimpleButton();
            this.listBillStates = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBillTypes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listSales.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaterial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listSuppliers.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBillID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBillStates.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dateEditTo
            // 
            this.dateEditTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dateEditTo.EditValue = null;
            this.dateEditTo.Location = new System.Drawing.Point(67, 208);
            this.dateEditTo.Name = "dateEditTo";
            this.dateEditTo.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dateEditTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditTo.Size = new System.Drawing.Size(200, 20);
            this.dateEditTo.TabIndex = 15;
            this.dateEditTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnControlKeyPress);
            // 
            // dateEditFrom
            // 
            this.dateEditFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dateEditFrom.EditValue = null;
            this.dateEditFrom.Location = new System.Drawing.Point(67, 182);
            this.dateEditFrom.Name = "dateEditFrom";
            this.dateEditFrom.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dateEditFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditFrom.Size = new System.Drawing.Size(200, 20);
            this.dateEditFrom.TabIndex = 13;
            this.dateEditFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnControlKeyPress);
            // 
            // listBillTypes
            // 
            this.listBillTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBillTypes.Location = new System.Drawing.Point(67, 56);
            this.listBillTypes.Name = "listBillTypes";
            this.listBillTypes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.listBillTypes.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ItemDesc", "名称")});
            this.listBillTypes.Properties.DisplayMember = "ItemDesc";
            this.listBillTypes.Properties.DropDownRows = 5;
            this.listBillTypes.Properties.NullText = "";
            this.listBillTypes.Properties.PopupSizeable = false;
            this.listBillTypes.Properties.ShowFooter = false;
            this.listBillTypes.Properties.ShowHeader = false;
            this.listBillTypes.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.listBillTypes.Properties.ValueMember = "ItemValue";
            this.listBillTypes.Size = new System.Drawing.Size(200, 20);
            this.listBillTypes.TabIndex = 5;
            this.listBillTypes.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.OnLookUpEditButtonClick);
            this.listBillTypes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnControlKeyPress);
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(5, 185);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(48, 12);
            this.labelControl8.TabIndex = 12;
            this.labelControl8.Text = "建单日期";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(5, 97);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(24, 12);
            this.labelControl7.TabIndex = 6;
            this.labelControl7.Text = "状态";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(47, 212);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(6, 12);
            this.labelControl6.TabIndex = 14;
            this.labelControl6.Text = "-";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(5, 58);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 12);
            this.labelControl5.TabIndex = 4;
            this.labelControl5.Text = "业务类型";
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.Location = new System.Drawing.Point(104, 236);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(79, 25);
            this.btnQuery.TabIndex = 16;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.OnQueryClick);
            // 
            // listSales
            // 
            this.listSales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listSales.Location = new System.Drawing.Point(67, 145);
            this.listSales.Name = "listSales";
            this.listSales.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.listSales.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("UserCode", "编号"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("UserName", 40, "姓名")});
            this.listSales.Properties.DisplayMember = "UserName";
            this.listSales.Properties.NullText = "";
            this.listSales.Properties.ShowFooter = false;
            this.listSales.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.listSales.Properties.ValueMember = "UserCode";
            this.listSales.Size = new System.Drawing.Size(200, 20);
            this.listSales.TabIndex = 11;
            this.listSales.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.OnLookUpEditButtonClick);
            this.listSales.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnControlKeyPress);
            // 
            // txtMaterial
            // 
            this.txtMaterial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaterial.Location = new System.Drawing.Point(67, 119);
            this.txtMaterial.Name = "txtMaterial";
            this.txtMaterial.Properties.MaxLength = 50;
            this.txtMaterial.Properties.NullValuePrompt = "支持编码、名称、拼音缩写的模糊查询";
            this.txtMaterial.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtMaterial.Size = new System.Drawing.Size(200, 20);
            this.txtMaterial.TabIndex = 9;
            this.txtMaterial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnControlKeyPress);
            // 
            // listSuppliers
            // 
            this.listSuppliers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listSuppliers.Location = new System.Drawing.Point(67, 30);
            this.listSuppliers.Name = "listSuppliers";
            this.listSuppliers.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.listSuppliers.Properties.DisplayMember = "SupplierNameS";
            this.listSuppliers.Properties.NullText = "";
            this.listSuppliers.Properties.ShowClearButton = false;
            this.listSuppliers.Properties.ShowFooter = false;
            this.listSuppliers.Properties.ValueMember = "SupplierCode";
            this.listSuppliers.Properties.View = this.searchLookUpEdit1View;
            this.listSuppliers.Size = new System.Drawing.Size(200, 20);
            this.listSuppliers.TabIndex = 3;
            this.listSuppliers.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.OnLookUpEditButtonClick);
            this.listSuppliers.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnControlKeyPress);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn8,
            this.gridColumn23,
            this.gridColumn24,
            this.gridColumn25,
            this.gridColumn26,
            this.gridColumn27});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.ID = "d05da462-d21e-44b1-a5d7-f3996607407c";
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "编号";
            this.gridColumn8.FieldName = "SupplierCode";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 0;
            this.gridColumn8.Width = 50;
            // 
            // gridColumn23
            // 
            this.gridColumn23.Caption = "名称";
            this.gridColumn23.FieldName = "SupplierNameS";
            this.gridColumn23.Name = "gridColumn23";
            this.gridColumn23.Visible = true;
            this.gridColumn23.VisibleIndex = 1;
            this.gridColumn23.Width = 90;
            // 
            // gridColumn24
            // 
            this.gridColumn24.Caption = "所属省份";
            this.gridColumn24.FieldName = "ProvinceName";
            this.gridColumn24.Name = "gridColumn24";
            this.gridColumn24.Visible = true;
            this.gridColumn24.VisibleIndex = 2;
            this.gridColumn24.Width = 72;
            // 
            // gridColumn25
            // 
            this.gridColumn25.Caption = "联系人";
            this.gridColumn25.FieldName = "ContactName";
            this.gridColumn25.Name = "gridColumn25";
            this.gridColumn25.Visible = true;
            this.gridColumn25.VisibleIndex = 3;
            this.gridColumn25.Width = 72;
            // 
            // gridColumn26
            // 
            this.gridColumn26.Caption = "联系电话";
            this.gridColumn26.FieldName = "Phone";
            this.gridColumn26.Name = "gridColumn26";
            this.gridColumn26.Visible = true;
            this.gridColumn26.VisibleIndex = 4;
            this.gridColumn26.Width = 70;
            // 
            // gridColumn27
            // 
            this.gridColumn27.Caption = "拼音缩写";
            this.gridColumn27.FieldName = "SupplierNamePY";
            this.gridColumn27.Name = "gridColumn27";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(5, 148);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(36, 12);
            this.labelControl4.TabIndex = 10;
            this.labelControl4.Text = "业务员";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(5, 32);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 12);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "供应商";
            // 
            // txtBillID
            // 
            this.txtBillID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBillID.Location = new System.Drawing.Point(67, 4);
            this.txtBillID.Name = "txtBillID";
            this.txtBillID.Properties.MaxLength = 50;
            this.txtBillID.Size = new System.Drawing.Size(200, 20);
            this.txtBillID.TabIndex = 1;
            this.txtBillID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnControlKeyPress);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(5, 122);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(24, 12);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "物料";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 12);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "采购单号";
            // 
            // btnCleanTextField
            // 
            this.btnCleanTextField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCleanTextField.Location = new System.Drawing.Point(188, 236);
            this.btnCleanTextField.Name = "btnCleanTextField";
            this.btnCleanTextField.Size = new System.Drawing.Size(79, 25);
            this.btnCleanTextField.TabIndex = 17;
            this.btnCleanTextField.Text = "清空";
            this.btnCleanTextField.Click += new System.EventHandler(this.OnCleanTextClick);
            // 
            // listBillStates
            // 
            this.listBillStates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBillStates.EditValue = "";
            this.listBillStates.Location = new System.Drawing.Point(67, 93);
            this.listBillStates.Name = "listBillStates";
            this.listBillStates.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.listBillStates.Properties.DisplayMember = "ItemDesc";
            this.listBillStates.Properties.SelectAllItemCaption = "全选";
            this.listBillStates.Properties.ValueMember = "ItemValue";
            this.listBillStates.Size = new System.Drawing.Size(200, 20);
            this.listBillStates.TabIndex = 7;
            this.listBillStates.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.OnBillStateButtonClick);
            this.listBillStates.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnControlKeyPress);
            // 
            // UcPoQueryConditionPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.btnCleanTextField);
            this.Controls.Add(this.dateEditTo);
            this.Controls.Add(this.dateEditFrom);
            this.Controls.Add(this.listBillTypes);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.listSales);
            this.Controls.Add(this.txtMaterial);
            this.Controls.Add(this.listSuppliers);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtBillID);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.listBillStates);
            this.Name = "UcPoQueryConditionPanel";
            this.Size = new System.Drawing.Size(270, 263);
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBillTypes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listSales.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaterial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listSuppliers.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBillID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBillStates.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.Columns.GridColumn gridColumn27;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn25;
        private DevExpress.XtraEditors.DateEdit dateEditTo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn26;
        private DevExpress.XtraEditors.DateEdit dateEditFrom;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn24;
        private DevExpress.XtraEditors.LookUpEdit listBillTypes;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraEditors.LookUpEdit listSales;
        private DevExpress.XtraEditors.TextEdit txtMaterial;
        private DevExpress.XtraEditors.SearchLookUpEdit listSuppliers;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn23;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtBillID;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnCleanTextField;
        private DevExpress.XtraEditors.CheckedComboBoxEdit listBillStates;
    }
}
