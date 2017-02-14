using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Stock
{
    public class JsonGetTempStockBySKU:BaseResult
    {
        public JsonGetTempStockBySKUResult[] result { get; set; }
    }
}
