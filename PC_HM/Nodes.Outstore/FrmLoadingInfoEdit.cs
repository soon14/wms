using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using DevExpress.XtraEditors;
using Nodes.Entities;
//using Nodes.DBHelper;
using Nodes.Shares;
using Nodes.UI;
using Nodes.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;
using Newtonsoft.Json;
using Nodes.Net;
using Nodes.DBHelper;

namespace Nodes.Outstore
{
    /// <summary>
    /// 编辑装车信息
    /// </summary>
    public partial class FrmLoadingInfoEdit : XtraForm
    {
        #region 变量

        private LoadingHeaderEntity _header = null;
        private List<UserEntity> UserAll = null;
        private HttpContext _httpContext = new HttpContext(XmlBaseClass.ReadResourcesValue("TMS_URL"));
        List<BaseCodeEntity> moStatusList = null;
        private bool _formIsLoaded = false;
        int flag = 0;
        #endregion

        #region 构造函数

        public FrmLoadingInfoEdit()
        {
            InitializeComponent();
        }

        public FrmLoadingInfoEdit(LoadingHeaderEntity header)
            : this()
        {
            Dictionary<string, object> settings = GlobeSettings.SystemSettings;
            if (!settings.ContainsKey("出库方式") || settings["出库方式"].ToString() == "1")
            {
                flag = 1;
            }
            this._header = header;
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

                //List<SOHeaderEntity> GridHeaders = gridHeader.DataSource as List<SOHeaderEntity>;
                //if (GridHeaders == null)
                //{
                //    GridHeaders = new List<SOHeaderEntity>();
                //}
                //List<SOHeaderEntity> headers = new List<SOHeaderEntity>();

                ////获取选中的单据，只处理显示出来的，不考虑由于过滤导致的未显示单据
                //GridHeaders.ForEach(header =>
                //{
                //    if (header.HasChecked)
                //    {
                //        headers.Add(ConvertUtil.Clone<SOHeaderEntity>(header));

                //    }
                //});
                //if (GridHeaders.Count > 0)
                //{
                //    GridHeaders.RemoveAll(header => header.HasChecked);
                //}
                List<SOHeaderEntity> headers = new List<SOHeaderEntity>();
                //获取选中的单据，只处理显示出来的，不考虑由于过滤导致的未显示单据
                for (int i = 0; i < this.gvHeader.DataRowCount; i++)
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
        /// <summary>
        /// 获取选择的装车订单
        /// </summary>
        public List<LoadingDetailEntity> SelectedLoadingDetails
        {
            get
            {
                gridView3.PostEditor();

                //List<LoadingDetailEntity> GridDetails = gridControl3.DataSource as List<LoadingDetailEntity>;
                //if (GridDetails == null)
                //{
                //    GridDetails = new List<LoadingDetailEntity>();
                //}
                //List<LoadingDetailEntity> Details = new List<LoadingDetailEntity>();

                ////获取选中的单据，只处理显示出来的，不考虑由于过滤导致的未显示单据
                //foreach (LoadingDetailEntity loItem in GridDetails)
                //{
                //    if (loItem.HasChecked)
                //    {
                //        if (loItem.CtState.Split(',').Where(state => state.Equals("893")).Count() > 0)//已上车增加提示
                //        {
                //            if (MsgBox.AskYes("订单" + loItem.BillNO + "已经装车是否移除？") == DialogResult.No)
                //            {
                //                continue;
                //            }
                //        }
                //        Details.Add(loItem);
                //    }
                //}
                //Details.ForEach(detail => GridDetails.Remove(detail));
                //for (int i = 0; i < GridDetails.Count; i++)
                //{
                //    GridDetails[i].InVehicleSort = i + 1;
                //}
                List<LoadingDetailEntity> headers = new List<LoadingDetailEntity>();
                //获取选中的单据，只处理显示出来的，不考虑由于过滤导致的未显示单据
                for (int i = 0; i < this.gridView3.DataRowCount; i++)
                {
                    LoadingDetailEntity header = gridView3.GetRow(i) as LoadingDetailEntity;
                    if (header != null && header.HasChecked)
                    {
                        headers.Add(header);
                    }
                }
                return headers;
            }
        }
        #endregion

        #region 事件
        private void textEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
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
        /// 装车信息：编辑装车信息--移除
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool DeleteDetails(List<LoadingDetailEntity> list,HttpContext _httpContext,int flag)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                #region list 转 json
                List<string> prop = new List<string>() { "BillNO", "LoadingNO" };
                string soHeaderEntity = GetRes<LoadingDetailEntity>(list, prop);
                loStr.Append("jsonStr=").Append(soHeaderEntity);
                #endregion
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_DeleteDetails);
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
                foreach (LoadingDetailEntity item in list)
                {
                    if (flag == 1)
                    {
                        ////matketID = map.ExecuteScalar<string>(sql1, new{ GROUP_NO =item.LoadingNO});
                        //result1 = map.Execute(sql2, new { GROUP_NO = item.LoadingNO, BillNo = item.BillNO });
                        RequestPackage request = new RequestPackage("removeOrderid.php");
                        request.Method = EHttpMethod.Get.ToString();
                        request.Params.Add("orderid", item.BillNO);

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
                return true;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return false;
        }

        /// <summary>
        /// 装车信息：编辑装车信息--移除task
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool RemoveLoadingTask(List<LoadingDetailEntity> list)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                if (list == null)
                    return false;
                string jsons = string.Empty;
                foreach (LoadingDetailEntity tm in list)
                {
                    jsons += tm.BillID;
                    jsons += ",";
                }
                jsons = jsons.Substring(0, jsons.Length - 1);
                loStr.Append("billId=").Append(jsons);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_RemoveLoadingTask);
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
        public bool Insert(ELogType type, string creator, string billNo, string description,
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
        public bool Insert(ELogType type, string creator, string billNo, string description,
            string module, string remark)
        {
            return Insert(type, creator, billNo, description, module, DateTime.Now, remark);
        }
        public bool Insert(ELogType type, string creator, string billNo, string description,
            string module)
        {
            return Insert(type, creator, billNo, description, module, DateTime.Now, null);
        }
        public bool Insert(ELogType type, string creator, string billNo, string module)
        {
            return Insert(type, creator, billNo, string.Empty, module, DateTime.Now, null);
        }
        #endregion

        /// <summary>
        /// 移除
        /// </summary>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                using (WaitDialogForm waitDialog = new WaitDialogForm("正在移除装车信息..."))
                {
                    List<LoadingDetailEntity> details = this.SelectedLoadingDetails;
                    if (details == null || details.Count == 0)
                    {
                        MsgBox.Warn("请选择已列入装车信息的订单!");
                        return;
                    }
                    #region 封装数据
                    //List<SOHeaderEntity> list = gridHeader.DataSource as List<SOHeaderEntity>;
                    //if (list == null)
                    //{
                    //    list = new List<SOHeaderEntity>();
                    //}
                    //foreach (LoadingDetailEntity detail in details)
                    //{
                    //    list.Add(new SOHeaderEntity()
                    //    {
                    //        CreateDate = detail.CreateDate,
                    //        BillTypeName = moStatusList.Where(status => status.ItemValue == detail.BillType).ToList()[0].ItemDesc,
                    //        OutstoreTypeName = moStatusList.Where(status => status.ItemValue == detail.OutStoreType).ToList()[0].ItemDesc,
                    //        StatusName = moStatusList.Where(status => status.ItemValue == detail.BillState).ToList()[0].ItemDesc,
                    //        PickZnTypeName = moStatusList.Where(status => status.ItemValue == detail.PickZnType).ToList()[0].ItemDesc,
                    //        Status = detail.BillState,
                    //        FromWarehouseName = detail.FromWhName,
                    //        Warehouse = detail.FromWhCode,
                    //        BillType = detail.BillType,
                    //        OutstoreType = detail.OutStoreType,
                    //        PickZnType = detail.PickZnType,
                    //        BoxNum = detail.WholeBoxCount,
                    //        CaseBoxNum = detail.BulkBoxCount,
                    //        TrayListStr = detail.TrayListStr,
                    //        CtState = detail.CtState,
                    //        VehicleNO = detail.MapLoadingNO,
                    //        SalesMan = detail.SalesMan,
                    //        ContractNO = detail.ContractNo,
                    //        CustomerName = detail.CustomerName,
                    //        Consignee = detail.CustomerContact,
                    //        ShTel = detail.CustomerPhone,
                    //        Address = detail.CustomerAddress,
                    //        BillID = detail.BillID,
                    //        BillNO = detail.BillNO
                    //    });
                    //}

                    //this.gridControl3.RefreshDataSource();
                    //this.gridHeader.RefreshDataSource();
                    #endregion
                    if (DeleteDetails(details,_httpContext,flag))//wanghongchao
                    {
                        RemoveLoadingTask(details);
                        string logStr = StringUtil.JoinBySign<LoadingDetailEntity>(details, "BillNO");
                        if (Encoding.Default.GetBytes(logStr).Length > 8000)
                        {
                            logStr = logStr.Substring(0, 8000);
                        }
                        Insert(ELogType.装车, GlobeSettings.LoginedUser.UserName, this._header.LoadingNO, logStr, this.Text, "移除");
                        this.LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 装车信息：编辑装车信息--获取指定装车编号中装车顺序最大的值
        /// </summary>
        /// <param name="loadingNo"></param>
        /// <returns></returns>
        public int GetMaxInVehicleSort(string loadingNo)
        {
            try
            {
                #region
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("loadingNo=").Append(loadingNo);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetMaxInVehicleSort);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return -1;
                }
                #endregion

                #region 正常错误处理

                JsonGetMaxInVehicleSort bill = JsonConvert.DeserializeObject<JsonGetMaxInVehicleSort>(jsonQuery);
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
                if (bill.result != null && bill.result.Length > 0)
                    return Convert.ToInt32(bill.result[0].vhSort);
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return -1;
        }

        /// <summary>
        /// 装车信息：编辑装车信息--插入详细
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool InsertDetails(List<LoadingDetailEntity> list,HttpContext _httpContext,int flag)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                #region list 转 json
                List<string> prop = new List<string>() { "LoadingNO", "BillNO", "InVehicleSort", "UpdateDate", "VehicleID" };
                string soHeaderEntity = GetRes<LoadingDetailEntity>(list, prop);
                loStr.Append("jsonStr=").Append(soHeaderEntity);
                #endregion
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_InsertDetails);
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
                foreach (LoadingDetailEntity detail in list)
                {
                if (flag == 1)
                    {
                        RequestPackage request = new RequestPackage("AddOrders.php");
                        request.Method = EHttpMethod.Get.ToString();
                        request.Params.Add("carid", detail.LoadingNO);
                        request.Params.Add("orderno", detail.BillNO);

                        ResponsePackage response = _httpContext.Request(request);

                        if (response.Result == EResponseResult.成功)
                        {
                            string jsonData = Encoding.Default.GetString(response.ResultData as byte[]);
                            if (jsonData == "NULL")
                            {

                            }
                        }
                        else
                        {
                            MsgBox.Warn("回传添加的订单数据失败！");
                        }
                }
                }
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
        /// 添加
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (WaitDialogForm waitDialog = new WaitDialogForm("正在添加装车信息..."))
                {
                    List<SOHeaderEntity> headers = this.SelectedBills;
                    if (headers == null || headers.Count == 0)
                    {
                        MsgBox.Warn("请选择待导入装车信息的订单!");
                        return;
                    }
                    // 封装数据
                    List<LoadingDetailEntity> list = list = new List<LoadingDetailEntity>();
                    int maxSort = GetMaxInVehicleSort(this._header.LoadingNO);
                    //int maxSort = gridView3.RowCount;
                    foreach (SOHeaderEntity header in headers)
                    {
                        maxSort++;
                        list.Add(new LoadingDetailEntity()
                        {
                            /*CreateDate = header.CreateDate,
                            BillType = header.BillType,
                            OutStoreType = header.OutstoreType,
                            PickZnType = header.PickZnType,
                            BillState = header.Status,
                            FromWhCode = header.FromWarehouse,
                            FromWhName = header.FromWarehouseName,*/
                            WholeBoxCount = header.BoxNum,
                            BulkBoxCount = header.CaseBoxNum,
                            //TrayListStr = header.TrayListStr,
                            CtState = header.CtState,
                            MapLoadingNO = header.VehicleNO,
                            SalesMan = header.SalesMan,
                            ContractNo = header.ContractNO,
                            CustomerName = header.CustomerName,
                            CustomerContact = header.Consignee,
                            CustomerPhone = header.ShTel,
                            CustomerAddress = header.Address,
                            BillID = header.BillID,
                            LoadingNO = this._header.LoadingNO,
                            BillNO = header.BillNO,
                            InVehicleSort = maxSort,
                            UpdateDate = DateTime.Now,
                            VehicleID = this._header.VehicleID
                        });
                    }

                    //this.gridControl3.RefreshDataSource();
                    //this.gridHeader.RefreshDataSource();

                    //this.gridControl3.DataSource = LoadingDal.GetLoadingDetails(this._header.LoadingNO);
                    if (InsertDetails(list, _httpContext, flag))
                    {
                        //foreach (LoadingDetailEntity item in list)
                        //{
                        //    CreateTask(item.BillID, "148");
                        //}
                        string logStr = StringUtil.JoinBySign<LoadingDetailEntity>(list, "BillNO");
                        if (Encoding.Default.GetBytes(logStr).Length > 8000)
                        {
                            logStr = logStr.Substring(0, 8000);
                        }
                        Insert(ELogType.装车, GlobeSettings.LoginedUser.UserName, this._header.LoadingNO, logStr, this.Text, "添加");
                        this.LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 装车信息：编辑装车信息-保存编辑-删除车辆信息
        /// </summary>
        /// <param name="voLoadingNo"></param>
        /// <param name="voBillID"></param>
        /// <param name="voUserStr"></param>
        /// <param name="voLoadingDetail"></param>
        /// <param name="voUpdateHeader"></param>
        /// <returns></returns>
        public bool DeleteVehicleInfo(string voLoadingNo, string voBillID, string voUserStr, string voLoadingDetail, string voUpdateHeader)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("voLoadingNo=").Append(voLoadingNo).Append("&");
                loStr.Append("voBillID=").Append(voBillID).Append("&");
                loStr.Append("voUserStr=").Append(voUserStr).Append("&");
                loStr.Append("voLoadingDetail=").Append(voLoadingDetail).Append("&");
                loStr.Append("voUpdateHeader=").Append(voUpdateHeader);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_DeleteVehicleInfo);
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
        /// 保存编辑
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                List<LoadingDetailEntity> loLoadingDetails = gridControl3.DataSource as List<LoadingDetailEntity>;
                if (listPersonnel.CheckedItems.Count <= 0)
                {
                    MsgBox.OK("请选择装车人员。");
                    return;
                }
                //if (loLoadingDetails.Count <= 0)
                //{
                //    MsgBox.OK("请增加单据。");
                //    return;
                //}
                StringBuilder loStr = new StringBuilder();
                UserEntity loUser = null;
                //批量增加装车员
                for (int i = 0; i < listPersonnel.CheckedItems.Count; i++)
                {
                    loUser = this.listPersonnel.CheckedItems[i] as UserEntity;
                    loStr.Append(",('").Append(_header.LoadingNO).Append("',");
                    loStr.Append("'").Append(loUser.UserName).Append("',");
                    loStr.Append("'").Append(loUser.UserCode).Append("',");
                    loStr.Append("'").Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Append("',");
                    loStr.AppendLine("'145')");
                }
                string loUsersStr = loStr.Remove(0, 1).Append(";").ToString();
                loStr = new StringBuilder();
                StringBuilder loUpdateStr = new StringBuilder();
                foreach (LoadingDetailEntity loDetail in loLoadingDetails)
                {
                    loStr.Append(",('").Append(_header.LoadingNO).Append("',");
                    loStr.Append("'").Append(loDetail.BillNO).Append("',");
                    loStr.Append("").Append(loDetail.InVehicleSort).Append(",");
                    loStr.Append("'").Append(loDetail.UpdateDate.ToString("yyyy-MM-dd HH:mm:ss")).Append("'");
                    loStr.AppendLine(")");
                    loUpdateStr.AppendLine("UPDATE WM_SO_HEADER H SET H.SHIP_NO = " + _header.VehicleID + " WHERE H.BILL_NO = '" + loDetail.BillNO + "';");
                }
                string loLoadingDetailStr = null;
                if (loStr.Length > 0)
                    loLoadingDetailStr = loStr.Remove(0, 1).Append(";").ToString();
                string loBillIdStr = string.Join(",", loLoadingDetails.Select(detail => detail.BillID.ToString()).ToArray());
                DeleteVehicleInfo(_header.LoadingNO, loBillIdStr, loUsersStr, loLoadingDetailStr, loUpdateStr.ToString());

                loLoadingDetails.ForEach(detail =>
                {
                    CreateTask(detail.BillID, "148");//创建任务-因为属于存储过程所以单独执行
                });

                string logStr = string.Join(",", loLoadingDetails.Select(detail => detail.BillNO.ToString()).ToArray());
                Insert(ELogType.装车, GlobeSettings.LoginedUser.UserName, this._header.LoadingNO, logStr, this.Text, "编辑装车信息");
                MsgBox.OK("执行成功。");



            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

        }

        /// <summary>
        /// 装车信息：编辑装车信息-添加人员
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool InsertUser(LoadingUserEntity user)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("loadingNO=").Append(user.LoadingNO).Append("&");
                loStr.Append("userName=").Append(user.UserName).Append("&");
                loStr.Append("userCode=").Append(user.UserCode).Append("&");
                loStr.Append("updateDate=").Append(user.UpdateDate);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_InsertUser);
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
        /// 装车信息：编辑装车信息-移除人员
        /// </summary>
        /// <param name="loadingNo"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public bool DeleteUser(string loadingNo, string userCode)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("loadingNO=").Append(loadingNo).Append("&");
                loStr.Append("userCode=").Append(userCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_DeleteUser);
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
        /// 添加移除人员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listPersonnel_ItemChecking(object sender, DevExpress.XtraEditors.Controls.ItemCheckingEventArgs e)
        {
            try
            {
                if (!this._formIsLoaded)
                    return;
                UserEntity user = this.listPersonnel.GetItem(e.Index) as UserEntity;
                if (user == null) return;
                if (e.NewValue == CheckState.Checked) // 添加人员
                {
                    LoadingUserEntity entity = new LoadingUserEntity()
                    {
                        LoadingNO = this._header.LoadingNO,
                        UserCode = user.UserCode,
                        UserName = user.UserName,
                        UpdateDate = DateTime.Now
                    };
                    InsertUser(entity);
                }
                else                                  // 移除人员
                {
                    DeleteUser(this._header.LoadingNO, user.UserCode);
                }
                user.HasChecked = e.NewValue == CheckState.Checked;
            }
            catch
            {
                e.Cancel = true;
            }
        }
        #endregion

