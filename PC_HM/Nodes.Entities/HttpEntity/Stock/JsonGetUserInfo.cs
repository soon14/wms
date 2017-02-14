using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Stock
{
    public class JsonGetUserInfo:BaseResult
    {
        public JsonGetUserInfoResult[] result { get; set; }
    }
}
