using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Stock
{
    public class JsonGetTempStockByBill:BaseResult
    {
        public JsonGetTempStockByBillResult[] result { get; set; }
    }
}
