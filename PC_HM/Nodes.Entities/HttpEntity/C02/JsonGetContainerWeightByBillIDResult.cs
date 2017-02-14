using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.C02
{
    public class JsonGetContainerWeightByBillIDResult
    {
        public string ctCode
        {
            get;
            set;
        }
        public string lastUpdateTime
        {
            get;
            set;
        }
        public string calcWeight
        {
            get;
            set;
        }
        public string billHeadId
        {
            get;
            set;
        }
        public string grossWeight
        {
            get;
            set;
        }
    }
}
