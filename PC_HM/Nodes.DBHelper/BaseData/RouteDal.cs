using System.Collections.Generic;
using Nodes.Dapper;
using Nodes.Entities;

namespace Nodes.DBHelper
{
    public class RouteDal
    {
        /// <summary>
        /// 检查计量单位编码是否已存在
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        private bool IsCodeExists(RouteEntity unit)
        {
            IMapper map = DatabaseInstance.Instance();
            string id = map.ExecuteScalar<string>("select RT_CODE from WM_ROUTE where RT_CODE = @COD", new { COD = unit.RouteCode });
            return !string.IsNullOrEmpty(id);
        }

        /// <summary>
        /// 添加或编辑计量单位
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isNew">添加或编辑</param>
        /// <returns></returns>
        public int Save(RouteEntity entity, bool isNew)
        {
            IMapper map = DatabaseInstance.Instance();
            int ret = -2;
            if (isNew)
            {
                //检查编号是否已经存在
                if (IsCodeExists(entity))
                    return -1;
                ret = map.Execute(string.Format("insert into WM_ROUTE(RT_CODE, RT_NAME, LAST_UPDATETIME) " +
                    "values(@COD, @NAM, {0})", map.GetSysDateString()),
                new
                {
                    COD = entity.RouteCode,
                    NAM = entity.RouteName
                });
            }
            else
            {
                //更新
                ret = map.Execute(string.Format("update WM_ROUTE set RT_NAME = @NAM, LAST_UPDATETIME = {0} where RT_CODE = @COD", map.GetSysDateString()),
                new
                {
                    COD = entity.RouteCode,
                    NAM = entity.RouteName
                });
            }
            return ret;
        }

        ///<summary>
        ///查询所有路线
        ///</summary>
        ///<returns></returns>
        public List<RouteEntity> GetAll()
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT RT_CODE, RT_NAME FROM WM_ROUTE";
            return map.Query<RouteEntity>(sql);
        }

        /// <summary>
        /// 删除计量单位
        /// </summary>
        /// <param name="RouteCode"></param>
        /// <returns></returns>
        public int DeleteUnit(string RouteCode)
        {
            IMapper map = DatabaseInstance.Instance();
            return map.Execute("delete from WM_ROUTE where RT_CODE = @COD", new { COD = RouteCode });
        }
    }
}
