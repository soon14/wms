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
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;
using Newtonsoft.Json;
using Nodes.DBHelper;
using Nodes.Net;

namespace Nodes.Outstore
{
    /// <summary>
    /// 待装车订单
    /// </summary>
    public partial class FrmLoadingTMSData : DevExpress.XtraEditors.XtraForm
    {
        #region 变量
        //private VehicleDal _vehicleDal = new VehicleDal();
        //private CallingDal callDal = new CallingDal();
        private HttpContext _httpContext = new HttpContext(XmlBaseClass.ReadResourcesValue("TMS_URL"));
        private List<UserEntity> userVehicle = null;
        private List<UserEntity> userWeight = null;
        #endregion

        #region 构造函数

        public FrmLoadingTMSData()
        {
            InitializeComponent();
        }

        #endregion


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
        /// 装车信息--分派装车-获取组别
        /// </summary>
        /// <param name="locState"></param> 本地状态：-1：所有；0：未装车；1：已装车
        /// <returns></returns>
        public List<TMSDataHeader> Select(int locState)
        {
            List<TMSDataHeader> list = new List<TMSDataHeader>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("locState=").Append(locState);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_Select);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonSelect bill = JsonConvert.DeserializeObject<JsonSelect>(jsonQuery);
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
                foreach (JsonSelectResult jbr in bill.result)
                {
                    TMSDataHeader asnEntity = new TMSDataHeader();
                    asnEntity.Attri1 = jbr.attri1;
                    asnEntity.Attri2 = jbr.attri2;
                    asnEntity.Attri3 = jbr.attri3;
                    asnEntity.Attri4 = jbr.attri4;
                    asnEntity.Attri5 = jbr.attri5;
                    asnEntity.car_type = jbr.type;
                    asnEntity.HeaderID = Convert.ToInt32(jbr.headerId);
                    asnEntity.id = jbr.groupNo;
                    asnEntity.LocalState = Convert.ToInt32(jbr.locState);
                    asnEntity.storehouse = jbr.code;
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

        #region 方法
        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            //UserDal userDal = new UserDal();

            // 获取车辆信息
            this.bindingSource1.DataSource = GetCarAll();
            userVehicle = ListUsersByRoleAndWarehouseCode(
               GlobeSettings.LoginedUser.WarehouseCode, "装车员");
            this.listPersonnel.DataSource = userVehicle;
            this.listPersonnel.DisplayMember = "UserName";

            // 获取称重人员
            userWeight = ListUsersByRoleAndWarehouseCode(
               GlobeSettings.LoginedUser.WarehouseCode, "称重员");
            this.listTransPersonal.DataSource = userWeight;
            this.listTransPersonal.DisplayMember = "UserName";

            // 获取组别
            this.cboGroup.Properties.DataSource = Select(0);
        }

        #endregion

        #region Override Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.LoadData();
        }
        #endregion

