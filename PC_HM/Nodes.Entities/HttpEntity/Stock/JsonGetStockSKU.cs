using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Stock
{
    public class JsonGetStockSKU:BaseResult
    {
        public JsonGetStockSKUResult[] result { get; set; }
    }
}
