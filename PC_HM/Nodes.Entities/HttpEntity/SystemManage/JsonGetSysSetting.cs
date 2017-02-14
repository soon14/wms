using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.SystemManage
{
    public class JsonGetSysSetting:BaseResult
    {
        public JsonGetSysSettingResult[] result { get; set; }
    }
}
