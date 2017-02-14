using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.BaseData
{
    public class JsonGetLocalAllResult
    {
        #region 0-10
        public string spec
        {
            get;
            set;
        }
        public string securityQty
        {
            get;
            set;
        }
        public string tempName
        {
            get;
            set;
        }
        public string skuName
        {
            get;
            set;
        }
        public string skuCode
        {
            get;
            set;
        }
        public string maxStockQty
        {
            get;
            set;
        }
        public string brdName
        {
            get;
            set;
        }
        public string skuType
        {
            get;
            set;
        }
        public string tempCode
        {
            get;
            set;
        }
        public string typName
        {
            get;
            set;
        }
        #endregion

        #region 11-16
        public string upperLocation
        {
            get;
            set;
        }
        public string itemDesc
        {
            get;
            set;
        }
        public string expDays
        {
            get;
            set;
        }
        public string lowerLocation
        {
            get;
            set;
        }
        public string minStockQty
        {
            get;
            set;
        }
        public string totalStockQty
        {
            get;
            set;
        }
        #endregion
    }
}
