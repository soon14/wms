using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraEditors;
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
    public partial class UcAsnQueryEngine : UserControl
    {
        AsnQueryDal asnQueryDal = new AsnQueryDal();
        bool hasLoadData = false;

        public UcAsnQueryEngine()
        {
            InitializeComponent();
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
        /// 按照次序排序的供应商列表
        /// </summary>
        /// <returns></returns>
        public List<SupplierEntity> ListActiveSupplierByPriority()
        {
            List<SupplierEntity> list = new List<SupplierEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_ListActiveSupplierByPriority);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonSupplier bill = JsonConvert.DeserializeObject<JsonSupplier>(jsonQuery);
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
                foreach (JsonSupplierResult jbr in bill.result)
                {
                    SupplierEntity asnEntity = new SupplierEntity();
                    asnEntity.Address = jbr.address;
                    asnEntity.AreaID = jbr.areaId;
                    asnEntity.AreaName = jbr.arName;
                    asnEntity.ContactName = jbr.contact;
                    asnEntity.IsActive = jbr.isActive;
                    asnEntity.IsOwn = jbr.isOwn;
                    asnEntity.Phone = jbr.phone;
                    asnEntity.Postcode = jbr.postCode;
                    asnEntity.Remark = jbr.remark;
                    asnEntity.SortOrder = Convert.ToInt32(jbr.sortOrder);
                    asnEntity.SupplierCode = jbr.sCode;
                    asnEntity.SupplierName = jbr.sName;
                    asnEntity.SupplierNameS = jbr.nameS;
                    asnEntity.UpdateBy = jbr.updateBy;
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.updateDate))
                            asnEntity.UpdateDate = Convert.ToDateTime(jbr.updateDate);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("UcAsnQueryEngine+ListActiveSupplierByPriority", msg);
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

        public void LoadDataSource()
        {
            if (hasLoadData)
                return;

            hasLoadData = true;

            dateEditFrom.DateTime = DateTime.Now.AddMonths(-1);
            dateEditTo.DateTime = DateTime.Now;

            try
            {
                //绑定供应商
                listSuppliers.Properties.DataSource = ListActiveSupplierByPriority();

                //绑定业务类型并默认选中第一个，采购单和收货单是同一个数据源
                listBillTypes.Properties.DataSource = GetItemList(BaseCodeConstant.PO_TYPE);

                //绑定单据状态
                listBillStates.Properties.DataSource = GetItemList(BaseCodeConstant.ASN_STATE);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        #region 公开的事件及函数
        public delegate void QueryComplete(List<AsnBodyEntity> dataSource);
        public event QueryComplete QueryCompleted;

        private void InitUI()
        {
            materialCode = null;
            billID = null;
            poNO = null;
            supplierCode = null;
            billTypeCode = null;
            billStateCode = null;
            dateFrom = null;
            dateTo = null;
        }

        private string materialCode;
        private string billID;
        private string poNO;
        private string billStateCode;
        private string supplierCode;
        private string billTypeCode;
        private DateTime? dateFrom;
        private DateTime? dateTo;
        private DateTime? dateComFrom;
        private DateTime? dateComTo;

        public string QueryCondition;
        public string ElapsedTime;
        private bool OnlyNotComplete = false;

        /// <summary>
        /// 锁定某一状态，创建收货单时需要查询二审及正在收货的单据
        /// </summary>
        /// <param name="stateCode"></param>
        public void LockThisState(string stateCode)
        {
            listBillStates.EditValue = stateCode;
            listBillStates.Enabled = false;
            listBillStates.RefreshEditValue();
        }

        /// <summary>
        /// 读取等待到货（也就是没有做到货登记）的单据
        /// </summary>
        /// <param name="warehouseCode"></param>
        /// <returns></returns>
        private List<AsnBodyEntity> QueryNotRelatedBills(string warehouseCode)
        {
            List<AsnBodyEntity> list = new List<AsnBodyEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billState=").Append(BillStateConst.ASN_STATE_CODE_COMPLETE).Append("&");
                loStr.Append("wareHouseCode=").Append(warehouseCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_QueryNotRelatedBills2);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonBills bill = JsonConvert.DeserializeObject<JsonBills>(jsonQuery);
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
                    if (!string.IsNullOrEmpty(jbr.rowColor))
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
                        //LogHelper.errorLog("UcAsnQueryEngine+QueryNotRelatedBills", msg);
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
                        //LogHelper.errorLog("UcAsnQueryEngine+QueryNotRelatedBills", msg);
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
                        //LogHelper.errorLog("UcAsnQueryEngine+QueryNotRelatedBills", msg);
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

        public void DoQueryNotCompleteBill(string queryCondition)
        {
            if (!string.IsNullOrEmpty(queryCondition))
                this.QueryCondition = queryCondition;

            OnlyNotComplete = true;
            watch.Start();
            List<AsnBodyEntity> result = QueryNotRelatedBills(GlobeSettings.LoginedUser.WarehouseCode);
            watch.Stop();

            ElapsedTime = string.Format("查询完成：耗时{0}秒", watch.ElapsedMilliseconds / 1000f);
            watch.Reset();

            if (QueryCompleted != null)
                QueryCompleted(result);
        }

        public void DoQuery(string billStateCode, string queryCondition)
        {
            this.QueryCondition = queryCondition;
            DoQuery(null, billStateCode, null, null, null, null, null, null, null,null,null);
        }

        public void Reload()
        {
            if (OnlyNotComplete)
                DoQueryNotCompleteBill(null);
            else
                DoQuery(this.billID, this.poNO, this.billStateCode, this.supplierCode, this.billTypeCode,
                    this.materialCode, null, this.dateFrom, this.dateTo,this.dateComFrom,this.dateComTo);
        }

        public void DoQuery(DateTime dateFrom, DateTime dateTo, string queryCondition)
        {
            this.QueryCondition = queryCondition;
            DoQuery(null, null, null, null, null, null, null, dateFrom, dateTo,dateComFrom,dateComTo);
        }

        /// <summary>
        /// 收货单据管理， 多条件查询
        /// </summary>
        /// <param name="warehouseCode"></param>
        /// <param name="billID"></param>
        /// <param name="poNO"></param>
        /// <param name="billState"></param>
        /// <param name="supplier"></param>
        /// <param name="billType"></param>
        /// <param name="material"></param>
        /// <param name="sales"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="dateComFrom"></param>
        /// <param name="dateComTo"></param>
        /// <returns></returns>
        public List<AsnBodyEntity> QueryBills(string warehouseCode, string billID, string poNO, string billState, string supplier,
            string billType, string material, string sales, DateTime? dateFrom, DateTime? dateTo, DateTime? dateComFrom, DateTime? dateComTo)
        {
            #region 组装slq条件
            //string strWhereCondition = "WHERE H.WH_CODE = @WH_CODE";

            ////建单日期
            //if (dateFrom.HasValue)
            //{
            //    strWhereCondition += " AND H.CREATE_DATE >= @P_CREATE_DATE_FROM";
            //}

            //if (dateTo.HasValue)
            //{
            //    strWhereCondition += " AND H.CREATE_DATE <= @P_CREATE_DATE_TO";
            //}
            ////最后更新日期
            //if (dateComFrom.HasValue)
            //{
            //    strWhereCondition += " AND H.LAST_UPDATETIME >= @P_CLODE_DATE_FROM";
            //}

            //if (dateComTo.HasValue)
            //{
            //    strWhereCondition += " AND H.LAST_UPDATETIME <= @P_CLODE_DATE_TO";
            //}

            ////单据编号
            //if (!string.IsNullOrEmpty(billID))
            //{
            //    strWhereCondition += " AND H.BILL_NO = @P_BILL_NO";
            //}

            ////原采购单编号
            //if (!string.IsNullOrEmpty(poNO))
            //{
            //    strWhereCondition += " AND H.PO_NO = @P_PO_NO";
            //}

            ////供应商
            //if (!string.IsNullOrEmpty(supplier))
            //{
            //    strWhereCondition += " AND H.SUPPLIER = @P_SUPPLIER";
            //}

            ////业务类型
            //if (!string.IsNullOrEmpty(billType))
            //{
            //    strWhereCondition += " AND H.BILL_TYPE = @P_BILL_TYPE";
            //}

            ////业务员
            //if (!string.IsNullOrEmpty(sales))
            //{
            //    strWhereCondition += " AND H.SALES = @P_SALES";
            //}

            ////状态有可能是多个，这个需要转换为OR，直接拼接成字符串，不用参数了
            //if (!string.IsNullOrEmpty(billState))
            //{
            //    //假设billState=12,13,15，函数FormatParameter转换为BILL_STATE = '12' OR BILL_STATE = '13' OR BILL_STATE = '15'
            //    strWhereCondition += string.Concat(" AND (", DBUtil.FormatParameter("H.BILL_STATE", billState), ")");
            //}

            ////物料编码或名称，支持模糊查询，因为物料在明细表中，反查出的主表数据会重复，所以要用DISTINCT
            ////另外不要使用字段拼接，oracle和sql的语法不一样
            //if (!string.IsNullOrEmpty(material))
            //{
            //    strWhereCondition += " AND EXISTS(SELECT 1 FROM WM_ASN_DETAIL D INNER JOIN WM_MATERIALS M ON D.MTL_CODE = M.MTL_CODE WHERE H.BILL_ID = D.BILL_ID AND (D.MTL_CODE like @P_MTL_CODE OR M.MTL_NAME LIKE @P_MTL_CODE OR M.MTL_NAME_S LIKE @P_MTL_CODE OR M.NAME_PY LIKE @P_MTL_CODE))";
            //}
            #endregion

            List<AsnBodyEntity> list = new List<AsnBodyEntity>();

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("warehouseCode=").Append(warehouseCode).Append("&");
                loStr.Append("dateFrom=").Append(dateFrom).Append("&");
                loStr.Append("dateTo=").Append(dateTo).Append("&");
                loStr.Append("dateComFrom=").Append(dateComFrom).Append("&");
                loStr.Append("dateComTo=").Append(dateComTo).Append("&");
                loStr.Append("billID=").Append(billID).Append("&");
                loStr.Append("poNO=").Append(poNO).Append("&");
                loStr.Append("billState=").Append(billState).Append("&");
                loStr.Append("billType=").Append(billType).Append("&");
                loStr.Append("material=").Append(material).Append("&");
                loStr.Append("sales=").Append(sales).Append("&");
                loStr.Append("supplier=").Append(supplier);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_QueryBills);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonBills bill = JsonConvert.DeserializeObject<JsonBills>(jsonQuery);
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
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.closeDate))
                            asnEntity.CloseDate = Convert.ToDateTime(jbr.closeDate);
                        
                        
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("UcAsnQueryEngine+QueryBills", msg);
                    }


                    try
                    {

                        if (!string.IsNullOrEmpty(jbr.printedTime))
                            asnEntity.PrintedTime = Convert.ToDateTime(jbr.printedTime);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("UcAsnQueryEngine+QueryBills", msg);
                    }

                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.createDate))
                            asnEntity.CreateDate = Convert.ToDateTime(jbr.createDate);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("UcAsnQueryEngine+QueryBills", msg);
                    }
                    


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

        Stopwatch watch = new Stopwatch();
        private void DoQuery(string billID, string poNO, string billStateCode, string supplierCode,
            string billTypeCode, string materialCode, string sales, DateTime? dateFrom, DateTime? dateTo, DateTime? dateComFrom, DateTime? dateComTo)
        {
            try
            {
                OnlyNotComplete = false;

                this.billID = billID;
                this.poNO = poNO;
                this.billStateCode = billStateCode;
                this.supplierCode = supplierCode;
                this.billTypeCode = billTypeCode;
                this.materialCode = materialCode;
                this.dateFrom = dateFrom;
                this.dateTo = dateTo;
                this.dateComFrom = dateComFrom;
                this.dateComTo = dateComTo;

                watch.Start();
                List<AsnBodyEntity> result = QueryBills(GlobeSettings.LoginedUser.WarehouseCode, billID, poNO,
                    billStateCode, supplierCode, billTypeCode, materialCode, null, dateFrom, dateTo, dateComFrom, dateComTo);
                watch.Stop();

                ElapsedTime = string.Format("查询完成：耗时{0}秒", watch.ElapsedMilliseconds / 1000f);
                watch.Reset();

                if (QueryCompleted != null)
                    QueryCompleted(result);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        #endregion

        private void OnQueryClick(object sender, EventArgs e)
        {
            string billID = ConvertUtil.StringToNull(txtBillID.Text);
            string poNO = ConvertUtil.StringToNull(txtPoNO.Text);
            string billStateCode = ConvertUtil.ObjectToNull(listBillStates.EditValue);
            string billTypeCode = ConvertUtil.ObjectToNull(listBillTypes.EditValue);
            string supplierCode = ConvertUtil.ObjectToNull(listSuppliers.EditValue);
            string materialCode = ConvertUtil.StringToNull(txtMaterial.Text);
            DateTime? dateFrom = null, dateTo = null;
            if (dateEditFrom.EditValue != null)
                dateFrom = dateEditFrom.DateTime.Date;

            if (dateEditTo.EditValue != null)
                dateTo = dateEditTo.DateTime.AddDays(1).Date;

            if (dateCloseStart.EditValue != null)
                dateComFrom = dateCloseStart.DateTime.Date;

            if (dateCloseEnd.EditValue != null)
                dateComTo = dateCloseEnd.DateTime.AddDays(1).Date;


            string separator = " && ";
            this.QueryCondition = string.Format(@"{0}{1}{2}{3}{4}{5}{6}",
                string.IsNullOrEmpty(billID) ? "" : "收货单号=" + billID + separator,
                string.IsNullOrEmpty(supplierCode) ? "" : "供应商=" + listSuppliers.Text + separator,
                string.IsNullOrEmpty(billTypeCode) ? "" : "业务类型=" + listBillTypes.Text + separator,
                string.IsNullOrEmpty(poNO) ? "" : "采购单号=" + poNO + separator,
                string.IsNullOrEmpty(billStateCode) ? "" : "单据状态=" + listBillStates.Text + separator,
                string.IsNullOrEmpty(materialCode) ? "" : "物料包含'" + materialCode + "'" + separator,
                //string.IsNullOrEmpty(sales) ? "" : "业务员=" + sales + separator,
                "建单日期介于【" + dateFrom.Value.ToShortDateString() + "】与【" + dateTo.Value.AddDays(-1).ToShortDateString() + "】之间");

            DoQuery(billID, poNO, billStateCode, supplierCode, billTypeCode,
                materialCode, null, dateFrom, dateTo,dateComFrom, dateComTo);
        }

        private void OnCleanTextClick(object sender, EventArgs e)
        {
            txtBillID.Text = txtMaterial.Text = txtPoNO.Text = null;
            listBillStates.EditValue = listBillTypes.EditValue = listSuppliers.EditValue = null;
        }

        private void OnControlKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                OnQueryClick(sender, e);
        }

        private void OnLookUpEditButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            BaseEdit editor = sender as BaseEdit;
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
                editor.EditValue = null;
        }

        private void OnBillStateButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
            {
                listBillStates.EditValue = null;
                listBillStates.RefreshEditValue();
            }
        }
    }
}
