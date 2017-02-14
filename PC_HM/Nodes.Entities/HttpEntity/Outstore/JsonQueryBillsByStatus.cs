using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Outstore
{
    public class JsonQueryBillsByStatus:BaseResult
    {
        public JsonQueryBillsByStatusResult[] result { get; set; }
    }
}
