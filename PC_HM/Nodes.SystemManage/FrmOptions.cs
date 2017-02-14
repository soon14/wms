using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using DevExpress.Skins;
using Nodes.Utils;
using DevExpress.XtraBars.Ribbon;

namespace Nodes.SystemManage
{
    public partial class FrmOptions : DevExpress.XtraEditors.XtraForm
    {
        public FrmOptions()
        {
            InitializeComponent();
        }

        private void FrmOptions_Load(object sender, EventArgs e)
        {
            optionsControl.SelectedTabIndex = 0;
            InitSelectedTabControl(optionsControl.SelectedTab);
        }        

        private void OnOKClick(object sender, EventArgs e)
        {
            IOptions option = GetCurrentOption();
            if (option != null)
            {
                option.Apply();
                this.DialogResult = DialogResult.OK;
            }
        }        

        private void backstageViewControl1_SelectedTabChanged(object sender, DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs e)
        {
            InitSelectedTabControl(optionsControl.SelectedTab);
        }

        IOptions GetCurrentOption()
        {
            BackstageViewTabItem tab = optionsControl.SelectedTab;
            BackstageViewClientControl panel = tab.ContentControl;
            IOptions uc = null;
            if (panel.Controls.Count > 0)
                uc = (IOptions)panel.Controls[0];
            return uc;
        }

        void InitSelectedTabControl(BackstageViewTabItem tab)
        {
            if (tab == null || tab.Tag == null) return;
            BackstageViewClientControl panel = tab.ContentControl;
            if (panel.Controls.Count > 0) return;

            string tag = tab.Tag.ToString();
            switch (tag)
            {
                case "外观设置":
                    ucSkinSettings skinSettings = new ucSkinSettings();
                    skinSettings.Dock = DockStyle.Fill;
                    panel.Controls.Add(skinSettings);
                    skinSettings.InitUI();
                    break;
            }
        }

        private void OnApplyClick(object sender, EventArgs e)
        {
            IOptions option = GetCurrentOption();
            if (option != null)
            {
                option.Apply();
            }
        }
    }
}