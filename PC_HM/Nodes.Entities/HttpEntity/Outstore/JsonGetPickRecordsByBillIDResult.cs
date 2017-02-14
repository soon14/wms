using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Outstore
{
    public class JsonGetPickRecordsByBillIDResult
    {
        /// <summary>
        /// 商品编码
        /// </summary>
        public string skuName
        {
            get;
            set;
        }
        /// <summary>
        /// 计量单位
        /// </summary>
        public string umName
        {
            get;
            set;
        }
        /// <summary>
        /// 拣货货位
        /// </summary>
        public string lcCode
        {
            get;
            set;
        }
        /// <summary>
        /// 拣货时间
        /// </summary>
        public string pickDate
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
        /// 拣货员
        /// </summary>
        public string userName
        {
            get;
            set;
        }
        /// <summary>
        /// 计量单位
        /// </summary>
        public string skuCode
        {
            get;
            set;
        }
        /// <summary>
        /// 拣货数量
        /// </summary>
        public string pickQty
        {
            get;
            set;
        }
    }
}
