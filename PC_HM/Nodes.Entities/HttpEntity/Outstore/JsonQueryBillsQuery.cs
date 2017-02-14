using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Outstore
{
    public class JsonQueryBillsQuery:BaseResult
    {
        public JsonQueryBillsQueryResult[] result { get; set; }
    }
}
