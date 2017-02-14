using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using Nodes.DBHelper;
using Nodes.Entities;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Nodes.Utils;
using Nodes.UI;
using Nodes.Shares;
using DevExpress.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;
using Newtonsoft.Json;

namespace Nodes.Outstore
{
    /// <summary>
    /// 待装车订单
    /// </summary>
    public partial class FrmNonLoadingBills : DevExpress.XtraEditors.XtraForm
    {
        #region 变量
        //private SODal _soDal = new SODal();
        //private VehicleDal _vehicleDal = new VehicleDal();
        //private CallingDal callDal = new CallingDal();
        private List<UserEntity> userVehicle = null;
        private List<UserEntity> userWeight = null;
        #endregion

        #region 构造函数

        public FrmNonLoadingBills()
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

        /// <summary>
        /// 获取等待称重并且未生成装车任务的订单
        /// </summary>
        /// <returns></returns>
        public List<SOHeaderEntity> GetUnLoadingBills()
        {
            List<SOHeaderEntity> list = new List<SOHeaderEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                string jsons = string.Empty;
                string jsonQuery = WebWork.SendRequest(jsons, WebWork.URL_GetUnLoadingBills);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetUnLoadingBills bill = JsonConvert.DeserializeObject<JsonGetUnLoadingBills>(jsonQuery);
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
                foreach (JsonGetUnLoadingBillsResult jbr in bill.result)
                {
                    SOHeaderEntity asnEntity = new SOHeaderEntity();
                    #region 0-10
                    asnEntity.Address = jbr.address;
                    asnEntity.Attri1 = Convert.ToInt32(jbr.attri1);
                    asnEntity.BillID = Convert.ToInt32(jbr.billId);
                    asnEntity.BillNO = jbr.billNo;
                    asnEntity.BillType = jbr.billType;
                    asnEntity.BillTypeName = jbr.billTypeName;
                    asnEntity.BoxNum = Convert.ToInt32(jbr.boxNum);
                    asnEntity.CaseBoxNum = Convert.ToInt32(jbr.caseBoxNum);
                    asnEntity.Consignee = jbr.contact;
                    asnEntity.ContractNO = jbr.contractNo;
                    #endregion
                    #region 11-20
                    asnEntity.CustomerName = jbr.cName;
                    asnEntity.DelayMark = Convert.ToInt32(jbr.delaymark);
                    asnEntity.FromWarehouse = jbr.fromWhCode;
                    asnEntity.OrderSort = Convert.ToInt32(jbr.orderSort);
                    asnEntity.OutstoreType = jbr.outstoreType;
                    asnEntity.OutstoreTypeName = jbr.outstoreTypeName;
                    asnEntity.Remark = jbr.remark;
                    asnEntity.RowForeColor = Convert.ToInt32(jbr.rowColor);
                    asnEntity.SalesMan = jbr.salesMan;
                    asnEntity.ShipNO = jbr.shipNo;
                    #endregion
                    #region 21-30
                    asnEntity.ShTel = jbr.phone;
                    asnEntity.Status = jbr.billState;
                    asnEntity.StatusName = jbr.statusName;
                    asnEntity.VehicleNO = jbr.vehicleNo;
                    asnEntity.FromWarehouseName = jbr.whName;
                    asnEntity.WmsRemark = jbr.wmsRemark;
                    asnEntity.XCoor = Convert.ToDecimal(jbr.xCoor);
                    asnEntity.YCoor = Convert.ToDecimal(jbr.yCoor);
                    asnEntity.PickZnType = jbr.znType;
                    asnEntity.PickZnTypeName = jbr.znTypeName;
                    #endregion
                    asnEntity.CustomerCode = jbr.cCode;

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

        #region 方法
        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {

            List<SOHeaderEntity> list = GetUnLoadingBills();
            this.gridHeader.DataSource = list;
            // 获取车辆信息
            this.bindingSource1.DataSource = GetCarAll();
            // 获取车辆信息
            userVehicle = ListUsersByRoleAndWarehouseCode(
               GlobeSettings.LoginedUser.WarehouseCode, "装车员");
            this.listPersonnel.DataSource = userVehicle;
            this.listPersonnel.DisplayMember = "UserName";

            // 获取称重人员
            userWeight = ListUsersByRoleAndWarehouseCode(
               GlobeSettings.LoginedUser.WarehouseCode, "称重员");
            this.listTransPersonal.DataSource = userWeight;
            this.listTransPersonal.DisplayMember = "UserName";
        }

        /// <summary>
        /// 根据订单ID查看关联笼车是否都已接收
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public bool IsReceiveContainer(int billID)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_IsReceiveContainer);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return false;
                }
                #endregion

