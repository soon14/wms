using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using Nodes.DBHelper;
using Nodes.Shares;
using Nodes.Entities;
using DevExpress.XtraEditors;
using Nodes.UI;
using Nodes.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Instore;
using Nodes.Entities.HttpEntity.Reports;
using Nodes.Entities.HttpEntity.SystemManage;
using Newtonsoft.Json;

namespace Nodes.SystemManage
{
    /// <summary>
    /// 人员调度
    /// </summary>
    public partial class FrmPersonalScheduling : DevExpress.XtraEditors.XtraForm
    {
        #region 构造函数

        public FrmPersonalScheduling()
        {
            InitializeComponent();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 收货单据管理， baseCode信息查询(用于业务类型和单据状态筛选条件)
        /// 获取活动状态的集合
        /// </summary>
        /// <param name="groupCode"></param>
        /// <returns></returns>
        public  List<BaseCodeEntity> GetItemList(string groupCode)
        {
            List<BaseCodeEntity> list = new List<BaseCodeEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("groupCode=").Append(groupCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetItemList);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonBaseCodeInfo bill = JsonConvert.DeserializeObject<JsonBaseCodeInfo>(jsonQuery);
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
                foreach (JsonBaseCodeInfoResult jbr in bill.result)
                {
                    BaseCodeEntity asnEntity = new BaseCodeEntity();
                    asnEntity.GroupCode = jbr.groupCode;
                    asnEntity.IsActive = jbr.isActive;
                    asnEntity.ItemDesc = jbr.itemDesc;
                    asnEntity.ItemValue = jbr.itemValue;
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
        /// 获取任务列表
        /// </summary>
        private void LoadTasks()
        {
            this.lookUpEdit1.Properties.DataSource = null;
            List<BaseCodeEntity> list = GetItemList("114");
            list.Insert(0, new BaseCodeEntity()
            {
                ItemDesc = "所有",
                ItemValue = "0"
            });
            this.lookUpEdit1.Properties.DataSource = list;
            this.lookUpEdit1.Properties.DisplayMember = "ItemDesc";
            this.lookUpEdit1.Properties.ValueMember = "ItemValue";
            this.lookUpEdit1.ItemIndex = 0;
        }

        /// <summary>
        /// 根据用户编号获取该用户可执行的任务,优先级等
        /// </summary>
        /// <param name="userCode">用户编号</param>
        /// <returns></returns>
        public  List<TaskEntity> GetTaskByUserCode(string userCode)
        {
            List<TaskEntity> list = new List<TaskEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("userCode=").Append(userCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetTaskByUserCode);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetTaskByUserCode bill = JsonConvert.DeserializeObject<JsonGetTaskByUserCode>(jsonQuery);
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
                foreach (JsonGetTaskByUserCodeResult jbr in bill.result)
                {
                    TaskEntity asnEntity = new TaskEntity();
                    asnEntity.TaskName = jbr.itemDesc;
                    asnEntity.RoleEnabled = Convert.ToBoolean(jbr.roleEnabled);
                    asnEntity.RoleID = Convert.ToInt32(jbr.roleId);
                    asnEntity.RoleName = jbr.roleName;
                    asnEntity.TaskTypeNo = Convert.ToInt32(jbr.taskTypeNo);
                    asnEntity.UserAttri = Convert.ToInt32(jbr.uAttri);
                    asnEntity.UserCode = jbr.userCode;
                    asnEntity.UserName = jbr.userName;
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
        /// 查询可执行任务人员
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public  List<UserEntity> ListUserRolesByPick(string userCode)
        {
            List<UserEntity> list = new List<UserEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("userCode=").Append(userCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_ListUserRolesByPick);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonListUserRolesByPick bill = JsonConvert.DeserializeObject<JsonListUserRolesByPick>(jsonQuery);
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
                foreach (JsonListUserRolesByPickResult jbr in bill.result)
                {
                    UserEntity asnEntity = new UserEntity();
                    asnEntity.Attri1 = Convert.ToInt32(jbr.attri1);
                    asnEntity.Attri2 = Convert.ToBoolean(jbr.attri2);
                    asnEntity.ROLE_ID = Convert.ToInt32(jbr.roleId);
                    asnEntity.UserCode = jbr.userCode;
                    asnEntity.RoleName = jbr.roleName;
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
        /// 根据人员可执行的任务
        /// </summary>
        private void LoadTaskBindRole(string userCode)
        {
            this.gridControl2.DataSource = null;
            List<TaskEntity> taskList = GetTaskByUserCode(userCode);
            List<TaskEntity> findList = taskList.FindAll(u => u.TaskTypeNo == 143);
            if (findList != null || findList.Count > 0)
            {
                List<UserEntity> tempList = ListUserRolesByPick(userCode);
                if (tempList != null && tempList.Count > 0)
                {
                    foreach (UserEntity entity in tempList)
                    {
                        foreach (TaskEntity item in findList)
                        {
                            if (item.RoleID == entity.ROLE_ID)
                            {
                                taskList.Add(new TaskEntity()
                                {
                                    UserCode = item.UserCode,
                                    UserName = item.UserName,
                                    TaskType = item.TaskType,
                                    UserAttri = entity.Attri1,
                                    RoleID = entity.ROLE_ID,
                                    RoleEnabled = entity.Attri2,
                                    TaskName = entity.RoleName
                                });
                                taskList.Remove(item);
                                break;
                            }
                        }
                    }
                }
            }
            this.gridControl2.DataSource = taskList;
        }
        #endregion

        #region Override Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.LoadTasks();
            this.btnQuery_Click(this.btnQuery, EventArgs.Empty);
        }
        #endregion

        #region 事件

        /// <summary>
        /// 调整指定任务的优先级
        /// </summary>
        private void btnChangeTaskLevel_Click(object sender, EventArgs e)
        {
            SimpleButton button = sender as SimpleButton;
            if (button == null)
                return;
            try
            {
                string tag = button.Tag as string;
                List<TaskEntity> taskList = this.gridView2.DataSource as List<TaskEntity>;
                TaskEntity taskEntity = this.gridView2.GetFocusedRow() as TaskEntity;
                UserEntity userEntity = this.gridView1.GetFocusedRow() as UserEntity;
                if (taskEntity == null || userEntity == null || taskList == null)
                    return;
                //TaskEntity tempTask = null;
                switch (tag)
                {
                    case "加":
                        if (taskList.Count == taskEntity.UserAttri)
                            return;
                        taskEntity.UserAttri++;
                        //tempTask = taskList.Find(u => u.UserAttri == taskEntity.UserAttri && u.RoleID != taskEntity.RoleID);
                        //if (tempTask != null)
                        //    tempTask.UserAttri--;
                        break;
                    case "减":
                        if (taskEntity.UserAttri == 0)
                            return;
                        taskEntity.UserAttri--;
                        //tempTask = taskList.Find(u => u.UserAttri == taskEntity.UserAttri && u.RoleID != taskEntity.RoleID);
                        //if (tempTask != null)
                        //    tempTask.UserAttri++;
                        break;
                }
                this.gridControl2.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
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
        /// 改变用户的任务优先级
        /// </summary>
        /// <param name="taskList">任务列表</param>
        /// <returns></returns>
        public bool ChangeUserTaskLevel(List<TaskEntity> taskList)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                List<string> prop = new List<string>() { "UserAttri", "UserCode", "RoleID", "RoleEnabled" };
                string jsonStr = GetResList<TaskEntity>("jsonStr", taskList, prop);
                jsonStr = "{" + jsonStr + "}";
                loStr.Append("jsonStr=").Append(jsonStr);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_ChangeUserTaskLevel);
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

        /// <summary>
        /// 应用
        /// </summary>
        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                List<TaskEntity> taskList = this.gridView2.DataSource as List<TaskEntity>;
                UserEntity userEntity = this.gridView1.GetFocusedRow() as UserEntity;
                if (taskList == null || taskList.Count == 0 || userEntity == null)
                    return;
                if (ChangeUserTaskLevel(taskList))
                {
                    this.LoadTaskBindRole(userEntity.UserCode);
                }
                else
                {
                    MsgBox.Err("修改失败!");
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
        /// <summary>
        /// 选择人员时显示该人员拥有的角色(绑定的任务)
        /// </summary>
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.gridControl2.DataSource = null;
            UserEntity entity = this.gridView1.GetFocusedRow() as UserEntity;
            if (entity == null)
                return;
            this.LoadTaskBindRole(entity.UserCode);
        }

        /// <summary>
        /// 查询统计（任务调度统计－获取某个库房下面的所有的拥有任务角色的人员
        /// </summary>
        /// <param name="warehouseCode"></param>
        /// <param name="baseCode"></param>
        /// <returns></returns>
        public  List<UserEntity> ListUsersByWarehouseCodeAndTask(string warehouseCode, string baseCode)
        {
            List<UserEntity> list = new List<UserEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("warehouseCode=").Append(warehouseCode).Append("&");
                loStr.Append("baseCode=").Append(baseCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_ListUsersByWarehouseCodeAndTask);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonListUsersByWarehouseCodeAndTask bill = JsonConvert.DeserializeObject<JsonListUsersByWarehouseCodeAndTask>(jsonQuery);
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


                #region 赋值
                foreach (JsonListUsersByWarehouseCodeAndTaskResult tm in bill.result)
                {
                    UserEntity sku = new UserEntity();
                    sku.AllowEdit = tm.allowEdit;
                    sku.Attri2 = Convert.ToBoolean(tm.attri2);
                    sku.IsOnline = tm.isOnLine;
                    sku.MobilePhone = tm.mobilePhone;
                    sku.Remark = tm.remark;
                    sku.RoleNameListStr = tm.roleList;
                    sku.RoleName = tm.roleName;
                    sku.UserCode = tm.userCode;
                    sku.UserName = tm.userName;
                    sku.UserPwd = tm.pwd;
                    sku.WarehouseCode = tm.whCode;
                    sku.WarehouseName = tm.whName;

                    list.Add(sku);
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
        /// 根据选择人任务,查询可执行该任务的人员
        /// </summary>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            string baseCode = this.lookUpEdit1.EditValue as string;
            if (baseCode == null)
                return;
            try
            {
                List<UserEntity> userList = ListUsersByWarehouseCodeAndTask(
                GlobeSettings.LoginedUser.WarehouseCode, baseCode);
                this.gridControl1.DataSource = userList;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
        #endregion
    }
}
