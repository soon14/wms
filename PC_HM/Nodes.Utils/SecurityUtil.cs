using System;
using System.Security.Cryptography;
using System.Text;

namespace Nodes.Utils
{
    public class SecurityUtil
    {
        /// <summary>
        /// 将普通字符串转换为Base64字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string StringToBase64(string source)
        {
            if (string.IsNullOrEmpty(source)) return string.Empty;

            byte[] b = Encoding.Default.GetBytes(source);
            return Convert.ToBase64String(b);
        }

        /// <summary>
        /// 将Base64字符串转换为普通字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string Base64ToString(string source)
        {
            if (string.IsNullOrEmpty(source)) return string.Empty;

            byte[] b = Convert.FromBase64String(source);
            return Encoding.Default.GetString(b);
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}
