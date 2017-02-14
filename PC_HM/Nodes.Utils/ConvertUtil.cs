using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

namespace Nodes.Utils
{
    public class ConvertUtil
    {
        /// <summary>
        /// 去除空格并将空字符串转换为null
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StringToNull(string str)
        {
            if (str == null)
                return null;
            else
                return str.Trim().Length == 0 ? null : str.Trim();
        }

        public static string ObjectToNull(object obj)
        {
            if (obj == DBNull.Value || obj == null)
                return null;
            else
                return StringToNull(obj.ToString());
        }

        public static string ToString(object obj)
        {
            if (obj == DBNull.Value || obj == null)
                return String.Empty;
            else
                return obj.ToString();
        }

        public static int ToInt(string s)
        {
            return int.Parse(s);
        }

        public static int ToInt(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return GlobalConst.INT_NULL;
            else
                return Convert.ToInt32(obj);
        }

        public static Int64 ToInt64(string s)
        {
            return Int64.Parse(s);
        }

        public static Int64 ToInt64(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return 0;
            else
                return Convert.ToInt64(obj);
        }

        public static bool ToBool(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return false;
            else
                return Convert.ToBoolean(obj);
        }

        public static decimal ToDecimal(string s)
        {
            if (string.IsNullOrEmpty(s))
                return decimal.Zero;

            return Decimal.Parse(s);
        }

        public static decimal ToDecimal(object obj)
        {
            if (obj == null || obj == DBNull.Value || obj.ToString().Equals(string.Empty))
                return decimal.Zero;
            else
                return Decimal.Parse(obj.ToString());
        }

        public static DateTime ToDatetime(object obj)
        {
            if (obj == null || obj == DBNull.Value || obj.ToString().Equals(string.Empty))
                return DateTime.Now;
            else
                return DateTime.Parse(obj.ToString());
        }

        public static Image ConvertBytesToImage(byte[] bytes)
        {
            Image img = null;
            if (bytes != null)
            {
                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    img = Image.FromStream(stream);
                    stream.Close();
                }
            }

            return img;
        }

