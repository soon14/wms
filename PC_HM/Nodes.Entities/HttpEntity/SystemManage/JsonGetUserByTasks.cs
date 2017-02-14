using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.SystemManage
{
    public class JsonGetUserByTasks:BaseResult
    {
        public JsonGetUserByTasksResult[] result { get; set; }
    }
}
