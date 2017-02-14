using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using DevExpress.Skins;
using Nodes.Utils;
using Nodes.UI;

namespace Nodes.SystemManage
{
    public partial class ucSkinSettings : DevExpress.XtraEditors.XtraUserControl, IOptions
    {
        public ucSkinSettings()
        {
            InitializeComponent();
        }
        public void InitUI()
        {
            try
            {
                LoadFonts();
                //LoadSkins();

                int fontSize = Properties.Settings.Default.FontSize;
                listFontFamilies.Text = string.IsNullOrEmpty(Properties.Settings.Default.FontFamily) ? SkinUtil.GetDefaultFont() : Properties.Settings.Default.FontFamily;
                listSkins.Text = Properties.Settings.Default.Skin;

                comboBoxEdit1.SelectedIndex = fontSize - 9;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        void LoadFonts()
        {
            //装载系统字体
            InstalledFontCollection _installedFonts = new InstalledFontCollection();
            FontFamily[] fonts = _installedFonts.Families;

            foreach (FontFamily f in fonts)
            {
                if (f.IsStyleAvailable(FontStyle.Regular) && PinYin.IsHanzi(f.Name))
                {
                    listFontFamilies.Properties.Items.Add(f.Name);
                }
            }
        }

        //void LoadSkins()
        //{
        //    foreach (SkinContainer skin in SkinManager.Default.Skins)
        //    {
        //        listSkins.Properties.Items.Add(skin.SkinName);
        //    }
        //}

        public void Apply()
        {
            //先查看是否更改了字体，如果更改了要应用新的字体
            int fontSize = comboBoxEdit1.SelectedIndex + 9;
            SkinUtil.SetFont(listFontFamilies.Text, fontSize);

            //再查看是否更改了皮肤，如果更改了要应用新的皮肤
            SkinUtil.SetSkin(listSkins.Text);

            //将更改保存到配置文件中
            Properties.Settings.Default.FontFamily = listFontFamilies.Text;
            Properties.Settings.Default.FontSize = fontSize;
            Properties.Settings.Default.Skin = listSkins.Text;

            Properties.Settings.Default.Save();
        }
    }
}
