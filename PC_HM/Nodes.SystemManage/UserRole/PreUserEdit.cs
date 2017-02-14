using System;
using System.Collections.Generic;
using System.Text;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.UI;
using Nodes.Utils;
using Nodes.Shares;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.SystemManage;
using Newtonsoft.Json;

namespace Nodes.SystemManage
{
    public class PreUserEdit
    {
        //private UserDal userDal = null;
        private IvUserEdit vUserEdit = null;
        public UserEntity User = null;

        public PreUserEdit(IvUserEdit vUserEdit)
        {
            this.vUserEdit = vUserEdit;
            //userDal = new UserDal();
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

        /// <summary>
        /// 用户管理---根据userCode查询角色信息
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public List<RoleEntity> ListUserRoles(string userCode)
        {
            List<RoleEntity> list = new List<RoleEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("userCode=").Append(userCode);
                string jsons = string.Empty;
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_ListUserRoles);
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

        public void GetRolesAndBindToGrid()
        {
            //RoleDal roleDal = new RoleDal();
            List<RoleEntity> roles = null;
            if (GlobeSettings.LoginedUser.UserCode == "000999")
                roles = ListRoles();
            else
                roles = ListUserRoles(GlobeSettings.LoginedUser.UserCode);
            vUserEdit.BindRoleToGrid(roles);
        }

        #region List转换成Json
        private string GetRes<T>(List<T> listobj, List<string> proptylist)
        {

            StringBuilder strb = new StringBuilder();
            List<string> result = new List<string>();
            string curname = default(string);
            foreach (var obj in listobj)
            {

                Type type = obj.GetType();

                curname = type.Name;


                List<string> curobjliststr = new List<string>();
                foreach (var curpropty in proptylist)
                {
                    string tmp = default(string);
                    var res01 = type.GetProperty(curpropty).GetValue(obj, null);
                    if (res01 == null)
                    {
                        tmp = null;
                    }
                    else
                    {
                        tmp = res01.ToString();
                    }
                    curobjliststr.Add("\"" + curpropty + "\"" + ":" + "\"" + tmp + "\"");
                }
                string curres = "{" + string.Join(",", curobjliststr.ToArray()) + "}";
                result.Add(curres);
            }
            strb.Append(":[" + string.Join(",", result.ToArray()) + "]");
            string ret = "\"" + curname + "\"" + strb.ToString();
            ret = ret.Insert(0, "{");
            ret = ret.Insert(ret.Length, "}");
            return ret;
        }


        private string GetResList<T>(List<T> listobj, List<string> proptylist)
        {

            StringBuilder strb = new StringBuilder();
            List<string> result = new List<string>();
            string curname = default(string);
            foreach (var obj in listobj)
            {

                Type type = obj.GetType();

                curname = type.Name;


                List<string> curobjliststr = new List<string>();
                foreach (var curpropty in proptylist)
                {
                    string tmp = default(string);
                    var res01 = type.GetProperty(curpropty).GetValue(obj, null);
                    if (res01 == null)
                    {
                        tmp = null;
                    }
                    else
                    {
                        tmp = res01.ToString();
                    }
                    curobjliststr.Add("\"" + curpropty + "\"" + ":" + "\"" + tmp + "\"");
                }
                string curres = "{" + string.Join(",", curobjliststr.ToArray()) + "}";
                result.Add(curres);
            }

            //strb.Append(":[" + string.Join(",", result.ToArray()) + "]");
            //string ret = "\""+ curname + "\"" + strb.ToString();
            //ret = ret.Insert(0, "{");
            //ret = ret.Insert(ret.Length, "}");
            return string.Join(",", result.ToArray());
        }

        private string GetResList<T>(string josnName, List<T> listobj, List<string> proptylist)
        {

            StringBuilder strb = new StringBuilder();
            List<string> result = new List<string>();
            string curname = default(string);
            foreach (var obj in listobj)
            {

                Type type = obj.GetType();

                curname = type.Name;


                List<string> curobjliststr = new List<string>();
                foreach (var curpropty in proptylist)
                {
                    string tmp = default(string);
                    var res01 = type.GetProperty(curpropty).GetValue(obj, null);
                    if (res01 == null)
                    {
                        tmp = null;
                    }
                    else
                    {
                        tmp = res01.ToString();
                    }
                    curobjliststr.Add("\"" + curpropty + "\"" + ":" + "\"" + tmp + "\"");
                }
                string curres = "{" + string.Join(",", curobjliststr.ToArray()) + "}";
                result.Add(curres);
            }

            strb.Append(":[" + string.Join(",", result.ToArray()) + "]");
            string ret = "\"" + josnName + "\"" + strb.ToString();
            //ret = ret.Insert(0, "{");
            //ret = ret.Insert(ret.Length, "}");
            return ret;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns>-2: 用户不存在，更新失败；-1：用户编号存在，新建失败；
        /// 0：写入或更新失败（写入失败理论上不可能，只能是异常；更新失败是该记录被别人删除了，）；1：成功</returns>
        public bool SaveInsertUsers(UserEntity user, List<RoleEntity> myRoles, bool isCreateNew, string updateBy)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                #region 
                loStr.Append("userCode=").Append(user.UserCode).Append("&");
                loStr.Append("userName=").Append(user.UserName).Append("&");
                loStr.Append("userPwd=").Append(user.UserPwd).Append("&");
                loStr.Append("userNamePy=").Append(user.UserNamePY).Append("&");
                loStr.Append("allowEdit=").Append(user.AllowEdit).Append("&");
                loStr.Append("isActive=").Append(user.IsActive).Append("&");
                loStr.Append("whCode=").Append(user.WarehouseCode).Append("&");
                loStr.Append("remark=").Append(user.Remark).Append("&");
                loStr.Append("lastUpdateBy=").Append(user.LastUpdateBy).Append("&");
                loStr.Append("isOwn=").Append(user.IsOwn).Append("&");
                loStr.Append("mobilePhone=").Append(user.MobilePhone).Append("&");
                loStr.Append("userType=").Append(user.UserType).Append("&");
                loStr.Append("userAttri=").Append(user.UserAttri).Append("&");
                loStr.Append("updateBy=").Append(updateBy).Append("&");
                loStr.Append("isCreateNew=").Append(isCreateNew).Append("&");

                #region jsons
                List<string> prop = new List<string>() { "RoleId" };
                string jsonStr = GetResList<RoleEntity>("jsonStr", myRoles, prop);
                jsonStr = "{" + jsonStr + "}";
                #endregion

                loStr.Append("jsonStr=").Append(jsonStr);
                #endregion
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveInsertUsers);
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

        public bool SaveUser(List<RoleEntity> myRoles)
        {
            UserEntity user = vUserEdit.PrepareSave();

            bool isCreateNew = vUserEdit.IsCreateNew();
            bool ret = SaveInsertUsers(user, myRoles, isCreateNew, GlobeSettings.LoginedUser.UserName);
            User = user;
            return ret;
        }
    }
}