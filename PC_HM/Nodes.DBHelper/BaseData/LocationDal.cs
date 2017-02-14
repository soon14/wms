using System;
using System.Collections.Generic;
using Nodes.Dapper;
using Nodes.Entities;

namespace Nodes.DBHelper
{
    public class LocationDal
    {
        protected readonly string SELECT_BODY = "SELECT L.LC_CODE, L.LC_NAME, L.ZN_CODE, " +
                "L.PASSAGE_CODE, L.FLOOR_CODE, L.SHELF_CODE, L.CELL_CODE, L.SORT_ORDER, " +
                "P.ZN_NAME, W.WH_CODE, W.WH_NAME, " +
                "L.IS_ACTIVE, L.LOWER_SIZE, L.UPPER_SIZE " +
                "FROM WM_LOCATION L " +
                "INNER JOIN WM_ZONE P ON P.ZN_CODE = L.ZN_CODE " +
                "INNER JOIN WM_WAREHOUSE W ON P.WH_CODE = W.WH_CODE";

        /// <summary>
        /// 检查货位编码是否已存在
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        private bool IsLocationCodeExists(LocationEntity Location)
        {
            IMapper map = DatabaseInstance.Instance();
            string id = map.ExecuteScalar<string>("select LC_CODE from WM_LOCATION where LC_CODE = @COD",
            new { COD = Location.LocationCode });

            return !string.IsNullOrEmpty(id);
        }

        /// <summary>
        /// 添加或编辑货位
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operatorFlag">添加或编辑</param>
        /// <returns>1：成功；-2：已被删除；-1：编号已存在；-3：货位使用中，不允许修改</returns>
        public int Save(LocationEntity entity, bool isNew)
        {
            IMapper map = DatabaseInstance.Instance();
            int ret = -2;

            if (isNew)
            {
                //检查编号是否已经存在
                if (IsLocationCodeExists(entity))
                    return -1;

                ret = map.Execute("insert into WM_LOCATION(LC_CODE, LC_NAME, ZN_CODE, PASSAGE_CODE, " +
                    "FLOOR_CODE, SHELF_CODE, CELL_CODE, SORT_ORDER, WH_CODE, IS_ACTIVE, " +
                    "LOWER_SIZE, UPPER_SIZE, UG_CODE, UM_CODE) " +
                    "values(@COD, @NAM, @ZONE_CODE, @PASSAGE_CODE, @FLOOR_CODE, @SHELF_CODE, " +
                    "@CELL_CODE, @SORT_ORDER, @WH_CODE, @IS_ACTIVE, @LOWER_SIZE, @UPPER_SIZE, @UG_CODE, @UM_CODE)",
                new
                {
                    COD = entity.LocationCode,
                    NAM = entity.LocationName,
                    ZONE_CODE = entity.ZoneCode,
                    PASSAGE_CODE = entity.PassageCode,
                    FLOOR_CODE = entity.FloorCode,
                    SHELF_CODE = entity.ShelfCode,
                    CELL_CODE = entity.CellCode,
                    SORT_ORDER = entity.SortOrder,
                    WH_CODE = entity.WarehouseCode,
                    IS_ACTIVE = entity.IsActive,
                    LOWER_SIZE = entity.LowerSize,
                    UPPER_SIZE = entity.UpperSize,
                    UG_CODE = entity.GrpCode,
                    UM_CODE = entity.UnitCode
                });
            }
            else
            {
                //bool isUsing = new StockDal().IsLocationUsing(entity.LocationCode);
                //if (isUsing) return -3;

                //更新
                ret = map.Execute("update WM_LOCATION set LC_NAME = @NAM, ZN_CODE = @ZONE_CODE, PASSAGE_CODE = @PASSAGE_CODE, " +
                    "FLOOR_CODE = @FLOOR_CODE, SHELF_CODE = @SHELF_CODE, CELL_CODE = @CELL_CODE, SORT_ORDER = @SORT_ORDER, IS_ACTIVE = @IS_ACTIVE, " +
                    "LOWER_SIZE = @LOWER_SIZE, UPPER_SIZE = @UPPER_SIZE, UG_CODE = @UG_CODE, UM_CODE = @UM_CODE where LC_CODE = @COD",
                new
                {
                    NAM = entity.LocationName,
                    ZONE_CODE = entity.ZoneCode,
                    PASSAGE_CODE = entity.PassageCode,
                    FLOOR_CODE = entity.FloorCode,
                    SHELF_CODE = entity.ShelfCode,
                    CELL_CODE = entity.CellCode,
                    SORT_ORDER = entity.SortOrder,
                    IS_ACTIVE = entity.IsActive,
                    LOWER_SIZE = entity.LowerSize,
                    UPPER_SIZE = entity.UpperSize,
                    UM_CODE = entity.UnitCode,
                    UG_CODE = entity.GrpCode,
                    COD = entity.LocationCode
                });
            }

            return ret;
        }

