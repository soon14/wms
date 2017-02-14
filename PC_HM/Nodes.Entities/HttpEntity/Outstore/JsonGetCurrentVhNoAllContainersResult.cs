using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Outstore
{
    public class JsonGetCurrentVhNoAllContainersResult
    {
        public string trainNo
        {
            get;
            set;
        }
        /// <summary>
        /// 装车顺序
        /// </summary>
        public string inVhSort
        {
            get;
            set;
        }
        /// <summary>
        /// 托盘编号
        /// </summary>
        public string ctCode
        {
            get;
            set;
        }
        /// <summary>
        /// 订单ID
        /// </summary>
        public string billId
        {
            get;
            set;
        }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string billNo
        {
            get;
            set;
        }
        public string ctType
        {
            get;
            set;
        }
        public string ctState
        {
            get;
            set;
        }
        /// <summary>
        /// 理论重量(kg)
        /// </summary>
        public string calcWeight
        {
            get;
            set;
        }
        /// <summary>
        /// 实际重量(kg)
        /// </summary>
        public string grossWeight
        {
            get;
            set;
        }
        /// <summary>
        /// 整货件数
        /// </summary>
        public string sailQty
        {
            get;
            set;
        }
    }
}
