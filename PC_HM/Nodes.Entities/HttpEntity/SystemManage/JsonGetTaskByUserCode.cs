using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.SystemManage
{
    public  class JsonGetTaskByUserCode:BaseResult
    {
        public JsonGetTaskByUserCodeResult[] result { get; set; }
    }
}
