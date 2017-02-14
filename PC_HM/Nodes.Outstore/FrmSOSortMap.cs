using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nodes.Shares;
using System.IO;
using DevExpress.Utils;
using Nodes.Resources;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.UI;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;
using Newtonsoft.Json;

namespace Nodes.Outstore
{
    /// <summary>
    /// 订单排序-百度地图
    /// </summary>
    public partial class FrmSOSortMap : DevExpress.XtraEditors.XtraForm
    {
        #region 自定义事件

        public event Action WebCompletedAction;             //web页面加载完成后调用的事件

        #endregion

        #region 常量
        // 地图 URL(按经纬度排序)
        private static readonly string HTML_URL_POINT = Path.Combine(PathUtil.ApplicationStartupPath, "Map\\amap.htm");//"http://upd.huimin.cn/OrderMap/Index";
        // 地图 URL(按地址排序)
        private const string HTML_URL_ADDRESS = "http://upd.huimin.cn/OrderMap/Index";
        // 接口 javascript 方法
        private const string M_INIT_MAP = "initMap";        // 初始化地图 initMapNew(str)
        private const string M_INIT_MAP_BAIDU = "initMapNew";
        private const string M_SUB_BILL = "submit";         // 提交排序订单
        private const string M_SUB_BILL_BAIDU = "submitBill";
        private const string M_CLEAR_RIGHT = "clearRight";  // 清空右侧序列
        #endregion

        #region 变量
        private List<SOSummaryEntity> _ungroupedBills = null;                           // 未排序的订单
        private bool _webIsCompleted = false;                                           // web 地图是否加载完成
        private List<SortMapSendDataEntity> _list = new List<SortMapSendDataEntity>();  // 统计出来需传送的数据
        //private SOQueryDal soQueryDal = new SOQueryDal();
        //private SODal soDal = new SODal();
        private EMapType _mapType = EMapType.高德地图;

        #endregion

        #region 构造函数

        public FrmSOSortMap()
        {
            InitializeComponent();
            Dictionary<string, object> settings = GlobeSettings.SystemSettings;
            if (settings.ContainsKey("地图类型"))
                this._mapType = (EMapType)ConvertUtil.ToInt(settings["地图类型"]);
        }

        #endregion

