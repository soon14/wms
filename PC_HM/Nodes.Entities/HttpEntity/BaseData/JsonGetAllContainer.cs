using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.BaseData
{
    public class JsonGetAllContainer:BaseResult
    {
        public JsonGetAllContainerResult[] result { get; set; }
    }
}
