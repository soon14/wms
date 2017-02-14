using System.Collections.Generic;
using Nodes.Dapper;
using Nodes.Entities;
using System;

namespace Nodes.DBHelper
{
    public class UnitGroupDal
    {
        protected readonly string SELECT_BODY = "SELECT  G.ID, G.UM_CODE, U.UM_NAME, SKU.SPEC, G.QTY,G.SKU_LEVEL, " +
            "G.SKU_CODE, SKU.SKU_NAME, G.SKU_BARCODE, " +
            "G.WEIGHT, G.LENGTH, G.WIDTH, G.HEIGHT, G.IS_ACTIVE FROM WM_UM_SKU G " +
            "INNER JOIN WM_SKU SKU ON G.SKU_CODE = SKU.SKU_CODE " +
            "LEFT JOIN WM_UM U ON G.UM_CODE = U.UM_CODE ";

        /// <summary>
        /// 检查编码是否已存在
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        private bool IsCodeExists(string skuCode, string umCode, string barcode)
        {
            IMapper map = DatabaseInstance.Instance();
            string id = map.ExecuteScalar<string>("select SKU_CODE from wm_um_sku where SKU_CODE = @skuCode and UM_CODE=@umCode and SKU_BARCODE=@barcode", new { skuCode = skuCode, umCode = umCode, barcode = barcode });
            return !string.IsNullOrEmpty(id);
        }

        ///<summary>
        ///查询所有计量单位组
        ///</summary>
        ///<returns></returns>
        public List<UnitGroupEntity> GetAll()
        {
            string getMultiBarcodesSql = @"SELECT A.ID, SKU_CODE, SKU_LEVEL, 
  GROUP_CONCAT(DISTINCT A.SKU_BARCODE) AS SKU_BARCODE, SKU_NAME, NAME_PY,
  SPEC, UM_CODE, UM_NAME, QTY, WEIGHT, LENGTH, WIDTH, HEIGHT, IS_ACTIVE 
  FROM (
SELECT wus.ID, wus.SKU_CODE, wus.SKU_LEVEL, wus.SKU_BARCODE, ws.SKU_NAME, ws.NAME_PY, ws.SPEC,
  wus.UM_CODE, wu.UM_NAME, wus.QTY, wus.WEIGHT, wus.LENGTH, wus.WIDTH, wus.HEIGHT, wus.IS_ACTIVE
  FROM wm_um_sku wus 
  LEFT JOIN wm_sku ws ON ws.SKU_CODE = wus.SKU_CODE
  LEFT JOIN wm_um wu ON wu.UM_CODE = wus.UM_CODE
UNION ALL 
SELECT wus.ID, wsb.SKU_CODE, wsb.SKU_LEVEL, wsb.SKU_BARCODE, ws.SKU_NAME, ws.NAME_PY, ws.SPEC,
  wsb.UM_CODE, wu.UM_NAME, wus.QTY, wus.WEIGHT, wus.LENGTH, wus.WIDTH, wus.HEIGHT, wus.IS_ACTIVE
  FROM wm_sku_barcode wsb 
  LEFT JOIN wm_um_sku wus ON wus.SKU_CODE = wsb.SKU_CODE AND wus.SKU_LEVEL = wsb.SKU_LEVEL
  LEFT JOIN wm_sku ws ON ws.SKU_CODE = wsb.SKU_CODE
  LEFT JOIN wm_um wu ON wu.UM_CODE = wsb.UM_CODE
 ) A GROUP BY A.SKU_CODE, A.SKU_LEVEL;";
            IMapper map = DatabaseInstance.Instance();
            return map.Query<UnitGroupEntity>(getMultiBarcodesSql);            
        }

        public List<UnitGroupEntity> GetAllActive()
        {
            IMapper map = DatabaseInstance.Instance();
            return map.Query<UnitGroupEntity>(SELECT_BODY + "WHERE G.IS_ACTIVE = 'Y'");
        }