        /// <summary>
        /// 显示关联的订单和装车员--订单
        /// </summary>
        /// <param name="loadingNo"></param>
        /// <returns></returns>
        public  List<LoadingDetailEntity> GetLoadingDetails(string loadingNo)
        {
            List<LoadingDetailEntity> list = new List<LoadingDetailEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("loadingNo=").Append(loadingNo).Append("&");
                loStr.Append("warehouseType=").Append(EUtilStroreType.WarehouseTypeToInt(GlobeSettings.LoginedUser.WarehouseType));
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetLoadingDetails);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetLoadingDetails bill = JsonConvert.DeserializeObject<JsonGetLoadingDetails>(jsonQuery);
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
                foreach (JsonGetLoadingDetailsResult jbr in bill.result)
                {
                    LoadingDetailEntity asnEntity = new LoadingDetailEntity();
                    #region 0-10
                    asnEntity.CustomerAddress = jbr.address;
                    asnEntity.BillID = Convert.ToInt32(jbr.billId);
                    asnEntity.BillNO = jbr.billNo;
                    asnEntity.BillState = jbr.billState;
                    asnEntity.BulkBoxCount = Convert.ToInt32(jbr.bulkCount);
                    asnEntity.BulkBoxCount2 = Convert.ToInt32(jbr.bulkCount2);
                    asnEntity.ContractNo = jbr.contractNo;
                    asnEntity.CustomerContact = jbr.contact;
                    asnEntity.CustomerName = jbr.cName;
                    asnEntity.CustomerPhone = jbr.phone;
                    #endregion
                    #region 11-20
                    asnEntity.DelayMark = Convert.ToInt32(jbr.delayMark);
                    asnEntity.ID = Convert.ToInt32(jbr.id);
                    asnEntity.InVehicleSort = Convert.ToInt32(jbr.inVhSort);
                    asnEntity.LoadingNO = jbr.vhTrainNo;
                    asnEntity.MapLoadingNO = jbr.vehicleNo;
                    asnEntity.Location = Convert.ToInt32(jbr.inVehicleSort);
                    asnEntity.SalesMan = jbr.salesMan;
                    asnEntity.SyncState = Convert.ToInt32(jbr.syncState);
                    asnEntity.WMSRemark = jbr.wmsRemark;

                    #region list
                    List<ContainerEntity> clist = new List<ContainerEntity>();
                    if (jbr.trayList != null)
                    {
                        foreach (JsonGetLoadingDetailsResultTrayList tm in jbr.trayList)
                        {
                            ContainerEntity temp = new ContainerEntity();
                            temp.ContainerCode = tm.ctCode;
                            clist.Add(temp);
                        }
                    }
                    #endregion

                    asnEntity.TrayList = clist;
                    asnEntity.WholeBoxCount = Convert.ToInt32(jbr.wholeCount);
                    #endregion
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.updateDate))
                            asnEntity.UpdateDate = Convert.ToDateTime(jbr.updateDate);
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
        public  List<SOHeaderEntity> GetUnLoadingBills()
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

