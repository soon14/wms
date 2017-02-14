using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Outstore
{
    public class JsonGetCurrentContainerInfoResult
    {
        /// <summary>
        /// 托盘类型（物流箱）
        /// </summary>
        public string ctType
        {
            get;
            set;
        }
        /// <summary>
        /// 容器状态
        /// </summary>
        public string ctState
        {
            get;
            set;
        }
        /// <summary>
        /// 净重; 去皮;（G）   去除托盘(托盘和地牛)的重量
        /// </summary>
        public decimal netWeight
        {
            get;
            set;
        }
        /// <summary>
        /// 毛重; 总重; 连皮;（G） 实际称重
        /// </summary>
        public decimal crossWeight
        {
            get;
            set;
        }
        /// <summary>
        /// 容器本身的重量
        /// </summary>
        public decimal ctWeight
        {
            get;
            set;
        }

        /// <summary>
        /// 和该容器关联的订单
        /// </summary>
        public string billHeadId
        {
            get;
            set;
        }
        /// <summary>
        /// 托盘编码（物流箱）
        /// </summary>
        public string ctCode
        {
            get;
            set;
        }
    }
}
