using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Utils;
using Nodes.Entities;
//using Nodes.DBHelper;
using Nodes.Shares;
using System.Windows.Forms;
using Nodes.UI;
using System.Threading;
using Nodes.SystemManage;
using DevExpress.XtraReports.UI;
using System.Diagnostics;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;
using Newtonsoft.Json;

namespace Nodes.Outstore
{
    public class PSoManage
    {
        //private SODal soDal;
        private ISoManage IParent;

        public PSoManage(ISoManage iso)
        {
            IParent = iso;
            //soDal = new SODal();
        }

        /// <summary>
        /// 返回单据的编号字符串，以逗号隔开
        /// </summary>
        /// <param name="bills"></param>
        /// <returns></returns>
        string GetBillNOs(List<SOHeaderEntity> bills)
        {
            string billNOs = string.Empty;
            foreach (SOHeaderEntity header in bills)
                billNOs += header.BillNO + ",";

            return billNOs.TrimEnd(',');
        }

        #region 处理查询
        private int QueryType = 1; //1：所有未完成；2：未完成（仅限7日内的单据）；3：自定义
        private string BillNO, BillType, BillState, OutboundType, Customer, SalesMan, Material;
        private DateTime DateFrom, DateTo;

        public void Requery()
        {
            BindQueryResult(QueryType, BillNO, Customer, SalesMan, BillType, BillState,
                OutboundType, Material, DateFrom, DateTo);
        }

