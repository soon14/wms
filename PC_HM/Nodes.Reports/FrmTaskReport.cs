using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
//using Nodes.DBHelper;
using Nodes.Shares;
using Nodes.UI;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Reports;
using Newtonsoft.Json;

namespace Nodes.Reports
{
    public partial class FrmTaskReport : DevExpress.XtraEditors.XtraForm
    {
        #region 构造函数

        public FrmTaskReport()
        {
            InitializeComponent();
        }

        #endregion

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

        #region 方法
        private void LoadUsers()
        {
            this.gridControl3.DataSource = ListUsersByWarehouseCodeAndTask(
                GlobeSettings.LoginedUser.WarehouseCode, "0");
        }
        #endregion

        #region Override Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            try
            {
                this.dateBegin.EditValue = DateTime.Now.AddDays(-7);
                this.dateEnd.EditValue = DateTime.Now;
                this.LoadUsers();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// 查询统计（任务调度统计－根据userCode查询任务调度）
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public  List<TaskEntity> GetReport(DateTime beginDate, DateTime endDate, string userCode)
        {
            List<TaskEntity> list = new List<TaskEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("beginDate=").Append(beginDate).Append("&");
                loStr.Append("endDate=").Append(endDate).Append("&");
                loStr.Append("userCode=").Append(userCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetReport);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetReport bill = JsonConvert.DeserializeObject<JsonGetReport>(jsonQuery);
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
                foreach (JsonGetReportResult tm in bill.result)
                {
                    TaskEntity asnEntity = new TaskEntity();
                    asnEntity.BillID = Convert.ToInt32(tm.billId);
                    asnEntity.IsCase = Convert.ToInt32(tm.isCase);
                    asnEntity.TaskName = tm.itemDesc;
                    asnEntity.TaskID = Convert.ToInt32(tm.id);
                    asnEntity.TaskQtyDecimal = Convert.ToDecimal(tm.taskQty);
                    asnEntity.UserCode = tm.userCode;
                    asnEntity.UserName = tm.userName;

                    #region
                    try
                    {
                        if (!string.IsNullOrEmpty(tm.closeDate))
                            asnEntity.CloseDate = Convert.ToDateTime(tm.closeDate);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    }
                    #endregion

                    #region
                    try
                    {
                        if (!string.IsNullOrEmpty(tm.confirmDate))
                            asnEntity.ConfirmDate = Convert.ToDateTime(tm.confirmDate);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    }
                    #endregion

                    #region
                    try
                    {
                        if (!string.IsNullOrEmpty(tm.createDate))
                            asnEntity.CreateDate = Convert.ToDateTime(tm.createDate);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    }
                    #endregion

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

        #region 事件

        private void gridView3_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                this.gridControl1.DataSource = null;
                UserEntity user = this.gridView3.GetFocusedRow() as UserEntity;
                if (user == null)
                    return;
                DateTime dateBegin = this.dateBegin.EditValue == null ? DateTime.Now.Date : ConvertUtil.ToDatetime(this.dateBegin.EditValue).Date;
                DateTime dateEnd = this.dateEnd.EditValue == null ? DateTime.Now.AddDays(1).Date.AddSeconds(-1) : ConvertUtil.ToDatetime(this.dateEnd.EditValue).AddDays(1).Date.AddSeconds(-1);
                List<TaskEntity> taskList = GetReport(dateBegin, dateEnd, user.UserCode);
                this.gridControl1.DataSource = taskList;
                decimal avgQty = taskList.Count == 0 ? 0 : taskList.Sum(u => u.TaskQtyDecimal) / taskList.Count;
                double avgTime = taskList.Count == 0 ? 0 : taskList.Sum(u => u.HistoryUseTime) / taskList.Count;
                this.gridView1.ViewCaption = string.Format(
                    "调度统计(平均数量：{0:f0}  平均用时：{1:f2})", avgQty, avgTime);

            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        #endregion
    }
}
