using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Outstore
{
    public class JsonGetDetails:BaseResult
    {
        public JsonGetDetailsResult[] result { get; set; }
    }
}
