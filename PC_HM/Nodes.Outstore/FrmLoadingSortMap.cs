using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Nodes.Shares;
using System.IO;
using DevExpress.Utils;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.UI;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;
using Newtonsoft.Json;
using Nodes.Net;

namespace Nodes.Outstore
{
    /// <summary>
    /// 订单排序-百度地图
    /// </summary>
    public partial class FrmLoadingSortMap : DevExpress.XtraEditors.XtraForm
    {
        #region 常量
        // 地图 URL(按经纬度排序)
        private static readonly string HTML_URL_POINT = Path.Combine(PathUtil.ApplicationStartupPath, "Map\\amap.htm");//"http://upd.huimin.cn/OrderMap/Index";
        // 地图 URL(按地址排序)
        private string HTML_URL_ADDRESS = "http://wms.api.huimin100.cn:8080/wmsCloud/baiduMap/orderMap";
        // 接口 javascript 方法
        private const string M_INIT_MAP = "initMap";     // 初始化地图 initMapNew(str)
        private const string M_INIT_MAP_BAIDU = "initMapNew";
        private const string M_SUB_BILL = "submit";     // 提交排序订单
        private const string M_SUB_BILL_BAIDU = "submitBill";
        private const string M_CLEAR_RIGHT = "clearRight";  // 清空右侧序列
        private HttpContext _httpContext = new HttpContext(XmlBaseClass.ReadResourcesValue("TMS_URL"));
        private bool IsOldMap = false;//是否用原来的地图功能,false表示不使用,true表示使用原来的地图功能
        #endregion

        #region 变量
        private List<SOHeaderEntity> _list = null;
        private List<UserEntity> _loadingUsers = null;
        private List<UserEntity> _transUsers = null;
        private VehicleEntity _vehicle = null;
        private List<SortMapSendDataEntity> _sendDataList = new List<SortMapSendDataEntity>();
        //private SOQueryDal soQueryDal = new SOQueryDal();
        //private SODal soDal = new SODal();
        private string _loadingNo = string.Empty;
        private bool _webIsCompleted = false;                                           // web 地图是否加载完成
        private EMapType _mapType = EMapType.高德地图;

        #endregion

        #region 构造函数

        public FrmLoadingSortMap()
        {
            InitializeComponent();

            Dictionary<string, object> settings = GlobeSettings.SystemSettings;
            if (settings.ContainsKey("地图类型"))
                this._mapType = (EMapType)ConvertUtil.ToInt(settings["地图类型"]);

            //整货仓使用新的地图功能，混合仓使用原来的地图功能；
            if (GlobeSettings.LoginedUser.WarehouseType == EWarehouseType.整货仓)
                IsOldMap = false;
            else
                IsOldMap = true;

            this.lineWay.Visible = !IsOldMap;//是否隐藏控件,新地图不需要隐藏
            this.clearLine.Visible = !IsOldMap;//是否隐藏控件,新地图不需要隐藏

           

            if (!IsOldMap)
            {
                HTML_URL_ADDRESS = "http://wms.api.huimin100.cn:8080/wmsCloud/baiduMap/orderMap";
            }
            else
            {
                HTML_URL_ADDRESS = "http://upd.huimin.cn/OrderMap/Index";
            }

            this.RefreshWeb();
            this._webIsCompleted = false;
        }

        public FrmLoadingSortMap(List<SOHeaderEntity> list, VehicleEntity vehicle)
            : this()
        {
            this._list = list;
            this._vehicle = vehicle;
        }

        public FrmLoadingSortMap(List<SOHeaderEntity> list, VehicleEntity vehicle, List<UserEntity> loadingUsers, List<UserEntity> transUsers)
            : this(list, vehicle)
        {
            this._loadingUsers = loadingUsers;
            this._transUsers = transUsers;
        }
        public FrmLoadingSortMap(List<SOHeaderEntity> list, VehicleEntity vehicle, List<UserEntity> loadingUsers, List<UserEntity> transUsers, string loadingNo)
            : this(list, vehicle, loadingUsers, transUsers)
        {
            this._loadingNo = loadingNo;
        }

        #endregion

