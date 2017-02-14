using System;
using System.Collections.Generic;
using Nodes.Dapper;
using Nodes.Entities;

namespace Nodes.DBHelper
{
    /// <summary>
    /// 不合格原因Bug Reason
    /// </summary>
    public class BugReasonDal
    {
        /// <summary>
        /// 检查编码是否已存在
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        private bool IsCodeExists(BusReasonEntity entity)
        {
            IMapper map = DatabaseInstance.Instance();
            string id = map.ExecuteScalar<string>("SELECT BUG_CODE FROM WM_BUG_REASON WHERE BUG_CODE = @BUG_CODE",
            new { BUG_CODE = entity.BugCode });
            return !string.IsNullOrEmpty(id);
        }

        /// <summary>
        /// 添加或编辑
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="operatorFlag">添加或编辑</param>
        /// <returns></returns>
        public int Save(BusReasonEntity entity, bool isNew)
        {
            IMapper map = DatabaseInstance.Instance();
            int ret = -2;
            if (isNew)
            {
                //检查编号是否已经存在
                if (IsCodeExists(entity))
                    return -1;
                ret = map.Execute("INSERT INTO WM_BUG_REASON(BUG_CODE, BUG_NAME) VALUES(@BUG_CODE, @BUG_NAME)",
                new
                {
                    BUG_CODE = entity.BugCode,
                    BUG_NAME = entity.BugName
                });
            }
            else
            {
                //更新
                ret = map.Execute("UPDATE WM_BUG_REASON SET BUG_NAME = @BUG_NAME WHERE BUG_CODE = @BUG_CODE",
                new
                {
                    BUG_CODE = entity.BugCode,
                    BUG_NAME = entity.BugName
                });
            }
            return ret;
        }

        ///<summary>
        ///查询所有
        ///</summary>
        ///<returns></returns>
        public List<BusReasonEntity> GetAll()
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT BUG_CODE, BUG_NAME FROM WM_BUG_REASON";
            return map.Query<BusReasonEntity>(sql);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="StockAreaCode"></param>
        /// <returns></returns>
        public int DeleteUnit(string Code)
        {
            IMapper map = DatabaseInstance.Instance();
            return map.Execute("DELETE FROM WM_BUG_REASON WHERE BUG_CODE = @BUG_CODE", new { BUG_CODE = Code });
        }
    }
}
