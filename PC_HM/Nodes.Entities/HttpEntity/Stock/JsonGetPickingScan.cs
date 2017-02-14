using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Stock
{
    public class JsonGetPickingScan:BaseResult
    {
        public JsonGetPickingScanResult[] result { get; set; }
    }
}
