using System;
using System.Collections.Generic;
using Nodes.Dapper;
using Nodes.Entities;

namespace Nodes.DBHelper
{
    /// <summary>
    /// 手持终端维护，对应的表是WM_PDA
    /// </summary>
    public class PDADal
    {
       /// <summary>
       /// 检查编码是否已存在
       /// </summary>
       /// <param name="dept"></param>
       /// <returns></returns>
        private bool IsCodeExists(PDAEntity entity)
       {
           IMapper map = DatabaseInstance.Instance();
           string id = map.ExecuteScalar<string>("SELECT PDA_CODE FROM WM_PDA WHERE PDA_CODE = @COD",
           new { COD = entity.PDACode });
           return !string.IsNullOrEmpty(id);
       }

       /// <summary>
       /// 添加或编辑
       /// </summary>
       /// <param name="entity"></param>
       /// <param name="operatorFlag">添加或编辑</param>
       /// <returns></returns>
        public int Save(PDAEntity entity, bool isNew)
       {
           IMapper map = DatabaseInstance.Instance();
           int ret = -2;
           if (isNew)
           {
               //检查编号是否已经存在
               if (IsCodeExists(entity))
                   return -1;
               ret = map.Execute("INSERT INTO WM_PDA(PDA_CODE, PDA_NAME, IP_ADDRESS, WH_CODE, IS_ACTIVE) " +
                   "VALUES(@PDA_CODE, @PDA_NAME, @IP_ADDRESS, @WH_CODE, @IS_ACTIVE)",
               new
               {
                   PDA_CODE = entity.PDACode,
                   PDA_NAME = entity.PDAName,
                   IP_ADDRESS = entity.IpAddress,
                   WH_CODE = entity.WarehouseCode,
                   IS_ACTIVE = entity.IsActive
               });
           }
           else
           {
               //更新
               ret = map.Execute("UPDATE WM_PDA SET PDA_NAME = @PDA_NAME, IP_ADDRESS = @IP_ADDRESS, WH_CODE = @WH_CODE, IS_ACTIVE = @IS_ACTIVE WHERE PDA_CODE = @PDA_CODE",
               new
               {
                   PDA_NAME = entity.PDAName,
                   IP_ADDRESS = entity.IpAddress,
                   WH_CODE = entity.WarehouseCode,
                   IS_ACTIVE = entity.IsActive,
                   PDA_CODE = entity.PDACode
               });
           }
           return ret;
       }

        ///<summary>
        ///查询所有
        ///</summary>
        ///<returns></returns>
        public List<PDAEntity> GetAll()
       { 
           IMapper map = DatabaseInstance.Instance();
           string sql = "SELECT P.PDA_CODE, P.IP_ADDRESS, P.WH_CODE, W.WH_NAME, P.PDA_NAME, P.IS_ACTIVE FROM WM_PDA P INNER JOIN WM_WAREHOUSE W ON P.WH_CODE = W.WH_CODE";
           return map.Query<PDAEntity>(sql);
       }
      
       /// <summary>
       /// 删除
       /// </summary>
       /// <param name="StockAreaCode"></param>
       /// <returns></returns>
       public int Delete(string Code)
       {
           IMapper map = DatabaseInstance.Instance();
           return map.Execute("DELETE FROM WM_PDA WHERE PDA_CODE = @PDA_CODE", new { PDA_CODE = Code });
       }

        /// <summary>
        /// 按仓库获取启用状态的手持
        /// </summary>
        /// <param name="warehouse"></param>
        /// <returns></returns>
       public List<PDAEntity> GetActiveByWarehouse(string warehouseCode)
       {
           IMapper map = DatabaseInstance.Instance();
           string sql = "SELECT P.PDA_CODE, P.IP_ADDRESS, " +
               "P.PDA_NAME, P.IS_ACTIVE FROM WM_PDA P WHERE P.WH_CODE = @WH_CODE";
           return map.Query<PDAEntity>(sql, new { WH_CODE = warehouseCode });
       }
    }
}
