using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Nodes.UI;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Resources;
using Nodes.Utils;
using System.Threading;
using DevExpress.XtraReports.UI;
using Nodes.Shares;
using Nodes.Common;
using System.Data;
using System.Collections;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;
using Newtonsoft.Json;

namespace Nodes.Outstore
{
    public partial class FrmLoadingTrain : DevExpress.XtraEditors.XtraForm
    {
        //private SODal soDal = null;
        //private ReturnManageDal returnDal = null;
        //private VehicleDal vehicleDal = null;
        List<SOHeaderEntity> List = null;
        private int ctCode, ctQtyNew, ctQtyOld = 0;
        private string _trainNo = "";
        public FrmLoadingTrain()
        {
            InitializeComponent();
        }

        public VehicleEntity Vehicle
        {
            get
            {
                if (lookUpEdit2.EditValue == null || lookUpEdit2.Text.Trim() == "")
                    return null;
                return lookUpEdit2.EditValue as VehicleEntity;
            }
        }
        
        public String TrainNo
        {
            get
            {
                return _trainNo;
            }
        }

        private void OnFrmLoad(object sender, EventArgs e)
        {
            try
            {
                toolPrint.ImageIndex = (int)AppResource.EIcons.print;

                this.BeginDate.EditValue = DateTime.Now.AddDays(-10);
                this.EndDate.EditValue = DateTime.Now;

                //this.soDal = new SODal();
                //this.returnDal = new ReturnManageDal();
                //this.vehicleDal = new VehicleDal();
                this.lookUpEdit2.Text = "请选择车辆号";
                LoadDataAndBindGrid();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                DoClickEvent(ConvertUtil.ToString(e.Item.Tag));
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void DoClickEvent(string tag)
        {
            switch (tag)
            {
                case "刷新":
                    LoadDataAndBindGrid();
                    OnbtnQueryClick(null, null);
                    break;
                case "打印发车单":
                    PrintLoading();
                    break;
                case "人员维护":
                    CreateUsers();
                    break;
                case "车次信息查询":
                    using (FrmLoadingTrainRecords records = new FrmLoadingTrainRecords())
                    {
                        records.ShowDialog();
                    }
                    break;
                case "回车确认":
                    this.ConfirmTrain();
                    break;
            }
        }

        /// <summary>
        /// 车次信息-人员维护--查询当前车次中车次信息的创建时间
        /// </summary>
        /// <param name="trainNO"></param>
        /// <returns></returns>
        public int GetVHCreateDate(string trainNO)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("trainNo=").Append(trainNO);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetVHCreateDate);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return 13;
                }
                #endregion

                #region 正常错误处理

