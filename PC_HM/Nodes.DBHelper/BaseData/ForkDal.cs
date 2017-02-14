using System.Collections.Generic;
using Nodes.Dapper;
using Nodes.Entities;

namespace Nodes.DBHelper
{
     public class ForkDal
    {
        /// <summary>
        /// 检查叉车编码是否已存在
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
         private bool IsCodeExists(ForkEntity fork)
        {
            IMapper map = DatabaseInstance.Instance();
            string id = map.ExecuteScalar<string>("SELECT FORK_CODE FROM WM_FORK WHERE FORK_CODE = @COD", new { COD = fork.ForkliftCode });
            return !string.IsNullOrEmpty(id);
        }

        /// <summary>
        /// 添加或编辑叉车
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isNew">添加或编辑</param>
        /// <returns></returns>
         public int Save(ForkEntity fork, bool isNew)
        {
            IMapper map = DatabaseInstance.Instance();
            int ret = -2;
            if (isNew)
            {
                //检查编号是否已经存在
                if (IsCodeExists(fork))
                    return -1;
                ret = map.Execute("INSERT INTO WM_FORK(FORK_CODE, FORK_NAME ) VALUES(@COD, @NAM)",
                new
                {
                    COD = fork.ForkliftCode,
                    NAM = fork.ForkliftName
                });
            }
            else
            {
                //更新
                ret = map.Execute("UPDATE WM_FORK SET FORK_NAME = @NAM WHERE FORK_CODE = @COD",
                new
                {
                    COD = fork.ForkliftCode,
                    NAM = fork.ForkliftName
                });
            }
            return ret;
        }

        ///<summary>
        ///查询所有叉车
        ///</summary>
        ///<returns></returns>
         public List<ForkEntity> GetAllFork()
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT FORK_CODE, FORK_NAME FROM WM_FORK WHERE IFNULL(IS_DELETED, 0) <> 1";
            return map.Query<ForkEntity>(sql);
        }

        /// <summary>
        /// 添加删除标记
        /// </summary>
        /// <param name="UnitCode"></param>
        /// <returns></returns>
         public int Delete(string forkCode)
        {
            IMapper map = DatabaseInstance.Instance();
            return map.Execute("UPDATE WM_FORK SET IS_DELETED = 1 WHERE FORK_CODE = @COD", new { COD = forkCode });
        }
    }
}
