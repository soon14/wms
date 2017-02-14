using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using DevExpress.Utils;

namespace Nodes.Resources
{
    public class AppResource
    {
        #region 常量
        /// <summary>
        /// 程序集的名称
        /// </summary>
        private static string CurrentAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
        #endregion

        #region 变量
        /// <summary>
        /// 当前程序集
        /// </summary>
        private static Assembly CurrentAssembly = Assembly.GetExecutingAssembly();

        #endregion

        #region 方法
        /// <summary>
        /// 在嵌入的资源文件中查找相应的图片
        /// </summary>
        /// <param name="name">资源图片的文件名称+扩展名</param>
        /// <returns></returns>
        public static Image GetIcon(string name)
        {
            Image image = null;
            try
            {
                const string ICON_PATH = ".Icons";
                if (!string.IsNullOrEmpty(name))
                {
                    StringBuilder sb = new StringBuilder();
                    if (name[0] != '.')
                        sb.AppendFormat("{0}{1}.{2}.png", AppResource.CurrentAssemblyName, ICON_PATH, name);
                    else
                        sb.AppendFormat("{0}{1}{2}.png", AppResource.CurrentAssemblyName, ICON_PATH, name);
                    using (Stream stream = CurrentAssembly.GetManifestResourceStream(sb.ToString()))
                    {
                        if (stream == null)
                            throw new Exception("加载资源文件失败，失败原因：可能丢失" + CurrentAssemblyName + ".dll文件。");
                        else
                            image = Image.FromStream(stream);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("AssemblyHelper.GetImage(string)->" + ex.Message);
                throw ex;
            }
            return image;
        }
        public static Image GetIcon(EIcons icon)
        {
            return GetIcon(icon.ToString());
        }
        public static Stream GetStream(string fullPath)
        {
            Stream stream = null;
            try
            {
                if (!string.IsNullOrEmpty(fullPath))
                {
                    string path = string.Format("{0}.{1}",
                        AppResource.CurrentAssemblyName, fullPath);
                    stream = CurrentAssembly.GetManifestResourceStream(path);
                    if (stream == null)
                        throw new Exception("加载资源文件失败，失败原因：可能丢失" + CurrentAssemblyName + ".dll文件。");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("AssemblyHelper.GetImage(string)->" + ex.Message);
                throw ex;
            }
            return stream;
        }


        private static ImageCollection m_images = null;
        public static ImageCollection LoadToolImages()
        {
            if (m_images == null)
                m_images = GetIcons();

            return m_images;
        }
        private static ImageCollection GetIcons()
        {
            ImageCollection imglist = new ImageCollection();
            string[] icons = Enum.GetNames(typeof(EIcons));
            if (icons != null && icons.Length > 0)
            {
                foreach (string item in icons)
                {
                    imglist.Images.Add(GetIcon(item));
                }
            }
            return imglist;
        }
        #endregion

        #region Enums
        public enum EIcons
        {
            add = 0,
            edit,
            open,
            delete,
            refresh,
            search,
            _lock,
            dropdown,
            print,
            tree,
            export,
            week,
            report,
            basedata,
            copy,
            save,
            today,
            log,
            ok,
            back,
            barcode16,
            excel,
            download,
            design,
            filter,
            message,
            remove,
            approved,
            information,
            eye,
            settings,
        };
        #endregion
    }
}
