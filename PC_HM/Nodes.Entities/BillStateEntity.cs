using Nodes.Dapper;

namespace Nodes.Entities
{
    public class BillStateEntity
    {
        [ColumnName("BILLSTATE")]
        public int BillState { get; set; }

        [ColumnName("STATEDESC")]
        public string StateDesc { get; set; }
    }
}
