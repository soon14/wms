using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using Nodes.Resources;
using Nodes.UI;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Shares;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Instore;
using Newtonsoft.Json;

namespace Nodes.Instore
{
    public partial class FrmCrossInstoreConfirm : DevExpress.XtraEditors.XtraForm
    {
        private CrossInstoreDal instoreDal = new CrossInstoreDal();
        private AsnDal asnDal = null;
        List<PODetailEntity> listDetail = null;
        public FrmCrossInstoreConfirm()
        {
            InitializeComponent();
        }

        private void OnFrmLoad(object sender, EventArgs e)
        {
            ImageCollection ic = AppResource.LoadToolImages();
            barManager1.Images = ic;
            toolAdd.ImageIndex = (int)AppResource.EIcons.add;
            toolEdit.ImageIndex = (int)AppResource.EIcons.edit;
            toolDel.ImageIndex = (int)AppResource.EIcons.delete;
            toolRefresh.ImageIndex = (int)AppResource.EIcons.refresh;
            toolSearch.ImageIndex = (int)AppResource.EIcons.search;
            toolSave.ImageIndex = (int)AppResource.EIcons.save;

            ReloadAsn();
        }

        /// <summary>
        /// 等待复核
        /// </summary>
        /// <param name="warehouseCode"></param>
        /// <param name="billState"></param>
        /// <returns></returns>
        public List<AsnBodyEntity> QueryOverStockBills(string warehouseCode, string billState)
        {
            List<AsnBodyEntity> list = new List<AsnBodyEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("whCode=").Append(warehouseCode).Append("&");
                loStr.Append("billState=").Append(billState);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_QueryOverStockBills);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonQueryOverStockBills bill = JsonConvert.DeserializeObject<JsonQueryOverStockBills>(jsonQuery);
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
                foreach (JsonQueryOverStockBillsResult jbr in bill.result)
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
                    asnEntity.BillStateDesc = jbr.itemDesc;
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
                        //LogHelper.errorLog("FrmCrossInstoreConfirm+QueryOverStockBills", msg);
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
                        //LogHelper.errorLog("FrmCrossInstoreConfirm+QueryOverStockBills", msg);
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
                        //LogHelper.errorLog("FrmCrossInstoreConfirm+QueryOverStockBills", msg);
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
                return list;
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return list;
        }

        private void ReloadAsn()
        {
            try
            {
                //List<AsnBodyEntity> result = QueryOverStockBills(GlobeSettings.LoginedUser.WarehouseCode, "23");
                bindingSource1.DataSource = QueryOverStockBills(GlobeSettings.LoginedUser.WarehouseCode, "23");

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


        void DoClickEvent(string tag)
        {
            switch (tag)
            {
                case "刷新":
                    ReloadAsn();
                    break;
                case "确认入库":
                    Save();
                    break;
            }
        }

        /// <summary>
        /// 越库收货， 获取退库临时收货区
        /// </summary>
        /// <param name="whCode"></param>
        /// <returns></returns>
        public string GetTempZone(string whCode)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("whCode=").Append(whCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetTempZone);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    return null;
                }
                #endregion

                #region 正常错误处理

                JsonGetTempZone bill = JsonConvert.DeserializeObject<JsonGetTempZone>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return null;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return null;
                }
                #endregion
                if(bill.result != null && bill.result.Length > 0)
                    return bill.result[0].lcCode;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return null;
        }

        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool BillState_Change(string state, AsnBodyEntity entity)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billState=").Append(state).Append("&");
                loStr.Append("billId=").Append(entity.BillID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_BillState_Change);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
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
        /// 越库收货确认
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="targetLocCode"></param>
        /// <param name="whCode"></param>
        /// <returns></returns>
        public bool SaveOverStock(PODetailEntity entity, string targetLocCode, string whCode,string username)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("detailId=").Append(entity.DetailID).Append("&");
                //loStr.Append("putQty=").Append(entity.PutQty).Append("&");
                loStr.Append("entity=").Append(JsonConvert.SerializeObject(entity)).Append("&");
                loStr.Append("userName=").Append(username);
                loStr.Append("targetLcCode=").Append(targetLocCode).Append("&");
                loStr.Append("whCode=").Append(whCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveOverStockOK);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
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

        private void Save()
        {
            AsnBodyEntity asnEntity = gvHeader.GetRow(gvHeader.FocusedRowHandle) as AsnBodyEntity;
            int setQty = 0;
            if (gvDetails.RowCount == 0)
                return;
            foreach (PODetailEntity entity in this.listDetail)
            {
                if (entity.PutQty < 0)
                {
                    MsgBox.Warn("越库数量不能小于‘0’！");
                    entity.PutQty = 0;
                    this.gvDetails.RefreshData();
                    return;
                }
                if (entity.PutQty > entity.PlanQty)
                {
                    MsgBox.Warn("越库数量不能大于订单计划量！");
                    entity.PutQty = 0;
                    this.gvDetails.RefreshData();
                    return;
                }
                if (entity.PutQty == 0)
                {
                    setQty++;
                }

            }
            if (setQty == gvDetails.RowCount)
            {
                MsgBox.Warn("请把越库数量全部设置完成之后再确认入库！");
                this.gvDetails.RefreshData();
                return;
            }
            if (MsgBox.AskOK("确定要越库收货吗？") != DialogResult.OK)
            {
                return;
            }
            string lcCode = GetTempZone(GlobeSettings.LoginedUser.WarehouseCode);
            if (String.IsNullOrEmpty(lcCode))
            {
                MsgBox.Warn("请先设置越库收货区！");
                return;
            }
            foreach (PODetailEntity entity in this.listDetail)
            {
                bool result = SaveOverStock(entity, lcCode, GlobeSettings.LoginedUser.WarehouseCode, GlobeSettings.LoginedUser.UserName);
                //LogDal.Insert(ELogType.越库, GlobeSettings.LoginedUser.UserName, ConvertUtil.ToString(entity.BillID), JsonConvert.SerializeObject(entity), "越库收货确认");
            }
            BillState_Change("27", asnEntity);
            ReloadAsn();
            gdDetails.DataSource = null;
            gvDetails.ViewCaption = String.Empty;
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
        private void OnHeadFocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvHeader.FocusedRowHandle < 0)
                return;
            AsnBodyEntity entity = gvHeader.GetRow(gvHeader.FocusedRowHandle) as AsnBodyEntity;
            gvDetails.ViewCaption = String.Format("订单号：{0}   供应商：{1}", entity.BillNO, entity.SupplierName);
            listDetail = GetDetailByBillID(entity.BillID);
            gdDetails.DataSource = listDetail;
        }
    }
}