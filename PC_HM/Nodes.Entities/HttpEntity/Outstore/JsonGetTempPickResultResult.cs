using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Outstore
{
    public class JsonGetTempPickResultResult
    {
        public JsonGetTempPickResultPick[] picktemp { get; set; }
        public JsonGetTempPickResultPickErr[] picktemperror { get; set; }
    }
}
