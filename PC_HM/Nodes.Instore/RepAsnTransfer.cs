using System;
using System.Collections.Generic;
using System.IO;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Shares;
using Nodes.Utils;
using Nodes.UI;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Instore;
using Newtonsoft.Json;

namespace Nodes.WMS.Inbound
{
    /// <summary>
    /// 调拨入库单
    /// </summary>
    public partial class RepAsnTransfer : DevExpress.XtraReports.UI.XtraReport
    {
        public readonly string RepFileName = "RepAsnTransfer.repx";
        public short copies = 1;
        public int BillID = -1;
        AsnDal asnDal = new AsnDal();
        AsnQueryDal asnQueryDal = new AsnQueryDal();
        ASNBody dataSource = null;
        AsnHeaderEntity header = null;

        public RepAsnTransfer()
        {
            InitializeComponent();

            string reportFilePath = Path.Combine(GlobeSettings.AppPath, RepFileName);
            if (File.Exists(reportFilePath)) this.LoadLayout(reportFilePath);

            this.PrintingSystem.StartPrint += new DevExpress.XtraPrinting.PrintDocumentEventHandler(PrintingSystem_StartPrint);
        }

        /// <summary>
        /// 收货单据管理，打印--查询订单主表信息
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public AsnHeaderEntity GetBillHeader(int billID)
        {
            AsnBodyEntity tm = new AsnBodyEntity();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetBillHeader);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tm;
                }
                #endregion

                #region 正常错误处理

