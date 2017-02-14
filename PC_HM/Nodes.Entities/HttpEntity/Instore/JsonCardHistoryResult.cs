using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Instore
{
    public class JsonCardHistoryResult
    {
        public string vehicleNo
        {
            get;
            set;
        }
        public string creator
        {
            get;
            set;
        }
        public string driver
        {
            get;
            set;
        }
        /// <summary>
        /// 供货商
        /// </summary>
        public string cName//cName///
        {
            get;
            set;
        }
        public string contact
        {
            get;
            set;
        }
        /// <summary>
        /// 入库单状态
        /// </summary>
        public string billStateDesc//billStateDesc
        {
            get;
            set;
        }
        public string cardNo
        {
            get;
            set;
        }
        /// <summary>
        /// 入库单编号
        /// </summary>
        public string billNo//billNo
        {
            get;
            set;
        }
        /// <summary>
        /// 关联时间
        /// </summary>
        public string createDate//createDate
        {
            get;
            set;
        }
    }

    public class JsonCardHistoryEntity
    {
        public string vehicleNo
        {
            get;
            set;
        }
        public string creator
        {
            get;
            set;
        }
        public string driver
        {
            get;
            set;
        }
        /// <summary>
        /// 供货商
        /// </summary>
        public string S_NAME//cName///
        {
            get;
            set;
        }
        public string contact
        {
            get;
            set;
        }
        /// <summary>
        /// 入库单状态
        /// </summary>
        public string BILL_STATE_DESC//billStateDesc
        {
            get;
            set;
        }
        public string cardNo
        {
            get;
            set;
        }
        /// <summary>
        /// 入库单编号
        /// </summary>
        public string BILL_NO//billNo
        {
            get;
            set;
        }
        /// <summary>
        /// 关联时间
        /// </summary>
        public string CREATE_DATE//createDate
        {
            get;
            set;
        }
    }
}
