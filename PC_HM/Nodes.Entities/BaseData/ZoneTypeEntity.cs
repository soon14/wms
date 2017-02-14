using Nodes.Dapper;

namespace Nodes.Entities
{
    /// <summary>
    /// 货区的功能划分
    /// </summary>
    public class ZoneTypeEntity
    {
        #region Model

        /// <summary>
        /// 编码
        /// </summary>
        [ColumnName("ZT_CODE")]
        public string Code
        {
            set;
            get;
        }

        /// <summary>
        /// 名称
        /// </summary>
        [ColumnName("ZT_NAME")]
        public string Name
        {
            set;
            get;
        }

        [ColumnName("REMARK")]
        public string Remark
        {
            get;
            set;
        }
        #endregion Model
    }
}

