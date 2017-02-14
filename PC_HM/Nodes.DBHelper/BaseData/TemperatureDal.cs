using System;
using System.Collections.Generic;
using Nodes.Dapper;
using Nodes.Entities;

namespace Nodes.DBHelper
{
    public class TemperatureDal
    {
        /// <summary>
        /// 检查编码是否已存在
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        private bool IsCodeExists(TemperatureEntity entity)
        {
            IMapper map = DatabaseInstance.Instance();
            string id = map.ExecuteScalar<string>("select TEMP_CODE from WM_TEMPERATURE where TEMP_CODE = @COD",
            new { COD = entity.TemperatureCode });
            if (!string.IsNullOrEmpty(id)) return true;

            return false;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operatorFlag">添加或编辑</param>
        /// <returns></returns>
        public int Save(TemperatureEntity entity, bool isNew)
        {
            IMapper map = DatabaseInstance.Instance();
            int ret = -2;
            if (isNew)
            {
                //检查编号是否已经存在
                if (IsCodeExists(entity))
                    return -1;
                ret = map.Execute("insert into WM_TEMPERATURE(TEMP_CODE, TEMP_NAME, ALLOW_EDIT, LOWER_LIMIT, UPPER_LIMIT) " +
                    "values(@COD, @NAM, @ALLOW_EDIT, @LOWER_LIMIT, @UPPER_LIMIT)",
                new
                {
                    COD = entity.TemperatureCode,
                    NAM = entity.TemperatureName,
                    ALLOW_EDIT = entity.AllowEdit,
                    LOWER_LIMIT = entity.LowerLimit,
                    UPPER_LIMIT = entity.UpperLimit
                });
            }
            else
            {
                //更新
                ret = map.Execute("update WM_TEMPERATURE set TEMP_NAME = @NAM, ALLOW_EDIT = @ALLOW_EDIT, LOWER_LIMIT = @LOWER_LIMIT, UPPER_LIMIT = @UPPER_LIMIT where TEMP_CODE = @COD",
                new
                {
                    COD = entity.TemperatureCode,
                    NAM = entity.TemperatureName,
                    ALLOW_EDIT = entity.AllowEdit,
                    LOWER_LIMIT = entity.LowerLimit,
                    UPPER_LIMIT = entity.UpperLimit
                });
            }
            return ret;
        }

        ///<summary>
        ///查询所有
        ///</summary>
        ///<returns></returns>
        public List<TemperatureEntity> GetAll()
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT TEMP_CODE, TEMP_NAME, ALLOW_EDIT, LOWER_LIMIT, UPPER_LIMIT FROM WM_TEMPERATURE";
            return map.Query<TemperatureEntity>(sql);
        }

        public List<UnitGroupEntity> GetUmName(string sku_code)
        { 
            string sql=string.Format ("SELECT wus.ID, wus.SKU_CODE, wus.SKU_LEVEL, wus.SKU_BARCODE, ws.SKU_NAME, ws.NAME_PY, ws.SPEC, " +
                      "wus.UM_CODE, wu.UM_NAME, wus.QTY, wus.WEIGHT, wus.LENGTH, wus.WIDTH, wus.HEIGHT, wus.IS_ACTIVE " +
                      "FROM wm_um_sku wus  " +
                      "LEFT JOIN wm_sku ws ON ws.SKU_CODE = wus.SKU_CODE " +
                      "LEFT JOIN wm_um wu ON wu.UM_CODE = wus.UM_CODE " +
                      "WHERE wus.SKU_CODE='{0}'",sku_code);
            IMapper map = DatabaseInstance.Instance();
            return map.Query<UnitGroupEntity>(sql);
        }

        /// <summary>
        /// 删除一行
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int DeleteOne(string code)
        {
            IMapper map = DatabaseInstance.Instance();

            //查看货位是否在引用中
            string sql = "SELECT ZN_CODE FROM WM_ZONE WHERE TEMP_CODE = @COD";
            string znCode = map.ExecuteScalar<string>(sql, new { COD = code });
            if (!string.IsNullOrEmpty(znCode))
                return -1;

            return map.Execute("delete from WM_TEMPERATURE where TEMP_CODE = @COD", new { COD = code });
        }
    }
}