                JsonGetVHCreateDate bill = JsonConvert.DeserializeObject<JsonGetVHCreateDate>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return 13;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return 13;
                }
                #endregion
                if(bill.result != null && bill.result.Length > 0)
                    return bill.result[0].diff;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return 13;
        }

        /// <summary>
        /// 清除现有的人员数据
        /// </summary>
        /// <param name="trainNO"></param>
        /// <returns></returns>
        public bool ClearUsers(string trainNO)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("trainNo=").Append(trainNO);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_ClearUsers);
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
        /// 车次关联司机-助理
        /// </summary>
        /// <param name="trainNO"></param>
        /// <param name="userName"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public bool CreateUsers(string trainNO, string userName, string userCode)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("trainNo=").Append(trainNO).Append("&");
                loStr.Append("userName=").Append(userName).Append("&");
                loStr.Append("userCode=").Append(userCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_CreateUsers);
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
        /// 关联司机和助理
        /// </summary>
        private void CreateUsers()
        {
            if (TrainNo == "")
            {
                return;
            }
            int hours = GetVHCreateDate(TrainNo);
            if (hours > 12)
            {
                MsgBox.Warn("'" + TrainNo + "'该装车单创建时间已经大于12小时，不能变更人员！");
                return;
            }

            FrmChoosePersonnel frm = new FrmChoosePersonnel(true);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                List<UserEntity> list = frm.SelectedPersonnelList;
                if (list.Count < 0)
                {
                    return;
                }
                bool ret = false;
                ClearUsers(this.TrainNo);
                foreach (UserEntity entity in list)
                {
                    ret = CreateUsers(this.TrainNo, entity.UserName, entity.UserCode);
                }
                LoadData(this._trainNo);
            }
        }

        /// <summary>
        /// 装车信息--查询所有
        /// </summary>
        /// <returns></returns>
        public List<VehicleEntity> GetCarAll()
        {
            List<VehicleEntity> list = new List<VehicleEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("vhNo=").Append(vehicleNO);
                string jsons = string.Empty;
                string jsonQuery = WebWork.SendRequest(jsons, WebWork.URL_GetCarAll);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetCarAll bill = JsonConvert.DeserializeObject<JsonGetCarAll>(jsonQuery);
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
                foreach (JsonGetCarAllResult jbr in bill.result)
                {
                    VehicleEntity asnEntity = new VehicleEntity();
                    asnEntity.ID = Convert.ToInt32(jbr.id);
                    asnEntity.IsActive = jbr.isActive;
                    asnEntity.RouteCode = jbr.rtCode;
                    asnEntity.RouteName = jbr.rtName;
                    asnEntity.UserCode = jbr.userCode;
                    asnEntity.UserName = jbr.userName;
                    asnEntity.UserPhone = jbr.mobilePhone;
                    asnEntity.VehicleCode = jbr.vhCode;
                    asnEntity.VehicleNO = jbr.vhNo;
                    asnEntity.VehicleVolume = Convert.ToDecimal(jbr.vhVolume);
                    asnEntity.VhAttri = jbr.vhAttri;
                    asnEntity.VhType = jbr.vhType;
                    asnEntity.VhAttriStr = jbr.itemDesc;
                    asnEntity.VhTypeStr = jbr.typeDesc;
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

        public void LoadDataAndBindGrid()
        {
            try
            {
                List<VehicleEntity> list = GetCarAll();

                VehicleEntity itm = new VehicleEntity();
                itm.ID = -1;
                itm.VehicleNO = "ALL";
                list.Insert(0, itm);
                this.bindingSource1.DataSource = list;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 打印装车单信息查询
        /// </summary>
        /// <param name="vhTrainNo"></param>
        /// <returns></returns>
        public List<OrderSortDetailPrintEntity> Query(string vhTrainNo)
        {
            List<OrderSortDetailPrintEntity> list = new List<OrderSortDetailPrintEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("vhTrainNo=").Append(vhTrainNo).Append("&");
                loStr.Append("warehouseType=").Append(EUtilStroreType.WarehouseTypeToInt(GlobeSettings.LoginedUser.WarehouseType));
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_Query);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonQuery bill = JsonConvert.DeserializeObject<JsonQuery>(jsonQuery);
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
                foreach (JsonQueryResult jbr in bill.result)
                {
                    OrderSortDetailPrintEntity asnEntity = new OrderSortDetailPrintEntity();
                    asnEntity.BillID = Convert.ToInt32(jbr.billId);
                    asnEntity.BillNo = jbr.billNo;
                    asnEntity.BillRemark = jbr.wmsRemark;
                    asnEntity.CustomerAddress = jbr.address;
                    asnEntity.CustomerName = jbr.cName;
                    asnEntity.FullCount = Convert.ToInt32(jbr.fullCount);
                    asnEntity.OrderSort = Convert.ToInt32(jbr.inVhSort);
                    asnEntity.Warehouse = jbr.whName;
                    //try
                    //{
                    //    if (!string.IsNullOrEmpty(jbr.closeDate))
                    //        asnEntity.CloseDate = Convert.ToDateTime(jbr.closeDate);
                    //    if (!string.IsNullOrEmpty(jbr.printedTime))
                    //        asnEntity.PrintedTime = Convert.ToDateTime(jbr.printedTime);
                    //    if (!string.IsNullOrEmpty(jbr.createDate))
                    //        asnEntity.CreateDate = Convert.ToDateTime(jbr.createDate);
                    //}
                    //catch (Exception msg)
                    //{
                    //    LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    //}
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
        /// 打印装车单getTrainSOUserEntity
        /// </summary>
        /// <param name="trainSO"></param>
        /// <returns></returns>
        public List<UserEntity> GetTrainSOUserEntity(string trainSO)
        {
            List<UserEntity> list = new List<UserEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("vhTrainNo=").Append(trainSO);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetTrainSOUserEntity);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetTrainSOUserEntity bill = JsonConvert.DeserializeObject<JsonGetTrainSOUserEntity>(jsonQuery);
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
                foreach (JsonGetTrainSOUserEntityResult jbr in bill.result)
                {
                    UserEntity asnEntity = new UserEntity();
                    asnEntity.BranchCode = jbr.branchCode;
                    asnEntity.MobilePhone = jbr.mobilePhone;
                    asnEntity.UserCode = jbr.userCode;
                    asnEntity.UserID = Convert.ToInt32(jbr.userId);
                    asnEntity.UserPwd = jbr.pwd;
                    asnEntity.UserName = jbr.userName;
                    //try
                    //{
                    //    if (!string.IsNullOrEmpty(jbr.closeDate))
                    //        asnEntity.CloseDate = Convert.ToDateTime(jbr.closeDate);
                    //    if (!string.IsNullOrEmpty(jbr.printedTime))
                    //        asnEntity.PrintedTime = Convert.ToDateTime(jbr.printedTime);
                    //    if (!string.IsNullOrEmpty(jbr.createDate))
                    //        asnEntity.CreateDate = Convert.ToDateTime(jbr.createDate);
                    //}
                    //catch (Exception msg)
                    //{
                    //    LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    //}
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
        /// 车次信息-打印装车单-获取所有有关联的托盘
        /// </summary>
        /// <returns></returns>
        public  List<ContainerEntity> GetContainerListByBillID()
        {
            List<ContainerEntity> list = new List<ContainerEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("vhTrainNo=").Append(trainSO);
                string jsons = string.Empty;
                string jsonQuery = WebWork.SendRequest(jsons, WebWork.URL_GetContainerListByBillIDNoPara);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetContainerListByBillID bill = JsonConvert.DeserializeObject<JsonGetContainerListByBillID>(jsonQuery);
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
                foreach (JsonGetContainerListByBillIDResult jbr in bill.result)
                {
                    ContainerEntity asnEntity = new ContainerEntity();
                    asnEntity.BillHeadID = Convert.ToInt32(jbr.billHeadId);
                    asnEntity.ContainerCode = jbr.ctCode;
                    asnEntity.ContainerName = jbr.ctName;
                    asnEntity.ContainerType = jbr.ctType;
                    asnEntity.ContainerTypeDesc = jbr.ctTypeDesc;
                    asnEntity.ContainerWeight = Convert.ToDecimal(jbr.ctWeight);
                    asnEntity.IsDelete = Convert.ToInt32(jbr.isDeleted);
                    //try
                    //{
                    //    if (!string.IsNullOrEmpty(jbr.closeDate))
                    //        asnEntity.CloseDate = Convert.ToDateTime(jbr.closeDate);
                    //    if (!string.IsNullOrEmpty(jbr.printedTime))
                    //        asnEntity.PrintedTime = Convert.ToDateTime(jbr.printedTime);
                    //    if (!string.IsNullOrEmpty(jbr.createDate))
                    //        asnEntity.CreateDate = Convert.ToDateTime(jbr.createDate);
                    //}
                    //catch (Exception msg)
                    //{
                    //    LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    //}
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
        /// 车次信息-打印装车单-获取所有有关联的托盘
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="wType"></param>
        /// <returns></returns>
        public  List<ContainerEntity> GetContainerListByBillID(int billID, EWarehouseType wType)
        {
            List<ContainerEntity> list = new List<ContainerEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID).Append("&");
                loStr.Append("warehouseType=").Append(EUtilStroreType.WarehouseTypeToInt(wType));
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetContainerListByBillID);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetContainerListByBillID bill = JsonConvert.DeserializeObject<JsonGetContainerListByBillID>(jsonQuery);
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
                foreach (JsonGetContainerListByBillIDResult jbr in bill.result)
                {
                    ContainerEntity asnEntity = new ContainerEntity();
                    asnEntity.BillHeadID = Convert.ToInt32(jbr.billHeadId);
                    asnEntity.ContainerCode = jbr.ctCode;
                    asnEntity.ContainerName = jbr.ctName;
                    asnEntity.ContainerType = jbr.ctType;
                    asnEntity.ContainerTypeDesc = jbr.ctTypeDesc;
                    asnEntity.ContainerWeight = Convert.ToDecimal(jbr.ctWeight);
                    asnEntity.IsDelete = Convert.ToInt32(jbr.isDeleted);
                    //try
                    //{
                    //    if (!string.IsNullOrEmpty(jbr.closeDate))
                    //        asnEntity.CloseDate = Convert.ToDateTime(jbr.closeDate);
                    //    if (!string.IsNullOrEmpty(jbr.printedTime))
                    //        asnEntity.PrintedTime = Convert.ToDateTime(jbr.printedTime);
                    //    if (!string.IsNullOrEmpty(jbr.createDate))
                    //        asnEntity.CreateDate = Convert.ToDateTime(jbr.createDate);
                    //}
                    //catch (Exception msg)
                    //{
                    //    LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    //}
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
        /// 修改订单状态为 693
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateBillStatus(int billID, string status)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID).Append("&");
                loStr.Append("status=").Append(status);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_UpdateBillStatus);
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
        /// 生成SO的操作日志信息
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="content"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool InsertSOLog(int billID, string content, string userName)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID).Append("&");
                loStr.Append("content=").Append(content).Append("&");
                loStr.Append("userName=").Append(userName);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_InsertSOLog);
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
        /// 打印装车单
        /// </summary>
        private void PrintLoading()
        {
            DataRowView row = this.gridView1.GetFocusedRow() as DataRowView;
            if (row == null)
            {
                MsgBox.Warn("请先选择车次信息！");
                return;
            }
            if (MsgBox.AskOK(string.Format("是否确认打印该车次的装车单？")) != DialogResult.OK)
                return;
            try
            {
                string vh_trainNo = ConvertUtil.ToString(row["VH_TRAIN_NO"]);
                VehicleEntity vehicle = lookUpEdit2.GetSelectedDataRow() as VehicleEntity;
                OrderSortPrintEntity dataSource = new OrderSortPrintEntity();
                dataSource.RandomCode = ConvertUtil.ToString(row["RANDOM_CODE"]);
                List<OrderSortDetailPrintEntity> details = Query(vh_trainNo);
                dataSource.Details = new List<OrderSortDetailPrintEntity>();
                foreach (OrderSortDetailPrintEntity item in details)
                {
                    if (!dataSource.Details.Exists(u => { return u.BillNo == item.BillNo; }))
                    {
                        dataSource.Details.Add(item);
                    }
                }
                dataSource.Warehouse = dataSource.Details.Count > 0 ? dataSource.Details[0].Warehouse : string.Empty;
                dataSource.VehicleNO = ConvertUtil.ToString(row["VH_NO"]);
                dataSource.UserList = GetTrainSOUserEntity(vh_trainNo);
                XtraReport repSO = new RepSOLoading(dataSource, "打印销售发车单");
                List<ContainerEntity> containerList = GetContainerListByBillID();
                foreach (OrderSortDetailPrintEntity item in dataSource.Details)
                {
                    item.BoxList = GetContainerListByBillID(item.BillID, GlobeSettings.LoginedUser.WarehouseType);
                    // 修改订单状态为 693
                    UpdateBillStatus(item.BillID, "693");
                    InsertSOLog(item.BillID, ESOOperationType.已发车.ToString(), GlobeSettings.LoginedUser.UserName);
                    //扣减库存
                    //soDal.PrintCutStock(item.BillID);
                }
                repSO.ShowPreviewDialog();
                //repSO.Print();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 获取车次信息
        /// </summary>
        /// <param name="vehicleNO"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataTable GetTrainSOMsg(string vehicleNO, DateTime beginDate, DateTime endDate)
        {
            #region
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("WH_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("VH_TRAIN_NO", Type.GetType("System.String"));
            tblDatas.Columns.Add("VH_NO", Type.GetType("System.String"));
            tblDatas.Columns.Add("RANDOM_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("VEHICLE_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("USER_PHONE", Type.GetType("System.String"));
            tblDatas.Columns.Add("WHOLE_GOODS", Type.GetType("System.Int32"));
            tblDatas.Columns.Add("BULK_CARGO_QTY", Type.GetType("System.Int32"));
            tblDatas.Columns.Add("USER_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("UPDATE_DATE", Type.GetType("System.DateTime"));
            tblDatas.Columns.Add("SYNC_STATE", Type.GetType("System.String"));
            tblDatas.Columns.Add("CONFIRM_DATE", Type.GetType("System.DateTime"));
            #endregion
            
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("vehicleNo=").Append(vehicleNO).Append("&");
                loStr.Append("beginDate=").Append(beginDate).Append("&");
                loStr.Append("endDate=").Append(endDate);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetTrainSOMsg);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetTrainSOMsg bill = JsonConvert.DeserializeObject<JsonGetTrainSOMsg>(jsonQuery);
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
                foreach (JsonGetTrainSOMsgResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["WH_CODE"] = tm.whCode;
                    newRow["VH_TRAIN_NO"] = tm.vhTrainNo;
                    newRow["VH_NO"] = tm.vhNo;
                    newRow["RANDOM_CODE"] = tm.randomCode;
                    newRow["VEHICLE_NAME"] = tm.vehicleName;
                    newRow["USER_PHONE"] = tm.userPhone;
                    newRow["WHOLE_GOODS"] = Convert.ToInt32(tm.wholeGoods);
                    newRow["BULK_CARGO_QTY"] = Convert.ToInt32(tm.bulkCargoQty);
                    newRow["USER_NAME"] = tm.userName;
                    if(!string.IsNullOrEmpty(tm.updateDate))
                        newRow["UPDATE_DATE"] = Convert.ToDateTime(tm.updateDate);
                    newRow["SYNC_STATE"] = tm.syncState;
                    if (!string.IsNullOrEmpty(tm.confirmDate))
                        newRow["CONFIRM_DATE"] = Convert.ToDateTime(tm.confirmDate);
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

        private void OnbtnQueryClick(object sender, EventArgs e)
        {
            try
            {
                if (Vehicle == null)
                {
                    MsgBox.Warn("请选择车辆信息。");
                    return;
                }

                //string vehicleNO = lookUpEdit2.EditValue.ToString();
                //if (vehicleNO.Length <= 0)
                //{
                //    MsgBox.Warn("请选择车辆信息。");
                //    return;
                //}
                DateTime beginDate = ConvertUtil.ToDatetime(this.BeginDate.EditValue);
                DateTime endDate = ConvertUtil.ToDatetime(this.EndDate.EditValue);
                string vehicleNO = Vehicle.VehicleNO;
                gridControl1.DataSource = GetTrainSOMsg(vehicleNO, beginDate, endDate);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        public void CreateTrainLoading()
        {
            if (this.List == null)
                return;
            if (Vehicle == null)
                return;
            //this.soDal.CreateTrain(GlobeSettings.LoginedUser.WarehouseCode, Vehicle.VehicleNO,GlobeSettings.LoginedUser.UserName, List);
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0)
                return;
            this._trainNo = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "VH_TRAIN_NO").ToString();
            LoadData(this._trainNo);
            gridView1.ViewCaption = String.Format("当前所选车次：{0} ;", this.TrainNo);
        }

        /// <summary>
        /// 获取车次订单明细
        /// </summary>
        /// <param name="trainSO"></param>
        /// <returns></returns>
        public DataTable GetTrainSODetailMsg(string trainSO)
        {
            #region
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("订单号", Type.GetType("System.String"));
            tblDatas.Columns.Add("装车顺序", Type.GetType("System.Int32"));
            tblDatas.Columns.Add("原始订单编号", Type.GetType("System.String"));
            tblDatas.Columns.Add("更新时间", Type.GetType("System.String"));
            tblDatas.Columns.Add("业务员", Type.GetType("System.String"));
            tblDatas.Columns.Add("联系电话", Type.GetType("System.String"));
            tblDatas.Columns.Add("客户名称", Type.GetType("System.String"));
            tblDatas.Columns.Add("客户姓名", Type.GetType("System.String"));
            tblDatas.Columns.Add("客户电话", Type.GetType("System.String"));
            tblDatas.Columns.Add("客户地址", Type.GetType("System.String"));
            #endregion
            
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("trainSo=").Append(trainSO);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetTrainSODetailMsg);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetTrainSODetailMsg bill = JsonConvert.DeserializeObject<JsonGetTrainSODetailMsg>(jsonQuery);
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
                foreach (JsonGetTrainSODetailMsgResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["订单号"] = tm.billNo;
                    newRow["装车顺序"] = Convert.ToInt32(tm.inVhSort);
                    newRow["原始订单编号"] = tm.orginalBillNo;
                    newRow["更新时间"] = tm.updateDate;
                    newRow["业务员"] = tm.salesMan;
                    newRow["联系电话"] = tm.contractNo;
                    newRow["客户名称"] = tm.cName;
                    newRow["客户姓名"] = tm.contact;
                    newRow["客户电话"] = tm.phone;
                    newRow["客户地址"] = tm.address;
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
        /// 人员维护--查询人员信息
        /// </summary>
        /// <param name="trainSO"></param>
        /// <returns></returns>
        public DataTable GetTrainSOUsersMsg(string trainSO)
        {
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("用户名称", Type.GetType("System.String"));
            tblDatas.Columns.Add("用户编码", Type.GetType("System.String"));
            tblDatas.Columns.Add("更新时间", Type.GetType("System.String"));
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("trainNo=").Append(trainSO);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetTrainSOUsersMsg);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetTrainSOUsersMsg bill = JsonConvert.DeserializeObject<JsonGetTrainSOUsersMsg>(jsonQuery);
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
                foreach (JsonGetTrainSOUsersMsgResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["用户名称"] = tm.userName;
                    newRow["用户编码"] = tm.userCode;
                    newRow["更新时间"] = tm.updateDate;
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

        private void LoadData(string trainNO)
        {
            DataTable dt = GetTrainSODetailMsg(trainNO);
            ArrayList indexList = new ArrayList();
            // 找出待删除的行索引   
            for (int i = 0; i < dt.Rows.Count - 1; i++)
            {
                if (!IsContain(indexList, i))
                {
                    for (int j = i + 1; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[i]["订单号"].ToString() == dt.Rows[j]["订单号"].ToString())
                        {
                            indexList.Add(j);
                        }
                    }
                }
            }
            indexList.Sort();
            // 排序
            for (int i = indexList.Count - 1; i >= 0; i--)// 根据待删除索引列表删除行  
            {
                int index = Convert.ToInt32(indexList[i]);
                dt.Rows.RemoveAt(index);
            }

            gridControl3.DataSource = dt;
            gridControl3.RefreshDataSource();
            gridControl2.DataSource = GetTrainSOUsersMsg(trainNO);
            gridControl2.RefreshDataSource();
        }
        /// <summary>   
        /// 判断数组中是否存在   
        /// </summary>   
        /// <param name="indexList">数组</param>   
        /// <param name="index">索引</param>   
        /// <returns></returns>   
        public bool IsContain(ArrayList indexList, int index)
        {
            for (int i = 0; i < indexList.Count; i++)
            {
                int tempIndex = Convert.ToInt32(indexList[i]);
                if (tempIndex == index)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 回车确认
        /// </summary>
        /// <param name="trainNo"></param>
        /// <returns></returns>
        public  int GetBulkCargoQty(string trainNo)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("trainNo=").Append(trainNo);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetBulkCargoQty);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return -1;
                }
                #endregion

                #region 正常错误处理

                JsonGetBulkCargoQty bill = JsonConvert.DeserializeObject<JsonGetBulkCargoQty>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return -1;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return -1;
                }
                #endregion
                if(bill.result != null && bill.result.Length > 0)
                    return Convert.ToInt32(bill.result[0].bulkCargoQty);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return -1;
        }

        /// <summary>
        /// 回车确认
        /// </summary>
        /// <param name="trainNo"></param>
        /// <param name="ctQty"></param>
        /// <returns></returns>
        public bool ConfirmTrain(string trainNo, int ctQty)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("trainNo=").Append(trainNo).Append("&");
                loStr.Append("ctQty=").Append(ctQty);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_ConfirmTrain);
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
        /// 装车信息--完成装车2,再次查询信息
        /// </summary>
        /// <param name="vhNO"></param>
        /// <param name="tpyeOpe"></param>
        /// <returns></returns>
        public List<SOHeaderEntity> GetHeaderInfoByBillNOS(string vhNO, int tpyeOpe)
        {
            List<SOHeaderEntity> list = new List<SOHeaderEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("vhNo=").Append(vhNO).Append("&");
                loStr.Append("type=").Append(tpyeOpe);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetHeaderInfoByBillNOS);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetHeaderInfoByBillNOS bill = JsonConvert.DeserializeObject<JsonGetHeaderInfoByBillNOS>(jsonQuery);
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
                foreach (JsonGetHeaderInfoByBillNOSResult jbr in bill.result)
                {
                    SOHeaderEntity asnEntity = new SOHeaderEntity();
                    asnEntity.BillID = Convert.ToInt32(jbr.billId);
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
        /// 回车确认
        /// </summary>
        public void ConfirmTrain()
        {
            try
            {
                DataRowView row = this.gridView1.GetFocusedRow() as DataRowView;
                if (row == null)
                {
                    MsgBox.Warn("请先选择车次信息！");
                    return;
                }
                object confirmDate = row["CONFIRM_DATE"];
                if (confirmDate != null && confirmDate.ToString() != string.Empty)
                {
                    MsgBox.Warn("不允许多次执行<回车确认>操作！");
                    return;
                }
                else
                {

                    string trainNo = ConvertUtil.ToString(row["VH_TRAIN_NO"]);
                    ctQtyOld = GetBulkCargoQty(trainNo);

                    FrmInputNumeral frm = new FrmInputNumeral(ENumeralType.Int32);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        ctQtyNew = ConvertUtil.ToInt(frm.IntQty);

                        ctCode = ctQtyNew;

                    }
                    else
                    {
                        ctCode = ctQtyOld;
                    }


                    if (ConfirmTrain(trainNo, ctCode))
                    {
                        List<SOHeaderEntity> listHeader = GetHeaderInfoByBillNOS(trainNo, 2);
                        foreach (SOHeaderEntity entity in listHeader)
                        {
                            InsertSOLog(entity.BillID, ESOOperationType.已关闭.ToString(), GlobeSettings.LoginedUser.UserName);
                        }
                        this.OnbtnQueryClick(this.btnQuery, EventArgs.Empty);
                        MsgBox.OK("操作成功。");
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
    }
}