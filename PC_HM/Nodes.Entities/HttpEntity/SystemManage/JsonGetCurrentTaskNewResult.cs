using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.SystemManage
{
    public class JsonGetCurrentTaskNewResult
    {
        #region 0-10
        public string billDesc
        {
            get;
            set;
        }
        public string userName
        {
            get;
            set;
        }
        public string itemDesc
        {
            get;
            set;
        }
        public string userCode
        {
            get;
            set;
        }
        public string taskDesc
        {
            get;
            set;
        }
        public string confirmDate
        {
            get;
            set;
        }
        public string taskType
        {
            get;
            set;
        }
        public string billId
        {
            get;
            set;
        }
        public string qty
        {
            get;
            set;
        }
        public string billTypeDEsc
        {
            get;
            set;
        }
        #endregion

        #region 11-14
        public string createDate
        {
            get;
            set;
        }
        public string isCase
        {
            get;
            set;
        }
        public string billNo
        {
            get;
            set;
        }
        public string tskId
        {
            get;
            set;
        }
        public string beginDate
        {
            get;
            set;
        }
        #endregion
    }
}
