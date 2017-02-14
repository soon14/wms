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
using Nodes.Entities;
using Nodes.UI;
using Nodes.Utils;
using DevExpress.XtraGrid;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;
using Nodes.Entities.HttpEntity.Reports;
using Newtonsoft.Json;

namespace Reports
{
    /// <summary>
    /// 移货/上架记录
    /// </summary>
    public partial class FrmQueryTransRecord : XtraForm
    {
        #region 变量
        private static GridColumnSummaryItem Grid2Item0 = null;
        #endregion

        #region 构造函数

        public FrmQueryTransRecord()
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
            List<UserEntity> userList = ListUsersByRoleAndWarehouseCode(GlobeSettings.LoginedUser.WarehouseCode, "叉车司机");
            this.gridControl3.DataSource = userList;
        }
        #endregion

        #region Override Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.dateBegin.EditValue = DateTime.Now.AddMonths(-1);
            this.dateEnd.EditValue = DateTime.Now;
            this.LoadData();
        }
        #endregion

        #region 事件

        /// <summary>
        /// 查询统计（叉车司机任务统计－查询移货记录）
        /// </summary>
        /// <param name="dateBegin"></param>
        /// <param name="dateEnd"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public  DataTable QueryTransRecords(DateTime dateBegin, DateTime dateEnd, string userCode)
        {
            #region DataTable
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("FROM_LC_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("TO_LC_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("UM_QTY", Type.GetType("System.Decimal"));
            tblDatas.Columns.Add("UM_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("CREATE_DATE", Type.GetType("System.DateTime"));
            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                #region 参数
                loStr.Append("userCode=").Append(userCode).Append("&");
                loStr.Append("beginDate=").Append(dateBegin).Append("&");
                loStr.Append("endDate=").Append(dateEnd);
                #endregion

                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_QueryTransRecordsChaChe);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonQueryTransRecords bill = JsonConvert.DeserializeObject<JsonQueryTransRecords>(jsonQuery);
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
                foreach (JsonQueryTransRecordsResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["FROM_LC_CODE"] = tm.fromLcCode;
                    newRow["TO_LC_CODE"] = tm.toLcCode;
                    newRow["UM_QTY"] = Convert.ToDecimal(tm.umQty);
                    newRow["UM_NAME"] = tm.umName;
                    newRow["CREATE_DATE"] = Convert.ToDateTime(tm.createDate);
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
        /// 查询统计（叉车司机任务统计－获取上架记录）
        /// </summary>
        /// <param name="dateBegin"></param>
        /// <param name="dateEnd"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public  DataTable GetPutawayRecords(DateTime dateBegin, DateTime dateEnd, string userCode)
        {
            #region DataTable
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("BILL_DETAIL_ID", Type.GetType("System.String"));
            tblDatas.Columns.Add("CT_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("SKU_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("SKU_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("LC_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("QTY", Type.GetType("System.Decimal"));
            tblDatas.Columns.Add("UM_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("PUT_TIME", Type.GetType("System.DateTime"));
            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                #region 参数
                loStr.Append("userCode=").Append(userCode).Append("&");
                loStr.Append("beginDate=").Append(dateBegin).Append("&");
                loStr.Append("endDate=").Append(dateEnd);
                #endregion

                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetPutawayRecords);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetPutawayRecords bill = JsonConvert.DeserializeObject<JsonGetPutawayRecords>(jsonQuery);
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
                foreach (JsonGetPutawayRecordsResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["BILL_DETAIL_ID"] = tm.billDetailId;
                    newRow["CT_CODE"] = tm.ctCode;
                    newRow["SKU_CODE"] = tm.skuCode;
                    newRow["SKU_NAME"] = tm.skuName;
                    newRow["LC_CODE"] = tm.lcCode;
                    if (!string.IsNullOrEmpty(tm.qty))
                        newRow["QTY"] = Convert.ToDecimal(tm.qty);
                    newRow["UM_NAME"] = tm.umName;
                    newRow["PUT_TIME"] = Convert.ToDateTime(tm.putTime);
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
        /// 查询统计（叉车司机任务统计－获取上架记录条数）
        /// </summary>
        /// <param name="dateBegin"></param>
        /// <param name="dateEnd"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public  int GetPutawayRecordsCount(DateTime dateBegin, DateTime dateEnd, string userCode)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("userCode=").Append(userCode).Append("&");
                loStr.Append("beginDate=").Append(dateBegin).Append("&");
                loStr.Append("endDate=").Append(dateEnd);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetPutawayRecordsCount);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return 0;
                }
                #endregion

                #region 正常错误处理

                JsonGetPutawayRecordsCount bill = JsonConvert.DeserializeObject<JsonGetPutawayRecordsCount>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return 0;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return 0;
                }
                #endregion

                if (bill.result != null && bill.result.Length > 0)
                    return Convert.ToInt32(bill.result[0].qty);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return 0;
        }

        private void gridView3_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                this.gridControl1.DataSource = null;
                this.gridControl2.DataSource = null;
                UserEntity user = this.gridView3.GetFocusedRow() as UserEntity;
                if (user == null)
                    return;
                DateTime dateBegin = this.dateBegin.EditValue == null ? DateTime.Now : ConvertUtil.ToDatetime(this.dateBegin.EditValue);
                DateTime dateEnd = this.dateEnd.EditValue == null ? DateTime.Now : ConvertUtil.ToDatetime(this.dateEnd.EditValue);
                this.gridControl1.DataSource = QueryTransRecords(
                    dateBegin, dateEnd, user.UserCode);
                this.gridControl2.DataSource = GetPutawayRecords(
                    dateBegin, dateEnd, user.UserCode);
                if (Grid2Item0 == null)
                {
                    Grid2Item0 = this.gridView2.Columns[0].Summary.Add(DevExpress.Data.SummaryItemType.Custom);
                }
                Grid2Item0.DisplayFormat = string.Format("共 {0} 次", 
                    GetPutawayRecordsCount(dateBegin, dateEnd, user.UserCode));
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        #endregion
    }
}
