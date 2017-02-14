using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Reports
{
    public class JsonGetReport:BaseResult
    {
        public JsonGetReportResult[] result { get; set; }
    }
}
