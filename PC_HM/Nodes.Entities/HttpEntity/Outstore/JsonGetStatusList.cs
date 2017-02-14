using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Outstore
{
    public class JsonGetStatusList:BaseResult
    {
        public JsonGetStatusListResult[] result { get; set; }
    }
}