        #region 方法
        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            if (this._header == null)
                return;
            this.gridControl3.DataSource = GetLoadingDetails(this._header.LoadingNO);
            this.gridHeader.DataSource = GetUnLoadingBills();
        }
        private void UpdateUserList()
        {
            if (this.listPersonnel.ItemCount == 0 || this._header.Users == null || this._header.Users.Count == 0)
                return;
            foreach (LoadingUserEntity item in this._header.Users)
            {
                for (int i = 0; i < this.listPersonnel.ItemCount; i++)
                {
                    UserEntity user = this.listPersonnel.GetItem(i) as UserEntity;
                    if (user == null)
                        continue;
                    if (item.UserCode == user.UserCode)
                    {
                        this.listPersonnel.SetItemCheckState(i, CheckState.Checked);
                        break;
                    }
                }
            }
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
        /// 获取活动状态的集合
        /// </summary>
        /// <returns></returns>
        public  List<BaseCodeEntity> GetStatusList()
        {
            List<BaseCodeEntity> list = new List<BaseCodeEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                string jsons = string.Empty;
                string jsonQuery = WebWork.SendRequest(jsons, WebWork.URL_GetStatusList);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetStatusList bill = JsonConvert.DeserializeObject<JsonGetStatusList>(jsonQuery);
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
                foreach (JsonGetStatusListResult jbr in bill.result)
                {
                    BaseCodeEntity asnEntity = new BaseCodeEntity();
                    asnEntity.IsActive = jbr.isActive;
                    asnEntity.GroupCode = jbr.groupCode;
                    asnEntity.ItemDesc = jbr.itemDesc;
                    asnEntity.ItemValue = jbr.itemValue;
                    asnEntity.Remark = jbr.remark;
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


        #region Override Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Text = string.Format("编辑装车信息(装车编号：{0})", this._header.LoadingNO);
            UserAll = ListUsersByRoleAndWarehouseCode(
                GlobeSettings.LoginedUser.WarehouseCode, "装车员");
            this.listPersonnel.DataSource = UserAll;
            this.listPersonnel.DisplayMember = "UserName";
            this.UpdateUserList();
            this.LoadData();
            moStatusList = GetStatusList();
        }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this._formIsLoaded = true;
        }
        #endregion

        #region "选中与复选框"
        private void LoadCheckBoxImage()
        {
            this.gridView3.Images = gvHeader.Images = GridUtil.GetCheckBoxImages();
            colCheck2.ImageIndex = colCheck.ImageIndex = 0;
        }

        private void OnViewMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                CheckOneGridColumn(gvHeader, "HasChecked", MousePosition);
            }
        }

        private void gridView3_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                CheckOneGrid3Column(this.gridView3, "HasChecked", MousePosition);
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
            if (hitInfo.HitTest == GridHitTest.Column && hitInfo.Column.FieldName == checkedField)
            {
                List<SOHeaderEntity> _data = gridHeader.DataSource as List<SOHeaderEntity>;
                if (_data == null) return;

                int currentIndex = hitInfo.Column.ImageIndex;
                bool flag = currentIndex == 0;
                for (int i = 0; i < gvHeader.RowCount; i++)
                {
                    gvHeader.SetRowCellValue(i, "HasChecked", flag);
                }
                hitInfo.Column.ImageIndex = 4 - currentIndex;
            }
        }
        private void CheckOneGrid3Column(GridView view, string checkedField, Point mousePosition)
        {
            Point p = view.GridControl.PointToClient(mousePosition);
            GridHitInfo hitInfo = view.CalcHitInfo(p);
            if (hitInfo.HitTest == GridHitTest.Column && hitInfo.Column.FieldName == checkedField)
            {
                List<LoadingDetailEntity> _data = gridHeader.DataSource as List<LoadingDetailEntity>;
                if (_data == null) return;

                int currentIndex = hitInfo.Column.ImageIndex;
                bool flag = currentIndex == 0;
                for (int i = 0; i < gvHeader.RowCount; i++)
                {
                    gvHeader.SetRowCellValue(i, "HasChecked", flag);
                }
                hitInfo.Column.ImageIndex = 4 - currentIndex;
            }
        }
        #endregion

        private void textEdit1_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            try
            {
                if (UserAll == null || UserAll.Count == 0)
                    return;
                string text = textEdit1.Text.Trim();
                List<UserEntity> list = null;
                if (!string.IsNullOrEmpty(text))
                {
                    list = UserAll.FindAll((item) =>
                    {
                        return item.UserCode.IndexOf(text) > -1 || item.UserName.IndexOf(text) > -1;
                    });
                }
                else
                {
                    list = UserAll;
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
        //上移
        private void btnUp_Click(object sender, EventArgs e)
        {
            if (gridView3.FocusedRowHandle - 1 < 0) return;
            List<LoadingDetailEntity> loDetails = gridControl3.DataSource as List<LoadingDetailEntity>;

            int RowNo = gridView3.FocusedRowHandle;
            LoadingDetailEntity loDetailBottom = gridView3.GetRow(RowNo) as LoadingDetailEntity;

            RowNo = gridView3.FocusedRowHandle - 1;
            LoadingDetailEntity loDetailTop = gridView3.GetRow(RowNo) as LoadingDetailEntity;
            if (loDetailBottom.CtState.Split(',').Where(state => state.Equals("87")).Count() >= 0)
            {
                return;
            }

            loDetailBottom.InVehicleSort = loDetailBottom.InVehicleSort - 1;
            loDetailTop.InVehicleSort = loDetailTop.InVehicleSort + 1;

            gridControl3.DataSource = loDetails.OrderBy(header => header.InVehicleSort).ToList();
            gridView3.MoveBy(RowNo);
        }
        //下移
        private void btnDown_Click(object sender, EventArgs e)
        {
            if (gridView3.FocusedRowHandle + 1 >= gridView3.RowCount) return;
            List<LoadingDetailEntity> loDetails = gridControl3.DataSource as List<LoadingDetailEntity>;

            int RowNo = gridView3.FocusedRowHandle;
            LoadingDetailEntity loDetailTop = gridView3.GetRow(RowNo) as LoadingDetailEntity;

            RowNo = gridView3.FocusedRowHandle + 1;
            LoadingDetailEntity loDetailBottom = gridView3.GetRow(RowNo) as LoadingDetailEntity;
            if (loDetailTop.CtState.Split(',').Where(state => state.Equals("87")).Count() >= 0)
            {
                return;
            }
            loDetailTop.InVehicleSort = loDetailTop.InVehicleSort + 1;
            loDetailBottom.InVehicleSort = loDetailBottom.InVehicleSort - 1;

            gridControl3.DataSource = loDetails.OrderBy(header => header.InVehicleSort).ToList();
            gridView3.MoveBy(RowNo);
        }
    }
}
