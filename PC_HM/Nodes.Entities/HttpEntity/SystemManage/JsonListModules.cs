using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.SystemManage
{
    public class JsonListModules:BaseResult
    {
        public JsonListModulesResult[] result { get; set; }
    }
}
