using System;
using Nodes.Dapper;

namespace Nodes.Entities
{
    /// <summary>
    /// 回收站：到货通知单表头
    /// </summary>
    public class DeletedAsnHeaderEntity : AsnHeaderEntity
    {
        [ColumnName("DELETED_TIME")]
        public DateTime DeletedTime { get; set; }

        [ColumnName("DELETED_USER")]
        public string DeletedUser { get; set; }
    }
}
