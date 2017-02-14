using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
//using Nodes.DBHelper;
using Nodes.Shares;
using Nodes.UI;
using Nodes.Utils;
using Nodes.Entities;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.SystemManage;
using Nodes.Entities.HttpEntity.Stock;
using Newtonsoft.Json;

namespace Nodes.SystemManage
{
    public partial class FrmUserState : DevExpress.XtraEditors.XtraForm
    {
        //Y：签到；N：签退
        private string registerType = "N";
        //UserDal userDal = new UserDal();

        public FrmUserState()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 人员状态表
        /// </summary>
        /// <param name="warehouseCode"></param>
        /// <returns></returns>
        public DataTable ListUserState(string warehouseCode)
        {
            #region
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("USER_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("USER_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("IS_ONLINE", Type.GetType("System.String"));
            tblDatas.Columns.Add("UPDATE_DATE", Type.GetType("System.String"));
            tblDatas.Columns.Add("ROLES", Type.GetType("System.String"));
            if (GlobeSettings.LoginedUser.WarehouseType != EWarehouseType.散货仓)
                tblDatas.Columns.Add("ITEM_DESC", Type.GetType("System.String"));
            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("whCode=").Append(warehouseCode).Append("&");
                loStr.Append("warehouseType=").Append(EUtilStroreType.WarehouseTypeToInt(GlobeSettings.LoginedUser.WarehouseType));
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_ListUserState);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonListUserState bill = JsonConvert.DeserializeObject<JsonListUserState>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return tblDatas;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return tblDatas;
                }
                #endregion

                #region 赋值
                foreach (JsonListUserStateResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["USER_CODE"] = tm.userCode;
                    newRow["USER_NAME"] = tm.userName;
                    newRow["IS_ONLINE"] = tm.isOnline;
                    newRow["UPDATE_DATE"] = tm.updateDate;
                    newRow["ROLES"] = tm.roles;
                    if (GlobeSettings.LoginedUser.WarehouseType != EWarehouseType.散货仓)
                        newRow["ITEM_DESC"] = tm.itemDesc;
                    tblDatas.Rows.Add(newRow);
                }
                return tblDatas;
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return tblDatas;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            try
            {
                bindingSource1.DataSource = ListUserState(GlobeSettings.LoginedUser.WarehouseCode);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
        /// <summary>
        /// 刷新按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OnFormLoad(null, null);
        }

        #region 插入日志记录
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="type">日志类型</param>
        /// <param name="creator">当前操作人</param>
        /// <param name="billNo">订单编号</param>
        /// <param name="description">操作描述</param>
        /// <param name="module">模块</param>
        /// <param name="createTime">创建时间</param>
        /// <param name="remark">备注信息</param>
        /// <returns></returns>
        public  bool Insert(ELogType type, string creator, string billNo, string description,
            string module, DateTime createTime, string remark)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("type=").Append(type).Append("&");
                loStr.Append("creator=").Append(creator).Append("&");
                loStr.Append("billNo=").Append(billNo).Append("&");
                loStr.Append("description=").Append(description).Append("&");
                loStr.Append("module=").Append(module).Append("&");
                loStr.Append("remark=").Append(remark);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_Insert);
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
        public  bool Insert(ELogType type, string creator, string billNo, string description,
            string module, string remark)
        {
            return Insert(type, creator, billNo, description, module, DateTime.Now, remark);
        }
        public  bool Insert(ELogType type, string creator, string billNo, string description,
            string module)
        {
            return Insert(type, creator, billNo, description, module, DateTime.Now, null);
        }
        public  bool Insert(ELogType type, string creator, string billNo, string module)
        {
            return Insert(type, creator, billNo, string.Empty, module, DateTime.Now, null);
        }
        #endregion

        /// <summary>
        /// 获取一个用户的详细信息
        /// </summary>
        /// <param name="USER_ID"></param>
        /// <returns></returns>
        public UserEntity GetUserInfo(string userCode)
        {
            UserEntity list = new UserEntity();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("userCode=").Append(userCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetUserInfo);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetUserInfo bill = JsonConvert.DeserializeObject<JsonGetUserInfo>(jsonQuery);
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

                foreach (JsonGetUserInfoResult tm in bill.result)
                {
                    #region 0-10
                    list.AllowEdit = tm.allowEdit;
                    list.BranchCode = tm.branchCode;
                    list.CenterWarehouseCode = tm.centerWhCode;
                    list.IsActive = tm.isActive;
                    list.IsCenter = Convert.ToInt32(tm.isCenterWh);
                    list.IsOwn = tm.isOwn;
                    list.MobilePhone = tm.mobilePhone;
                    list.UserPwd = tm.pwd;
                    list.Remark = tm.remark;
                    list.LastUpdateBy = tm.updateBy;
                    #endregion

                    #region 11-15
                    list.UserCode = tm.userCode;
                    list.UserName = tm.userName;
                    list.WarehouseCode = tm.whCode;
                    list.WarehouseName = tm.whName;
                    #endregion

                    if (!string.IsNullOrEmpty(tm.updateDate))
                        list.LastUpdateDate = Convert.ToDateTime(tm.updateDate);
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
        /// 考勤登记
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="onlineType"></param>
        /// <returns>1:成功；-1：员工号不存在; -2:密码错误</returns>
        public bool LoginRegister(string userID, string onlineType, string userPwd)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("userCode=").Append(userID).Append("&");
                loStr.Append("onlineType=").Append(onlineType).Append("&");
                loStr.Append("userPwd=").Append(userPwd).Append("&");
                loStr.Append("wareHouseType=").Append(EUtilStroreType.WarehouseTypeToInt(GlobeSettings.LoginedUser.WarehouseType));
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_LoginRegister);
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
        /// 签退按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(gridView1.SelectedRowsCount < 1)
            {
                MsgBox.Warn("请你选择要签退的人员！");
                return;
            }

            FrmTempAuthorize fta = new FrmTempAuthorize("管理员");
            if (fta.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            DataRowView user1 = gridView1.GetRow(gridView1.FocusedRowHandle) as DataRowView;

            UserEntity user2 = GetUserInfo(user1["USER_CODE"].ToString());

            bool result = LoginRegister(user2.UserCode, registerType, user2.UserPwd);

            if (!result)
            {
                //MsgBox.Warn("该人员有未处理完的任务，不允许签退！");
                return;
            }

            Insert(ELogType.签退, GlobeSettings.LoginedUser.UserName, user2.UserCode, "pc签退");

            OnFormLoad(null, null);
            
        }


    }
}