using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Nodes.Entities;
//using Nodes.DBHelper;
using Nodes.Utils;
using DevExpress.Utils;
using Nodes.Resources;
using DevExpress.XtraGrid.Views.Grid;
using Nodes.UI;
using Nodes.Entities.HttpEntity.Outstore;
using Newtonsoft.Json;

namespace Nodes.Outstore
{
    public partial class FrmPickTaskManager : DevExpress.XtraEditors.XtraForm
    {
        //private SODal soDal = new SODal();

        public FrmPickTaskManager()
        {
            InitializeComponent();
        }

        private void FrmTaskManager_Load(object sender, EventArgs e)
        {
            ImageCollection ic = AppResource.LoadToolImages();
            barManager1.Images = ic;
            barButtonItem1.ImageIndex = (int)AppResource.EIcons.search;
            barButtonItem2.ImageIndex = (int)AppResource.EIcons.search;
            barButtonItem3.ImageIndex = (int)AppResource.EIcons.search;

            //绑定拣货员
            //BindingPickPerson();
            queryNotCreatePickPlanBill();
        }

        //private void BindingPickPerson()
        //{
        //    List<UserEntity> users = new UserDal().ListUsersByRoleAndWarehouse(GlobeSettings.LoginedUser.WarehouseCode, GlobeSettings.SOPickPersonRoleName);
        //    listPickPerson.DataSource = users;
        //}

        /// <summary>
        /// 查询未拣配计算单据信息
        /// </summary>
        private void queryNotCreatePickPlanBill()
        {
            BindQueryResult(1, BaseCodeConstant.SO_WAIT_TASK);
        }

        /// <summary>
        /// 查询未开始拣货单据信息
        /// </summary>
        private void queryNotStartPickBill()
        {
            BindQueryResult(2, BaseCodeConstant.SO_WAIT_TASK + "," + BaseCodeConstant.SO_WAIT_PICKING);
        }

        /// <summary>
        /// 查询进行中的单据信息
        /// </summary>
        private void queryPickingBill()
        {
            BindQueryResult(3, SysCodeConstant.SO_STATUS_PICKING);
        }

        int queryType = 0;

        /// <summary>
        /// 根据订单状态查询，支持多状态情况（用逗号隔开），例如status可以是100901，也可以是100901,100902或'100901','100902'
        /// </summary>
        /// <param name="status"></param>
        /// <param name="warehouse"></param>
        /// <returns></returns>
        public List<SOHeaderEntity> QueryBillsByStatus(string status, int setting)
        {
            List<SOHeaderEntity> list = new List<SOHeaderEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("status=").Append(status).Append("&");
                loStr.Append("setting=").Append(setting);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_QueryBillsByStatus);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonQueryBillsByStatus bill = JsonConvert.DeserializeObject<JsonQueryBillsByStatus>(jsonQuery);
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
                foreach (JsonQueryBillsByStatusResult jbr in bill.result)
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
                    asnEntity.DelayMark = Convert.ToInt32(jbr.delaymark);
                    #endregion

                    #region 11-20
                    asnEntity.FromWarehouse = jbr.fromWhCode;
                    asnEntity.FromWarehouseName = jbr.fromWhName;
                    asnEntity.OutstoreType = jbr.outstoreType;
                    asnEntity.OutstoreTypeName = jbr.outstoreTypeName;
                    asnEntity.PickZnType = jbr.pickZnType;
                    asnEntity.PickZnTypeName = jbr.pickZnTypeName;
                    asnEntity.Remark = jbr.remark;
                    asnEntity.RowForeColor = Convert.ToInt32(jbr.rowColor);
                    asnEntity.SalesMan = jbr.salesMan;
                    asnEntity.ShipNO = jbr.shipNo;
                    #endregion
                    #region 21---
                    asnEntity.StatusName = jbr.statusName;
                    asnEntity.Status = jbr.billState;
                    asnEntity.VehicleNO = jbr.vehicleNo;
                    asnEntity.WmsRemark = jbr.wmsRemark;
                    asnEntity.ShTel = jbr.phone;
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
                        //LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
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

