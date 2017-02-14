using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Nodes.Utils
{
    /// <summary>
    /// 针对String常用方法
    /// </summary>
    public class StringUtil
    {
        /// <summary>
        /// 默认符号（默认情况下，分隔字符串或连接字符串使用此符号）
        /// </summary>
        public const string DEF_SIGN = ",";
        /// <summary>
        /// 将集合中某个属性通过指定符号连接成一个字符串
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="collection">集合</param>
        /// <param name="property">需要连接的属性名称</param>
        /// <returns></returns>
        public static string JoinBySign<T>(IEnumerable<T> collection, string property)
        {
            return JoinBySign<T>(collection, property, DEF_SIGN);
        }
        public static string JoinBySign<T>(IEnumerable<T> collection, string property, string sign)
        {
            StringBuilder sb = new StringBuilder();
            foreach (T item in collection)
            {
                Type type = item.GetType();
                if (type == typeof(int) || type == typeof(string))
                {
                    sb.AppendFormat("{0}{1}", item, sign);
                }
                else
                {
                    PropertyInfo[] propertyInfoList = type.GetProperties();
                    if (propertyInfoList == null || propertyInfoList.Length == 0)
                        continue;
                    foreach (PropertyInfo propertyInfo in propertyInfoList)
                    {
                        if (propertyInfo.Name == property)
                        {
                            object obj = propertyInfo.GetValue(item, null);
                            sb.AppendFormat("{0}{1}", obj, sign);
                            break;
                        }
                    }
                }
            }
            string result = sb.ToString();
            if (result.Length > 0 && result.LastIndexOf(sign) == result.Length - 1)
                result = result.Substring(0, result.Length - 1);
            return result;
        }
    }
}
