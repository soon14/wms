using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Outstore
{
    public class JsonGetContainerByBillIDResult
    {
        /// <summary>
        /// 容器状态
        /// </summary>
        public string ctState
        {
            get;
            set;
        }
        /// <summary>
        /// 车次编号
        /// </summary>
        public string vhTrainNo
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string billId
        {
            get;
            set;
        }
        /// <summary>
        /// 托盘位号
        /// </summary>
        public string lcCode
        {
            get;
            set;
        }
        /// <summary>
        /// 容器编号
        /// </summary>
        public string ctCode
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
    }
}
