namespace Nodes.Instore
{
    partial class FrmPoFirstApprove
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
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.popupControlContainer1 = new DevExpress.XtraBars.PopupControlContainer(this.components);
            this.ucQueryCondition = new Nodes.Instore.UcPoQueryEngine();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem8 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.ucPoBody1 = new Nodes.Instore.UcPoBody();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupControlContainer1)).BeginInit();
            this.popupControlContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.AllowCustomization = false;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem1,
            this.barButtonItem2,
            this.barButtonItem3,
            this.barButtonItem4,
            this.barButtonItem5,
            this.barButtonItem6,
            this.barButtonItem7,
            this.barButtonItem8});
            this.barManager1.MaxItemId = 8;
            // 
            // bar1
            // 
            this.bar1.BarName = "工具";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem6),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem5),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem3, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem4),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem8, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem7, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "工具";
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Caption = "刷新";
            this.barButtonItem6.Id = 5;
            this.barButtonItem6.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5);
            this.barButtonItem6.Name = "barButtonItem6";
            this.barButtonItem6.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem6.ShortcutKeyDisplayString = "可以使用快捷键 F5";
            this.barButtonItem6.Tag = "刷新";
            this.barButtonItem6.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.ActAsDropDown = true;
            this.barButtonItem5.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.barButtonItem5.Caption = "查找";
            this.barButtonItem5.DropDownControl = this.popupControlContainer1;
            this.barButtonItem5.Id = 4;
            this.barButtonItem5.Name = "barButtonItem5";
            this.barButtonItem5.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem5.Tag = "";
            // 
            // popupControlContainer1
            // 
            this.popupControlContainer1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.popupControlContainer1.CloseOnLostFocus = false;
            this.popupControlContainer1.CloseOnOuterMouseClick = false;
            this.popupControlContainer1.Controls.Add(this.ucQueryCondition);
            this.popupControlContainer1.Location = new System.Drawing.Point(348, 12);
            this.popupControlContainer1.Manager = this.barManager1;
            this.popupControlContainer1.Name = "popupControlContainer1";
            this.popupControlContainer1.Size = new System.Drawing.Size(272, 273);
            this.popupControlContainer1.TabIndex = 1;
            this.popupControlContainer1.Visible = false;
            this.popupControlContainer1.Popup += new System.EventHandler(this.popupControlContainer1_Popup);
            // 
            // ucQueryCondition
            // 
            this.ucQueryCondition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucQueryCondition.Location = new System.Drawing.Point(0, 0);
            this.ucQueryCondition.Name = "ucQueryCondition";
            this.ucQueryCondition.Size = new System.Drawing.Size(272, 273);
            this.ucQueryCondition.TabIndex = 0;
            this.ucQueryCondition.QueryCompleted += new Nodes.Instore.UcPoQueryEngine.QueryComplete(this.OnQueryCompleted);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "所有待审批";
            this.barButtonItem3.Hint = "列出”已经提交，尚未一审“的采购单。";
            this.barButtonItem3.Id = 2;
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem3.Tag = "所有待审批";
            this.barButtonItem3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "所有已审批";
            this.barButtonItem4.Hint = "列出”已经一审，尚未二审或收货“的采购单。";
            this.barButtonItem4.Id = 3;
            this.barButtonItem4.Name = "barButtonItem4";
            this.barButtonItem4.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem4.Tag = "所有已审批";
            this.barButtonItem4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // barButtonItem8
            // 
            this.barButtonItem8.Caption = "单据日志";
            this.barButtonItem8.Id = 7;
            this.barButtonItem8.Name = "barButtonItem8";
            this.barButtonItem8.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem8.Tag = "单据日志";
            this.barButtonItem8.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // barButtonItem7
            // 
            this.barButtonItem7.Caption = "编写备注";
            this.barButtonItem7.Id = 6;
            this.barButtonItem7.Name = "barButtonItem7";
            this.barButtonItem7.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem7.Tag = "编写备注";
            this.barButtonItem7.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "审批";
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem1.Tag = "审批";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "反审";
            this.barButtonItem2.Id = 1;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            toolTipTitleItem1.Text = "提示";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "只能反审自己审核通过的单据。";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.barButtonItem2.SuperTip = superToolTip1;
            this.barButtonItem2.Tag = "反审";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1037, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 449);
            this.barDockControlBottom.Size = new System.Drawing.Size(1037, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 418);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1037, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 418);
            // 
            // ucPoBody1
            // 
            this.ucPoBody1.DataSource = null;
            this.ucPoBody1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPoBody1.Location = new System.Drawing.Point(0, 31);
            this.ucPoBody1.Name = "ucPoBody1";
            this.ucPoBody1.Size = new System.Drawing.Size(1037, 418);
            this.ucPoBody1.TabIndex = 9;
            // 
            // FrmPoFirstApprove
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1037, 449);
            this.Controls.Add(this.popupControlContainer1);
            this.Controls.Add(this.ucPoBody1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmPoFirstApprove";
            this.Text = "采购单一审";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupControlContainer1)).EndInit();
            this.popupControlContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.XtraBars.PopupControlContainer popupControlContainer1;
        private UcPoQueryEngine ucQueryCondition;
        private DevExpress.XtraBars.BarButtonItem barButtonItem7;
        private DevExpress.XtraBars.BarButtonItem barButtonItem8;
        private UcPoBody ucPoBody1;
    }
}