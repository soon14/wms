using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.SystemManage
{
    public class JsonGetAllUsers:BaseResult
    {
        public JsonGetAllUsersResult[] result { get; set; }
    }
}
