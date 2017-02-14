using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.BaseData
{
    public class JsonGetAllRecLocation:BaseResult
    {
        public JsonGetAllRecLocationResult[] result { get; set; }
    }
}
