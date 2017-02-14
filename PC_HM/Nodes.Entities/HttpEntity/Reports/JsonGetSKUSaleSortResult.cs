using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Reports
{
    public class JsonGetSKUSaleSortResult
    {
        /// <summary>
        /// 销售单位
        /// </summary>
        public string umName
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
        /// 商品名称
        /// </summary>
        public string skuName
        {
            get;
            set;
        }
        /// <summary>
        /// 销售数量
        /// </summary>
        public string qty
        {
            get;
            set;
        }
        /// <summary>
        /// 商品编码
        /// </summary>
        public string skuCode
        {
            get;
            set;
        }
    }
}
