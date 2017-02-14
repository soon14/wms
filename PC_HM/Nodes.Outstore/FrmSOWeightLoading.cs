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
    public partial class FrmSOWeightLoading : DevExpress.XtraEditors.XtraForm
    {
        //private SODal soDal = null;
        //private SOWeightDal soWeightDal = null;
        //private VehicleDal vehicleDal = null;

        private string VhTrainNo = null;                        //当前装车任务的编号（整车称重完毕后清空）
        private VehicleEntity CurrentVehicle = null;                    //当前装车任务的车辆（整车称重完毕后清空）        
        private DataTable vhTrainNoContainers = null;           //当前车辆所有托盘列表

        private int BillID = -1;
        private SOHeaderEntity soHeader = null;

        private string matchRex = @"^S\s{1,2}S\s{1,10}\S{1,10}\s{1,2}kg";

        private decimal realWeight = 0;           //存实际称重        
        //private decimal diNiuWeight = 0;           //（每个托盘）扫描的地牛重量(每托称完后清0)
        private ContainerEntity CurrentDiNiu = null;             //（称重托盘时）使用的地牛
        private decimal wLXsWeight = 0;          //每批物流箱总重量

        private int tuoPanAllNum = 0;                           //当前订单所有托盘数  
        private int wuLiuXiangAllNum = 0;                       //当前订单所有物流箱数
        //private int wuLiuXiangRealAllNum = 0;                    //已扫描的全部物流箱    （已改为根据状态判断是否已扫描）
        //private List<string> groupTPList = new List<string>();     //（每批/每单）已称重的托盘        
        private List<string> groupWLXList = new List<string>(); //（每批）已扫描的物流箱

        //private List<ContainerEntity> ContainerList = new List<ContainerEntity>();    //存放当前订单所有容器


        public FrmSOWeightLoading()
        {
            InitializeComponent();
        }

        private void OnFrmLoad(object sender, EventArgs e)
        {
            //this.soDal = new SODal();
            //this.soWeightDal = new SOWeightDal();
            //this.vehicleDal = new VehicleDal();

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
                    TryOpenCom();
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

                //this.gridControlForContainer.DataSource = this.ContainerList;

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
                        Insert(ELogType.装车, GlobeSettings.LoginedUser.UserName, soHeader.BillNO, "装车称重关闭界面", "装车称重", DateTime.Now, "装车称重中途关闭界面");
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
                lblMsg.Show("请扫描地牛或物流箱或托盘！", false);
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

        #region save
        /// <summary>
        /// 根据当前扫描的容器(物流箱或托盘)查出与该容器关联的订单信息
        /// </summary>
        /// <param name="containerCode"></param>
        /// <returns></returns>
        public SOHeaderEntity GetBillInfoByContainer(string containerCode)
        {
            SOHeaderEntity tm = new SOHeaderEntity();
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
                    return tm;
                }
                #endregion

                #region 正常错误处理

                JsonGetBillInfoByContainer bill = JsonConvert.DeserializeObject<JsonGetBillInfoByContainer>(jsonQuery);
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
            return tm;
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
        /// 检测订单物流箱是否全部在电子称上称重
        /// </summary>
        public bool IsWeightedAllWLXByBillID(int billID)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_IsWeightedAllWLXByBillID);
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
        #endregion

        /// <summary>
        /// 获取本组物流箱重量
        /// </summary>
        /// <param name="wlxList"></param>
        /// <returns></returns>
        public decimal GetWLXsGrossWeight(List<string> wlxList)
        {
            decimal ret = -1;
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                #region jsons 
                string jsons = string.Empty;
                foreach (string tmp in wlxList)
                {
                    jsons += tmp + ",";
                }
                jsons = jsons.Substring(0, jsons.Length - 1);
                #endregion
                loStr.Append("ctCodes=").Append(jsons);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetWLXsGrossWeight);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return ret;
                }
                #endregion

                #region 正常错误处理

                JsonGetWLXsGrossWeight bill = JsonConvert.DeserializeObject<JsonGetWLXsGrossWeight>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return ret;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return ret;
                }
                #endregion

                if(bill.result != null && bill.result.Length > 0)
                    return StringToDecimal.GetTwoDecimal(bill.result[0].sumWeight);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return ret;
        }

        /// <summary>
        /// 获取指定类型、非指定状态的容器个数
        /// </summary>
        public int GetNumOfContainer(int billID, string ctType, string ctState)
        {
            int ret = -1;
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID).Append("&");
                loStr.Append("ctType=").Append(ctType).Append("&");
                loStr.Append("ctState=").Append(ctState);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetNumOfContainer);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return ret;
                }
                #endregion

                #region 正常错误处理

                JsonGetNumOfContainer bill = JsonConvert.DeserializeObject<JsonGetNumOfContainer>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return ret;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return ret;
                }
                #endregion

                if (bill.result != null && bill.result.Length > 0)
                    return Convert.ToInt32(bill.result[0].counts);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return ret;
        }


        /// <summary>
        /// 更新目标物流箱的状态
        /// </summary>
        /// <param name="containerCode"></param>
        /// <param name="stateValue"></param>
        /// <returns></returns>
        public bool UpdateWuliuxiangState(string wuliuxiangCode, string stateValue)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("wuliuxiangCode=").Append(wuliuxiangCode).Append("&");
                loStr.Append("stateValue=").Append(stateValue);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_UpdateWuliuxiangState);
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

        //装车称重 (整散一起称重) 主流程
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

                //转换为克，更精确
                realWeight = realWeight * 1000;
            }
            catch (Exception ex)
            {
                lblMsg.Show(ex.Message, false);
                return;
            }

            try
            {

                if ((!ctCode.StartsWith("3") && !ctCode.StartsWith("2") && !ctCode.StartsWith("5")) || ctCode.Length < 6)
                {
                    lblMsg.Show("请扫描地牛或物流箱或托盘！", false);
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
                    //diNiuWeight = currentContainer.ContainerWeight;   //记录 当前使用的 地牛
                    CurrentDiNiu = currentContainer;                   //记录 当前使用的 地牛
                    lblMsg.Show("地牛已扫描，请继续扫描托盘。");
                    return;
                }
                else
                {
                    ctWeight = currentContainer.ContainerWeight;
                }

                #region 根据当前扫描的容器(物流箱或托盘)查出与该容器关联的订单的所有容器列表

                //获得当前容器所关联的订单信息
                soHeader = GetBillInfoByContainer(ctCode);
                if (soHeader == null)
                {
                    lblMsg.Show("该容器没有相关的订单信息，请检查。", false);
                    return;
                }

                //1、如果查到对应的单据，就显示到界面上，无法后面的验证是否有问题
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

                //如果订单有物流箱，但未全部称重，卡住并提示 【混合仓验证物流箱是否均已称重】               
                if (!IsWeightedAllWLXByBillID(soHeader.BillID))
                {
                    ShowTuoPanDetails(soHeader.BillID, currentContainer.ContainerCode);
                    MsgBox.Warn("当前订单还有物流箱未在电子称上称重，\r\n请先完成该订单的散货称重再称重此订单的托盘！");
                    return;
                }

                //显示当前装车编号的车辆（与装车编号 DEBUG时会显示）                
                DataTable dt = GetBillVhNoAndVhTrainNo(ctCode);
                if (dt.Rows.Count == 0)
                {

                    lblMsg.Show("该托盘所属订单未查到装车信息，请先在[装车信息]里分派装车。", false);
                    return;
                }

                string currentVhTrainNo = ConvertUtil.ToString(dt.Rows[0]["VH_TRAIN_NO"]);
                string currentTruckName = ConvertUtil.ToString(dt.Rows[0]["VH_NO"]);

                if (string.IsNullOrEmpty(currentTruckName))
                {
                    lblMsg.Show("该托盘所属订单的装车信息没有指定车辆，请检查。", false);
                    return;
                }


                #region 装车编号验证(严格按照分派装车(二次排序)后的顺序称重装车，不符合预期，卡死)

                //检测当前 托盘 所属订单的所有托盘是否均已称重(允许再称) || 当前 托盘 的订单是否和预计的装车顺序的未完成称重的订单吻合
                //如果不符合，弹出错误提示窗口：请按照装车顺序依次完成订单的称重！

                //托盘所属订单是否属于当前装车编号
                if (this.VhTrainNo != currentVhTrainNo)
                { //不属于当前车辆

                    if (!string.IsNullOrEmpty(this.VhTrainNo))    //不是新的装车编号
                    { //是否切换车辆

                        string msg = string.Format("需授权确认：确定要中断当前车辆并开始【{1}】的称重装车吗?\r\n\r\n警告：托盘【{2}】所属订单【{0}】\r\n分派的车辆是【{1}】，中途切车易造成装车混乱！！", soHeader.BillNO, currentTruckName, ctCode);
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

                            Insert(ELogType.装车, fta.AuthUserCode, soHeader.BillNO, "装车称重中途切车", "装车称重", DateTime.Now, "装车称重中途切车");
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

                //刷新所属车辆
                lblVhTrainNo.Text = this.VhTrainNo;
                lblTruckName.Text = this.CurrentVehicle.VehicleNO;

                //显示当前车辆(装车编号)所有订单的托盘/物流箱
                this.vhTrainNoContainers = ShowVhTrainNoContainers(this.VhTrainNo, soHeader.BillID, ctCode, CurrentDiNiu, ctWeight, 0, realWeight);

                //订单的装车顺序是否与预计的装车编号未称重最小/最大装车顺序匹配||装车编号未称重最小/最大装车顺序为NULL(全已称重订单允许再称)
                //expectResult：1,属于符合预计的托盘;-1,无关联的订单;-2,无装车编号;0,不属于符合预计的托盘
                bool expectResult = CheckTuopanIsExpect(ctCode);  //P_SO_CONTAINER_WEIGHT_EXPECT
                if (!expectResult)
                {
                    //MsgBox.Warn("此托盘所属订单不符合预期称重顺序，请按照装车顺序依次完成订单的称重装车！");
                    return;
                }

                #endregion


                //如果上一个单据称重没有全部完成，不能换单据
                if (BillID != -1 && BillID != soHeader.BillID)
                {
                    MsgBox.Warn("[上个托盘复核未通过]或[上个订单还有未称重的容器]，\r\n请确保整个订单的所有容器均复核通过后再称此托盘！");
                    return;
                }

                //：2、查到单据后，把相关联的【状态不为892的】物流箱全部取出来，显示到界面上，然后再做验证      
                this.BillID = soHeader.BillID;
                //DataTable dtContainers = ShowBillContainers(soHeader.BillID, ctCode, realWeight / 1000, wLXsWeight / 1000);

                //记录该单据所有托盘和物流箱的数量
                tuoPanAllNum = this.vhTrainNoContainers.Select(string.Format("BILL_ID = {0} AND CT_TYPE = '50'", this.BillID)).Length;
                wuLiuXiangAllNum = this.vhTrainNoContainers.Select(string.Format("BILL_ID = {0} AND CT_TYPE = '51'", this.BillID)).Length;

                //当前单据已称重托盘数量
                int weightedBillTuopanCount = this.vhTrainNoContainers.Select(string.Format("BILL_ID = '{0}' AND CT_STATE = '87' AND CT_TYPE = '50'", this.BillID)).Length;

                //托盘状态为87的数量
                int currentCTState87Num = this.vhTrainNoContainers.Select(string.Format("CT_CODE = '{0}' AND CT_STATE = '87'", ctCode)).Length;

                bool isLastTuoPan = false;       //每单最后一个托盘
                bool isLastWuLiuXiang = false;    //最后一个物流箱（针对全散单或整货全缺）

                //托盘 还是 物流箱【混合仓 托盘/物流箱】
                bool isPallet = ctCode.StartsWith("3");

                //称重复核员
                string authUserCode = string.Empty;

                #region 托盘

                //如果是托盘，验证是否有偏差，更新重量；若是最后一个托盘，需要更新单据状态
                if (isPallet)
                {
                    //获得系统设置的标准偏差  【托盘标准偏差】
                    decimal diffSet = Nodes.Utils.ConvertUtil.ToDecimal(GetSystemDiffSet("托盘标准偏差"));
                    if (diffSet == 0) //偏差设置为空或0
                    {
                        MsgBox.Warn("系统中[托盘标准偏差]设置有误，请及时联系库管或技术人员进行处理！");
                        return;
                    }

                    //获取本组物流箱重量
                    if (groupWLXList.Count != 0)
                    {
                        wLXsWeight = GetWLXsGrossWeight(groupWLXList);
                    }

                    //显示当前车辆(装车编号)所有订单的托盘/物流箱
                    this.vhTrainNoContainers = ShowVhTrainNoContainers(this.VhTrainNo, soHeader.BillID, ctCode, CurrentDiNiu, ctWeight, wLXsWeight, realWeight);

                    //检查是否超出偏差,超出偏差，要复核
                    decimal diffNow = Nodes.Utils.ConvertUtil.ToDecimal(lblDiffByPallet.Text) * 1000;
                    if (Math.Abs(diffNow) > Math.Abs(diffSet))
                    {
                        lblMsg.Show("重量偏差过大！请检查。", false);

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

                    decimal netWeight = realWeight - (CurrentDiNiu == null ? 0 : CurrentDiNiu.ContainerWeight) - currentContainer.ContainerWeight;

                    //更新托盘状态为87
                   UpdateContainerStateInfo(currentContainer.ContainerCode, "87", realWeight, netWeight);

                    //写入称重记录 
                    InsertWeightRecord(soHeader.BillID, currentContainer.ContainerCode, realWeight, netWeight, GlobeSettings.LoginedUser.UserCode, authUserCode, CurrentVehicle.VehicleCode);

                    //刷新容器列表
                    //显示当前车辆(装车编号)所有订单的托盘/物流箱
                    this.vhTrainNoContainers = ShowVhTrainNoContainers(this.VhTrainNo, soHeader.BillID, ctCode, CurrentDiNiu, ctWeight, wLXsWeight, realWeight);

                    //查看是否为最后一个托盘
                    //if (tuoPanAllNumByBill - tuoPanWeightedByBill.Count == 1 && !tuoPanWeightedByBill.Contains(ctCode))//此判定同单多地磅称不行
                    bool flag1 = currentCTState87Num == 0 && (tuoPanAllNum - weightedBillTuopanCount <= 1);
                    bool flag2 = currentCTState87Num == 1 && (tuoPanAllNum - weightedBillTuopanCount == 0);
                    if (flag1 || flag2)
                    {
                        isLastTuoPan = true;
                    }

                    if (!isLastTuoPan)
                    { //不是订单最后一个托盘

                        //初始化托盘级变量
                        InitTuoPanVariables();

                        lblMsg.Show("称重通过，请扫描当前订单的下一个容器。", true);
                    }
                    else
                    { //订单最后一个托盘

                        bool allWLXWeighted = false;    //订单若有物流箱，是否已全部二次称重(状态892)
                        allWLXWeighted = GetNumOfContainer(soHeader.BillID, "51", "892") == 0;
                        if (!allWLXWeighted)
                        {
                            MsgBox.Warn("当前订单的物流箱有遗漏，请找齐后重新扫描称重！");
                            return;
                        }
                        else
                        {
                            //更新订单状态
                            UpdateCurrentBillState(BillID, "66");
                            //写入出库单日志
                            InsertSoLog(BillID, "称重完成", DateTime.Now, GlobeSettings.LoginedUser.UserName);

                            lblMsg.Show("称重通过，订单称重完毕！", true);

                            this.vhTrainNoContainers = ShowVhTrainNoContainers(this.VhTrainNo, soHeader.BillID, ctCode, CurrentDiNiu, ctWeight, wLXsWeight, realWeight);

                            //初始化Bill级数据
                            InitWeightShowInfo();

                            //检查整车是否称重完毕
                            CheckCarIsFinished();
                        }
                    }


                }
                #endregion 托盘

                #region 物流箱
                else  //是物流箱
                {
                    //此处【订单排序后已把全散单置为等待称重】
                    //检查单据状态是否为等待拣配计算，这个状态极有可能是全部散件造成的，如果状态为等待称重，不用判断是否有散件了
                    //do sth. 全部为散件要把单据状态改为等待称重

                    //更新物流箱状态                        
                    UpdateWuliuxiangState(currentContainer.ContainerCode, "892");

                    //写入物流箱称重记录
                    InsertWeightRecord(soHeader.BillID, currentContainer.ContainerCode, currentContainer.GrossWeight, currentContainer.NetWeight, GlobeSettings.LoginedUser.UserCode, null, CurrentVehicle.VehicleCode);

                    //加入列表，待最后托盘扫描时加上重量
                    if (!groupWLXList.Contains(currentContainer.ContainerCode))
                    {
                        groupWLXList.Add(currentContainer.ContainerCode);//物流箱
                    }

                    //当前单据已二次称重的物流箱数量
                    int secondWeightedBillWLXCount = this.vhTrainNoContainers.Select(string.Format("BILL_ID = '{0}' AND CT_STATE = '892' AND CT_TYPE = '51'", this.BillID)).Length;

                    //查看是否为 全散单 最后一个物流箱，若是最后一个物流箱，把BillID置为-1，wLXsWeight=0 isLastWuLiuXiang
                    if (tuoPanAllNum == 0 && wuLiuXiangAllNum - secondWeightedBillWLXCount == 0)  //最后一个物流箱（针对全散单或整货全缺）
                    {
                        isLastWuLiuXiang = true;
                    }

                    if (!isLastWuLiuXiang)
                    {
                        lblMsg.Show("请扫描当前订单的下一个容器。", true);
                        return;
                    }
                    else
                    {
                        //更新订单状态
                        UpdateCurrentBillState(BillID, "66");
                        //写入出库单日志
                        InsertSoLog(BillID, "称重完成", DateTime.Now, GlobeSettings.LoginedUser.UserName);

                        lblMsg.Show("称重通过，订单称重完毕！", true);
                        //初始化数据
                        InitWeightShowInfo();

                        //检查整车是否称重完毕
                        CheckCarIsFinished();

                    }//end if (isLastWuLiuXiang) 

                }
                #endregion 物流箱

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
                TryCloseCom();
            }
            else
            {
                TryOpenCom();
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
                    serialPort2.PortName = "COM2";
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

        //显示装车编号的所有容器信息
        private DataTable ShowVhTrainNoContainers(string vhTrainNo, int billID, string ctCode, ContainerEntity diNiu, decimal tuoPanWeight, decimal wLXsWeight, decimal realWeight)
        {
            if (diNiu != null && ctCode.StartsWith("3"))
            {
                //记录所使用的地牛到托盘的LC_CODE 以便算到理论重量(不然重量偏差列会不准确)
                UpdateContainerStateSetDiNiu(ctCode, diNiu.ContainerCode);
            }

            string loadingOrder = ConvertUtil.ToString(GetSystemDiffSet("称重装车顺序")).Trim();

            ////显示所有与本单据关联的容器以及重量 【true表示获取物流箱】 
            //DataTable dataContainers = this.soWeightDal.GetCurrentVhNoAllContainers(vhTrainNo, loadingOrder, true);
            DataTable dataContainers = GetCurrentVhNoAllContainers(vhTrainNo, loadingOrder, true, true);
            this.gridControlForContainer.DataSource = dataContainers;

            //定位明细行(车辆容器太多时方便查看)
            LocateContainerRow(dataContainers, ctCode);

            //显示托盘明细
            ShowTuoPanDetails(billID, ctCode);

            //显示本箱偏差(kg)
            decimal ctCalcWeight = ConvertUtil.ToDecimal(dataContainers.Compute("SUM(CALC_WEIGHT)", string.Format("CT_CODE = '{0}'", ctCode)));

            decimal diNiuAndTuoPanWeight = (diNiu == null ? 0 : diNiu.ContainerWeight / 1000) + tuoPanWeight / 1000;
            decimal expectWeight = wLXsWeight / 1000 + ctCalcWeight;    //预估重量：地牛 + 托盘 + 物流箱 + 货品

            lblCalcWeight.Text = string.Format("{0:f2}={1:f2}+{2:f2}", expectWeight, expectWeight - diNiuAndTuoPanWeight, diNiuAndTuoPanWeight);//
            lblDiffByPallet.Text = string.Format("{0:f2}", (realWeight / 1000 - expectWeight));   //偏差 是 实际重量 减去 预估重量

            ////显示本单的偏差值，因为电子称显示的都是毛重，所以算的全部是毛重，这样人就更直观
            //decimal totalCalcWeight = ConvertUtil.ToDecimal(dataContainers.Compute("SUM(CALC_WEIGHT)", null));   //理论总重量
            //decimal totalHasWeight = ConvertUtil.ToDecimal(dataContainers.Compute("SUM(GROSS_WEIGHT)", null));   //已称重总重量
            //lblDiffByBill.Text = string.Format("{0:f2}-{1:f2}={2:f2}", totalHasWeight + lastWeight - ctHasWeight - wlxWeight, totalCalcWeight, totalHasWeight + lastWeight - ctHasWeight - wlxWeight - totalCalcWeight);

            return dataContainers;
        }


        //定位容器行
        private void LocateContainerRow(DataTable dataContainers, string ctCode)
        {
            DataRow[] drs = dataContainers.Select(string.Format("CT_CODE = '{0}'", ctCode));
            try
            {
                int testIndex = dataContainers.Rows.IndexOf(drs[0]);
                this.gvContainers.MoveBy(testIndex);
            }
            catch { }
        }

        //显示容器明细
        private void ShowTuoPanDetails(int billID, string ctCode)
        {
            //托盘拣货明细
            this.gridControlForDetail.DataSource = GetPickRecordsByCtCode(billID, ctCode);
            this.gvCtDetails.ViewCaption = string.Format("容器[{0}]的明细", ctCode);
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

        //容器行选中事件
        private void gvContainers_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            DataRow row = this.gvContainers.GetFocusedDataRow();
            string ctCode = Nodes.Utils.ConvertUtil.ToString(row["CT_CODE"]);
            int billId = Nodes.Utils.ConvertUtil.ToInt(row["BILL_ID"]);
            //根据选择行的托盘获得该单该托盘明细
            this.gridControlForDetail.DataSource = GetPickRecordsByCtCode(billId, ctCode);
            this.gvCtDetails.ViewCaption = string.Format("托盘[{0}]的明细", ctCode);
        }

        /// <summary>
        /// 根据车辆编号获得车辆信息
        /// </summary>
        /// <param name="vehicleNO"></param>
        /// <returns></returns>
        public VehicleEntity GetVehicleIDByNO(string vehicleNO)
        {
            VehicleEntity tm = new VehicleEntity();
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
                    return tm;
                }
                #endregion

                #region 正常错误处理

                JsonGetVehicleIDByNO bill = JsonConvert.DeserializeObject<JsonGetVehicleIDByNO>(jsonQuery);
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
            return tm;
        }

        //重置车辆与装车编号
        private void ResetVhTrainNoAndShowCar(string currentVhTrainNo, string currentTruckName)
        {
            //记录装车编号与车辆
            this.VhTrainNo = currentVhTrainNo;
            this.CurrentVehicle = GetVehicleIDByNO(currentTruckName);
        }

        //检查整车是否称重完毕
        private void CheckCarIsFinished()
        {
            int weightedTuoPanCount = this.vhTrainNoContainers.Select("(CT_TYPE='50' AND CT_STATE = '87') OR (CT_TYPE='51' AND CT_STATE = '892')").Length;

            //当前装车编号里的托盘 全部称重完毕
            if (weightedTuoPanCount == this.vhTrainNoContainers.Rows.Count)
            {
                //初始化Car级数据
                this.VhTrainNo = null;
                this.CurrentVehicle = null;
                this.lblVhTrainNo.Text = string.Empty;
                this.lblTruckName.Text = string.Concat(this.lblTruckName.Text, "(已完成)"); //称重后不清除留下做提示
                MsgBox.OK("当前车辆的托盘和物流箱全部称重完毕。");
            }
        }

        //初始化TuoPan级数据（每个托盘称重完毕后初始化）
        private void InitTuoPanVariables()
        {
            CurrentDiNiu = null;

            //每批物流箱总重量清0
            wLXsWeight = 0;
            //每组物流箱初始化
            groupWLXList = new List<string>();

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

            //每批物流箱总重量清0
            wLXsWeight = 0;
            //每组物流箱初始化
            groupWLXList = new List<string>();

            wuLiuXiangAllNum = 0;
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