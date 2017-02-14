namespace Nodes.Outstore
{
    partial class FrmSOWeight
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtBox = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lblCurrentWright = new DevExpress.XtraEditors.LabelControl();
            this.lblMsg = new Nodes.UI.SplashLabel();
            this.lblComState = new DevExpress.XtraEditors.LabelControl();
            this.btnOpenCom = new DevExpress.XtraEditors.SimpleButton();
            this.listComs = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lblCtCode = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.lblBillNO = new DevExpress.XtraEditors.LabelControl();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.lblCalcWeight = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.lblDiffByBill = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listComs.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("宋体", 18F);
            this.labelControl1.Location = new System.Drawing.Point(43, 42);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(96, 24);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "物流箱：";
            // 
            // txtBox
            // 
            this.txtBox.EditValue = "";
            this.txtBox.Location = new System.Drawing.Point(147, 26);
            this.txtBox.Name = "txtBox";
            this.txtBox.Properties.Appearance.Font = new System.Drawing.Font("宋体", 35F, System.Drawing.FontStyle.Bold);
            this.txtBox.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.txtBox.Properties.Appearance.Options.UseFont = true;
            this.txtBox.Properties.Appearance.Options.UseForeColor = true;
            this.txtBox.Size = new System.Drawing.Size(361, 60);
            this.txtBox.TabIndex = 1;
            this.txtBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("宋体", 18F);
            this.labelControl2.Location = new System.Drawing.Point(16, 239);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(120, 24);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "本箱重量：";
            // 
            // lblCurrentWright
            // 
            this.lblCurrentWright.Appearance.Font = new System.Drawing.Font("宋体", 35F, System.Drawing.FontStyle.Bold);
            this.lblCurrentWright.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblCurrentWright.Location = new System.Drawing.Point(147, 231);
            this.lblCurrentWright.Name = "lblCurrentWright";
            this.lblCurrentWright.Size = new System.Drawing.Size(25, 47);
            this.lblCurrentWright.TabIndex = 2;
            this.lblCurrentWright.Text = "0";
            // 
            // lblMsg
            // 
            this.lblMsg.Appearance.Font = new System.Drawing.Font("宋体", 28F);
            this.lblMsg.Location = new System.Drawing.Point(150, 361);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 37);
            this.lblMsg.TabIndex = 6;
            this.lblMsg.Visible = false;
            // 
            // lblComState
            // 
            this.lblComState.Appearance.Font = new System.Drawing.Font("宋体", 12F);
            this.lblComState.Location = new System.Drawing.Point(111, 532);
            this.lblComState.Name = "lblComState";
            this.lblComState.Size = new System.Drawing.Size(8, 16);
            this.lblComState.TabIndex = 3;
            this.lblComState.Text = " ";
            // 
            // btnOpenCom
            // 
            this.btnOpenCom.Appearance.Font = new System.Drawing.Font("宋体", 15F);
            this.btnOpenCom.Appearance.Options.UseFont = true;
            this.btnOpenCom.Location = new System.Drawing.Point(272, 496);
            this.btnOpenCom.Name = "btnOpenCom";
            this.btnOpenCom.Size = new System.Drawing.Size(98, 29);
            this.btnOpenCom.TabIndex = 2;
            this.btnOpenCom.Text = "打开串口";
            this.btnOpenCom.Click += new System.EventHandler(this.OnOpenComClick);
            // 
            // listComs
            // 
            this.listComs.Location = new System.Drawing.Point(108, 497);
            this.listComs.Name = "listComs";
            this.listComs.Properties.Appearance.Font = new System.Drawing.Font("宋体", 15F);
            this.listComs.Properties.Appearance.Options.UseFont = true;
            this.listComs.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.listComs.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.listComs.Properties.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5"});
            this.listComs.Size = new System.Drawing.Size(158, 29);
            this.listComs.TabIndex = 1;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("宋体", 15F);
            this.labelControl4.Location = new System.Drawing.Point(20, 499);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(80, 20);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "串口号：";
            // 
            // lblCtCode
            // 
            this.lblCtCode.Appearance.Font = new System.Drawing.Font("Arial", 35F);
            this.lblCtCode.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblCtCode.Location = new System.Drawing.Point(147, 110);
            this.lblCtCode.Name = "lblCtCode";
            this.lblCtCode.Size = new System.Drawing.Size(0, 53);
            this.lblCtCode.TabIndex = 0;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("宋体", 18F);
            this.labelControl3.Location = new System.Drawing.Point(40, 122);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(96, 24);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "物流箱：";
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Location = new System.Drawing.Point(527, 264);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(560, 352);
            this.gridControl1.TabIndex = 7;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn3,
            this.gridColumn2,
            this.gridColumn4});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.ID = "8fdc3c84-7486-4833-9de6-486b5ef8b755";
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowViewCaption = true;
            this.gridView1.RowHeight = 28;
            this.gridView1.ViewCaption = "箱内数据";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "商品名称";
            this.gridColumn1.FieldName = "SKU_NAME";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "商品条码";
            this.gridColumn5.FieldName = "SKU_BARCODE";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "单位";
            this.gridColumn6.FieldName = "UM_NAME";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "拣货数量";
            this.gridColumn3.DisplayFormat.FormatString = "{0:f0}";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn3.FieldName = "QTY";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "QTY", "{0:f0}")});
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "单品重量(g)";
            this.gridColumn2.DisplayFormat.FormatString = "{0:f0}";
            this.gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn2.FieldName = "WEIGHT";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 4;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.gridColumn4.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn4.Caption = "重量合计(g)";
            this.gridColumn4.DisplayFormat.FormatString = "{0:f0}";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "LINE_WEIGHT";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "LINE_WEIGHT", "合计:{0:f0}")});
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 5;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("宋体", 18F);
            this.labelControl5.Location = new System.Drawing.Point(64, 181);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(72, 24);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "单据：";
            // 
            // lblBillNO
            // 
            this.lblBillNO.Appearance.Font = new System.Drawing.Font("Arial", 20F);
            this.lblBillNO.Location = new System.Drawing.Point(147, 177);
            this.lblBillNO.Name = "lblBillNO";
            this.lblBillNO.Size = new System.Drawing.Size(0, 32);
            this.lblBillNO.TabIndex = 0;
            // 
            // gridControl2
            // 
            this.gridControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl2.Location = new System.Drawing.Point(527, 7);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(560, 251);
            this.gridControl2.TabIndex = 8;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10});
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.ID = "47340b32-b81b-48d8-b202-dea0b76faf0e";
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsView.EnableAppearanceOddRow = true;
            this.gridView2.OptionsView.ShowFooter = true;
            this.gridView2.OptionsView.ShowViewCaption = true;
            this.gridView2.RowHeight = 28;
            this.gridView2.ViewCaption = "本单据物流箱列表";
            this.gridView2.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView2_RowClick);
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "物流箱";
            this.gridColumn7.FieldName = "CT_CODE";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 0;
            this.gridColumn7.Width = 105;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "理论重量(kg)";
            this.gridColumn8.DisplayFormat.FormatString = "{0:f2}";
            this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn8.FieldName = "CALC_WEIGHT";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "CALC_WEIGHT", "{0:f2}")});
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 1;
            this.gridColumn8.Width = 103;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "实称重量(kg)";
            this.gridColumn9.DisplayFormat.FormatString = "{0:f2}";
            this.gridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn9.FieldName = "GROSS_WEIGHT";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "GROSS_WEIGHT", "{0:f2}")});
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 2;
            this.gridColumn9.Width = 105;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "重量偏差(kg)";
            this.gridColumn10.DisplayFormat.FormatString = "f2";
            this.gridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn10.FieldName = "DIFF";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DIFF", "{0:f2}")});
            this.gridColumn10.UnboundExpression = "[GROSS_WEIGHT] - [CALC_WEIGHT]";
            this.gridColumn10.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 3;
            this.gridColumn10.Width = 141;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("宋体", 18F);
            this.labelControl6.Location = new System.Drawing.Point(16, 306);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(120, 24);
            this.labelControl6.TabIndex = 2;
            this.labelControl6.Text = "理论重量：";
            // 
            // lblCalcWeight
            // 
            this.lblCalcWeight.Appearance.Font = new System.Drawing.Font("宋体", 35F, System.Drawing.FontStyle.Bold);
            this.lblCalcWeight.Appearance.ForeColor = System.Drawing.Color.Green;
            this.lblCalcWeight.Location = new System.Drawing.Point(147, 297);
            this.lblCalcWeight.Name = "lblCalcWeight";
            this.lblCalcWeight.Size = new System.Drawing.Size(25, 47);
            this.lblCalcWeight.TabIndex = 2;
            this.lblCalcWeight.Text = "0";
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("宋体", 18F);
            this.labelControl7.Location = new System.Drawing.Point(16, 424);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(120, 24);
            this.labelControl7.TabIndex = 2;
            this.labelControl7.Text = "本单偏差：";
            // 
            // lblDiffByBill
            // 
            this.lblDiffByBill.Appearance.Font = new System.Drawing.Font("宋体", 35F, System.Drawing.FontStyle.Bold);
            this.lblDiffByBill.Appearance.ForeColor = System.Drawing.Color.Green;
            this.lblDiffByBill.Location = new System.Drawing.Point(147, 414);
            this.lblDiffByBill.Name = "lblDiffByBill";
            this.lblDiffByBill.Size = new System.Drawing.Size(25, 47);
            this.lblDiffByBill.TabIndex = 2;
            this.lblDiffByBill.Text = "0";
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("宋体", 18F);
            this.labelControl8.Location = new System.Drawing.Point(328, 239);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(24, 24);
            this.labelControl8.TabIndex = 10;
            this.labelControl8.Text = "kg";
            // 
            // FrmSOWeight
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1095, 628);
            this.Controls.Add(this.lblDiffByBill);
            this.Controls.Add(this.lblCalcWeight);
            this.Controls.Add(this.lblCurrentWright);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.gridControl2);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.lblComState);
            this.Controls.Add(this.txtBox);
            this.Controls.Add(this.btnOpenCom);
            this.Controls.Add(this.listComs);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lblBillNO);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.lblCtCode);
            this.Controls.Add(this.labelControl8);
            this.Name = "FrmSOWeight";
            this.Text = "散货称重";
            this.Load += new System.EventHandler(this.OnFrmLoad);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFrmClosing);
            ((System.ComponentModel.ISupportInitialize)(this.txtBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listComs.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtBox;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl lblCurrentWright;
        private System.IO.Ports.SerialPort serialPort1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl lblComState;
        private DevExpress.XtraEditors.SimpleButton btnOpenCom;
        private DevExpress.XtraEditors.ComboBoxEdit listComs;
        private Nodes.UI.SplashLabel lblMsg;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.LabelControl lblCtCode;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl lblBillNO;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl lblCalcWeight;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl lblDiffByBill;
        private DevExpress.XtraEditors.LabelControl labelControl8;
    }
}