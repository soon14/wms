using System.Collections.Generic;
using Nodes.Dapper;
using Nodes.Entities;

namespace Nodes.DBHelper
{
    public class UnitDal
    {
        /// <summary>
        /// 检查计量单位编码是否已存在
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        private bool IsUnitCodeExists(UnitEntity unit)
        {
            IMapper map = DatabaseInstance.Instance();
            string id = map.ExecuteScalar<string>("select UM_CODE from WM_UM where UM_CODE = @COD", new { COD = unit.UnitCode });
            return !string.IsNullOrEmpty(id);
        }

        /// <summary>
        /// 添加或编辑计量单位
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isNew">添加或编辑</param>
        /// <returns></returns>
        public int Save(UnitEntity entity, bool isNew)
        {
            IMapper map = DatabaseInstance.Instance();
            int ret = -2;
            if (isNew)
            {
                //检查编号是否已经存在
                if (IsUnitCodeExists(entity))
                    return -1;
                ret = map.Execute("insert into WM_UM(UM_CODE, UM_NAME) " +
                    "values(@COD, @NAM)",
                new
                {
                    COD = entity.UnitCode,
                    NAM = entity.UnitName
                });
            }
            else
            {
                //更新
                ret = map.Execute("update WM_UM set UM_NAME = @NAM where UM_CODE = @COD",
                new
                {
                    COD = entity.UnitCode,
                    NAM = entity.UnitName
                });
            }
            return ret;
        }

        ///<summary>
        ///查询所有计量单位
        ///</summary>
        ///<returns></returns>
        public List<UnitEntity> GetAllUnit()
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT UM_CODE, UM_NAME FROM WM_UM";
            return map.Query<UnitEntity>(sql);
        }

        /// <summary>
        /// 删除计量单位
        /// </summary>
        /// <param name="UnitCode"></param>
        /// <returns></returns>
        public int DeleteUnit(string UnitCode)
        {
            IMapper map = DatabaseInstance.Instance();
            return map.Execute("delete from WM_UM where UM_CODE = @COD", new { COD = UnitCode });
        }
    }
}
