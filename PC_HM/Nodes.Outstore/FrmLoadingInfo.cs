using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nodes.Utils;
using Nodes.UI;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Common;
using Nodes.Shares;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;
using Newtonsoft.Json;
using DevExpress.Utils;
using Nodes.Net;
using System.Net;
using System.Net.Security;

namespace Nodes.Outstore
{
    /// <summary>
    /// 装车信息窗口
    /// </summary>
    public partial class FrmLoadingInfo : DevExpress.XtraEditors.XtraForm
    {
        private HttpContext _httpContext = new HttpContext(XmlBaseClass.ReadResourcesValue("TMS_URL"));
        #region 构造函数

        public FrmLoadingInfo()
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

        #region 方法
        private void LoadData()
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
        /// 刷新数据
        /// </summary>
        private void RefreshData()
        {
            if (this.searchLookUpEdit1.EditValue != null)
            {
                this.btnQuery_Click(this.btnQuery, EventArgs.Empty);
            }
        }
        /// <summary>
        /// 分派装车
        /// </summary>
        private void CreateLoadingInfo()
        {
            Dictionary<string, object> settings = GlobeSettings.SystemSettings;
            if (settings.ContainsKey("出库方式") && settings["出库方式"].ToString() == "1")
            {
                using (FrmLoadingTMSData frmLoadingTMSData = new FrmLoadingTMSData())
                {
                    if (frmLoadingTMSData.ShowDialog() != DialogResult.OK)
                        return;
                    this.RefreshData();
                }
            }
            else
            {
                using (FrmNonLoadingBills frmNonLoadingBills = new FrmNonLoadingBills())
                {
                    if (frmNonLoadingBills.ShowDialog() != DialogResult.OK)
                        return;
                    this.RefreshData();
                }
            }
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
        /// 装车信息：车辆变更
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        public bool ChangeVehicle(LoadingHeaderEntity header)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("vehicleId=").Append(header.VehicleID).Append("&");
                loStr.Append("loadingNo=").Append(header.LoadingNO);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_ChangeVehicle);
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
        /// 变更车辆
        /// </summary>
        private void ChangeVehicle()
        {
            LoadingHeaderEntity header = this.gridView1.GetFocusedRow() as LoadingHeaderEntity;
            if (header == null)
            {
                MsgBox.Warn("请先选择装车信息");
                return;
            }
            if (header.Details.Count > 0 && !header.Details.Exists(u => u.BillState != "68" && u.BillState != "693"))
            {
                MsgBox.Warn("该装车信息中的所有订单已经<发货完成>，不可编辑！");
                return;
            }
            using (FrmChooseVehicle frmChooseVehicle = new FrmChooseVehicle(header.VehicleID))
            {
                if (frmChooseVehicle.ShowDialog() == DialogResult.OK)
                {
                    string oldVehicleNO = header.VehicleNO;
                    header.VehicleID = frmChooseVehicle.SelectedVehicle.ID;
                    if (ChangeVehicle(header))
                    {
                        // 添加日志记录并刷新界面
                        Insert(ELogType.装车, GlobeSettings.LoginedUser.UserName,
                            header.LoadingNO, oldVehicleNO, "车辆变更", DateTime.Now, 
                            frmChooseVehicle.SelectedVehicle.VehicleNO);
                        this.RefreshData();
                    }
                }
            }
        }

        private void EditLoadingInfo()
        {
            LoadingHeaderEntity header = this.gridView1.GetFocusedRow() as LoadingHeaderEntity;
            if (header == null)
            {
                MsgBox.Warn("请先选择装车信息！");
                return;
            }
            else if (header.TrainDate != null && header.TrainDate != DateTime.MinValue &&
                DateTime.Now.Subtract(ConvertUtil.ToDatetime(header.TrainDate)).TotalHours > 12)
            {
                MsgBox.Warn("当前装车信息已生成车次数据超过12小时，不允许编辑！");
                return;
            }
            if (header.Details.Count > 0 && !header.Details.Exists(u => u.BillState != "68" && u.BillState != "693"))
            {
                MsgBox.Warn("该装车信息中的所有订单已经<发货完成>，不可编辑！");
                return;
            }
            using (FrmLoadingInfoEdit frmLoadingInfoEdit = new FrmLoadingInfoEdit(header))
            {
                frmLoadingInfoEdit.ShowDialog();
                this.RefreshData();
            }
        }

