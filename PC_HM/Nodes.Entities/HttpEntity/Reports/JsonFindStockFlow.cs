using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Reports
{
    public class JsonFindStockFlow:BaseResult
    {
        public JsonFindStockFlowResult[] result { get; set; }
    }
}