        #region 事件

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 装车信息：编辑装车信息-修改TMS表头的本地状态
        /// </summary>
        /// <param name="headerID"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool UpdateLocState(int headerID, int state)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("headerID=").Append(headerID).Append("&");
                loStr.Append("state=").Append(state);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_UpdateLocState);
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
        /// 装车
        /// </summary>
        private void btnCreateTask_Click(object sender, EventArgs e)
        {
            try
            {
                TMSDataHeader header = this.cboGroup.EditValue as TMSDataHeader;
                if (header == null)
                {
                    MsgBox.Warn("请选择组别！");
                    return;
                }

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

                VehicleEntity vehicle = this.searchLookUpEdit1.EditValue as VehicleEntity;
                if (vehicle == null)
                {
                    MsgBox.Warn("请选择车辆！");
                    return;
                }
                List<SOHeaderEntity> soHeaderList = this.gridHeader.DataSource as List<SOHeaderEntity>;
                if (soHeaderList == null || soHeaderList.Count == 0)
                {
                    MsgBox.Warn("当前组别未找到任何订单！");
                    return;
                }

                //装车人员
                List<UserEntity> userLoadingList = new List<UserEntity>();
                foreach (var obj in this.listPersonnel.CheckedItems)
                {
                    if (obj is UserEntity)
                    {
                        UserEntity user = obj as UserEntity;
                        userLoadingList.Add(user);
                    }
                }

                //称重人员
                List<UserEntity> userTransList = new List<UserEntity>();
                foreach (var obj in this.listTransPersonal.CheckedItems)
                {
                    if (obj is UserEntity)
                    {
                        UserEntity user = obj as UserEntity;
                        userTransList.Add(user);
                    }
                }

                LoadingHeaderEntity loadingHeader = new LoadingHeaderEntity()
                {
                    WarehouseCode = GlobeSettings.LoginedUser.WarehouseCode,
                    LoadingNO = DateTime.Now.ToString("yyyyMMddHHmmssms"),
                    VehicleID = vehicle.ID,
                    UserName = GlobeSettings.LoginedUser.UserName,
                    UpdateDate = DateTime.Now
                };

                List<LoadingDetailEntity> details = new List<LoadingDetailEntity>();
                foreach (SOHeaderEntity data in soHeaderList)
                {
                    details.Add(new LoadingDetailEntity()
                    {
                        LoadingNO = loadingHeader.LoadingNO,
                        BillNO = data.BillNO,
                        InVehicleSort = ConvertUtil.ToInt(data.OrderSort),
                        UpdateDate = DateTime.Now,
                        BillID = data.BillID
                    });
                }

                List<LoadingUserEntity> users = new List<LoadingUserEntity>();
                foreach (UserEntity item in userLoadingList)
                {
                    users.Add(new LoadingUserEntity()
                    {
                        LoadingNO = loadingHeader.LoadingNO,
                        UserName = item.UserName,
                        UserCode = item.UserCode,
                        UpdateDate = DateTime.Now
                    });
                }

                //规定排序
                TMSDataHeader tmsDataHeader = this.cboGroup.EditValue as TMSDataHeader;
                if (tmsDataHeader == null)
                    return;
                using (FrmLoadingSortMap frmSOSortMap = new FrmLoadingSortMap(this.SelectedBills, vehicle, userLoadingList, userTransList, tmsDataHeader.id))
                {
                    if (frmSOSortMap.ShowDialog() == DialogResult.OK)
                    {
                        //生成叫号信息
                        foreach (UserEntity user in userLoadingList)
                        {
                            CreateCalling(BaseCodeConstant.TASK_LOADING, vehicle.VehicleNO, user.UserName, user.UserCode, -1);
                        }

                        // 修改TMS表头的本地状态
                        UpdateLocState(header.HeaderID, 1);
                    }
                }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MsgBox.Err("错误：" + ex.Message);
            }
        }

        /// <summary>
        /// 获取所有订单
        /// </summary>
        public List<SOHeaderEntity> SelectedBills
        {
            get
            {
                gvHeader.PostEditor();

                List<SOHeaderEntity> headers = new List<SOHeaderEntity>();
                //获取所有的单据，只处理显示出来的，不考虑由于过滤导致的未显示单据
                for (int i = 0; i < gvHeader.DataRowCount; i++)
                {
                    SOHeaderEntity header = gvHeader.GetRow(i) as SOHeaderEntity;
                    //if (header != null && header.HasChecked)
                    //{
                    headers.Add(header);
                    //}
                }
                return headers;
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

        /// <summary>
        /// 根据分组，查询待装车信息
        /// </summary>
        /// <param name="groupNo"></param>
        /// <returns></returns>
        public List<SOHeaderEntity> Details(string groupNo)
        {
            List<SOHeaderEntity> list = new List<SOHeaderEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("groupNo=").Append(groupNo);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_Details);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonDetails bill = JsonConvert.DeserializeObject<JsonDetails>(jsonQuery);
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
                foreach (JsonDetailsResult jbr in bill.result)
                {
                    SOHeaderEntity asnEntity = new SOHeaderEntity();
                    #region 0-10
                    asnEntity.Address = jbr.address;
                    asnEntity.BillID = Convert.ToInt32(jbr.billId);
                    asnEntity.BillNO = jbr.billNo;
                    asnEntity.BillType = jbr.billType;
                    asnEntity.BillTypeName = jbr.billTypeName;
                    asnEntity.BoxNum = Convert.ToInt32(jbr.boxNum);
                    asnEntity.CaseBoxNum = Convert.ToInt32(jbr.caseBoxNum);
                    asnEntity.Consignee = jbr.contact;
                    asnEntity.ContractNO = jbr.contractNo;
                    asnEntity.CustomerName = jbr.cName;
                    #endregion
                    #region 11-20
                    asnEntity.DelayMark = Convert.ToInt32(jbr.delayMark);
                    asnEntity.FromWarehouse = jbr.fromWhCode;
                    asnEntity.FromWarehouseName = jbr.fromWhName;
                    asnEntity.OrderSort = Convert.ToInt32(jbr.orderSort);
                    asnEntity.OutstoreType = jbr.outStoreType;
                    asnEntity.OutstoreTypeName = jbr.outStoreTypeName;
                    asnEntity.PickZnType = jbr.pickZnType;
                    asnEntity.PickZnTypeName = jbr.pickZnTypeName;
                    asnEntity.Remark = jbr.remark;
                    asnEntity.RowForeColor = Convert.ToInt32(jbr.rowColor);
                    #endregion
                    #region 21-30
                    asnEntity.SalesMan = jbr.salesMan;
                    asnEntity.ShipNO = jbr.shipNo;
                    asnEntity.ShTel = jbr.phone;
                    asnEntity.Status = jbr.billState;
                    asnEntity.StatusName = jbr.statusName;
                    asnEntity.VehicleNO = jbr.vehicleNo;
                    asnEntity.WmsRemark = jbr.wmsRemark;
                    asnEntity.XCoor = Convert.ToDecimal(jbr.xCoor);
                    asnEntity.YCoor = Convert.ToDecimal(jbr.yCoor);
                    asnEntity.CustomerCode = jbr.cCode;
                    //asnEntity.FromWarehouseName = jbr.wh_name;
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

        /// <summary>
        /// 得到车辆类型
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        public string GetVHtype(TMSDataHeader header)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("groupNo=").Append(header.id);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetVHtype);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return null;
                }
                #endregion

                #region 正常错误处理

                JsonGetVHtype bill = JsonConvert.DeserializeObject<JsonGetVHtype>(jsonQuery);
                if (bill == null)
                {
                    //MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return null;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return null;
                }
                #endregion
                if (bill.result != null && bill.result.Length > 0)
                    return bill.result[0].billTypeName;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return null;
        }

        /// <summary>
        /// 选择组别后显示该组别的所有订单信息
        /// </summary>
        private void cboGroup_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                TMSDataHeader header = this.cboGroup.EditValue as TMSDataHeader;
                if (header == null)
                    return;
                this.gridHeader.DataSource = Details(header.id);
                lblVHtype.Text = GetVHtype(header);

            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
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
        /// 根据派单id 32位（组别）删除数据
        /// </summary>
        /// <param name="tmsDataHeader"></param>
        /// <returns></returns>
        public  bool DelleteGp(TMSDataHeader tmsDataHeader)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("groupNo=").Append(tmsDataHeader.id);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_DelleteGp);
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

        private void button1_Click(object sender, EventArgs e)
        {
            TMSDataHeader header = this.cboGroup.EditValue as TMSDataHeader;
            if (header == null)
                return;
            if (MsgBox.AskOK("确定要取消吗?") == DialogResult.No)
            {
                return;
            }
            if (!DelleteGp(header))
            {
                MsgBox.Warn("删除失败，请重新操作！");
            }
            RequestPackage request = new RequestPackage("jscanshu.php");
            request.Method = EHttpMethod.Get.ToString();
            request.Params.Add("id", header.id);

            ResponsePackage response = _httpContext.Request(request);

            if (response.Result == EResponseResult.成功)
            {
                MsgBox.Warn("取消成功");
                LoadData();
                this.gridHeader.DataSource = null;
            }
            else
            {
                MsgBox.Warn("取消失败，请检查网络！");
            }
        }
    }
}