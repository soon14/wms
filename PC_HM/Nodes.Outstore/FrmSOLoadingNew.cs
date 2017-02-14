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
using System.Text;
using DevExpress.Utils;
using System.Data;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;
using Newtonsoft.Json;

namespace Nodes.Outstore
{
    public partial class FrmSOLoadingNew : DevExpress.XtraEditors.XtraForm
    {
        //SOWeightDal soWeightDal = new SOWeightDal();
        //private SODal soDal = null;
        //private ReturnManageDal returnDal = null;
        //private VehicleDal vehicleDal = null;
        List<SOHeaderEntity> List = null;
        List<UserEntity> listUsers = null;
        private int SanHuo = 0;     //散货件数（物流箱数）
        private int ZhengHuo = 0;   //整货件数
        StringBuilder sb = null;
        public FrmSOLoadingNew()
        {
            InitializeComponent();
        }
        //DataRowView row = searchLookUpEdit1.EditValue as DataRowView;
        public DataRowView row
        {
            get
            {
                if (searchLookUpEdit1.EditValue == null || searchLookUpEdit1.Text.Trim() == "")
                    return null;
                return searchLookUpEdit1.EditValue as DataRowView;
            }
        }
        public VehicleEntity Vehicle
        {
            get
            {
                if (searchLookUpEdit1.EditValue == null || searchLookUpEdit1.Text.Trim() == "")
                    return null;
                return searchLookUpEdit1.GetSelectedDataRow() as VehicleEntity;
            }
        }

        private void OnFrmLoad(object sender, EventArgs e)
        {
            try
            {
                toolPrint.ImageIndex = (int)AppResource.EIcons.print;

                //this.soDal = new SODal();
                //this.returnDal = new ReturnManageDal();
                //this.vehicleDal = new VehicleDal();

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
                case "打印销售发货单":
                    PrintSO();
                    break;
            }
        }

