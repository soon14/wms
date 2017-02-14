using Nodes.Dapper;

namespace Nodes.Entities
{
    /// <summary>
    /// 温控策略
    /// </summary>
    public class TemperatureEntity : BaseEntity
    {
        #region Model

        /// <summary>
        /// 温控编码
        /// </summary>
        [ColumnName("TEMP_CODE")]
        public string TemperatureCode
        {
            set;
            get;
        }

        /// <summary>
        /// 温控描述
        /// </summary>
        [ColumnName("TEMP_NAME")]
        public string TemperatureName
        {
            set;
            get;
        }

        /// <summary>
        /// 温度下限
        /// </summary>
        [ColumnName("LOWER_LIMIT")]
        public int? LowerLimit
        {
            set;
            get;
        }

        /// <summary>
        /// 温度上限
        /// </summary>
        [ColumnName("UPPER_LIMIT")]
        public int? UpperLimit
        {
            set;
            get;
        }
        #endregion Model
    }
}