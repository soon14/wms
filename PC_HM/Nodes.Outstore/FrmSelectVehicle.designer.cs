namespace Nodes.Outstore
{
    partial class FrmSelectVehicle
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
            this.lookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // lookUpEdit1
            // 
            this.lookUpEdit1.Location = new System.Drawing.Point(126, 35);
            this.lookUpEdit1.Name = "lookUpEdit1";
            this.lookUpEdit1.Properties.Appearance.Font = new System.Drawing.Font("宋体", 15F);
            this.lookUpEdit1.Properties.Appearance.Options.UseFont = true;
            this.lookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit1.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("VehicleNO", "车牌号"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("RouteName", "线路名称"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("UserName", "司机姓名")});
            this.lookUpEdit1.Properties.DataSource = this.bindingSource1;
            this.lookUpEdit1.Properties.DisplayMember = "JianCheng";
            this.lookUpEdit1.Properties.NullText = "";
            this.lookUpEdit1.Properties.ValueMember = "ID";
            this.lookUpEdit1.Size = new System.Drawing.Size(361, 29);
            this.lookUpEdit1.TabIndex = 10;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("宋体", 18F);
            this.labelControl3.Location = new System.Drawing.Point(14, 34);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(120, 24);
            this.labelControl3.TabIndex = 9;
            this.labelControl3.Text = "选择车辆：";
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("宋体", 12F);
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.Location = new System.Drawing.Point(179, 84);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(140, 34);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "确 定";
            this.btnOK.Click += new System.EventHandler(this.btnOKOnClick);
            // 
            // FrmSelectVehicle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 134);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lookUpEdit1);
            this.Controls.Add(this.labelControl3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSelectVehicle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择车辆信息";
            this.Load += new System.EventHandler(this.FrmSelectVehicle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LookUpEdit lookUpEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}