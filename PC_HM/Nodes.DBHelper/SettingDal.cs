using Nodes.Dapper;
using Nodes.Entities;
using System.Data;
using System;
using Nodes.Utils;

namespace Nodes.DBHelper
{
    /// <summary>
    /// 系统设置
    /// ITEM	            VALUE1	VALUE2	WAREHOUSE
    /// SeqBarcodePattern	8	    A	    01
    /// PagingSql	        NULL	        SELECT * FROM (SELECT ROW_NUMBER() OVER({OrderByField}) AS rowNum, {QueryField} FROM {TableName} {WhereCondition}) AS t WHERE rowNum > {RowIndexFrom} AND rowNum <= {RowIndexTo}	NULL
    /// </summary>
    public class SettingDal
    {
        /// <summary>
        /// 获取系统设置
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSysSetting()
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT ID, SET_ITEM, SET_VALUE, SET_GROUP, REMARK FROM WM_SETTING WHERE IS_ACTIVE = 'Y' ORDER BY ID ASC";
            return map.LoadTable(sql);
        }

        /// <summary>
        /// 获取系统设置
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSysLoadingSetting()
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT ID, SET_ITEM, SET_VALUE, SET_GROUP, REMARK FROM WM_SETTING ORDER BY ID ASC";
            return map.LoadTable(sql);
        }

        public static void SaveSysSetting(DataTable data)
        {
            IMapper map = DatabaseInstance.Instance();
            foreach (DataRow field in data.Rows)
            {
                int affectRows = map.Execute("UPDATE WM_SETTING SET SET_VALUE = @ItemValue WHERE ID = @ID",
                    new { ItemValue = ConvertUtil.ObjectToNull(field["SET_VALUE"]), ID = ConvertUtil.ToString(field["ID"]) });
                if (affectRows != 1)
                    throw new Exception(string.Format("更新字段“{0}”的值时发生系统错误，请稍后重试或联系管理员解决此问题。", ConvertUtil.ToString(field["SET_ITEM"])));
            }
        }
        public static int SaveSettings(string key, string value)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = @"UPDATE WM_SETTING SET SET_VALUE = @ItemValue WHERE SET_ITEM = @SetItem ";
            return map.Execute(sql, new { ItemValue = value, SetItem = key });
        }
        /// <summary>
        /// 读取条码规范定义表
        /// </summary>
        /// <param name="warehouse"></param>
        /// <returns></returns>
        public static DataTable GetBarcodeRule(string warehouse)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT ID, BARCODE, RULE FROM WM_BARCODE_RULE WHERE WH_CODE = @Warehouse";
            return map.LoadTable(sql, new { Warehouse = warehouse });
        }

        public static void SaveBarcodeRule(DataTable data)
        {
            IMapper map = DatabaseInstance.Instance();
            foreach (DataRow field in data.Rows)
            {
                int affectRows = map.Execute("UPDATE WM_BARCODE_RULE SET RULE = @Rule WHERE ID = @ID",
                    new { Rule = ConvertUtil.ObjectToNull(field["RULE"]), ID = ConvertUtil.ToString(field["ID"]) });
                if (affectRows != 1)
                    throw new Exception(string.Format("更新字段“{0}”的值时发生系统错误，请稍后重试或联系管理员解决此问题。", ConvertUtil.ToString(field["BARCODE"])));
            }
        }

        public static SettingEntity GetValue(string item, string group)
        {
            string sql = @"SELECT * FROM WM_SETTING WHERE SET_ITEM=@Item";
            IMapper map = DatabaseInstance.Instance();
            return map.QuerySingle<SettingEntity>(sql, new { Item = item });
        }
    }
}