        public List<UnitGroupItemEntity> GetItemsByGrpCode(string grpCode)
        {
            string sql = "SELECT G.UG_CODE, G.UM_CODE, U.UM_NAME, 1 PACK_QTY " +
                "FROM UNIT_GROUP G INNER JOIN WM_UM U ON G.UM_CODE = U.UM_CODE " +
                "WHERE G.UG_CODE = @UG_CODE " +
                "UNION ALL " +
                "SELECT I.UG_CODE, I.UM_CODE, U.UM_NAME, I.PACK_QTY " +
                "FROM UNIT_GROUP_ITEM I INNER JOIN WM_UM U ON I.UM_CODE = U.UM_CODE WHERE I.UG_CODE = @UG_CODE";
            IMapper map = DatabaseInstance.Instance();
            return map.Query<UnitGroupItemEntity>(sql, new { UG_CODE = grpCode });
        }

        /// <summary>
        /// 删除计量单位组
        /// </summary>
        /// <param name="UnitCode"></param>
        /// <returns></returns>
        public int Delete(int ID)
        {
            IMapper map = DatabaseInstance.Instance();
            return map.Execute("delete from wm_um_sku where ID = @ID", new { ID = ID });
        }

        public int Save(string UmCode, int Qty, string SkuCode, string Barcode,
            decimal Weight, decimal Length, decimal Width, decimal Height, string IsActive, int ID, bool isCreateNew)
        {
            IMapper map = DatabaseInstance.Instance();
            int sUnit = 0; //销售单位与库存单位，转换倍数大于1时为销售单位，否则为库存单位
            if (Qty > 1)
                sUnit = 1;

            int ret = -1;
            if (isCreateNew) //新增
            {
                if (IsCodeExists(SkuCode, UmCode, Barcode))
                    return -1;

                ret = map.Execute("INSERT INTO wm_um_sku "
                                + "(UM_CODE ,QTY ,SKU_CODE ,SKU_BARCODE ,WEIGHT ,LENGTH ,WIDTH ,HEIGHT ,IS_ACTIVE ,LAST_UPDATETIME, S_UNIT) "
                                + "VALUES(@umCode, @qty, @skuCode, @barcode, @weight, @length, @width, @height, @isActive, NOW(), @SUnit)",
                new
                {
                    umCode = UmCode,
                    qty = Qty,
                    skuCode = SkuCode,
                    barcode = Barcode,
                    weight = Weight,
                    length = Length,
                    width = Width,
                    height = Height,
                    isActive = IsActive,
                    SUnit = sUnit
                });
            }
            else
            {
                //查看是否在使用中，否则不允许更新

                //更新
                ret = map.Execute("UPDATE wm_um_sku SET UM_CODE = @umCode, QTY = @qty, SKU_CODE = @skuCode, " +
                    "WEIGHT = @weight, LENGTH = @length, WIDTH = @width, HEIGHT = @height, IS_ACTIVE = @isActive, " +
                    "LAST_UPDATETIME = NOW(), S_UNIT = @SUnit WHERE ID = @ID",
                new
                {
                    umCode = UmCode,
                    qty = Qty,
                    skuCode = SkuCode,
                    //barcode = Barcode,  //查出来的可能有多条码，不更新
                    weight = Weight,
                    length = Length,
                    width = Width,
                    height = Height,
                    isActive = IsActive,
                    SUnit = sUnit,
                    ID = ID
                });
            }

            return ret;
        }

        public int SaveItem(string grpCode, string unitCode, decimal packQty)
        {
            //先检查是否重复
            IMapper map = DatabaseInstance.Instance();
            string gCode = map.ExecuteScalar<string>("SELECT UG_CODE FROM UNIT_GROUP_ITEM WHERE UG_CODE = @UG_CODE AND UM_CODE = @UM_CODE",
                new { UG_CODE = grpCode, UM_CODE = unitCode });
            if (!string.IsNullOrEmpty(gCode))
                return -1;

            return map.Execute("INSERT INTO UNIT_GROUP_ITEM(UG_CODE, UM_CODE, PACK_QTY) VALUES(@UG_CODE, @UM_CODE, @PACK_QTY)",
                        new { UG_CODE = grpCode, UM_CODE = unitCode, PACK_QTY = packQty });
        }
    }
}
