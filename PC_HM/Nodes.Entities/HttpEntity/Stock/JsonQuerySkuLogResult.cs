using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Stock
{
    public class JsonQuerySkuLogResult
    {
        /// <summary>
        /// 业务类型
        /// </summary>
        public string evt
        {
            get;
            set;
        }
        /// <summary>
        /// 日期
        /// </summary>
        public string evtDate
        {
            get;
            set;
        }
        /// <summary>
        /// 数量
        /// </summary>
        public string qty
        {
            get;
            set;
        }
        /// <summary>
        /// 货位
        /// </summary>
        public string lcCode
        {
            get;
            set;
        }
        /// <summary>
        /// 操作人
        /// </summary>
        public string userName
        {
            get;
            set;
        }
        /// <summary>
        /// 单据编号
        /// </summary>
        public string billNo
        {
            get;
            set;
        }
    }
}
