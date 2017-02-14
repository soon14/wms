using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Reports
{
    public class JsonQuerySoDetails:BaseResult
    {
        public JsonQuerySoDetailsResult[] result { get; set; }
    }
}
