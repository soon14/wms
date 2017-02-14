using Nodes.Dapper;

namespace Nodes.Entities
{
    public class BaseCodeEntity
    {
        [ColumnName("GROUP_CODE")]
        public string GroupCode { get; set; }

        [ColumnName("ITEM_VALUE")]
        public string ItemValue { get; set; }

        [ColumnName("ITEM_DESC")]
        public string ItemDesc { get; set; }

        [ColumnName("IS_ACTIVE")]
        public string IsActive { get; set; }

        [ColumnName("REMARK")]
        public string Remark { get; set; }

        public string Level { get; set; }

        public override string ToString()
        {
            return this.ItemDesc;
        }
    }
}
