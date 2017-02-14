using System;
using System.Collections.Generic;
using Nodes.Dapper;
using Nodes.Entities;
using System.Data;

namespace Nodes.DBHelper
{
    public class MaterialDal
    {
        /// <summary>
        /// 连接了WM_MATERIAL_TYPE，UNIT_GROUP，WM_UM，WM_TEMPERATURE四张表
        /// </summary>
        private const string SELECT_MATERIAL_BODY =
            "SELECT M.SKU_ID, M.SKU_CODE, M.SKU_NAME, M.SKU_NAME_S, M.NAME_PY, M.TYP_CODE, TYP.TYP_NAME, " +
  "M.BRD_CODE, BR.BRD_NAME, M.BARCODE1, M.BARCODE2, M.EXP_DAYS, M.SPEC, M.MIN_STOCK_QTY, M.MAX_STOCK_QTY,  " +
  "M.PICK_TYPE, M.TEMP_CODE, TMP.TEMP_NAME, M.IS_ACTIVE,   " +
  "M.SORT_ORDER, M.IS_OWN, M.REMARK, M.UPDATE_BY, M.UPDATE_DATE    " +
  "FROM WM_SKU M  " +
  "LEFT JOIN BRANDS BR ON BR.BRD_CODE = M.BRD_CODE   " +
  "LEFT JOIN WM_SKU_TYPE TYP ON TYP.TYP_CODE = M.TYP_CODE   " +
  "LEFT JOIN WM_TEMPERATURE TMP ON TMP.TEMP_CODE = M.TEMP_CODE";

        /// <summary>
        /// 检查物料编码是否已存在
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        private bool IsMaterialCodeExists(MaterialEntity Material)
        {
            IMapper map = DatabaseInstance.Instance();
            string id = map.ExecuteScalar<string>("SELECT MTL_CODE FROM WM_MATERIALS WHERE MTL_CODE = @COD",
            new { COD = Material.MaterialCode });

            return !string.IsNullOrEmpty(id);
        }

        /// <summary>
        /// 添加或编辑物料
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isNew">添加或编辑</param>
        /// <returns>1:成功；-1：物料编号已存在；-2：已关联其它供应商，无法置为空；-3：该关联已存在</returns>
        public int Save(MaterialEntity entity, string umcode, int safeQty, bool isNew)
        {
            IMapper map = DatabaseInstance.Instance();
            int ret = 0;

            if (isNew)
            {
                //检查编号是否已经存在
                if (IsMaterialCodeExists(entity))
                    return -1;

                ret = map.Execute(
                    string.Format("INSERT INTO WM_MATERIALS(MTL_CODE, MTL_NAME, MTL_NAME_S, NAME_PY, TYP_CODE, UG_CODE, UM_CODE, SUP_CODE, " +
                    "SPEC, BARCODE, PRICE, MAX_STOCK_QTY, MIN_STOCK_QTY, TEMP_CODE, IS_ACTIVE, " +
                    "MTL_STR1, MTL_STR2, MTL_STR3, MTL_STR4, MTL_NUM1, MTL_NUM2, MTL_DATE1, MTL_DATE2, " +
                    "SORT_ORDER, IS_OWN, REMARK, UPDATE_BY, UPDATE_DATE) " +
                    "VALUES(@MTL_CODE, @MTL_NAME, @MTL_NAME_S, @NAME_PY, @TYP_CODE, @UG_CODE, @UM_CODE, @SUP_CODE, " +
                    "@SPEC, @BARCODE, @PRICE, @MAX_STOCK_QTY, @MIN_STOCK_QTY, @TEMP_CODE, @IS_ACTIVE, " +
                    "@MTL_STR1, @MTL_STR2, @MTL_STR3, @MTL_STR4, @MTL_NUM1, @MTL_NUM2, @MTL_DATE1, @MTL_DATE2, " +
                    "@SORT_ORDER, @IS_OWN, @REMARK, @UPDATE_BY, {0})", map.GetSysDateString()),
                new
                {
                    MTL_CODE = entity.MaterialCode,
                    MTL_NAME = entity.MaterialName,
                    MTL_NAME_S = entity.MaterialNameS,
                    NAME_PY = entity.MaterialNamePY,
                    TYP_CODE = entity.MaterialTypeCode,

                    UG_CODE = entity.UnitGrpCode,
                    UM_CODE = entity.UnitCode,
                    SUP_CODE = entity.SupplierCode,
                    //SPEC = entity.BrandCode,
                    //BARCODE = entity.Barcode1,

                    //PRICE = entity.Price,
                    MAX_STOCK_QTY = entity.MaxStockQty,
                    MIN_STOCK_QTY = entity.MinStockQty,
                    TEMP_CODE = entity.TemperatureCode,
                    IS_ACTIVE = entity.IsActive,

                    SORT_ORDER = entity.SortOrder,
                    IS_OWN = entity.IsActive,
                    REMARK = entity.Remark,
                    UPDATE_BY = entity.LastUpdateBy
                });
            }
            else
            {
                ////检查默认供应商：若已关联其它供应商，不能置为空，并且不能与已关联的重复
                //ret = IsSupplierValid(entity.MaterialCode, entity.SupplierCode);
                //if (ret == -1)
                //    return -2;
                //else if (ret == -2)
                //    return -3;
                //else
                //{
                string sql = string.Format("SELECT {0}*wus.QTY qty  FROM  wm_um_sku wus " +
                               " WHERE wus.SKU_CODE ='{1}'AND wus.UM_CODE='{2}'", safeQty, entity.MaterialCode, umcode);
                int qty = Convert.ToInt32(map.ExecuteScalar<Object>(sql));

                sql = "UPDATE wm_sku ws " +
                    "SET ws.SECURITY_QTY = @SECURITY_QTY, " +
                    "ws.TEMP_CODE = @TEMP_CODE, " +
                    "ws.MIN_STOCK_QTY = @MIN_STOCK_QTY, " +
                    "ws.MAX_STOCK_QTY = @MAX_STOCK_QTY, " +
                    "ws.UPDATE_BY = @UPDATE_BY, " +
                    "ws.UPDATE_DATE = @UPDATE_DATE, " +
                    "ws.SKU_TYPE = @SKU_TYPE " +
                    "WHERE ws.SKU_CODE = @SKU_CODE;";
                ret = map.Execute(sql, new
                {
                    SECURITY_QTY = qty,
                    TEMP_CODE = entity.TemperatureCode,
                    MIN_STOCK_QTY = entity.MinStockQty,
                    MAX_STOCK_QTY = entity.MaxStockQty,
                    UPDATE_BY = entity.LastUpdateBy,
                    UPDATE_DATE = entity.LastUpdateDate,
                    SKU_TYPE = entity.SkuType,
                    SKU_CODE = entity.MaterialCode
                });
                //}
            }

            return ret;
        }

