namespace Nodes.Instore
{
    partial class UcAsnQueryEngine
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
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.txtBillID = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnCleanTextField = new DevExpress.XtraEditors.SimpleButton();
            this.listBillStates = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.txtPoNO = new DevExpress.XtraEditors.TextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.listBillTypes = new DevExpress.XtraEditors.LookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn24 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn26 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn27 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.listSuppliers = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtMaterial = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.dateCloseStart = new DevExpress.XtraEditors.DateEdit();
            this.dateCloseEnd = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBillID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBillStates.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPoNO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBillTypes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listSuppliers.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaterial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateCloseStart.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateCloseStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateCloseEnd.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateCloseEnd.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dateEditTo
            // 
            this.dateEditTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dateEditTo.EditValue = null;
            this.dateEditTo.Location = new System.Drawing.Point(67, 111);
            this.dateEditTo.Name = "dateEditTo";
            this.dateEditTo.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dateEditTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditTo.Size = new System.Drawing.Size(201, 20);
            this.dateEditTo.TabIndex = 17;
            this.dateEditTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnControlKeyPress);
            // 
            // dateEditFrom
            // 
            this.dateEditFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dateEditFrom.EditValue = null;
            this.dateEditFrom.Location = new System.Drawing.Point(67, 85);
            this.dateEditFrom.Name = "dateEditFrom";
            this.dateEditFrom.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dateEditFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEditFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditFrom.Size = new System.Drawing.Size(201, 20);
            this.dateEditFrom.TabIndex = 15;
            this.dateEditFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnControlKeyPress);
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(5, 88);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(48, 12);
            this.labelControl8.TabIndex = 14;
            this.labelControl8.Text = "建单日期";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(5, 59);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(24, 12);
            this.labelControl7.TabIndex = 8;
            this.labelControl7.Text = "状态";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(47, 115);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(6, 12);
            this.labelControl6.TabIndex = 16;
            this.labelControl6.Text = "-";
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.Location = new System.Drawing.Point(105, 184);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(79, 25);
            this.btnQuery.TabIndex = 18;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.OnQueryClick);
            // 
            // txtBillID
            // 
            this.txtBillID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBillID.Location = new System.Drawing.Point(67, 4);
            this.txtBillID.Name = "txtBillID";
            this.txtBillID.Properties.MaxLength = 50;
            this.txtBillID.Size = new System.Drawing.Size(201, 20);
            this.txtBillID.TabIndex = 1;
            this.txtBillID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnControlKeyPress);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 12);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "收货单号";
            // 
            // btnCleanTextField
            // 
            this.btnCleanTextField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCleanTextField.Location = new System.Drawing.Point(189, 184);
            this.btnCleanTextField.Name = "btnCleanTextField";
            this.btnCleanTextField.Size = new System.Drawing.Size(79, 25);
            this.btnCleanTextField.TabIndex = 19;
            this.btnCleanTextField.Text = "清空";
            this.btnCleanTextField.Click += new System.EventHandler(this.OnCleanTextClick);
            // 
            // listBillStates
            // 
            this.listBillStates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBillStates.EditValue = "";
            this.listBillStates.Location = new System.Drawing.Point(67, 56);
            this.listBillStates.Name = "listBillStates";
            this.listBillStates.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.listBillStates.Properties.DisplayMember = "ItemDesc";
            this.listBillStates.Properties.SelectAllItemCaption = "全选";
            this.listBillStates.Properties.ValueMember = "ItemValue";
            this.listBillStates.Size = new System.Drawing.Size(201, 20);
            this.listBillStates.TabIndex = 9;
            this.listBillStates.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.OnBillStateButtonClick);
            this.listBillStates.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnControlKeyPress);
            // 
            // txtPoNO
            // 
            this.txtPoNO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPoNO.Location = new System.Drawing.Point(67, 224);
            this.txtPoNO.Name = "txtPoNO";
            this.txtPoNO.Properties.MaxLength = 50;
            this.txtPoNO.Size = new System.Drawing.Size(201, 20);
            this.txtPoNO.TabIndex = 7;
            this.txtPoNO.Visible = false;
            this.txtPoNO.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnControlKeyPress);
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(5, 228);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(48, 12);
            this.labelControl9.TabIndex = 6;
            this.labelControl9.Text = "原始单号";
            this.labelControl9.Visible = false;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(5, 32);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 12);
            this.labelControl5.TabIndex = 4;
            this.labelControl5.Text = "业务类型";
            // 
            // listBillTypes
            // 
            this.listBillTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBillTypes.Location = new System.Drawing.Point(67, 30);
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
            this.listBillTypes.Size = new System.Drawing.Size(201, 20);
            this.listBillTypes.TabIndex = 5;
            this.listBillTypes.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.OnLookUpEditButtonClick);
            this.listBillTypes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnControlKeyPress);
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
            this.searchLookUpEdit1View.ID = "18261bd2-ee16-461d-ace8-896365347e8c";
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = true;
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
            // listSuppliers
            // 
            this.listSuppliers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listSuppliers.Location = new System.Drawing.Point(67, 225);
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
            this.listSuppliers.Size = new System.Drawing.Size(201, 20);
            this.listSuppliers.TabIndex = 3;
            this.listSuppliers.Visible = false;
            this.listSuppliers.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.OnLookUpEditButtonClick);
            this.listSuppliers.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnControlKeyPress);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(5, 227);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 12);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "供应商";
            this.labelControl2.Visible = false;
            // 
            // txtMaterial
            // 
            this.txtMaterial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaterial.Location = new System.Drawing.Point(67, 225);
            this.txtMaterial.Name = "txtMaterial";
            this.txtMaterial.Properties.MaxLength = 50;
            this.txtMaterial.Properties.NullValuePrompt = "支持模糊查询";
            this.txtMaterial.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtMaterial.Size = new System.Drawing.Size(201, 20);
            this.txtMaterial.TabIndex = 11;
            this.txtMaterial.Visible = false;
            this.txtMaterial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnControlKeyPress);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(5, 227);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(24, 12);
            this.labelControl3.TabIndex = 10;
            this.labelControl3.Text = "物料";
            this.labelControl3.Visible = false;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(47, 165);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(6, 12);
            this.labelControl4.TabIndex = 16;
            this.labelControl4.Text = "-";
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(5, 138);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(48, 12);
            this.labelControl10.TabIndex = 14;
            this.labelControl10.Text = "完成日期";
            // 
            // dateCloseStart
            // 
            this.dateCloseStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dateCloseStart.EditValue = null;
            this.dateCloseStart.Location = new System.Drawing.Point(67, 135);
            this.dateCloseStart.Name = "dateCloseStart";
            this.dateCloseStart.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dateCloseStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateCloseStart.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateCloseStart.Size = new System.Drawing.Size(201, 20);
            this.dateCloseStart.TabIndex = 15;
            this.dateCloseStart.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnControlKeyPress);
            // 
            // dateCloseEnd
            // 
            this.dateCloseEnd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dateCloseEnd.EditValue = null;
            this.dateCloseEnd.Location = new System.Drawing.Point(67, 161);
            this.dateCloseEnd.Name = "dateCloseEnd";
            this.dateCloseEnd.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dateCloseEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateCloseEnd.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateCloseEnd.Size = new System.Drawing.Size(201, 20);
            this.dateCloseEnd.TabIndex = 17;
            this.dateCloseEnd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnControlKeyPress);
            // 
            // UcAsnQueryEngine
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.btnCleanTextField);
            this.Controls.Add(this.dateCloseEnd);
            this.Controls.Add(this.dateCloseStart);
            this.Controls.Add(this.dateEditTo);
            this.Controls.Add(this.dateEditFrom);
            this.Controls.Add(this.labelControl10);
            this.Controls.Add(this.listBillTypes);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.txtMaterial);
            this.Controls.Add(this.listSuppliers);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtPoNO);
            this.Controls.Add(this.txtBillID);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.listBillStates);
            this.Name = "UcAsnQueryEngine";
            this.Size = new System.Drawing.Size(271, 218);
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBillID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBillStates.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPoNO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBillTypes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listSuppliers.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaterial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateCloseStart.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateCloseStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateCloseEnd.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateCloseEnd.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit dateEditTo;
        private DevExpress.XtraEditors.DateEdit dateEditFrom;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraEditors.TextEdit txtBillID;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnCleanTextField;
        private DevExpress.XtraEditors.CheckedComboBoxEdit listBillStates;
        private DevExpress.XtraEditors.TextEdit txtPoNO;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LookUpEdit listBillTypes;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn23;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn24;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn25;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn26;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn27;
        private DevExpress.XtraEditors.SearchLookUpEdit listSuppliers;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtMaterial;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.DateEdit dateCloseStart;
        private DevExpress.XtraEditors.DateEdit dateCloseEnd;
    }
}
