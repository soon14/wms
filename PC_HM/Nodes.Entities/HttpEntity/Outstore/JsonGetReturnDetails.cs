using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Outstore
{
    public class JsonGetReturnDetails:BaseResult
    {
        public JsonGetReturnDetailsResult[] result { get; set; }
    }
}
