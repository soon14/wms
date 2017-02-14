using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Instore
{
    public class JsonBills:BaseResult
    {
        public JsonBillsResult[] result { get; set; }
    }
}
