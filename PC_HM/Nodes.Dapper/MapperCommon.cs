using System.Collections.Generic;
using System.Data;
using System.Linq;
using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace Nodes.Dapper
{
    public abstract class MapperCommon
    {
        public IDbConnection conn;
        protected string connString = string.Empty;

        public MapperCommon(string connString)
        {
            this.connString = connString;
            conn = CreateConnection();
        }

        public abstract IDbConnection CreateConnection();

        #region MySqlScript实现
        public virtual int ExecuteMySqlScript(string sql)
        {
            OpenConnection();
            MySqlConnection MySqlConn = (MySqlConnection)conn;
            MySqlScript sqlScript = new MySqlScript(MySqlConn);
            sqlScript.Query = sql;
            return sqlScript.Execute();
        }
        #endregion

        #region Execute实现
        public virtual int Execute(string sql)
        {
            return Execute(sql, null);
        }

        public virtual int Execute(string sql, object param)
        {
            return Execute(sql, param, CommandType.Text);
        }

        public virtual int Execute(string sql, object param, CommandType? commandType)
        {
            return Execute(sql, param, null, commandType);
        }

        public virtual int Execute(string sql, object param, IDbTransaction trans)
        {
            return Execute(sql, param, trans, CommandType.Text);
        }

        public virtual int Execute(string sql, object param, int? commandTimeout, CommandType? commandType)
        {
            return Execute(sql, param, null, commandTimeout, commandType);
        }

        public virtual int Execute(string sql, object param, IDbTransaction trans, CommandType commandType)
        {
            return Execute(sql, param, trans, null, commandType);
        }

        public virtual int Execute(string sql, object param, IDbTransaction trans, int? commandTimeout, CommandType? commandType)
        {
            OpenConnection();

            sql = SetupCommandText(commandType, sql);
            return conn.Execute(sql, param, trans, commandTimeout, commandType);
        }

        public virtual IDbTransaction BeginTransaction()
        {
            return conn.BeginTransaction();
        }
        #endregion

        #region Query实现
        public virtual List<T> Query<T>(string sql)
        {
            return Query<T>(sql, null);
        }
        public virtual List<T> Query<T>(string sql, object param)
        {
            return Query<T>(sql, param, false, CommandType.Text);
        }

        public virtual List<T> Query<T>(string sql, object param, bool buffered)
        {
            return Query<T>(sql, param, buffered, null);
        }
        public virtual List<T> Query<T>(string sql, object param, bool buffered, CommandType? commandType)
        {
            OpenConnection();

            sql = SetupCommandText(commandType, sql);
            return conn.Query<T>(sql, param, null, buffered, null, commandType).ToList();
        }
        #endregion

        #region QuerySingle实现
        public virtual T QuerySingle<T>(string sql)
        {
            return QuerySingle<T>(sql, null);
        }

        public virtual T QuerySingle<T>(string sql, object param)
        {
            return QuerySingle<T>(sql, param, CommandType.Text);
        }

        public virtual T QuerySingle<T>(string sql, object param, CommandType? commandType)
        {
            return QuerySingle<T>(sql, param, 1200, commandType);
        }

        public virtual T QuerySingle<T>(string sql, object param, int? commandTimeout, CommandType? commandType)
        {
            OpenConnection();
            sql = SetupCommandText(commandType, sql);
            return conn.QuerySingle<T>(sql, param, null, commandTimeout, commandType);
        }
        #endregion

        #region LoadDataSet实现
        public virtual DataTable LoadTable(string sql)
        {
            return LoadTable(sql, null);
        }

        public virtual DataTable LoadTable(string sql, object param)
        {
            return LoadTable(sql, param, CommandType.Text);
        }

        public virtual DataTable LoadTable(string sql, object param, CommandType cmdType)
        {
            return LoadDataSet(sql, param, cmdType).Tables[0];
        }

        public virtual DataSet LoadDataSet(string sql)
        {
            return LoadDataSet(sql, null);
        }

        public virtual DataSet LoadDataSet(string sql, object param)
        {
            return LoadDataSet(sql, param, CommandType.Text);
        }

        public virtual IDbDataAdapter GetDataAdapter()
        {
            return null;
        }

        public virtual DataSet LoadDataSet(string sql, object param, CommandType cmdType)
        {
            OpenConnection();
            sql = SetupCommandText(cmdType, sql);
            return conn.LoadDataSet(GetDataAdapter(), sql, param, null, null, cmdType);
        }
        #endregion

        public virtual T ExecuteScalar<T>(string sql)
        {
            return ExecuteScalar<T>(sql, null);
        }

        public virtual T ExecuteScalar<T>(string sql, object param)
        {
            OpenConnection();
            sql = SetupCommandText(CommandType.Text, sql);
            return conn.ExecuteScalar<T>(sql, param, null, null, CommandType.Text);
        }

        public virtual T ExecuteScalar<T>(string sql, object param, IDbTransaction trans, CommandType? commandType)
        {
            OpenConnection();

            sql = SetupCommandText(commandType, sql);
            return conn.ExecuteScalar<T>(sql, param, trans, null, commandType);
        }

        public void OpenConnection()
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.ConnectionString = this.connString;
                ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["WMSConnectionString"];
                if (settings != null && this.connString ==settings.ConnectionString)
                {
                    conn.Dispose();
                    conn = CreateConnection();
                }
                conn.Open();
            }
        }

        private string SetupCommandText(CommandType? commandType, string sql)
        {
            //李文明 2014-6-27 添加参数标记符“@”或“:”的自动替换
            if (commandType == null || commandType.Value == CommandType.Text)
            {
                if (DatabaseInstance.DBProvider == DatabaseProvider.Oracle)
                    sql = sql.Replace('@', ':');
                //else
                //    sql = sql.Replace(':', '@');//影响时间格式的字段，会将时间部分的冒号替换，导致系统无法识别
            }

            return sql;
        }

        public virtual string GetSysDateString()
        {
            return "GETDATE()";
        }
    }
}