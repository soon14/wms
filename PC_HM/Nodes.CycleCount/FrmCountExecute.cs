using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Shares;
using Nodes.Utils;
using Nodes.UI;
using DevExpress.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.CycleCount;
using Newtonsoft.Json;

namespace Nodes.CycleCount
{
    public partial class FrmCountExecute : DevExpress.XtraEditors.XtraForm
    {

        public FrmCountExecute()
        {
            InitializeComponent();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// 盘点差异调整---读取差异调整单
        /// </summary>
        /// <param name="warehouse"></param>
        /// <param name="billStatus"></param>
        /// <returns></returns>
        public List<CountHeaderEntity> GetBills(string warehouse, string billStatus)
        {
            List<CountHeaderEntity> list = new List<CountHeaderEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("warehouse=").Append(warehouse).Append("&");
                loStr.Append("billStatus=").Append(billStatus);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetBills);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetBills bill = JsonConvert.DeserializeObject<JsonGetBills>(jsonQuery);
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
                foreach (JsonGetBillsResult jbr in bill.result)
                {
                    CountHeaderEntity asnEntity = new CountHeaderEntity();
                    asnEntity.BillID = Convert.ToInt32(jbr.billId);
                    asnEntity.BillState = jbr.billState;
                    asnEntity.Creator = jbr.creator;
                    asnEntity.Remark = jbr.remark;
                    asnEntity.Warehouse = jbr.whCode;
                    try
                    {
                        //if (!string.IsNullOrEmpty(jbr.closeDate))
                        //    asnEntity.CloseDate = Convert.ToDateTime(jbr.closeDate);
                        //if (!string.IsNullOrEmpty(jbr.printedTime))
                        //    asnEntity.PrintedTime = Convert.ToDateTime(jbr.printedTime);
                        if (!string.IsNullOrEmpty(jbr.createDate))
                            asnEntity.CreateDate = Convert.ToDateTime(jbr.createDate);
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

        private void LoadData()
        {
            //读取差异调整单
            List<CountHeaderEntity> headers = GetBills(GlobeSettings.LoginedUser.WarehouseCode, "等待调整,134");
            listBillNO.Properties.DataSource = headers;
        }

        /// <summary>
        /// 盘点单管理---报告上传
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public DataTable GetReportOnlyDiff(int billID)
        {
            #region DataTable
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("ZN_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("LC_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("SKU_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("COUNT_QTY", Type.GetType("System.Decimal"));
            tblDatas.Columns.Add("STOCK_QTY", Type.GetType("System.Decimal"));
            tblDatas.Columns.Add("DIFF_QTY", Type.GetType("System.Decimal"));
            tblDatas.Columns.Add("STOCK_EXP_DATE", Type.GetType("System.DateTime"));
            tblDatas.Columns.Add("EXP_DATE", Type.GetType("System.DateTime"));
            tblDatas.Columns.Add("SPEC", Type.GetType("System.String"));
            tblDatas.Columns.Add("REMARK", Type.GetType("System.String"));
            tblDatas.Columns.Add("UPLOADED", Type.GetType("System.Boolean"));
            tblDatas.Columns.Add("SKU_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("UM_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("IS_EFFECT", Type.GetType("System.String"));
            tblDatas.Columns.Add("ID", Type.GetType("System.Int32"));
            tblDatas.Columns.Add("BILL_ID", Type.GetType("System.Int32"));

            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetReportOnlyDiff);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetReportOnlyDiff bill = JsonConvert.DeserializeObject<JsonGetReportOnlyDiff>(jsonQuery);
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
                foreach (JsonGetReportOnlyDiffResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["ZN_NAME"] = tm.znName;
                    newRow["LC_CODE"] = tm.lcCode;
                    newRow["SKU_NAME"] = tm.skuName;
                    newRow["SKU_CODE"] = tm.skuCode;
                    newRow["COUNT_QTY"] = StringToDecimal.GetTwoDecimal(tm.countQty);
                    newRow["STOCK_QTY"] = StringToDecimal.GetTwoDecimal(tm.stockQty);
                    newRow["DIFF_QTY"] = StringToDecimal.GetTwoDecimal(tm.countQty) - StringToDecimal.GetTwoDecimal(tm.stockQty);
                    newRow["UM_NAME"] = tm.umName;
                    newRow["ID"] = Convert.ToInt32(tm.id);
                    newRow["BILL_ID"] = Convert.ToInt32(tm.billId);
                    newRow["IS_EFFECT"] = tm.ifEffect;
                    newRow["SPEC"] = tm.spec;
                    newRow["REMARK"] = tm.remark;
                    newRow["UPLOADED"] = Convert.ToBoolean(tm.uploaded);
                    if (!string.IsNullOrEmpty(tm.stockExpDate))
                        newRow["STOCK_EXP_DATE"] = tm.stockExpDate;
                    if (!string.IsNullOrEmpty(tm.expDate))
                        newRow["EXP_DATE"] = tm.expDate;
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

        private void listBillNO_EditValueChanged(object sender, EventArgs e)
        {
            CountHeaderEntity header = listBillNO.GetSelectedDataRow() as CountHeaderEntity;
            if (header != null)
            {
                labelControl3.Text = header.CreateDate.ToLongDateString();
                gridControl2.DataSource = GetReportOnlyDiff(header.BillID);
            }
        }

        /// <summary>
        /// 盘点差异调整
        /// </summary>
        /// <param name="id"></param>
        /// <param name="warehouse"></param>
        /// <returns></returns>
        public bool ExecuteStock(int id, string warehouse)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("id=").Append(id).Append("&");
                loStr.Append("warehouse=").Append(warehouse);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_ExecuteStock);
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
        /// 盘点差异调整--更新状态
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="billState"></param>
        /// <returns></returns>
        public bool UpdateBillState(int billID, string billState)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID).Append("&");
                loStr.Append("billState=").Append(billState);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_UpdateBillState);
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
        /// 盘点差异调整
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="billState"></param>
        /// <returns></returns>
        public bool CycleCountStockExecute(int billID)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID).Append("&");
                loStr.Append("warehouse=").Append(GlobeSettings.LoginedUser.WarehouseCode).Append("&");
                loStr.Append("userName=").Append(GlobeSettings.LoginedUser.UserName);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_CycleCountStockExecute);
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataTable data = gridControl2.DataSource as DataTable;
            if (data == null)
                return;
            if (MsgBox.AskOK("是否提交差异表")  != DialogResult.OK)
                return;

            try
            {
                using (WaitDialogForm tm = new WaitDialogForm("请稍等...", "正在执行库存调整"))
                {
                    #region
                    CountHeaderEntity header = listBillNO.GetSelectedDataRow() as CountHeaderEntity;
                    if (header == null)
                    {
                        MsgBox.Warn("盘点单无效！");
                        return;
                    }
                    if (!CycleCountStockExecute(header.BillID))
                    {
                        return;
                    }
                    else
                        MsgBox.OK("执行完成。");
                    #endregion
                }
               

                #region
                //int billId = -1;
                //foreach (DataRow row in data.Rows)
                //{
                //    billId = ConvertUtil.ToInt(row["BILL_ID"]);
                //    bool result = ExecuteStock(ConvertUtil.ToInt(row["ID"]), GlobeSettings.LoginedUser.WarehouseCode);
                //    if (!result)
                //    {
                //        MsgBox.Warn(string.Format("物料编码为{0}的物料未维护最小计量单位，请尽早维护！", ConvertUtil.ToString(row["SKU_CODE"])));
                //    }
                //    object[] array ={row["ID"],row["BILL_ID"],row["LC_CODE"],
                //                   row["ZN_NAME"],row["SKU_CODE"],row["UM_NAME"],
                //                   row["SKU_NAME"],row["SPEC"],row["COUNT_QTY"],
                //                   row["STOCK_QTY"],row["REMARK"],row["IS_EFFECT"]};
                //    Insert(ELogType.盘点, GlobeSettings.LoginedUser.UserName, ConvertUtil.ToString(billId), ConvertUtil.ToString(result), "盘点差异调整",
                //        DateTime.Now, JsonConvert.SerializeObject(array));
                //}

                ////更新单据状态
                //UpdateBillState(billId, "已执行调整");
                //frmWait.Close();
                //MsgBox.OK("执行完成。");
                //LoadData();
                #endregion
            }
            catch (Exception ex)
            {
                //frmWait.Close();
                MsgBox.Err(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (MsgBox.AskOK("确定取消该盘点单的差异调整？") == DialogResult.OK)
                {
                    //取消订单的差异调整
                    if (String.IsNullOrEmpty(listBillNO.Text.Trim()))
                    {
                        MsgBox.Warn("请选择要取消的盘点单。");
                        return;
                    }
                    //更新单据状态
                    if (UpdateBillState(ConvertUtil.ToInt(listBillNO.Text), "调整已取消"))
                    {
                        LoadData();
                        gridControl2.DataSource = null;
                    }
                }
            }
            catch
            { }
        }

        /// <summary>
        /// 盘点单管理---复盘============获取当前盘点差异单据的明细
        /// </summary>
        /// <param name="billNo"></param>
        /// <returns></returns>
        public List<LocationEntity> ListGetLocations(int billNo)
        {
            List<LocationEntity> list = new List<LocationEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billNo);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_ListGetLocations);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonListGetLocations bill = JsonConvert.DeserializeObject<JsonListGetLocations>(jsonQuery);
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
                foreach (JsonListGetLocationsResult jbr in bill.result)
                {
                    LocationEntity asnEntity = new LocationEntity();
                    asnEntity.CountQty = Convert.ToDecimal(jbr.countQty);
                    asnEntity.LocationCode = jbr.lcCode;
                    asnEntity.StockQty = Convert.ToDecimal(jbr.stockQty);

                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.stockExpDate))
                            asnEntity.ExpDateStock = Convert.ToDateTime(jbr.stockExpDate);

                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    }

                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.expDate))
                            asnEntity.ExpDate = Convert.ToDateTime(jbr.expDate);
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

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (listBillNO.Text.Trim() == "")
            {
                MsgBox.Warn("请选择一个差异调整单！");
                return;
            }
            int billNo = ConvertUtil.ToInt(listBillNO.Text);
            List<LocationEntity> locations = ListGetLocations(billNo);

            FrmLocationConfirm frmConfirm = new FrmLocationConfirm(locations, "复盘");
            frmConfirm.ShowDialog();
            frmConfirm.Dispose();
        }

        private void sbFlash_Click(object sender, EventArgs e)
        {
            using (WaitDialogForm tm = new WaitDialogForm("正在加载数据..."))
            {
                LoadData();
            }
        }
    }
}
