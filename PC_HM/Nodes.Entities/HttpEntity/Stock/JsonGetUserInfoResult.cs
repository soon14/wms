using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Stock
{
    public class JsonGetUserInfoResult
    {
        #region 0-10
        public string isOwn
        {
            get;
            set;
        }
        public string updateDate
        {
            get;
            set;
        }
        public string remark
        {
            get;
            set;
        }
        public string userName
        {
            get;
            set;
        }
        public string isActive
        {
            get;
            set;
        }
        public string userCode
        {
            get;
            set;
        }
        public string branchCode
        {
            get;
            set;
        }
        public string whName
        {
            get;
            set;
        }
        public string whCode
        {
            get;
            set;
        }
        public string mobilePhone
        {
            get;
            set;
        }
        #endregion

        #region 11-15
        public string updateBy
        {
            get;
            set;
        }
        public string allowEdit
        {
            get;
            set;
        }
        public string isCenterWh
        {
            get;
            set;
        }
        public string centerWhCode
        {
            get;
            set;
        }
        public string pwd
        {
            get;
            set;
        }
        #endregion
    }
}
