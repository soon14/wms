using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Outstore
{
    public class JsonGetBulkCargoQty:BaseResult
    {
        public JsonGetBulkCargoQtyResult[] result { get; set; }
    }
}
