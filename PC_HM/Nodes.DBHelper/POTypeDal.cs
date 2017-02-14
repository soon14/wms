using System;
using System.Collections.Generic;
using Nodes.Dapper;
using Nodes.Entities;
using System.Data;

namespace Nodes.WMS.DBHelper
{
    /// <summary>
    /// 入库类型是系统数据，不在客户端维护
    /// </summary>
    public class POTypeDal
    {
        /// <summary>
        /// 列出状态为“启用”并且按优先级排序的入库类型
        /// </summary>
        /// <returns></returns>
        public DataTable ListActiveTypes()
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT COD, DES FROM INSTORE_TYPE WHERE ACTIVED = 1 ORDER BY PRI ASC";
            return map.LoadTable(sql);
        }
    }
}
