using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Outstore
{
    public class JsonIsHaveOtherStatusResult
    {
        /// <summary>
        /// 订单状态
        /// </summary>
        public string billStateName
        {
            get;
            set;
        }
        /// <summary>
        /// 订单名称
        /// </summary>
        public string billNo
        {
            get;
            set;
        }
    }
}
