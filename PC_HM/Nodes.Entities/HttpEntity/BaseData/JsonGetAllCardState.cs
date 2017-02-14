using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.BaseData
{
    public class JsonGetAllCardState:BaseResult
    {
        public JsonGetAllCardStateResult[] result { get; set; }
    }
}