        /// <summary>
        /// 装车信息--完成装车
        /// </summary>
        /// <param name="loadingNo"></param>
        /// <returns></returns>
        public bool FinishLoadingInfo(string loadingNo)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("vehicleNo=").Append(loadingNo);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_FinishLoadingInfo);
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


        private void FinishLoading()
        {
            try
            {
                LoadingHeaderEntity header = this.gridView1.GetFocusedRow() as LoadingHeaderEntity;
                if (header == null)
                {
                    MsgBox.Warn("请先选择装车信息");
                    return;
                }
                else if (header.Details.Count > 0 && header.Details.Exists(u => u.BillState != "68" && u.BillState != "693"))
                {
                    MsgBox.Warn("该装车信息中的存在未<发货完成>订单，不可进行<完成装车>操作！");
                    return;
                }
                else if (header.FinishDate != null)
                {
                    MsgBox.Warn("不允许多次执行<完成装车>操作！");
                    return;
                }
                else if (MsgBox.AskOK("是否确认执行<完成装车>操作？") == DialogResult.OK)
                {
                    if (FinishLoadingInfo(header.LoadingNO))
                    {

                        List<SOHeaderEntity> listHeader = GetHeaderInfoByBillNOS(header.LoadingNO,1);
                        foreach (SOHeaderEntity entity in listHeader)
                        {
                            InsertSOLog(entity.BillID, ESOOperationType.装车完毕.ToString(), GlobeSettings.LoginedUser.UserName);
                        }
                        this.RefreshData();
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 装车信息--删除没有明显的装车表头
        /// </summary>
        /// <param name="loadingNo"></param>
        /// <returns></returns>
        public bool DeleteLoading(string loadingNo)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("loadingNo=").Append(loadingNo);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_DeleteLoading);
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

        private void DeleteLoadingOrder() 
        {
            if (gridView1.SelectedRowsCount < 1)
            {
                MsgBox.Warn("请选择要删除的行！");
                return;
            }
            LoadingHeaderEntity header = this.gridView1.GetFocusedRow() as LoadingHeaderEntity;

            bool result = DeleteLoading(header.LoadingNO);

            if (!result)
            {
                //MsgBox.Warn("该装车单存在明细不能删除！");
                return;
            }
            else
            {
                MsgBox.Warn("删除成功！");
                this.btnQuery_Click(this.btnQuery, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 订单落放明细,查找商品明细
        /// </summary>
        /// <returns></returns>
        public List<Nodes.DBHelper.Print.SOFindGoodsDetail> GetFindGoodsDetailVhCode(string vhCode)
        {
            List<Nodes.DBHelper.Print.SOFindGoodsDetail> list = new List<Nodes.DBHelper.Print.SOFindGoodsDetail>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("vhCode=").Append(vhCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetFindGoodsDetailVhCode, 300000);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetFindGoodsDetailVhCode bill = JsonConvert.DeserializeObject<JsonGetFindGoodsDetailVhCode>(jsonQuery);
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
                #region 赋值

                foreach (JsonGetFindGoodsDetailVhCodeResult tm in bill.result)
                {
                    Nodes.DBHelper.Print.SOFindGoodsDetail sku = new Nodes.DBHelper.Print.SOFindGoodsDetail();
                    #region 0-7
                    sku.CustomerAddress = tm.address;
                    sku.BillID = Convert.ToInt32(tm.billId);
                    sku.BillNo = tm.billNo;
                    sku.CustomerName = tm.cName;
                    sku.DelayMark = Convert.ToInt32(tm.delayMark);
                    sku.SanNum = Convert.ToInt32(tm.sanNum);
                    sku.ZhengNum = Convert.ToInt32(tm.zhengNum);
                    sku.LoadingSort = Convert.ToInt32(tm.loadingSort);
                    #endregion

                    list.Add(sku);
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
        /// 物流箱信息
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="wareHouseType"></param>
        /// <returns></returns>
        public List<JsonGetWuLiuXiangInfoResult> GetWuLiuXiangInfo2(string wareHouseType, string vhNo)
        {
            #region
            //DataTable tblDatas = new DataTable("Datas");
            //tblDatas.Columns.Add("CTL_ID", Type.GetType("System.Int32"));
            //tblDatas.Columns.Add("CT_CODE", Type.GetType("System.String"));
            //tblDatas.Columns.Add("LC_CODE", Type.GetType("System.String"));
            #endregion
            List<JsonGetWuLiuXiangInfoResult> list = new List<JsonGetWuLiuXiangInfoResult>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("vhTraninNo=").Append(vhNo);//.Append("&");
                //loStr.Append("wareHouseType=").Append(EUtilStroreType.WarehouseTypeToInt(GlobeSettings.LoginedUser.WarehouseType));
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetWuLiuXiangInfo2);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetWuLiuXiangInfo bill = JsonConvert.DeserializeObject<JsonGetWuLiuXiangInfo>(jsonQuery);
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

                #region 赋值
                foreach (JsonGetWuLiuXiangInfoResult tm in bill.result)
                {
                    //DataRow newRow;
                    //newRow = tblDatas.NewRow();
                    //newRow["CTL_ID"] = System.Convert.ToInt32(tm.ctlId);
                    //newRow["CT_CODE"] = tm.ctCode;
                    //newRow["LC_CODE"] = tm.lcCode;
                    //tblDatas.Rows.Add(newRow);

                    list.Add(tm);
                }
                //list.Sort();
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
        /// 返回值DataTable
        /// </summary>
        /// <param name="wareHouseType"></param>
        /// <param name="vhNo"></param>
        /// <returns></returns>
        public DataTable GetWuLiuXiangInfo3(string wareHouseType, string vhNo)
        {
            #region
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("CTL_ID", Type.GetType("System.Int32"));
            tblDatas.Columns.Add("CT_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("LC_CODE", Type.GetType("System.String"));
            #endregion
            List<JsonGetWuLiuXiangInfoResult> list = new List<JsonGetWuLiuXiangInfoResult>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("vhTraninNo=").Append(vhNo);//.Append("&");
                //loStr.Append("wareHouseType=").Append(EUtilStroreType.WarehouseTypeToInt(GlobeSettings.LoginedUser.WarehouseType));
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetWuLiuXiangInfo2);
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

        private string InsertStr(string lcCode)
        {
            string temp = lcCode;
            temp = temp.Insert(0, "00000");
            temp = temp.Substring(temp.Length - 5, temp.Length - (temp.Length - 5));
            return temp;
        }

        private void PrintFindGoods()
        {
            try
            {
                LoadingHeaderEntity header = this.gridView1.GetFocusedRow() as LoadingHeaderEntity;
                if (header == null)
                {
                    MsgBox.Warn("请先选择装车信息");
                    return;
                }
                List<Nodes.DBHelper.Print.SOFindGoodsDetail> list = GetFindGoodsDetailVhCode(header.LoadingNO);
                //List<JsonGetWuLiuXiangInfoResult> dt = GetWuLiuXiangInfo2(GlobeSettings.LoginedUser.WarehouseType.ToString(), header.LoadingNO);
                DataTable dt = GetWuLiuXiangInfo3(GlobeSettings.LoginedUser.WarehouseType.ToString(), header.LoadingNO);

                SOFindGoodsDetailList dataSource = new SOFindGoodsDetailList();
                if (list != null && list.Count > 0)
                {
                    foreach (Nodes.DBHelper.Print.SOFindGoodsDetail item in list)
                    {
                        dataSource.Details.Add(Nodes.Outstore.SOFindGoodsDetail.Convert(item));
                    }
                    #region
                    //int index = 0;
                    //int CTInfo = 0;
                    //foreach (Nodes.DBHelper.Print.SOFindGoodsDetail item in list)
                    //{
                    //    dataSource.Details.Add(Nodes.Outstore.SOFindGoodsDetail.Convert(item));
                    //    #region 对笼车位进行排序
                    //    if (dt != null && dt.Count > 0)
                    //    {
                    //        StringBuilder sb = new StringBuilder();
                    //        int jIndex = 0,NumIndex = 0;
                    //        for (int kIndex = CTInfo; jIndex < dataSource.Details[index].XiangStrNum; jIndex++)
                    //        {
                    //            JsonGetWuLiuXiangInfoResult row = dt[kIndex+jIndex];
                    //            {
                    //                sb.AppendFormat("{0}-{1}-{2}",
                    //                string.IsNullOrEmpty(row.ctlId) ? "(空)" : row.ctlId,
                    //                string.IsNullOrEmpty(row.lcCode) ? "(空)" : row.lcCode,
                    //                string.IsNullOrEmpty(row.ctCode) ? "(空)" : row.ctCode);
                    //                NumIndex++;
                    //                if (NumIndex < dataSource.Details[index].XiangStrNum)
                    //                {
                    //                    sb.Append("\r\n");
                    //                }
                    //                CTInfo++;
                    //            }
                    //        }

                    //        dataSource.Details[index].DetailLongChe = sb.ToString();

                    //        #region 注释
                    //        //JsonRepFindGoods ShowInfo = new JsonRepFindGoods();
                    //        //ShowInfo.ShowInfo = sb.ToString();
                    //        //dataSource.LongChe.Add(ShowInfo);
                    //        #endregion

                    //        #region 注释
                    //        //foreach (JsonGetWuLiuXiangInfoResult row in dt)
                    //        //{
                    //        //    if (JsonIndex < dataSource.Details[index].XiangStrNum)
                    //        //    {
                    //        //        sb.AppendFormat("{0}-{1}-{2}",
                    //        //        string.IsNullOrEmpty(row.ctlId) ? "(空)" : row.ctlId,
                    //        //        string.IsNullOrEmpty(row.lcCode) ? "(空)" : row.lcCode,
                    //        //        string.IsNullOrEmpty(row.ctCode) ? "(空)" : row.ctCode);
                    //        //        {
                    //        //            sb.Append("\r\n");
                    //        //        }
                    //        //        JsonIndex++;
                    //        //    }
                    //        //}

                    //        //dataSource.LongCheStr = sb.ToString();
                    //        #endregion
                    //    }
                    //    #endregion

                    //    index++;
                    //}
                    #endregion

                    //绑定数据
                    #region 对笼车位进行排序
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        //先进行排序
                        DataView dv = dt.DefaultView;
                        dv.Sort = "CTL_ID Asc";
                        dt = dv.ToTable();

                        StringBuilder sb = new StringBuilder();
                        int index = 1;//需要添加的回车 ; 
                        string longcheCode = string.Empty;//标记笼车分组
                        string lcFlag = string.Empty;//表示是否根据笼车号进行换行处理
                        foreach (DataRow row in dt.Rows)
                        {
                            int ctlID = System.Convert.ToInt32(row["CTL_ID"]);// (ctlID == 0) ? "(空)" : (ctlID.ToString().Length  == 8) ? "散货未到" : ctlID.ToString(),
                            #region no
                            /*sb.AppendFormat("{0}{1}-{2}-{3}{4}",
                                "        ",
                                (ctlID == 0) ? "(空)" : ctlID.ToString(),
                                string.IsNullOrEmpty(ConvertUtil.ToString(row["LC_CODE"])) ? "(空)" : row["LC_CODE"],
                                string.IsNullOrEmpty(ConvertUtil.ToString(row["CT_CODE"])) ? "(空)" : row["CT_CODE"],
                                "               ");
                            index++;
                            if (index < dt.Rows.Count && (index % 8 == 0))
                            {
                                isFirst = true;
                                sb.Append("\r\n");
                            }*/
                            #endregion

                            #region
                            string ctcode = ConvertUtil.ToString(row["CT_CODE"]);
                            string lcCode = ConvertUtil.ToString(row["LC_CODE"]);
                            
                            if (longcheCode == ctlID.ToString())
                            {
                                #region
                                if (lcFlag == lcCode)
                                {
                                    #region
                                    sb.AppendFormat("{0}{1}",
                                    string.IsNullOrEmpty(ctcode) ? "(空)" : InsertStr(ctcode),
                                   "    ");
                                    index++;
                                    if (index < dt.Rows.Count && (index % 12 == 0))
                                    {
                                        sb.Append("\r\n");
                                        //添加31个遇上一行对齐
                                        sb.Append("                               ");
                                        //sb.Append("                       ");23
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region
                                    if (sb.Length > 0)//去掉末尾空格
                                    {
                                        sb.Remove(sb.Length - 4, 4);
                                        sb.Append("】\r\n");
                                    }
                                    index = 1;//重置换行个数
                                    lcFlag = lcCode;
                                    longcheCode = ctlID.ToString();
                                    string idInfo = "0" + ctlID.ToString();//2
                                    sb.AppendFormat("【{0}-{1}】--【",
                                        (ctlID == 0) ? "**" : (ctlID.ToString().Length == 8) ?
                                        "##" : (ctlID.ToString().Length == 1) ? idInfo : ctlID.ToString(),
                                    string.IsNullOrEmpty(lcCode) ? "空" : InsertStr(lcCode));
                                    //sb.Append("\r\n");
                                    sb.AppendFormat("{0}{1}",
                                    string.IsNullOrEmpty(ctcode) ? "(空)" : InsertStr(ctcode),
                                    "    ");
                                    #endregion
                                }
                                #endregion
                            }
                            else
                            {
                                #region 
                                if (sb.Length > 0)//去掉末尾空格
                                {
                                    sb.Remove(sb.Length - 4, 4);
                                    sb.Append("】\r\n");
                                }
                                index = 1;//重置换行个数
                                lcFlag = lcCode;
                                longcheCode = ctlID.ToString();
                                string idInfo = "0" + ctlID.ToString();//2
                                sb.AppendFormat("【{0}-{1}】--【",
                                    (ctlID == 0) ? "**" : (ctlID.ToString().Length == 8) ?
                                    "##" : (ctlID.ToString().Length == 1) ? idInfo : ctlID.ToString(),
                                string.IsNullOrEmpty(lcCode) ? "空" : InsertStr(lcCode));
                                //sb.Append("\r\n");
                                sb.AppendFormat("{0}{1}",
                                string.IsNullOrEmpty(ctcode) ? "(空)" : InsertStr(ctcode),
                                "    ");
                                #endregion
                                //"               ");
                            }
                            #endregion
                        }

                        //去掉末尾空格
                        sb.Remove(sb.Length - 4, 4);
                        sb.Append("】");
                        dataSource.LongCheStr = sb.ToString();
                    }
                    #endregion
                }

                using (RepSOFindGoods rep = new RepSOFindGoods(header.WarehouseName, header.VehicleNO, dataSource, "打印装车单"))
                {
                    rep.ShowPreviewDialog();
                }
            }
            catch (Exception msg)
            {
                MsgBox.Warn(msg.Message);
                MsgBox.Warn(msg.Source);
                MsgBox.Warn(msg.StackTrace);
            }
        }

        #endregion

        #region 事件

        private void barButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (e.Item.Tag == null)
                    return;
                switch (ConvertUtil.ToString(e.Item.Tag))
                {
                    case "刷新":
                        break;
                    case "分派装车":
                        this.CreateLoadingInfo();
                        break;
                    case "编辑装车信息":
                        this.EditLoadingInfo();
                        break;
                    case "装车记录查询":
                        using (FrmLoadingRecords frmRecords = new FrmLoadingRecords())
                        {
                            frmRecords.ShowDialog();
                        }
                        break;
                    case "完成装车":
                        this.FinishLoading();
                        break;
                    case "变更车辆":
                        this.ChangeVehicle();
                        break;
                    case "删除":
                        this.DeleteLoadingOrder();
                        break;
                    case "打印装车单":
                        this.PrintFindGoods();
                        break;
                    case "排车":
                        this.RequestPlanBills();
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 获取所有装车信息表头
        /// </summary>
        /// <param name="vehicleID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<LoadingHeaderEntity> GetLoadingHeaders(int vehicleID, DateTime beginDate, DateTime endDate)
        {
            List<LoadingHeaderEntity> list = new List<LoadingHeaderEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("vehicleID=").Append(vehicleID).Append("&");
                loStr.Append("beginDate=").Append(beginDate).Append("&");
                loStr.Append("endDate=").Append(endDate);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetLoadingHeaders);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetLoadingHeaders bill = JsonConvert.DeserializeObject<JsonGetLoadingHeaders>(jsonQuery);
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
                foreach (JsonGetLoadingHeadersResult jbr in bill.result)
                {
                    LoadingHeaderEntity asnEntity = new LoadingHeaderEntity();
                    asnEntity.VehicleID = Convert.ToInt32(jbr.vhId);
                    asnEntity.VehicleNO = jbr.vhNo;
                    asnEntity.UserName = jbr.userName;
                    asnEntity.WarehouseName = jbr.whName;
                    asnEntity.LoadingNO = jbr.vhTrainNo;
                    asnEntity.TrainNo = jbr.trainNo;

                    #region
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.finishDate))
                            asnEntity.FinishDate = Convert.ToDateTime(jbr.finishDate);

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
                        if (!string.IsNullOrEmpty(jbr.trainDate))
                            asnEntity.TrainDate = Convert.ToDateTime(jbr.trainDate);

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
                        if (!string.IsNullOrEmpty(jbr.updateDate))
                        {
                            asnEntity.UpdateDate = Convert.ToDateTime(jbr.updateDate);
                        }
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
        /// 查询
        /// </summary>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                VehicleEntity vehicle = this.searchLookUpEdit1.EditValue as VehicleEntity;
                if (vehicle == null)
                {
                    MsgBox.Warn("请选择车辆信息。");
                    return;
                }
                DateTime beginDate = ConvertUtil.ToDatetime(this.BeginDate.EditValue).Date;
                DateTime endDate = ConvertUtil.ToDatetime(this.EndDate.EditValue).Date.AddDays(1);

                this.gridControl3.DataSource = this.gridControl2.DataSource = null;
                gridControl1.DataSource = GetLoadingHeaders(vehicle.ID, beginDate, endDate);
                this.gridView1_FocusedRowChanged(null, null);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 显示关联的订单和装车员--装车员
        /// </summary>
        /// <param name="loadingNo"></param>
        /// <returns></returns>
        public List<LoadingUserEntity> GetLoadingUsers(string loadingNo)
        {
            List<LoadingUserEntity> list = new List<LoadingUserEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("loadingNo=").Append(loadingNo);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetLoadingUsers);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetLoadingUsers bill = JsonConvert.DeserializeObject<JsonGetLoadingUsers>(jsonQuery);
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
                foreach (JsonGetLoadingUsersResult jbr in bill.result)
                {
                    LoadingUserEntity asnEntity = new LoadingUserEntity();
                    asnEntity.UserCode = jbr.userCode;
                    asnEntity.UserName = jbr.userName;
                    asnEntity.TaskDesc = jbr.itemDesc;
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
        /// 显示关联的订单和装车员--订单
        /// </summary>
        /// <param name="loadingNo"></param>
        /// <returns></returns>
        public List<LoadingDetailEntity> GetLoadingDetails(string loadingNo)
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
        /// 显示关联的订单和装车员
        /// </summary>
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                LoadingHeaderEntity header = this.gridView1.GetFocusedRow() as LoadingHeaderEntity;
                if (header == null) return;
                header.Users = GetLoadingUsers(header.LoadingNO);
                header.Details = GetLoadingDetails(header.LoadingNO);
                this.gridView3.ViewCaption = string.Format(
                    "装车编号:{0} (共计:{1} 单)   车牌号：{2}", 
                    header.LoadingNO, header.Details == null ? 0 : header.Details.Count, header.VehicleNO);
                this.gridControl2.DataSource = header.Users;
                this.gridControl3.DataSource = header.Details;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void gridView1_RowDoubleClick(object sender, EventArgs e)
        {
            this.EditLoadingInfo();
        }

         /// <summary>
        /// 新增分组
        /// </summary>
        /// <param name="header">分组对象</param>
        /// <returns>新增结果</returns>
        public bool RequestPlanBillsInsert(TMSDataHeader header)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("jsonStr=").Append(EntitiesToJson.ToJson(header));
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_RequestPlanBillsInsert);
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

        private void RequestPlanBills()
        {
            // 查看系统设置，是否走接口流程
            Dictionary<string, object> settings = GlobeSettings.SystemSettings;
            if (!settings.ContainsKey("出库方式") || settings["出库方式"].ToString() != "1")
            {
                MsgBox.Warn("当前系统设置下，功能不可用！");
                return;
            }
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
                    //response.ResultData = "{\"id\":\"C20201612071120044161\",\"car_type\":\"20\",\"start_time\":\"2016-12-07 19:20:04\",\"storehouse\":\"105\",\"order_list\":{\"50979\":{\"x\":\"116.382804\",\"y\":\"39.962855\",\"order_info\":{\"PM201025262809977569413\":{\"sort\":0,\"zhengnum\":0,\"sannum\":0}}},\"267068\":{\"x\":\"116.388809\",\"y\":\"39.953289\",\"order_info\":{\"PM201168651810385485933\":{\"sort\":0,\"zhengnum\":0,\"sannum\":0}}},\"86256\":{\"x\":\"116.342047\",\"y\":\"39.912885\",\"order_info\":{\"PM201077321810259244695\":{\"sort\":0,\"zhengnum\":0,\"sannum\":0},\"PM201077321810063544050\":{\"sort\":0,\"zhengnum\":0,\"sannum\":0}}},\"90792\":{\"x\":\"116.334125\",\"y\":\"39.908395\",\"order_info\":{\"PM221038549810034238194\":{\"sort\":0,\"zhengnum\":0,\"sannum\":0}}},\"83974\":{\"x\":\"116.351107\",\"y\":\"39.925385\",\"order_info\":{\"PM201046313810031529293\":{\"sort\":0,\"zhengnum\":0,\"sannum\":0}}},\"452548\":{\"x\":\"116.378721\",\"y\":\"39.955882\",\"order_info\":{\"PM201360118810251665537\":{\"sort\":0,\"zhengnum\":0,\"sannum\":0}}},\"46133\":{\"x\":\"116.331273\",\"y\":\"39.909094\",\"order_info\":{\"PC111028442810179012680\":{\"sort\":0,\"zhengnum\":0,\"sannum\":0}}},\"276293\":{\"x\":\"116.36064\",\"y\":\"39.941351\",\"order_info\":{\"PM201177977809921652439\":{\"sort\":0,\"zhengnum\":0,\"sannum\":0}}},\"22197\":{\"x\":\"116.371361\",\"y\":\"39.952515\",\"order_info\":{\"PP301015055810233902507\":{\"sort\":0,\"zhengnum\":0,\"sannum\":0}}},\"49125\":{\"x\":\"116.317596\",\"y\":\"39.899857\",\"order_info\":{\"PM201046518810358876554\":{\"sort\":0,\"zhengnum\":0,\"sannum\":0}}},\"25773\":{\"x\":\"116.343117\",\"y\":\"39.927124\",\"order_info\":{\"PM221032303810347028448\":{\"sort\":0,\"zhengnum\":0,\"sannum\":0}}},\"31685\":{\"x\":\"116.358932\",\"y\":\"39.944412\",\"order_info\":{\"PP301018462810276367479\":{\"sort\":0,\"zhengnum\":0,\"sannum\":0}}}}}";
                    if (response.Result == EResponseResult.成功)
                    {
                        jsonData = Encoding.Default.GetString(response.ResultData as byte[]);
                        //jsonData = response.ResultData.ToString();//测试
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

                        //TMSDataDAL.Insert(header);bingtao
                        bool result = RequestPlanBillsInsert(header);//whc
                        if (result)
                        {
                            // 修改订单状态
                            // Update By 万伟超
                            foreach (string marketKey in header.order_list.Keys)
                            {
                                TMSDataMarket market = header.order_list[marketKey];

                                string billIds = StringUtil.JoinBySign<TMSDataDetail>(market.order_info.Values, "orderid");
                                //soDal.UpdateBillsState(billIds, "61", "60");
                            }
                            this.LoadData();
                            // 计算共有多少单
                            int totalOrder = 0;
                            foreach (string key in header.order_list.Keys)
                            {
                                TMSDataMarket dataMarket = header.order_list[key];
                                totalOrder += dataMarket.order_info.Count;
                            }
                            MsgBox.OK(string.Format("获取数据成功！组别编号：{0}  共 {1} 单。",
                                header.id, totalOrder));
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
        #endregion

        #region Override Methods

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.BeginDate.EditValue = DateTime.Now.AddDays(-10);
            this.EndDate.EditValue = DateTime.Now;
            this.LoadData();
        }
        #endregion
    }
}
