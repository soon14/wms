using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.C02
{
    public class JsonQueryBillsByStatus:BaseResult
    {
        public JsonQueryBillsByStatusResult[] result { get; set; }
    }
}
