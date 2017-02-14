using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Outstore
{
    public class JsonListContainerState:BaseResult
    {
        public JsonListContainerStateResult[] result { get; set; }
    }
}
