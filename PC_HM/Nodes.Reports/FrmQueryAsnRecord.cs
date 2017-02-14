using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nodes.Shares;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.UI;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;
using Nodes.Entities.HttpEntity.Reports;
using Newtonsoft.Json;

namespace Reports
{
    /// <summary>
    /// 收货绩效考核
    /// </summary>
    public partial class FrmQueryAsnRecord : DevExpress.XtraEditors.XtraForm
    {

        #region 构造函数

        public FrmQueryAsnRecord()
        {
            InitializeComponent();
        }

        #endregion

        /// <summary>
        /// 列出某个组织下面的某个角色的成员，例如保税库的发货员，状态必须是启用的
        /// </summary>
        /// <param name="warehouseCode"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public List<UserEntity> ListUsersByRoleAndWarehouseCode(string warehouseCode, string roleName)
        {
            List<UserEntity> list = new List<UserEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("warehouseCode=").Append(warehouseCode).Append("&");
                loStr.Append("roleName=").Append(roleName);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_ListUsersByRoleAndWarehouseCode);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonListUsersByRoleAndWarehouseCode bill = JsonConvert.DeserializeObject<JsonListUsersByRoleAndWarehouseCode>(jsonQuery);
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
                foreach (JsonListUsersByRoleAndWarehouseCodeResult jbr in bill.result)
                {
                    UserEntity asnEntity = new UserEntity();
                    asnEntity.AllowEdit = jbr.allowEdit;
                    asnEntity.IsActive = jbr.isActive;
                    asnEntity.IsOnline = jbr.isOnline;
                    asnEntity.MobilePhone = jbr.mobilePhone;
                    asnEntity.Remark = jbr.remark;
                    asnEntity.ROLE_ID = Convert.ToInt32(jbr.roleId);
                    asnEntity.UserCode = jbr.userCode;
                    asnEntity.UserName = jbr.userName;
                    asnEntity.WarehouseCode = jbr.whCode;
                    asnEntity.WarehouseName = jbr.whName;
                    asnEntity.UserPwd = jbr.pwd;
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

        #region 方法
        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            List<UserEntity> userList = ListUsersByRoleAndWarehouseCode(
                GlobeSettings.LoginedUser.WarehouseCode, "收货清点员");
            this.gridControl3.DataSource = userList;
        }
        #endregion

        #region Override Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            try
            {
                this.dateBegin.EditValue = DateTime.Now.AddMonths(-1);
                this.dateEnd.EditValue = DateTime.Now;
                this.LoadData();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// 查询统计（收货绩效考核－获取人员入库清点记录）
        /// </summary>
        /// <param name="dateBegin"></param>
        /// <param name="dateEnd"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public DataTable GetAsnRecords(DateTime dateBegin, DateTime dateEnd, string userCode)
        {
            #region DataTable
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("BILL_NO", Type.GetType("System.String"));
            tblDatas.Columns.Add("SKU_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("SKU_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("CHECK_QTY", Type.GetType("System.Decimal"));
            tblDatas.Columns.Add("UM_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("CREATE_DATE", Type.GetType("System.DateTime"));
            tblDatas.Columns.Add("CHECK_STATE", Type.GetType("System.String"));
            #endregion
            
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("userCode=").Append(userCode).Append("&");
                loStr.Append("beginDate=").Append(dateBegin).Append("&");
                loStr.Append("endDate=").Append(dateEnd);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetAsnRecords);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetAsnRecords bill = JsonConvert.DeserializeObject<JsonGetAsnRecords>(jsonQuery);
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
                foreach (JsonGetAsnRecordsResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["BILL_NO"] = tm.billNo;
                    newRow["SKU_CODE"] = tm.skuCode;
                    newRow["SKU_NAME"] = tm.skuName;
                    newRow["CHECK_QTY"] = Convert.ToDecimal(tm.checkQty);
                    newRow["UM_NAME"] = tm.umName;
                    newRow["CREATE_DATE"] = Convert.ToDateTime(tm.createDate);
                    newRow["CHECK_STATE"] = tm.checkState;
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

        /// <summary>
        /// 查询统计（收货绩效考核－获取crn记录）
        /// </summary>
        /// <param name="dateBegin"></param>
        /// <param name="dateEnd"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public  DataTable GetCrnRecords(DateTime dateBegin, DateTime dateEnd, string userCode)
        {
            #region DataTable
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("BILL_NO", Type.GetType("System.String"));
            tblDatas.Columns.Add("SKU_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("SKU_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("CHECK_QTY", Type.GetType("System.Decimal"));
            tblDatas.Columns.Add("UM_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("CREATE_DATE", Type.GetType("System.DateTime"));
            tblDatas.Columns.Add("CHECK_STATE", Type.GetType("System.String"));
            #endregion
            
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("userCode=").Append(userCode).Append("&");
                loStr.Append("beginDate=").Append(dateBegin).Append("&");
                loStr.Append("endDate=").Append(dateEnd);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetCrnRecords);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetAsnRecords bill = JsonConvert.DeserializeObject<JsonGetAsnRecords>(jsonQuery);
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
                foreach (JsonGetAsnRecordsResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["BILL_NO"] = tm.billNo;
                    newRow["SKU_CODE"] = tm.skuCode;
                    newRow["SKU_NAME"] = tm.skuName;
                    newRow["CHECK_QTY"] = Convert.ToDecimal(tm.checkQty);
                    newRow["UM_NAME"] = tm.umName;
                    newRow["CREATE_DATE"] = Convert.ToDateTime(tm.createDate);
                    newRow["CHECK_STATE"] = tm.checkState;
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

        #region 事件

        private void gridView3_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                this.gridControl1.DataSource = null;
                UserEntity user = this.gridView3.GetFocusedRow() as UserEntity;
                if (user == null)
                    return;
                DateTime dateBegin = this.dateBegin.EditValue == null ? DateTime.Now : ConvertUtil.ToDatetime(this.dateBegin.EditValue);
                DateTime dateEnd = this.dateEnd.EditValue == null ? DateTime.Now : ConvertUtil.ToDatetime(this.dateEnd.EditValue);
                this.gridControl1.DataSource = GetAsnRecords(dateBegin, dateEnd, user.UserCode);
                this.gridControl2.DataSource = GetCrnRecords(dateBegin, dateEnd, user.UserCode);
                
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
        #endregion
    }
}
