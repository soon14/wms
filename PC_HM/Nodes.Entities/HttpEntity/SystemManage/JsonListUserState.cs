using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.SystemManage
{
    public class JsonListUserState:BaseResult
    {
        public JsonListUserStateResult[] result { get; set; }
    }
}
