using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nodes.Entities;
using Nodes.Shares;
using Nodes.DBHelper;
using Nodes.Utils;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Nodes.Resources;
using Nodes.UI;
using Nodes.Net;
using System.Collections;
using Newtonsoft.Json;
using Nodes.Common;
using System.Net;
using System.Net.Security;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;

namespace Nodes.Outstore
{
    /// <summary>
    /// 拣货计划
    /// </summary>
    public partial class FrmPickPlans : Form
    {
        #region 变量

        private SODal soDal = new SODal();
        int queryType = 0;
        string billStatus = string.Empty;
        private HttpContext _httpContext = new HttpContext(XmlBaseClass.ReadResourcesValue("TMS_URL"));
        #endregion

        #region 构造函数

        public FrmPickPlans()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void OnFormLoad(object sender, EventArgs e)
        {
            ImageCollection ic = AppResource.LoadToolImages();
            barManager1.Images = ic;
            barButtonItem6.ImageIndex = (int)AppResource.EIcons.refresh;
            barButtonItem1.ImageIndex = (int)AppResource.EIcons.add;
            barButtonItem2.ImageIndex = (int)AppResource.EIcons.dropdown;
            toolDelBill.ImageIndex = (int)AppResource.EIcons.delete;
            barButtonItem3.ImageIndex = (int)AppResource.EIcons.delete;
            barButtonItem4.ImageIndex = (int)AppResource.EIcons.search;
            barButtonItem5.ImageIndex = (int)AppResource.EIcons.search;
            barButtonItem9.ImageIndex = (int)AppResource.EIcons.search;
            barButtonItem7.ImageIndex = (int)AppResource.EIcons.print;
            barButtonItem8.ImageIndex = (int)AppResource.EIcons.download;

            LoadCheckBoxImage();

            queryNotCreatePickPlanBill();
        }
        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DoClickEvent(ConvertUtil.ToString(e.Item.Tag));
        }
        void frmStrategy_dataSourceChanged(object sender, EventArgs e)
        {
            bindingSource1.ResetCurrentItem();
        }
        void frmResult_DataChanged(object sender, EventArgs e)
        {
            ReloadCheckedBillState();
        }

        /// <summary>
        /// 实现同一客户不同订单置位选中状态
        /// </summary>
        public void SetSameCustomerCheck(SOHeaderEntity header,bool isVisibleAll = false)
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

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {       
            ShowFocusDetail();
            ShowFocusedPickPlan();
        }
        private void gvHeader_RowCellStyle(object sender, RowCellStyleEventArgs e)
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
            SOHeaderEntity selectedHeader = SelectedHeader;
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
                List<SOHeaderEntity> _data = bindingSource1.DataSource as List<SOHeaderEntity>;
                if (_data == null) return;

