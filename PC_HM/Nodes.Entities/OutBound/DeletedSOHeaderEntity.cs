using System;
using Nodes.Dapper;

namespace Nodes.Entities
{
    /// <summary>
    /// 到货通知单表头
    /// </summary>
    public class DeletedSOHeaderEntity: SOHeaderEntity
    {
        [ColumnName("DELETE_TIME")]
        public DateTime DeletedTime { get; set; }

        [ColumnName("DELETE_USER")]
        public string DeletedUser { get; set; }
    }
}
