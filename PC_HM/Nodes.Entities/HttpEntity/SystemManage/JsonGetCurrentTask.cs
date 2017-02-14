using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.SystemManage
{
    public class JsonGetCurrentTask:BaseResult
    {
        public JsonGetCurrentTaskResult[] result { get; set; }
    }
}
