using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Nodes.Utils
{
    public class ExceptionHandler
    {
        public static string Format(Exception ex)
        {
            Cursor.Current = Cursors.Default;
            Type t = ex.GetType();
            if (t.Name.ToLower() == "mysqlexception")
            {
                MySqlException e = ex as MySqlException;
                switch (e.Number)
                {
                    case -2:
                        return "查询结果集过大，网络传输超时失败。";
                    case 2:
                    case 53:
                        return "无法连接到服务器，请检查您的网络连接，也有可能是服务器地址填写错误。";
                    case 208:
                        return "表结构丢失。";
                    case 547:
                        return string.Format("违反外键约束:{0}", e.Message);
                    case 201:
                    case 2812:
                        return "存储过程丢失。";
                    case 2627:
                    case 2601:
                        return string.Format("违反主键约束:{0}", e.Message);
                    case 4060:
                        return "无效的数据库名称。";
                    case 8145:
                        return "存储过程参数配置不正确，请联系管理员解决该问题。";
                    case 8178:
                        return "缺少输入或输出参数";
                    case 18456:
                        return "登录数据库失败，无效的用户名或密码。";
                    case 11:
                    case 17142:
                        return "无法连接到数据库，请检查数据库是否已启动。";
                    default:
                        return e.Message;
                }
            }
            else if (t == typeof(System.Net.WebException))
            {
                return "无法连接到应用服务器。";
            }
            else
            {
                return ex.Message;
            }
        }
    }
}
