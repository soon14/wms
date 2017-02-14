namespace Nodes.SystemManage
{
    partial class FrmNonLoadingBills
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
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnCreateTask = new DevExpress.XtraEditors.SimpleButton();
            this.gridHeader = new DevExpress.XtraGrid.GridControl();
            this.gvHeader = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn40 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn44 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cboVehicle = new DevExpress.XtraEditors.LookUpEdit();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.listPersonnel = new DevExpress.XtraEditors.CheckedListBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboVehicle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listPersonnel)).BeginInit();
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
            // gridColumn10
            // 
            this.gridColumn10.Caption = "排序类型";
            this.gridColumn10.FieldName = "Attri1";
            this.gridColumn10.Name = "gridColumn10";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(655, 371);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(94, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCreateTask
            // 
            this.btnCreateTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateTask.Location = new System.Drawing.Point(549, 371);
            this.btnCreateTask.Name = "btnCreateTask";
            this.btnCreateTask.Size = new System.Drawing.Size(94, 23);
            this.btnCreateTask.TabIndex = 1;
            this.btnCreateTask.Text = "生成装车任务";
            this.btnCreateTask.Click += new System.EventHandler(this.btnCreateTask_Click);
            // 
            // gridHeader
            // 
            this.gridHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridHeader.Location = new System.Drawing.Point(176, 12);
            this.gridHeader.MainView = this.gvHeader;
            this.gridHeader.Name = "gridHeader";
            this.gridHeader.Size = new System.Drawing.Size(573, 353);
            this.gridHeader.TabIndex = 6;
            this.gridHeader.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvHeader});
            // 
            // gvHeader
            // 
            this.gvHeader.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCheck,
            this.gridColumn40,
            this.gridColumn44,
            this.gridColumn9,
            this.gridColumn11,
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
            this.gridColumn4,
            this.gridColumn8,
            this.gridColumn10});
            styleFormatCondition1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            styleFormatCondition1.Appearance.Options.UseBackColor = true;
            styleFormatCondition1.ApplyToRow = true;
            styleFormatCondition1.Column = this.gridColumn4;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            styleFormatCondition1.Value1 = "1";
            styleFormatCondition2.Appearance.BackColor = System.Drawing.Color.Lime;
            styleFormatCondition2.Appearance.Options.UseBackColor = true;
            styleFormatCondition2.ApplyToRow = true;
            styleFormatCondition2.Column = this.gridColumn10;
            styleFormatCondition2.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            styleFormatCondition2.Value1 = "10";
            this.gvHeader.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1,
            styleFormatCondition2});
            this.gvHeader.GridControl = this.gridHeader;
            this.gvHeader.GroupCount = 1;
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
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn46, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn1, DevExpress.Data.ColumnSortOrder.Descending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn8, DevExpress.Data.ColumnSortOrder.Ascending)});
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
            this.gridColumn40.VisibleIndex = 2;
            this.gridColumn40.Width = 190;
            // 
            // gridColumn44
            // 
            this.gridColumn44.Caption = "单据类型";
            this.gridColumn44.FieldName = "BillTypeName";
            this.gridColumn44.Name = "gridColumn44";
            this.gridColumn44.Visible = true;
            this.gridColumn44.VisibleIndex = 6;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "整件件数";
            this.gridColumn9.DisplayFormat.FormatString = "f0";
            this.gridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn9.FieldName = "BoxNum";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 3;
            // 
            // gridColumn11
            // 
            this.gridColumn11.AppearanceCell.ForeColor = System.Drawing.Color.Red;
            this.gridColumn11.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn11.Caption = "散货件数(预估)";
            this.gridColumn11.DisplayFormat.FormatString = "f0";
            this.gridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn11.FieldName = "CaseBoxNum";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 4;
            this.gridColumn11.Width = 95;
            // 
            // gridColumn46
            // 
            this.gridColumn46.Caption = "单据状态";
            this.gridColumn46.FieldName = "StatusName";
            this.gridColumn46.Name = "gridColumn46";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "来自仓库";
            this.gridColumn3.FieldName = "FromWarehouseName";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 8;
            // 
            // gridColumn48
            // 
            this.gridColumn48.Caption = "拣货方式";
            this.gridColumn48.FieldName = "OutstoreTypeName";
            this.gridColumn48.Name = "gridColumn48";
            this.gridColumn48.Visible = true;
            this.gridColumn48.VisibleIndex = 7;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "拣货区域";
            this.gridColumn2.FieldName = "PickZnTypeName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 10;
            // 
            // gridColumn49
            // 
            this.gridColumn49.Caption = "业务员";
            this.gridColumn49.FieldName = "SalesMan";
            this.gridColumn49.Name = "gridColumn49";
            this.gridColumn49.Visible = true;
            this.gridColumn49.VisibleIndex = 11;
            this.gridColumn49.Width = 70;
            // 
            // gridColumn20
            // 
            this.gridColumn20.Caption = "业务电话";
            this.gridColumn20.FieldName = "ContractNO";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 12;
            // 
            // gridColumn53
            // 
            this.gridColumn53.Caption = "客户";
            this.gridColumn53.FieldName = "CustomerName";
            this.gridColumn53.Name = "gridColumn53";
            this.gridColumn53.Visible = true;
            this.gridColumn53.VisibleIndex = 9;
            this.gridColumn53.Width = 140;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "收货人";
            this.gridColumn7.FieldName = "Consignee";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 13;
            this.gridColumn7.Width = 70;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "客户地址";
            this.gridColumn6.FieldName = "Address";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 110;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "收货人电话";
            this.gridColumn5.FieldName = "ShTel";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 14;
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
            this.gridColumn1.VisibleIndex = 15;
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
            this.gridColumn19.VisibleIndex = 16;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "WMS备注";
            this.gridColumn12.FieldName = "WmsRemark";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 17;
            this.gridColumn12.Width = 120;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "打印标记";
            this.gridColumn17.FieldName = "HasPrinted";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 18;
            this.gridColumn17.Width = 60;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "车次编号";
            this.gridColumn8.FieldName = "VehicleNO";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 1;
            this.gridColumn8.Width = 140;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl1.Appearance.Font = new System.Drawing.Font("宋体", 12F);
            this.labelControl1.Location = new System.Drawing.Point(12, 375);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(32, 16);
            this.labelControl1.TabIndex = 13;
            this.labelControl1.Text = "车辆";
            // 
            // cboVehicle
            // 
            this.cboVehicle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboVehicle.Location = new System.Drawing.Point(50, 372);
            this.cboVehicle.Name = "cboVehicle";
            this.cboVehicle.Properties.Appearance.Font = new System.Drawing.Font("宋体", 12F);
            this.cboVehicle.Properties.Appearance.Options.UseFont = true;
            this.cboVehicle.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboVehicle.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("VehicleNO", "车牌号"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("RouteName", "线路名称"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("UserName", "司机姓名")});
            this.cboVehicle.Properties.DataSource = this.bindingSource1;
            this.cboVehicle.Properties.DisplayMember = "JianCheng";
            this.cboVehicle.Properties.NullText = "";
            this.cboVehicle.Properties.NullValuePrompt = "选择车辆";
            this.cboVehicle.Properties.NullValuePromptShowForEmptyValue = true;
            this.cboVehicle.Size = new System.Drawing.Size(242, 25);
            this.cboVehicle.TabIndex = 12;
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupControl1.Controls.Add(this.listPersonnel);
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(158, 353);
            this.groupControl1.TabIndex = 14;
            this.groupControl1.Text = "装车人员";
            // 
            // listPersonnel
            // 
            this.listPersonnel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listPersonnel.HotTrackSelectMode = DevExpress.XtraEditors.HotTrackSelectMode.SelectItemOnClick;
            this.listPersonnel.Location = new System.Drawing.Point(5, 25);
            this.listPersonnel.Name = "listPersonnel";
            this.listPersonnel.Size = new System.Drawing.Size(148, 323);
            this.listPersonnel.TabIndex = 0;
            // 
            // FrmNonLoadingBills
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(761, 404);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.cboVehicle);
            this.Controls.Add(this.gridHeader);
            this.Controls.Add(this.btnCreateTask);
            this.Controls.Add(this.btnClose);
            this.Name = "FrmNonLoadingBills";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "待装车订单";
            ((System.ComponentModel.ISupportInitialize)(this.gridHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboVehicle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listPersonnel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnCreateTask;
        private DevExpress.XtraGrid.GridControl gridHeader;
        private DevExpress.XtraGrid.Views.Grid.GridView gvHeader;
        private DevExpress.XtraGrid.Columns.GridColumn colCheck;
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
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit cboVehicle;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.CheckedListBoxControl listPersonnel;

    }
}