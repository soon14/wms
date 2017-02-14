using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.SystemManage
{
    public class JsonQueryStockWarmResult
    {
        #region 0-10
        public string spec
        {
            get;
            set;
        }
        public string znName
        {
            get;
            set;
        }
        public string lcCode
        {
            get;
            set;
        }
        public string skuName
        {
            get;
            set;
        }
        public string qty
        {
            get;
            set;
        }
        public string expDate
        {
            get;
            set;
        }
        public string skuCode
        {
            get;
            set;
        }
        public string pickingQty
        {
            get;
            set;
        }
        public string id
        {
            get;
            set;
        }
        public string umName
        {
            get;
            set;
        }
        #endregion

        #region 11-14
        public string latestIn
        {
            get;
            set;
        }
        public string occupyQty
        {
            get;
            set;
        }
        public string expDays
        {
            get;
            set;
        }
        public string latestOut
        {
            get;
            set;
        }
        #endregion
    }
}