        ///<summary>
        ///查询所有物料，用于物料维护，如果是填充其他界面，请调用GetActiveMaterials()函数
        ///</summary>
        ///<returns></returns>
        public List<MaterialEntity> GetAll()
        {
            IMapper map = DatabaseInstance.Instance();
            //return map.Query<MaterialEntity>(string.Format("{0} WHERE SW.WH_CODE = '{1}'", SELECT_MATERIAL_BODY, warehouse));
            string sql = @"SELECT A.SKU_CODE, A.SKU_NAME, b.BRD_NAME, wst.TYP_NAME, A.SPEC, A.EXP_DAYS, wt.TEMP_CODE, wt.TEMP_NAME, 
  MIN_STOCK_QTY, MAX_STOCK_QTY, SECURITY_QTY, A.LOWER_LOCATION, A.UPPER_LOCATION,A.SKU_TYPE,C.ITEM_DESC,
  SUM(IFNULL(S.QTY, 0)) TOTAL_STOCK_QTY 
  FROM WM_SKU A 
  LEFT JOIN brands b ON b.BRD_CODE = A.BRD_CODE 
  LEFT JOIN wm_sku_type wst ON wst.TYP_CODE = A.TYP_CODE 
  LEFT JOIN wm_temperature wt ON wt.TEMP_CODE = A.TEMP_CODE 
  LEFT JOIN WM_BASE_CODE C ON A.SKU_TYPE = C.ITEM_VALUE
  LEFT JOIN WM_STOCK S ON S.SKU_CODE = A.SKU_CODE
  GROUP BY A.SKU_CODE;";
            return map.Query<MaterialEntity>(sql);
        }
        public List<MaterialEntity> GetLocalAll()
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = @"SELECT A.SKU_CODE, A.SKU_NAME, b.BRD_NAME, wst.TYP_NAME, A.SPEC, A.EXP_DAYS, wt.TEMP_CODE, wt.TEMP_NAME, 
  MIN_STOCK_QTY, MAX_STOCK_QTY, SECURITY_QTY, A.LOWER_LOCATION, A.UPPER_LOCATION,A.SKU_TYPE,C.ITEM_DESC,
  SUM(IFNULL(S.QTY, 0)) TOTAL_STOCK_QTY 
  FROM WM_SKU A 
  LEFT JOIN brands b ON b.BRD_CODE = A.BRD_CODE 
  LEFT JOIN wm_sku_type wst ON wst.TYP_CODE = A.TYP_CODE 
  LEFT JOIN wm_temperature wt ON wt.TEMP_CODE = A.TEMP_CODE 
  LEFT JOIN WM_BASE_CODE C ON A.SKU_TYPE = C.ITEM_VALUE
  INNER JOIN WM_STOCK S ON S.SKU_CODE = A.SKU_CODE
  GROUP BY A.SKU_CODE;";
            return map.Query<MaterialEntity>(sql);
        }

