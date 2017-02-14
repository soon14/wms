using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Shares;
using Nodes.UI;
using Nodes.Utils;
using DevExpress.Utils;
using Nodes.Resources;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Instore;
using Nodes.Entities.HttpEntity.CycleCount;
using Newtonsoft.Json;

namespace Nodes.CycleCount
{
    public partial class FrmCountManager : DevExpress.XtraEditors.XtraForm
    {
        
        private int QueryType = 1; //1：所有未完成；2：未完成（仅限7日内的单据）；3：自定义
        private string BillNO, BillState;
        private DateTime DateFrom, DateTo;

        public FrmCountManager()
        {
            InitializeComponent();
        }
       
        /// <summary>
        /// 默认系统加载一月内的单据
        /// </summary>
        private void InitDate()
        {
            dateEditFrom.DateTime = System.DateTime.Now.AddMonths(-1);
            dateEditTo.DateTime = System.DateTime.Now;
        }

        private CountHeaderEntity SelectedCountHeader
        {
            get
            {
                if (gridView1.FocusedRowHandle < 0)
                    return null;
                else
                    return gridView1.GetFocusedRow() as CountHeaderEntity;
            }
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            ImageCollection ic = AppResource.LoadToolImages();
            barManager1.Images = ic;
            barButtonItem7.ImageIndex = (int)AppResource.EIcons.report;
            barButtonItem1.ImageIndex = (int)AppResource.EIcons.ok;
            barButtonItem2.ImageIndex = (int)AppResource.EIcons.refresh;
            barButtonItem3.ImageIndex = (int)AppResource.EIcons.search;
            barButtonItem4.ImageIndex = (int)AppResource.EIcons.approved;
            barButtonItem5.ImageIndex = (int)AppResource.EIcons.week;
            barButtonItem6.ImageIndex = (int)AppResource.EIcons.design;
            barButtonItem8.ImageIndex = (int)AppResource.EIcons.report;

            InitDate();
            BindLookUpControl();
            Query(1, null, null);
        }

        public void Query(int queryType, DateTime? dateFrom, DateTime? dateTo)
        {
            BindQueryResult(queryType, null, null, dateFrom, dateTo);
        }

        private void Reload()
        {
            BindQueryResult(this.QueryType, this.BillNO, this.BillState, this.DateFrom, this.DateTo);
        }

