using System.Collections.Generic;
using Nodes.Dapper;
using Nodes.Entities;

namespace Nodes.DBHelper
{
    public class BrandDal
    {
        /// <summary>
        /// 检查品牌编码是否已存在
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        private bool IsCodeExists(BrandEntity brands)
        {
            IMapper map = DatabaseInstance.Instance();
            string id = map.ExecuteScalar<string>("SELECT BRD_CODE FROM BRANDS WHERE BRD_CODE = @COD", new { COD = brands.BrandCode });
            return !string.IsNullOrEmpty(id);
        }

        /// <summary>
        /// 获取所有的品牌
        /// </summary>
        public List<BrandEntity> GetAllBrands()
        {
            IMapper map = DatabaseInstance.Instance();
            return map.Query<BrandEntity>("SELECT BRD_CODE,BRD_NAME FROM BRANDS");
        }

        /// <summary>
        /// 添加或编辑品牌
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isNew">添加或编辑</param>
        /// <returns></returns>
        public int Save(BrandEntity brands, bool isNew)
        {
            IMapper map = DatabaseInstance.Instance();
            int ret = -2;
            if (isNew)
            {
                //检查编号是否已经存在
                if (IsCodeExists(brands))
                    return -1;
                ret = map.Execute("INSERT INTO BRANDS(BRD_CODE, BRD_NAME) VALUES(@COD, @NAM)",
                new
                {
                    COD = brands.BrandCode,
                    NAM = brands.BrandName
                });
            }
            else
            {
                //更新
                ret = map.Execute("UPDATE BRANDS SET BRD_NAME = @NAM WHERE BRD_CODE = @COD",
                new
                {
                    COD = brands.BrandCode,
                    NAM = brands.BrandName
                });
            }
            return ret;
        }

        /// <summary>
        /// 删除品牌
        /// </summary>
        /// <param name="UnitCode"></param>
        /// <returns></returns>
        public int Delete(string brandsCode)
        {
            IMapper map = DatabaseInstance.Instance();
            return map.Execute("DELETE FROM BRANDS WHERE BRD_CODE = @COD; ", new { COD = brandsCode });
        }

        public void CreateRelationWithSupplier(string brandCode, List<SupplierEntity> suppliers)
        {
            IMapper map = DatabaseInstance.Instance();

            string sql = "INSERT INTO BRAND_SUPPLIER(BRD_CODE, S_CODE) VALUES(@BrandCode, @SupplierCode)";
            DynamicParameters parms = new DynamicParameters();
            parms.Add("BrandCode", brandCode);
            parms.Add("SupplierCode");

            foreach (SupplierEntity supplier in suppliers)
            {
                //先查看是否已建立关联，略过，也要排除默认供应商
                if (!IsRelationExists(brandCode, supplier.SupplierCode))
                {
                    parms.Set("SupplierCode", supplier.SupplierCode);
                    map.Execute(sql, parms);
                }
            }
        }

        private bool IsRelationExists(string brandCode, string supplierCode)
        {
            IMapper map = DatabaseInstance.Instance();
            string code = map.ExecuteScalar<string>("SELECT BRD_CODE FROM BRAND_SUPPLIER WHERE BRD_CODE = @BRD_COD AND S_CODE = @S_CODE",
                new { BRD_COD = brandCode, S_CODE = supplierCode });

            return !string.IsNullOrEmpty(code);
        }

        public int DeleteRelationSupplier(string brandCode, string supplierCode)
        {
            IMapper map = DatabaseInstance.Instance();
            return map.Execute("DELETE FROM BRAND_SUPPLIER WHERE BRD_CODE = @BRD_COD AND S_CODE = @S_CODE",
                new { BRD_COD = brandCode, S_CODE = supplierCode });
        }

        public List<SupplierEntity> ListRelationSuppliers(string brandCode)
        {
            IMapper map = DatabaseInstance.Instance();
            return map.Query<SupplierEntity>("SELECT BS.S_CODE, S.S_NAME FROM BRAND_SUPPLIER BS "+
                "INNER JOIN SUPPLIERS S ON BS.S_CODE = S.S_CODE WHERE BS.BRD_CODE = @brdCOD",
                new { brdCOD = brandCode });
        }
    }
}
