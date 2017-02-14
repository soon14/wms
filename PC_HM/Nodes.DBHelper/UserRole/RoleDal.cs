using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Nodes.Entities;
using Nodes.Dapper;
using System.Data.SqlClient;

namespace Nodes.DBHelper
{
    public class RoleDal
    {
        public List<RoleEntity> ListUserRoles(string userCode)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = string.Format("select a.ROLE_ID, a.ROLE_NAME, a.ALLOW_EDIT, a.REMARK "
                         + "from ROLES a INNER JOIN user_role ur ON a.ROLE_ID = ur.ROLE_ID WHERE ur.USER_CODE = {0} ", userCode);
            return map.Query<RoleEntity>(sql);
        }

        public List<RoleEntity> ListRoles()
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "select ROLE_ID, ROLE_NAME, ALLOW_EDIT, REMARK from ROLES";
            return map.Query<RoleEntity>(sql);
        }

        public RoleEntity GetRoleInfo(int Id)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "select ROLE_ID, ROLE_NAME, ALLOW_EDIT, REMARK from ROLES where ROLE_ID = @ID";
            return map.QuerySingle<RoleEntity>(sql, new { ID = Id });
        }

        public int? GetRoleIDByName(string roleName)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "select ROLE_ID from ROLES where ROLE_NAME = @ROLE_NAME";
            return map.ExecuteScalar<int?>(sql, new { ROLE_NAME = roleName });
        }

        /// <summary>
        /// 获取某个用户的角色信息
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public List<RoleEntity> ListMyRoles(string userCode)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "select r.ROLE_ID, r.ROLE_NAME from USER_ROLE u inner join ROLES r on u.ROLE_ID = r.ROLE_ID where u.USER_CODE = @UserCode";
            return map.Query<RoleEntity>(sql, new { UserCode = userCode });
        }

        /// <summary>
        /// 列出某个角色下面的所有权限及下级权限
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public List<ModuleEntity> ListModulesByRoleID(int roleId)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "select m.MODULE_ID, m.MENU_NAME, m.PARENT_ID, m.DEEP " +
                "from MODULE_ROLE x inner join MODULES m on x.MODULE_ID = m.MODULE_ID " +
                "where x.ROLE_ID = @RoleId";
            return map.Query<ModuleEntity>(sql, new { RoleId = roleId });
        }

        /// <summary>
        /// 列出某个角色下面的所有用户
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public List<UserEntity> ListUsersByRoleID(int roleId)
        {
            IMapper map = DatabaseInstance.Instance();

            string sql = "SELECT DISTINCT U.USER_CODE, U.USER_NAME, U.WH_CODE, W.WH_NAME,L.IS_ONLINE " +
                "FROM USER_ROLE UR INNER JOIN USERS U ON UR.USER_CODE = U.USER_CODE " +
                "INNER JOIN WM_WAREHOUSE W ON U.WH_CODE = W.WH_CODE " +
                "LEFT JOIN user_online L ON U.USER_CODE=L.USER_CODE " +
                "WHERE UR.ROLE_ID = @RoleId ";
            return map.Query<UserEntity>(sql, new { RoleId = roleId });
        }

        /// <summary>
        /// 列出某个权限下的所有关联角色
        /// </summary>
        /// <param name="moduleID"></param>
        /// <returns></returns>
        public List<RoleEntity> ListRolesByModuleID(string moduleID)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "select r.ROLE_ID, r.ROLE_NAME from MODULE_ROLE x inner join ROLES r on r.ROLE_ID = x.ROLE_ID where x.MODULE_ID = @ModuleID";
            return map.Query<RoleEntity>(sql, new { ModuleID = moduleID });
        }

        /// <summary>
        /// 列出某个权限下的所有用户
        /// </summary>
        /// <param name="moduleID"></param>
        /// <returns></returns>
        public List<UserEntity> ListUsersByModuleID(string moduleID)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT DISTINCT U.USER_CODE, U.USER_NAME, U.WH_CODE, W.WH_NAME FROM MODULE_ROLE MR " +
                "INNER JOIN USER_ROLE UR ON MR.ROLE_ID = UR.ROLE_ID " +
                "INNER JOIN USERS U ON U.USER_CODE = UR.USER_CODE " +
                "INNER JOIN WM_WAREHOUSE W ON U.WH_CODE = W.WH_CODE " +
                "WHERE MR.MODULE_ID = @ModuleID";
            return map.Query<UserEntity>(sql, new { ModuleID = moduleID });
        }

        public List<ModuleEntity> ListModules()
        {
            IMapper map = DatabaseInstance.Instance();
            return map.Query<ModuleEntity>("select MODULE_ID, MENU_NAME, PARENT_ID, DEEP, MODULE_TYPE from MODULES WHERE IS_ACTIVE = 'Y' ORDER BY SORT_ORDER ASC");
        }

        public int SaveRole(RoleEntity role, List<ModuleEntity> modules, bool isCreateNew)
        {
            IMapper map = DatabaseInstance.Instance();
            //IDbTransaction trans = map.BeginTransaction();

            int? roleID = GetRoleIDByName(role.RoleName);
            //新增
            if (isCreateNew)
            {
                if (roleID != null)
                    return -1;

                map.Execute("insert into ROLES(ROLE_NAME, ALLOW_EDIT, REMARK) values(@RoleName, @AllowEdit, @Remark)", 
                    new { RoleName = role.RoleName, AllowEdit = role.AllowEdit, Remark = role.Remark });

                role.RoleId = map.GetAutoIncreasementID("ROLES", "SEQ_ROLE_ID");
            }
            else //更新
            {
                //为了防止重名
                if (roleID != null && roleID.Value != role.RoleId)
                    return -2;

                map.Execute("update Roles set ROLE_NAME = @RoleName, REMARK = @Remark where ROLE_ID = @RoleId", 
                    new { RoleName = role.RoleName, Remark = role.Remark, RoleId = role.RoleId });
                map.Execute("delete from MODULE_ROLE where ROLE_ID = @RoleId", new { RoleId = role.RoleId });
            }

            foreach (ModuleEntity module in modules)
            {
                map.Execute("insert into MODULE_ROLE(ROLE_ID, MODULE_ID) values(@RoleId, @ModuleId)", 
                    new { RoleId = role.RoleId, ModuleId = module.ModuleID });
            }

            return 1;
            //trans.Commit();
            //return role.RoleId;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns>0：不允许删除；1：成功</returns>
        public int DeleteRole(int roleId)
        {
            RoleEntity re = GetRoleInfo(roleId);
            if (re.AllowEdit == "N")
                return 0;
            
            IMapper map = DatabaseInstance.Instance();
            IDbTransaction trans = map.BeginTransaction();
            map.Execute("delete from USER_ROLE where ROLE_ID = @RoleId", new { RoleId = roleId }, trans);
            map.Execute("delete from MODULE_ROLE where ROLE_ID = @RoleId", new { RoleId = roleId }, trans);
            map.Execute("delete from ROLES where ROLE_ID = @RoleId", new { RoleId = roleId }, trans);
            trans.Commit();

            return 1;
        }
        public static string GetRoleNameByTaskType(string taskType)
        {
            string sql = string.Format(
@"SELECT R.ROLE_NAME FROM ROLES R WHERE R.Attri1 = '{0}'", taskType);
            IMapper map = DatabaseInstance.Instance();
            return map.ExecuteScalar<string>(sql);
        }
    }
}
