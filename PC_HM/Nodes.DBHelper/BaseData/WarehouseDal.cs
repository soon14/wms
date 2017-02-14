using System;
using System.Collections.Generic;
using Nodes.Dapper;
using Nodes.Entities;

namespace Nodes.DBHelper
{
    public class WarehouseDal
    {
        /// <summary>
        /// 检查仓库编码是否已存在
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        private bool IsWarehouseCodeExists(WarehouseEntity Warehouse)
        {
            IMapper map = DatabaseInstance.Instance();
            string id = map.ExecuteScalar<string>("select WH_CODE from WM_WAREHOUSE where WH_CODE = @COD",
            new { COD = Warehouse.WarehouseCode });

            return !string.IsNullOrEmpty(id);
        }

        /// <summary>
        /// 添加或编辑仓库
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operatorFlag">添加或编辑</param>
        /// <returns></returns>
        public int WarehouseAddAndUpdate(WarehouseEntity entity, bool isNew)
        {
            IMapper map = DatabaseInstance.Instance();
            int ret = -2;

            if (isNew)
            {
                //检查编号是否已经存在
                if (IsWarehouseCodeExists(entity))
                    return -1;
                ret = map.Execute("insert into WM_WAREHOUSE(WH_CODE, WH_NAME, ORG_CODE) " +
                    "values(@COD, @NAM, @ORG_CODE)",
                new
                {
                    COD = entity.WarehouseCode,
                    NAM = entity.WarehouseName,
                    ORG_CODE = entity.OrgCode
                });
            }
            else
            {
                //更新
                ret = map.Execute("update WM_WAREHOUSE set WH_NAME = @NAM, ORG_CODE = @ORG_CODE where WH_CODE = @COD",
                new
                {
                    COD = entity.WarehouseCode,
                    NAM = entity.WarehouseName,
                    ORG_CODE = entity.OrgCode
                });
            }
            return ret;
        }

        ///<summary>
        ///查询所有仓库
        ///</summary>
        ///<returns></returns>
        public List<WarehouseEntity> GetAllWarehouse()
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT W.WH_CODE, W.WH_NAME, W.ORG_CODE, A.ORG_NAME FROM WM_WAREHOUSE W LEFT JOIN ORGANIZATIONS A ON A.ORG_CODE = W.ORG_CODE";
            List<WarehouseEntity> WarehouseEntities = map.Query<WarehouseEntity>(sql);
            return WarehouseEntities;
        }

        ///<summary>
        ///根据大区查询所有仓库
        ///</summary>
        ///<returns></returns>
        public List<WarehouseEntity> GetAllWarehouseByOrg(string orgCode)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT W.WH_CODE, W.WH_NAME FROM WM_WAREHOUSE W LEFT join ORGANIZATIONS A ON A.ORG_CODE = W.ORG_CODE WHERE A.ORG_CODE = @COD";
            return map.Query<WarehouseEntity>(sql, new { COD = orgCode });
        }

        ///<summary>
        ///根据库房信息
        ///</summary>
        ///<returns></returns>
        public WarehouseEntity GetWarehouseByCode(string whCode)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT W.WH_CODE, W.WH_NAME, W.X_COOR, W.Y_COOR FROM WM_WAREHOUSE W WHERE W.WH_CODE = @COD";
            return map.QuerySingle<WarehouseEntity>(sql, new { COD = whCode });
        }

        /// <summary>
        /// 删除仓库
        /// </summary>
        /// <param name="StockWarehouseCode"></param>
        /// <returns></returns>
        public bool DeleteWarehouse(string WarehouseCode)
        {
            IMapper map = DatabaseInstance.Instance();
            string Code = map.ExecuteScalar<string>("SELECT ZN_CODE FROM WM_ZONE where WH_CODE = @COD",
            new { COD = WarehouseCode });
            if (!string.IsNullOrEmpty(Code)) return false;

            map.Execute("delete from WM_WAREHOUSE where WH_CODE = @COD", new { COD = WarehouseCode });
            return true;
        }
    }
}