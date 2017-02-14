using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Net;
using Nodes.Utils;

namespace Nodes.Adapter
{
    public class AppContext
    {
        private static string localIPAddress = null;
        private static string localURL = null;
        //public static HttpServer HttpServer = null;

        public static string LocalIPAddress
        {
            get
            {
                if (string.IsNullOrEmpty(localIPAddress))
                    localIPAddress = IPUtil.GetLocalIP();
                return localIPAddress;
            }
        }

        public static string LocalURL
        {
            get
            {
                if (string.IsNullOrEmpty(localURL))
                    localURL = string.Format("http://{0}:{1}/", LocalIPAddress, AppConfig.LocalPort);
                return localURL;
            }
        }
    }
}
