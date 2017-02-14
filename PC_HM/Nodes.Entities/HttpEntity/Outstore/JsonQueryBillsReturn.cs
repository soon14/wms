using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Outstore
{
    public class JsonQueryBillsReturn:BaseResult
    {
        public JsonQueryBillsReturnResult[] result { get; set; }
    }
}
