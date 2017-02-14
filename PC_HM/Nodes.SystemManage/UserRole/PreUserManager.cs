using System;
using System.Collections.Generic;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.Shares;
using Nodes.UI;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;
using Newtonsoft.Json;

namespace Nodes.SystemManage
{
    public class PreUserManager
    {
        private IvUserManager vUserManager = null;
        //private UserDal userDal = null;

        public PreUserManager(IvUserManager vUserManager)
        {
            this.vUserManager = vUserManager;
            //this.userDal = new UserDal();
        }

        /// <summary>
        /// 车次信息---查询所有人员
        /// </summary>
        /// <returns></returns>
        public List<UserEntity> ListUsers()
        {
            List<UserEntity> list = new List<UserEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                string jsons = string.Empty;
                string jsonQuery = WebWork.SendRequest(jsons, WebWork.URL_ListUsers);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonListUsers bill = JsonConvert.DeserializeObject<JsonListUsers>(jsonQuery);
                if (bill == null)
                {
                    //MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return list;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return list;
                }
                #endregion

                #region 赋值数据
                foreach (JsonListUsersResult jbr in bill.result)
                {
                    UserEntity asnEntity = new UserEntity();
                    asnEntity.AllowEdit = jbr.allowEdit;
                    asnEntity.IsActive = jbr.isActive;
                    asnEntity.IsOnline = jbr.isOnline;
                    asnEntity.MobilePhone = jbr.mobilePhone;
                    asnEntity.Remark = jbr.remark;
                    asnEntity.UserCode = jbr.userCode;
                    asnEntity.UserName = jbr.userName;
                    asnEntity.WarehouseCode = jbr.whCode;
                    asnEntity.WarehouseName = jbr.whName;
                    asnEntity.UserPwd = jbr.pwd;
                    asnEntity.UserTypeStr = jbr.itemDesc;
                    asnEntity.UserAttri = jbr.userAttri;
                    asnEntity.UserAttriStr = jbr.bcItemDesc;
                    asnEntity.UserType = jbr.userType;
                    try
                    {
                        //if (!string.IsNullOrEmpty(jbr.updateDate))
                        //    asnEntity.UpdateDate = Convert.ToDateTime(jbr.updateDate);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    }
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

        public void ListUsersAndBindToGrid()
        {
            List<UserEntity> users = ListUsers();
            vUserManager.BindGrid(users);
        }

        /// <summary>
        /// 删除用户，先从用户角色关联表删除，再从用户表删除
        /// </summary>
        /// <param name="USER_ID"></param>
        /// <returns></returns>
        public bool DeleteUserZLM(string userCode)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("userCode=").Append(userCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_DeleteUserZLM);
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

        public void DeleteUser(UserEntity user)
        {
            try
            {
                bool ret = DeleteUserZLM(user.UserCode);
                if (ret)
                {
                    vUserManager.RemoveRowFromGrid(user);
                }
                //else if (ret == 0)
                //{
                //    MsgBox.Warn("该用户可能已经被其他人删除，删除失败。");
                //}
                //else if (ret == 1)
                //{
                //    MsgBox.Warn("这是系统预定义的用户，不允许删除。");
                //}
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="USER_ID"></param>
        /// <returns></returns>
        public bool ChangePassword(string userCode, string newPwd)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("userCode=").Append(userCode).Append("&");
                loStr.Append("newPwd=").Append(newPwd);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_ChangePassword);
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

        public void ResetPwd(UserEntity user)
        {
            try
            {
                bool ret = ChangePassword(user.UserCode, GlobeSettings.InitialPassword);
                if (ret)
                {
                    MsgBox.OK("密码已经重置成功，为了安全请及时更改此密码。");
                }
                //else
                //{
                //    MsgBox.Warn("该用户可能已经被其他人删除，重置密码失败。");
                //}
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
    }
}
