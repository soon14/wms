using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Stock
{
    public class JsonGetAllLocation:BaseResult
    {
        public JsonGetAllLocationResult[] result { get; set; }
    }
}
