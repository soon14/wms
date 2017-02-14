using System.Drawing;
using System.IO;
using System.Reflection;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.Utils;

namespace Nodes.UI
{
    public class SkinUtil
    {
        #region “皮肤和字体设置”

        /// <summary>
        /// 设置主题皮肤
        /// </summary>
        /// <param name="skinName"></param>
        public static void SetSkin(string skinName)
        {
            UserLookAndFeel.Default.SetSkinStyle(skinName);
        }

        public static string GetDefaultFont()
        {
            string fontName = null;
            if (System.Environment.OSVersion.Version.Major >= 6)
                fontName = "微软雅黑";
            else
                fontName = "宋体";

            return fontName;
        }

        public static void SetFont(string fontName, int fontSize)
        {
            if (string.IsNullOrEmpty(fontName))
                fontName = GetDefaultFont();

            Font _font = new Font(fontName, fontSize);
            AppearanceObject.DefaultFont = _font;
        }

        public static void SetFont(string fontName)
        {
            SetFont(fontName, 9);
        }

        public static void SetFont(int fontSize)
        {
            SetFont(null, fontSize);
        }

        public static bool RegisterSkin(string skinFileName)
        {
            bool _success = false;
            if (File.Exists(skinFileName))
            {
                Assembly assembly = Assembly.LoadFile(skinFileName);
                _success = SkinManager.Default.RegisterAssembly(assembly);
            }

            return _success;
        }

        #endregion
    }
}
