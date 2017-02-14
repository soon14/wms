using System;
using System.Data;

/*
 * 李文明
 * 2013-3-7
 * 
 * 在SqlMapper的基础上进行一些扩展
 * 自定义的一些通用的扩展方法不要直接写到原文件中，请写到该文件中便于维护
 * 
 * */

namespace Nodes.Dapper
{
    static partial class SqlMapper
    {
        #region 扩展方法
        /// <summary>
        /// 取出单个对象，而不是List列表
        /// 模仿Query封装
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cnn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static T QuerySingle<T>(this IDbConnection cnn, string sql, object param, IDbTransaction transaction, int? commandTimeout, CommandType? commandType)
        {
            var identity = new Identity(sql, commandType, cnn, typeof(T), param == null ? null : param.GetType(), null);
            var info = GetCacheInfo(identity);
            try
            {
                using (var cmd = SetupCommand(cnn, transaction, sql, info.ParamReader, param, commandTimeout, commandType))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (info.Deserializer == null)
                        {
                            info.Deserializer = GetDeserializer<T>(reader, 0, -1, false);
                            SetQueryCache(identity, info);
                        }

                        var deserializer = (Func<IDataReader, T>)info.Deserializer;

                        if (reader.Read())
                        {
                            var next = deserializer(reader);
                            return next;
                        }
                        else
                        {
                            return default(T);
                        }
                    }
                }
            }
            finally
            {
                PurgeQueryCache(identity);
            }
        }

        /// <summary>
        /// 获取表数据到内存表，使用该方法时请使用配套的CreateConnection，而不要使用OpenConnection方法
        /// </summary>
        /// <returns>返回表结构类型数据</returns>
        public static DataSet LoadDataSet(this IDbConnection cnn, IDbDataAdapter adapter, string sql, 
            object param, IDbTransaction transaction, int? commandTimeout, CommandType? commandType)
        {
            CacheInfo info = null;
            object prm = (object)param;
            
            // nice and simple
            if (prm != null)
            {
                Identity identity = new Identity(sql, commandType, cnn, null, (object)param == null ? null : ((object)param).GetType(), null);
                info = GetCacheInfo(identity);
            }

            IDbCommand cmd = SetupCommand(cnn, transaction, sql, info == null ? null : info.ParamReader, prm, commandTimeout, commandType);
            adapter.SelectCommand = cmd;
            adapter.SelectCommand.CommandTimeout = 1200;
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            return ds;
        }

        /// <summary>
        /// 获取第一行第一列的值
        /// </summary>
        /// <returns>返回指定类型的数据</returns>
        public static T ExecuteScalar<T>(this IDbConnection cnn, string sql, object param, IDbTransaction transaction,
            int? commandTimeout, CommandType? commandType)
        {
            CacheInfo info = null;
            object prm = (object)param;

            // nice and simple
            if (prm != null)
            {
                Identity identity = new Identity(sql, commandType, cnn, null, prm == null ? null : prm.GetType(), null);
                info = GetCacheInfo(identity);
            }

            IDbCommand cmd = SetupCommand(cnn, transaction, sql, info == null ? null : info.ParamReader, prm, commandTimeout, commandType);
            cmd.CommandTimeout = 1200;
            object returnValue = cmd.ExecuteScalar();
            if (returnValue is T)
                return (T)returnValue;
            else if (returnValue == DBNull.Value || returnValue == null)
                return default(T);
            else
                throw new Exception("Object returned was of the wrong type.");
        }
        #endregion
    }
}
