using System;
using System.Collections.Generic;
using Nodes.Dapper;
using Nodes.Entities;
using System.Data;

namespace Nodes.DBHelper
{
    public class UserDal
    {
        /// <summary>
        /// 获取一个用户的详细信息
        /// </summary>
        /// <param name="USER_ID"></param>
        /// <returns></returns>
        public UserEntity GetUserInfo(string userCode)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = @"SELECT U.USER_CODE, U.USER_NAME, U.BRANCH_CODE, U.PWD, U.ALLOW_EDIT, U.IS_ACTIVE, 
  U.WH_CODE, W.WH_NAME, U.REMARK, U.UPDATE_BY, U.UPDATE_DATE, U.IS_OWN, U.MOBILE_PHONE,
  W.IS_CENTER_WH, W.CENTER_WH_CODE
 FROM USERS U 
 INNER JOIN WM_WAREHOUSE W ON U.WH_CODE = W.WH_CODE 
 WHERE U.USER_CODE = @UserCode AND IFNULL(U.IS_DELETED, 0) <> 1 ";
            return map.QuerySingle<UserEntity>(sql, new { UserCode = userCode });
        }

        public static bool HasThisRight(string userCode, short moduleId)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "select ru.USER_CODE from USER_ROLE as ru " +
                "inner join MODULE_ROLE as mr on ru.ROLE_ID = mr.ROLE_ID " +
                "where ru.USER_CODE = @UserCode and mr.MODULE_ID = @ModuleId AND IFNULL(RU.IS_DELETED, 0) <> 1";

