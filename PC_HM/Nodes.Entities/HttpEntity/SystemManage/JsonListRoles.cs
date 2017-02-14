using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.SystemManage
{
    public class JsonListRoles:BaseResult
    {
        public JsonListRolesResult[] result { get; set; }
    }
}
