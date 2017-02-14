using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Reports
{
    public class JsonGetAsnRecords:BaseResult
    {
        public JsonGetAsnRecordsResult[] result { get; set; }
    }
}
