using System.Data;
using System.Data.SQLite;
using System;

namespace Nodes.Dapper
{
    public partial class SQLiteInstance : MapperCommon, IMapper
    {
        public SQLiteInstance(string connString)
            : base(connString)
        {
        }

        /// <summary>
        /// 只创建连接，而不打开
        /// </summary>
        /// <returns></returns>
        public override IDbConnection CreateConnection()
        {
            return new SQLiteConnection(base.connString);
        }

        public override IDbDataAdapter GetDataAdapter()
        {
            return new SQLiteDataAdapter();
        }

        public override string GetSysDateString()
        {
            return "datetime('now')";
        }

        public int GetAutoIncreasementID(string tableName, string sequenceName)
        {
            object id = ExecuteScalar<object>(string.Format("select last_insert_rowid() from {0}", tableName));
            return Convert.ToInt32(id);
        }

        public string GetPagingString()
        {
            return "select {QueryField} from {TableName} {WhereCondition} {OrderByField} limit {PageSize} offset {RowIndexFrom}";
        }
    }
}