            string _UserCode = map.ExecuteScalar<string>(sql, new { UserCode = userCode, ModuleId = moduleId });
            return !string.IsNullOrEmpty(_UserCode);
        }

        /// <summary>
        /// 根据用户身份信息，查看是否由此权限
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="pwd"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public string TempAuthorize(string userCode, string pwd, string roleName)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT U.USER_CODE FROM USERS U " +
                "INNER JOIN USER_ROLE UR ON U.USER_CODE = UR.USER_CODE " +
                "INNER JOIN ROLES R ON R.ROLE_ID = UR.ROLE_ID " +
                "WHERE U.USER_CODE = @UserCode AND U.IS_ACTIVE = 'Y' AND R.ROLE_NAME = @RoleName AND U.PWD = @Password AND IFNULL(U.IS_DELETED, 0) <> 1";
            return map.ExecuteScalar<string>(sql, new { UserCode = userCode, RoleName = roleName, Password = pwd });
        }

        public List<UserEntity> ListUsers()
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT U.USER_CODE, U.USER_NAME, U.PWD, U.ALLOW_EDIT, U.IS_ACTIVE, U.WH_CODE, W.WH_NAME, U.REMARK, " +
                "U.MOBILE_PHONE, U.USER_TYPE, wbc.ITEM_DESC USER_TYPE_DESC, U.USER_ATTRI, BC.ITEM_DESC USER_ATTRI_DESC, " +
                "  UO.IS_ONLINE " +
                "FROM USERS U INNER JOIN WM_WAREHOUSE W ON U.WH_CODE = W.WH_CODE " +
                "LEFT JOIN wm_base_code wbc ON U.USER_TYPE = wbc.ITEM_VALUE " +
                "LEFT JOIN WM_BASE_CODE BC ON U.USER_ATTRI = BC.ITEM_VALUE " +
                "LEFT JOIN user_online UO ON UO.USER_CODE =U.USER_CODE  " +
                "WHERE IFNULL(U.IS_DELETED, 0) <> 1";
            return map.Query<UserEntity>(sql);
        }

        /// <summary>
        /// 列出某个组织下面的某个角色的成员，例如保税库的发货员，状态必须是启用的
        /// </summary>
        /// <param name="warehouse"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public List<UserEntity> ListUsersByRoleAndWarehouseCode(string warehouseCode, string roleName)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT DISTINCT U.USER_CODE, U.USER_NAME, U.PWD, U.ALLOW_EDIT, U.IS_ACTIVE, " +
                "U.WH_CODE, W.WH_NAME, U.REMARK, U.MOBILE_PHONE ,uo.IS_ONLINE,R.ROLE_ID  " +
                "FROM USERS U " +
                "INNER JOIN user_online uo ON uo.USER_CODE =U.USER_CODE  " +
                "INNER JOIN WM_WAREHOUSE W ON U.WH_CODE = W.WH_CODE " +
                "INNER JOIN USER_ROLE UR ON U.USER_CODE = UR.USER_CODE " +
                "INNER JOIN ROLES R ON R.ROLE_ID = UR.ROLE_ID " +
                "WHERE U.WH_CODE = @WarehouseCode AND U.IS_ACTIVE = 'Y' AND R.ROLE_NAME = @RoleName AND IFNULL(U.IS_DELETED, 0) <> 1 AND UR.Attri2='1'";
            return map.Query<UserEntity>(sql, new { WarehouseCode = warehouseCode, RoleName = roleName });
        }

        /// <summary>
        /// 列出某个库房下面的成员，例如保税库的发货员，状态必须是启用的
        /// </summary>
        /// <param name="warehouse"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static List<UserEntity> ListUsersByWarehouseCode(string warehouseCode)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT DISTINCT U.USER_CODE, U.USER_NAME, U.PWD, U.ALLOW_EDIT, U.IS_ACTIVE, " +
                "U.WH_CODE, W.WH_NAME, U.REMARK, U.MOBILE_PHONE, U.USER_TYPE, WBC.ITEM_DESC USER_TYPE_DESC, " +
                "U.USER_ATTRI, BC.ITEM_DESC USER_ATTRI_DESC " +
                "FROM USERS U " +
                "INNER JOIN WM_WAREHOUSE W ON U.WH_CODE = W.WH_CODE " +
                "LEFT JOIN WM_BASE_CODE WBC ON WBC.ITEM_VALUE = U.USER_TYPE " +
                "LEFT JOIN WM_BASE_CODE BC ON BC.ITEM_VALUE = U.USER_TYPE " +
                "WHERE U.WH_CODE = @WarehouseCode AND U.IS_ACTIVE = 'Y' AND IFNULL(U.IS_DELETED, 0) <> 1";
            return map.Query<UserEntity>(sql, new { WarehouseCode = warehouseCode });
        }

        /// <summary>
        /// 获取某个库房下面的所有的拥有任务角色的人员
        /// </summary>
        /// <param name="warehouseCode"></param>
        /// <returns></returns>
        public static List<UserEntity> ListUsersByWarehouseCodeAndTask(string warehouseCode, string baseCode)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = string.Format(@"SELECT U.USER_CODE, U.USER_NAME, U.PWD, U.ALLOW_EDIT, UO.IS_ONLINE, 
  U.WH_CODE, W.WH_NAME, U.REMARK, U.MOBILE_PHONE, R.ROLE_NAME, UR.ATTRI2, GROUP_CONCAT(R.ROLE_NAME) ROLE_LIST
  FROM USERS U 
  INNER JOIN WM_WAREHOUSE W ON U.WH_CODE = W.WH_CODE 
  LEFT JOIN USER_ONLINE UO ON UO.USER_CODE = U.USER_CODE
  LEFT JOIN USER_ROLE UR ON UR.USER_CODE = U.USER_CODE
  LEFT JOIN ROLES R ON UR.ROLE_ID = R.ROLE_ID
  WHERE U.IS_ACTIVE = 'Y' AND IFNULL(U.IS_DELETED, 0) <> 1 AND R.Attri1 IN (SELECT C.ITEM_VALUE FROM WM_BASE_CODE C WHERE C.GROUP_CODE = '114' AND C.IS_ACTIVE = 'Y')
    AND U.WH_CODE = @WarehouseCode {0}
  GROUP BY U.USER_CODE
  ORDER BY UO.IS_ONLINE DESC", baseCode == "0" ? string.Empty : " AND UR.Attri2 = 1 AND R.Attri1 = " + baseCode);
            return map.Query<UserEntity>(sql, new { WarehouseCode = warehouseCode });
        }

        public List<UserEntity> ListUsersByRoleAndWarehouseCodeForCount(string warehouseCode, string roleName)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = "SELECT DISTINCT U.USER_CODE, U.USER_NAME, U.PWD, U.ALLOW_EDIT, U.IS_ACTIVE, " +
                "U.WH_CODE, W.WH_NAME, U.REMARK, U.MOBILE_PHONE " +
                "FROM USERS U " +
                "INNER JOIN WM_WAREHOUSE W ON U.WH_CODE = W.WH_CODE " +
                "INNER JOIN USER_ROLE UR ON U.USER_CODE = UR.USER_CODE " +
                "INNER JOIN ROLES R ON R.ROLE_ID = UR.ROLE_ID " +
                "INNER JOIN USER_ONLINE UO ON U.USER_CODE = UO.USER_CODE AND UO.IS_ONLINE = 'Y' " +
                "WHERE U.WH_CODE = @WarehouseCode AND U.IS_ACTIVE = 'Y' AND R.ROLE_NAME = @RoleName AND IFNULL(U.IS_DELETED, 0) <> 1";
            return map.Query<UserEntity>(sql, new { WarehouseCode = warehouseCode, RoleName = roleName });
        }
        /// <summary>
        /// 删除用户，先从用户角色关联表删除，再从用户表删除
        /// </summary>
        /// <param name="USER_ID"></param>
        /// <returns>-1：出现异常；0：没查到USER_ID记录；1：不允许删除；2：删除成功</returns>
        public int DeleteUser(string userCode)
        {
            int retVal = -1;

            UserEntity user = GetUserInfo(userCode);
            if (user == null)
                retVal = 0;
            else if (user.AllowEdit == "N")
                retVal = 1;
            else
            {
                IMapper map = DatabaseInstance.Instance();

                //删除时先从引用表删除，再从主表删除
                //map.Execute("delete from USER_ROLE where USER_CODE = @UserCode", new { UserCode = userCode });
                map.Execute("UPDATE USERS SET IS_DELETED = 1 where USER_CODE = @UserCode", new { UserCode = userCode });

                retVal = 2;
            }

            return retVal;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="USER_ID"></param>
        /// <returns></returns>
        public int ChangePassword(string userCode, string newPwd)
        {
            IMapper map = DatabaseInstance.Instance();
            return map.Execute("update USERS set PWD = @Pwd where USER_CODE = @UserCode", new { UserCode = userCode, Pwd = newPwd });
        }

        public DataTable ListUserState(string warehouseCode)
        {
            IMapper map = DatabaseInstance.Instance();
            string sql = string.Format("SELECT U.USER_CODE, U.USER_NAME, OL.IS_ONLINE, OL.UPDATE_DATE, " +
                "GROUP_CONCAT(R.ROLE_NAME ORDER BY R.ROLE_NAME ASC) ROLES ,wbc.ITEM_DESC " +
                "FROM USERS U " +
                "LEFT JOIN USER_ONLINE OL ON U.USER_CODE = OL.USER_CODE " +
                "LEFT JOIN USER_ROLE UR ON U.USER_CODE = UR.USER_CODE " +
                "LEFT JOIN ROLES R ON UR.ROLE_ID = R.ROLE_ID " +
                "LEFT JOIN tasks t ON t.USER_CODE=UR.USER_CODE " +
                "LEFT JOIN wm_base_code wbc ON wbc.ITEM_VALUE=t.TASK_TYPE " +
                "WHERE U.IS_ACTIVE = 'Y' AND U.WH_CODE = '{0}' AND IFNULL(U.IS_DELETED, 0) <> 1 GROUP BY U.USER_CODE", warehouseCode);
            return map.LoadTable(sql);
        }

        /// <summary>
        /// 检查用户编号是否已经存在
        /// </summary>
        /// <param name="USER_ID"></param>
        /// <returns></returns>
        private bool IsUserCodeExists(string userCode)
        {
            IMapper map = DatabaseInstance.Instance();
            string id = map.ExecuteScalar<string>("select USER_CODE from USERS where USER_CODE = @UserCode",
            new { UserCode = userCode });

            return !string.IsNullOrEmpty(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns>-2: 用户不存在，更新失败；-1：用户编号存在，新建失败；
        /// 0：写入或更新失败（写入失败理论上不可能，只能是异常；更新失败是该记录被别人删除了，）；1：成功</returns>
        public int  Save(UserEntity user, List<RoleEntity> myRoles, bool isCreateNew, string updateBy)
        {
            int ret = 0;

            //检查编号是否已经存在
            bool exists = IsUserCodeExists(user.UserCode);
            if (isCreateNew && exists)
                return -1;

            //更新时，检查用户是否存在
            if (!isCreateNew && !exists)
                return -2;

            IMapper map = DatabaseInstance.Instance();
            string sql = string.Format("insert into USERS(USER_CODE, USER_NAME, PWD, BRANCH_CODE, ALLOW_EDIT, IS_ACTIVE, WH_CODE, REMARK, UPDATE_BY, UPDATE_DATE, IS_OWN, MOBILE_PHONE, USER_TYPE, USER_ATTRI) " +
                    "values(@UserCode, @UserName, @Userpwd, @UserNamePY, @AllowEdit, @IsActive, @WhCode, @Remark, @LastUpdateBy, {0}, @IsOwn, @MobilePhone, @UserType, @UserAttri)", map.GetSysDateString());
            if (isCreateNew)//新增
            {
                ret = map.Execute(sql,
                new
                {
                    UserCode = user.UserCode,
                    UserName = user.UserName,
                    Userpwd = user.UserPwd,
                    UserNamePY = user.UserNamePY,
                    AllowEdit = user.AllowEdit,
                    IsActive = user.IsActive,
                    WhCode = user.WarehouseCode,
                    Remark = user.Remark,
                    LastUpdateBy = user.LastUpdateBy,
                    IsOwn = 'Y',
                    MobilePhone = user.MobilePhone,
                    UserType = user.UserType,
                    UserAttri = user.UserAttri
                });
            }
            else
            {
                sql = string.Format(
                    "update USERS set USER_NAME = @UserName, BRANCH_CODE = @UserNamePY, IS_ACTIVE = @IsActive, WH_CODE = @WhCode, " +
                    "REMARK = @Remark, UPDATE_BY = @LastUpdateBy, UPDATE_DATE = {0}, MOBILE_PHONE = @MobilePhone, USER_TYPE = @UserType, " +
                    "USER_ATTRI = @UserAttri " +
                    "where USER_CODE = @UserCode", map.GetSysDateString());
                ret = map.Execute(sql,
                new
                {
                    UserName = user.UserName,
                    UserNamePY = user.UserNamePY,
                    IsActive = user.IsActive,
                    WhCode = user.WarehouseCode,
                    Remark = user.Remark,
                    LastUpdateBy = user.LastUpdateBy,
                    UserCode = user.UserCode,
                    MobilePhone = user.MobilePhone,
                    UserType = user.UserType,
                    UserAttri = user.UserAttri
                });

                //更新用户信息时，需要删除原来跟角色的关联关系，然后重新添加关联
                map.Execute("delete from USER_ROLE where USER_CODE = @UserCode", new { UserCode = user.UserCode });
            }

            //添加与角色的关联
            foreach (RoleEntity role in myRoles)
                map.Execute("insert into USER_ROLE(USER_CODE, ROLE_ID) values(@UserCode, @RoleId)", new { UserCode = user.UserCode, RoleId = role.RoleId });

            return ret;
        }

        /// <summary>
        /// 改变用户的任务优先级
        /// </summary>
        /// <param name="taskList">任务列表</param>
        /// <returns></returns>
        public static int ChangeUserTaskLevel(List<TaskEntity> taskList)
        {
            int result = -1;
            IMapper map = DatabaseInstance.Instance();
            IDbTransaction trans = map.BeginTransaction();
            try
            {
                foreach (TaskEntity item in taskList)
                {
                    result += map.Execute(
                        @"UPDATE USER_ROLE R set R.ATTRI1 = @Attri, R.Attri2 = @Enable
                    WHERE R.USER_CODE = @UserCode AND R.ROLE_ID = @RoleID ",
                        new { Attri = item.UserAttri, UserCode = item.UserCode, RoleID = item.RoleID, Enable = item.RoleEnabled });
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            return result;
        }

        public static List<UserEntity> ListUserRolesByPick(string userCode)
        {
            string sql = @"SELECT U.USER_CODE, U.ROLE_ID, U.ATTRI1, U.ATTRI2, (CASE WHEN A.ROLE_NAME = '拣货员(整)' THEN '拣货任务(整)' ELSE '拣货任务(散)' END) ROLE_NAME
  FROM USER_ROLE U
  INNER JOIN (SELECT R.ROLE_ID, R.ROLE_NAME FROM ROLES R WHERE R.ROLE_NAME IN ('拣货员(散)', '拣货员(整)')) A ON A.ROLE_ID = U.ROLE_ID
  AND U.USER_CODE = @UserCode ";
            IMapper map = DatabaseInstance.Instance();
            return map.Query<UserEntity>(sql, new { UserCode = userCode });
        }

        #region 登录日志
        public int InsertLoginLog(LoginLogEntiy loginlog)
        {
            IMapper map = DatabaseInstance.Instance();
            int ret = -1;
            ret = map.Execute(
                string.Format("insert into LOGIN_LOG(USERCODE, IP, LOGINDATE, LOGINTYPE) " +
                "values(@USER_CODE, @IP, {0}, @LOGINTYPE)", map.GetSysDateString()),
            new
            {
                USER_CODE = loginlog.UserCode,
                IP = loginlog.IP,
                LOGINTYPE = loginlog.LoginType
            });
            return ret;
        }

        public List<LoginLogEntiy> ListLoginLogs(LoginLogEntiy log, DateTime DateFrom, DateTime DateTo)
        {
            IMapper map = DatabaseInstance.Instance();

            string sql = "select l.IP, l.LOGINDATE, l.LOGINTYPE, u.USER_CODE, u.USER_NAME from LOGIN_LOG l " +
                "inner join Users u on u.USER_CODE = l.USER_CODE " +
                "where (@IP is null or l.IP = @IP) and (@Name is null or u.USER_NAME = @Name) " +
                "and (@Code is null or u.USER_CODE = @Code) and (@DateFrom is null or l.LOGINDATE >= @DateFrom) " +
                "and (@DateTo is null or l.LOGINDATE <= @DateTo)";
            return map.Query<LoginLogEntiy>(sql,
                new
                {
                    IP = log.IP,
                    Name = log.UserName,
                    Code = log.UserCode,
                    DateFrom = DateFrom,
                    DateTo = DateTo
                });
        }

        #endregion

        /// <summary>
        /// 获取用户编号最大值（自动生成用户编号）
        /// </summary>
        /// <param name="ctType"></param>
        /// <returns></returns>
        public string GetMaxUserCode()
        {
            string sql = string.Format("SELECT u.USER_CODE FROM users u ORDER BY LENGTH(u.USER_CODE) DESC, u.USER_CODE DESC LIMIT 1;");
            object result = DatabaseInstance.Instance().ExecuteScalar<object>(sql);
            return Utils.ConvertUtil.ToString(result);
        }

        /// <summary>
        /// 根据库房编号获取用户编号最大值（自动生成用户编号）
        /// </summary>
        /// <param name="ctType"></param>
        /// <returns></returns>
        public string GetMaxUserCode(string wareHouseCode)
        {
            string str = @"SELECT MAX(CAST(SUBSTRING(A.USER_CODE,LOCATE('{0}',A.USER_CODE)+LENGTH('{0}'))AS SIGNED)) FROM(
SELECT u.USER_CODE FROM users u WHERE u.USER_CODE LIKE '%{0}%') A;";
            string sql = string.Format(str, wareHouseCode);
            object result = DatabaseInstance.Instance().ExecuteScalar<object>(sql);
            string resultStr = Utils.ConvertUtil.ToString(result);
            return string.IsNullOrEmpty(resultStr) ? "0" : resultStr;
        }


        /// <summary>
        /// 考勤登记
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="onlineType"></param>
        /// <returns>1:成功；-1：员工号不存在; -2:密码错误</returns>
        public static int LoginRegister(string userID, string onlineType, string userPwd)
        {
            IMapper map = DatabaseInstance.Instance();
            DynamicParameters p = new DynamicParameters();

            p.Add("V_USER_CODE", userID);
            p.Add("V_USER_PWD", userPwd);
            p.Add("V_ONLINE_TYPE", onlineType);
            p.AddOut("V_RESULT",DbType.Int32);

            return map.Execute("P_SYS_LOGIN_REGISTER",p, CommandType.StoredProcedure);
        }
    }
}