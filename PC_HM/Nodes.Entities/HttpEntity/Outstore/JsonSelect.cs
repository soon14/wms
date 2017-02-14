using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Outstore
{
    public class JsonSelect:BaseResult
    {
        public JsonSelectResult[] result { get; set; }
    }
}
