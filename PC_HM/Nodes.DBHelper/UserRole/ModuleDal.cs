using System.Collections.Generic;
using Nodes.Dapper;
using Nodes.Entities;

namespace Nodes.DBHelper
{
    public class ModuleDal
    {
        public List<ModuleEntity> SystemMenuLists()
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "select MODULE_ID, MENU_NAME, PARENT_ID, DEEP, FORM_NAME from MODULES WHERE IS_ACTIVE = 'Y' AND DEEP <=1 ORDER BY MODULE_ID ASC";
            return map.Query<ModuleEntity>(sql);
        }

        public List<ModuleEntity> ListSystemMenus(string loginedUserCode)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT DISTINCT M.MODULE_ID, M.MENU_NAME, M.PARENT_ID, M.DEEP, M.FORM_NAME, M.SORT_ORDER, M.MODULE_TYPE FROM MODULES M " +
                "INNER JOIN MODULE_ROLE MR ON M.MODULE_ID = MR.MODULE_ID " +
                "INNER JOIN USER_ROLE UR ON UR.ROLE_ID = MR.ROLE_ID " +
                "WHERE M.DEEP < 2 AND M.IS_ACTIVE = 'Y' AND M.MODULE_TYPE = 1 AND UR.USER_CODE = @USERCODE " +
                "ORDER BY M.SORT_ORDER ASC";
            return map.Query<ModuleEntity>(sql, new { UserCode = loginedUserCode });
        }

        public List<ModuleEntity> ListSystemMenus()
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT DISTINCT M.MODULE_ID, M.MENU_NAME, M.PARENT_ID, M.DEEP, M.FORM_NAME, M.SORT_ORDER, M.MODULE_TYPE FROM MODULES M " +
                "WHERE M.DEEP < 2 AND M.IS_ACTIVE = 'Y' AND M.MODULE_TYPE = 1 " +
                "ORDER BY M.SORT_ORDER ASC";
            return map.Query<ModuleEntity>(sql);
        }
    }
}