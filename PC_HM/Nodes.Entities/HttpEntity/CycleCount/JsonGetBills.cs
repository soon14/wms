using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.CycleCount
{
    public class JsonGetBills:BaseResult
    {
        public JsonGetBillsResult[] result { get; set; }
    }
}
