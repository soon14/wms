using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.BaseData
{
    public class JsonGetAllLocationByZone:BaseResult
    {
        public JsonGetAllLocationByZoneResult[] result { get; set; }
    }
}
