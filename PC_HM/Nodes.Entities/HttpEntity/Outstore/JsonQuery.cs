using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Outstore
{
    public class JsonQuery:BaseResult
    {
        public JsonQueryResult[] result { get; set; }
    }
}