        /// <summary>
        /// 根据编码查找某个物料
        /// </summary>
        /// <param name="materialCode"></param>
        /// <returns></returns>
        public MaterialEntity FindMaterialByCode(string materialCode)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = SELECT_MATERIAL_BODY + "where M.MTL_CODE = @MaterialCode";
            return map.QuerySingle<MaterialEntity>(sql, new { MaterialCode = materialCode });
        }

        #region 处理关联供应商

        /// <summary>
        /// 查看设置的默认供应商是否有效
        /// 若已关联其它供应商，不能置为空，并且不能与已关联的重复
        /// </summary>
        /// <param name="materialCode"></param>
        /// <param name="supplierCode"></param>
        /// <returns>0:有效，可以使用；-1：已关联其它供应商，无法置为空；-2：该关联已存在</returns>
        public int IsSupplierValid(string materialCode, string supplierCode)
        {

            //若已关联其它供应商，不能置为空
            if (string.IsNullOrEmpty(supplierCode))
            {
                IMapper map = DatabaseInstance.Instance();
                if (!string.IsNullOrEmpty(
                    map.ExecuteScalar<string>("SELECT MS.SUP_CODE FROM WM_MATERIAL_SUPPLIER MS WHERE MS.MTL_CODE = @MATERIAL", new { MATERIAL = materialCode })))
                    return -1;
                else
                    return 0;
            }
            else //若不为空，需要查看是否已关联
            {
                if (IsRelationExists(materialCode, supplierCode))
                    return -2;
                else
                    return 0;
            }
        }

        public List<SupplierEntity> ListRelationSuppliers(string materialCode)
        {
            IMapper map = DatabaseInstance.Instance();
            return map.Query<SupplierEntity>("SELECT MS.SUP_CODE, S.SUP_NAME FROM WM_MATERIAL_SUPPLIER MS INNER JOIN SUPPLIERS S ON MS.SUP_CODE = S.SUP_CODE WHERE MS.MTL_CODE = @MATERIAL",
                new { MATERIAL = materialCode });
        }

        public void CreateRelationWithSupplier(string materialCode, string defaultSupplier, List<SupplierEntity> suppliers)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "INSERT INTO WM_MATERIAL_SUPPLIER(MTL_CODE, SUP_CODE) VALUES(@MATERIAL, @SUPPLIER)";
            DynamicParameters parms = new DynamicParameters();
            parms.Add("MATERIAL", materialCode);
            parms.Add("SUPPLIER");

            foreach (SupplierEntity supplier in suppliers)
            {
                //先查看是否已建立关联，略过，也要排除默认供应商
                if (supplier.SupplierCode != defaultSupplier && !IsRelationExists(materialCode, supplier.SupplierCode))
                {
                    parms.Set("SUPPLIER", supplier.SupplierCode);
                    map.Execute(sql, parms);
                }
            }
        }

        private bool IsRelationExists(string materialCode, string supplierCode)
        {
            IMapper map = DatabaseInstance.Instance();
            string code = map.ExecuteScalar<string>("SELECT MTL_CODE FROM WM_MATERIAL_SUPPLIER WHERE MTL_CODE = @MATERIAL AND SUP_CODE = @SUPPLIER",
                new { MATERIAL = materialCode, SUPPLIER = supplierCode });

            return !string.IsNullOrEmpty(code);
        }

        public int DeleteRelationSupplier(string materialCode, string supplierCode)
        {
            IMapper map = DatabaseInstance.Instance();
            return map.Execute("DELETE FROM WM_MATERIAL_SUPPLIER WHERE MTL_CODE = @MATERIAL AND SUP_CODE = @SUPPLIER",
                new { MATERIAL = materialCode, SUPPLIER = supplierCode });
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="materialCode"></param>
        /// <param name="materialNameOrPY"></param>
        /// <param name="materialTypeCode"></param>
        /// <param name="supplier"></param>
        /// <param name="brand"></param>
        /// <param name="spec"></param>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public List<MaterialEntity> QueryMaterials(string materialCode, string materialNameOrPY, string materialTypeCode,
            string supplier, string spec, string barcode, string str1)
        {
            IMapper map = DatabaseInstance.Instance();
            return map.Query<MaterialEntity>("P_MTL_QUERY_ACTIVE",
                new
                {
                    P_MTL_CODE = materialCode,
                    P_MTL_NAME_OR_PY = materialNameOrPY,
                    P_TYP_CODE = materialTypeCode,
                    P_SUP_CODE = supplier,
                    P_SPEC = spec,
                    P_BARCODE = barcode,
                    P_MTL_STR1 = str1
                },
                true,
                CommandType.StoredProcedure);
        }

        public List<MaterialEntity> GetActiveMaterials()
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = SELECT_MATERIAL_BODY + "WHERE M.IS_ACTIVE = 'Y' ORDER BY M.SORT_ORDER DESC";
            return map.Query<MaterialEntity>(sql);
        }

        /// <summary>
        /// 删除物料
        /// </summary>
        /// <param name="StockMaterialCode"></param>
        /// <returns></returns>
        public bool DeleteMaterial(string MaterialCode)
        {
            IMapper map = DatabaseInstance.Instance();
            map.Execute("delete from WM_MATERIALS where MTL_CODE = @MTL_CODE", new { MTL_CODE = MaterialCode });
            return true;
        }
    }
}