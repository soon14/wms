using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Reports
{
    public class JsonSummaryByPersonnel:BaseResult
    {
        public JsonSummaryByPersonnelResult[] result { get; set; }
    }

    public class JsonSummaryByPersonnelSanhuo : BaseResult
    {
        public JsonSummaryByPersonnelResultSanhuo[] result { get; set; }
    }
}
