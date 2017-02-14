using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Nodes.UI;
//using Nodes.DBHelper;
using Nodes.Shares;
using Nodes.SystemManage;
using Nodes.Utils;
using System.Text.RegularExpressions;
using System.Text;
using Nodes.Entities;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;
using Nodes.Entities.HttpEntity.Instore;
using Newtonsoft.Json;

namespace Nodes.Outstore
{
    public partial class FrmSOWeightLoading_Simple : DevExpress.XtraEditors.XtraForm
    {

        private string VhTrainNo = null;                        //当前装车任务的编号（整车称重完毕后清空）
        private VehicleEntity CurrentVehicle = null;                    //当前装车任务的车辆（整车称重完毕后清空）        
        private DataTable vhTrainNoContainers = null;           //当前车辆所有托盘列表

        private int BillID = -1;
        private SOHeaderEntity soHeader = null;                  //当前托盘所属订单

        private string matchRex = @"^S\s{1,2}S\s{1,10}\S{1,10}\s{1,2}kg";

        private decimal realWeight = 0;                         //存实际称重        
        //private decimal diNiuWeight = 0;                        //（每个托盘）扫描的地牛重量(每托称完后清0)
        private ContainerEntity CurrentDiNiu = null;             //（称重托盘时）使用的地牛

        private int tuoPanAllNum = 0;                           //当前订单所有托盘数  
        //private List<string> groupTPList = new List<string>();     //（每批/每单）已称重的托盘        


        public FrmSOWeightLoading_Simple()
        {
            InitializeComponent();
        }

        private void OnFrmLoad(object sender, EventArgs e)
        {
            try
            {
                txtBoxForContainerCode.Focus();

#if !DEBUG
                lblVhTrainNo.Visible = false;
#else
                lblVhTrainNo.Visible = true;
                comboBoxEditCom.Text = "200.00";
                lblCurrentWeight.Text = comboBoxEditCom.Text;
                comboBoxEditCom.TextChanged += new EventHandler(btnWeightForTest_TextChanged);
#endif

                if (!string.IsNullOrEmpty(Properties.Settings.Default.COMWeightNo))
                {
                    comboBoxEditCom.Text = Properties.Settings.Default.COMWeightNo;
                    TryOpenCom();    //尝试打开串口
                }

                #region 测试 报警灯
                /*
                try
                {
                    if (!serialPort2.IsOpen)
                    {
                        serialPort2.PortName = "COM3";
                        serialPort2.BaudRate = 9600;
                        serialPort2.Open();

                        lblComState.Text += "报警灯串口打开成功；";
                        timer1.Enabled = true;
                        this.txtBox.Focus();
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Show("报警灯：" + ex.Message, false);
                }
                 */
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
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


        private void OnFrmClosing(object sender, FormClosingEventArgs e)
        {
            //如果单据称重没有全部完成，提示
            if (soHeader != null)
            {
                if (BillID != -1 && BillID == soHeader.BillID)
                {
                    string msg = string.Format("订单【{0}】还有称重未通过的托盘，\r\n请确保整个订单的所有托盘称重通过，以免遗漏托盘！\r\n确定要关闭称重界面吗？", soHeader.BillNO);
                    DialogResult dr = MsgBox.AskOK(msg);
                    if (dr != DialogResult.OK)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        Insert(ELogType.装车, GlobeSettings.LoginedUser.UserName, soHeader.BillNO, "整货称重", "整货称重关闭界面", DateTime.Now, "整货称重中途关闭界面");
                        TryCloseCom();    //尝试关闭串口
                    }
                }
            }
        }

        private void FrmSOWeight_Shown(object sender, EventArgs e)
        {
            txtBoxForContainerCode.Focus();
        }

        private void btnWeightForTest_TextChanged(object sender, EventArgs e)
        {
            lblCurrentWeight.Text = comboBoxEditCom.Text; //输入模拟重量 方便测试
        }

        //文本框回车事件
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                WriteToSerialPort();  //向电子称串口发送指令
            }
        }

        //向串口发送指令  获取 重量数据
        private void WriteToSerialPort()
        {
            //先查看扫描的条码是否为空
            if (txtBoxForContainerCode.Text.Trim() == "")
            {
                lblMsg.Show("请扫描地牛或托盘！", false);
                txtBoxForContainerCode.Text = String.Empty;
                return;
            }

            //回车后，先给电子称串口发送指令“S\r\n"
            try
            {
#if !DEBUG
                if (serialPort1.IsOpen)
                {
                    serialPort1.WriteLine("S");
                }
                else
                {
                    lblMsg.Show("请先确认串口是否打开。", false);
                    txtBoxForContainerCode.SelectAll();
                    txtBoxForContainerCode.Focus();
                }
#else
                Save();//测试用
#endif
            }
            catch (Exception ex)
            {
                lblMsg.Show(ex.Message, false);
            }
        }

        #region 保存
        /// <summary>
        /// 获取当前容器的信息
        /// </summary>
        /// <param name="containerCode"></param>
        /// <returns></returns>
        public ContainerEntity GetCurrentContainerInfo(string containerCode)
        {
            ContainerEntity tm = new ContainerEntity();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("ctCode=").Append(containerCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetCurrentContainerInfo);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tm;
                }
                #endregion

                #region 正常错误处理

