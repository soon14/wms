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
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.UI;
using Newtonsoft.Json;
using Nodes.Utils;

namespace Nodes.SystemManage
{
    /// <summary>
    /// 订单排序-百度地图
    /// </summary>
    public partial class FrmSOSortMap : DevExpress.XtraEditors.XtraForm
    {
        #region 常量
        // 地图 URL(按经纬度排序)
        private const string HTML_URL_POINT = "http://upd.huimin.cn/OrderMap/Index";
        // 地图 URL(按地址排序)
        private const string HTML_URL_ADDRESS = "http://upd.huimin.cn/OrderMap/IndexAddress";
        // 接口 javascript 方法
        private const string M_INIT_MAP = "initMapNew";     // 初始化地图 initMapNew(str)
        private const string M_SUB_BILL = "submitBill";     // 提交排序订单
        private const string M_CLEAR_RIGHT = "clearRight";  // 清空右侧序列
        #endregion

        #region 变量
        private List<SOHeaderEntity> _list = null;
        private VehicleEntity _vehicle = null;
        private List<SortMapSendDataEntity> _sendDataList = new List<SortMapSendDataEntity>();
        private SOQueryDal soQueryDal = new SOQueryDal();
        private SODal soDal = new SODal();

        #endregion

        #region 构造函数

        public FrmSOSortMap()
        {
            InitializeComponent();
        }

        public FrmSOSortMap(List<SOHeaderEntity> list, VehicleEntity vehicle)
            : this()
        {
            this._list = list;
            this._vehicle = vehicle;
            this.RefreshWeb();
        }

        #endregion

        #region 方法
        /// <summary>
        /// 刷新 WebBrowser 的页面
        /// </summary>
        private void RefreshWeb()
        {
            this.webBrowser1.Navigate(HTML_URL_POINT);
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

        /// <summary>
        /// 记录 web 页面是否已加载完成
        /// </summary>
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                int findCount = 1;
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
                        boxNum = bill.BoxNum
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
                if (this._sendDataList.Count > 0)
                {
                    string result = JsonConvert.SerializeObject(this._sendDataList);
                    // 调用接口(javascript)方法
                    this.InvokeScript(M_INIT_MAP, result);
                }
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
        /// 生成装车任务
        /// </summary>
        private void btnCreateTask_Click(object sender, EventArgs e)
        {
            try
            {
                object obj = null;
                if ((obj = this.InvokeScript(M_SUB_BILL)) == null)
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
                    foreach (SortMapReceiveDataEntity data in list)
                    {
                        SOHeaderEntity header = this._list.Find(new Predicate<SOHeaderEntity>(
                            (item) =>
                            {
                                return item.BillNO == data.orderId;
                            }));
                        if (header == null) continue;
                        sortList.Add(new OrderSortEntity()
                        {
                            VehicleNo = ConvertUtil.ToString(this._vehicle.ID),
                            BillNo = data.orderId,
                            InVehicleSort = ConvertUtil.ToInt(data.paixu),
                            PiecesQty = data.boxNum,
                            Attri1 = 10
                        });
                    }
                    // 存储排序记录
                    OrderSortDal.Insert(sortList);
                    string selectedBillIDs = StringUtil.JoinBySign<SOHeaderEntity>(this._list, "BillID");
                    string errMsg = TaskDal.CreateLoadingTask(selectedBillIDs, ConvertUtil.ToString(this._vehicle.ID));
                    if (!string.IsNullOrEmpty(errMsg))
                        throw new Exception(errMsg);
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err("错误：" + ex.Message);
            }
        }

        #endregion
    }
}
