using System.Collections.Generic;
using System.Net;

namespace Nodes.Utils
{
    public class IPUtil
    {
        /// <summary>
        /// 获取我的IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIP()
        {
            string myIP = string.Empty;
            foreach (IPAddress ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    myIP = ip.ToString();
                    break;
                }
            }

            return myIP;
        }

        public static string[] GetLocalIPArray()
        {
            List<string> list = new List<string>();
            foreach (IPAddress ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    list.Add(ip.ToString());
                }
            }
            return list.ToArray();
        }
    }
}
