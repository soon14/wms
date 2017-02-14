using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Stock
{
    public class JsonGetStockSKUResult
    {
        public string skuName
        {
            get;
            set;
        }
        public string totalQty
        {
            get;
            set;
        }
        public string umName
        {
            get;
            set;
        }
        public string minStockQty
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
    }
}
