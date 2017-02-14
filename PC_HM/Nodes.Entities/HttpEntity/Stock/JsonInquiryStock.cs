using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Stock
{
    public class JsonInquiryStock:BaseResult
    {
        public JsonInquiryStockResult[] result { get; set; }
    }
}
