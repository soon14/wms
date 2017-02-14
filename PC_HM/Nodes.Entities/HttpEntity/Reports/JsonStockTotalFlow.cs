using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Reports
{
    public class JsonStockTotalFlow:BaseResult
    {
        public JsonStockTotalFlowResult[] result { get; set; }
    }
}
