using System;
using System.Collections.Generic;
using Nodes.Dapper;
using Nodes.Entities;

namespace Nodes.DBHelper
{
    public class CombMaterialDal
    {
        /// <summary>
        /// 列出某个组分料下面的子物料
        /// </summary>
        /// <param name="combCode"></param>
        /// <returns></returns>
        public List<MaterialEntity> ListMaterialsByCombCode(string combCode)
        {
            string sql = "SELECT S.COD, S.NAM, S.BRAND, S.PRODUCT_LINE, " +
               "S.PRICE FROM COMB_MATERIAL CM inner join MATERIAL S on CM.MATERIAL_COD = S.COD " +
               "WHERE CM.COMB_COD = @CombCode";

            IMapper map = DatabaseInstance.Instance();
            return map.Query<MaterialEntity>(sql, new { CombCode = combCode });
        }

        public bool ItemExists(string combCode, string materialCode)
        {
            string sql = "SELECT ID FROM COMB_MATERIAL WHERE COMB_COD = @CombCode AND MATERIAL_COD = @MaterialCode";
            IMapper map = DatabaseInstance.Instance();
            object id = map.ExecuteScalar<object>(sql, new { CombCode = combCode, MaterialCode = materialCode });
            return id != null;
        }

        public bool AddItemToComb(string combCode, string materialCode)
        {
            if (ItemExists(combCode, materialCode))
                return false;

            //更新物料类别为组分料
            UpdateMaterialType(combCode);

            string sql = "INSERT INTO COMB_MATERIAL(COMB_COD, MATERIAL_COD) VALUES(@CombCode, @MaterialCode)";
            IMapper map = DatabaseInstance.Instance();
            map.Execute(sql, new { CombCode = combCode, MaterialCode = materialCode });
            return true;
        }

        public int RemoveItem(string combCode, string materialCode)
        {
            string sql = "DELETE FROM COMB_MATERIAL WHERE COMB_COD = @CombCode AND MATERIAL_COD = @MaterialCode";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new { CombCode = combCode, MaterialCode = materialCode });
        }

        public int UpdateMaterialType(string combCode)
        {
            string sql = string.Format("UPDATE MATERIAL SET MATERIAL_TYPE = '{0}' WHERE COD = @MaterialCode", SysCodeConstant.MATERIAL_TYPE_COMB);
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new { MaterialCode = combCode });
        }

        /// <summary>
        /// 将组分料重新修改为普通物料
        /// </summary>
        /// <param name="combCode"></param>
        /// <returns></returns>
        public int UpdateComToMaterialType(string combCode)
        {
            string sql = string.Format("UPDATE MATERIAL SET MATERIAL_TYPE = '{0}' WHERE COD = @MaterialCode", SysCodeConstant.MATERIAL_TYPE_MATERIAL);
            IMapper map = DatabaseInstance.Instance();
            map.Execute(sql, new { MaterialCode = combCode });

            sql = "DELETE FROM COMB_MATERIAL WHERE COMB_COD = @CombCode";
            return map.Execute(sql, new { CombCode = combCode });
        }
    }
}