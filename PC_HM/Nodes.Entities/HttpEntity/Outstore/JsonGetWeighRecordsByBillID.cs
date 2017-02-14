using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Outstore
{
    public class JsonGetWeighRecordsByBillID:BaseResult
    {
        public JsonGetWeighRecordsByBillIDResult[] result { get; set; }
    }
}
