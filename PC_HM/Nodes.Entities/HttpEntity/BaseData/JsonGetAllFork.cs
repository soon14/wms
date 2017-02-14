using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.BaseData
{
    public class JsonGetAllFork:BaseResult
    {
        public JsonGetAllForkResult[] result { get; set; }
    }
}