        /// <summary>
        /// 盘点单管理---根据条件查询盘点单
        /// </summary>
        /// <param name="warehouse"></param>
        /// <param name="billNO"></param>
        /// <param name="billStatus"></param>
        /// <param name="showNotComplete"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public List<CountHeaderEntity> QueryBills(string warehouse, string billNO,
          string billStatus, bool showNotComplete, DateTime? dateFrom, DateTime? dateTo)
        {
            List<CountHeaderEntity> list = new List<CountHeaderEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("warehouse=").Append(warehouse).Append("&");
                loStr.Append("billNO=").Append(billNO).Append("&");
                loStr.Append("billStatus=").Append(billStatus).Append("&");
                loStr.Append("showNotComplete=").Append(showNotComplete).Append("&");
                loStr.Append("dateFrom=").Append(dateFrom).Append("&");
                loStr.Append("dateTo=").Append(dateTo);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_QueryBills_PanDian);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonQueryBillsPanDian bill = JsonConvert.DeserializeObject<JsonQueryBillsPanDian>(jsonQuery);
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
                foreach (JsonQueryBillsPanDianResult jbr in bill.result)
                {
                    CountHeaderEntity asnEntity = new CountHeaderEntity();
                    asnEntity.BillID = Convert.ToInt32(jbr.billId);
                    asnEntity.BillState = jbr.billState;
                    asnEntity.Creator = jbr.creator;
                    asnEntity.Remark = jbr.remark;
                    asnEntity.StateName = jbr.itemDesc;
                    asnEntity.TagDesc = jbr.tagDesc;
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

        public void BindQueryResult(int queryType, string billNO, string billStatus, DateTime? dateFrom, DateTime? dateTo)
        {
            try
            {
                string queryCondition = string.Empty;
                if (queryType == 1)
                    queryCondition = "查询条件：所有未完成的盘点单。";
                else if (queryType == 2)
                    queryCondition = "查询条件：近一周内未完成的盘点单。";
                else if (queryType == 3)
                {
                    if (dateFrom.Value > dateTo.Value)
                    {
                        MsgBox.Warn("开始时间不能大于结束时间。");
                        return;
                    }

                    if (dateFrom.Value.Subtract(dateTo.Value).Days > 180)
                    {
                        MsgBox.Warn("时间跨度不能超过180天。");
                        return;
                    }

                    barStaticItem1.Caption = "查询条件：自定义查询";
                }

                this.QueryType = queryType;
                List<CountHeaderEntity> soHeaderEntitys = null;                
                if (this.QueryType == 1)
                {
                    soHeaderEntitys = QueryBills(GlobeSettings.LoginedUser.WarehouseCode, null, null, true, null, null);
                }
                else if (this.QueryType == 2)
                {
                    DateFrom = dateFrom.Value;
                    DateTo = dateTo.Value;
                    soHeaderEntitys = QueryBills(GlobeSettings.LoginedUser.WarehouseCode, null, null, true, dateFrom, dateTo);
                }
                else
                {
                    this.BillNO = string.IsNullOrEmpty(billNO) ? null : billNO;
                    this.BillState = string.IsNullOrEmpty(billStatus) ? null : billStatus;
                    DateFrom = dateFrom.Value;
                    DateTo = dateTo.Value;

                    string separator = " && ";
                    queryCondition = string.Format(@"{0}{1}{2}",
                        string.IsNullOrEmpty(this.BillNO) ? "" : "盘点单号=" + this.BillNO + separator,
                        string.IsNullOrEmpty(this.BillState) ? "" : "状态=" + listBillState.Text + separator,
                        "建单日期介于【" + dateFrom.Value.ToShortDateString() + "】与【" + dateTo.Value.AddDays(-1).ToShortDateString() + "】之间");

                    soHeaderEntitys = QueryBills(GlobeSettings.LoginedUser.WarehouseCode, BillNO, BillState, false,
                        DateFrom, DateTo);
                }

                barStaticItem1.Caption = queryCondition;
                bindingSource1.DataSource = soHeaderEntitys;
                ShowBillDetail();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DoClickEvent(ConvertUtil.ToString(e.Item.Tag));
        }

        private void DoClickEvent(string tag)
        {
            switch (tag)
            {            
                case "刷新":
                    Reload();
                    break;
                case "所有未完成":
                    Query(1, DateTime.Now, DateTime.Now);
                    break;
                case "近一周单据":
                    Query(2, DateTime.Now.AddDays(-7).Date, DateTime.Now.AddDays(1).Date);
                    break;
                case "单据完成":
                    DoComplete();
                    break;
                case "查看报告":
                    DoViewReport();
                    break;
                case "报告上传":
                    DoReportToERP();
                    break;
                case "复盘":
                    DoRecount();
                    break;

            }
        }

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
        /// 绑定页面下拉列表
        /// </summary>
        private void BindLookUpControl()
        {
            //单据状态
            List<BaseCodeEntity> entityLists = GetItemList(BaseCodeConstant.COUNT_STATE);
            entityLists.Add(new BaseCodeEntity() { ItemValue = "10000", ItemDesc = "已执行调整" });
            entityLists.Add(new BaseCodeEntity() { ItemValue = "10001", ItemDesc = "等待调整" });
            entityLists.Add(new BaseCodeEntity() { ItemValue = "10002", ItemDesc = "调整已取消" });
            listBillState.Properties.DisplayMember = "ItemDesc";
            listBillState.Properties.ValueMember = "ItemValue";
            listBillState.Properties.DataSource = entityLists;
        }

        private void QueryCustom()
        {
            string billNO = txtBillNO.Text.Trim();
            string billState = ConvertUtil.ToString(listBillState.EditValue);

            BindQueryResult(3, billNO, StringTrim.DeleteTrim(billState), dateEditFrom.DateTime.Date, dateEditTo.DateTime.AddDays(1).Date);
            ClosePopup();
        }

        /// <summary>
        /// 盘点单管理--落放位
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public List<CountDetailEntity> GetCountLocation(int billID)
        {
            List<CountDetailEntity> list = new List<CountDetailEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetCountLocation);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetCountLocation bill = JsonConvert.DeserializeObject<JsonGetCountLocation>(jsonQuery);
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
                foreach (JsonGetCountLocationResult jbr in bill.result)
                {
                    CountDetailEntity asnEntity = new CountDetailEntity();
                    asnEntity.BillID = Convert.ToInt32(jbr.billId);
                    asnEntity.CellCode = jbr.cellCode;
                    asnEntity.FloorCode = jbr.floorCode;
                    asnEntity.ID = Convert.ToInt32(jbr.id);
                    asnEntity.Location = jbr.lcCode;
                    asnEntity.LocationState = jbr.lcState;
                    asnEntity.PassageCode = jbr.passageCode;
                    asnEntity.ShelfCode = jbr.shelfCode;
                    asnEntity.ZoneCode = jbr.znCode;
                    asnEntity.ZoneName = jbr.znName;
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
        /// 盘点单管理--盘点记录
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public List<CountDetailEntity> GetCountRecords(int billID)
        {
            List<CountDetailEntity> list = new List<CountDetailEntity>();

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetCountRecords);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetCountRecords bill = JsonConvert.DeserializeObject<JsonGetCountRecords>(jsonQuery);
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
                foreach (JsonGetCountRecordsResult jbr in bill.result)
                {
                    CountDetailEntity asnEntity = new CountDetailEntity();
                    asnEntity.BillID = Convert.ToInt32(jbr.billId);
                    asnEntity.ID = Convert.ToInt32(jbr.id);
                    asnEntity.Location = jbr.lcCode;
                    asnEntity.MaterialCode = jbr.skuCode;
                    asnEntity.MaterialName = jbr.skuName;
                    asnEntity.Qty = Convert.ToDecimal(jbr.qty);
                    asnEntity.UserCode = jbr.userCode;
                    asnEntity.UserName = jbr.userName;
                    asnEntity.ZoneCode = jbr.znCode;
                    asnEntity.ZoneName = jbr.znName;
                    //jbr.skuBarCode;
                    try
                    {
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

        /// <summary>
        /// 盘点单管理---跟库存实时比对，显示报告
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public DataTable GetReportVsStock(int billID)
        {
            #region
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("ZN_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("LC_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("SKU_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("COUNT_QTY", Type.GetType("System.Decimal"));
            tblDatas.Columns.Add("STOCK_QTY", Type.GetType("System.Decimal"));
            tblDatas.Columns.Add("DIFF_QTY", Type.GetType("System.Decimal"));
            tblDatas.Columns.Add("STOCK_EXP_DATE", Type.GetType("System.DateTime"));
            tblDatas.Columns.Add("EXP_DATE", Type.GetType("System.DateTime"));
            tblDatas.Columns.Add("IS_SYNC", Type.GetType("System.String"));

            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetReportVsStock);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetReportVsStock bill = JsonConvert.DeserializeObject<JsonGetReportVsStock>(jsonQuery);
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
                foreach (JsonGetReportVsStockResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["ZN_NAME"] = tm.znName;
                    newRow["LC_CODE"] = tm.lcCode;
                    newRow["SKU_NAME"] = tm.skuName;
                    newRow["COUNT_QTY"] = StringToDecimal.GetTwoDecimal(tm.countQty);
                    newRow["STOCK_QTY"] = StringToDecimal.GetTwoDecimal(tm.stockQty);
                    newRow["DIFF_QTY"] = StringToDecimal.GetTwoDecimal(tm.countQty) - StringToDecimal.GetTwoDecimal(tm.stockQty);
                    if (!string.IsNullOrEmpty(tm.stockExpDate))
                        newRow["STOCK_EXP_DATE"] = tm.stockExpDate;
                    if (!string.IsNullOrEmpty(tm.expDate))
                        newRow["EXP_DATE"] = tm.expDate;
                    //newRow["IS_SYNC"] =;
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

        private void ShowBillDetail()
        {
            if (SelectedCountHeader == null)
            {
                gridControl2.DataSource = null;
                return;
            }

            try
            {
                gridControl3.DataSource = GetCountLocation(SelectedCountHeader.BillID);
                gridControl4.DataSource = GetCountRecords(SelectedCountHeader.BillID);
                gridControl2.DataSource = GetReportVsStock(SelectedCountHeader.BillID);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void DoViewReport()
        {
            CountHeaderEntity header = SelectedCountHeader;
            if (header == null)
            {
                MsgBox.Err("请选中要查看的盘点单。");
                return;
            }

            FrmCountRecordVsStock frmReport = new FrmCountRecordVsStock(header.BillID);
            frmReport.ShowDialog();
        }

        /// <summary>
        /// 盘点单管理---报告上传
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public CountHeaderEntity GetBillInfo(int billID)
        {
            CountHeaderEntity asnEntity = new CountHeaderEntity();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetBillInfo);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return asnEntity;
                }
                #endregion

                #region 正常错误处理

                JsonGetBillInfo bill = JsonConvert.DeserializeObject<JsonGetBillInfo>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return asnEntity;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return asnEntity;
                }
                #endregion
                List<CountHeaderEntity> list = new List<CountHeaderEntity>();
                #region 赋值数据
                foreach (JsonGetBillInfoResult jbr in bill.result)
                {
                    asnEntity.BillID = Convert.ToInt32(jbr.billId);
                    asnEntity.BillState = jbr.billState;
                    asnEntity.Creator = jbr.creator;
                    asnEntity.Remark = jbr.remark;
                    asnEntity.Warehouse = jbr.whCode;
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.createDate))
                            asnEntity.CreateDate = Convert.ToDateTime(jbr.createDate);

                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    }

                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.completeDate))
                            asnEntity.CompleteDate = Convert.ToDateTime(jbr.completeDate);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    }

                    list.Add(asnEntity);
                }
                return asnEntity;
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return asnEntity;
        }

        private void DoReportToERP()
        {
            CountHeaderEntity header = SelectedCountHeader;
            if (header == null)
            {
                MsgBox.Warn("请选中要查看的盘点单。");
                return;
            }

            //查看单据状态是否为完成，否则不允许进入
            CountHeaderEntity _header = GetBillInfo(header.BillID);
            if (_header == null || _header.BillState != BaseCodeConstant.COUNT_STATE_CLOSE)
            {
                MsgBox.Warn("必须是已经完成的盘点单才可以上传。");
                return;
            }

            FrmReportToERP frmReport = new FrmReportToERP(header.BillID);
            frmReport.ShowDialog();
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

        private void DoRecount()
        {
            CountHeaderEntity header = SelectedCountHeader;
            if (header == null)
            {
                MsgBox.Warn("请选中要完成的单据。");
                return;
            }
            if (Convert .ToInt32 ( header.BillState) < Convert.ToInt32( BaseCodeConstant.COUNT_STATE_CLOSE))
            {
                MsgBox.Warn(string.Format("单据“{0}”还没盘点完成。", header.BillID));
                return;
            }
            List<LocationEntity> locations = ListGetLocations(header.BillID);

            FrmLocationConfirm frmConfirm = new FrmLocationConfirm(locations, "复盘");
            frmConfirm.ShowDialog();
            frmConfirm.Dispose();
        }

        /// <summary>
        /// 盘点单管理---完成订单
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool CompleteBill(int billID, string userName)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID).Append("&");
                loStr.Append("userName=").Append(userName);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_CompleteBill);
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

