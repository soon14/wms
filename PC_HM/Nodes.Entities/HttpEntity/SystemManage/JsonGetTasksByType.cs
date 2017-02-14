using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.SystemManage
{
    public class JsonGetTasksByType:BaseResult
    {
        public JsonGetTasksByTypeResult[] result { get; set; }
    }
}
