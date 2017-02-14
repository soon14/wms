using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Outstore
{
    public class JsonGetSystemDiffSet:BaseResult
    {
        public JsonGetSystemDiffSetResult[] result { get; set; }
    }
}