                #region 正常错误处理

                JsonIsReceiveContainer bill = JsonConvert.DeserializeObject<JsonIsReceiveContainer>(jsonQuery);
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

                if(bill.result != null && bill.result.Length > 0)
                    return bill.result[0].counts == null ? true : ConvertUtil.ToInt(bill.result[0].counts) == 0;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return false;
        }

        /// <summary>
        /// 验证散货信息
        /// </summary>
        /// <param name="errorMsg"></param>
        private void ValidateContainerState(out string errorMsg)
        {
            errorMsg = string.Empty;
            if (GlobeSettings.LoginedUser.WarehouseType != EWarehouseType.整货仓)
                return;
            List<SOHeaderEntity> list = this.SelectedBills;
            if (list == null || list.Count == 0)
                return;
            foreach (SOHeaderEntity entity in list)
            {
                if (entity.DelayMark == 1)
                    continue;
                // 检查同步状态
                if (entity.SyncState < 5 && entity.CaseBoxNum > 0)
                {
                    errorMsg = string.Format("订单：{0} 散货信息还未同步完成，第三方车辆不允许排此单！", entity.BillNO);
                    break;
                }
                // 根据订单查询相关笼车是否都已接收完成
                else if (!IsReceiveContainer(entity.BillID))
                {
                    errorMsg = string.Format("订单：{0} 关联的笼车还未接收，第三方车辆不允许排此单！", entity.BillNO);
                    break;
                }
            }
        }
        #endregion

        #region 属性
        /// <summary>
        /// 获取选择的订单
        /// </summary>
        public List<SOHeaderEntity> SelectedBills
        {
            get
            {
                gvHeader.PostEditor();

                List<SOHeaderEntity> headers = new List<SOHeaderEntity>();
                //获取选中的单据，只处理显示出来的，不考虑由于过滤导致的未显示单据
                for (int i = 0; i < gvHeader.DataRowCount; i++)
                {
                    SOHeaderEntity header = gvHeader.GetRow(i) as SOHeaderEntity;
                    if (header != null && header.HasChecked)
                    {
                        headers.Add(header);
                    }
                }
                return headers;
            }
        }
        #endregion