        #region 属性
        public string HtmlUrl
        {
            get
            {
                if (this._mapType == EMapType.高德地图)
                    return HTML_URL_POINT;
                else
                    return HTML_URL_ADDRESS;
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


        /// <summary>
        /// 查询未分组的订单
        /// </summary>
        /// <param name="billStates"></param>
        /// <returns></returns>
        public List<SOSummaryEntity> QueryBills(string billStates)
        {
            List<SOSummaryEntity> list = new List<SOSummaryEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billStates=").Append(billStates);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_QueryBillsSortMap);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonQueryBillsSortMap bill = JsonConvert.DeserializeObject<JsonQueryBillsSortMap>(jsonQuery);
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
                foreach (JsonQueryBillsSortMapResult jbr in bill.result)
                {
                    SOSummaryEntity asnEntity = new SOSummaryEntity();
                    #region 0-10
                    asnEntity.Address = jbr.address;
                    asnEntity.Amount = Convert.ToDecimal(jbr.amount);
                    asnEntity.BillID = Convert.ToInt32(jbr.billId);
                    asnEntity.BillNO = jbr.billNo;
                    asnEntity.BillState = jbr.billState;
                    asnEntity.BillStateName = jbr.itemDesc;
                    asnEntity.CustomerName = jbr.cName;
                    asnEntity.Distance = Convert.ToDecimal(jbr.distance);
                    asnEntity.FromWarehouse = jbr.fromWhCode;
                    asnEntity.OrderSort = Convert.ToInt32(jbr.sortOrder);
                    #endregion

                    #region 11-20
                    asnEntity.RouteCode = jbr.rtCode;
                    asnEntity.RouteName = jbr.rtName;
                    asnEntity.TotalCount = Convert.ToInt32(jbr.totalCount);
                    asnEntity.Volume = Convert.ToDecimal(jbr.volume);
                    //asnEntity.WarehouseName = jbr.whName;
                    asnEntity.FromWarehouseName = jbr.whName;
                    asnEntity.Xcoor = Convert.ToDecimal(jbr.xCoor);
                    asnEntity.Ycoor = Convert.ToDecimal(jbr.yCoor);
                    asnEntity.CustomerCode = jbr.cCode;
                    #endregion
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
        /// 加载未排序订单
        /// </summary>
        private void LoadUnGroupBills()
        {
            this._ungroupedBills = QueryBills(BaseCodeConstant.SO_WAIT_GROUP);// soQueryDal.QueryBills(BaseCodeConstant.SO_WAIT_GROUP, 2);
            this.bindingSource1.DataSource = _ungroupedBills;
            this.gridControl1.RefreshDataSource();
        }
        /// <summary>
        /// 刷新 WebBrowser 的页面
        /// </summary>
        private void RefreshWeb()
        {
            //this.webBrowser1.DocumentText = File.ReadAllText(HTML_URL_POINT, Encoding.UTF8);
            this.webBrowser1.Navigate(this.HtmlUrl);

        }
        /// <summary>
        /// 封装数据，导入地图并显示
        /// </summary>
        private void SendData()
        {
            if (this._list == null || this._list.Count == 0)
            {
                this.toolInsertMap.Enabled = !(this.toolUndo.Enabled = this.toolSubmit.Enabled = false);
                return;
            }
            string result = JsonConvert.SerializeObject(this._list);
            // 调用接口(javascript)方法
            this.InvokeScript(this.InitMap, result);
        }
        /// <summary>
        /// 对 WebBrowser 的 InvokeScript 方法进行第二次封装
        /// </summary>
        private object InvokeScript(string methodName, params object[] args)
        {
            return this.webBrowser1.Document.InvokeScript(methodName, args);
        }
        #endregion

        #region Override Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ImageCollection ic = AppResource.LoadToolImages();
            this.barManager1.Images = ic;
            this.toolRefresh.ImageIndex = (int)AppResource.EIcons.refresh;
            this.toolUndo.ImageIndex = this.toolInsertMap.ImageIndex = (int)AppResource.EIcons.back;
            this.toolSubmit.ImageIndex = (int)AppResource.EIcons.save;

            this.LoadUnGroupBills();
            this.RefreshWeb();
        }
        #endregion

        #region 事件

        /// <summary>
        /// 记录 web 页面是否已加载完成
        /// </summary>
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this._webIsCompleted = true;
            //if (!this._webIsCompleted && this.WebCompletedAction != null)
            //{
            //    this._webIsCompleted = true;
            //    this.WebCompletedAction();
            //}
        }
        /// <summary>
        /// 刷新 GridView、webBrowser
        /// </summary>
        private void toolRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.LoadUnGroupBills();
            this.RefreshWeb();
            this._webIsCompleted = false;
        }


        /// <summary>
        /// 存储排序记录
        /// </summary>
        /// <param name="billIDs"></param>
        /// <param name="userName"></param>
        /// <param name="errBillNO"></param>
        /// <returns></returns>
        public bool SaveSortOrders(string billIDs, string userName, out string errBillNO)
        {
            errBillNO = "";
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billIds=").Append(billIDs).Append("&");
                loStr.Append("userName=").Append(userName).Append("&");
                loStr.Append("warehouseType=").Append(EUtilStroreType.WarehouseTypeToInt(GlobeSettings.LoginedUser.WarehouseType)).Append("&");
                loStr.Append("uuid=").Append(Guid.NewGuid().ToString().Replace("-", ""));
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveSortOrders);
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


        private string GetResList<T>(List<T> listobj, List<string> proptylist)
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

            //strb.Append(":[" + string.Join(",", result.ToArray()) + "]");
            //string ret = "\""+ curname + "\"" + strb.ToString();
            //ret = ret.Insert(0, "{");
            //ret = ret.Insert(ret.Length, "}");
            return string.Join(",", result.ToArray());
        }

        private string GetResList<T>(string josnName, List<T> listobj, List<string> proptylist)
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
            string ret = "\"" + josnName + "\"" + strb.ToString();
            //ret = ret.Insert(0, "{");
            //ret = ret.Insert(ret.Length, "}");
            return ret;
        }

        #endregion

