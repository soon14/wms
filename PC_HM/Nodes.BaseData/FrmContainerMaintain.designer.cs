namespace Nodes.BaseData
{
    partial class FrmContainerMaintain
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
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEditCom = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnOpenCom = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.btnTest = new DevExpress.XtraEditors.SimpleButton();
            this.lblContainerType = new DevExpress.XtraEditors.LabelControl();
            this.lblCurrentWeight = new DevExpress.XtraEditors.LabelControl();
            this.lblActualWeight = new DevExpress.XtraEditors.LabelControl();
            this.btnSubmit = new DevExpress.XtraEditors.SimpleButton();
            this.txtContainerCode = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditCom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtContainerCode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("宋体", 15F);
            this.labelControl4.Location = new System.Drawing.Point(31, 30);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(80, 20);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "串口号：";
            // 
            // comboBoxEditCom
            // 
            this.comboBoxEditCom.Location = new System.Drawing.Point(108, 26);
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
            this.comboBoxEditCom.Size = new System.Drawing.Size(213, 29);
            this.comboBoxEditCom.TabIndex = 4;
            // 
            // btnOpenCom
            // 
            this.btnOpenCom.Appearance.Font = new System.Drawing.Font("宋体", 15F);
            this.btnOpenCom.Appearance.Options.UseFont = true;
            this.btnOpenCom.Location = new System.Drawing.Point(338, 25);
            this.btnOpenCom.Name = "btnOpenCom";
            this.btnOpenCom.Size = new System.Drawing.Size(98, 30);
            this.btnOpenCom.TabIndex = 5;
            this.btnOpenCom.Text = "打开串口";
            this.btnOpenCom.Click += new System.EventHandler(this.btnOpenCom_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.txtContainerCode);
            this.groupControl1.Controls.Add(this.btnTest);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.lblActualWeight);
            this.groupControl1.Controls.Add(this.lblCurrentWeight);
            this.groupControl1.Controls.Add(this.lblContainerType);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Location = new System.Drawing.Point(31, 73);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(532, 286);
            this.groupControl1.TabIndex = 6;
            this.groupControl1.Text = "容器信息";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl1.Location = new System.Drawing.Point(38, 61);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(68, 21);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "容器编号:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl2.Location = new System.Drawing.Point(38, 165);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(68, 21);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "当前重量:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl3.Location = new System.Drawing.Point(38, 217);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(68, 21);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "实称重量:";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl5.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl5.Location = new System.Drawing.Point(38, 113);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(68, 21);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "容器类型:";
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(452, 25);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 7;
            this.btnTest.Text = "测试";
            this.btnTest.Visible = false;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // lblContainerType
            // 
            this.lblContainerType.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblContainerType.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblContainerType.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblContainerType.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblContainerType.Location = new System.Drawing.Point(112, 113);
            this.lblContainerType.Name = "lblContainerType";
            this.lblContainerType.Size = new System.Drawing.Size(247, 21);
            this.lblContainerType.TabIndex = 0;
            this.lblContainerType.Text = "容器类型";
            // 
            // lblCurrentWeight
            // 
            this.lblCurrentWeight.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCurrentWeight.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblCurrentWeight.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblCurrentWeight.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCurrentWeight.Location = new System.Drawing.Point(112, 165);
            this.lblCurrentWeight.Name = "lblCurrentWeight";
            this.lblCurrentWeight.Size = new System.Drawing.Size(247, 21);
            this.lblCurrentWeight.TabIndex = 0;
            this.lblCurrentWeight.Text = "当前重量";
            // 
            // lblActualWeight
            // 
            this.lblActualWeight.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblActualWeight.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblActualWeight.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblActualWeight.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblActualWeight.Location = new System.Drawing.Point(112, 217);
            this.lblActualWeight.Name = "lblActualWeight";
            this.lblActualWeight.Size = new System.Drawing.Size(247, 21);
            this.lblActualWeight.TabIndex = 0;
            this.lblActualWeight.Text = "实称重量";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Appearance.Font = new System.Drawing.Font("宋体", 15F);
            this.btnSubmit.Appearance.Options.UseFont = true;
            this.btnSubmit.Location = new System.Drawing.Point(460, 25);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(98, 30);
            this.btnSubmit.TabIndex = 5;
            this.btnSubmit.Text = "提交重量";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // txtContainerCode
            // 
            this.txtContainerCode.Location = new System.Drawing.Point(112, 57);
            this.txtContainerCode.Name = "txtContainerCode";
            this.txtContainerCode.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txtContainerCode.Properties.Appearance.Options.UseFont = true;
            this.txtContainerCode.Size = new System.Drawing.Size(247, 28);
            this.txtContainerCode.TabIndex = 8;
            this.txtContainerCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContainerCode_KeyDown);
            // 
            // FrmContainerMaintain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(593, 371);
            this.Controls.Add(this.comboBoxEditCom);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnOpenCom);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmContainerMaintain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "容器重量维护";
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditCom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtContainerCode.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditCom;
        private DevExpress.XtraEditors.SimpleButton btnOpenCom;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.IO.Ports.SerialPort serialPort1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnTest;
        private DevExpress.XtraEditors.LabelControl lblActualWeight;
        private DevExpress.XtraEditors.LabelControl lblCurrentWeight;
        private DevExpress.XtraEditors.LabelControl lblContainerType;
        private DevExpress.XtraEditors.SimpleButton btnSubmit;
        private DevExpress.XtraEditors.TextEdit txtContainerCode;
    }
}