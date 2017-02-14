using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Outstore
{
    public class JsonListContainerStateResult
    {
        /// <summary>
        /// 物流箱状态
        /// </summary>
        public string stateDesc
        {
            get;
            set;
        }
        /// <summary>
        /// 物流箱编号
        /// </summary>
        public string ctCode
        {
            get;
            set;
        }
        /// <summary>
        /// 关联的出库单
        /// </summary>
        public string billNo
        {
            get;
            set;
        }
        /// <summary>
        /// 关联的笼车
        /// </summary>
        public string lcCode
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
        /// 客户
        /// </summary>
        public string cName
        {
            get;
            set;
        }
        public string billHeadId
        {
            get;
            set;
        }
        public string uniqueCode
        {
            get;
            set;
        }
    }
}
