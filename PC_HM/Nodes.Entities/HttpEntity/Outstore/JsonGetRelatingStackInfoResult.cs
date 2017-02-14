using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Outstore
{
    public class JsonGetRelatingStackInfoResult
    {
        /// <summary>
        /// 单位
        /// </summary>
        public string umName
        {
            get;
            set;
        }
        /// <summary>
        /// 复核人员
        /// </summary>
        public string checkName
        {
            get;
            set;
        }
        /// <summary>
        /// 托盘号
        /// </summary>
        public string ctCode
        {
            get;
            set;
        }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string skuName
        {
            get;
            set;
        }
        /// <summary>
        /// 清点数
        /// </summary>
        public decimal qty
        {
            get;
            set;
        }
        /// <summary>
        /// 托盘状态
        /// </summary>
        public string itemDesc
        {
            get;
            set;
        }
        /// <summary>
        /// 生产日期
        /// </summary>
        public string productDate
        {
            get;
            set;
        }
        /// <summary>
        /// 清点人员
        /// </summary>
        public string creator
        {
            get;
            set;
        }
    }
}