        private void DoComplete()
        {
            CountHeaderEntity header = SelectedCountHeader;
            if (header == null)
            {
                MsgBox.Warn("请选中要完成的单据。");
                return;
            }

            if (header.BillState == BaseCodeConstant.COUNT_STATE_CLOSE)
            {
                MsgBox.Warn(string.Format("单据“{0}”盘点已完成。", header.BillID));
                return;
            }

            if (MsgBox.AskOK(string.Format("确认将单据”{0}“设置为完成状态吗？", header.BillID)) != DialogResult.OK)
                return;

            try
            {
                bool result = CompleteBill(header.BillID, GlobeSettings.LoginedUser.UserName);
                if (result)
                {
                    header.BillState = BaseCodeConstant.COUNT_STATE_CLOSE;
                    header.StateName = "已完成";
                    MsgBox.OK("单据成功设置为完成状态。");
                }

                //int result = CompleteBill(header.BillID, GlobeSettings.LoginedUser.UserName);
                //if (result == 1)
                //{
                //    header.BillState = BaseCodeConstant.COUNT_STATE_CLOSE;
                //    header.StateName = "已完成";
                //    MsgBox.OK("单据成功设置为完成状态。");
                //}
                //else if (result == -1)
                //    MsgBox.Warn("未找到该盘点单，可能已经被其他人删除，请刷新数据后重试。");
                //else if (result == -2)
                //    MsgBox.Warn("该盘点单已经完成，请刷新数据后重试。");
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void ClosePopup()
        {
            popupControlContainer1.HidePopup();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            QueryCustom();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClosePopup();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ShowBillDetail();
        }
    }
}