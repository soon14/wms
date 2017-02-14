using System;
using System.Collections.Generic;
using Nodes.Dapper;
using Nodes.Entities;

namespace Nodes.DBHelper
{
    public class MaterialTypeDal
    {
        /// <summary>
        /// 检查主分类编码是否已存在
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool IsCodeExists(MaterialTypeEntity type)
        {
            IMapper map = DatabaseInstance.Instance();
            string id = map.ExecuteScalar<string>("SELECT TYP_CODE FROM WM_SKU_TYPE WHERE TYP_CODE = @COD",
            new { COD = type.MaterialTypeCode });

            return !string.IsNullOrEmpty(id);
        }

        /// <summary>
        /// 添加或编辑分类
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isNew">添加或编辑</param>
        /// <returns></returns>
        public int Save(MaterialTypeEntity entity, bool isNew)
        {
            IMapper map = DatabaseInstance.Instance();
            int ret = -2;

            if (isNew)
            {
                //检查编号是否已经存在
                if (IsCodeExists(entity))
                    return -1;
                ret = map.Execute("INSERT INTO WM_SKU_TYPE(TYP_CODE, TYP_NAME, ZN_CODE) VALUES(@COD, @NAM, @ZN_CODE)",
                new
                {
                    COD = entity.MaterialTypeCode,
                    NAM = entity.MaterialTypeName,
                    ZN_CODE = entity.ZoneCode
                });
            }
            else
            {
                //更新
                ret = map.Execute("UPDATE WM_SKU_TYPE SET TYP_NAME = @NAM, ZN_CODE = @ZN_CODE WHERE TYP_CODE = @COD",
                new
                {
                    COD = entity.MaterialTypeCode,
                    NAM = entity.MaterialTypeName,
                    ZN_CODE = entity.ZoneCode
                });
            }
            return ret;
        }

        ///<summary>
        ///查询所有分类
        ///</summary>
        ///<returns></returns>
        public List<MaterialTypeEntity> GetAll()
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT T.TYP_CODE, T.TYP_NAME, T.ZN_CODE, Z.ZN_NAME FROM WM_SKU_TYPE T LEFT JOIN WM_ZONE Z ON T.ZN_CODE = Z.ZN_CODE";
            return map.Query<MaterialTypeEntity>(sql);
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int Delete(string code)
        {
            IMapper map = DatabaseInstance.Instance();
            return map.Execute("DELETE FROM WM_SKU_TYPE WHERE TYP_CODE = @COD", new { COD = code });
        }
    }
}
