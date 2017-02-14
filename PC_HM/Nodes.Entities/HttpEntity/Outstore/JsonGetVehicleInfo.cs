using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Outstore
{
    public class JsonGetVehicleInfo:BaseResult
    {
        public JsonGetVehicleInfoResult[] result { get; set; }
    }
}
