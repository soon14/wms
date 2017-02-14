using System.Data;
using Devart.Data.Oracle;
using System;

namespace Nodes.Dapper
{
    public partial class OracleInstance : MapperCommon, IMapper
    {
        public OracleInstance(string connString)
            : base(connString)
        {
        }

        /// <summary>
        /// 只创建连接，而不打开
        /// </summary>
        /// <returns></returns>
        public override IDbConnection CreateConnection()
        {
            OracleConnection conn = new OracleConnection(base.connString);
            //conn.NumberMappings.Add(OracleNumberType.Number, 1, 6, typeof(int));
            //conn.NumberMappings.Add(OracleNumberType.Number, 7, 30, typeof(decimal));

            return conn;
        }

        public override IDbDataAdapter GetDataAdapter()
        {
            return new OracleDataAdapter();
        }

        public override string GetSysDateString()
        {
            return "SYSDATE";
        }

        public int GetAutoIncreasementID(string tableName, string sequenceName)
        {
            object id = ExecuteScalar<object>(string.Format("SELECT {0}.CURRVAL from dual", sequenceName));
            return Convert.ToInt32(id);
        }

        public string GetPagingString()
        {
            //注意A_TABLE_A和B_TABLE_B，起了个怪名，防止跟传进来的TableName重名了
            return "SELECT * FROM " +
                "(SELECT A_TABLE_A.*, ROWNUM rowNO FROM " +
                "(SELECT {QueryField} FROM {TableName} {WhereCondition} {OrderByField}) A_TABLE_A) B_TABLE_B " +
                "WHERE B_TABLE_B.rowNO BETWEEN {RowIndexFrom} AND {RowIndexTo}";
        }
    }
}
