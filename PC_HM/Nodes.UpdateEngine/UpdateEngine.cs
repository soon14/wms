using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace Nodes.UpdateEngine
{
    public class UpdateHelper
    {
        public const string manifestFile = "manifest.xml";
        public static string AppPath = Application.StartupPath;

        /// <summary>
        /// 读取本机的版本配置信息文件manifest.xml
        /// </summary>
        /// <param name="appId">暂时未用上</param>
        /// <returns></returns>
        public static VersionInfo LoadVersion(string appId)
        {
            return TryDeserial<VersionInfo>(Path.Combine(AppPath, manifestFile));
        }

        public static void UpdateVersion(string appId, string newVer)
        {
            VersionInfo ver = LoadVersion(appId);
            ver.VER = newVer;
            ver.UPDATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            Serialize<VersionInfo>(Path.Combine(AppPath, manifestFile), ver);
        }

        #region 下面两个函数复制在Nodes.WMS.Utils.XmlBaseClass，不用直接使用Nodes.WMS.Utils文件，防止需要更新Utils文件时被占用
        public static T TryDeserial<T>(string fileName)
        {
            object obj = null;

            if (File.Exists(fileName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                StreamReader stream = new StreamReader(fileName);
                obj = serializer.Deserialize(stream);
                stream.Close();
            }

            if (obj != null && obj is T)
            {
                return (T)obj;
            }

            return default(T);
        }

        public static void Serialize<T>(string fileName, T obj)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                ser.Serialize(stream, obj);
                stream.Close();
            }
        }
        #endregion
    }
}
