using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Nodes.Dapper
{
    public partial class MySqlInstance : MapperCommon, IMapper
    {
        public MySqlInstance(string connString)
            : base(connString)
        {
        }

        /// <summary>
        /// 只创建连接，而不打开
        /// </summary>
        /// <returns></returns>
        public override IDbConnection CreateConnection()
        {
            return new MySqlConnection(base.connString);
        }

        public override IDbDataAdapter GetDataAdapter()
        {
            return new MySqlDataAdapter();
        }

        public override string GetSysDateString()
        {
            return "NOW()";
        }

        public int GetAutoIncreasementID(string tableName, string sequenceName)
        {
            object id = ExecuteScalar<object>("SELECT LAST_INSERT_ID()");
            return Convert.ToInt32(id);
        }

        public string GetPagingString()
        {
            return "SELECT {QueryField} FROM {TableName} {WhereCondition} LIMIT {RowIndexFrom}, {RowIndexTo}";
        }
    }
}