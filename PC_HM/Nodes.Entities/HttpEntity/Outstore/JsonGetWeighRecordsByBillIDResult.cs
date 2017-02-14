using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Outstore
{
    public class JsonGetWeighRecordsByBillIDResult
    {
        /// <summary>
        /// 净重
        /// </summary>
        public string netWeight
        {
            get;
            set;
        }
        /// <summary>
        /// 净重
        /// </summary>
        public string crossWeight
        {
            get;
            set;
        }
        /// <summary>
        /// 车辆
        /// </summary>
        public string vhNo
        {
            get;
            set;
        }
        /// <summary>
        /// 称重员
        /// </summary>
        public string userName
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
        /// 复核员
        /// </summary>
        public string authUserName
        {
            get;
            set;
        }
        /// <summary>
        /// 称重时间
        /// </summary>
        public string createDate
        {
            get;
            set;
        }
    }
}
