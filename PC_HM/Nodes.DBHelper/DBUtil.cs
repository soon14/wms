using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Entities;
using Nodes.Dapper;
using System.Data;
using Nodes.UpdateEngine;

namespace Nodes.DBHelper
{
    public class DBUtil
    {
        /// <summary>
        /// 把形式为100902或100901,100902或'100901','100902'变成'100902'或'100901','100902'
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string FormatParameter(string param)
        {
            if (string.IsNullOrEmpty(param))
                throw new ArgumentNullException("参数不能为NULL。");

            if (param.IndexOf(',') > 0)
            {
                //100901,100902或'100901','100902'，先移除单引号，再统一添加
                param = param.Replace("'", "");
                param = param.Replace(" ", "");
                param = param.Replace(",", "','");
            }

            //再加两头的单引号，变成'100901','100902'
            return "'" + param + "'";
        }

        /// <summary>
        /// 将param=12,13,15格式化为BILL_STATE = '12' OR BILL_STATE = '13' OR BILL_STATE = '15'
        /// </summary>
        /// <param name="tableFieldName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string FormatParameter(string tableFieldName, string param)
        {
            if (string.IsNullOrEmpty(param))
                throw new ArgumentNullException("参数不能为NULL。");

            //先移除单引号、空格
            param = param.Replace("'", "").Replace(" ", "");

            string result = string.Empty;
            string[] items = param.Split(',');
            foreach(string item in items)
                result += string.Concat(tableFieldName, " = '", item, "' OR ");

            //去除最后多余的四个字符：" OR "
            return result.Remove(result.Length - 4);
        }

        public static Nodes.Entities.VersionInfo GetVersion(string id)
        {
            string sql = "select ID, VER, URL, REMARK, UPDATE_FLAG, WH_CODE from UPDATE_VER where ID = @Id";
            IMapper map = DatabaseInstance.WMSInstance();
            return map.QuerySingle<Nodes.Entities.VersionInfo>(sql, new { Id = id });
        }

        public static DataTable GetUpdateFiles(string id, string oldVersion, string newVersion)
        {
            string sql = "select FILENAME from UPDATE_FILES where ID = @Id and VERSION > @OldVersion and VERSION < @NewVersion";
            IMapper map = DatabaseInstance.WMSInstance();
            return map.LoadTable(sql, new { Id = id, OldVersion = oldVersion, NewVersion = newVersion });
        }
    }
}
