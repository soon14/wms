namespace Nodes.Outstore
{
    partial class FrmSOWeightLoading
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
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition2 = new DevExpress.XtraGrid.StyleFormatCondition();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.gvContainers = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControlForContainer = new DevExpress.XtraGrid.GridControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.btnOpenCom = new DevExpress.XtraEditors.SimpleButton();
            this.serialPort2 = new System.IO.Ports.SerialPort(this.components);
            this.lblComState = new DevExpress.XtraEditors.LabelControl();
            this.lblMsg = new Nodes.UI.SplashLabel();
            this.lblTruckName = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lblBIllNO = new DevExpress.XtraEditors.LabelControl();
            this.lblDiffByPallet = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.lblCalcWeight = new DevExpress.XtraEditors.LabelControl();
            this.lblCustomerName = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEditCom = new DevExpress.XtraEditors.ComboBoxEdit();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvCtDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControlForDetail = new DevExpress.XtraGrid.GridControl();
            this.txtBoxForContainerCode = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblCurrentWeight = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lblCustomerAddress = new DevExpress.XtraEditors.LabelControl();
            this.lblVhTrainNo = new DevExpress.XtraEditors.LabelControl();
            this.lblCtCode = new DevExpress.XtraEditors.LabelControl();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gvContainers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlForContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditCom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCtDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlForDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBoxForContainerCode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "状态(测试后隐藏)";
            this.gridColumn17.FieldName = "CT_STATE";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Width = 20;
            // 
            // labelControl14
            // 
            this.labelControl14.Appearance.Font = new System.Drawing.Font("宋体", 12F);
            this.labelControl14.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.labelControl14.Location = new System.Drawing.Point(3, 529);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(408, 16);
            this.labelControl14.TabIndex = 70;
            this.labelControl14.Text = "★扫描顺序:先扫描地牛或物流箱(若包含),最后扫描托盘;";
            // 
            // gvContainers
            // 
            this.gvContainers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn18,
            this.gridColumn1,
            this.gridColumn20,
            this.gridColumn19,
            this.gridColumn21,
            this.gridColumn2,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn12,
            this.gridColumn17});
            styleFormatCondition1.Appearance.BackColor = System.Drawing.Color.Lime;
            styleFormatCondition1.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            styleFormatCondition1.Appearance.BorderColor = System.Drawing.Color.White;
            styleFormatCondition1.Appearance.Options.UseBackColor = true;
            styleFormatCondition1.Appearance.Options.UseBorderColor = true;
            styleFormatCondition1.ApplyToRow = true;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
            styleFormatCondition1.Expression = "CT_TYPE=\'50\' AND CT_STATE=\'87\'";
            styleFormatCondition2.Appearance.BackColor = System.Drawing.Color.Lime;
            styleFormatCondition2.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            styleFormatCondition2.Appearance.BorderColor = System.Drawing.Color.White;
            styleFormatCondition2.Appearance.Options.UseBackColor = true;
            styleFormatCondition2.Appearance.Options.UseBorderColor = true;
            styleFormatCondition2.ApplyToRow = true;
            styleFormatCondition2.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
            styleFormatCondition2.Expression = "CT_TYPE=\'51\' AND CT_STATE=\'892\'";
            this.gvContainers.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1,
            styleFormatCondition2});
            this.gvContainers.GridControl = this.gridControlForContainer;
            this.gvContainers.ID = "7df7112a-3dd8-41ab-87d3-cef177501a73";
            this.gvContainers.IndicatorWidth = 30;
            this.gvContainers.Name = "gvContainers";
            this.gvContainers.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gvContainers.OptionsView.EnableAppearanceOddRow = true;
            this.gvContainers.OptionsView.ShowFooter = true;
            this.gvContainers.OptionsView.ShowViewCaption = true;
            this.gvContainers.RowHeight = 22;
            this.gvContainers.ViewCaption = "当前车辆所有单据的容器列表";
            this.gvContainers.ViewCaptionHeight = 2;
            this.gvContainers.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvContainers_RowClick);
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "装车顺序";
            this.gridColumn18.FieldName = "IN_VH_SORT";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 0;
            this.gridColumn18.Width = 45;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "容器编号";
            this.gridColumn1.FieldName = "CT_CODE";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "CT_CODE", "{0:f0}")});
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 72;
            // 
            // gridColumn20
            // 
            this.gridColumn20.Caption = "订单ID";
            this.gridColumn20.FieldName = "BILL_ID";
            this.gridColumn20.Name = "gridColumn20";
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "订单编号";
            this.gridColumn19.FieldName = "BILL_NO";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 2;
            this.gridColumn19.Width = 137;
            // 
            // gridColumn21
            // 
            this.gridColumn21.Caption = "件数";
            this.gridColumn21.FieldName = "SAILQTY";
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "SAILQTY", "{0:f0}")});
            this.gridColumn21.Visible = true;
            this.gridColumn21.VisibleIndex = 3;
            this.gridColumn21.Width = 45;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "理论重量(kg)";
            this.gridColumn2.DisplayFormat.FormatString = "{0:f2}";
            this.gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn2.FieldName = "CALC_WEIGHT";
            this.gridColumn2.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "CALC_WEIGHT", "{0:f2}")});
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 4;
            this.gridColumn2.Width = 84;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "实际重量(kg)";
            this.gridColumn15.DisplayFormat.FormatString = "{0:f2}";
            this.gridColumn15.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn15.FieldName = "GROSS_WEIGHT";
            this.gridColumn15.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "GROSS_WEIGHT", "{0:f2}")});
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 5;
            this.gridColumn15.Width = 84;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "重量偏差(kg)";
            this.gridColumn16.DisplayFormat.FormatString = "{0:f2}";
            this.gridColumn16.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn16.FieldName = "DIFF";
            this.gridColumn16.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DIFF", "{0:f2}")});
            this.gridColumn16.UnboundExpression = "[GROSS_WEIGHT] - [CALC_WEIGHT]";
            this.gridColumn16.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 6;
            this.gridColumn16.Width = 84;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "容器类型";
            this.gridColumn12.FieldName = "CT_TYPE";
            this.gridColumn12.Name = "gridColumn12";
            // 
            // gridControlForContainer
            // 
            this.gridControlForContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControlForContainer.Location = new System.Drawing.Point(507, 7);
            this.gridControlForContainer.MainView = this.gvContainers;
            this.gridControlForContainer.Name = "gridControlForContainer";
            this.gridControlForContainer.Size = new System.Drawing.Size(585, 290);
            this.gridControlForContainer.TabIndex = 66;
            this.gridControlForContainer.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvContainers});
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("宋体", 15F);
            this.labelControl4.Location = new System.Drawing.Point(10, 457);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(110, 20);
            this.labelControl4.TabIndex = 42;
            this.labelControl4.Text = "地磅串口号:";
            // 
            // timer1
            // 
            this.timer1.Interval = 2800;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // btnOpenCom
            // 
            this.btnOpenCom.Appearance.Font = new System.Drawing.Font("宋体", 13F);
            this.btnOpenCom.Appearance.Options.UseFont = true;
            this.btnOpenCom.Location = new System.Drawing.Point(288, 452);
            this.btnOpenCom.Name = "btnOpenCom";
            this.btnOpenCom.Size = new System.Drawing.Size(83, 30);
            this.btnOpenCom.TabIndex = 44;
            this.btnOpenCom.Text = "打开串口";
            this.btnOpenCom.Click += new System.EventHandler(this.OnOpenComClick);
            // 
            // lblComState
            // 
            this.lblComState.Appearance.Font = new System.Drawing.Font("宋体", 12F);
            this.lblComState.Location = new System.Drawing.Point(124, 488);
            this.lblComState.Name = "lblComState";
            this.lblComState.Size = new System.Drawing.Size(8, 16);
            this.lblComState.TabIndex = 45;
            this.lblComState.Text = " ";
            // 
            // lblMsg
            // 
            this.lblMsg.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lblMsg.Appearance.Font = new System.Drawing.Font("宋体", 28F);
            this.lblMsg.Location = new System.Drawing.Point(15, 291);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(76, 38);
            this.lblMsg.TabIndex = 61;
            this.lblMsg.Text = "成功";
            this.lblMsg.Visible = false;
            // 
            // lblTruckName
            // 
            this.lblTruckName.Appearance.Font = new System.Drawing.Font("宋体", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTruckName.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lblTruckName.Location = new System.Drawing.Point(129, 18);
            this.lblTruckName.Name = "lblTruckName";
            this.lblTruckName.Size = new System.Drawing.Size(0, 29);
            this.lblTruckName.TabIndex = 68;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("宋体", 18F);
            this.labelControl3.Location = new System.Drawing.Point(15, 19);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(108, 24);
            this.labelControl3.TabIndex = 67;
            this.labelControl3.Text = "所属车辆:";
            // 
            // lblBIllNO
            // 
            this.lblBIllNO.Appearance.Font = new System.Drawing.Font("宋体", 15F);
            this.lblBIllNO.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lblBIllNO.Location = new System.Drawing.Point(110, 341);
            this.lblBIllNO.Name = "lblBIllNO";
            this.lblBIllNO.Size = new System.Drawing.Size(0, 20);
            this.lblBIllNO.TabIndex = 49;
            // 
            // lblDiffByPallet
            // 
            this.lblDiffByPallet.Appearance.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold);
            this.lblDiffByPallet.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblDiffByPallet.Location = new System.Drawing.Point(101, 243);
            this.lblDiffByPallet.Name = "lblDiffByPallet";
            this.lblDiffByPallet.Size = new System.Drawing.Size(21, 40);
            this.lblDiffByPallet.TabIndex = 59;
            this.lblDiffByPallet.Text = "0";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("宋体", 15F);
            this.labelControl6.Location = new System.Drawing.Point(14, 342);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(90, 20);
            this.labelControl6.TabIndex = 46;
            this.labelControl6.Text = "当前订单:";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("宋体", 15F);
            this.labelControl5.Location = new System.Drawing.Point(11, 214);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(90, 20);
            this.labelControl5.TabIndex = 58;
            this.labelControl5.Text = "理论重量:";
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("宋体", 15F);
            this.labelControl10.Location = new System.Drawing.Point(10, 253);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(90, 20);
            this.labelControl10.TabIndex = 57;
            this.labelControl10.Text = "重量偏差:";
            // 
            // lblCalcWeight
            // 
            this.lblCalcWeight.Appearance.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold);
            this.lblCalcWeight.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblCalcWeight.Location = new System.Drawing.Point(101, 204);
            this.lblCalcWeight.Name = "lblCalcWeight";
            this.lblCalcWeight.Size = new System.Drawing.Size(21, 40);
            this.lblCalcWeight.TabIndex = 60;
            this.lblCalcWeight.Text = "0";
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.Appearance.Font = new System.Drawing.Font("宋体", 15F);
            this.lblCustomerName.Location = new System.Drawing.Point(110, 375);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(0, 20);
            this.lblCustomerName.TabIndex = 51;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("宋体", 15F);
            this.labelControl7.Location = new System.Drawing.Point(14, 376);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(90, 20);
            this.labelControl7.TabIndex = 48;
            this.labelControl7.Text = "客户信息:";
            // 
            // comboBoxEditCom
            // 
            this.comboBoxEditCom.Location = new System.Drawing.Point(124, 453);
            this.comboBoxEditCom.Name = "comboBoxEditCom";
            this.comboBoxEditCom.Properties.Appearance.Font = new System.Drawing.Font("宋体", 15F);
            this.comboBoxEditCom.Properties.Appearance.Options.UseFont = true;
            this.comboBoxEditCom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditCom.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxEditCom.Properties.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5"});
            this.comboBoxEditCom.Size = new System.Drawing.Size(158, 29);
            this.comboBoxEditCom.TabIndex = 43;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "商品名称";
            this.gridColumn3.FieldName = "SKU_NAME";
            this.gridColumn3.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "SKU_NAME", "{0:f0}")});
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 177;
            // 
            // gvCtDetails
            // 
            this.gvCtDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn6,
            this.gridColumn11,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn7,
            this.gridColumn8});
            this.gvCtDetails.GridControl = this.gridControlForDetail;
            this.gvCtDetails.ID = "69999f2b-a442-49f5-bd6b-1cc9c8bfcba1";
            this.gvCtDetails.IndicatorWidth = 30;
            this.gvCtDetails.Name = "gvCtDetails";
            this.gvCtDetails.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gvCtDetails.OptionsView.ColumnAutoWidth = false;
            this.gvCtDetails.OptionsView.EnableAppearanceOddRow = true;
            this.gvCtDetails.OptionsView.ShowFooter = true;
            this.gvCtDetails.OptionsView.ShowViewCaption = true;
            this.gvCtDetails.ViewCaption = "容器明细";
            this.gvCtDetails.ViewCaptionHeight = 2;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "商品编码";
            this.gridColumn6.FieldName = "SKU_CODE";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 1;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "规格";
            this.gridColumn11.FieldName = "SPEC";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "拣货数量";
            this.gridColumn4.FieldName = "SAILQTY";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "SAILQTY", "{0:f0}")});
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 65;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "单位";
            this.gridColumn5.FieldName = "SAILUMNAME";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 65;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "单位重量(g)";
            this.gridColumn9.FieldName = "WEIGHT";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 5;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "总重量(g)";
            this.gridColumn10.FieldName = "TotalWeight";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalWeight", "{0:f2}")});
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 6;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "拣货人";
            this.gridColumn7.FieldName = "USER_NAME";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 7;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "拣货时间";
            this.gridColumn8.FieldName = "PICK_DATE";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 8;
            // 
            // gridControlForDetail
            // 
            this.gridControlForDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControlForDetail.Location = new System.Drawing.Point(507, 299);
            this.gridControlForDetail.MainView = this.gvCtDetails;
            this.gridControlForDetail.Name = "gridControlForDetail";
            this.gridControlForDetail.Size = new System.Drawing.Size(585, 323);
            this.gridControlForDetail.TabIndex = 52;
            this.gridControlForDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCtDetails});
            // 
            // txtBoxForContainerCode
            // 
            this.txtBoxForContainerCode.EditValue = "";
            this.txtBoxForContainerCode.Location = new System.Drawing.Point(129, 59);
            this.txtBoxForContainerCode.Name = "txtBoxForContainerCode";
            this.txtBoxForContainerCode.Properties.Appearance.Font = new System.Drawing.Font("宋体", 26F, System.Drawing.FontStyle.Bold);
            this.txtBoxForContainerCode.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.txtBoxForContainerCode.Properties.Appearance.Options.UseFont = true;
            this.txtBoxForContainerCode.Properties.Appearance.Options.UseForeColor = true;
            this.txtBoxForContainerCode.Size = new System.Drawing.Size(361, 46);
            this.txtBoxForContainerCode.TabIndex = 54;
            this.txtBoxForContainerCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("宋体", 18F);
            this.labelControl1.Location = new System.Drawing.Point(11, 70);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(108, 24);
            this.labelControl1.TabIndex = 53;
            this.labelControl1.Text = "扫描容器:";
            // 
            // lblCurrentWeight
            // 
            this.lblCurrentWeight.Appearance.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold);
            this.lblCurrentWeight.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblCurrentWeight.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Horizontal;
            this.lblCurrentWeight.Location = new System.Drawing.Point(101, 164);
            this.lblCurrentWeight.Name = "lblCurrentWeight";
            this.lblCurrentWeight.Size = new System.Drawing.Size(21, 40);
            this.lblCurrentWeight.TabIndex = 55;
            this.lblCurrentWeight.Text = "0";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("宋体", 15F);
            this.labelControl2.Location = new System.Drawing.Point(12, 172);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(90, 20);
            this.labelControl2.TabIndex = 56;
            this.labelControl2.Text = "当前重量:";
            // 
            // lblCustomerAddress
            // 
            this.lblCustomerAddress.Appearance.Font = new System.Drawing.Font("宋体", 15F);
            this.lblCustomerAddress.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblCustomerAddress.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCustomerAddress.Location = new System.Drawing.Point(110, 399);
            this.lblCustomerAddress.Name = "lblCustomerAddress";
            this.lblCustomerAddress.Size = new System.Drawing.Size(391, 52);
            this.lblCustomerAddress.TabIndex = 50;
            // 
            // lblVhTrainNo
            // 
            this.lblVhTrainNo.Appearance.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblVhTrainNo.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lblVhTrainNo.Location = new System.Drawing.Point(323, 22);
            this.lblVhTrainNo.Name = "lblVhTrainNo";
            this.lblVhTrainNo.Size = new System.Drawing.Size(236, 19);
            this.lblVhTrainNo.TabIndex = 69;
            this.lblVhTrainNo.Text = "装车编号yyyyMMddHHmmssms";
            this.lblVhTrainNo.Visible = false;
            // 
            // lblCtCode
            // 
            this.lblCtCode.Appearance.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCtCode.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lblCtCode.Location = new System.Drawing.Point(129, 115);
            this.lblCtCode.Name = "lblCtCode";
            this.lblCtCode.Size = new System.Drawing.Size(0, 33);
            this.lblCtCode.TabIndex = 62;
            // 
            // labelControl13
            // 
            this.labelControl13.Appearance.Font = new System.Drawing.Font("宋体", 12F);
            this.labelControl13.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.labelControl13.Location = new System.Drawing.Point(3, 510);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(312, 16);
            this.labelControl13.TabIndex = 64;
            this.labelControl13.Text = "★需先在[装车信息]里分派装车并指定车辆;";
            // 
            // labelControl11
            // 
            this.labelControl11.Appearance.Font = new System.Drawing.Font("宋体", 18F);
            this.labelControl11.Location = new System.Drawing.Point(8, 124);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(108, 24);
            this.labelControl11.TabIndex = 63;
            this.labelControl11.Text = "    容器:";
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("宋体", 15F);
            this.labelControl8.Location = new System.Drawing.Point(13, 410);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(90, 20);
            this.labelControl8.TabIndex = 47;
            this.labelControl8.Text = "客户地址:";
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("宋体", 18F);
            this.labelControl9.Location = new System.Drawing.Point(268, 169);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(24, 24);
            this.labelControl9.TabIndex = 65;
            this.labelControl9.Text = "kg";
            // 
            // labelControl12
            // 
            this.labelControl12.Appearance.Font = new System.Drawing.Font("宋体", 18F);
            this.labelControl12.Location = new System.Drawing.Point(268, 249);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(24, 24);
            this.labelControl12.TabIndex = 65;
            this.labelControl12.Text = "kg";
            // 
            // labelControl15
            // 
            this.labelControl15.Appearance.Font = new System.Drawing.Font("宋体", 12F);
            this.labelControl15.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.labelControl15.Location = new System.Drawing.Point(3, 549);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(440, 16);
            this.labelControl15.TabIndex = 71;
            this.labelControl15.Text = "★可使用手持功能[容器配对]找寻当前托盘所属订单的物流箱;";
            // 
            // FrmSOWeightLoading
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1095, 628);
            this.Controls.Add(this.lblDiffByPallet);
            this.Controls.Add(this.lblCalcWeight);
            this.Controls.Add(this.lblCurrentWeight);
            this.Controls.Add(this.labelControl15);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.labelControl14);
            this.Controls.Add(this.gridControlForContainer);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.btnOpenCom);
            this.Controls.Add(this.lblComState);
            this.Controls.Add(this.lblTruckName);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.lblBIllNO);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl10);
            this.Controls.Add(this.lblCustomerName);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.comboBoxEditCom);
            this.Controls.Add(this.txtBoxForContainerCode);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.gridControlForDetail);
            this.Controls.Add(this.lblCustomerAddress);
            this.Controls.Add(this.lblVhTrainNo);
            this.Controls.Add(this.lblCtCode);
            this.Controls.Add(this.labelControl13);
            this.Controls.Add(this.labelControl11);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.labelControl12);
            this.Controls.Add(this.labelControl9);
            this.Name = "FrmSOWeightLoading";
            this.Text = "装车称重";
            this.Load += new System.EventHandler(this.OnFrmLoad);
            this.Shown += new System.EventHandler(this.FrmSOWeight_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFrmClosing);
            ((System.ComponentModel.ISupportInitialize)(this.gvContainers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlForContainer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditCom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCtDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlForDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBoxForContainerCode.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraGrid.Views.Grid.GridView gvContainers;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.GridControl gridControlForContainer;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private System.Windows.Forms.Timer timer1;
        private System.IO.Ports.SerialPort serialPort1;
        private DevExpress.XtraEditors.SimpleButton btnOpenCom;
        private System.IO.Ports.SerialPort serialPort2;
        private DevExpress.XtraEditors.LabelControl lblComState;
        private Nodes.UI.SplashLabel lblMsg;
        private DevExpress.XtraEditors.LabelControl lblTruckName;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl lblBIllNO;
        private DevExpress.XtraEditors.LabelControl lblDiffByPallet;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl lblCalcWeight;
        private DevExpress.XtraEditors.LabelControl lblCustomerName;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditCom;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCtDetails;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.GridControl gridControlForDetail;
        private DevExpress.XtraEditors.TextEdit txtBoxForContainerCode;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lblCurrentWeight;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl lblCustomerAddress;
        private DevExpress.XtraEditors.LabelControl lblVhTrainNo;
        private DevExpress.XtraEditors.LabelControl lblCtCode;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LabelControl labelControl15;

    }
}