                JsonBills bill = JsonConvert.DeserializeObject<JsonBills>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return tm;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return tm;
                }
                #endregion
                List<AsnBodyEntity> list = new List<AsnBodyEntity>();
                #region 赋值数据
                foreach (JsonBillsResult jbr in bill.result)
                {
                    AsnBodyEntity asnEntity = new AsnBodyEntity();
                    asnEntity.OriginalBillNO = jbr.originalBillNo;
                    asnEntity.InstoreTypeDesc = jbr.instoreTypeDesc;
                    asnEntity.Creator = jbr.creator;
                    asnEntity.InstoreType = jbr.instoreType;
                    asnEntity.ContractNO = jbr.contractNo;
                    asnEntity.BillType = jbr.billType;
                    //nameS
                    asnEntity.RowForeColor = Convert.ToInt32(jbr.rowColor);
                    asnEntity.BillState = jbr.billState;
                    asnEntity.BillStateDesc = jbr.billStateDesc;
                    asnEntity.Remark = jbr.remark;

                    #region
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.closeDate))
                            asnEntity.CloseDate = Convert.ToDateTime(jbr.closeDate);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("RepAsnTransfer+GetBillHeader", msg);
                    }
                    #endregion

                    #region
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.printedTime))
                            asnEntity.PrintedTime = Convert.ToDateTime(jbr.printedTime);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("RepAsnTransfer+GetBillHeader", msg);
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
                        //LogHelper.errorLog("RepAsnTransfer+GetBillHeader", msg);
                    }
                    #endregion

                    asnEntity.WmsRemark = jbr.wmsRemark;
                    asnEntity.Printed = Convert.ToInt32(jbr.printed);
                    //sCode
                    //asnEntity.SupplierCode = jbr.cName;
                    asnEntity.SupplierName = jbr.cName;
                    asnEntity.BillID = Convert.ToInt32(jbr.billId);
                    asnEntity.BillNO = jbr.billNo;
                    asnEntity.Sales = jbr.salesMan;
                    asnEntity.BillTypeDesc = jbr.billTypeDesc;
                    list.Add(asnEntity);
                }
                if (list.Count > 0)
                    return list[0];
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return tm;
        }

        /// <summary>
        /// 收货单据管理，打印---查询仓库信息
        /// </summary>
        /// <param name="whCode"></param>
        /// <returns></returns>
        public WarehouseEntity GetWarehouseByCode(string whCode)
        {
            WarehouseEntity tm = new WarehouseEntity();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("whCode=").Append(whCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetWarehouseByCode);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tm;
                }
                #endregion

                #region 正常错误处理

                JsonGetWarehouseByCode bill = JsonConvert.DeserializeObject<JsonGetWarehouseByCode>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return tm;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return tm;
                }
                #endregion
                List<WarehouseEntity> list = new List<WarehouseEntity>();
                #region 赋值数据
                foreach (JsonGetWarehouseByCodeResult jbr in bill.result)
                {
                    WarehouseEntity asnEntity = new WarehouseEntity();
                    asnEntity.WarehouseCode = jbr.whCode;
                    asnEntity.WarehouseName = jbr.whName;
                    asnEntity.XCoor = jbr.xCoor;
                    asnEntity.YCoor = jbr.yCoor;
                    list.Add(asnEntity);
                }
                if (list.Count > 0)
                    return list[0];
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return tm;
        }

        /// <summary>
        /// 收货单据管理， 查询入库单明细
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public List<PODetailEntity> GetDetailByBillID(int billID)
        {
            List<PODetailEntity> list = new List<PODetailEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetDetailByBillID);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetDetailByBillID bill = JsonConvert.DeserializeObject<JsonGetDetailByBillID>(jsonQuery);
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
                foreach (JsonGetDetailByBillIDResult jbr in bill.result)
                {
                    PODetailEntity asnEntity = new PODetailEntity();
                    asnEntity.Barcode1 = jbr.barCode1;
                    asnEntity.BatchNO = jbr.batchNo;
                    asnEntity.BillID = Convert.ToInt32(jbr.billId);
                    asnEntity.DetailID = Convert.ToInt32(jbr.id);
                    asnEntity.ExpDate = jbr.expDate;
                    //asnEntity.MaterialName = jbr.namePy;
                    asnEntity.MaterialName = jbr.skuName;
                    asnEntity.MaterialNameS = jbr.skuNameS;
                    asnEntity.MaterialCode = jbr.skuCode;
                    asnEntity.PlanQty = Convert.ToInt32(jbr.qty);
                    asnEntity.Price = jbr.price;
                    asnEntity.PutQty = Convert.ToInt32(jbr.putQty);
                    asnEntity.Remark = jbr.remark;
                    asnEntity.Spec = jbr.spec;
                    asnEntity.UnitName = jbr.umName;
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
        /// 收货单据管理，更新打印次数
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="printed"></param>
        /// <returns></returns>
        public bool UpdatePrinted(int billID, int printed)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID).Append("&");
                loStr.Append("printed=").Append(printed);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_UpdatePrinted);
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

        public RepAsnTransfer(int billID, short copies)
            : this()
        {
            BillID = billID;
            this.copies = copies;

            //获取数据
            try
            {
                header = GetBillHeader(BillID);
                WarehouseEntity warehouseEntity = GetWarehouseByCode(header.Sales);
                if (warehouseEntity != null)
                    this.lblFormWarehouse.Text = warehouseEntity.WarehouseName;
                else
                    this.lblFormWarehouse.Text = header.Sales;
                this.xrLabel10.Text = GlobeSettings.LoginedUser.WarehouseName;
                List<PODetailEntity> details = GetDetailByBillID(BillID);

                dataSource = new ASNBody();
                dataSource.Header = header;
                dataSource.Details = details;
                decimal n = 0;
                foreach (PODetailEntity entity in this.dataSource.Details)
                {
                    decimal num = Math.Ceiling(ConvertUtil.ToDecimal(entity.MaterialName.Length) / ConvertUtil.ToDecimal(12));
                    if (num > 2)
                        n += num;
                }
                // 更新打印次数
                if (header.Printed == 0)
                {
                    this.xrLabel7.Text = (header.Printed + 1).ToString();
                    UpdatePrinted(header.BillID, header.Printed);
                    header.Printed++;
                    UpdatePrinted(header.BillID, header.Printed);
                }
                else
                {
                    header.Printed++;
                    this.xrLabel7.Text = header.Printed.ToString();
                    UpdatePrinted(header.BillID, header.Printed);
                }
               
                this.PageHeight = (int)(details.Count + n) * 65 + 920;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        void PrintingSystem_StartPrint(object sender, DevExpress.XtraPrinting.PrintDocumentEventArgs e)
        {
            e.PrintDocument.PrinterSettings.Collate = true;
            e.PrintDocument.PrinterSettings.Copies = this.copies;
        }

        private void XtraReport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.DataSource = dataSource;
            this.DataMember = "Details";
        }

        private void RepAsn_AfterPrint(object sender, EventArgs e)
        {
            //记录打印张数和人
            int pageCount = this.Pages.Count * this.copies;
            //new BillLogDal().SavePrintLog(header.BillNO, pageCount, "打印调拨入库单", GlobeSettings.LoginedUser.UserName);
        }
    }
}
