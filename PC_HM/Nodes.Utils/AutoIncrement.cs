using System;
using System.Text;

namespace Nodes.Utils
{
    /// <summary>
    /// 实现数字或字母递增
    /// </summary>
    public class AutoIncrement
    {
        /// <summary>
        /// 把有数字或字母组成的字符串递增。
        /// 例如01递增后为02，A01递增为A02,00AA递增为00AB，A99递增为B00，Z0000递增为A0000
        /// </summary>
        /// <param name="currCode"></param>
        /// <returns></returns>
        public static string NextCode(string currCode)
        {
            byte[] ASCIIValues = ASCIIEncoding.ASCII.GetBytes(currCode);
            int StringLength = ASCIIValues.Length;
            bool isAllZed = true;
            bool isAllNine = true;

            //Check if all has ZZZ.... then do nothing just return empty string.
            for (int i = 0; i < StringLength - 1; i++)
            {
                if (ASCIIValues[i] != 90)
                {
                    isAllZed = false;
                    break;
                }
            }

            if (isAllZed && ASCIIValues[StringLength - 1] == 57)
            {
                ASCIIValues[StringLength - 1] = 64;
            }

            // Check if all has 999... then make it A0
            for (int i = 0; i < StringLength; i++)
            {
                if (ASCIIValues[i] != 57)
                {
                    isAllNine = false;
                    break;
                }
            }

            if (isAllNine)
            {
                ASCIIValues[StringLength - 1] = 47;
                ASCIIValues[0] = 65;
                for (int i = 1; i < StringLength - 1; i++)
                {
                    ASCIIValues[i] = 48;
                }
            }

            for (int i = StringLength; i > 0; i--)
            {
                if (i - StringLength == 0)
                {
                    ASCIIValues[i - 1] += 1;
                }

                if (ASCIIValues[i - 1] == 58)
                {
                    ASCIIValues[i - 1] = 48;
                    if (i - 2 == -1)
                    {
                        break;
                    }
                    ASCIIValues[i - 2] += 1;
                }
                else if (ASCIIValues[i - 1] == 91)
                {
                    ASCIIValues[i - 1] = 65;
                    if (i - 2 == -1)
                    {
                        break;
                    }

                    ASCIIValues[i - 2] += 1;
                }
                else
                {
                    break;
                }
            }

            return ASCIIEncoding.ASCII.GetString(ASCIIValues);
        }

        //另外一个算法，备用
        public static string AutoNoResult(string strNumber)
        {
            int intcounter = 1;
            int inttrynumber = 0;
            string strtext = "";
            string strresult = "";
            while (strNumber.Length + 1 > intcounter)
            {
                if (int.TryParse(strNumber.Substring(strNumber.Length - intcounter, intcounter), out inttrynumber) == true)
                {
                    strtext = strNumber.Substring(strNumber.Length - intcounter, intcounter);
                }
                else
                {
                    strtext = Convert.ToString(Convert.ToInt64(strtext) + 1);
                    return strresult = strNumber.Substring(0, strNumber.Length - strtext.Length) + strtext;
                }
                intcounter++;
            }
            strtext = Convert.ToString(Convert.ToDouble(strtext) + 1);
            return strresult = strNumber.Substring(0, strNumber.Length - strtext.Length) + strtext;
        }
    }
}
