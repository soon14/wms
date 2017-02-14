using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Nodes.Dapper
{
    public interface IMapper
    {
        #region MySqlScript
        int ExecuteMySqlScript(string sql);
        #endregion

        #region Execute
        int Execute(string sql);
        int Execute(string sql, object param);
        int Execute(string sql, object param, CommandType? commandType);
        int Execute(string sql, object param, int? commandTimeout, CommandType? commandType);
        int Execute(string sql, object param, IDbTransaction trans);
        int Execute(string sql, object param, IDbTransaction trans, CommandType commandType);
        IDbTransaction BeginTransaction();
        #endregion

        #region Query
        List<T> Query<T>(string sql);
        List<T> Query<T>(string sql, object param);
        List<T> Query<T>(string sql, object param, bool buffered);
        List<T> Query<T>(string sql, object param, bool buffered, CommandType? commandType);
        #endregion

        #region QuerySingle
        T QuerySingle<T>(string sql);
        T QuerySingle<T>(string sql, object param);
        T QuerySingle<T>(string sql, object param, CommandType? commandType);
        T QuerySingle<T>(string sql, object param, int? commandTimeout, CommandType? commandType);
        #endregion

        #region LoadDataSet
        DataTable LoadTable(string sql);
        DataTable LoadTable(string sql, object param);
        DataTable LoadTable(string sql, object param, CommandType cmdType);
        DataSet LoadDataSet(string sql);
        DataSet LoadDataSet(string sql, object param);
        DataSet LoadDataSet(string sql, object param, CommandType cmdType);
        #endregion

        #region ExecuteScalar
        T ExecuteScalar<T>(string sql);
        T ExecuteScalar<T>(string sql, object param);
        T ExecuteScalar<T>(string sql, object param, IDbTransaction trans, CommandType? commandType);
        #endregion

        #region "数据库方言处理：sysdate-getdate()；自动增长ID"
        /// <summary>
        /// 获取系统时间的函数名称，Oracle是Sysdate，sql server是getdate()
        /// </summary>
        /// <returns></returns>
        string GetSysDateString();

        /// <summary>
        /// 获取自动增长的ID
        /// </summary>
        /// <returns></returns>
        int GetAutoIncreasementID(string tableName, string sequenceName);

        /// <summary>
        /// 分页查询字符串
        /// </summary>
        /// <returns></returns>
        string GetPagingString();
        #endregion
    }
}
