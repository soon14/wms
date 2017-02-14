using System.Collections.Generic;
using Nodes.Dapper;
using Nodes.Entities;

namespace Nodes.DBHelper
{
    public class OrganizationDal
    {
        /// <summary>
        /// 检查编码是否已存在
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        private bool IsCodeExists(OrgEntity org)
        {
            IMapper map = DatabaseInstance.Instance();
            string id = map.ExecuteScalar<string>("select ORG_CODE from ORGANIZATIONS where ORG_CODE = @COD",
            new { COD = org.OrgCode });

            if (!string.IsNullOrEmpty(id)) return true;
            return false;
        }

        /// <summary>
        /// 添加或编辑
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isNew">添加或编辑</param>
        /// <returns></returns>
        public int Save(OrgEntity entity, bool isNew)
        {
            IMapper map = DatabaseInstance.Instance();
            int ret = -2;

            if (isNew)
            {
                //检查编号是否已经存在
                if (IsCodeExists(entity))
                    return -1;
                ret = map.Execute("insert into ORGANIZATIONS(ORG_CODE, ORG_NAME, ALLOW_EDIT, IS_ACTIVE) " +
                    "values(@COD, @NAM, @ALLOW_EDIT, @IS_ACTIVE)",
                new
                {
                    COD = entity.OrgCode,
                    NAM = entity.OrgName,
                    ALLOW_EDIT = entity.AllowEdit,
                    IS_ACTIVE = entity.IsActive
                });
            }
            else
            {
                //更新
                ret = map.Execute("update ORGANIZATIONS set ORG_NAME = @NAM where ORG_CODE = @COD",
                new
                {
                    COD = entity.OrgCode,
                    NAM = entity.OrgName
                });
            }
            return ret;
        }

        ///<summary>
        ///枚举所有
        ///</summary>
        ///<returns></returns>
        public List<OrgEntity> GetAll()
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT ORG_CODE, ORG_NAME FROM ORGANIZATIONS ORDER BY ORG_CODE ASC";
            return map.Query<OrgEntity>(sql);
        }

        /// <summary>
        /// 删除大区
        /// </summary>
        /// <param name="StockAreaCode"></param>
        /// <returns></returns>
        public int Delete(string orgCode)
        {
            IMapper map = DatabaseInstance.Instance();
            string warehouseCode = map.ExecuteScalar<string>("SELECT W.WH_CODE FROM WM_WAREHOUSE W " +
                "INNER JOIN ORGANIZATIONS A ON A.ORG_CODE = W.ORG_CODE WHERE A.ORG_CODE = @COD", new { COD = orgCode });
            if (!string.IsNullOrEmpty(warehouseCode)) return -1;

            //查看是否有用户
            string userCode = map.ExecuteScalar<string>("SELECT U.USER_CODE FROM USERS U " +
                "INNER JOIN ORGANIZATIONS A ON A.ORG_CODE = U.ORG_CODE WHERE U.ORG_CODE = @COD", new { COD = orgCode });
            if (!string.IsNullOrEmpty(userCode)) return -2;

            return map.Execute("DELETE FROM ORGANIZATIONS WHERE ORG_CODE = @COD", new { COD = orgCode });
        }
    }
}
