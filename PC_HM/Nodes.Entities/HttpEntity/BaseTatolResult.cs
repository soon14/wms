using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity
{
    public class BaseTatolResult
    {
        /// <summary>
        ///  标记 0  成功，1 系统错误, 2 错误信息
        /// </summary>
        public int flag
        {
            get;
            set;
        }

        /// <suummary>
        /// 状态
        /// </summary>
        public string status
        {
            get;
            set;
        }


        /// <summary>
        /// 错误内容
        /// </summary>
        public string error
        {
            get;
            set;
        }

        /// <summary>
        /// 记录总数
        /// </summary>
        public int total
        {
            get;
            set;
        }
    }
}
