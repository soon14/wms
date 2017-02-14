using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Outstore
{
    public class JsonGetConfirmHistory:BaseResult
    {
        public JsonGetConfirmHistoryResult[] result { get; set; }
    }
}
