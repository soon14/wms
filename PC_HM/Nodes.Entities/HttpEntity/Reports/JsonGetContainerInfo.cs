using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Reports
{
    public class JsonGetContainerInfo:BaseResult
    {
        public JsonGetContainerInfoResult[] result { get; set; }
    }
}
