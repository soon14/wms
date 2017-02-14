namespace Nodes.WMS.Inbound
{
    partial class FrmPrintLabel
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
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.btnDesignMaterialLabel = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrintMaterialLabel = new DevExpress.XtraEditors.SimpleButton();
            this.spSequenceQty = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtSequenceBarcode = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.btnReprintSequence = new DevExpress.XtraEditors.SimpleButton();
            this.spReprintBarcodeFrom = new DevExpress.XtraEditors.SpinEdit();
            this.spReprintSequenceQty = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.xtraTabPage4 = new DevExpress.XtraTab.XtraTabPage();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spSequenceQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSequenceBarcode.Properties)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spReprintBarcodeFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spReprintSequenceQty.Properties)).BeginInit();
            this.xtraTabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.HeaderButtonsShowMode = DevExpress.XtraTab.TabButtonShowMode.Always;
            this.xtraTabControl1.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.xtraTabControl1.HeaderOrientation = DevExpress.XtraTab.TabOrientation.Horizontal;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(691, 415);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPage4});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.btnDesignMaterialLabel);
            this.xtraTabPage1.Controls.Add(this.btnPrintMaterialLabel);
            this.xtraTabPage1.Controls.Add(this.spSequenceQty);
            this.xtraTabPage1.Controls.Add(this.labelControl3);
            this.xtraTabPage1.Controls.Add(this.txtSequenceBarcode);
            this.xtraTabPage1.Controls.Add(this.labelControl4);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(589, 409);
            this.xtraTabPage1.Text = "打印流水号标签";
            // 
            // btnDesignMaterialLabel
            // 
            this.btnDesignMaterialLabel.Location = new System.Drawing.Point(356, 119);
            this.btnDesignMaterialLabel.Name = "btnDesignMaterialLabel";
            this.btnDesignMaterialLabel.Size = new System.Drawing.Size(87, 25);
            this.btnDesignMaterialLabel.TabIndex = 9;
            this.btnDesignMaterialLabel.Text = "修改标签样式";
            this.btnDesignMaterialLabel.Click += new System.EventHandler(this.btnDesignMaterialLabel_Click);
            // 
            // btnPrintMaterialLabel
            // 
            this.btnPrintMaterialLabel.Location = new System.Drawing.Point(266, 119);
            this.btnPrintMaterialLabel.Name = "btnPrintMaterialLabel";
            this.btnPrintMaterialLabel.Size = new System.Drawing.Size(87, 25);
            this.btnPrintMaterialLabel.TabIndex = 10;
            this.btnPrintMaterialLabel.Text = "打印";
            this.btnPrintMaterialLabel.Click += new System.EventHandler(this.btnPrintMBarcode_Click);
            // 
            // spSequenceQty
            // 
            this.spSequenceQty.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spSequenceQty.Location = new System.Drawing.Point(226, 63);
            this.spSequenceQty.Name = "spSequenceQty";
            this.spSequenceQty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spSequenceQty.Properties.IsFloatValue = false;
            this.spSequenceQty.Properties.Mask.EditMask = "N00";
            this.spSequenceQty.Properties.MaxValue = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.spSequenceQty.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spSequenceQty.Size = new System.Drawing.Size(217, 20);
            this.spSequenceQty.TabIndex = 8;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(136, 67);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 12);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "标签数量";
            // 
            // txtSequenceBarcode
            // 
            this.txtSequenceBarcode.EditValue = "";
            this.txtSequenceBarcode.Location = new System.Drawing.Point(226, 27);
            this.txtSequenceBarcode.Name = "txtSequenceBarcode";
            this.txtSequenceBarcode.Properties.ReadOnly = true;
            this.txtSequenceBarcode.Size = new System.Drawing.Size(217, 20);
            this.txtSequenceBarcode.TabIndex = 6;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(136, 30);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(72, 12);
            this.labelControl4.TabIndex = 5;
            this.labelControl4.Text = "起始条码内容";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.btnReprintSequence);
            this.xtraTabPage2.Controls.Add(this.spReprintBarcodeFrom);
            this.xtraTabPage2.Controls.Add(this.spReprintSequenceQty);
            this.xtraTabPage2.Controls.Add(this.labelControl5);
            this.xtraTabPage2.Controls.Add(this.labelControl9);
            this.xtraTabPage2.Controls.Add(this.labelControl6);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(589, 365);
            this.xtraTabPage2.Text = "补打流水号标签";
            // 
            // btnReprintSequence
            // 
            this.btnReprintSequence.Location = new System.Drawing.Point(337, 135);
            this.btnReprintSequence.Name = "btnReprintSequence";
            this.btnReprintSequence.Size = new System.Drawing.Size(87, 25);
            this.btnReprintSequence.TabIndex = 10;
            this.btnReprintSequence.Text = "打印";
            this.btnReprintSequence.Click += new System.EventHandler(this.OnReprintSequenceLabel);
            // 
            // spReprintBarcodeFrom
            // 
            this.spReprintBarcodeFrom.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spReprintBarcodeFrom.Location = new System.Drawing.Point(207, 26);
            this.spReprintBarcodeFrom.Name = "spReprintBarcodeFrom";
            this.spReprintBarcodeFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spReprintBarcodeFrom.Properties.IsFloatValue = false;
            this.spReprintBarcodeFrom.Properties.Mask.EditMask = "N00";
            this.spReprintBarcodeFrom.Properties.MaxValue = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.spReprintBarcodeFrom.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spReprintBarcodeFrom.Size = new System.Drawing.Size(217, 20);
            this.spReprintBarcodeFrom.TabIndex = 8;
            // 
            // spReprintSequenceQty
            // 
            this.spReprintSequenceQty.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spReprintSequenceQty.Location = new System.Drawing.Point(207, 89);
            this.spReprintSequenceQty.Name = "spReprintSequenceQty";
            this.spReprintSequenceQty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spReprintSequenceQty.Properties.IsFloatValue = false;
            this.spReprintSequenceQty.Properties.Mask.EditMask = "N00";
            this.spReprintSequenceQty.Properties.MaxValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.spReprintSequenceQty.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spReprintSequenceQty.Size = new System.Drawing.Size(217, 20);
            this.spReprintSequenceQty.TabIndex = 8;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(137, 92);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 12);
            this.labelControl5.TabIndex = 7;
            this.labelControl5.Text = "标签数量";
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl9.Location = new System.Drawing.Point(207, 50);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(306, 24);
            this.labelControl9.TabIndex = 5;
            this.labelControl9.Text = "* 只需要输入流水号部分，假设条码的内容是M2005，\r\n输入2005即可，前缀字符\'M\'不要输入，系统会自动补全。";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(137, 29);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(60, 12);
            this.labelControl6.TabIndex = 5;
            this.labelControl6.Text = "起始流水号";
            // 
            // xtraTabPage4
            // 
            this.xtraTabPage4.Controls.Add(this.simpleButton1);
            this.xtraTabPage4.Controls.Add(this.textEdit1);
            this.xtraTabPage4.Controls.Add(this.labelControl1);
            this.xtraTabPage4.Name = "xtraTabPage4";
            this.xtraTabPage4.Size = new System.Drawing.Size(589, 365);
            this.xtraTabPage4.Text = "自由补打";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(298, 103);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(87, 25);
            this.simpleButton1.TabIndex = 11;
            this.simpleButton1.Text = "打印";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // textEdit1
            // 
            this.textEdit1.EditValue = "";
            this.textEdit1.Location = new System.Drawing.Point(127, 63);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.MaxLength = 20;
            this.textEdit1.Size = new System.Drawing.Size(258, 20);
            this.textEdit1.TabIndex = 8;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(127, 44);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(240, 12);
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "输入条码内容，套用流水号标签模板进行打印";
            // 
            // FrmPrintLabel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(691, 415);
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "FrmPrintLabel";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "标签打印中心";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spSequenceQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSequenceBarcode.Properties)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            this.xtraTabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spReprintBarcodeFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spReprintSequenceQty.Properties)).EndInit();
            this.xtraTabPage4.ResumeLayout(false);
            this.xtraTabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage4;
        private DevExpress.XtraEditors.SimpleButton btnDesignMaterialLabel;
        private DevExpress.XtraEditors.SimpleButton btnPrintMaterialLabel;
        private DevExpress.XtraEditors.SpinEdit spSequenceQty;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtSequenceBarcode;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton btnReprintSequence;
        private DevExpress.XtraEditors.SpinEdit spReprintSequenceQty;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.SpinEdit spReprintBarcodeFrom;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}