using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.BaseData
{
    public class JsonGetAllWarehouse:BaseResult
    {
        public JsonGetAllWarehouseResult[] result { get; set; }
    }
}
