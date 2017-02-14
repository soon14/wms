using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity
{
    public class JsonLogin :BaseResult
    {
        public JsonLoginResult[] result { get; set; }
    }
}
