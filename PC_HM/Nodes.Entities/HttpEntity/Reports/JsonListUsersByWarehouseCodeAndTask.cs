using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Reports
{
    public class JsonListUsersByWarehouseCodeAndTask:BaseResult
    {
        public JsonListUsersByWarehouseCodeAndTaskResult[] result { get; set; }
    }
}