                JsonGetCurrentContainerInfo bill = JsonConvert.DeserializeObject<JsonGetCurrentContainerInfo>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return tm;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return tm;
                }
                #endregion
                List<ContainerEntity> list = new List<ContainerEntity>();
                #region 赋值数据
                foreach (JsonGetCurrentContainerInfoResult jbr in bill.result)
                {
                    ContainerEntity asnEntity = new ContainerEntity();
                    asnEntity.BillHeadID = Convert.ToInt32(jbr.billHeadId);
                    asnEntity.ContainerCode = jbr.ctCode;
                    asnEntity.ContainerState = jbr.ctState;
                    asnEntity.ContainerType = jbr.ctType;
                    asnEntity.ContainerWeight = jbr.ctWeight;
                    asnEntity.GrossWeight = jbr.crossWeight;
                    asnEntity.NetWeight = jbr.netWeight;
                    //asnEntity.CalcWeight  理论重量
                    list.Add(asnEntity);
                }
                if (list.Count > 0)
                    return list[0];
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return tm;
        }


        /// <summary>
        /// 根据当前扫描的容器(物流箱或托盘)查出与该容器关联的订单信息
        /// </summary>
        /// <param name="containerCode"></param>
        /// <returns></returns>
        public SOHeaderEntity GetBillInfoByContainer(string containerCode)
        {
            SOHeaderEntity tem = new SOHeaderEntity();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("ctCode=").Append(containerCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetBillInfoByContainer);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tem;
                }
                #endregion

                #region 正常错误处理

                JsonGetBillInfoByContainer bill = JsonConvert.DeserializeObject<JsonGetBillInfoByContainer>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return tem;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return tem;
                }
                #endregion
                List<SOHeaderEntity> list = new List<SOHeaderEntity>();
                #region 赋值数据
                foreach (JsonGetBillInfoByContainerResult jbr in bill.result)
                {
                    SOHeaderEntity asnEntity = new SOHeaderEntity();
                    asnEntity.Address = jbr.address;
                    asnEntity.BillID = Convert.ToInt32(jbr.billId);
                    asnEntity.BillNO = jbr.billNo;
                    asnEntity.CustomerName = jbr.cName;
                    asnEntity.CancelFlag = Convert.ToInt32(jbr.cancelFlag);
                    asnEntity.Consignee = jbr.contact;
                    asnEntity.SyncState = Convert.ToInt32(jbr.syncState);
                    asnEntity.Status = jbr.billState;
                    list.Add(asnEntity);
                }
                if (list.Count > 0)
                    return list[0];
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return tem;
        }

        /// <summary>
        /// 获取指定订单的称重记录数量，不包含传入的容器编号
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="ctCode"></param>
        /// <returns></returns>
        public  bool GetWeightRecordsCountByBillID(int billID, string ctCode)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID).Append("&");
                loStr.Append("ctCode=").Append(ctCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetWeightRecordsCountByBillID);
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
        /// 获得系统设置某项的值 (如 物流箱标准偏差)
        /// </summary>
        /// <returns></returns>
        public object GetSystemDiffSet(string itemName)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("item=").Append(itemName);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetSystemDiffSet);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return null;
                }
                #endregion

                #region 正常错误处理

                JsonGetSystemDiffSet bill = JsonConvert.DeserializeObject<JsonGetSystemDiffSet>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return null;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return null;
                }
                #endregion
                if (bill.result == null)
                    return null;
                if (bill.result.Length > 0)
                    return bill.result[0].setValue as object;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return null;
        }

        /// <summary>
        /// 向数据库写日志
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="ctCode"></param>
        /// <returns></returns>
        public  bool InsertSoLog(int billID, string evt, DateTime dateTime, string userName)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID).Append("&");
                loStr.Append("creator=").Append(userName).Append("&");
                loStr.Append("evt=").Append(evt);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_InsertSoLog);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
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
        /// 更新目标订单信息
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="stateValue"></param>
        /// <returns></returns>
        public bool UpdateCurrentBillState(int billID, string stateValue)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billState=").Append(stateValue).Append("&");
                loStr.Append("warehouseType=").Append(EUtilStroreType.WarehouseTypeToInt(GlobeSettings.LoginedUser.WarehouseType)).Append("&");
                loStr.Append("billId=").Append(billID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_UpdateCurrentBillState);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
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
        /// 判断单据有指定类型的任务的个数
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="isCase"></param>
        /// <returns></returns>
        public int GetCountOfTaskByCase(int billID, int isCase)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID).Append("&");
                loStr.Append("isCase=").Append(isCase);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetCountOfTaskByCase);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    return -1;
                }
                #endregion

                #region 正常错误处理

                JsonGetCountOfTaskByCase bill = JsonConvert.DeserializeObject<JsonGetCountOfTaskByCase>(jsonQuery);
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
                if (bill.result == null)
                    return -1;
                if (bill.result.Length > 0)
                    return bill.result[0].total;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return -1;
        }

        /// <summary>
        /// 判断单据是否有指定的货品
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public bool IsHasCase(int billID, int isCase)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID).Append("&");
                loStr.Append("isCase=").Append(isCase);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_IsHasCase);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    return false;
                }
                #endregion

                #region 正常错误处理

                JsonIsHasCase bill = JsonConvert.DeserializeObject<JsonIsHasCase>(jsonQuery);
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
                if (bill.result == null)
                    return false;
                if (bill.result.Length > 0)
                    return true;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return false;
        }

        /// <summary>
        /// 更新容器状态表信息,写入称重是所用的地牛
        /// </summary>
        /// <param name="ctCode"></param>
        /// <param name="dnCode"></param>
        /// <returns></returns>
        public bool UpdateContainerStateSetDiNiu(string ctCode, string dnCode)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("dnCode=").Append(dnCode).Append("&");
                loStr.Append("ctCode=").Append(ctCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_UpdateContainerStateSetDiNiu);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
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
        /// 更新容器状态表信息
        /// </summary>
        /// <returns></returns>
        public bool UpdateContainerStateInfo(string ctCode, string ctState, decimal grossWeight, decimal netWeight)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("ctState=").Append(ctState).Append("&");
                loStr.Append("grossWeight=").Append(grossWeight).Append("&");
                loStr.Append("netWeight=").Append(netWeight).Append("&");
                loStr.Append("ctCode=").Append(ctCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_UpdateContainerStateInfo);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
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
        /// 1:整货称重，合并 1：更新托盘状态为87  2：清空托盘 3：写入称重记录
        /// </summary>
        /// <param name="ctCode"></param>
        /// <param name="ctState"></param>
        /// <param name="grossWeight"></param>
        /// <param name="netWeight"></param>
        /// <param name="billID"></param>
        /// <param name="userCode"></param>
        /// <param name="authUserCode"></param>
        /// <param name="vhCode"></param>
        /// <returns></returns>
        public bool UpdateContainerStateInfoRecord(string ctCode, string ctState, decimal grossWeight, decimal netWeight,
            int billID, string userCode, string authUserCode, string vhCode)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                #region
                loStr.Append("ctState=").Append(ctState).Append("&");
                loStr.Append("grossWeight=").Append(grossWeight).Append("&");
                loStr.Append("netWeight=").Append(netWeight).Append("&");
                loStr.Append("billID=").Append(billID).Append("&");
                loStr.Append("userCode=").Append(userCode).Append("&");
                loStr.Append("authUserCode=").Append(authUserCode).Append("&");
                loStr.Append("vhCode=").Append(vhCode).Append("&");
                loStr.Append("ctCode=").Append(ctCode);
                #endregion
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_UpdateContainerStateInfoRecord);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
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
        /// 显示当前装车编号的车辆（与装车编号 DEBUG时会显示）
        /// </summary>
        /// <param name="ctCode"></param>
        /// <returns></returns>
        public DataTable GetBillVhNoAndVhTrainNo(string ctCode)
        {
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("VH_TRAIN_NO", Type.GetType("System.String"));
            tblDatas.Columns.Add("VH_ID", Type.GetType("System.String"));
            tblDatas.Columns.Add("VH_NO", Type.GetType("System.String"));
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("ctCode=").Append(ctCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetBillVhNoAndVhTrainNo);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetBillVhNoAndVhTrainNo bill = JsonConvert.DeserializeObject<JsonGetBillVhNoAndVhTrainNo>(jsonQuery);
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

                List<JsonVehiclesEntity> jb = new List<JsonVehiclesEntity>();
                #region 赋值
                foreach (JsonGetBillVhNoAndVhTrainNoResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["VH_TRAIN_NO"] = tm.vhTrainNo;
                    newRow["VH_ID"] = tm.vhId;
                    newRow["VH_NO"] = tm.vhNo;
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
        /// 根据车辆编号获得车辆信息
        /// </summary>
        /// <param name="vehicleNO"></param>
        /// <returns></returns>
        public VehicleEntity GetVehicleIDByNO(string vehicleNO)
        {
            VehicleEntity temp = new VehicleEntity();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("vhNo=").Append(vehicleNO);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetVehicleIDByNO);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return temp;
                }
                #endregion

                #region 正常错误处理

                JsonGetVehicleIDByNO bill = JsonConvert.DeserializeObject<JsonGetVehicleIDByNO>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return temp;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return temp;
                }
                #endregion
                List<VehicleEntity> list = new List<VehicleEntity>();
                #region 赋值数据
                foreach (JsonGetVehicleIDByNOResult jbr in bill.result)
                {
                    VehicleEntity asnEntity = new VehicleEntity();
                    asnEntity.ID = Convert.ToInt32(jbr.id);
                    asnEntity.IsActive = jbr.isActive;
                    asnEntity.RouteCode = jbr.rtCode;
                    asnEntity.UserCode = jbr.userCode;
                    asnEntity.VehicleCode = jbr.vhCode;
                    asnEntity.VehicleNO = jbr.vhNo;
                    asnEntity.VehicleVolume = jbr.vhVolume;
                    list.Add(asnEntity);
                }
                if (list.Count > 0)
                    return list[0];
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return temp;
        }

        /// <summary>
        /// 写入称重记录
        /// </summary>
        /// <returns></returns>
        public bool InsertWeightRecord(int billID, string ctCode, decimal grossWeight, decimal netWeight, string userCode, string authUserCode, string vhCode)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID).Append("&");
                loStr.Append("ctCode=").Append(ctCode).Append("&");
                loStr.Append("crossWeight=").Append(grossWeight).Append("&");
                loStr.Append("netWeight=").Append(netWeight).Append("&");
                loStr.Append("userCode=").Append(userCode).Append("&");
                loStr.Append("authUserCode=").Append(authUserCode).Append("&");
                loStr.Append("vhCode=").Append(vhCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_InsertWeightRecord);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
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
        /// 检查当前托盘是否符合预期的 称重装车顺序
        /// </summary>
        public bool CheckTuopanIsExpect(string ctCode)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("ctCode=").Append(ctCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_CheckTuopanIsExpect);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
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

        public bool ClearCtl(string ct_code, int billID)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("ctCode=").Append(ct_code).Append("&");
                loStr.Append("billID=").Append(billID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_ClearCtl);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
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
        /// 获取当前托盘内最轻商品的重量（销售单位）
        /// </summary>
        /// <returns></returns>
        public string GetCTCodeMinWeight(string ctCode, int billID)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("ctCode=").Append(ctCode).Append("&");
                loStr.Append("billId=").Append(billID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetCTCodeMinWeight);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    return null;
                }
                #endregion

                #region 正常错误处理

                JsonGetCTCodeMinWeight bill = JsonConvert.DeserializeObject<JsonGetCTCodeMinWeight>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return null;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return null;
                }
                #endregion
                if (bill.result != null && bill.result.Length > 0)
                    return bill.result[0].minWeight;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return null;
        }

        /// <summary>
        /// sql:查询散货任务关闭，并且物流箱都验证了
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="isCase"></param>
        /// <returns></returns>
        public bool JudgetContainerReversed(int billID, int isCase)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID).Append("&");
                loStr.Append("isCase=").Append(isCase);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_JudgetContainerReversed);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    return false;
                }
                #endregion

                #region 正常错误处理

                JsonJudgetContainerReversed bill = JsonConvert.DeserializeObject<JsonJudgetContainerReversed>(jsonQuery);
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

                if(bill.result != null &&bill.result.Length >0 && Convert.ToInt32(bill.result[0].counts) > 0)
                    return true;
                
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return false;
        }
        #endregion

        /// <summary>
        /// 整货称重 主流程
        /// </summary>
        private void Save()
        {
            string strWeight = lblCurrentWeight.Text; //实际称的重
            string ctCode = txtBoxForContainerCode.Text.Trim();
            decimal ctWeight = 0;
            lblCtCode.Text = ctCode;

            txtBoxForContainerCode.SelectAll();   //扫描后选择待下次扫描替换
            txtBoxForContainerCode.Focus();

            //确认读取的电子称重量是否有效
            try
            {
                realWeight = ConvertUtil.ToDecimal(strWeight);
                if (realWeight <= 0)
                {

                    lblMsg.Show("读取的电子称数据无效，必须为大于0的数值，请联系管理员。", false);
                    return;
                }

                //转换为克(g)，更精确
                realWeight = realWeight * 1000;
            }
            catch (Exception ex)
            {
                lblMsg.Show(ex.Message, false);
                return;
            }

            try
            {
                if ((!ctCode.StartsWith("3") && !ctCode.StartsWith("5")) || ctCode.Length < 6)
                {
                    lblMsg.Show("请扫描地牛或托盘！", false);
                    return;
                }

                ContainerEntity currentContainer = GetCurrentContainerInfo(ctCode);

                if (currentContainer == null)
                {
                    lblMsg.Show("未找到该容器的信息，请先维护基础信息或替换为可用条码！", false);
                    return;
                }

                if (ctCode.StartsWith("5"))
                {
                    //diNiuWeight = currentContainer.ContainerWeight;   //记录 当前使用的 地牛重量
                    CurrentDiNiu = currentContainer;                   //记录 当前使用的 地牛
                    lblMsg.Show("地牛已扫描，请继续扫描托盘。");
                    return;
                }
                else
                {
                    ctWeight = currentContainer.ContainerWeight;
                }

                #region 根据当前扫描的托盘查出与该托盘关联的订单的所有托盘列表

                //获得当前托盘所关联的订单信息
                soHeader = GetBillInfoByContainer(ctCode);
                if (soHeader == null)
                {

                    lblMsg.Show("该托盘没有相关的订单信息，请检查。", false);
                    return;
                }

                //如果查到对应的单据，就显示到界面上，无法后面的验证是否有问题
                ShowBillInfo(soHeader);

                // 如果该订单已经取消且称重记录为0，则不允许继续称重，并清空界面信息
                if (soHeader.CancelFlag == 1 && GetWeightRecordsCountByBillID(soHeader.BillID, ctCode))
                {
                    MsgBox.Warn("该订单已被取消，请停止称重！");
                    return;
                }

                //判断单据状态是否为等待称重65以及66（等待装车，有可能称重出现了问题，需要重新称，只要没有发出去就应该允许重新称重）
                if (soHeader.Status != "65" && soHeader.Status != "66" && soHeader.Status != "692")
                {
                    lblMsg.Show("该托盘所属订单的状态不是<等待称重>或<等待装车>，请检查。", false);
                    return;
                }

                //显示当前装车编号的车辆（与装车编号 DEBUG时会显示）                
                DataTable dt = GetBillVhNoAndVhTrainNo(ctCode);
                if (dt.Rows.Count == 0)
                {

                    lblMsg.Show("该托盘所属订单未查到装车信息，请先在[装车信息]里分派装车。", false);
                    return;
                }

                string currentTruckName = ConvertUtil.ToString(dt.Rows[0]["VH_NO"]);
                string currentVhTrainNo = ConvertUtil.ToString(dt.Rows[0]["VH_TRAIN_NO"]);

                if (string.IsNullOrEmpty(currentTruckName))
                {
                    lblMsg.Show("该托盘所属订单的装车信息没有指定车辆，请检查。", false);
                    return;
                }

                #region 装车编号验证(未限制装车称重顺序)

                ////如果为空（装车编号首单），显示车辆，写入this.VhTrainNo 
                //if (string.IsNullOrEmpty(this.VhTrainNo) || this.CurrentVehicle == null)
                //{
                //    lblTruckName.Text = currentTruckName;
                //    lblVhTrainNo.Text = currentVhTrainNo;
                //    this.VhTrainNo = currentVhTrainNo;
                //    this.CurrentVehicle = this.soWeightDal.GetVehicleIDByNO(currentTruckName);
                //}
                //if (currentVhTrainNo != this.VhTrainNo || currentTruckName != CurrentVehicle.VehicleNO)
                //{
                //    gridControlForDetail.DataSource = null;
                //    string msg = string.Format("订单{0}分派的车辆是[{1}]，\r\n请先完成当前车辆[{2}]所有托盘的称重。", soHeader.BillNO, currentTruckName, CurrentVehicle.VehicleNO);
                //    MsgBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}

                ////显示当前车辆(装车编号)所有订单的托盘
                //this.vhTrainNoContainers = ShowVhTrainNoContainers(this.VhTrainNo, soHeader.BillID, ctCode, realWeight);

                #endregion

                #region 装车编号验证(严格按照分派装车(二次排序)后的顺序称重装车，不符合预期，卡死)

                //检测当前 托盘 所属订单的所有托盘是否均已称重(允许再称) || 当前 托盘 的订单是否和预计的装车顺序的未完成称重的订单吻合
                //如果不符合，弹出错误提示窗口：请按照装车顺序依次完成订单的称重！

                int IsMix = Nodes.Utils.ConvertUtil.ToInt(GetSystemDiffSet("称重车辆切换"));
                //托盘所属订单是否属于当前装车编号
                if (this.VhTrainNo != currentVhTrainNo && IsMix == 0)
                { //不属于当前车辆
                    if (!string.IsNullOrEmpty(this.VhTrainNo))    //不是新的装车编号
                    { //是否切换车辆
                        string msg = string.Format("需授权确认：确定要中断当前车辆并开始【{1}】的称重装车吗?\r\n警告：托盘【{2}】所属订单【{0}】\r\n分派的车辆是【{1}】，\r\n中途切车易造成装车混乱！", soHeader.BillNO, currentTruckName, ctCode);
                        DialogResult isSure = MsgBox.AskYes(msg);
                        if (isSure == DialogResult.Yes)
                        {
                            FrmTempAuthorize fta = new FrmTempAuthorize("称重复核员");
                            if (fta.ShowDialog() != DialogResult.OK)
                            {
                                txtBoxForContainerCode.Text = string.Empty;
                                MsgBox.OK("已取消切车操作，请继续当前车的称重装车。");
                                return;
                            }
                            Insert(ELogType.装车, fta.AuthUserCode, soHeader.BillNO, "整货称重", "整货称重切车", DateTime.Now, "整货称重中途切车");
                            this.BillID = soHeader.BillID;
                            //重置装车编号与车辆 刷新所属车辆
                            ResetVhTrainNoAndShowCar(currentVhTrainNo, currentTruckName);
                            MsgBox.OK("已授权切车，请稍后完成上个车辆的称重装车！");
                        }
                        else
                        {
                            txtBoxForContainerCode.Text = string.Empty;
                            MsgBox.OK("已取消切车操作，请继续当前车的称重装车。");
                            return;
                        }
                    }
                    else
                    {
                        //新的装车编号,重置装车编号与车辆 刷新所属车辆
                        ResetVhTrainNoAndShowCar(currentVhTrainNo, currentTruckName);
                    }
                }
                else if (this.VhTrainNo != currentVhTrainNo && IsMix != 0)
                    ResetVhTrainNoAndShowCar(currentVhTrainNo, currentTruckName);

                //刷新所属车辆
                lblVhTrainNo.Text = this.VhTrainNo;
                lblTruckName.Text = this.CurrentVehicle.VehicleNO;

                //显示当前车辆(装车编号)所有订单的托盘
                this.vhTrainNoContainers = ShowVhTrainNoContainers(this.VhTrainNo, soHeader.BillID, ctCode, CurrentDiNiu, ctWeight, realWeight);

                //订单的装车顺序是否与预计的装车编号未称重最小/最大装车顺序匹配||装车编号未称重最小/最大装车顺序为NULL(全已称重订单允许再称)
                //expectResult：1,属于符合预计的托盘;-1,无关联的订单;-2,无装车编号;0,不属于符合预计的托盘
                bool expectResult = CheckTuopanIsExpect(ctCode);  //P_SO_CONTAINER_WEIGHT_EXPECT
                if (!expectResult)
                {

                    //MsgBox.Warn("此托盘所属订单不符合预期称重顺序，请按分派的装车顺序依次完成订单的称重装车！");
                    return;
                }

                #endregion

                //如果上一个单据称重没有全部完成，不能换单据
                //if (BillID != -1 && BillID != soHeader.BillID)
                //{

                //    string msg = "[上个托盘复核未通过]或[上个订单还有未称重的托盘]，\r\n请确保复核通过整个订单的所有托盘后再称此托盘！";
                //    MsgBox.Warn(msg);
                //    return;
                //}

                this.BillID = soHeader.BillID;

                //记录当前单据的所有托盘数量
                tuoPanAllNum = this.vhTrainNoContainers.Select(string.Format("BILL_ID = '{0}'", soHeader.BillID)).Length;

                //当前单据已称重托盘数量
                int weightedBillTuopanCount = this.vhTrainNoContainers.Select(string.Format("BILL_ID = '{0}' AND CT_STATE = '87'", this.BillID)).Length;

                //当前托盘是否已称重
                int currentCTState87Num = this.vhTrainNoContainers.Select(string.Format("CT_CODE = '{0}' AND CT_STATE = '87'", ctCode)).Length;

                bool isLastTuoPan = false;
                //查看是否为最后一个托盘
                //if (tuoPanAllNumByBill - tuoPanWeightedByBill.Count == 1 && !tuoPanWeightedByBill.Contains(ctCode))//此判定同单多地磅称不行
                bool flag1 = currentCTState87Num == 0 && (tuoPanAllNum - weightedBillTuopanCount <= 1);
                bool flag2 = currentCTState87Num == 1 && (tuoPanAllNum - weightedBillTuopanCount == 0);
                if (flag1 || flag2)
                {
                    isLastTuoPan = true;
                }

                //称重复核员
                string authUserCode = string.Empty;
                //获得系统设置的标准偏差  【托盘标准偏差】
                decimal diffSet = Nodes.Utils.ConvertUtil.ToDecimal(GetSystemDiffSet("托盘标准偏差"));
                if (diffSet == 0) //偏差设置为空或0
                {
                    MsgBox.Warn("系统中[托盘标准偏差]设置有误，请及时联系库管或技术人员进行处理！");
                    return;
                }

                //获取当前托盘内最轻商品的重量（销售单位）
                decimal MinWeight = Nodes.Utils.ConvertUtil.ToDecimal(GetCTCodeMinWeight(ctCode, soHeader.BillID));
                //如果最轻商品重量大于系统偏差值则使用最轻商品数量；如果小于则使用标准偏差值
                if (diffSet < MinWeight * 3 / 4)
                {
                    diffSet = MinWeight * 3 / 4;
                }



                //显示当前车辆(装车编号)所有订单的托盘
                this.vhTrainNoContainers = ShowVhTrainNoContainers(this.VhTrainNo, soHeader.BillID, ctCode, CurrentDiNiu, ctWeight, realWeight);

                //检查是否超出偏差,超出偏差，要复核
                decimal diffNow = Nodes.Utils.ConvertUtil.ToDecimal(lblDiffByPallet.Text) * 1000;
                if (Math.Abs(diffNow) >= Math.Abs(diffSet))
                {
                    lblMsg.Show("偏差过大，请复核校验！", false);

                    OpenAlarm();

                    FrmTempAuthorize fta = new FrmTempAuthorize("称重复核员");
                    if (fta.ShowDialog() != DialogResult.OK)
                    {
                        if (CurrentDiNiu != null)
                        {
                            CurrentDiNiu = null;
                            //还原为null
                            UpdateContainerStateSetDiNiu(ctCode, null);
                        }
                        MsgBox.Warn("当前托盘授权复核【未通过】，请校验商品后重新称重此托盘！");
                        return;
                    }
                    authUserCode = fta.AuthUserCode;
                }

                OpenNormal();

                //更新托盘状态和写入称重记录
                decimal netWeight = realWeight - (CurrentDiNiu == null ? 0 : CurrentDiNiu.ContainerWeight) - currentContainer.ContainerWeight;

                #region
                ////更新托盘状态为87
                //UpdateContainerStateInfo(currentContainer.ContainerCode, "87", realWeight, netWeight);
                //ClearCtl(currentContainer.ContainerCode, soHeader.BillID);

                ////写入称重记录 
                //InsertWeightRecord(soHeader.BillID, currentContainer.ContainerCode, realWeight, netWeight, GlobeSettings.LoginedUser.UserCode, authUserCode, CurrentVehicle.VehicleCode);
                #endregion

                UpdateContainerStateInfoRecord(currentContainer.ContainerCode, "87", realWeight, netWeight, soHeader.BillID, 
                    GlobeSettings.LoginedUser.UserCode, authUserCode, CurrentVehicle.VehicleCode);

                //显示当前车辆(装车编号)所有订单的托盘/物流箱
                this.vhTrainNoContainers = ShowVhTrainNoContainers(this.VhTrainNo, soHeader.BillID, ctCode, CurrentDiNiu, ctWeight, realWeight);

                //检查是否有散件【整货称重专用】
                bool hasCase2 = true;

                if (isLastTuoPan)
                {
                    //检查是否有散件
                    hasCase2 = IsHasCase(this.BillID, 2);

                    //混合仓 有散货 但 无散货任务（散已归整） 也置为66
                    bool mixCase = (GlobeSettings.LoginedUser.WarehouseType == EWarehouseType.混合仓) && (GetCountOfTaskByCase(this.BillID, 2) == 0);

                    if (!hasCase2 || mixCase)
                    {
                        //更新全整货（包括散归整）订单状态
                        UpdateCurrentBillState(this.BillID, "66");
                    }
                    if (GlobeSettings.LoginedUser.WarehouseType == EWarehouseType.混合仓)
                    {
                        //散货任务已经做完，并且物流箱都验证了
                        if (hasCase2 && GetCountOfTaskByCase(this.BillID, 2) > 0 && !JudgetContainerReversed(this.BillID, 2))
                        {
                            UpdateCurrentBillState(this.BillID, "66");
                        }
                    }


                    //写入出库单日志
                    InsertSoLog(BillID, "整货称重完成", DateTime.Now, GlobeSettings.LoginedUser.UserName);

                    lblMsg.Show("通过，订单整货称重完毕！", true);

                    this.vhTrainNoContainers = ShowVhTrainNoContainers(this.VhTrainNo, soHeader.BillID, ctCode, CurrentDiNiu, ctWeight, realWeight);

                    //初始化Bill级数据
                    InitWeightShowInfo();

                    int weightedTuoPanCount = this.vhTrainNoContainers.Select("CT_STATE = '87'").Length;

                    //当前装车编号里的托盘 全部称重完毕
                    if (weightedTuoPanCount == this.vhTrainNoContainers.Rows.Count)
                    {
                        //初始化Car级数据
                        this.VhTrainNo = null;
                        this.CurrentVehicle = null;
                        this.lblVhTrainNo.Text = string.Empty;
                        this.lblTruckName.Text = string.Concat(this.lblTruckName.Text, "(已完成)"); //称重后不清除留下做提示
                        MsgBox.OK("当前车辆的整货全部称重完毕，\r\n若有散货，请在手持[散货装车]上确认车辆物流箱。");

                    }
                }
                else  //不是最后一个托盘
                {
                    this.vhTrainNoContainers = ShowVhTrainNoContainers(this.VhTrainNo, soHeader.BillID, ctCode, CurrentDiNiu, ctWeight, realWeight);

                    //初始化TuoPan级数据
                    txtBoxForContainerCode.Text = string.Empty;
                    lblCurrentWeight.Text = "0";
                    //lblCalcWeight.Text = "0";
                    lblDiffByPallet.Text = "0";

                    lblMsg.Show("通过，请扫描当前订单的下一个托盘。", true);
                }//end if (isLastTuoPan)

                #endregion

            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }


        //地磅重量数据接收事件
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                System.Threading.Thread.Sleep(100);
                if (!serialPort1.IsOpen)
                {
                    lblMsg.Show("请先打开串口。", false);
                    return;
                }

                string str = serialPort1.ReadExisting();
                Match m = Regex.Match(str, matchRex, RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    str = m.Value.ToUpper().Replace(" ", "").Replace("S", "").Replace("KG", "");
                    if (!string.IsNullOrEmpty(str))
                    {

                        this.BeginInvoke(new DegCtrl(InvokeFunction), new object[] { str });
                    }
                }

            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private delegate void DegCtrl(string str);

        private void InvokeFunction(string str)
        {
            str = str.Replace("\r\n ", "").Replace("\r", "").Replace("\n", "");
            lblCurrentWeight.Text = str;

            Save();
        }


        //打开串口按钮单击事件
        private void OnOpenComClick(object sender, EventArgs e)
        {
            if (comboBoxEditCom.Text.Trim() == "")
            {
                lblMsg.Show("请设置串口号！", false);
                return;
            }

            if (btnOpenCom.Text == "关闭串口")
            {
                TryCloseCom();    //尝试关闭串口
            }
            else
            {
                TryOpenCom();     //尝试打开串口
            }
        }

        private void TryOpenCom()
        {
            try
            {
                if (!serialPort1.IsOpen)
                {
                    string comName = comboBoxEditCom.Text.Trim();
                    serialPort1.PortName = comName;
                    serialPort1.BaudRate = 9600;
                    serialPort1.Open();

                    //打开成功后，记录下最近一次的串口，下次启动时自动填充并尝试打开
                    Properties.Settings.Default.COMWeightNo = comName;
                    Properties.Settings.Default.Save();

                    lblComState.Text = "地磅串口打开成功；";
                    comboBoxEditCom.Enabled = false;
                    btnOpenCom.Text = "关闭串口";
                    this.txtBoxForContainerCode.Focus();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Show(ex.Message, false);
            }

            try
            {
                if (!serialPort2.IsOpen)
                {
                    serialPort2.PortName = "COM2"; //"COM2";
                    serialPort2.BaudRate = 9600;
                    serialPort2.Open();

                    lblComState.Text += "报警灯串口打开成功；";
                    timer1.Enabled = true;
                    this.txtBoxForContainerCode.Focus();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Show("报警灯：" + ex.Message, false);
            }
        }

        private void TryCloseCom()
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Close();

                    btnOpenCom.Text = "打开串口";
                    btnOpenCom.Enabled = true;
                    comboBoxEditCom.Enabled = true;
                    lblComState.Text = "地磅串口已成功关闭";
                }

                if (serialPort2.IsOpen)
                {
                    serialPort2.Close();

                    timer1.Enabled = false;
                    lblComState.Text += "报警灯串口已成功关闭";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Show(ex.Message, false);
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                serialPort2.Write("hi");
                //lblMsg.Show("HI", false);
            }
            catch
            { }
        }

        /// <summary>
        /// 异常报警
        /// </summary>
        public void OpenAlarm()
        {
            try
            {
                if (!serialPort2.IsOpen)
                    return;
                for (int i = 0; i < 3; i++)
                {
                    Thread.Sleep(300);
                    serialPort2.Write("m0c00");
                }
            }
            catch
            { }
        }

        /// <summary>
        /// 正常闪烁
        /// </summary>
        public void OpenNormal()
        {
            try
            {
                if (!serialPort2.IsOpen)
                    return;
                for (int i = 0; i < 1; i++)
                {
                    Thread.Sleep(200);
                    serialPort2.Write("m0100");
                }
            }
            catch
            { }
        }


        //显示称重界面的当前订单的相关信息
        private void ShowBillInfo(SOHeaderEntity soHeader)
        {
            lblBIllNO.Text = soHeader.BillNO;
            lblCustomerName.Text = soHeader.CustomerName;
            lblCustomerAddress.Text = soHeader.Address;
        }

        /// <summary>
        /// 查询当前装车编号所有的订单的托盘（理论重量含托盘和地牛自重）
        /// </summary>
        /// <param name="vhTrainNo"></param>
        /// <param name="loadingOrder"></param>
        /// <param name="isIncludeWLX"></param>
        /// <param name="isBulkToCase"></param>
        /// <returns></returns>
        public DataTable GetCurrentVhNoAllContainers(string vhTrainNo, string loadingOrder, bool isIncludeWLX, bool isBulkToCase)
        {
            #region
            DataTable tblDatas = new DataTable("Datas");//CALC_WEIGHT
            tblDatas.Columns.Add("IN_VH_SORT", Type.GetType("System.Int32"));
            tblDatas.Columns.Add("CT_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("BILL_ID", Type.GetType("System.String"));
            tblDatas.Columns.Add("BILL_NO", Type.GetType("System.String"));
            tblDatas.Columns.Add("SAILQTY", Type.GetType("System.String"));
            tblDatas.Columns.Add("CALC_WEIGHT", Type.GetType("System.Decimal"));
            tblDatas.Columns.Add("GROSS_WEIGHT", Type.GetType("System.Decimal"));
            tblDatas.Columns.Add("DIFF", Type.GetType("System.String"));
            tblDatas.Columns.Add("CT_STATE", Type.GetType("System.String"));
            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("vhTrainNo=").Append(vhTrainNo).Append("&");
                loStr.Append("loadingOrder=").Append(loadingOrder).Append("&");
                loStr.Append("isIncludeWLX=").Append(isIncludeWLX).Append("&");
                loStr.Append("isBulkToCase=").Append(isBulkToCase);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetCurrentVhNoAllContainers);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetCurrentVhNoAllContainers bill = JsonConvert.DeserializeObject<JsonGetCurrentVhNoAllContainers>(jsonQuery);
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

                List<JsonVehiclesEntity> jb = new List<JsonVehiclesEntity>();
                #region 赋值
                foreach (JsonGetCurrentVhNoAllContainersResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["IN_VH_SORT"] = Convert.ToInt32(tm.inVhSort);
                    newRow["CT_CODE"] = tm.ctCode;
                    newRow["BILL_ID"] = tm.billId;
                    newRow["BILL_NO"] = tm.billNo;
                    newRow["SAILQTY"] = tm.sailQty;
                    newRow["CALC_WEIGHT"] = Convert.ToDecimal(tm.calcWeight);
                    newRow["GROSS_WEIGHT"] = Convert.ToDecimal(tm.grossWeight);
                    decimal d = Convert.ToDecimal(tm.grossWeight) - Convert.ToDecimal(tm.calcWeight);
                    // 保留小数后两位 
                    d = Math.Round(d, 2);
                    newRow["DIFF"] = d;
                    newRow["CT_STATE"] = tm.ctState;
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


        //显示装车编号的所有托盘信息
        private DataTable ShowVhTrainNoContainers(string vhTrainNo, int billID, string ctCode, ContainerEntity diNiu, decimal tuoPanWeight, decimal lastWeight)
        {

            if (diNiu != null)
            {
                //记录所使用的地牛到LC_CODE 以便算到理论重量(不然重量偏差列会不准确)
                UpdateContainerStateSetDiNiu(ctCode, diNiu.ContainerCode);
            }

            string loadingOrder = ConvertUtil.ToString(GetSystemDiffSet("称重装车顺序")).Trim();

            ////显示所有与本单据关联的托盘以及重量【false表示不获取物流箱】 
            //DataTable dataContainers = this.soWeightDal.GetCurrentVhNoAllContainers(vhTrainNo, loadingOrder, false);
            DataTable dataContainers = GetCurrentVhNoAllContainers(vhTrainNo, loadingOrder, false, true);
            this.gridControlForContainer.DataSource = dataContainers;

            //定位明细行(车辆容器太多时方便查看)
            LocateContainerRow(dataContainers, ctCode);

            //显示托盘明细
            ShowTuoPanDetails(billID, ctCode);

            //显示本箱偏差(kg)
            decimal ctCalcWeight = ConvertUtil.ToDecimal(dataContainers.Compute("SUM(CALC_WEIGHT)", string.Format("CT_CODE = '{0}'", ctCode)));

            decimal diNiuAndTuoPanWeight = (diNiu == null ? 0 : diNiu.ContainerWeight / 1000) + tuoPanWeight / 1000;

            lblCalcWeight.Text = string.Format("{0:f2}={1:f2}+{2:f2}", ctCalcWeight, ctCalcWeight - diNiuAndTuoPanWeight, diNiuAndTuoPanWeight);
            lblDiffByPallet.Text = string.Format("{0:f2}", (realWeight / 1000 - ctCalcWeight));   //偏差 是 实际重量 减去 理论重量

            ////显示本单的偏差值，因为电子称显示的都是毛重，所以算的全部是毛重，这样人就更直观
            //decimal totalCalcWeight = ConvertUtil.ToDecimal(dataContainers.Compute("SUM(CALC_WEIGHT)", null));   //理论总重量
            //decimal totalHasWeight = ConvertUtil.ToDecimal(dataContainers.Compute("SUM(GROSS_WEIGHT)", null));   //已称重总重量
            //lblDiffByBill.Text = string.Format("{0:f2}-{1:f2}={2:f2}", totalHasWeight + lastWeight - ctHasWeight - wlxWeight, totalCalcWeight, totalHasWeight + lastWeight - ctHasWeight - wlxWeight - totalCalcWeight);

            return dataContainers;

        }

        //定位托盘行
        private void LocateContainerRow(DataTable dataContainers, string ctCode)
        {
            DataRow[] drs = dataContainers.Select(string.Format("CT_CODE = {0}", ctCode));
            try
            {
                int testIndex = dataContainers.Rows.IndexOf(drs[0]);
                this.gvContainers.MoveBy(testIndex);
            }
            catch { }
        }

        /// <summary>
        /// 获取订单指定托盘的记录，如果是散归整，计算整箱的重量
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="ctCode"></param>
        /// <returns></returns>
        public DataTable GetPickRecordsByCtCode(int billID, string ctCode)
        {
            #region
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("SKU_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("SKU_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("SPEC", Type.GetType("System.String"));
            tblDatas.Columns.Add("SAILQTY", Type.GetType("System.String"));
            tblDatas.Columns.Add("SAILUMNAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("WEIGHT", Type.GetType("System.String"));
            tblDatas.Columns.Add("TotalWeight", Type.GetType("System.String"));
            tblDatas.Columns.Add("USER_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("PICK_DATE", Type.GetType("System.String"));
            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billID=").Append(billID).Append("&");
                loStr.Append("ctCode=").Append(ctCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetPickRecordsByCtCode);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetPickRecordsByCtCode bill = JsonConvert.DeserializeObject<JsonGetPickRecordsByCtCode>(jsonQuery);
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

                List<JsonVehiclesEntity> jb = new List<JsonVehiclesEntity>();
                #region 赋值
                foreach (JsonGetPickRecordsByCtCodeResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["SKU_NAME"] = tm.skuName;
                    newRow["SKU_CODE"] = tm.skuCode;
                    newRow["SPEC"] = tm.spec;
                    newRow["SAILQTY"] = tm.sailQty;
                    newRow["SAILUMNAME"] = tm.sailUmName;
                    newRow["WEIGHT"] = tm.weight;
                    newRow["TotalWeight"] = tm.totalWeight;
                    newRow["USER_NAME"] = tm.userName;
                    newRow["PICK_DATE"] = tm.pickDate;
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

        //显示托盘明细
        private void ShowTuoPanDetails(int billID, string ctCode)
        {
            ////托盘拣货明细
            //this.gridControlForDetail.DataSource = this.soDal.GetPickRecordsByCtCode(billID, ctCode);

            this.gridControlForDetail.DataSource = GetPickRecordsByCtCode(billID, ctCode);

            this.gvCtDetails.ViewCaption = string.Format("托盘[{0}]的明细", ctCode);
        }

        //托盘行选中事件
        private void gvContainers_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            DataRow row = this.gvContainers.GetFocusedDataRow();
            string ctCode = Nodes.Utils.ConvertUtil.ToString(row["CT_CODE"]);
            int billId = Nodes.Utils.ConvertUtil.ToInt(row["BILL_ID"]);

            ShowTuoPanDetails(billId, ctCode);
        }

        //重置车辆与装车编号
        private void ResetVhTrainNoAndShowCar(string currentVhTrainNo, string currentTruckName)
        {
            //记录装车编号与车辆
            this.VhTrainNo = currentVhTrainNo;
            this.CurrentVehicle = GetVehicleIDByNO(currentTruckName);
        }


        /// <summary>
        /// 更新托盘状态和写入称重记录
        /// </summary>
        private void UpdateContainerStateAndInsertWeightRecord(string ctCode, string vehicleCode, string authUserCode, decimal diNiuWeight)
        {
            decimal grossWeight = Nodes.Utils.ConvertUtil.ToDecimal(lblCurrentWeight.Text) * 1000;
            decimal tuoPanWeight = GetCurrentContainerInfo(ctCode).ContainerWeight;
            decimal netWeight = grossWeight - diNiuWeight - tuoPanWeight; //净重=毛重-地牛重量-托盘重量

            //更新托盘状态表信息
            UpdateContainerStateInfo(ctCode, "87", grossWeight, netWeight);

            string userCode = GlobeSettings.LoginedUser.UserCode;

            //写入称重记录
            InsertWeightRecord(BillID, ctCode, grossWeight, netWeight, userCode, authUserCode, vehicleCode);
        }

        //初始化TuoPan级数据（每个托盘称重完毕后初始化）
        private void InitTuoPanVariables()
        {
            CurrentDiNiu = null;

            //清空部分显示内容
            txtBoxForContainerCode.Text = string.Empty;
            lblCurrentWeight.Text = "0";
            //lblCalcWeight.Text = "0";
            lblDiffByPallet.Text = "0";
        }

        //初始化Bill级数据（每单称重完毕后初始化）
        private void InitWeightShowInfo()
        {
            CurrentDiNiu = null;

            tuoPanAllNum = 0;

            //因为是最后一个，把单号置为-1
            BillID = -1;

            //初始化界面显示
            txtBoxForContainerCode.Text = string.Empty;
            //lblCtCode.Text = string.Empty;
            //lblCurrentWeight.Text = "0";
            lblCalcWeight.Text = "0";
            lblDiffByPallet.Text = "0";
            //lblDiffByBill.Text = "0";
            //lblBIllNO.Text = string.Empty;
            //lblCustomerName.Text = string.Empty;
            //lblCustomerAddress.Text = string.Empty;
            //gridControlForContainer.DataSource = null;
            //gridControlForDetail.DataSource = null;
        }

    }
}