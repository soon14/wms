using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.BaseData
{
    public class JsonGetAllRoute:BaseResult
    {
        public JsonGetAllRouteResult[] result { get; set; }
    }
}
