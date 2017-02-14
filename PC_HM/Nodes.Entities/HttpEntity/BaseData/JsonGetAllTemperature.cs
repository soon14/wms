using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.BaseData
{
    public class JsonGetAllTemperature:BaseResult
    {
        public JsonGetAllTemperatureResult[] result { get; set; }
    }
}
