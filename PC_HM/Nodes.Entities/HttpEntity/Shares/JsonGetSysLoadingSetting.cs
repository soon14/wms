using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Entities;

namespace Nodes.Entities.HttpEntity.Shares
{
    public class JsonGetSysLoadingSetting:BaseResult
    {
        public JsonGetSysLoadingSettingResult[] result { get; set; }
    }
}
