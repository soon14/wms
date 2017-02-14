using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Outstore
{
    public class JsonGetPickRecordsByCtCodeResult
    {
        /// <summary>
        /// 商品编码
        /// </summary>
        public string skuCode
        {
            get;
            set;
        }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string skuName
        {
            get;
            set;
        }
        /// <summary>
        /// 规格
        /// </summary>
        public string spec
        {
            get;
            set;
        }
        /// <summary>
        /// 拣货人
        /// </summary>
        public string userName
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
        /// 拣货数量
        /// </summary>
        public string sailQty
        {
            get;
            set;
        }
        /// <summary>
        /// 单位
        /// </summary>
        public string sailUmName
        {
            get;
            set;
        }
        /// <summary>
        /// 单位重量(g)
        /// </summary>
        public string weight
        {
            get;
            set;
        }
        /// <summary>
        /// 总重量(g)
        /// </summary>
        public string totalWeight
        {
            get;
            set;
        }
    }
}
