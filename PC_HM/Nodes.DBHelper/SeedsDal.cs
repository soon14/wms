using System;
using Nodes.Dapper;

namespace Nodes.DBHelper
{
    public class SeedsDal
    {
        /// <summary>
        /// 读取“当前”种子的值，使用时需要加1
        /// </summary>
        /// <param name="type">参考Enum SeedTypes</param>
        /// <returns></returns>
        public long GetCurrentSeed(int type, string warehouse)
        {
            IMapper map = DatabaseInstance.Instance();
            return map.ExecuteScalar<long>("select VAL from SEEDS where TYP = @Type and WAREHOUSE = @Warehouse", new { Type = type, Warehouse = warehouse });
        }

        /// <summary>
        /// 更新种子
        /// </summary>
        /// <param name="type"></param>
        /// <param name="step"></param>
        public void IncreaseSeed(int type, long step, string warehouse)
        {
            IMapper map = DatabaseInstance.Instance();
            map.ExecuteScalar<int>("UPDATE SEEDS SET VAL = VAL + @Step WHERE TYP = @Type and WAREHOUSE = @Warehouse", new { Step = step, Type = type, Warehouse = warehouse });
        }
    }
}
