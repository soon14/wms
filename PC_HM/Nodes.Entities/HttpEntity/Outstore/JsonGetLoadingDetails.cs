using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Outstore
{
    public class JsonGetLoadingDetails:BaseResult
    {
        public JsonGetLoadingDetailsResult[] result { get; set; }
    }
}
