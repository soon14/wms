using System.Collections.Generic;
using Nodes.Dapper;
using Nodes.Entities;

namespace Nodes.DBHelper
{
    public class SkuWarehouseDal
    {
        ///<summary>
        ///查询所有
        ///</summary>
        ///<returns></returns>
        public List<SkuWarehouseEntity> GetAllSkuWarehouse()
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = @"SELECT A.SKU_CODE ,A.SKU_NAME,A.SPEC,A.MIN_STOCK_QTY,A.MAX_STOCK_QTY, " +
      "A.LOWER_LOCATION,A.UPPER_LOCATION,A.PICK_TYPE, A.SECURITY_QTY " +
      "FROM WM_SKU A ";
            return map.Query<SkuWarehouseEntity>(sql);
        }

        public int Save(SkuWarehouseEntity entity, bool isCreateNew)
        {
            IMapper map = DatabaseInstance.Instance();
            //int sUnit = 0; //销售单位与库存单位，转换倍数大于1时为销售单位，否则为库存单位
            //if (Qty > 1)
            //    sUnit = 1;

            int ret = -1;
            if (isCreateNew) //新增
            {
                //if (IsCodeExists(SkuCode, UmCode, Barcode))
                //    return -1;

                //ret = map.Execute("INSERT INTO wm_um_sku "
                //                + "(UM_CODE ,QTY ,SKU_CODE ,SKU_BARCODE ,WEIGHT ,LENGTH ,WIDTH ,HEIGHT ,IS_ACTIVE ,LAST_UPDATETIME, S_UNIT) "
                //                + "VALUES(@umCode, @qty, @skuCode, @barcode, @weight, @length, @width, @height, @isActive, NOW(), @SUnit)",
                //new
                //{
                //    umCode = UmCode,
                //    qty = Qty,
                //    skuCode = SkuCode,
                //    barcode = Barcode,
                //    weight = Weight,
                //    length = Length,
                //    width = Width,
                //    height = Height,
                //    isActive = IsActive,
                //    SUnit = sUnit
                //});
            }
            else
            {
                //查看是否在使用中，否则不允许更新

                //更新【如果允许WMS修改物料信息需要修改操作的表为wm_sku】
                ret = map.Execute("UPDATE wm_sku_warehouse wsw SET wsw.MIN_STOCK_QTY=@minStockQty,wsw.MAX_STOCK_QTY=@maxStockQty,wsw.SECURITY_QTY=@safeQty WHERE wsw.ID=@ID ",
                new
                {
                    spec = entity.Spec,
                    minStockQty = entity.MinStockQty,
                    maxStockQty = entity.MaxStockQty,
                    safeQty = entity.SecurityQty,
                    ID = entity.SkuWarehouseID
                });
            }

            return ret;
        }
    }
}
