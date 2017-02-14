using System;
using Nodes.Utils;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace Nodes.Dapper
{
    public class DatabaseInstance
    {
        protected static DatabaseProvider dbProvider = DatabaseProvider.None;
        protected static string connectionString = null;
        private static IMapper instance = null;

       
        public static IMapper Instance()
        {
            if (instance == null)
            {
                //读取配置文件，查看是哪个数据库，根据DBProvider实例化，读取连接字符串
                instance = new MySqlInstance(DbConnectionString);
                //if (DBProvider == DatabaseProvider.SQLServer)
                //{
                //    instance = new SqlServerInstance(DbConnectionString);
                //}
                //else if (DBProvider == DatabaseProvider.Oracle)
                //{
                //    instance = new OracleInstance(DbConnectionString);
                //}
                //else if (DBProvider == DatabaseProvider.SQLite)
                //{
                //    instance = new SQLiteInstance(DbConnectionString);
                //}
                //else
                //{
                //    throw new Exception("请配置数据库连接信息。");
                //}
            }

            return instance;
        }

        /// <summary>
        /// 从配置文件中读取数据库连接串
        /// </summary>
        private static string DbConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(connectionString))
                {

                    string ipAddress = XmlBaseClass.ReadConfigValue("ConnectionString", "Value");
                    if(ipAddress == "115.28.157.138")
                        connectionString = string.Format("Server={0};Database=d01;User Id=nodes;Password=nodes;pooling=true;Allow User Variables=True;", ipAddress);
                    else
                        connectionString = string.Format("Server={0};Database=wmsx;User Id=hmwms_write;Password=huimin@2016_wms;pooling=true;Allow User Variables=True;", ipAddress);
                    //对于SQLite数据库连接串，需要指定目录
                    //connectionString = connectionString.Replace("{AppDir}", PathUtil.ApplicationStartupPath);
                }

                return connectionString;
            }
        }

        #region "WMSSource"
        protected static string WMSConnectionString = null;
        private static IMapper wmsInstance = null;
        public static IMapper WMSInstance()
        {
            if (wmsInstance == null)
            {
                //读取配置文件，查看是哪个数据库，根据DBProvider实例化，读取连接字符串
                wmsInstance = new MySqlInstance(WMSDbConnectionString);
            }

            return wmsInstance;
        }
        /// <summary>
        /// 从配置文件中读取服务器数据库连接串
        /// </summary>
        private static string WMSDbConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(WMSConnectionString))
                {
                    WMSConnectionString = ConfigurationManager.ConnectionStrings["WMSConnectionString"].ConnectionString; //XmlBaseClass.ReadConfigValue("WMSConnectionString", "Value");
                    dbProvider = DatabaseProvider.MySql;
                    //对于SQLite数据库连接串，需要指定目录
                    //connectionString = connectionString.Replace("{AppDir}", PathUtil.ApplicationStartupPath);
                }

                return WMSConnectionString;
            }
        }
        #endregion

        /// <summary>
        /// 从配置文件中读取数据库类型
        /// </summary>
        public static DatabaseProvider DBProvider
        {
            get
            {
                if (dbProvider == DatabaseProvider.None)
                {
                    string provider = XmlBaseClass.ReadConfigValue("ConnectionString", "Provider");
                    if (provider.Equals("SqlServer", StringComparison.OrdinalIgnoreCase))
                        dbProvider = DatabaseProvider.SQLServer;
                    else if (provider.Equals("Oracle", StringComparison.OrdinalIgnoreCase))
                        dbProvider = DatabaseProvider.Oracle;
                    else if (provider.Equals("MySql", StringComparison.OrdinalIgnoreCase))
                        dbProvider = DatabaseProvider.MySql;
                    else if (provider.Equals("SQLite", StringComparison.OrdinalIgnoreCase))
                        dbProvider = DatabaseProvider.SQLite;
                    else
                        dbProvider = DatabaseProvider.None;
                }

                return dbProvider;
            }
        }
    }

    public enum DatabaseProvider
    {
        SQLServer, Oracle, MySql, SQLite, None
    }
}
