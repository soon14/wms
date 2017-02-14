using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Utils
{
    public class EUtilStroreType
    {
        public static int WarehouseTypeToInt(object obj)
        {
            int ret = 0;
            if (obj == null)
                ret = 1;
            else
            {
                ret = Convert.ToInt32(obj) + 1;
            }

            return ret;
        }
    }
}
