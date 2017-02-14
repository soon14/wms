using System;
using System.Collections.Generic;
using Nodes.Dapper;
using Nodes.Entities;

namespace Nodes.DBHelper
{
    public class StatusDal
    {
        ///<summary>
        ///查询所有状态
        ///</summary>
        ///<returns></returns>
        public List<StatusEntity> GetAllStatus()
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT  COD,NAM FROM STATUS";
            List<StatusEntity> StatusEntities = map.Query<StatusEntity>(sql);
            return StatusEntities;
        }
    }
}
