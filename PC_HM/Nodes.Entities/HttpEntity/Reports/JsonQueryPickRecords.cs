using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Reports
{
    public class JsonQueryPickRecords:BaseResult
    {
        public JsonQueryPickRecordsResult[] result { get; set; }
    }
}
