using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Instore
{
    public class JsonQueryOverStockBills:BaseResult
    {
        public JsonQueryOverStockBillsResult[] result { get; set; }
    }
}
