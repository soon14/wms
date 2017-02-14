using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Utils;

namespace Nodes.Entities
{
    /// <summary>
    /// 用户信息
    /// 对应表：USERS
    /// </summary>
    public class JsonUserEntityResult
    {
        public string userCode
        {
            get;
            set;
        }
        public string userName
        {
            get;
            set;
        }
    }
}
