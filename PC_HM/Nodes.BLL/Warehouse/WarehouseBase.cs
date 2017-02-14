using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.DBHelper
{
    [Serializable]
    public class WarehouseBase
    {
        public virtual void GetIsCaseQty(StringBuilder strBuilder, out int? isCase1, out int? isCase2);
    }
}
