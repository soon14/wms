using System;
using System.Collections.Generic;
using Nodes.Dapper;
using Nodes.Entities;

namespace Nodes.DBHelper
{
    public class ZoneDal
    {
       /// <summary>
       /// 检查货区编码是否已存在
       /// </summary>
       /// <param name="dept"></param>
       /// <returns></returns>
       private bool IsZoneCodeExists(ZoneEntity zone)
       {
           IMapper map = DatabaseInstance.Instance();
           string id = map.ExecuteScalar<string>("select ZN_CODE from WM_ZONE where ZN_CODE = @COD",
           new { COD = zone.ZoneCode });

           return !string.IsNullOrEmpty(id);
       }

       /// <summary>
       /// 添加或编辑货区
       /// </summary>
       /// <param name="entity"></param>
       /// <param name="operatorFlag">添加或编辑</param>
       /// <returns></returns>
       public int Save(ZoneEntity entity, bool isNew)
       {
           IMapper map = DatabaseInstance.Instance();
           int ret = -2;
           
           if (isNew)
           {
               //检查编号是否已经存在
               if (IsZoneCodeExists(entity))
                   return -1;
               ret = map.Execute("INSERT INTO WM_ZONE(ZN_CODE, ZN_NAME, ZT_CODE, WH_CODE, TEMP_CODE, IS_ACTIVE) " +
                   "VALUES(@COD, @NAM, @ZT_CODE, @WH_CODE, @TEMP_CODE, @IS_ACTIVE)",
               new
               {
                   COD = entity.ZoneCode,
                   NAM = entity.ZoneName,
                   ZT_CODE = entity.ZoneTypeCode,
                   WH_CODE = entity.WarehouseCode,
                   TEMP_CODE = entity.TemperatureCode,
                   IS_ACTIVE = entity.IsActive
               });
           }
           else
           {
               //更新
               ret = map.Execute("update WM_ZONE set ZN_NAME = @NAM, ZT_CODE = @ZT_CODE, WH_CODE = @WH_CODE, TEMP_CODE = @TEMP_CODE, IS_ACTIVE = @IS_ACTIVE where ZN_CODE = @COD",
               new
               {
                   COD = entity.ZoneCode,
                   NAM = entity.ZoneName,
                   ZT_CODE = entity.ZoneTypeCode,
                   WH_CODE = entity.WarehouseCode,
                   TEMP_CODE = entity.TemperatureCode,
                   IS_ACTIVE = entity.IsActive
               });
           }

           return ret;
       }

       ///<summary>
       ///查询所有货区
       ///</summary>
       ///<returns></returns>
       public List<ZoneEntity> GetAllZone()
       {
           IMapper map = DatabaseInstance.Instance();
           string sql = "SELECT P.ZN_CODE, P.ZN_NAME, P.WH_CODE, W.WH_NAME, " +
               "P.TEMP_CODE, T.TEMP_NAME, P.ZT_CODE, B.ITEM_DESC ZT_NAME, P.IS_ACTIVE FROM WM_ZONE P " +
               "INNER JOIN WM_WAREHOUSE W ON W.WH_CODE = P.WH_CODE " +
               "INNER JOIN WM_TEMPERATURE T ON P.TEMP_CODE = T.TEMP_CODE " +
               "INNER JOIN WM_BASE_CODE B ON B.GROUP_CODE = '101' AND B.ITEM_VALUE = P.ZT_CODE";
           return map.Query<ZoneEntity>(sql);
       }

       ///<summary>
       ///根据仓库查询所有货区和货位
       ///</summary>
       ///<returns></returns>
       public List<ZoneEntity> GetZoneByWarehouseCode(string warehouseCode)
       {
           IMapper map = DatabaseInstance.Instance();
           string sql = "SELECT P.ZN_CODE, P.ZN_NAME FROM WM_ZONE P " +
               "INNER join WM_WAREHOUSE W ON W.WH_CODE = P.WH_CODE where W.WH_CODE = @COD";
           return map.Query<ZoneEntity>(sql, new { COD = warehouseCode });
       }

       /// <summary>
       /// 删除货区
       /// </summary>
       /// <param name="StockZoneCode"></param>
       /// <returns></returns>
       public bool DeleteZone(string zoneCode)
       {
           IMapper map = DatabaseInstance.Instance();
           string locCode = map.ExecuteScalar<string>("SELECT L.LC_CODE FROM WM_LOCATION L inner join WM_ZONE Z ON Z.ZN_CODE = L.ZN_CODE where Z.ZN_CODE = @COD",
           new { COD = zoneCode });
           if (!string.IsNullOrEmpty(locCode)) return false;

           map.Execute("delete from WM_ZONE where ZN_CODE = @COD", new { COD = zoneCode });
           return true;
       }   
    }
}
