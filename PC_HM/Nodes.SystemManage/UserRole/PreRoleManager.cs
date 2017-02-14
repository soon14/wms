using System;
using System.Collections.Generic;
using System.Text;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Shares;
using Nodes.Utils;
using Nodes.UI;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.SystemManage;
using Newtonsoft.Json;

namespace Nodes.SystemManage
{
    public class PreRoleManager
    {
        private IvRoleManager vRoleManager = null;
        //private RoleDal roleDal = null;

        public PreRoleManager(IvRoleManager vRoleManager)
        {
            this.vRoleManager = vRoleManager;
            //roleDal = new RoleDal();
        }

        /// <summary>
        /// 系统管理--角色管理--查询
        /// </summary>
        /// <returns></returns>
        public List<RoleEntity> ListRoles()
        {
            List<RoleEntity> list = new List<RoleEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("vhNo=").Append(vehicleNO);
                string jsons = string.Empty;
                string jsonQuery = WebWork.SendRequest(jsons, WebWork.URL_ListRoles);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonListRoles bill = JsonConvert.DeserializeObject<JsonListRoles>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return list;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return list;
                }
                #endregion

                #region 赋值数据
                foreach (JsonListRolesResult jbr in bill.result)
                {
                    RoleEntity asnEntity = new RoleEntity();
                    asnEntity.RoleId = Convert.ToInt32(jbr.roleId);
                    asnEntity.AllowEdit = jbr.allowEdit;
                    asnEntity.RoleName = jbr.roleName;
                    asnEntity.Remark = jbr.remark;
                    list.Add(asnEntity);
                }
                return list;
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return list;
        }

        public void ListRolesAndBindToGrid()
        {
            List<RoleEntity> roles = ListRoles();
            vRoleManager.BindGrid(roles);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns>0：不允许删除；1：成功</returns>
        public bool DeleteRole(int roleId)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("roleId=").Append(roleId);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_DeleteRole);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return false;
                }
                #endregion

                #region 正常错误处理

                Sucess bill = JsonConvert.DeserializeObject<Sucess>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return false;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return false;
                }
                #endregion

                return true;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return false;
        }

        public bool DeleteRole(RoleEntity role)
        {
            return DeleteRole(role.RoleId);
        }
    }
}
