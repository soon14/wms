using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Utils
{
    public class DeleteSpecialStr
    {
        /// <summary>
        /// 转移特殊字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DeleteStr(string str)
        {
            string dataStr = string.Empty;
            if (str.Contains("%"))
                dataStr = str.Replace("%", "%25");
            //if (str.Contains("&"))
            //    dataStr = str.Replace("&","%26");
            //if (str.Contains("@"))
                //dataStr = str.Replace("@", "%40");
            //if (str.Contains("#"))
            //    dataStr = str.Replace("#", "%23");

            if (string.IsNullOrEmpty(dataStr))
                dataStr = str;
            return dataStr;
        }
    }
}
