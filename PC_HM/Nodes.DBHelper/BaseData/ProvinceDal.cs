using System;
using System.Collections.Generic;
using Nodes.Dapper;
using Nodes.Entities;

namespace Nodes.DBHelper
{
    public class ProvinceDal
    {

        /// <summary>
        /// 检查省份编码是否已存在
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        private bool IsProvinceCodeExists(ProvinceEntity Province)
        {
            IMapper map = DatabaseInstance.Instance();
            string id = map.ExecuteScalar<string>("select PROV_CODE from PROVINCES where PROV_CODE = @COD",
            new { COD = Province.ProvinceCode });
            return !string.IsNullOrEmpty(id);
        }

        /// <summary>
        /// 添加或编辑省份
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operatorFlag">添加或编辑</param>
        /// <returns></returns>
        public int Save(ProvinceEntity entity, bool isNew)
        {
            IMapper map = DatabaseInstance.Instance();
            int ret = -2;
            if (isNew)
            {
                //检查编号是否已经存在
                if (IsProvinceCodeExists(entity))
                    return -1;
                ret = map.Execute("insert into PROVINCES(PROV_CODE, PROV_NAME, ALIASNAME, AREACODE, CAPITAL, NAME_PY) " +
                    "values(@Code, @Name, @AliasName, @AreaCode, @Capital, @NamePY)",
                new
                {
                    Code = entity.ProvinceCode,
                    Name = entity.ProvinceName,
                    AliasName = entity.AliasName,
                    AreaCode = entity.AreaCode,
                    Capital = entity.Capital,
                    NamePY = entity.NamePY,
                });
            }
            else
            {
                //更新
                ret = map.Execute("update PROVINCES set PROV_NAME = @Name, ALIASNAME = @AliasName, AREACODE = @AreaCode, CAPITAL = @Capital, NAME_PY = @NamePY where PROV_CODE = @COD",
                new
                {
                    Name = entity.ProvinceName,
                    AliasName = entity.AliasName,
                    AreaCode = entity.AreaCode,
                    Capital = entity.Capital,
                    NamePY = entity.NamePY,
                    COD = entity.ProvinceCode
                });
            }
            return ret;
        }

        ///<summary>
        ///查询所有省份
        ///</summary>
        ///<returns></returns>
        public List<ProvinceEntity> GetAllProvince()
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT PROV_CODE, PROV_NAME, ALIASNAME, AREACODE, CAPITAL, NAME_PY FROM PROVINCES ORDER BY PROV_NAME ASC";
            return map.Query<ProvinceEntity>(sql);
        }

        /// <summary>
        /// 删除省份
        /// </summary>
        /// <param name="StockAreaCode"></param>
        /// <returns></returns>
        public int DeleteProvince(string ProvinceCode)
        {
            IMapper map = DatabaseInstance.Instance();
            return map.Execute("delete from PROVINCES where PROV_CODE = @COD", new { COD = ProvinceCode });
        }
    }
}