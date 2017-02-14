using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Utils;
using System.Configuration;

namespace Nodes.Adapter
{
    public static class AppConfig
    {
        private static int localPort = -1;
        public static int LocalPort
        {
            get
            {
                if (localPort == -1)
                    localPort = ConvertUtil.ToInt(ConfigurationManager.AppSettings["LOCAL_PORT"]);
                return localPort;
            }
        }
    }
}
