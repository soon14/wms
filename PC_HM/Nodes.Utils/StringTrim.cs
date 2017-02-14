using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Utils
{
    //去除所有空格
    public class StringTrim
    {
        public static string DeleteTrim(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            #region 去除空格
            string err = str;
            string[] arr2 = err.Split(new char[] { ' ' });
            err = "";
            foreach (string tm in arr2)
                err += tm;
            #endregion

            return err;
        }
    }
}
