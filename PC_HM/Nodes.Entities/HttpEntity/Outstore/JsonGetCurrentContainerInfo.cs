using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Outstore
{
    public class JsonGetCurrentContainerInfo:BaseResult
    {
        public JsonGetCurrentContainerInfoResult[] result { get; set; }
    }
}