        #region 属性
        public string HtmlUrl
        {
            get
            {
                if (!IsOldMap)//表示调用李奇才的方法
                    return HTML_URL_ADDRESS;
                else
                {
                    if(this._mapType == EMapType.高德地图)
                        return HTML_URL_POINT;
                    else
                        return HTML_URL_ADDRESS;
                } 
            }
        }
        public string InitMap
        {
            get
            {
                if (this._mapType == EMapType.高德地图)
                    return M_INIT_MAP;
                else
                    return M_INIT_MAP_BAIDU;
            }
        }
        public string Submit
        {
            get
            {
                if (this._mapType == EMapType.高德地图)
                    return M_SUB_BILL;
                else
                    return M_SUB_BILL_BAIDU;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 刷新 WebBrowser 的页面
        /// </summary>
        private void RefreshWeb()
        {
            this._webIsCompleted = false;
            this.webBrowser1.Navigate(this.HtmlUrl);
        }
        /// <summary>
        /// 对 WebBrowser 的 InvokeScript 方法进行第二次封装
        /// </summary>
        private object InvokeScript(string methodName, params object[] args)
        {
            return this.webBrowser1.Document.InvokeScript(methodName, args);
        }
        #endregion

        #region 事件

        //public bool InitMapScript(string data)
        //{
        //    try
        //    {
        //        #region 请求数据
        //        System.Text.StringBuilder loStr = new System.Text.StringBuilder();
        //        loStr.Append("jsonArray=").Append(data);
        //        string jsonQuery = WebWork.SendRequestMap(loStr.ToString(), HTML_URL_ADDRESS);
        //        //if (string.IsNullOrEmpty(jsonQuery))
        //        {
        //            //MsgBox.Warn(WebWork.RESULT_NULL);
        //            //LogHelper.InfoLog(WebWork.RESULT_NULL);
        //            //return false;
        //        }
        //        #endregion

        //        //#region 正常错误处理

        //        //Sucess bill = JsonConvert.DeserializeObject<Sucess>(jsonQuery);
        //        //if (bill == null)
        //        //{
        //        //    MsgBox.Warn(WebWork.JSON_DATA_NULL);
        //        //    return false;
        //        //}
        //        //if (bill.flag != 0)
        //        //{
        //        //    MsgBox.Warn(bill.error);
        //        //    return false;
        //        //}
        //        //#endregion

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MsgBox.Err(ex.Message);
        //    }

        //    return false;
        //}

        /// <summary>
        /// web 页面加载完成后加载数据
        /// </summary>
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                if (this._webIsCompleted)
                    return;
                int findCount = 1;
                #region 0000
                foreach (SOHeaderEntity bill in this._list)
                {
                    if (bill == null) continue;
                    SortMapSendDataEntity data = new SortMapSendDataEntity()
                    {
                        BillID = bill.BillID,
                        orderId = bill.BillNO,
                        marketName = bill.CustomerName,
                        marketAddress = bill.Address,
                        lat = bill.YCoor,
                        lng = bill.XCoor,
                        boxNum = bill.BoxNum + bill.CaseBoxNum,
                        zhengBoxNum = bill.BoxNum,
                        caseBoxNum = bill.CaseBoxNum
                    };
                    if (!this._sendDataList.Contains(data))
                        this._sendDataList.Add(data);
                    findCount = 1;
                    while (findCount > 0)
                    {
                        SortMapSendDataEntity findData = this._sendDataList.Find(
                            u => u.lat == data.lat && u.lng == data.lng && u.BillID != data.BillID);
                        if (findData == null)
                        {
                            findCount = 0;
                            break;
                        }
                        else
                        {
                            data.lng += 0.0005m;
                        }
                    }
                }
                #endregion

                #region 初始化数据
                if (this._sendDataList.Count > 0)
                {
                    string result = JsonConvert.SerializeObject(this._sendDataList);

                    // 调用接口(javascript)方法
                    if (!IsOldMap)//表示调用李奇才的方法
                        this.InvokeScript(M_INIT_MAP, result); 
                    else
                        this.InvokeScript(this.InitMap, result);
                }
                #endregion
                this._webIsCompleted = true;
            }
            catch (Exception ex)
            {
                MsgBox.Err("错误：" + ex.Message);
            }
        }
        /// <summary>
        /// 刷新
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.RefreshWeb();
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
        /// 根据LoadingNo查询车次信息
        /// </summary>
        /// <param name="LoadingNO"></param>
        /// <returns></returns>
        public  List<SortMapReceiveDataEntity> GetLoadingNOUnSelected(string LoadingNO)
        {
            List<SortMapReceiveDataEntity> list = new List<SortMapReceiveDataEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("LoadingNo=").Append(LoadingNO);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetLoadingNOUnSelected);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetLoadingNOUnSelected bill = JsonConvert.DeserializeObject<JsonGetLoadingNOUnSelected>(jsonQuery);
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
                foreach (JsonGetLoadingNOUnSelectedResult jbr in bill.result)
                {
                    SortMapReceiveDataEntity asnEntity = new SortMapReceiveDataEntity();
                    asnEntity.orderId = jbr.orderId;
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
        /// 生成装车任务
        /// </summary>
        private void btnCreateTask_Click(object sender, EventArgs e)
        {
            try
            {
                using (WaitDialogForm waitDialog = new WaitDialogForm("提示", "正在存储装车信息，请稍后..."))
                {
                    int outType = 0;
                    object obj = null;

                    Dictionary<string, object> settings = GlobeSettings.SystemSettings;
                    if (settings.ContainsKey("出库方式") && settings["出库方式"].ToString() == "1")
                        outType = 1;

                    #region 
                    if (!IsOldMap)//表示调用李奇才的方法
                    {
                        if ((obj = this.InvokeScript("submit")) == null)
                        {
                            MsgBox.Warn("未找到已排序订单！");
                            return;
                        }
                    }
                    else
                    {
                        if ((obj = this.InvokeScript(this.Submit)) == null)
                        {
                            MsgBox.Warn("未找到已排序订单！");
                            return;
                        }
                    }
                    #endregion

                    List<SortMapReceiveDataEntity> list = JsonConvert.DeserializeObject<List<SortMapReceiveDataEntity>>(obj.ToString());
                    if (list == null || list.Count == 0)
                    {
                        MsgBox.Warn("未找到已排序订单");
                        return;
                    }
                    if (list.Count == this._list.Count ||
                        MsgBox.AskOK("还有未排序的订单，是否继续提交？") == DialogResult.OK)
                    {
                        LoadingHeaderEntity header = new LoadingHeaderEntity()
                        {
                            WarehouseCode = GlobeSettings.LoginedUser.WarehouseCode,
                            LoadingNO = outType == 1 ? this._loadingNo : DateTime.Now.ToString("yyyyMMddHHmmssms"),
                            VehicleID = this._vehicle == null ? 0 : this._vehicle.ID,
                            UserName = GlobeSettings.LoginedUser.UserName,
                            UpdateDate = DateTime.Now
                        };
                        List<LoadingDetailEntity> details = new List<LoadingDetailEntity>();
                        foreach (SortMapReceiveDataEntity data in list)
                        {
                            SOHeaderEntity soHeader = this._list.Find(new Predicate<SOHeaderEntity>(
                                (item) =>
                                {
                                    return item.BillNO == data.orderId;
                                }));
                            if (soHeader == null) continue;
                            details.Add(new LoadingDetailEntity()
                            {
                                LoadingNO = header.LoadingNO,
                                BillNO = soHeader.BillNO,
                                InVehicleSort = ConvertUtil.ToInt(data.paixu),
                                UpdateDate = DateTime.Now,
                                BillID = soHeader.BillID
                            });
                        }
                        List<LoadingUserEntity> users = new List<LoadingUserEntity>();
                        if (this._loadingUsers != null && this._loadingUsers.Count > 0)
                        {
                            foreach (UserEntity item in this._loadingUsers)
                            {
                                users.Add(new LoadingUserEntity()
                                {
                                    LoadingNO = header.LoadingNO,
                                    UserName = item.UserName,
                                    UserCode = item.UserCode,
                                    UpdateDate = DateTime.Now,
                                    TaskType = "145"
                                });
                            }
                        }

                        List<LoadingUserEntity> transUsers = new List<LoadingUserEntity>();
                        if (this._transUsers != null && this._transUsers.Count > 0)
                        {
                            foreach (UserEntity item in this._transUsers)
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
                        }

                        header.Details = details;
                        users.AddRange(transUsers);
                        header.Users = users;
                        // 存储排序记录
                        CreateLoadingInfo(header);
                        //返回没有排序的订单
                        if (!string.IsNullOrEmpty(this._loadingNo))
                        {
                            List<SortMapReceiveDataEntity> billsUnSelected = GetLoadingNOUnSelected(this._loadingNo);
                            if (billsUnSelected.Count > 0)
                            {
                                foreach (SortMapReceiveDataEntity item in billsUnSelected)
                                {
                                    RequestPackage request = new RequestPackage("removeOrderid.php");
                                    request.Method = EHttpMethod.Get.ToString();
                                    request.Params.Add("orderid", item.orderId);

                                    ResponsePackage response = _httpContext.Request(request);

                                    if (response.Result == EResponseResult.成功)
                                    {
                                        string jsonData = Encoding.Default.GetString(response.ResultData as byte[]);
                                        if (jsonData == "NULL")
                                        {
                                            MsgBox.Warn("网络环境异常，请检查网络！");
                                        }
                                    }
                                }
                            }
                        }
                        
                       
                        ///装车重新分任务
                        foreach (SortMapReceiveDataEntity data in list)
                        {
                            SOHeaderEntity soHeader = this._list.Find(new Predicate<SOHeaderEntity>(
                                (item) =>
                                {
                                    return item.BillNO == data.orderId;
                                }));
                            CreateTask(soHeader.BillID, "148");
                        }
                        AutoAssignTask();
                        this.DialogResult = DialogResult.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err("错误：" + ex.Message);
            }
        }

        #endregion

        private void lineWay_Click(object sender, EventArgs e)
        {
            this.InvokeScript("searchDriving");//添加途径路线
        }

        private void clearLine_Click(object sender, EventArgs e)
        {
            this.InvokeScript("clearDriving");//清除途径路线
        }
    }
}