        #region Override Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.LoadCheckBoxImage();
            this.LoadData();
        }
        #endregion

        #region "选中与复选框"
        private void LoadCheckBoxImage()
        {
            gvHeader.Images = GridUtil.GetCheckBoxImages();
            colCheck.ImageIndex = 0;
        }

        private void OnViewMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                CheckOneGridColumn(gvHeader, "HasChecked", MousePosition);
            }
        }

        private void OnViewCellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName != "HasChecked") return;
            SOHeaderEntity selectedHeader = gvHeader.GetFocusedRow() as SOHeaderEntity;
            if (selectedHeader == null) return;

            selectedHeader.HasChecked = ConvertUtil.ToBool(e.Value);
            gvHeader.CloseEditor();
        }

        private void CheckOneGridColumn(GridView view, string checkedField, Point mousePosition)
        {
            Point p = view.GridControl.PointToClient(mousePosition);
            GridHitInfo hitInfo = view.CalcHitInfo(p);
            #region
            if (hitInfo.HitTest == GridHitTest.Column && hitInfo.Column.FieldName == checkedField)
            {
                List<SOHeaderEntity> _data = gridHeader.DataSource as List<SOHeaderEntity>;
                if (_data == null) return;

                int currentIndex = hitInfo.Column.ImageIndex;
                bool flag = currentIndex == 0;
                #region
                for (int i = 0; i < gvHeader.RowCount; i++)
                {
                    gvHeader.SetRowCellValue(i, "HasChecked", flag);
                    SOHeaderEntity tmp = gvHeader.GetRow(i) as SOHeaderEntity;
                    if (tmp != null)
                    {
                        //分派装车，查看是否有同一个客户，是否还有其他订单不是当前状态的
                        IsHaveOtherStatus(tmp.CustomerCode);
                    }
                }

                #endregion

                //_data.ForEach(d => d.HasChecked = flag);
                hitInfo.Column.ImageIndex = 4 - currentIndex;
            }
            else
            {
                #region
                SOHeaderEntity selectedHeader = gvHeader.GetFocusedRow() as SOHeaderEntity;
                if (selectedHeader == null) return;

                //分派装车，查看是否有同一个客户，是否还有其他订单不是当前状态的
                IsHaveOtherStatus(selectedHeader.CustomerCode);

                //实现同一客户不同订单置位选中状态
                SetSameCustomerCheck(selectedHeader);
                #endregion
            }
            #endregion

            
        }
        #endregion

        #region 事件

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 生成叫号信息
        /// </summary>
        /// <param name="callType"></param>
        /// <param name="billNO"></param>
        /// <param name="description"></param>
        /// <param name="userCode"></param>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public bool CreateCalling(string callType, string billNO, string description, string userCode, int taskID)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("callType=").Append(callType).Append("&");
                loStr.Append("billNo=").Append(billNO).Append("&");
                loStr.Append("description=").Append(description).Append("&");
                loStr.Append("userCode=").Append(userCode).Append("&");
                loStr.Append("taskId=").Append(taskID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_CreateCalling);
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
        /// 规定装车顺序
        /// </summary>
        private void btnCreateTask_Click(object sender, EventArgs e)
        {
            try
            {
                if (listPersonnel.CheckedItems.Count < 1)
                {
                    MsgBox.Warn("请选择装车人员！");
                    return;
                }
                if (listTransPersonal.CheckedItems.Count < 1)
                {
                    MsgBox.Warn("请选称重人员！");
                    return;
                }
                if (this.SelectedBills.Count == 0)
                {
                    MsgBox.Warn("请选择订单！");
                    return;
                }
                VehicleEntity vehicle = this.searchLookUpEdit1.EditValue as VehicleEntity;
                if (vehicle == null)
                {
                    MsgBox.Warn("请选择车辆！");
                    return;
                }
                // 如果选择的是第三方车辆，验证散货信息
                if (vehicle.VhType == "191")
                {
                    string errorMsg = string.Empty;
                    this.ValidateContainerState(out errorMsg);
                    if (!string.IsNullOrEmpty(errorMsg))
                    {
                        MsgBox.Warn(errorMsg);
                        return;
                    }
                }
                List<UserEntity> userLoadingList = new List<UserEntity>();
                foreach (var obj in this.listPersonnel.CheckedItems)
                {
                    if (obj is UserEntity)
                    {
                        UserEntity user = obj as UserEntity;
                        userLoadingList.Add(user);
                    }
                }

                List<UserEntity> userTransList = new List<UserEntity>();
                foreach (var obj in this.listTransPersonal.CheckedItems)
                {
                    if (obj is UserEntity)
                    {
                        UserEntity user = obj as UserEntity;
                        userTransList.Add(user);
                    }
                }

                using (FrmLoadingSortMap frmSOSortMap = new FrmLoadingSortMap(this.SelectedBills, vehicle, userLoadingList, userTransList))
                {
                    if (frmSOSortMap.ShowDialog() == DialogResult.OK)
                    {
                        //生成叫号信息
                        foreach (UserEntity user in userLoadingList)
                        {
                            CreateCalling(BaseCodeConstant.TASK_LOADING, vehicle.VehicleNO, user.UserName, user.UserCode, -1);
                        }
                        this.LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err("错误：" + ex.Message);
            }
        }


        #region List转换成Json
        private string GetRes<T>(List<T> listobj, List<string> proptylist)
        {

            StringBuilder strb = new StringBuilder();
            List<string> result = new List<string>();
            string curname = default(string);
            foreach (var obj in listobj)
            {

                Type type = obj.GetType();

                curname = type.Name;


                List<string> curobjliststr = new List<string>();
                foreach (var curpropty in proptylist)
                {
                    string tmp = default(string);
                    var res01 = type.GetProperty(curpropty).GetValue(obj, null);
                    if (res01 == null)
                    {
                        tmp = null;
                    }
                    else
                    {
                        tmp = res01.ToString();
                    }
                    curobjliststr.Add("\"" + curpropty + "\"" + ":" + "\"" + tmp + "\"");
                }
                string curres = "{" + string.Join(",", curobjliststr.ToArray()) + "}";
                result.Add(curres);
            }
            strb.Append(":[" + string.Join(",", result.ToArray()) + "]");
            string ret = "\"" + curname + "\"" + strb.ToString();
            ret = ret.Insert(0, "{");
            ret = ret.Insert(ret.Length, "}");
            return ret;
        }

        #endregion

        /// <summary>
        /// 存储排序记录
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        public bool CreateLoadingInfo(LoadingHeaderEntity header)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("warehouseCode=").Append(header.WarehouseCode).Append("&");
                loStr.Append("loadingNO=").Append(header.LoadingNO).Append("&");
                loStr.Append("vehicleID=").Append(header.VehicleID).Append("&");
                loStr.Append("updateDate=").Append(header.UpdateDate).Append("&");
                loStr.Append("userName=").Append(header.UserName).Append("&");
                #region list 转 json
                List<string> prop = new List<string>() { "LoadingNO", "BillNO", "InVehicleSort", "UpdateDate", "BillID" };
                string soHeaderEntity = GetRes<LoadingDetailEntity>(header.Details, prop);
                loStr.Append("jsonDetail=").Append(soHeaderEntity).Append("&");
                List<string> user = new List<string>() { "LoadingNO", "UserName", "UserCode", "UpdateDate", "TaskType" };
                string loadingUserEntity = GetRes<LoadingUserEntity>(header.Users, user);
                loStr.Append("jsonUser=").Append(loadingUserEntity);
                #endregion
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_CreateLoadingInfo);
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
        /// 创建任务
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="taskType"></param>
        /// <returns></returns>
        public bool CreateTask(int billID, string taskType)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID).Append("&");
                loStr.Append("taskType=").Append(taskType);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_CreateTask);
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
        /// 任务自动刷新
        /// </summary>
        /// <returns></returns>
        public bool AutoAssignTask()
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("wareHouseType=").Append(EUtilStroreType.WarehouseTypeToInt(GlobeSettings.LoginedUser.WarehouseType));
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_AutoAssignTask);
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
        /// 引用第一次排序的顺序
        /// </summary>
        private void btnLoadingSort_Click(object sender, EventArgs e)
        {
            try
            {
                if (listPersonnel.CheckedItems.Count < 1)
                {
                    MsgBox.Warn("请选择装车人员！");
                    return;
                }
                if (listTransPersonal.CheckedItems.Count < 1)
                {
                    MsgBox.Warn("请选称重人员！");
                    return;
                }
                if (this.SelectedBills.Count == 0)
                {
                    MsgBox.Warn("请选择订单！");
                    return;
                }
                VehicleEntity vehicle = this.searchLookUpEdit1.EditValue as VehicleEntity;
                if (vehicle == null)
                {
                    MsgBox.Warn("请选择车辆！");
                    return;
                }
                // 如果选择的是第三方车辆，验证散货信息
                if (vehicle.VhType == "191")
                {
                    string errorMsg = string.Empty;
                    this.ValidateContainerState(out errorMsg);
                    if (!string.IsNullOrEmpty(errorMsg))
                    {
                        MsgBox.Warn(errorMsg);
                        return;
                    }
                }
                using (WaitDialogForm waitDialog = new WaitDialogForm())
                {
                    
                    List<SOHeaderEntity> headerList = this.SelectedBills;
                    SOHeaderEntity tempHeader = headerList[0];
                    if (headerList.Exists(u => u.VehicleNO != tempHeader.VehicleNO))
                    {
                        MsgBox.Warn("选择的订单必须为同一车次！");
                        return;
                    }
                    List<UserEntity> userLoadingList = new List<UserEntity>();
                    foreach (var obj in this.listPersonnel.CheckedItems)
                    {
                        if (obj is UserEntity)
                        {
                            UserEntity user = obj as UserEntity;
                            userLoadingList.Add(user);
                        }
                    }

                    List<UserEntity> userTransList = new List<UserEntity>();
                    foreach (var obj in this.listTransPersonal.CheckedItems)
                    {
                        if (obj is UserEntity)
                        {
                            UserEntity user = obj as UserEntity;
                            userTransList.Add(user);
                        }
                    }

                    LoadingHeaderEntity header = new LoadingHeaderEntity()
                    {
                        WarehouseCode = GlobeSettings.LoginedUser.WarehouseCode,
                        LoadingNO = DateTime.Now.ToString("yyyyMMddHHmmssms"),
                        VehicleID = vehicle.ID,
                        UserName = GlobeSettings.LoginedUser.UserName,
                        UpdateDate = DateTime.Now,
                        VehicleNO = vehicle.VehicleNO
                    };
                    List<LoadingDetailEntity> details = new List<LoadingDetailEntity>();
                    foreach (SOHeaderEntity data in headerList)
                    {
                        details.Add(new LoadingDetailEntity()
                        {
                            LoadingNO = header.LoadingNO,
                            BillNO = data.BillNO,
                            InVehicleSort = ConvertUtil.ToInt(data.OrderSort),
                            UpdateDate = DateTime.Now
                        });
                    }

                    List<LoadingUserEntity> loadingUsers = new List<LoadingUserEntity>();
                    foreach (UserEntity item in userLoadingList)
                    {
                        loadingUsers.Add(new LoadingUserEntity()
                        {
                            LoadingNO = header.LoadingNO,
                            UserName = item.UserName,
                            UserCode = item.UserCode,
                            UpdateDate = DateTime.Now,
                            TaskType = "145"
                        });
                    }

                    List<LoadingUserEntity> transUsers = new List<LoadingUserEntity>();
                    foreach (UserEntity item in userTransList)
                    {
                        transUsers.Add(new LoadingUserEntity()
                        {
                            LoadingNO = header.LoadingNO,
                            UserName = item.UserName,
                            UserCode = item.UserCode,
                            UpdateDate = DateTime.Now,
                            TaskType = "148"
                        });
                    }

                    header.Details = details;
                    loadingUsers.AddRange(transUsers);
                    header.Users = loadingUsers;
                    // 存储排序记录
                    CreateLoadingInfo(header);

                    ///生成和分配装车任务
                    foreach (SOHeaderEntity data in headerList)
                    {
                        CreateTask(data.BillID, "148");
                        AutoAssignTask();
                    }
                    //生成叫号信息
                    foreach (LoadingUserEntity user in header.Users)
                    {
                        CreateCalling(BaseCodeConstant.TASK_LOADING, header.VehicleNO, user.UserName, user.UserCode, -1);
                    }
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void textEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string userCode = this.textEdit1.Text.Trim();
                for (int i = 0; i < this.listPersonnel.ItemCount; i++)
                {
                    UserEntity user = this.listPersonnel.GetItem(i) as UserEntity;
                    if (user == null || userCode != user.UserCode)
                        continue;
                    if (this.listPersonnel.GetItemChecked(i))
                        this.listPersonnel.SetItemCheckState(i, CheckState.Unchecked);
                    else
                        this.listPersonnel.SetItemCheckState(i, CheckState.Checked);
                }
                this.textEdit1.Text = string.Empty;
            }
        }
        #endregion

        #region 查询的事件
        private void textEdit1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            try
            {
                if (userVehicle == null || userVehicle.Count == 0)
                    return;
                string text = textEdit1.Text.Trim();
                List<UserEntity> list = null;
                if (!string.IsNullOrEmpty(text))
                {
                    list = userVehicle.FindAll((item) =>
                    {
                        return item.UserCode.IndexOf(text) > -1 || item.UserName.IndexOf(text) > -1;
                    });
                }
                else
                {
                    list = userVehicle;
                }
                list.Sort();
                this.listPersonnel.DataSource = list;
                for (int i = 0; i < list.Count; i++)
                {
                    this.listPersonnel.SetItemChecked(i, list[i].HasChecked);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void textEdit2_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            try
            {
                if (userWeight == null || userWeight.Count == 0)
                    return;
                string text = textEdit2.Text.Trim();
                List<UserEntity> list = null;
                if (!string.IsNullOrEmpty(text))
                {
                    list = userWeight.FindAll((item) =>
                    {
                        return item.UserCode.IndexOf(text) > -1 || item.UserName.IndexOf(text) > -1;
                    });
                }
                else
                {
                    list = userWeight;
                }
                list.Sort();
                this.listTransPersonal.DataSource = list;
                for (int i = 0; i < list.Count; i++)
                {
                    this.listTransPersonal.SetItemChecked(i, list[i].HasChecked);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void listPersonnel_ItemChecking(object sender, DevExpress.XtraEditors.Controls.ItemCheckingEventArgs e)
        {
            UserEntity user = this.listPersonnel.GetItem(e.Index) as UserEntity;
            user.HasChecked = e.NewValue == CheckState.Checked;
        }

        private void listTransPersonal_ItemChecking(object sender, DevExpress.XtraEditors.Controls.ItemCheckingEventArgs e)
        {
            UserEntity user = this.listTransPersonal.GetItem(e.Index) as UserEntity;
            user.HasChecked = e.NewValue == CheckState.Checked;
        }
        #endregion

        /// <summary>
        /// 实现同一客户不同订单置位选中状态
        /// </summary>
        public void SetSameCustomerCheck(SOHeaderEntity header, bool isVisibleAll = false)
        {
            try
            {
                if (header != null)
                {
                    //获取选中的单据，只处理显示出来的，不考虑由于过滤导致的未显示单据
                    bool isCheck = true;

                    #region
                    if (isVisibleAll)
                    {
                        if (header.HasChecked == true)
                            isCheck = true;
                        else
                            isCheck = false;
                    }
                    else
                    {
                        if (header.HasChecked == false)
                            isCheck = true;
                        else
                            isCheck = false;
                    }
                    #endregion

                    for (int i = 0; i < gvHeader.RowCount; i++)
                    {
                        SOHeaderEntity tmp = gvHeader.GetRow(i) as SOHeaderEntity;
                        if (tmp != null && header.CustomerCode == tmp.CustomerCode)
                        {
                            gvHeader.SetRowCellValue(i, "HasChecked", isCheck);
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// 分派装车，查看是否有同一个客户，是否还有其他订单不是当前状态的
        /// </summary>
        /// <param name="cCode">客户编码</param>
        public void IsHaveOtherStatus(string cCode)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("cCode=").Append(cCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_IsHaveOtherStatus);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                }
                #endregion

                #region 正常错误处理

                JsonIsHaveOtherStatus bill = JsonConvert.DeserializeObject<JsonIsHaveOtherStatus>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                }
                #endregion

                #region 提示
                if (bill.result.Length > 0)
                {
                    string msg = "此客户还有" + bill.result.Length.ToString() + "张订单，请尽量排到一辆车。\n\r";//此客户还有一张订单，订单号:XXXXXXXX,目前是XXXX状态，请尽量排到一辆车
                    foreach (JsonIsHaveOtherStatusResult entity in bill.result)
                    {
                        msg += "订单号:" + entity.billNo;
                        msg += ",订单状态：" + entity.billStateName + "。\n\r";
                    }

                    MsgBox.Warn(msg);
                }
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void gvHeader_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
        }
    }
}
