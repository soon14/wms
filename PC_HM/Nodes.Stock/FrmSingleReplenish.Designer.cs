namespace Nodes.Stock
{
    partial class FrmSingleReplenish
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtQty = new DevExpress.XtraEditors.TextEdit();
            this.btnCreate = new DevExpress.XtraEditors.SimpleButton();
            this.lblUnit = new DevExpress.XtraEditors.LabelControl();
            this.searchLookUpEdit1 = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.txtQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl1.Location = new System.Drawing.Point(81, 67);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(42, 14);
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "商品：";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl2.Location = new System.Drawing.Point(81, 129);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(42, 14);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "数量：";
            // 
            // txtQty
            // 
            this.txtQty.EditValue = "0";
            this.txtQty.Location = new System.Drawing.Point(129, 125);
            this.txtQty.Name = "txtQty";
            this.txtQty.Properties.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtQty.Properties.Appearance.Options.UseFont = true;
            this.txtQty.Size = new System.Drawing.Size(245, 22);
            this.txtQty.TabIndex = 8;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(380, 209);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(96, 35);
            this.btnCreate.TabIndex = 9;
            this.btnCreate.Text = "生成补货任务";
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // lblUnit
            // 
            this.lblUnit.Appearance.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUnit.Location = new System.Drawing.Point(380, 129);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(0, 14);
            this.lblUnit.TabIndex = 10;
            // 
            // searchLookUpEdit1
            // 
            this.searchLookUpEdit1.EditValue = "[编辑";
            this.searchLookUpEdit1.Location = new System.Drawing.Point(129, 64);
            this.searchLookUpEdit1.Name = "searchLookUpEdit1";
            this.searchLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.searchLookUpEdit1.Properties.DisplayMember = "商品名称";
            this.searchLookUpEdit1.Properties.NullText = "请选择商品";
            this.searchLookUpEdit1.Properties.View = this.searchLookUpEdit1View;
            this.searchLookUpEdit1.Size = new System.Drawing.Size(347, 20);
            this.searchLookUpEdit1.TabIndex = 11;
            this.searchLookUpEdit1.EditValueChanged += new System.EventHandler(this.cboSkuList_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.ID = "bf09de25-652e-45d3-a0f9-49d1a02894b6";
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = true;
            // 
            // FrmSingleReplenish
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 283);
            this.Controls.Add(this.searchLookUpEdit1);
            this.Controls.Add(this.lblUnit);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSingleReplenish";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "单品补货";
            ((System.ComponentModel.ISupportInitialize)(this.txtQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtQty;
        private DevExpress.XtraEditors.SimpleButton btnCreate;
        private DevExpress.XtraEditors.LabelControl lblUnit;
        private DevExpress.XtraEditors.SearchLookUpEdit searchLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
    }
}