        ///<summary>
        ///查询所有货位
        ///</summary>
        ///<returns></returns>
        public List<LocationEntity> GetAllLocation()
        {
            IMapper map = DatabaseInstance.Instance();
            return map.Query<LocationEntity>(SELECT_BODY);
        }

        public LocationEntity FindLocationByCode(string code)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = SELECT_BODY + " WHERE L.LC_CODE = @CODE";
            return map.QuerySingle<LocationEntity>(sql, new { CODE = code });
        }

        ///<summary>
        ///根据所选货区查询所有货位
        ///</summary>
        ///<returns></returns>
        public List<LocationEntity> GetAllLocationByZone(string zoneCode)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = SELECT_BODY + " WHERE P.ZN_CODE = @COD";
            return map.Query<LocationEntity>(sql, new { COD = zoneCode });
        }

        /// <summary>
        /// 获取存储区货位（获取未完成并且上传未执行的货位 彭伟 2015-11-04）
        /// </summary>
        /// <param name="warehouse"></param>
        /// <returns></returns>
        public List<LocationEntity> GetStockLocation()
        {
            IMapper map = DatabaseInstance.Instance();
            //string sql = "SELECT L.LC_CODE, L.LC_NAME, L.ZN_CODE, P.ZN_NAME FROM WM_LOCATION L " +
            //    "INNER JOIN WM_ZONE P ON P.ZN_CODE = L.ZN_CODE WHERE L.IS_ACTIVE = 'Y'";
            string sql = @"SELECT L.LC_CODE, L.LC_NAME, L.ZN_CODE, P.ZN_NAME, A.BILL_ID, A.BILL_STATE
  FROM WM_LOCATION L
  INNER JOIN WM_ZONE P ON P.ZN_CODE = L.ZN_CODE 
  LEFT JOIN (SELECT H.BILL_ID, (CASE WHEN C.ITEM_DESC IS NULL THEN H.BILL_STATE ELSE C.ITEM_DESC END) BILL_STATE, L.LC_CODE 
              FROM WM_COUNT_HEADER H
              LEFT JOIN WM_COUNT_LOCATION L ON L.BILL_ID = H.BILL_ID
              LEFT JOIN WM_BASE_CODE C ON C.ITEM_VALUE = H.BILL_STATE
    WHERE H.BILL_STATE IN ('130', '131', '等待差异调整')) A ON A.LC_CODE = L.LC_CODE
  WHERE L.IS_ACTIVE = 'Y'
  ORDER BY A.BILL_STATE";
            return map.Query<LocationEntity>(sql);
        }

        /// <summary>
        /// 删除货位
        /// </summary>
        /// <param name="StockLocationCode"></param>
        /// <returns></returns>
        public bool DeleteLocation(string LocationCode)
        {
            //bool isUsing = new StockDal().IsLocationUsing(LocationCode);
            //if (isUsing) return false;

            IMapper map = DatabaseInstance.Instance();
            map.Execute("delete from WM_LOCATION where LC_CODE = @COD", new { COD = LocationCode });
            return true;
        }

        public long JudgeStock(string lc_code)
        {
            string sql = "SELECT COUNT(1) FROM wm_stock WHERE LC_CODE =@LC_CODE  AND QTY > 0";
            IMapper map = DatabaseInstance.Instance();
            return map.ExecuteScalar<long>(sql, new { LC_CODE = lc_code });
        }
    }
}
