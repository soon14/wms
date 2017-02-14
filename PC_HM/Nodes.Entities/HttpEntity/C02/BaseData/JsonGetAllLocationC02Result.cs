using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Entities.HttpEntity.BaseData;

namespace Nodes.Entities.HttpEntity.C02.BaseData
{
    public class JsonGetAllLocationC02Result : JsonGetAllLocationByZoneResult
    {
        public string chCode
        {
            get;
            set;
        }
        public string chName
        {
            get;
            set;
        }
    }
}
