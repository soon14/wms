using Nodes.Dapper;

namespace Nodes.Entities
{
    /// <summary>
    /// 收货单明细信息
    /// </summary>
    public class ASNDetailEntity : PODetailEntity
    {
        /// <summary>
        /// 已验收数量
        /// </summary>
        [ColumnName("CHECK_QTY")]
        public int? CheckedQty { get; set; }

        /// <summary>
        /// 已上架数量
        /// </summary>
        [ColumnName("PUT_QTY")]
        public int? PutawayQty { get; set; }

    }
}