                int currentIndex = hitInfo.Column.ImageIndex;
                bool flag = currentIndex == 0;
                for (int i = 0; i < gvHeader.RowCount; i++)
                {
                    if (gvHeader.IsRowVisible(i) == RowVisibleState.Visible)
                    {
                        gvHeader.SetRowCellValue(i, "HasChecked", flag);

                        SOHeaderEntity tmp = gvHeader.GetRow(i) as SOHeaderEntity;
                        if (tmp != null)
                            SetSameCustomerCheck(tmp, true);
                    }
                }
                //_data.ForEach(d => d.HasChecked = flag);
                hitInfo.Column.ImageIndex = 4 - currentIndex;
            }
            else
            {
                #region
                SOHeaderEntity selectedHeader = gvHeader.GetFocusedRow() as SOHeaderEntity;
                if (selectedHeader != null)

                    //实现同一客户不同订单置位选中状态
                    SetSameCustomerCheck(selectedHeader);
                #endregion
            }
            
        }
        #endregion

        #region 方法
        private void RequestPlanBills()
        {
            #region 000
            // 查看系统设置，是否走接口流程
            Dictionary<string, object> settings = GlobeSettings.SystemSettings;
            if (!settings.ContainsKey("出库方式") || settings["出库方式"].ToString() != "1")
            {
                MsgBox.Warn("当前系统设置下，功能不可用！");
                return;
            }
            #endregion

            string jsonData = string.Empty;
            try
            {
                string car_type = string.Empty;
                using (FrmChooseBaseCode frmChooseCarType = new FrmChooseBaseCode("131"))
                {
                    if (frmChooseCarType.ShowDialog() == DialogResult.OK)
                    {
                        car_type = frmChooseCarType.SelectedBaseCode.ItemValue;
                    }
                }
                if (string.IsNullOrEmpty(car_type))
                {
                    MsgBox.Warn("请选择车辆类型后再操作！");
                    return;
                }
                #region 此段代码作用：忽略证书验证

                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                ServicePointManager.ServerCertificateValidationCallback = new
                RemoteCertificateValidationCallback
                (
                   delegate { return true; }
                );

                #endregion

                #region 访问远程接口
                using (WaitDialogForm waitDialog = new WaitDialogForm("正在访问远程接口"))
                {
                    RequestPackage request = new RequestPackage("get_orders.php");
                    request.Method = EHttpMethod.Get.ToString();
                    request.Params.Add("storeid", GlobeSettings.LoginedUser.WarehouseCode);
                    request.Params.Add("car_type", car_type);
                    ResponsePackage response = this._httpContext.Request(request);
                    //ResponsePackage response = new ResponsePackage();
                    //response.Result = EResponseResult.成功;
                    //response.ResultData = @"{'id':'C10201603231049533085','car_type':'10','start_time':'2016-03-23 18:49:53','storehouse':'104','order_list':{'143909':{'x':'116.504446','y':'40.068661','order_info':{'BM201022688512330848030':{'sort':0,'zhengnum':0,'sannum':0},'BC101100396512332899456':{'sort':0,'zhengnum':0,'sannum':0}}},'163987':{'x':'116.395645','y':'39.929986','order_info':{'BM221075862512337029287':{'sort':0,'zhengnum':0,'sannum':0}}}}}";
                    if (response.Result == EResponseResult.成功)
                    {
                        #region 处理解析数据
                        jsonData = Encoding.Default.GetString(response.ResultData as byte[]);
                        if (jsonData == "NULL")
                        {
                            MsgBox.Warn("没有任何数据");
                            return;
                        }
                        // 将返回的数据添加到新表中
                        TMSDataHeader header = JsonConvert.DeserializeObject<TMSDataHeader>(jsonData);
                        if (header == null)
                        {
                            MsgBox.Err("获取数据失败！");
                            return;
                        }
                        if (header.order_list == null || header.order_list.Count == 0)
                        {
                            MsgBox.Err("返回数据不包含订单信息！");
                            return;
                        }
                        #endregion
                        int result = TMSDataDAL.Insert(header);
                        if (result > 0)
                        {
                            #region 修改订单状态
                            // 修改订单状态
                            // Update By 万伟超
                            foreach (string marketKey in header.order_list.Keys)
                            {
                                TMSDataMarket market = header.order_list[marketKey];

                                string billIds = StringUtil.JoinBySign<TMSDataDetail>(market.order_info.Values, "orderid");
                                //soDal.UpdateBillsState(billIds, "61", "60");
                            }
                            this.Reload();
                            // 计算共有多少单
                            int totalOrder = 0;
                            foreach (string key in header.order_list.Keys)
                            {
                                TMSDataMarket dataMarket = header.order_list[key];
                                totalOrder += dataMarket.order_info.Count;
                            }
                            MsgBox.OK(string.Format("获取数据成功！组别编号：{0}  共 {1} 单。",
                                header.id, totalOrder));
                            #endregion
                        }
                        else
                        {
                            MsgBox.Err("存储数据失败，请将以下文本复制提供给技术人员：\r\n\r\n" + jsonData);
                        }
                    }
                    else
                    {
                        MsgBox.Err(response.ErrMessage);
                    }
                }
                #endregion
            }
            catch (JsonException ex)
            {
                MsgBox.Err(string.Format(
                    "数据格式异常，转换出错！\r\n异常信息：{0}\r\nTMS返回数据：{1}",
                    ex.Message, jsonData));
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
        /// <summary>
        /// 查询未拣配计算单据信息
        /// </summary>
        private void queryNotCreatePickPlanBill()
        {
            BindQueryResult(1, BaseCodeConstant.SO_WAIT_PICK);
        }
        /// <summary>
        /// 查询未开始拣货单据信息
        /// </summary>
        private void queryNotStartPickBill()
        {
            BindQueryResult(2, BaseCodeConstant.SO_WAIT_PICK + "," +
                BaseCodeConstant.SO_WAIT_TASK + "," + BaseCodeConstant.SO_WAIT_PICKING);
        }

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
                    if (!string.IsNullOrEmpty(jbr.rowColor))
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
                        MsgBox.Warn(msg.Message + msg.StackTrace + jbr.closeDate + "aa" + jbr.billNo);
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
                        MsgBox.Warn(msg.Message + msg.StackTrace + jbr.createDate + "bb" + jbr.billNo);
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
                this.billStatus = billStatus;
                Dictionary<string, object> settings = GlobeSettings.SystemSettings;
                List<SOHeaderEntity> asnHeaderEntitys = QueryBillsByStatus(
                    billStatus, !settings.ContainsKey("出库方式") ? 0 : ConvertUtil.ToInt(settings["出库方式"]));
                bindingSource1.DataSource = asnHeaderEntitys;
                ShowFocusedPickPlan();
                ShowFocusDetail();

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
                lblQueryCondition.Text = "过滤条件: 未拣配计算的单据。";
            else
                lblQueryCondition.Text = "过滤条件: 尚未开始拣货的单据。";
        }
        private void Reload()
        {
            BindQueryResult(this.queryType, this.billStatus);
        }
        private void DoClickEvent(string itemTag)
        {
            switch (itemTag)
            {
                case "刷新":
                    Reload();
                    break;
                case "未拣配计算":
                    queryNotCreatePickPlanBill();
                    break;
                case "未开始拣货":
                    queryNotStartPickBill();
                    break;
                case "生成拣货计划":
                    DoCreatePickPlan();
                    break;
                case "修改拣货方式":
                    DoPickStrategy();
                    break;
                case "删除拣货计划":
                    DoDeleteSelectPickPlan();
                    break;
                case "删除选中单据":
                    DoDeleteSelectBill();
                    break;
                case "排车":
                    RequestPlanBills();
                    break;
                case "全整订单":
                    QueryAllCaseBill();
                    break;
                default:
                    MsgBox.Warn("未找到为该按钮设置的事件：" + itemTag);
                    break;
            }
        }
        private void DoPickStrategy()
        {
            SOHeaderEntity selectedHeader = SelectedHeader;
            if (selectedHeader == null)
            {
                MsgBox.Warn("请选中要设置的单据行。");
            }
            else if (selectedHeader.Status != BaseCodeConstant.SO_WAIT_PICK)
            {
                MsgBox.Warn("只有等待拣配计算的单据才能修改拣货方式。");
            }
            else
            {
                FrmPickStrategy frmStrategy = new FrmPickStrategy(selectedHeader);
                frmStrategy.dataSourceChanged += new EventHandler(frmStrategy_dataSourceChanged);
                frmStrategy.ShowDialog();
            }
        }
        private List<SOHeaderEntity> SelectedBills()
        {
            gvHeader.PostEditor();

            List<SOHeaderEntity> headers = new List<SOHeaderEntity>();
            //获取选中的单据，只处理显示出来的，不考虑由于过滤导致的未显示单据
            for (int i = 0; i < gvHeader.RowCount; i++)
            {
                if (gvHeader.IsRowVisible(i) == RowVisibleState.Visible)
                {
                    SOHeaderEntity header = gvHeader.GetRow(i) as SOHeaderEntity;
                    if (header.HasChecked)
                    {
                        headers.Add(header);
                    }
                }
            }

            return headers;
        }
        private string SelectedBillIDs(List<SOHeaderEntity> headers)
        {
            string selectedBillIDs = string.Empty;
            foreach (SOHeaderEntity header in headers)
                selectedBillIDs += string.Format("{0},", header.BillID);
            StringUtil.JoinBySign<SOHeaderEntity>(headers, "BillID", ",");
            //去除最后一个逗号
            return selectedBillIDs.TrimEnd(',');
        }
        private string SelectedBillNOs(List<SOHeaderEntity> headers)
        {
            string selectedBillIDs = string.Empty;
            foreach (SOHeaderEntity header in headers)
                selectedBillIDs += string.Format("{0},", header.BillNO);

            //去除最后一个逗号
            return selectedBillIDs.TrimEnd(',');
        }

        /// <summary>
        /// 拣货计划 ： 判断临时表里面是否有生成的记录
        /// </summary>
        /// <param name="billIDs"></param>
        /// <returns></returns>
        public bool JudgeIsNext(string billIDs)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billIds=").Append(billIDs);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_JudgeIsNext);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return false;
                }
                #endregion

                #region 正常错误处理

                JsonJudgeIsNext bill = JsonConvert.DeserializeObject<JsonJudgeIsNext>(jsonQuery);
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
                if (bill.result != null && bill.result.Length > 0)
                    return bill.result[0].flag;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return false;
        }

        /// <summary>
        /// 拣货计划（删除拣货计划临时数据 ）
        /// </summary>
        /// <returns></returns>
        public bool DeletePickTemp()
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_DeletePickTemp);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return false;
                }
                #endregion

                #region 正常错误处理

                JsonJudgeIsNext bill = JsonConvert.DeserializeObject<JsonJudgeIsNext>(jsonQuery);
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
                if (bill.result != null && bill.result.Length > 0)
                    return bill.result[0].flag;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return false;
        }

        /// <summary>
        /// 拣货计划 ：生成拣货计划
        /// </summary>
        /// <param name="billIDs"></param>
        /// <param name="tempID"></param>
        /// <param name="errMsg"></param>
        public JsonCreatePickPlanResult CreatePickPlan(string billIDs, out string tempID, out string errMsg)
        {
            JsonCreatePickPlanResult tm = new JsonCreatePickPlanResult();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billIds=").Append(billIDs).Append("&");
                loStr.Append("houseType=").Append(EUtilStroreType.WarehouseTypeToInt(GlobeSettings.LoginedUser.WarehouseType));
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_CreatePickPlan);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    tempID = string.Empty;
                    errMsg = string.Empty;
                    return tm;
                }
                #endregion

                #region 正常错误处理

                JsonCreatePickPlan bill = JsonConvert.DeserializeObject<JsonCreatePickPlan>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    tempID = string.Empty;
                    errMsg = string.Empty;
                    return tm;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    tempID = string.Empty;
                    errMsg = string.Empty;
                    return tm;
                }
                #endregion
                List<JsonCreatePickPlanResult> list = new List<JsonCreatePickPlanResult>();
                #region 赋值数据
                foreach (JsonCreatePickPlanResult jbr in bill.result)
                {
                    list.Add(jbr);
                }
                if (list.Count > 0)
                {
                    tempID = list[0].tempId;
                    errMsg = list[0].errMsg;
                    return list[0];
                }
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            tempID = string.Empty;
            errMsg = string.Empty;
            return tm;
        }

        private void DoCreatePickPlan()
        {
            List<SOHeaderEntity> headers = SelectedBills();
            if (headers.Count == 0)
            {
                MsgBox.Warn("请勾选要执行拣配计算的单据行。");
                return;
            }

            string selectedBillIDs = SelectedBillIDs(headers);
            try
            {
                string tempID = "", errMsg = "";

                //判断临时表里面是否有生成的记录
                if (JudgeIsNext(selectedBillIDs))
                {
                    using (WaitDialogForm frm = new WaitDialogForm("正在生成拣货计划", "请稍等"))
                    {
                        CreatePickPlan(selectedBillIDs, out tempID, out errMsg);
                    }
                }
                else
                {
                    MsgBox.Warn(string.Format("该批订单含有已经生成拣货计划订单，请刷新列表，重新选择！"));
                    Reload();
                    return;
                }

                if (!string.IsNullOrEmpty(errMsg))
                {
                    DeletePickTemp();
                    MsgBox.Warn(errMsg);
                }
                else
                {
                    FrmTempPickPlan frmResult = new FrmTempPickPlan(tempID, errMsg, selectedBillIDs);
                    frmResult.DataChanged += new EventHandler(frmResult_DataChanged);
                    frmResult.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
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

        void ReloadCheckedBillState()
        {
            List<SOHeaderEntity> headers = SelectedBills();
            foreach (SOHeaderEntity header in headers)
            {
                SOHeaderEntity _header = GetBillStatus(header.BillID);
                header.Status = _header.Status;
                header.StatusName = _header.StatusName;
            }

            bindingSource1.ResetBindings(false);
        }
        private void DoDeleteSelectBill()
        {
            //#region 000
            //int result = 0;
            //List<SOHeaderEntity> selectedHeaders = SelectedBills();
            //if (selectedHeaders.Count == 0)
            //{
            //    MsgBox.Warn("请选中要删除的单据行。");
            //    return;
            //}

            //if (MsgBox.AskOK(string.Format("共选中{0}个单据“{1}”，确定要删除吗？会同时删除拣货计划，拣货策略等信息。", selectedHeaders.Count, SelectedBillNOs(selectedHeaders))) != DialogResult.OK)
            //    return;

            //try
            //{
            //    foreach (SOHeaderEntity header in selectedHeaders)
            //    {
            //        result = soDal.DeleteBill(header.BillID, GlobeSettings.LoginedUser.UserName);

            //        if (result == 0)
            //        {
            //            bindingSource1.Remove(header);
            //        }
            //        else if (result == 1)
            //            MsgBox.Warn(string.Format("单据“{0}”未找到。", header.BillNO));
            //        else
            //            MsgBox.Warn(string.Format("单据“{0}”状态不对，只有已拣配计算并且未开始拣货的单据才可以删除，请刷新数据后重试。", header.BillNO));
            //    }

            //    ShowFocusDetail();
            //    ShowFocusedPickPlan();
            //}
            //catch (Exception ex)
            //{
            //    MsgBox.Err(ex.Message);
            //}
            //#endregion
        }

        /// <summary>
        /// 拣货计划（删除已有拣配计算的结果，必须是未开始拣货）
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int DeletePickPlan(int billID, string userName)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID).Append("&");
                loStr.Append("WarehouseType=").Append(EUtilStroreType.WarehouseTypeToInt(GlobeSettings.LoginedUser.WarehouseType)).Append("&");
                loStr.Append("userName=").Append(userName);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_DeletePickPlan);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return -7;
                }
                #endregion

                #region 正常错误处理

                JsonSaveStrategy bill = JsonConvert.DeserializeObject<JsonSaveStrategy>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return -7;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return -7;
                }
                #endregion
                if (bill.result != null && bill.result.Length > 0)
                    return Convert.ToInt32(bill.result[0].vResult);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return -7;
        }

        private void DoDeleteSelectPickPlan()
        {
            int result = 0;
            List<SOHeaderEntity> selectedHeaders = SelectedBills();
            if (selectedHeaders.Count == 0)
            {
                MsgBox.Warn("请选中要删除的单据行。");
                return;
            }

            if (MsgBox.AskOK(string.Format("共选中{0}个单据“{1}”，确定要删除拣配计算结果吗？", selectedHeaders.Count, SelectedBillNOs(selectedHeaders))) != DialogResult.OK)
                return;

            try
            {
                foreach (SOHeaderEntity header in selectedHeaders)
                {
                    result = DeletePickPlan(header.BillID, GlobeSettings.LoginedUser.UserName);

                    if (result == -1)
                        MsgBox.Warn(string.Format("单据“{0}”未找到。", header.BillNO));
                    else if (result == -2)
                        MsgBox.Warn(string.Format("单据“{0}”状态不对，只有尚未开始拣货的单据才可以删除，请刷新数据后重试。", header.BillNO));
                    else if (result == -3)
                        MsgBox.Warn(string.Format("单据“{0}”状态不对，只有等待任务分派的单据才可以删除，请刷新数据后重试。", header.BillNO));
                    else
                    {
                        //可以删除
                    }
                }

                ReloadCheckedBillState();
                ShowFocusedPickPlan();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
        SOHeaderEntity SelectedHeader
        {
            get
            {
                if (gvHeader.FocusedRowHandle < 0)
                    return null;
                else
                    return gvHeader.GetFocusedRow() as SOHeaderEntity;
            }
        }
        /// <summary>
        /// 出库单管理，查询出库单明细
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public List<SODetailEntity> GetDetails(int billID)
        {
            List<SODetailEntity> list = new List<SODetailEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billID=").Append(billID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetDetails);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetDetails bill = JsonConvert.DeserializeObject<JsonGetDetails>(jsonQuery);
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
                foreach (JsonGetDetailsResult jbr in bill.result)
                {
                    SODetailEntity asnEntity = new SODetailEntity();
                    #region 0-10
                    asnEntity.BatchNO = jbr.batchNo;
                    asnEntity.BillID = Convert.ToInt32(jbr.billId);
                    asnEntity.CombMaterial = jbr.comMaterial;
                    asnEntity.DetailID = Convert.ToInt32(jbr.detailId);
                    asnEntity.DueDate = jbr.dueDate;
                    asnEntity.IsCase = Convert.ToInt32(jbr.isCase);
                    asnEntity.MaterialCode = jbr.skuCode;
                    asnEntity.MaterialName = jbr.skuName;
                    asnEntity.PickQty = Convert.ToDecimal(jbr.pickQty);
                    asnEntity.Price1 = Convert.ToDecimal(jbr.price);
                    #endregion
                    #region 11-20
                    asnEntity.Qty = Convert.ToDecimal(jbr.qty);
                    asnEntity.Remark = jbr.remark;
                    asnEntity.RowNO = Convert.ToInt32(jbr.rowNo);
                    ///jbr.rowNo1;
                    asnEntity.SkuBarcode = jbr.skuBarCode;
                    asnEntity.Spec = jbr.spec;
                    asnEntity.SuitNum = Convert.ToDecimal(jbr.suitNum);
                    asnEntity.UnitCode = jbr.umCode;
                    asnEntity.UnitName = jbr.umName;
                    #endregion
                    try
                    {
                        //if (!string.IsNullOrEmpty(jbr.closeDate))
                        //    asnEntity.CloseDate = Convert.ToDateTime(jbr.closeDate);
                    }
                    catch (Exception msg)
                    {
                        //MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("PSoManage+QueryBills", msg);
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

        private void ShowFocusDetail()
        {
            SOHeaderEntity selectedHeader = SelectedHeader;
            if (selectedHeader == null)
            {
                gridDetails.DataSource = null;
                gvDetails.ViewCaption = "未选择单据";
            }
            else
            {
                gridDetails.DataSource = GetDetails(selectedHeader.BillID);
                gvDetails.ViewCaption = string.Format("单据号: {0}", selectedHeader.BillNO);
            }
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
                        MsgBox.Warn(msg.Message + msg.StackTrace + jbr.createDate);
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
                        MsgBox.Warn(msg.Message + msg.StackTrace + jbr.createDate);
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

        void ShowFocusedPickPlan()
        {
            SOHeaderEntity selectedHeader = SelectedHeader;
            if (selectedHeader == null)
            {
                gridPlans.DataSource = null;
                gvPlans.ViewCaption = "未选择单据";
            }
            else
            {
                gridPlans.DataSource = GetPickPlan(selectedHeader.BillID);
                gvPlans.ViewCaption = string.Format("单据号: {0}", selectedHeader.BillNO);
            }
        }

        /// <summary>
        /// 全整订单
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public List<SOHeaderEntity> QueryAllCaseBill(int setting)
        {
            List<SOHeaderEntity> list = new List<SOHeaderEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("setting=").Append(setting);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_QueryAllCaseBill);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion 

                #region 正常错误处理

                JsonQueryAllCaseBill bill = JsonConvert.DeserializeObject<JsonQueryAllCaseBill>(jsonQuery);
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
                foreach (JsonQueryAllCaseBillResult jbr in bill.result)
                {
                    SOHeaderEntity asnEntity = new SOHeaderEntity();
                    #region 0-10
                    asnEntity.Address = jbr.address; 
                    asnEntity.BillID = Convert.ToInt32(jbr.billId);
                    asnEntity.BillNO = jbr.billNo;
                    asnEntity.Status = jbr.billState;
                    asnEntity.BillType = jbr.billType;
                    asnEntity.BillTypeName = jbr.billTypeName;
                    asnEntity.CaseStr = jbr.caseStr;
                    asnEntity.CustomerCode = jbr.cCode;
                    asnEntity.CustomerName = jbr.cName;
                    asnEntity.Consignee = jbr.contact;
                     
                    #endregion
                    #region 11-20
                    asnEntity.DelayMark = Convert.ToInt32(jbr.delayMark);
                    asnEntity.FromWarehouse = jbr.fromWhCode;
                    asnEntity.FromWarehouseName = jbr.fromWhName;
                    asnEntity.OutstoreType = jbr.outStoreType;
                    asnEntity.OutstoreTypeName = jbr.outStoreTypeName;
                    asnEntity.ShTel = jbr.phone;
                    asnEntity.PickZnType = jbr.pickZnType;
                    asnEntity.PickZnTypeName = jbr.pickZnTypeName;
                    asnEntity.Remark = jbr.remark;
                    asnEntity.RowForeColor = Convert.ToInt32(jbr.rowColor);
                    #endregion

                    #region 21-28
                    asnEntity.SalesMan = jbr.salesMan;
                    asnEntity.ShipNO = jbr.shipNo;
                    asnEntity.StatusName = jbr.statusName;
                    asnEntity.VehicleNO = jbr.vehicleNo;
                    asnEntity.WmsRemark = jbr.wmsRemark;
                    asnEntity.ContractNO = jbr.contractNo;
                    #endregion

                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.closeDate))
                            asnEntity.CloseDate = Convert.ToDateTime(jbr.closeDate);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("PSoManage+QueryBills", msg);
                    }

                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.createDate))
                            asnEntity.CreateDate = Convert.ToDateTime(jbr.createDate);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("PSoManage+QueryBills", msg);
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

        private void QueryAllCaseBill()
        {
            #region 公能不用
            Dictionary<string, object> settings = GlobeSettings.SystemSettings;
            bindingSource1.DataSource = QueryAllCaseBill(!settings.ContainsKey("出库方式") ? 0 : ConvertUtil.ToInt(settings["出库方式"]));
            #endregion
        }
        #endregion

    }
}
