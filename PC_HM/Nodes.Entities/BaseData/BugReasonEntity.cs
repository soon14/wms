using Nodes.Dapper;

namespace Nodes.Entities
{
    /// <summary>
    /// 不合格原因: Bug Reason
    /// </summary>
    public class BusReasonEntity
    {
        /// <summary>
        /// 编码
        /// </summary>
        [ColumnName("BUG_CODE")]
        public string BugCode
        {
            set;
            get;
        }

        /// <summary>
        /// 名称
        /// </summary>
        [ColumnName("BUG_NAME")]
        public string BugName
        {
            set;
            get;
        }
    }
}