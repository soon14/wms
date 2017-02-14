using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Outstore
{
    public class JsonListUsersByRoleAndWarehouseCode:BaseResult
    {
        public JsonListUsersByRoleAndWarehouseCodeResult[] result { get; set; }
    }
}
