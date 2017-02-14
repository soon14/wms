using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Nodes.Utils
{
    public class XmlBaseClass
    {
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 序列化到文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="obj"></param>
        public static void Serialize<T>(string fileName, T obj)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                ser.Serialize(stream, obj);
                stream.Close();
            }
        }

        /// <summary>
        /// 从config.xml文件中读取键值：configuration\[element]\[key]
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ReadConfigValue(string element, string key)
        {
            string configFile = Path.Combine(PathUtil.ApplicationStartupPath, "config.xml"); //获取config.xml文件的路径
            XmlDocument xml = new XmlDocument();
            xml.Load(configFile);
            return xml["configuration"][element].GetAttribute(key);
        }
        public static string ReadResourcesValue(string key)
        {
            string resourceFile = Path.Combine(PathUtil.ApplicationStartupPath, "resources.xml");
            if (!File.Exists(resourceFile))
                return string.Empty;
            XmlDocument doc = new XmlDocument();
            doc.Load(resourceFile);
            XmlNode xmlNode = doc.SelectSingleNode("resources/add[@key='" + key + "']");
            if (xmlNode == null)
                return string.Empty;
            return xmlNode.Attributes["value"].Value;
        }

        public static bool WriteConfigValue(string element, string attribute, string value)
        {
            bool result = false;
            try
            {
                XmlDocument xml = new XmlDocument();
                string configFile = Path.Combine(PathUtil.ApplicationStartupPath, "config.xml");
                xml.Load(configFile);
                xml["configuration"][element].SetAttribute(attribute, value);
                xml.Save(configFile);
                result = true;
            }
            catch
            { }
            return result;
        }
    }
}
