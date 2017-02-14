using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nodes.Entities;
using Nodes.Shares;
//using Nodes.DBHelper;
using Nodes.Utils;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Nodes.Resources;
using Nodes.UI;
using Nodes.Common;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;
using Newtonsoft.Json;

namespace Nodes.Outstore
{
    public partial class FrmAcrossOutbound : Form
    {
        //private SODal soDal = new SODal();

        public FrmAcrossOutbound()
        {
            InitializeComponent();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            ImageCollection ic = AppResource.LoadToolImages();
            barManager1.Images = ic;
            barButtonItem6.ImageIndex = (int)AppResource.EIcons.refresh;
            toolDelBill.ImageIndex = (int)AppResource.EIcons.delete;

            LoadWaitingPickBills();
        }

        /// <summary>
        /// 出库单管理，，自定义查询
        /// </summary>
        /// <param name="billNO"></param>
        /// <param name="customer"></param>
        /// <param name="saleMan"></param>
        /// <param name="billType"></param>
        /// <param name="billStatus"></param>
        /// <param name="outboundType"></param>
        /// <param name="shipNO"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public List<SOHeaderEntity> QueryBills(string billNO, string customer, string saleMan, string billType,
            string billStatus, string outboundType, string shipNO, DateTime dateFrom, DateTime dateTo)
        {
            List<SOHeaderEntity> list = new List<SOHeaderEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billNO=").Append(billNO).Append("&");
                loStr.Append("customer=").Append(customer).Append("&");
                loStr.Append("saleMan=").Append(saleMan).Append("&");
                loStr.Append("billType=").Append(billType).Append("&");
                loStr.Append("billStatus=").Append(billStatus).Append("&");
                loStr.Append("outboundType=").Append(outboundType).Append("&");
                loStr.Append("shipNO=").Append(shipNO).Append("&");
                loStr.Append("dateFrom=").Append(dateFrom).Append("&");
                loStr.Append("beginRow=").Append("&");
                loStr.Append("rows=").Append("&");
                loStr.Append("dateTo=").Append(dateTo);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_selectBillBody, 30000);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonQueryBills bill = JsonConvert.DeserializeObject<JsonQueryBills>(jsonQuery);
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
                foreach (JsonQueryBillsResult jbr in bill.result)
                {
                    SOHeaderEntity asnEntity = new SOHeaderEntity();
                    #region 0-10
                    asnEntity.Address = jbr.address;
                    asnEntity.BillID = Convert.ToInt32(jbr.billId);
                    asnEntity.BillNO = jbr.billNo;
                    asnEntity.BillType = jbr.billType;
                    asnEntity.BillTypeName = jbr.billTypeName;
                    asnEntity.ContractNO = jbr.contractNo;
                    asnEntity.Consignee = jbr.contact;
                    asnEntity.CustomerName = jbr.cName;
                    asnEntity.CustomerCode = jbr.cCode;
                    asnEntity.DelayMark = Convert.ToInt32(jbr.delayMark);
                    #endregion

                    #region 11-20
                    asnEntity.CancelFlag = Convert.ToInt32(jbr.cancelFlag);
                    asnEntity.ConfirmFlag = Convert.ToInt32(jbr.confirmFlag);
                    asnEntity.CrnAmount = Convert.ToDecimal(jbr.crmAmount);
                    asnEntity.CustomerCode = jbr.cCode;
                    asnEntity.CustomerName = jbr.cName;
                    asnEntity.DeliverymanMobile = jbr.mobilePhone;
                    asnEntity.FromWarehouse = jbr.fromWhCode;
                    asnEntity.FromWarehouseName = jbr.fromWhName;
                    asnEntity.OtherAmount = Convert.ToDecimal(jbr.otherAmount);
                    asnEntity.OutstoreType = jbr.outStoreType;
                    #endregion
                    #region 21---30
                    asnEntity.OutstoreTypeName = jbr.outStoreTypeName;
                    asnEntity.PayedAmount = Convert.ToDecimal(jbr.payedAmount);
                    asnEntity.PayMethod = Convert.ToInt32(jbr.payMethod);
                    asnEntity.PickZnType = jbr.pickZnType;
                    asnEntity.PickZnTypeName = jbr.pickZnTypeName;
                    asnEntity.RealAmount = Convert.ToDecimal(jbr.realAmount);
                    asnEntity.ReceiveAmount = Convert.ToDecimal(jbr.receiveAmount);
                    asnEntity.Remark = jbr.remark;
                    asnEntity.RowForeColor = Convert.ToInt32(jbr.rowColor);
                    asnEntity.SalesMan = jbr.salesMan;
                    #endregion
                    #region 31-40
                    asnEntity.ShipNO = jbr.shipNo;
                    asnEntity.ShTel = jbr.phone;
                    asnEntity.Status = jbr.billState;
                    asnEntity.StatusName = jbr.statusName;
                    asnEntity.Warehouse = jbr.whCode;
                    asnEntity.WarehouseName = jbr.whName;
                    asnEntity.WmsRemark = jbr.wmsRemark;
                    asnEntity.Printed = Convert.ToInt32(jbr.printed);
                    #endregion

                    #region
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.closeDate))
                            asnEntity.CloseDate = Convert.ToDateTime(jbr.closeDate);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("PSoManage+QueryBills", msg);
                    }
                    #endregion

                    #region
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.createDate))
                            asnEntity.CreateDate = Convert.ToDateTime(jbr.createDate);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("PSoManage+QueryBills", msg);
                    }
                    #endregion

                    #region
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.confirmDate))
                            asnEntity.ConfirmDate = Convert.ToDateTime(jbr.confirmDate);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("PSoManage+QueryBills", msg);
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

        private void LoadWaitingPickBills()
        {
            try
            {
                List<SOHeaderEntity> soHeaderEntitys = QueryBills(
                    null, null, null, null, "60,61,62,63,65,66,67",
                    BaseCodeConstant.OUT_TYPE_CROSS, null, DateTime.Now.AddYears(-1), DateTime.Now.AddDays(1));
                bindingSource1.DataSource = soHeaderEntitys;
                ShowFocusedPickPlan();
                ShowFocusDetail();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void Reload()
        {
            LoadWaitingPickBills();
        }

        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DoClickEvent(ConvertUtil.ToString(e.Item.Tag));
        }

        private void DoClickEvent(string itemTag)
        {
            switch (itemTag)
            {
                case "刷新":
                    Reload();
                    break;
                case "确认发货":
                    DoAcrossOutbound();
                    break;
                default:
                    MsgBox.Warn("未找到为该按钮设置的事件：" + itemTag);
                    break;
            }
        }

        /// <summary>
        /// 越库出库，确认发货
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="userCode"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool AcrossOutbound(int billID, string userCode, string userName)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billID=").Append(billID).Append("&");
                loStr.Append("userCode=").Append(userCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_AcrossOutbound);
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
        public bool Insert(ELogType type, string creator, string billNo, string description,
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
        public bool Insert(ELogType type, string creator, string billNo, string description,
            string module, string remark)
        {
            return Insert(type, creator, billNo, description, module, DateTime.Now, remark);
        }
        public bool Insert(ELogType type, string creator, string billNo, string description,
            string module)
        {
            return Insert(type, creator, billNo, description, module, DateTime.Now, null);
        }
        public bool Insert(ELogType type, string creator, string billNo, string module)
        {
            return Insert(type, creator, billNo, string.Empty, module, DateTime.Now, null);
        }
        #endregion

        private void DoAcrossOutbound()
        {
            SOHeaderEntity selectedHeader = SelectedHeader;
            if (selectedHeader == null)
            {
                MsgBox.Warn("请选中单据行。");
                return;
            }
            if (MsgBox.AskOK(string.Format("单据“{0}”确定执行出库操作吗？", selectedHeader.BillNO)) != DialogResult.OK)
                return;

            try
            {
                bool result = AcrossOutbound(selectedHeader.BillID,
                    string.Empty, GlobeSettings.LoginedUser.UserName);
                Insert(ELogType.越库, GlobeSettings.LoginedUser.UserName, selectedHeader.BillNO, string.Empty, "越库出库");
                if (result)
                {
                    gridPlans.DataSource = null;
                    Reload();

                    MsgBox.OK("越库发货成功。");
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ShowFocusedPickPlan();
            ShowFocusDetail();
        }

        SOHeaderEntity SelectedHeader
        {
            get
            {
                if (gvHeader.GetFocusedRowCellValue("BillNO") == null)
                    return null;
                else
                    return gvHeader.GetFocusedRow() as SOHeaderEntity;
            }
        }

        /// <summary>
        /// 出库单管理，查询出库单明细
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public List<SODetailEntity> GetDetails(int billID)
        {
            List<SODetailEntity> list = new List<SODetailEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billID=").Append(billID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetDetails);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetDetails bill = JsonConvert.DeserializeObject<JsonGetDetails>(jsonQuery);
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
                foreach (JsonGetDetailsResult jbr in bill.result)
                {
                    SODetailEntity asnEntity = new SODetailEntity();
                    #region 0-10
                    asnEntity.BatchNO = jbr.batchNo;
                    asnEntity.BillID = Convert.ToInt32(jbr.billId);
                    asnEntity.CombMaterial = jbr.comMaterial;
                    asnEntity.DetailID = Convert.ToInt32(jbr.detailId);
                    asnEntity.DueDate = jbr.dueDate;
                    asnEntity.IsCase = Convert.ToInt32(jbr.isCase);
                    asnEntity.MaterialCode = jbr.skuCode;
                    asnEntity.MaterialName = jbr.skuName;
                    asnEntity.PickQty = Convert.ToDecimal(jbr.pickQty);
                    asnEntity.Price1 = Convert.ToDecimal(jbr.price);
                    #endregion
                    #region 11-20
                    asnEntity.Qty = Convert.ToDecimal(jbr.qty);
                    asnEntity.Remark = jbr.remark;
                    asnEntity.RowNO = Convert.ToInt32(jbr.rowNo);
                    ///jbr.rowNo1;
                    asnEntity.SkuBarcode = jbr.skuBarCode;
                    asnEntity.Spec = jbr.spec;
                    asnEntity.SuitNum = Convert.ToDecimal(jbr.suitNum);
                    asnEntity.UnitCode = jbr.umCode;
                    asnEntity.UnitName = jbr.umName;
                    #endregion
                    try
                    {
                        //if (!string.IsNullOrEmpty(jbr.closeDate))
                        //    asnEntity.CloseDate = Convert.ToDateTime(jbr.closeDate);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("PSoManage+QueryBills", msg);
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

        private void ShowFocusDetail()
        {
            SOHeaderEntity selectedHeader = SelectedHeader;
            if (selectedHeader == null)
            {
                gridDetails.DataSource = null;
                gvDetails.ViewCaption = "未选择单据";
            }
            else
            {
                gridDetails.DataSource = GetDetails(selectedHeader.BillID);
                gvDetails.ViewCaption = string.Format("单据号: {0}", selectedHeader.BillNO);
            }
        }

        /// <summary>
        /// 捡货任务管理-捡货商品列表
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public List<PickPlanEntity> GetPickPlan(int billID)
        {
            List<PickPlanEntity> list = new List<PickPlanEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetPickPlan);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetPickPlan bill = JsonConvert.DeserializeObject<JsonGetPickPlan>(jsonQuery);
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
                foreach (JsonGetPickPlanResult jbr in bill.result)
                {
                    PickPlanEntity asnEntity = new PickPlanEntity();
                    #region 0-10
                    asnEntity.BillID = Convert.ToInt32(jbr.billId);
                    asnEntity.BillNO = jbr.billNo;
                    asnEntity.ComMaterial = jbr.comMaterial;
                    asnEntity.DetailID = Convert.ToInt32(jbr.detailId);
                    asnEntity.Location = jbr.lcCode;
                    asnEntity.Material = jbr.skuCode;
                    asnEntity.MaterialName = jbr.skuName;
                    asnEntity.Qty = jbr.qty;
                    asnEntity.RowNO = Convert.ToInt32(jbr.rowNo);
                    asnEntity.SaleUnit = jbr.saleUnit;
                    #endregion
                    #region 11-20
                    asnEntity.SaleUnitTransValue = Convert.ToInt32(jbr.saleTransValue);
                    asnEntity.SkuBarcode = jbr.skuBarcode;
                    asnEntity.STOCK_ID = Convert.ToInt32(jbr.stockId);
                    asnEntity.UnitCode = jbr.umCode;
                    asnEntity.UnitName = jbr.umName;
                    #endregion

                    #region
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.createDate))
                            asnEntity.CreateData = Convert.ToDateTime(jbr.createDate);

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
                        if (!string.IsNullOrEmpty(jbr.expDate))
                            asnEntity.ExpDate = Convert.ToDateTime(jbr.expDate);
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

        void ShowFocusedPickPlan()
        {
            SOHeaderEntity selectedHeader = SelectedHeader;
            if (selectedHeader == null)
            {
                gridPlans.DataSource = null;
                gvPlans.ViewCaption = "未选择单据";
            }
            else
            {
                gridPlans.DataSource = GetPickPlan(selectedHeader.BillID);
                gvPlans.ViewCaption = string.Format("单据号: {0}", selectedHeader.BillNO);
            }
        }
    }
}
