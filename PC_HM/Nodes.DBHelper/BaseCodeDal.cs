using System.Collections.Generic;
using System.Data;
using Nodes.Dapper;
using Nodes.Entities;
using System;

namespace Nodes.DBHelper
{
    public class BaseCodeDal
    {
        /// <summary>
        /// 根据分组编号获取基础数据信息
        /// </summary>
        /// <param name="groupCode"></param>
        /// <returns></returns>
        public static DataTable GetItems(string groupCode)
        {
            IMapper map = DatabaseInstance.Instance();
            return map.LoadTable("select GROUP_CODE, ITEM_VALUE, ITEM_DESC, IS_ACTIVE, REMARK FROM WM_BASE_CODE WHERE IS_ACTIVE = 'Y' AND GROUP_CODE = @GroupCode", new { GroupCode = groupCode });
        }

        /// <summary>
        /// 获取活动状态的集合
        /// </summary>
        /// <param name="groupCode"></param>
        /// <returns></returns>
        public static List<BaseCodeEntity> GetItemList(string groupCode)
        {
            IMapper map = DatabaseInstance.Instance();

            string sql = "SELECT GROUP_CODE, ITEM_VALUE, ITEM_DESC, IS_ACTIVE, REMARK FROM WM_BASE_CODE WHERE IS_ACTIVE = 'Y' AND GROUP_CODE = @GroupCode";
            return map.Query<BaseCodeEntity>(sql, new { GroupCode = groupCode });
        }

        public static List<CustomFieldEntity> GetCustomFields(string groupName)
        {
            IMapper map = DatabaseInstance.Instance();

            string sql = "SELECT ID, GROUP_NAME, FIELD_NAME, FIELD_DESC, IS_ACTIVE, REMARK FROM CUSTOM_FIELDS WHERE GROUP_NAME = @GroupName";
            return map.Query<CustomFieldEntity>(sql, new { GroupName = groupName });
        }

        /// <summary>
        /// 更新采购单的审批方式：一审及双审
        /// </summary>
        /// <param name="groupCode"></param>
        /// <param name="itemValue"></param>
        /// <returns>返回的是受影响的行数，必须等于1，若大于1证明数据乱了</returns>
        public static int UpdatePOApproveType(string groupCode, string itemValue)
        {
            IMapper map = DatabaseInstance.Instance();
            int affectRows = map.Execute("UPDATE WM_BASE_CODE SET ITEM_VALUE = @ItemValue WHERE GROUP_CODE = @GroupCode", new { ItemValue = itemValue, GroupCode = groupCode });
            if (affectRows == 1)
            {
                if (itemValue == "1")
                {
                    //仅一审的话，需要把二审相关设置禁止掉
                    map.Execute("UPDATE WM_BASE_CODE SET IS_ACTIVE = 'N' WHERE GROUP_CODE = '102' AND ITEM_VALUE = '13'");
                    map.Execute("UPDATE MODULES SET IS_ACTIVE = 'N' WHERE MODULE_ID = '3_03'");
                }
                else
                {
                    map.Execute("UPDATE WM_BASE_CODE SET IS_ACTIVE = 'Y' WHERE GROUP_CODE = '102' AND ITEM_VALUE = '13'");
                    map.Execute("UPDATE MODULES SET IS_ACTIVE = 'Y' WHERE MODULE_ID = '3_03'");
                }
            }

            return affectRows;
        }

        public static void UpdateMaterialCustomField(List<CustomFieldEntity> fields)
        {
            IMapper map = DatabaseInstance.Instance();
            foreach (CustomFieldEntity field in fields)
            {
                int affectRows = map.Execute("UPDATE CUSTOM_FIELDS SET FIELD_DESC = @FieldDesc, IS_ACTIVE = @IsActive WHERE ID = @ID",
                    new { FieldDesc = field.FieldDesc, IsActive = field.IsActive, ID = field.ID });
                if (affectRows != 1)
                    throw new Exception(string.Format("更新字段“{0”的值时发生系统错误，请稍后重试或联系管理员解决此问题。", field.FieldName));
            }
        }

        public static int UpdateFieldByAttri(int itemValue, int attri)
        {
            string sql = string.Format(
                "UPDATE WM_BASE_CODE SET Attri1={0} WHERE ITEM_VALUE = {1}",
                attri, itemValue);
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql);
        }
    }
}
