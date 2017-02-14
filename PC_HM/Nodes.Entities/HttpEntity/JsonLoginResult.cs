using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity
{
    public class JsonLoginResult
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public string isActive { get; set; }

        /// <summary>
        /// 中央城编码
        /// </summary>
        public string center_wh_code { get; set; }

        /// <summary>
        /// 是否是中央仓
        /// </summary>
        public int is_center_wh { get; set; }

        /// <summary>
        /// 仓库名称
        /// </summary>
        public string wh_name { get; set; }

        /// <summary>
        /// 仓库类型
        /// </summary>
        public int wareHouseType { get; set; }

        /// <summary>
        /// 用户编码
        /// </summary>
        public string userCode { get; set; }

        /// <summary>
        /// 仓库编码
        /// </summary>
        public string wh_code { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string passWord { get; set; }

        /// <summary>
        /// 是否在线
        /// </summary>
        public string isOnline { get; set; }

        /// <summary>
        /// 服务器时间
        /// </summary>
        public string serverTime { get; set; }

    }
}
