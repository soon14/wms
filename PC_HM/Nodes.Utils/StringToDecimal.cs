using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Utils
{
    public class StringToDecimal
    {
        /// <summary>
        /// string转换decimal 得到2位小数点 
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static decimal GetTwoDecimal(string num)
        {
            if (string.IsNullOrEmpty(num))
                return Math.Round(Convert.ToDecimal("0.000"), 2);
            string ret = num;
            if (ret.Contains("."))
                ret = ret.Insert(ret.Length, "00");
            else
                ret = ret.Insert(ret.Length, ".00");
            return Math.Round(Convert.ToDecimal(ret), 2);
        }
    }
}
