using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Nodes.Entities;
//using Nodes.DBHelper;
using System.IO;
using Nodes.Shares;
using System.Collections.Generic;
using Nodes.Utils;
using Nodes.UI;
using System.Linq;
using System.Windows.Forms;
using Print.Dapper;
using System.Data;
using System.Text;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;
using Newtonsoft.Json;


namespace Nodes.Outstore
{
    /// <summary>
    /// 2015-07-17 惠民确认的版本  （销售发货单）
    /// </summary>
    public partial class RepSOFindGoods : DevExpress.XtraReports.UI.XtraReport
    {
        #region 变量

        public readonly string RepFileName = "RepSOFindGoods.repx";
        public short copies = 1;
        private string _module = string.Empty;

        private SOFindGoodsDetailList _dataSource = null;
        #endregion

        #region 构造函数

        public RepSOFindGoods()
        {
            InitializeComponent();
            //string reportFilePath = Path.Combine(GlobeSettings.AppPath, RepFileName);
            //if (File.Exists(reportFilePath)) this.LoadLayout(reportFilePath);

            this.PrintingSystem.StartPrint += new DevExpress.XtraPrinting.PrintDocumentEventHandler(PrintingSystem_StartPrint);
        }

        public RepSOFindGoods(string warehouseNo, string vehicleNo, SOFindGoodsDetailList dataSource, string module)
            : this()
        {
            this.xrWarehouseNo.Text = warehouseNo;
            this.xrVehicleNo.Text = vehicleNo;
            this.xrDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this._dataSource = dataSource;
            this._module = module;
            //this.xrLabel2.Text = dataSource.LongCheStr;
        }

        public RepSOFindGoods(string warehouseNo, string vehicleNo, SOFindGoodsDetailList dataSource, List<JsonGetWuLiuXiangInfoResult> dt, string module)
            : this()
        {
            this.xrWarehouseNo.Text = warehouseNo;
            this.xrVehicleNo.Text = vehicleNo;
            this.xrDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this._dataSource = dataSource;
            this._module = module;
        }

        #endregion

        void PrintingSystem_StartPrint(object sender, DevExpress.XtraPrinting.PrintDocumentEventArgs e)
        {
            e.PrintDocument.PrinterSettings.Collate = true;
            e.PrintDocument.PrinterSettings.Copies = this.copies;
        }

        private void XtraReport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.DataSource = _dataSource;
            this.DataMember = "Details";
        }