        /// <summary>
        /// 存储排序记录
        /// </summary>
        /// <param name="list"></param>
        public bool Insert(List<OrderSortEntity> list)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                List<string> prop = new List<string>() { "BillNo", "Attri1", "VehicleNo", "InVehicleSort", "PiecesQty" };
                string pickPlanEntity = GetResList<OrderSortEntity>("jsonStr", list, prop);
                string jsons = "{" + pickPlanEntity + "," + pickPlanEntity + "}";
                loStr.Append("jsonStr=").Append(jsons);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_InsertSortBill);
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
        /// 补单，检测是否有遗漏的相同客户不同订单
        /// </summary>
        /// <param name="bills"></param>
        public void CheckAgionCustomer(object bills)
        {
            if(bills == null)   return;

            try
            {
                #region
                int findCount = 1;
                foreach (int rowIndex in bills as int[])
                {
                    SOSummaryEntity bill = this.gridView1.GetRow(rowIndex) as SOSummaryEntity;
                    if (bill == null) continue;

                    #region
                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        SOSummaryEntity tmp = gridView1.GetRow(i) as SOSummaryEntity;
                        if (tmp != null && bill.CustomerCode == tmp.CustomerCode && bill.BillNO != tmp.BillNO)
                        {
                            SortMapSendDataEntity data = new SortMapSendDataEntity()
                            {
                                BillID = tmp.BillID,
                                orderId = tmp.BillNO,
                                marketName = tmp.CustomerName,
                                marketAddress = tmp.Address,
                                lat = tmp.Ycoor,
                                lng = tmp.Xcoor,
                                boxNum = tmp.TotalCount
                            };
                            if (!this._list.Contains(data))
                            {
                                gridView1.SelectRow(i);
                                this._list.Add(data);
                            }
                            findCount = 1;

                            #region
                            while (findCount > 0)
                            {
                                SortMapSendDataEntity findData = this._list.Find(u => u.lat == data.lat && u.lng == data.lng && u.BillID != data.BillID);
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
                            #endregion
                        }
                    }
                    #endregion
                }
                #endregion
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 将选择的订单导入到地图中显示
        /// </summary>
        private void toolInsertMap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //Dictionary<string, object> settings = GlobeSettings.SystemSettings;
                //if (settings.ContainsKey("出库方式") && settings["出库方式"].ToString() == "1")
                //{
                //    MsgBox.Warn("出库方式已修改，请直接在【拣货计划】里操作！");
                //    return;
                //}
                if (!this._webIsCompleted)
                {
                    MsgBox.Warn("等待加载地图请稍后...");
                    return;
                }
                int[] bills = this.gridView1.GetSelectedRows();
                if (bills == null || bills.Length == 0)
                {
                    MsgBox.Warn("请选择需要排序的单据！");
                    return;
                }
                using (WaitDialogForm dialog = new WaitDialogForm("正在计算并导入..."))
                {
                    if (!this._webIsCompleted)
                        return;
                    this._list.Clear();
                    int findCount = 1;
                    #region 组装选择的订单信息
                    foreach (int rowIndex in bills)
                    {
                        #region
                        SOSummaryEntity bill = this.gridView1.GetRow(rowIndex) as SOSummaryEntity;
                        if (bill == null) continue;
                        SortMapSendDataEntity data = new SortMapSendDataEntity()
                        {
                            BillID = bill.BillID,
                            orderId = bill.BillNO,
                            marketName = bill.CustomerName,
                            marketAddress = bill.Address,
                            lat = bill.Ycoor,
                            lng = bill.Xcoor,
                            boxNum = bill.TotalCount
                        };
                        if (!this._list.Contains(data))
                            this._list.Add(data);
                        findCount = 1;
                        while (findCount > 0)
                        {
                            SortMapSendDataEntity findData = this._list.Find(u => u.lat == data.lat && u.lng == data.lng && u.BillID != data.BillID);
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
                        #endregion
                    }
                    #endregion

                    #region 补单
                    CheckAgionCustomer(bills);
                    #endregion

                    this.SendData();
                    this.toolInsertMap.Enabled = !(this.toolUndo.Enabled = this.toolSubmit.Enabled = true);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err("错误：" + ex.Message);
            }
        }
        /// <summary>
        /// 提交本组
        /// </summary>
        private void toolSubmit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                object obj = null;
                if (!this._webIsCompleted || (obj = this.InvokeScript(this.Submit)) == null)
                {
                    MsgBox.Warn("未找到已排序订单！");
                    return;
                }
                List<SortMapReceiveDataEntity> list = JsonConvert.DeserializeObject<List<SortMapReceiveDataEntity>>(obj.ToString());
                if (list == null || list.Count == 0)
                {
                    MsgBox.Warn("未找到已排序订单");
                    return;
                }
                if (list.Count == this._list.Count ||
                    MsgBox.AskOK("还有未排序的订单，是否继续提交？") == DialogResult.OK)
                {
                    string billIDs = string.Empty;
                    List<OrderSortEntity> sortList = new List<OrderSortEntity>();
                    string soNO = DateTime.Now.ToString("SOyyyyMMddHHmmssms");
                    foreach (SortMapReceiveDataEntity data in list)
                    {
                        SortMapSendDataEntity sendData = this._list.Find(new Predicate<SortMapSendDataEntity>(
                            (item) =>
                            {
                                return item.orderId == data.orderId;
                            }));
                        if (sendData == null) continue;
                        billIDs += string.Format("{0},", sendData.BillID);
                        sortList.Add(new OrderSortEntity()
                        {
                            VehicleNo = soNO,
                            BillNo = data.orderId,
                            InVehicleSort = ConvertUtil.ToInt(data.paixu),
                            PiecesQty = sendData.boxNum,
                            Attri1 = 1
                        });
                        this._list.Remove(sendData);
                    }
                    //去除最后一个逗号
                    billIDs = billIDs.TrimEnd(',');
                    string errBillNO = string.Empty;
                    // 存储排序记录
                    Insert(sortList);
                    bool recordCount = SaveSortOrders(billIDs, GlobeSettings.LoginedUser.UserName, out errBillNO);
                    if (recordCount)
                    {
                        //this.InvokeScript(M_CLEAR_RIGHT);
                        this.SendData();
                        //this.WebCompletedAction -= this.SendData;
                        //this.WebCompletedAction += this.SendData;
                        this.RefreshWeb();
                        this.LoadUnGroupBills();
                        MsgBox.OK("保存成功。");
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err("错误：" + ex.Message);
            }
        }
        /// <summary>
        /// 撤销本组
        /// </summary>
        private void toolUndo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this._webIsCompleted = false;
            this.RefreshWeb();
            this.toolInsertMap.Enabled = !(this.toolUndo.Enabled = this.toolSubmit.Enabled = false);
        }
        #endregion
    }

    public enum EMapType : uint
    {
        高德地图 = 0,
        百度地图 = 1,
    }
}