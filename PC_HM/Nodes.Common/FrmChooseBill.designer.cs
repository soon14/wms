namespace Nodes.Common
{
    partial class FrmChooseBill
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
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition2 = new DevExpress.XtraGrid.StyleFormatCondition();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnEnter = new DevExpress.XtraEditors.SimpleButton();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gridHeader = new DevExpress.XtraGrid.GridControl();
            this.gvHeader = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn40 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn44 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn46 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn48 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn49 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn53 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHeader)).BeginInit();
            this.SuspendLayout();
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "延时标记";
            this.gridColumn4.DisplayFormat.FormatString = "f0";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "DelayMark";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Width = 60;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(576, 312);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 20);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "关闭";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnEnter
            // 
            this.btnEnter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnter.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnEnter.Appearance.Options.UseFont = true;
            this.btnEnter.Location = new System.Drawing.Point(484, 312);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(86, 20);
            this.btnEnter.TabIndex = 0;
            this.btnEnter.Text = "确定";
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // gridHeader
            // 
            this.gridHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridHeader.DataSource = this.bindingSource1;
            this.gridHeader.Location = new System.Drawing.Point(12, 12);
            this.gridHeader.MainView = this.gvHeader;
            this.gridHeader.Name = "gridHeader";
            this.gridHeader.Size = new System.Drawing.Size(650, 294);
            this.gridHeader.TabIndex = 5;
            this.gridHeader.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvHeader});
            // 
            // gvHeader
            // 
            this.gvHeader.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCheck,
            this.gridColumn40,
            this.gridColumn44,
            this.gridColumn46,
            this.gridColumn3,
            this.gridColumn48,
            this.gridColumn2,
            this.gridColumn49,
            this.gridColumn20,
            this.gridColumn53,
            this.gridColumn7,
            this.gridColumn6,
            this.gridColumn5,
            this.gridColumn1,
            this.gridColumn19,
            this.gridColumn12,
            this.gridColumn17,
            this.gridColumn4});
            styleFormatCondition2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            styleFormatCondition2.Appearance.Options.UseBackColor = true;
            styleFormatCondition2.ApplyToRow = true;
            styleFormatCondition2.Column = this.gridColumn4;
            styleFormatCondition2.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            styleFormatCondition2.Value1 = "1";
            this.gvHeader.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition2});
            this.gvHeader.GridControl = this.gridHeader;
            this.gvHeader.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "StatusName", null, "")});
            this.gvHeader.ID = "33679cea-d538-46c1-9954-e4e6bd4e3e85";
            this.gvHeader.Name = "gvHeader";
            this.gvHeader.OptionsBehavior.Editable = true;
            this.gvHeader.OptionsSelection.MultiSelect = true;
            this.gvHeader.OptionsView.ColumnAutoWidth = false;
            this.gvHeader.OptionsView.EnableAppearanceOddRow = true;
            this.gvHeader.OptionsView.ShowAutoFilterRow = true;
            this.gvHeader.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn40, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn1, DevExpress.Data.ColumnSortOrder.Descending)});
            this.gvHeader.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnViewMouseUp);
            // 
            // colCheck
            // 
            this.colCheck.Caption = "选中";
            this.colCheck.FieldName = "HasChecked";
            this.colCheck.Name = "colCheck";
            this.colCheck.OptionsColumn.AllowEdit = true;
            this.colCheck.OptionsColumn.AllowMove = false;
            this.colCheck.OptionsColumn.AllowShowHide = false;
            this.colCheck.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colCheck.OptionsColumn.FixedWidth = true;
            this.colCheck.Visible = true;
            this.colCheck.VisibleIndex = 0;
            this.colCheck.Width = 60;
            // 
            // gridColumn40
            // 
            this.gridColumn40.Caption = "单据号";
            this.gridColumn40.FieldName = "BillNO";
            this.gridColumn40.Name = "gridColumn40";
            this.gridColumn40.Visible = true;
            this.gridColumn40.VisibleIndex = 1;
            this.gridColumn40.Width = 190;
            // 
            // gridColumn44
            // 
            this.gridColumn44.Caption = "单据类型";
            this.gridColumn44.FieldName = "BillTypeName";
            this.gridColumn44.Name = "gridColumn44";
            this.gridColumn44.Visible = true;
            this.gridColumn44.VisibleIndex = 2;
            // 
            // gridColumn46
            // 
            this.gridColumn46.Caption = "单据状态";
            this.gridColumn46.FieldName = "StatusName";
            this.gridColumn46.Name = "gridColumn46";
            this.gridColumn46.Visible = true;
            this.gridColumn46.VisibleIndex = 16;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "来自仓库";
            this.gridColumn3.FieldName = "FromWarehouseName";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 4;
            // 
            // gridColumn48
            // 
            this.gridColumn48.Caption = "拣货方式";
            this.gridColumn48.FieldName = "OutstoreTypeName";
            this.gridColumn48.Name = "gridColumn48";
            this.gridColumn48.Visible = true;
            this.gridColumn48.VisibleIndex = 3;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "拣货区域";
            this.gridColumn2.FieldName = "PickZnTypeName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 6;
            // 
            // gridColumn49
            // 
            this.gridColumn49.Caption = "业务员";
            this.gridColumn49.FieldName = "SalesMan";
            this.gridColumn49.Name = "gridColumn49";
            this.gridColumn49.Visible = true;
            this.gridColumn49.VisibleIndex = 7;
            this.gridColumn49.Width = 70;
            // 
            // gridColumn20
            // 
            this.gridColumn20.Caption = "业务电话";
            this.gridColumn20.FieldName = "ContractNO";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 8;
            // 
            // gridColumn53
            // 
            this.gridColumn53.Caption = "客户";
            this.gridColumn53.FieldName = "CustomerName";
            this.gridColumn53.Name = "gridColumn53";
            this.gridColumn53.Visible = true;
            this.gridColumn53.VisibleIndex = 5;
            this.gridColumn53.Width = 140;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "收货人";
            this.gridColumn7.FieldName = "Consignee";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 9;
            this.gridColumn7.Width = 70;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "客户地址";
            this.gridColumn6.FieldName = "Address";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 10;
            this.gridColumn6.Width = 110;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "收货人电话";
            this.gridColumn5.FieldName = "ShTel";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 11;
            this.gridColumn5.Width = 94;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "下单日期";
            this.gridColumn1.DisplayFormat.FormatString = "g";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn1.FieldName = "CreateDate";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 12;
            this.gridColumn1.Width = 110;
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "审核日期";
            this.gridColumn19.DisplayFormat.FormatString = "g";
            this.gridColumn19.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn19.FieldName = "ConfirmDate";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 13;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "WMS备注";
            this.gridColumn12.FieldName = "WmsRemark";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 14;
            this.gridColumn12.Width = 120;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "打印标记";
            this.gridColumn17.FieldName = "HasPrinted";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 15;
            this.gridColumn17.Width = 60;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl1.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(12, 315);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(466, 17);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "注：黄色背景为二次发货订单";
            // 
            // FrmChooseBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 341);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.gridHeader);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.btnCancel);
            this.Name = "FrmChooseBill";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择订单";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHeader)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnEnter;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraGrid.GridControl gridHeader;
        private DevExpress.XtraGrid.Views.Grid.GridView gvHeader;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn40;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn44;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn46;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn48;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn49;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn53;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn colCheck;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}