        public void Query(int queryType, DateTime dateFrom, DateTime dateTo)
        {
            BindQueryResult(queryType, null, null, null, null, null, null, null, dateFrom, dateTo);
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
                    if (!string.IsNullOrEmpty(jbr.rowColor))
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

        /// <summary>
        /// 按照库房、收货方式、状态（是小于某个状态）的单据
        /// </summary>
        /// <param name="warehouse"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public List<SOHeaderEntity> QueryBillsQuickly(string outboundType, DateTime? dateFrom, DateTime? dateTo)
        {
            List<SOHeaderEntity> list = new List<SOHeaderEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("queryType=").Append(outboundType).Append("&");
                loStr.Append("dateFrom=").Append(dateFrom).Append("&");
                loStr.Append("dateTo=").Append(dateTo);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_QueryBillsQuicklyBill, 30000);
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
                    if (!string.IsNullOrEmpty(jbr.rowColor))
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
                        //LogHelper.errorLog("PSoManage+QueryBillsQuickly", msg);
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
                        //LogHelper.errorLog("PSoManage+QueryBillsQuickly", msg);
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
                        //LogHelper.errorLog("PSoManage+QueryBillsQuickly", msg);
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

        public void BindQueryResult(int queryType, string billNO, string customer, string salesMan, string billType,
            string billStatus, string outboundType, string material, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                if (queryType == 3)
                {
                    if (dateFrom > dateTo)
                    {
                        MsgBox.Warn("开始时间不能大于结束时间。");
                        return;
                    }

                    if (dateFrom.Subtract(dateTo).Days > 180)
                    {
                        MsgBox.Warn("时间区间不能超过180天。");
                        return;
                    }
                }

                this.QueryType = queryType;
                List<SOHeaderEntity> soHeaderEntitys = null;

                if (this.QueryType == 3)
                {
                    this.BillNO = string.IsNullOrEmpty(billNO) ? null : billNO;
                    this.BillType = string.IsNullOrEmpty(billType) ? null : billType;
                    this.BillState = string.IsNullOrEmpty(billStatus) ? null : billStatus;
                    this.OutboundType = string.IsNullOrEmpty(outboundType) ? null : outboundType;
                    this.Customer = string.IsNullOrEmpty(customer) ? null : customer;
                    this.SalesMan = string.IsNullOrEmpty(salesMan) ? null : salesMan;
                    this.Material = string.IsNullOrEmpty(material) ? null : material;
                    DateFrom = dateFrom;
                    DateTo = dateTo;
                    soHeaderEntitys = QueryBills(BillNO, Customer, SalesMan, BillType, BillState,
                        OutboundType, Material, DateFrom, DateTo);
                }
                else if (this.QueryType == 1)
                {
                    soHeaderEntitys = QueryBillsQuickly(null, null, null);
                }
                else
                {
                    DateFrom = dateFrom;
                    DateTo = dateTo;
                    soHeaderEntitys = QueryBillsQuickly(null, dateFrom, dateTo);
                }

                IParent.BindingGrid(soHeaderEntitys);
                IParent.ShowFocusDetail();

                IParent.ShowQueryCondition(QueryType, BillNO, Customer, SalesMan, BillType, BillState,
                OutboundType, material, DateFrom, DateTo);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
        #endregion

        #region 编辑备注
        public void WriteWMSRemark()
        {
            SOHeaderEntity header = IParent.GetFocusedBill();
            if (header == null)
            {
                MsgBox.Warn("请选中要修改的单据。");
            }
            else
            {
                //目前只修改颜色和备注
                FrmEditSO frmEdit = new FrmEditSO(header);
                if (frmEdit.ShowDialog() == DialogResult.OK)
                {
                    //刷新界面显示
                    header.UpdateRemark(frmEdit.Remark, frmEdit.SelectedColor);
                }
            }
        }
        #endregion

        /// <summary>
        /// 查看拣货记录
        /// </summary>
        public void ShowPickRecords()
        {
            SOHeaderEntity header = IParent.GetFocusedBill();
            if (header == null)
            {
                MsgBox.Warn("请选中要查看的行。");
            }
            else
            {
                FrmListPickRecords frmPickRecord = new FrmListPickRecords(header.BillID, header.BillNO);
                frmPickRecord.ShowDialog();
            }
        }

        /// <summary>
        /// 查看称重记录 2015-6-10 10:41:22 by wangjw
        /// </summary>
        public void ShowWeighRecords()
        {
            SOHeaderEntity header = IParent.GetFocusedBill();
            if (header == null)
            {
                MsgBox.Warn("请选中要查看的行。");
            }
            else
            {
                FrmListWeighRecords frmWeightRecords = new FrmListWeighRecords(header.BillID, header.BillNO);
                frmWeightRecords.ShowDialog();
            }
        }

        /// <summary>
        /// 查看单据明细行物料入库信息
        /// </summary>
        public void ShowOutboundDetail()
        {
            SOHeaderEntity header = IParent.GetFocusedBill();
            if (header == null)
            {
                MsgBox.Warn("请选中要查看的行。");
            }
            else
            {
            }
        }

        /// <summary>
        /// 显示拣货计划
        /// </summary>
        public void ShowPickPlan()
        {
            SOHeaderEntity header = IParent.GetFocusedBill();
            if (header == null)
            {
                MsgBox.Warn("请选中要查看的行。");
            }
            else
            {
                FrmListPickPlan frmListPickPlan = new FrmListPickPlan(header.BillID, header.BillNO);
                frmListPickPlan.ShowDialog();
            }
        }

        public void ShowBillLog()
        {
            SOHeaderEntity header = IParent.GetFocusedBill();
            if (header == null)
            {
                MsgBox.Warn("请选中要查看的行。");
            }
            else
            {
                FrmViewBillLog frmLog = new FrmViewBillLog(header.BillID, header.BillNO, "出库单据");
                frmLog.ShowDialog();
            }
        }

        /// <summary>
        /// 退货单管理,更新打印标记为已打印
        /// </summary>
        public bool UpdatePrintedFlag(int billID, string creator, string BillNO,int num)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billID=").Append(billID).Append("&");
                loStr.Append("billNo=").Append(BillNO).Append("&");
                loStr.Append("userName=").Append(creator).Append("&");
                loStr.Append("warehouseType=").Append(EUtilStroreType.WarehouseTypeToInt(GlobeSettings.LoginedUser.WarehouseType)).Append("&");
                loStr.Append("printed=").Append(num);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_UpdatePrintedFlagLongMiao);
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

        public void PrintSO()
        {
            List<SOHeaderEntity> focusedBills = IParent.GetFocusedBills();
            if (focusedBills.Count == 0)
            {
                MsgBox.Warn("请选中要打印的单据。");
                return;
            }
            foreach (SOHeaderEntity entity in focusedBills)
            {
                int status = ConvertUtil.ToInt(entity.Status);
                if (status != 68 && status != 693)
                {
                    MsgBox.Warn(String.Format("只有订单状态为<发货完成>或<已发车>后才能在此打印单据。"));
                    return;
                }
            }

            if (MsgBox.AskOK(string.Format("一共选中了“{0}”个单据“{1}”，确定要开始打印吗？",
                focusedBills.Count, GetBillNOs(focusedBills))) != DialogResult.OK)
                return;
            SOBody dataSource = null;
            //CompanyEntity company = new CompanyDal().GetCompanys()[0];        whc 2016-11-14 16:32:43
            bool printed = false;
            int pick_suit_type = ConvertUtil.ToInt(GlobeSettings.SystemSettings["套餐分拣方式"]);
            string module = "出库单管理";
            NewPrint.sellorder sellOrder = new NewPrint.sellorder();
            //sellOrder.printorder(string.Format("{0}#{1}", GlobeSettings.LoginedUser.WarehouseName, StringUtil.JoinBySign<SOHeaderEntity>(focusedBills, "BillNO", ";")));
            //return;
            foreach (SOHeaderEntity header in focusedBills)
            {
                sellOrder.printorder(string.Format("{0}#{1}",
                    GlobeSettings.LoginedUser.WarehouseName, header.BillNO));
                #region 现已不采用
                //dataSource = new SOBody();
                //#region 采购退货注释
                ////if (header.BillType == "124")
                ////{
                ////    if (header.Status == BaseCodeConstant.SO_STATUS_CLOSE)  // 如果状态为“发货完成”，打印单据
                ////    {
                ////        dataSource.Header = header;
                ////        dataSource.CompanyInfo = company;
                ////        dataSource.ReportDetails = soDal.GetDetailsForPrint(header.BillID);

                ////        RepAsnReturn repSO = new RepAsnReturn(dataSource);
                ////        //repSO.ShowPreviewDialog();
                ////        repSO.Print();
                ////        printed = true;
                ////    }
                ////    else
                ////    {
                ////        MsgBox.Warn(string.Format("单据：{0} 发货完成后才能打印。"));
                ////    }
                ////}
                ////else
                ////{
                //#endregion
                //dataSource.Header = header;
                //dataSource.CompanyInfo = company;
                //dataSource.ReportDetails = soDal.GetDetailsForPrint(header.BillID, pick_suit_type);
                //dataSource.ReportDetailAttri = soDal.GeDetailAttri(header.BillID);   // 该订单所有优惠券
                //XtraReport repSO = null;
                //if (header.BillType == "121" || header.BillType == "122" || header.BillType == "123")
                //{
                //    repSO = new RepSOTransfer(dataSource, module);
                //}
                //else if (header.BillType == "125")
                //{
                //    repSO = new RepSOBreakage(dataSource, module);
                //}
                //else if (pick_suit_type == 0)
                //{
                //    repSO = new RepSO_New(dataSource, module);
                //}
                //else if (pick_suit_type == 1)
                //{
                //    repSO = new RepSO_New2(dataSource, module);
                //}
                //repSO.ShowPreviewDialog();
                ////repSO.Print();
                #endregion
                printed = true;
                //}
                if (printed)
                {
                    //更新打印标记为已打印
                    UpdatePrintedFlag(header.BillID, GlobeSettings.LoginedUser.UserName, header.BillNO,0);
                    header.Printed = 1;
                    //Insert(ELogType.打印, GlobeSettings.LoginedUser.UserName, header.BillNO, header.BillTypeName, "出库单管理");
                }
            }

            IParent.RefreshHeaderGrid();
        }

        public void CloseBill()
        {
            SOHeaderEntity header = IParent.GetFocusedBill();
            if (header == null)
            {
                MsgBox.Warn("请选中要设置为“发货完成”的行。");
                return;
            }

            //先从界面上判断一下，减少网络交互和数据库负载
            if (header.Status == BaseCodeConstant.SO_STATUS_CLOSE)
            {
                MsgBox.Warn(string.Format("单据“{0}”已经设置为“发货完成”，不允许多次执行。", header.BillNO));
                return;
            }

            if (header.Status == "68" || header.Status == "693")
            {
                MsgBox.Warn(string.Format("单据“{0}”已经“发货完成”，不允许“取消”。", header.BillNO));
                return;
            }

            if (MsgBox.AskOK(string.Format("确认要将单据“{0}”设置为“发货完成”吗？", header.BillNO)) != DialogResult.OK)
                return;

            try
            {
                //FrmTempAuthorize frmAuthorize = new FrmTempAuthorize("称重复核员");
                //if (frmAuthorize.ShowDialog() == DialogResult.OK)
                //{
                //    soDal.CloseBill(header.BillID, GlobeSettings.LoginedUser.UserName);
                //    //成功，刷新界面即可，不再提示

                //    Query(1, DateTime.Now, DateTime.Now);
                //    IParent.RefreshHeaderGrid();
                //    LogDal.Insert(ELogType.订单状态变更, GlobeSettings.LoginedUser.UserName, header.BillNO, "称重复核员：" + frmAuthorize.AuthUserCode, "出库单管理");
                //}
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        public bool SetBillStatesSend(int billID, string state, int vehicleID, string username)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("userName=").Append(username).Append("&");
                loStr.Append("billID=").Append(billID).Append("&");
                loStr.Append("state=").Append(state).Append("&");
                loStr.Append("vehicleID=").Append(vehicleID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SetBillStatesSend);
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

        public void SetBillState()
        {
            SOHeaderEntity header = IParent.GetFocusedBill();
            if (header == null)
            {
                MsgBox.Warn("请选中要设置为“等待发货”的行。");
                return;
            }
            if (header.Status != "61")
            {
                MsgBox.Warn("只有等待拣配的订单才能设置等待装车。");
                return;
            }
            try
            {
                using (FrmTempAuthorize frmAuto = new FrmTempAuthorize("称重复核员"))
                {
                    if (frmAuto.ShowDialog() == DialogResult.OK)
                    {
                        //int ret = soDal.SetBillStates(header.BillID, "66", 0);
                        //if (ret > 0)
                        bool ret = SetBillStatesSend(header.BillID, "66", 0, GlobeSettings.LoginedUser.UserName);
                        if (ret)
                        {
                            Query(1, DateTime.Now, DateTime.Now);
                        }
                        else
                        {
                            MsgBox.Warn("该订单已经生成拣货任务，必须按照流程完成订单。");
                        }
                        //LogDal.Insert(ELogType.订单状态变更, GlobeSettings.LoginedUser.UserName, header.BillNO, "手动[等待装车]", "出库单管理");
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 出库单管理：修改出库方式
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool UpdateOutstoreStype(int billID, string type)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billID=").Append(billID).Append("&");
                loStr.Append("type=").Append(type);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_UpdateOutstoreStype);
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

        public void UpdateOutstoreStype()
        {
            SOHeaderEntity header = IParent.GetFocusedBill();
            if (header == null)
            {
                MsgBox.Warn("请选择要操作的订单。");
                return;
            }
            if (header.Status != "60")
            {
                MsgBox.Warn("只有等待排序的订单才能更改出库方式。");
                return;
            }
            try
            {
                FrmOutstoreTypeModify frmOutstoreTypeModify = new FrmOutstoreTypeModify(header);
                if (frmOutstoreTypeModify.ShowDialog() == DialogResult.OK)
                {
                    bool ret = UpdateOutstoreStype(header.BillID, frmOutstoreTypeModify.ItemValue);
                    if (ret)
                    {
                        Query(1, DateTime.Now, DateTime.Now);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 取消单据，并清除任务
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="userName"></param>
        public bool CancelBill(int billID, string userName, int StoreType)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billID=").Append(billID).Append("&");
                loStr.Append("warehouseType=").Append(StoreType).Append("&");
                loStr.Append("userName=").Append(userName);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_CancelBill);
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

        public void CancelOrder()
        {
            SOHeaderEntity header = IParent.GetFocusedBill();
            if (header == null)
            {
                MsgBox.Warn("请选择要“取消”的订单。");
                return;
            }
            //先从界面上判断一下，减少网络交互和数据库负载
            if (header.Status == "693")
            {
                MsgBox.Warn(string.Format("单据“{0}”已发车，不允许取消。", header.BillNO));
                return;
            }
            else if (header.CancelFlag == 1)
            {
                MsgBox.Warn(string.Format("单据“{0}”已经被“取消”，不允许多次执行。", header.BillNO));
                return;
            }
            if (MsgBox.AskOK(string.Format("确认要将单据“{0}”执行“取消”操作吗？", header.BillNO)) != DialogResult.OK)
                return;
            try
            {
                FrmTempAuthorize frmAuthorize = new FrmTempAuthorize("称重复核员");
                if (frmAuthorize.ShowDialog() == DialogResult.OK)
                {
                    //string errorStr = soDal.CancelBill(header.BillID, GlobeSettings.LoginedUser.UserName);
                    //if (string.IsNullOrEmpty(errorStr))
                    bool errorStr = CancelBill(header.BillID, GlobeSettings.LoginedUser.UserName,
                        EUtilStroreType.WarehouseTypeToInt(GlobeSettings.LoginedUser.WarehouseType));
                    if (errorStr)
                    {
                        //成功，刷新界面即可，不再提示
                        Query(1, DateTime.Now, DateTime.Now);
                        IParent.RefreshHeaderGrid();
                        // 如果为整货仓，提示用户；如果该订单物流箱已接收，系统已将商品库存转移到900货位
                        if (GlobeSettings.LoginedUser.WarehouseType == EWarehouseType.整货仓)
                        {
                            MsgBox.OK("如果当前订单存在已接收的物流箱，系统会将散货商品转移到900-01-01货位。");
                        }
                        // 存储过程中已在 WM_SO_LOG 表里记录日志
                        //LogDal.Insert(ELogType.订单状态变更, GlobeSettings.LoginedUser.UserName, header.BillNO, "称重复核员：" + frmAuthorize.AuthUserCode, "出库单管理");
                    }
                    //else
                    //{
                    //    throw new Exception(errorStr);
                    //}
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        public void ContainerDescribe() 
        {
            SOHeaderEntity header = IParent.GetFocusedBill();
            if (header == null)
            {
                MsgBox.Warn("请选中要查看的行。");
            }
            else
            {
                FrmSOContainerDescribe frmWeightRecords = new FrmSOContainerDescribe(header.BillID, header.BillNO);
                frmWeightRecords.ShowDialog();
            }
        }
    }
}