        /// <summary>
        /// 打印销售发货单－获取车辆信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetVehicleInfo()
        {
            #region
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("VH_NO", Type.GetType("System.String"));
            tblDatas.Columns.Add("RT_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("USER_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("ID", Type.GetType("System.String"));
            tblDatas.Columns.Add("JIANCH", Type.GetType("System.String"));
            #endregion

            try
            {
                #region 请求数据
                string jsons = string.Empty;
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                string jsonQuery = WebWork.SendRequest(jsons, WebWork.URL_GetVehicleInfo);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetVehicleInfo bill = JsonConvert.DeserializeObject<JsonGetVehicleInfo>(jsonQuery);
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
                foreach (JsonGetVehicleInfoResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["ID"] = tm.vId;
                    newRow["VH_NO"] = tm.vhNo;
                    newRow["RT_NAME"] = tm.rtName;
                    newRow["USER_NAME"] = tm.userName;
                    newRow["JIANCH"] = tm.jianCh;
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

        public void LoadDataAndBindGrid()
        {
            try
            {
                //bindingSource1.DataSource = this.vehicleDal.GetAll();
                bindingSource1.DataSource = GetVehicleInfo();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 获取未称重或未验证的容器信息
        /// </summary>
        /// <param name="vhID"></param>
        /// <returns></returns>
        public DataTable GetCtCodeCanT(int vhID,string loadingNo)
        {
            #region
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("BILL_NO", Type.GetType("System.String"));
            tblDatas.Columns.Add("CT_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("STATE", Type.GetType("System.String"));
            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("vhId=").Append(vhID).Append("&");
                loStr.Append("vehicleTrainNo=").Append(loadingNo);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetCtCodeCanT);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetCtCodeCanT bill = JsonConvert.DeserializeObject<JsonGetCtCodeCanT>(jsonQuery);
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
                foreach (JsonGetCtCodeCanTResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["BILL_NO"] = tm.billNo;
                    newRow["CT_CODE"] = tm.ctCode;
                    newRow["STATE"] = tm.state;
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
        /// 打印销售发货单－获取一个未选择人员的装车信息
        /// </summary>
        /// <param name="loadingNo"></param>
        /// <returns></returns>
        public List<LoadingUserEntity> GetLoadingInfoByNonChooseUser(string loadingNo)
        {
            List<LoadingUserEntity> list = new List<LoadingUserEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("loadingNO=").Append(loadingNo);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetLoadingInfoByNonChooseUser);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetLoadingInfoByNonChooseUser bill = JsonConvert.DeserializeObject<JsonGetLoadingInfoByNonChooseUser>(jsonQuery);
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
                foreach (JsonGetLoadingInfoByNonChooseUserResult jbr in bill.result)
                {
                    LoadingUserEntity asnEntity = new LoadingUserEntity();
                    asnEntity.ID = Convert.ToInt32(jbr.id);
                    asnEntity.LoadingNO = jbr.vhTrainNo;
                    asnEntity.TaskType = jbr.attri1;
                    asnEntity.UserCode = jbr.userCode;
                    asnEntity.UserName = jbr.userName;
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
        /// 打印销售发货单－生成车次信息
        /// </summary>
        /// <param name="whCode"></param>
        /// <param name="creator"></param>
        /// <param name="vhNo"></param>
        /// <param name="vehicleName"></param>
        /// <param name="userPhone"></param>
        /// <param name="list"></param>
        /// <param name="listUsers"></param>
        /// <param name="warehouseType"></param>
        /// <param name="loadingNo"></param>
        /// <returns></returns>
        public bool CreateTrain(string whCode, string creator, string vhNo, string vehicleName,
            string userPhone, List<SOHeaderEntity> list, List<UserEntity> listUsers,
            EWarehouseType warehouseType, string loadingNo)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("whCode=").Append(whCode).Append("&");
                loStr.Append("creator=").Append(creator).Append("&");
                loStr.Append("vehicleNo=").Append(vhNo).Append("&");
                loStr.Append("vehicleName=").Append(vehicleName).Append("&");
                loStr.Append("vehiclePhone=").Append(userPhone).Append("&");
                loStr.Append("loadingNo=").Append(loadingNo).Append("&");
                loStr.Append("warehouseType=").Append(EUtilStroreType.WarehouseTypeToInt(warehouseType)).Append("&");
                #region list 转 json
                List<string> prop = new List<string>() { "BillNO", "OriginalBillNo", "BillID", "BillType" };
                string soHeaderEntity = GetRes<SOHeaderEntity>(list, prop);
                loStr.Append("soHeaderEntity=").Append(soHeaderEntity).Append("&");
                List<string> prop1 = new List<string>() { "UserName", "UserCode", "ROLE_ID" };
                string userStr = GetRes<UserEntity>(listUsers, prop1);
                loStr.Append("listUsers=").Append(userStr);
                #endregion
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_CreateTrain);
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
        /// 打印销售发货单－释放订单相关托盘
        /// </summary>
        /// <param name="billID"></param>
        public bool UpdateContainerState(int billID)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_UpdateContainerState);
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

        public bool UpdatePrintedFlag(int billID, string creator, string BillNO,int num)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billID=").Append(billID).Append("&");
                loStr.Append("userName=").Append(creator).Append("&");
                loStr.Append("billNo=").Append(BillNO).Append("&");
                loStr.Append("warehouseType=").Append(EUtilStroreType.WarehouseTypeToInt(GlobeSettings.LoginedUser.WarehouseType)).Append("&");
                loStr.Append("printed=").Append(num);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_UpdatePrintedFlagLongMiao);
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
        /// 得到未同步到物流箱的订单，检测
        /// </summary>
        /// <param name="vhID"></param>
        /// <param name="loadingNo"></param>
        /// <returns></returns>
        public bool GetSyncCodeCanT(int vhID, string loadingNo)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("vhId=").Append(vhID).Append("&");
                loStr.Append("vehicleTrainNo=").Append(loadingNo);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetSyncCodeCanT);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return false;
                }
                #endregion

                #region 正常错误处理

                JsonGetSyncCodeCanT bill = JsonConvert.DeserializeObject<JsonGetSyncCodeCanT>(jsonQuery);
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

                #region 赋值
                if (bill.result[0].billNos == "0")
                    return true;
                else
                {
                    string msg = bill.result[0].billNos;
                    msg += ",散货信息未到。";
                    MsgBox.Warn(msg);
                    return false;
                }
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return false;
        }
        
        /// <summary>
        /// 打印销售发货单
        /// </summary>
        public void PrintSO()
        {
            if (this.List == null || this.List.Count == 0)
            {
                MsgBox.Warn("请查询要打印的车辆信息。");
                return;
            }

            LoadingHeaderEntity loadingHeader = this.searchLookUpEdit2.EditValue as LoadingHeaderEntity;

            if (GlobeSettings.LoginedUser.WarehouseType == EWarehouseType.整货仓)
            {
                if (!GetSyncCodeCanT(ConvertUtil.ToInt(row["ID"]), loadingHeader.LoadingNO))
                    return;
            }

            DataTable ctCanTDT = GetCtCodeCanT(ConvertUtil.ToInt(row["ID"]), loadingHeader.LoadingNO);
            if (ctCanTDT.Rows.Count > 0)
            {
                FrmSoWeightCanT frmsoWeigheCant = new FrmSoWeightCanT(ctCanTDT);
                frmsoWeigheCant.StartPosition = FormStartPosition.CenterScreen;
                frmsoWeigheCant.Show();
                return;
            }
            

            if (loadingHeader == null)
            {
                MsgBox.Warn("未找到装车信息，请联系技术人员！");
                return;
            }
            // 验证当前选择的装车信息是否有选择人员
            List<LoadingUserEntity> loadingUsers = GetLoadingInfoByNonChooseUser(loadingHeader.LoadingNO);
            if (loadingUsers == null || loadingUsers.Count == 0)
            {
                MsgBox.Warn(string.Format("装车编号：{0} 未选择装车人员，请选择装车人员后再打印销售发货单；", loadingHeader.LoadingNO));
                return;
            }
            if (MsgBox.AskOK(string.Format("确定打印该车辆的销售发货单？")) != DialogResult.OK)
                return;
            // 选择司机和助理信息
            //FrmChoosePersonnel frm = new FrmChoosePersonnel(true);
            FrmPersonChoosen frm = new FrmPersonChoosen();
            if (frm.ShowDialog() != DialogResult.OK)
                return;

            bool ret = CreateTrain(
                GlobeSettings.LoginedUser.WarehouseCode,
                GlobeSettings.LoginedUser.UserName,
                row["VH_NO"].ToString(),
                frm.SelectedPersonnel.UserName,
                frm.SelectedPersonnel.MobilePhone,
                this.List,
                frm.SelectedPersonnelList,
                GlobeSettings.LoginedUser.WarehouseType,
                loadingHeader.LoadingNO);
            if(!ret)
            {
                MsgBox.Warn("生成车次信息失败，请重新生成！！！");
                return;
            }

            List<int> tempBillIDs = new List<int>();
            try
            {
                using (WaitDialogForm wait = new WaitDialogForm("正在打印，请稍侯..."))
                {
                    
                    int pick_suit_type = ConvertUtil.ToInt(GlobeSettings.SystemSettings["套餐分拣方式"]);
                    string module = "打印销售发货单";
                    //sb = new StringBuilder();
                    NewPrint.sellorder sellOrder = new NewPrint.sellorder();
                    foreach (SOHeaderEntity header in this.List)
                    {
                        int printInt = 2;
                        try
                        {
                            for (int i = 0; i < printInt; i++)
                            {
                                sellOrder.printorder(string.Format("{0}#{1}",
                                    GlobeSettings.LoginedUser.WarehouseName, header.BillNO));
                                Thread.Sleep(200);
                            }
                        }
                        catch
                        {
                            // 屏蔽打印时的错误
                        }
                        Insert(ELogType.打印, GlobeSettings.LoginedUser.UserName, header.BillNO, header.BillTypeName, this.Text, row["VH_NO"].ToString());
                        InsertSOLog(header.BillID, ESOOperationType.已打印销售发货单.ToString(), GlobeSettings.LoginedUser.UserName);
                        //List<ReturnHeaderEntity> listReturn = this.returnDal.GetReturnBill(header.CustomerCode);
                        //foreach (ReturnHeaderEntity entity in listReturn)
                        //{
                        //    RepReturn repReturn = new RepReturn(entity.BillID, 1, module);
                        //    //repReturn.ShowPreviewDialog();
                        //    for (int i = 0; i < 3; i++)
                        //    {
                        //        Thread.Sleep(50);
                        //        repReturn.Print();
                        //    }
                        //    //更新打印标记为已打印、把对应的送货单号写入退货单
                        //    this.returnDal.UpdatePrintedFlag(entity.BillID, header.BillNO, header.ShipNO);
                        //}
                        //更新打印标记为已打印
                        UpdatePrintedFlag(header.BillID, GlobeSettings.LoginedUser.UserName, header.BillNO, printInt);
                        header.Printed = 1;
                        UpdateContainerState(header.BillID);
                    }
                    // 打印完成以后，修改车次的同步状态为1、更新整散数量
                    //LoadingDal.UpdateTrainInfo(trainNo, StringUtil.JoinBySign<int>(tempBillIDs, ""), GlobeSettings.LoginedUser.WarehouseType);
                    //CreateTrainLoading();
                }
                //OnbtnQueryClick(null, null);
            }
            catch (Exception ex)
            {
                MsgBox.Err("打印时遇到错误：" + ex.Message);
            }
            this.searchLookUpEdit1_EditValueChanged(this.searchLookUpEdit1, EventArgs.Empty);
        }

        public List<SOHeaderEntity> GetVhicleHeadersInfoByBillID(int vehicleID, string loadingNo)
        {
            List<SOHeaderEntity> list = new List<SOHeaderEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("vehicleID=").Append(vehicleID).Append("&");
                loStr.Append("vehicleTrainNo=").Append(loadingNo);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetVhicleHeadersInfoByBillID);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonQueryBills bill = JsonConvert.DeserializeObject<JsonQueryBills>(jsonQuery);
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
                foreach (JsonQueryBillsResult jbr in bill.result)
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
                    asnEntity.DelayMark = Convert.ToInt32(jbr.delayMark);
                    #endregion

                    #region 11-20
                    asnEntity.CancelFlag = Convert.ToInt32(jbr.cancelFlag);
                    asnEntity.ConfirmFlag = Convert.ToInt32(jbr.confirmFlag);
                    asnEntity.CrnAmount = Convert.ToDecimal(jbr.crmAmount);
                    asnEntity.CustomerCode = jbr.cCode;
                    asnEntity.CustomerName = jbr.cName;
                    asnEntity.DeliverymanMobile = jbr.mobilePhone;
                    asnEntity.FromWarehouse = jbr.fromWhCode;
                    asnEntity.FromWarehouseName = jbr.fromWhName;
                    asnEntity.OtherAmount = Convert.ToDecimal(jbr.otherAmount);
                    asnEntity.OutstoreType = jbr.outStoreType;
                    #endregion
                    #region 21---30
                    asnEntity.OutstoreTypeName = jbr.outStoreTypeName;
                    asnEntity.PayedAmount = Convert.ToDecimal(jbr.payedAmount);
                    asnEntity.PayMethod = Convert.ToInt32(jbr.payMethod);
                    asnEntity.PickZnType = jbr.pickZnType;
                    asnEntity.PickZnTypeName = jbr.pickZnTypeName;
                    asnEntity.RealAmount = Convert.ToDecimal(jbr.realAmount);
                    asnEntity.ReceiveAmount = Convert.ToDecimal(jbr.receiveAmount);
                    asnEntity.Remark = jbr.remark;
                    asnEntity.RowForeColor = Convert.ToInt32(jbr.rowColor);
                    asnEntity.SalesMan = jbr.salesMan;
                    #endregion
                    #region 31-40
                    asnEntity.ShipNO = jbr.shipNo;
                    asnEntity.ShTel = jbr.phone;
                    asnEntity.Status = jbr.billState;
                    asnEntity.StatusName = jbr.statusName;
                    asnEntity.Warehouse = jbr.whCode;
                    asnEntity.WarehouseName = jbr.whName;
                    asnEntity.WmsRemark = jbr.wmsRemark;
                    asnEntity.OriginalBillNo = jbr.originalBillNo;
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

                    #region
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.confirmDate))
                            asnEntity.ConfirmDate = Convert.ToDateTime(jbr.confirmDate);
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

        private void OnbtnQueryClick(object sender, EventArgs e)
        {
            DoQuery();
        }

        private void DoQuery()
        {
            try
            {
                if (row == null)
                {
                    MsgBox.Warn("请选择车辆信息。");
                    return;
                }
                int vehicleID = ConvertUtil.ToInt(row["ID"]);
                if (vehicleID <= 0)
                {
                    MsgBox.Warn("请选择车辆信息。");
                    return;
                }
                LoadingHeaderEntity loadingHeader = this.searchLookUpEdit2.EditValue as LoadingHeaderEntity;
                if (loadingHeader == null)
                {
                    MsgBox.Warn("请选择装车编号！");
                    return;
                }
                List = GetVhicleHeadersInfoByBillID(vehicleID, loadingHeader.LoadingNO);
                #region #差异# D04 使用此段代码
                // 找到二次发货的订单重置运单编号
                //List.ForEach(u =>
                //{
                //    if (u.DelayMark == 1)
                //    {
                //        SOHeaderEntity entity = List.Find(h => !string.IsNullOrEmpty(h.ShipNO) && h.VehicleNO == u.VehicleNO);
                //        if (entity != null)
                //        {
                //            u.ShipNO = entity.ShipNO;
                //            this.soDal.JoinBillNOAndVehicle(entity.ShipNO, u.BillID);
                //        }
                //    }
                //});
                #endregion
                gridControl1.DataSource = List;
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
            //CreateTrain(             whc
            //    GlobeSettings.LoginedUser.WarehouseCode,
            //    GlobeSettings.LoginedUser.UserName,
            //    row["VH_NO"].ToString(),
            //    row["VEHICLE_NAME"].ToString(),
            //    row["USER_PHONE"].ToString(),
            //    this.sb,
            //    List,
            //    listUsers,
            //    GlobeSettings.LoginedUser.WarehouseType);
        }

        /// <summary>
        /// 获取选择车辆的所有装车编号
        /// </summary>
        /// <param name="vehicleID"></param>
        /// <returns></returns>
        public  List<LoadingHeaderEntity> GetLoadingHeaderByVehicleID(int vehicleID)
        {
            List<LoadingHeaderEntity> list = new List<LoadingHeaderEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("vehicleID=").Append(vehicleID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetLoadingHeaderByVehicleID);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetLoadingHeaderByVehicleID bill = JsonConvert.DeserializeObject<JsonGetLoadingHeaderByVehicleID>(jsonQuery);
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
                foreach (JsonGetLoadingHeaderByVehicleIDResult jbr in bill.result)
                {
                    LoadingHeaderEntity asnEntity = new LoadingHeaderEntity();
                    asnEntity.LoadingNO = jbr.vhTrain;
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
        /// 获取选择车辆的所有装车编号
        /// </summary>
        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                using (WaitDialogForm dialog = new WaitDialogForm())
                {
                    this.searchLookUpEdit2.Properties.DataSource = null;
                    this.searchLookUpEdit2.EditValue = null;
                    this.gridControl1.DataSource = null;

                    List<LoadingHeaderEntity> list = GetLoadingHeaderByVehicleID(ConvertUtil.ToInt(row["ID"]));
                    if (list == null || list.Count == 0)
                    {
                        MsgBox.Warn("未找到与该车辆关联的装车信息！");
                        return;
                    }
                    this.searchLookUpEdit2.Properties.DataSource = list;
                    this.searchLookUpEdit2.EditValue = list[0];
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void searchLookUpEdit2_EditValueChanged(object sender, EventArgs e)
        {
            LoadingHeaderEntity loadingHeader = this.searchLookUpEdit2.EditValue as LoadingHeaderEntity;
            if (loadingHeader == null)
                return;
            
            DoQuery();
        }
    }
}