using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.SystemManage
{
    public class JsonListModulesByRoleID:BaseResult
    {
        public JsonListModulesByRoleIDResult[] result { get; set; }
    }
}