        private void BindQueryResult(int queryType, string billStatus)
        {
            try
            {
                this.queryType = queryType;

                List<SOHeaderEntity> asnHeaderEntitys = QueryBillsByStatus(billStatus, 0);
                bindingSource1.DataSource = asnHeaderEntitys;
                ShowFocusedBillPickPlan();

                QueryConditionDisplay();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 查询条件信息显示
        /// </summary>
        private void QueryConditionDisplay()
        {
            if (this.queryType == 1)
                lblQueryCondition.Text = "过滤条件: <color=royalblue>未任务分派</color>的单据。";
            else if(this.queryType == 2)
                lblQueryCondition.Text = "过滤条件: <color=royalblue>未开始拣货</color>的单据。";
            else if (this.queryType == 3)
                lblQueryCondition.Text = "过滤条件: <color=royalblue>进行中的</color>的单据。";
        }

        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DoClickEvent(ConvertUtil.ToString(e.Item.Tag));
        }

        private void DoClickEvent(string itemTag)
        {
            switch (itemTag)
            {
                case "未分派任务":
                    queryNotCreatePickPlanBill();
                    break;
                case "未开始拣货":
                    queryNotStartPickBill();
                    break;
                case "进行中的任务":
                    queryPickingBill();
                    break;
                default:
                    MsgBox.Warn("未找到为该按钮设置的事件：" + itemTag);
                    break;
            }
        }

        private SOHeaderEntity SelectHeader
        {
            get
            {
                if (gridView1.FocusedRowHandle < 0)
                    return null;
                else
                    return gridView1.GetFocusedRow() as SOHeaderEntity;
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ShowFocusedBillPickPlan();
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

        void ShowFocusedBillPickPlan()
        {
            SOHeaderEntity selectedHeader = SelectHeader;
            if (selectedHeader == null)
            {
                gridControl2.DataSource = null;

                gridView2.ViewCaption = "未选中任何单据";
                //simpleButton1.Enabled = false;
                //listPickPerson.UnCheckAll();
            }
            else
            {
                List<PickPlanEntity> plans = GetPickPlan(selectedHeader.BillID);
                gridControl2.DataSource = plans;
                gridView2.ViewCaption = string.Format("拣货单据：{0}", selectedHeader.BillNO);
                //simpleButton1.Enabled = true;
                //CheckMyPDA(selectedHeader.BillID);
            }
        }

        //private void CheckMyPDA(int billID)
        //{
        //    List<UserEntity> pdas = soDal.LoadPickPersonByBillID(billID);
        //    listPickPerson.UnCheckAll();
        //    foreach (UserEntity pda in pdas)
        //    {
        //        for (int i = 0; i < listPickPerson.ItemCount; i++)
        //        {
        //            if (ConvertUtil.ToString(listPickPerson.GetItemValue(i)) == pda.UserCode)
        //                listPickPerson.SetItemChecked(i, true);
        //        }
        //    }
        //}

        /// <summary>
        /// 拣货计划（获得订单状态 ）
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public SOHeaderEntity GetBillStatus(int billID)
        {
            SOHeaderEntity asnEntity = new SOHeaderEntity();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetBillStatus);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return asnEntity;
                }
                #endregion

                #region 正常错误处理

                JsonGetBillStatus bill = JsonConvert.DeserializeObject<JsonGetBillStatus>(jsonQuery);
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
                List<SOHeaderEntity> list = new List<SOHeaderEntity>();
                
                #region 赋值数据
                foreach (JsonGetBillStatusResult jbr in bill.result)
                {

                    asnEntity.Status = jbr.billState;
                    asnEntity.StatusName = jbr.statusName;
                    //try
                    //{
                    //    if (!string.IsNullOrEmpty(jbr.closeDate))
                    //        asnEntity.CloseDate = Convert.ToDateTime(jbr.closeDate);
                    //    if (!string.IsNullOrEmpty(jbr.createDate))
                    //        asnEntity.CreateDate = Convert.ToDateTime(jbr.createDate);
                    //}
                    //catch (Exception msg)
                    //{
                    //    LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    //}
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

        private void OnSaveTaskClick(object sender, EventArgs e)
        {
            //判断选中的单据状态
            SOHeaderEntity selectedHeader = SelectHeader;
            if (selectedHeader == null)
            {
                MsgBox.Warn("请选中一个单据。");
                return;
            }

            try
            {
                int billID = selectedHeader.BillID;

                //查看单据的状态是否为未分派
                SOHeaderEntity header = GetBillStatus(billID);
                if (header.Status.CompareTo(SysCodeConstant.SO_STATUS_WAITING_PICK) > 0)
                {
                    MsgBox.Warn("只有未开始拣货的单据才能继续。");
                    return;
                }

                if (header.Status == SysCodeConstant.SO_STATUS_WAITING_PICK)
                {
                    if (MsgBox.AskOK("单据已经分派过任务了，是否要重新分派？") != DialogResult.OK)
                        return;

                    //if (listPickPerson.CheckedItems.Count == 0)
                    //{
                    //    if (MsgBox.AskOK("您未勾选任何拣货员，是否要重置单据的状态为“等待任务分派”？") == DialogResult.OK)
                    //    {
                    //        soDal.DeleteTask(billID);
                    //        soDal.UpdateBillStatus(billID, SysCodeConstant.SO_STATUS_WAITING_ASSIGN_TASK);

                    //        //更新界面显示
                    //        UpdateUIState(selectedHeader, billID);
                    //    }

                    //    return;
                    //}
                }

                ////判断是否选中手持
                //if (listPickPerson.CheckedItems.Count == 0)
                //{
                //    MsgBox.Warn("请勾选至少一位拣货员。");
                //    return;
                //}

                //soDal.DeleteTask(billID);
                //foreach (var item in listPickPerson.CheckedItems)
                //{
                //    UserEntity user = item as UserEntity;
                //    soDal.SaveTask(billID, user.UserCode, GlobeSettings.LoginedUser.UserName);
                //}
                //soDal.UpdateBillStatus(billID, SysCodeConstant.SO_STATUS_WAITING_PICK);
                //BillLogDal.WriteStatusUpdate(billID, "205", "903", GlobeSettings.LoginedUser.UserName);

                ////更新界面显示
                //UpdateUIState(selectedHeader, billID);

                //MsgBox.OK("保存成功。");
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void UpdateUIState(SOHeaderEntity selectedHeader, int billID)
        {
            //更新界面显示
            SOHeaderEntity _header = GetBillStatus(billID);
            selectedHeader.Status = _header.Status;
            selectedHeader.StatusName = _header.StatusName;
            bindingSource1.ResetCurrentItem();
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView vw = (sender as GridView);
            try
            {
                SOHeaderEntity header = vw.GetRow(e.RowHandle) as SOHeaderEntity;
                if (header != null)
                {
                    if (header.RowForeColor != null)
                    {
                        e.Appearance.ForeColor = Color.FromArgb(header.RowForeColor.Value);
                    }
                }
            }
            catch (Exception ex) { }
        }
    }
}