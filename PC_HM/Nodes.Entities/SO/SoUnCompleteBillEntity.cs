using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.Entities.SO
{
    public class SoUnCompleteBillEntity
    {
        [ColumnName("BILLNOS")]
        public string BillNoS { get; set; }
    }
}
