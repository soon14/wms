using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.SystemManage
{
    public class JsonListUsersByRoleID:BaseResult
    {
        public JsonListUsersByRoleIDResult[] result { get; set; }
    }
}
