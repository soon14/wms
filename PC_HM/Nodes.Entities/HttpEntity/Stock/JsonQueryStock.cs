using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Stock
{
    public class JsonQueryStock:BaseResult
    {
        public JsonQueryStockResult[] result { get; set; }
        public int total
        {
            get;
            set;
        }
    }
}
