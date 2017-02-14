using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Instore
{
    public class JsonGetDetailByBillID:BaseResult
    {
        public JsonGetDetailByBillIDResult[] result { get; set; }
    }
}
