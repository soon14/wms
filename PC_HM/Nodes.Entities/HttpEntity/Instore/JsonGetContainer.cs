using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Instore
{
    public class JsonGetContainer:BaseResult
    {
        public JsonGetContainerResult[] result { get; set; }
    }
}
