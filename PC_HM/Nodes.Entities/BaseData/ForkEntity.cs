using Nodes.Dapper;

namespace Nodes.Entities
{
    public class ForkEntity
    {
        /// <summary>
        /// 叉车编码
        /// </summary>
        [ColumnName("FORK_CODE")]
        public string ForkliftCode { get; set; }

        /// <summary>
        /// 叉车名称  
        /// </summary>
        [ColumnName("FORK_NAME")]
        public string ForkliftName { get; set; }

        [ColumnName("IS_DELETED")]
        public int? IsDeleted { get; set; }

    }
}
