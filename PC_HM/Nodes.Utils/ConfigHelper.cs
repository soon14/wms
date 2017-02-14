/*
 * 创建人：单龙
 * 创建时间：2014-03-06
 * 描述：操作配置文件帮助类
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

// 公共工具程序命名空间
namespace Nodes.WMS.Utils
{
    public static class ConfigHelper
    {
        private static string AppPath = AppDomain.CurrentDomain.BaseDirectory; //获取当前文件夹的路径

        private static string ConfigFile = AppPath + @"\config.xml"; //获取config.xml文件的路径

        /// <summary>
        /// 暂存区特殊货位编号
        /// </summary>
        /// <returns></returns>
        public static string GetCrossInstoreLocationCode()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(ConfigFile);

            return xml["configuration"]["CrossInstoreLocationCode"].GetAttribute("Code").ToString();
        }
    }
}
