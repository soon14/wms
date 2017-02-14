using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Outstore
{
    public class JsonListUsers:BaseResult
    {
        public JsonListUsersResult[] result { get; set; }
    }
}
