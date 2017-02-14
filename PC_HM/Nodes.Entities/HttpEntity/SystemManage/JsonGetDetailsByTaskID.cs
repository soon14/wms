using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.SystemManage
{
    public class JsonGetDetailsByTaskID:BaseResult
    {
        public JsonGetDetailsByTaskIDResult[] result { get; set; }
    }
}
