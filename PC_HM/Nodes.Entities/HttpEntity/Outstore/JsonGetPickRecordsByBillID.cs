using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Outstore
{
    public class JsonGetPickRecordsByBillID:BaseResult
    {
        public JsonGetPickRecordsByBillIDResult[] result { get; set; }
    }
}