        public static byte[] ConvertImageToBytes(Image img)
        {
            byte[] bs = null;
            if (img != null)
            {
                using (Bitmap bmp = new Bitmap(img))
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        bs = stream.ToArray();
                        stream.Close();
                    }

                    bmp.Dispose();
                }
            }

            return bs;
        }

        /// <summary>
        /// 判断是否整数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsInt(string str)
        {
            int i = 0;
            return int.TryParse(str, out i);
        }

        public static bool IsInt(object str)
        {
            if (str == null)
                return false;

            int i = 0;
            return int.TryParse(str.ToString(), out i);
        }

        /// <summary>
        /// 判断是否正整数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsPositiveInt(string str)
        {
            uint result = 0;
            return uint.TryParse(str, out result);
        }

        public static bool IsNumeric(string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            int i = 0;
            for (i = 0; i < str.Length; i++)
            {
                if (!char.IsDigit(str, i))
                    break;
            }

            return (i == str.Length);
        }

        public static bool IsDecimal(string str)
        {
            decimal d = decimal.Zero;
            return decimal.TryParse(str, out d);
        }

        /// <summary>
        /// 验证的字符串格式是否为yy-MM-dd或yyyy-MM-dd
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDateTime(string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            if (str.Length < 6)
                return false;

            return Regex.IsMatch(str, @"^(\d{2}|\d{4})-((0([1-9]{1}))|(1[0|1|2]))-(([0-2]([1-9]{1}))|([1|2|3][0|1]))$");
        }

        #region 将数字转换中文大写人民币字符串，来自网络
        /// <summary>
        /// 将数字转换中文大写人民币字符串
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string ToRMB(Double e)
        {
            return ToRMB(System.Convert.ToDecimal(e));
        }

        /// <summary>
        /// 将数字转换中文大写人民币字符串
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string ToRMB(Decimal e)
        {
            string eString;//数字的格式化字符串
            string eNum;//单数字
            int eLen;//格式化字符串长度

            System.Text.StringBuilder rmb = new System.Text.StringBuilder();//人民币大写
            string yuan;//圆
            bool seriesZero;//连续0标志
            bool minus = false;//负数标志

            if (e == 0m)
            {
                return "零圆整";
            }

            if (e < 0m)
            {
                minus = true;
                e = System.Math.Abs(e);
            }

            if (e > 999999999999.99m)
            {
                throw new Exception("超过最大范围");
            }

            eString = e.ToString("0.00");
            eLen = eString.Length;
            yuan = (eString.Substring(0, 1) == "0" ? "" : "圆");

            eNum = eString.Substring(eLen - 1, 1);//分位
            if (eNum == "0")
            {
                rmb.Append("整");
                seriesZero = true;
            }
            else
            {
                rmb.Append(stringNum(eNum) + "分");
                seriesZero = false;
            }

            eNum = eString.Substring(eLen - 2, 1);//角位
            if (eNum == "0")
            {
                if (!seriesZero)
                {
                    if (!(eLen == 4 && eString.Substring(0, 1) == "0"))
                    {
                        rmb.Insert(0, "零");
                    }
                }
            }
            else
            {
                rmb.Insert(0, stringNum(eNum) + "角");
                seriesZero = false;
            }

            if (eLen <= 7)
            {
                rmb.Insert(0, stringNum4(eString.Substring(0, eLen - 3)) + yuan);
            }
            else if (eLen <= 11)
            {
                rmb.Insert(0, stringNum4(eString.Substring(eLen - 7, 4)) + yuan);
                rmb.Insert(0, stringNum4(eString.Substring(0, eLen - 7)) + "万");
            }
            else if (eLen <= 15)
            {
                rmb.Insert(0, stringNum4(eString.Substring(eLen - 7, 4)) + yuan);
                rmb.Insert(0, stringNum4(eString.Substring(eLen - 11, 4)) + (eString.Substring(eLen - 11, 4) == "0000" ? "" : "万"));
                rmb.Insert(0, stringNum4(eString.Substring(0, eLen - 11)) + "亿");
            }

            if (minus) rmb.Insert(0, "负");

            return rmb.ToString();
        }

        private static string stringNum4(string eNum4)
        {
            string eNum;
            bool seriesZero = false;
            System.Text.StringBuilder rmb4 = new System.Text.StringBuilder();
            int eLen = eNum4.Length;

            eNum = eNum4.Substring(eLen - 1, 1);//个位
            if (eNum == "0")
            {
                seriesZero = true;
            }
            else
            {
                rmb4.Append(stringNum(eNum));
            }

            if (eLen >= 2)//十位
            {
                eNum = eNum4.Substring(eLen - 2, 1);
                if (eNum == "0")
                {
                    if (!seriesZero)
                    {
                        rmb4.Insert(0, "零");
                        seriesZero = true;
                    }
                }
                else
                {
                    rmb4.Insert(0, stringNum(eNum) + "拾");
                    seriesZero = false;
                }
            }

            if (eLen >= 3)//百位
            {
                eNum = eNum4.Substring(eLen - 3, 1);
                if (eNum == "0")
                {
                    if (!seriesZero)
                    {
                        rmb4.Insert(0, "零");
                        seriesZero = true;
                    }
                }
                else
                {
                    rmb4.Insert(0, stringNum(eNum) + "佰");
                    seriesZero = false;
                }
            }

            if (eLen == 4)//千位
            {
                eNum = eNum4.Substring(0, 1);
                if (eNum == "0")
                {
                    if (!seriesZero)
                    {
                        rmb4.Insert(0, "零");
                        seriesZero = true;
                    }
                }
                else
                {
                    rmb4.Insert(0, stringNum(eNum) + "仟");
                    seriesZero = false;
                }
            }

            return rmb4.ToString();
        }

        private static string stringNum(string eNum)
        {
            switch (eNum)
            {
                case "1":
                    return "壹";
                case "2":
                    return "贰";
                case "3":
                    return "叁";
                case "4":
                    return "肆";
                case "5":
                    return "伍";
                case "6":
                    return "陆";
                case "7":
                    return "柒";
                case "8":
                    return "捌";
                case "9":
                    return "玖";
                default:
                    return "";
            }
        }
        #endregion

        /// <summary>
        /// 实现实体类的深拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="RealObject"></param>
        /// <returns></returns>
        public static T Clone<T>(T RealObject)
        {
            using (Stream objectStream = new MemoryStream())
            {
                using (Stream stream = new MemoryStream())
                {
                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                    serializer.Serialize(stream, RealObject);
                    stream.Seek(0, SeekOrigin.Begin);
                    return (T)serializer.Deserialize(stream);
                }  
            }
        }  
    }
}
