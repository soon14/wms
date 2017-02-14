using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.SystemManage
{
    public class JsonListRolesByModuleID:BaseResult
    {
        public JsonListRolesByModuleIDResult[] result { get; set; }
    }
}