        #region 插入日志记录
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="type">日志类型</param>
        /// <param name="creator">当前操作人</param>
        /// <param name="billNo">订单编号</param>
        /// <param name="description">操作描述</param>
        /// <param name="module">模块</param>
        /// <param name="createTime">创建时间</param>
        /// <param name="remark">备注信息</param>
        /// <returns></returns>
        public  bool Insert(ELogType type, string creator, string billNo, string description,
            string module, DateTime createTime, string remark)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("type=").Append(type).Append("&");
                loStr.Append("creator=").Append(creator).Append("&");
                loStr.Append("billNo=").Append(billNo).Append("&");
                loStr.Append("description=").Append(description).Append("&");
                loStr.Append("module=").Append(module).Append("&");
                loStr.Append("remark=").Append(remark);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_Insert);
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
        public  bool Insert(ELogType type, string creator, string billNo, string description,
            string module, string remark)
        {
            return Insert(type, creator, billNo, description, module, DateTime.Now, remark);
        }
        public  bool Insert(ELogType type, string creator, string billNo, string description,
            string module)
        {
            return Insert(type, creator, billNo, description, module, DateTime.Now, null);
        }
        public  bool Insert(ELogType type, string creator, string billNo, string module)
        {
            return Insert(type, creator, billNo, string.Empty, module, DateTime.Now, null);
        }
        #endregion

        private void RepSO_AfterPrint(object sender, EventArgs e)
        {
            //记录打印张数和人
            int pageCount = this.Pages.Count * this.copies;
            //new BillLogDal().SavePrintLog(this._dataSource.VehicleNO, pageCount, "打印装车单", GlobeSettings.LoginedUser.UserName);
            string billIDs = string.Empty;
            foreach (SOFindGoodsDetail item in this._dataSource.Details)
            {
                billIDs += (item.BillID + ",");
            }
            Insert(ELogType.打印, GlobeSettings.LoginedUser.UserName, this.xrVehicleNo.Text, "装车单", this._module + "-RepSOFindGoods", billIDs);
        }
    }

    [Serializable]
    public class SOFindGoodsDetail
    {
        public int BillID { get; set; }
        public string BillNo { get; set; }
        public int LoadingSort { get; set; }
        public int ZhengNum { get; set; }
        public int SanNum { get; set; }
        public string CustomerName{ get; set;}
        public string StoreName
        {
            get
            {
                return string.Format("{1}{0}",
                    this.CustomerName,
                    this.DelayMark != 0 ? "★" : string.Empty);
            }
        }
        public string CustomerAddress { get; set; }
        public string Customer
        {
            get
            {
                return string.Format("{0}",this.CustomerAddress);
                //return string.Format("{2}[{0}] {1}",
                //    this.CustomerName, this.CustomerAddress,
                //    this.DelayMark != 0 ? "*" : string.Empty);
            }
        }

        /// <summary>
        /// 托盘信息
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public  DataTable GetTuoPanInfo(int billID)
        {
            #region
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("CTL_ID", Type.GetType("System.String"));
            tblDatas.Columns.Add("CT_CODE", Type.GetType("System.String"));
            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetTuoPanInfo);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetTuoPanInfo bill = JsonConvert.DeserializeObject<JsonGetTuoPanInfo>(jsonQuery);
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
                foreach (JsonGetTuoPanInfoResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["CTL_ID"] = tm.ctlId;
                    newRow["CT_CODE"] = tm.ctCode;
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

        private string _tuoPanStr = null;
        public string TuoPanStr
        {
            get
            {
                if (this._tuoPanStr == null)
                {
                    DataTable dt = GetTuoPanInfo(this.BillID);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        int index = 0;
                        foreach (DataRow row in dt.Rows)
                        {
                            string tmp = ConvertUtil.ToString(row["CT_CODE"]);
                            //tmp = tmp.Substring(5, tmp.Length - 5);
                            sb.AppendFormat("{0}-{1}",
                                string.IsNullOrEmpty(ConvertUtil.ToString(row["CTL_ID"])) ? "(空)" : row["CTL_ID"],
                                string.IsNullOrEmpty(tmp) ? "(空)" : tmp);
                            index++;
                            if (index < dt.Rows.Count)
                            {
                                sb.Append("\r\n");
                            }

                        }
                        this._tuoPanStr = sb.ToString();
                    }
                }
                return this._tuoPanStr;
            }
        }

        /// <summary>
        /// 物流箱信息
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="wareHouseType"></param>
        /// <returns></returns>
        public DataTable GetWuLiuXiangInfo(int billID, string wareHouseType)
        {
            #region
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("CTL_ID", Type.GetType("System.Int32"));
            tblDatas.Columns.Add("CT_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("LC_CODE", Type.GetType("System.String"));
            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID).Append("&");
                loStr.Append("wareHouseType=").Append(EUtilStroreType.WarehouseTypeToInt(GlobeSettings.LoginedUser.WarehouseType));
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetWuLiuXiangInfo);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetWuLiuXiangInfo bill = JsonConvert.DeserializeObject<JsonGetWuLiuXiangInfo>(jsonQuery);
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
                foreach (JsonGetWuLiuXiangInfoResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["CTL_ID"] = System.Convert.ToInt32(tm.ctlId);
                    newRow["CT_CODE"] = tm.ctCode;
                    newRow["LC_CODE"] = tm.lcCode;
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

        private int _xiangStrNum = 0;
        public int XiangStrNum
        {
            set
            {
                if (_xiangStrNum == 0)
                    _xiangStrNum = value;
            }
            get
            {
                return _xiangStrNum;
            }
        }


        private string InsertStr(string lcCode)
        {
            string temp = lcCode;
            temp = temp.Insert(0, "00000");
            temp = temp.Substring(temp.Length - 5, temp.Length - (temp.Length - 5));
            return temp;
        }

        private string _xiangStr = null;
        public string XiangStr
        {
            set
            {
                if (this._xiangStr == null)
                {
                    DataTable dt = GetWuLiuXiangInfo(this.BillID, GlobeSettings.LoginedUser.WarehouseType.ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        //先进行排序
                        DataView dv = dt.DefaultView;
                        dv.Sort = "CTL_ID Asc";
                        dt = dv.ToTable();

                        StringBuilder sb = new StringBuilder();
                        int index = 0;
                        foreach (DataRow row in dt.Rows)
                        {
                            int ctlID = System.Convert.ToInt32(row["CTL_ID"]);
                            string ctcode = ConvertUtil.ToString(row["CT_CODE"]);
                            string lcCode = ConvertUtil.ToString(row["LC_CODE"]);
                            string idInfo = "0" + ctlID.ToString();
                            sb.AppendFormat("{0}-{1}-{2}{3}",
                                (ctlID == 0) ? "**" : (ctlID.ToString().Length == 8) ?
                                "##" : (ctlID.ToString().Length == 1) ? System.Convert.ToInt32(idInfo).ToString() : ctlID.ToString(),
                                string.IsNullOrEmpty(lcCode) ? "空" : InsertStr(lcCode),
                                string.IsNullOrEmpty(ctcode) ? "空" : InsertStr(ctcode),
                                "      ");
                            index++;
                            if (index < dt.Rows.Count && (index%5 == 0))
                            {
                                sb.Append("\r\n");
                            }

                            #region 000
                            /*int ctlID = System.Convert.ToInt32(row["CTL_ID"]);
                            string ctcode = ConvertUtil.ToString(row["CT_CODE"]);
                            string lcCode = ConvertUtil.ToString(row["LC_CODE"]);
                            string idInfo = "0" + ctlID.ToString();
                            sb.AppendFormat("{0}-{1}-{2}",
                                (ctlID == 0) ? "**" : (ctlID.ToString().Length == 8) ? 
                                "##" : (ctlID.ToString().Length == 1) ? idInfo : ctlID.ToString(),
                                string.IsNullOrEmpty(lcCode) ? "空" : lcCode.Substring(4, lcCode.Length - 4),
                                string.IsNullOrEmpty(ctcode) ? "空" : ctcode.Substring(5,ctcode.Length - 5));
                            index++;
                            if (index < dt.Rows.Count)
                            {
                                sb.Append("\r\n");
                            }*/
                            #endregion
                        }
                        XiangStrNum = dt.Rows.Count;
                        this._xiangStr = sb.ToString();
                    }
                }
            }
            get
            {
                return this._xiangStr;
            }
        }

        private string _DetailLongChe = null;
        public string DetailLongChe
        {
            set
            {
                if (this._DetailLongChe == null)
                {

                    this._DetailLongChe = value;
                }
            }
            get
            {
                return this._DetailLongChe;
            }
        }

        public int DelayMark { get; set; }

        public static SOFindGoodsDetail Convert(Nodes.DBHelper.Print.SOFindGoodsDetail detail)
        {
            Nodes.Outstore.SOFindGoodsDetail copy = null;
            if (detail != null)
            {
                //copy = new Nodes.Outstore.SOFindGoodsDetail();
                copy = new Nodes.Outstore.SOFindGoodsDetail()
                {
                    BillID = detail.BillID,
                    BillNo = detail.BillNo,
                    LoadingSort = detail.LoadingSort,
                    ZhengNum = detail.ZhengNum,
                    SanNum = detail.SanNum,
                    CustomerName = detail.CustomerName,
                    CustomerAddress = detail.CustomerAddress,
                    DelayMark = detail.DelayMark,
                    XiangStr = null,
                };

                //copy.BillID = detail.BillID;
                //copy.BillNo = detail.BillNo;
                //copy.LoadingSort = detail.LoadingSort;
                //copy.ZhengNum = detail.ZhengNum;
                //copy.SanNum = detail.SanNum;
                //copy.CustomerName = detail.CustomerName;
                //copy.CustomerAddress = detail.CustomerAddress;
                //copy.DelayMark = detail.DelayMark;
            }
            return copy;
        }
    }
    [Serializable]
    public class SOFindGoodsDetailList
    {
        private List<SOFindGoodsDetail> _details = null;
        public List<SOFindGoodsDetail> Details
        {
            get
            {
                if (this._details == null)
                    this._details = new List<SOFindGoodsDetail>();
                return this._details;
            }
        }

        private List<JsonRepFindGoods> _LongChe = null;
        public List<JsonRepFindGoods> LongChe
        {
            get
            {
                if (this._LongChe == null)
                    this._LongChe = new List<JsonRepFindGoods>();
                return this._LongChe;
            }
        }

        private string _LongCheStr = null;
        public string LongCheStr
        {
            set
            {
                if (string.IsNullOrEmpty(_LongCheStr))
                    _LongCheStr = value;
            }
            get
            {
                return this._LongCheStr;
            }
        }
    }
}
