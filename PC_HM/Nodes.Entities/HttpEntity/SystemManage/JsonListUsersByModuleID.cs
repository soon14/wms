using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.SystemManage
{
    public class JsonListUsersByModuleID:BaseResult
    {
        public JsonListUsersByModuleIDResult[] result { get; set; }
    }
}
