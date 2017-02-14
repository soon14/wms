using Nodes.Dapper;

namespace Nodes.Entities
{
    public class SettingEntity
    {
        [ColumnName("ID")]
        public int ID { get; set; }
        [ColumnName("SET_ITEM")]
        public string Item { get; set; }
        [ColumnName("SET_VALUE")]
        public string Value { get; set; }
        [ColumnName("SET_GROUP")]
        public string Group { get; set; }
        [ColumnName("REMARK")]
        public string Remark { get; set; }
    }
}
