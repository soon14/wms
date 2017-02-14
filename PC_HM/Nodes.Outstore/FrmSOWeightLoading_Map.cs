using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Nodes.UI;
using Nodes.DBHelper;
using Nodes.Shares;
using Nodes.SystemManage;
using Nodes.Utils;
using System.Text.RegularExpressions;
using System.Text;
using Nodes.Entities;

namespace Nodes.Outstore
{
    public partial class FrmSOWeightLoading_Map : DevExpress.XtraEditors.XtraForm
    {
        private VehicleDal vehicleDal = null;
        private SODal soDal = null;
        private SOWeightDal soWeightDal = null;

        private string matchRex = @"^S\s{1,2}S\s{1,10}\S{1,10}\s{1,2}kg";
        private int BillID = -1;
        private decimal realWeight = 0;    //存实际称重
        private SOHeaderEntity soHeader = null;   //当前托盘所属订单
        private int tuoPanAllNumByBill = 0;  //当前订单所有托盘数  
        private List<string> tuoPanWeightedByBill = new List<string>(); //当前订单已称重的托盘        
        private List<string> tuoPanWeightedByCar = new List<string>(); //当前车次已称重的托盘
        private DataTable vehicleTripContainers = null;    //车次所有托盘列表

        public FrmSOWeightLoading_Map()
        {
            InitializeComponent();
        }

        private void OnFrmLoad(object sender, EventArgs e)
        {
            try
            {
                this.soDal = new SODal();
                this.soWeightDal = new SOWeightDal();
                this.vehicleDal = new VehicleDal();
                txtBoxForContainerCode.Focus();

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

                LoadDataAndBindGrid();    //绑定车辆信息
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void OnFrmClosing(object sender, FormClosingEventArgs e)
        {
            TryCloseCom();    //尝试关闭串口
        }

        private void FrmSOWeight_Shown(object sender, EventArgs e)
        {
            txtBoxForContainerCode.Focus();
        }

        /// <summary>
        /// 尝试打开串口
        /// </summary>
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

        /// <summary>
        /// 绑定车辆信息
        /// </summary>
        public void LoadDataAndBindGrid()
        {
            try
            {
                bindingSource1.DataSource = this.vehicleDal.GetAll();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 尝试关闭串口
        /// </summary>
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

        /// <summary>
        /// 车次整货称重主流程
        /// </summary>
        private void Save()
        {
            string strWeight = lblCurrentWeight.Text; //实际称的重
            string ctCode = txtBoxForContainerCode.Text.Trim();
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
                if (lookUpEditForVehicles.Text == "")
                {
                    lblMsg.Show("请选择车辆。", false);
                    return;
                }

                if (!ctCode.StartsWith("30") || ctCode == "" || ctCode.Length <= 2)
                {
                    lblMsg.Show("请扫描托盘！", false);
                    return;
                }

                //先刷新一下最新托盘列表
                ReloadContainers();

                //获得当前托盘所关联的订单信息
                soHeader = this.soWeightDal.GetBillInfoByContainer(ctCode);
                if (soHeader == null)
                {
                    lblMsg.Show("该托盘没有相关的订单信息，请检查。", false);
                    return;
                }

                //如果查到对应的单据，就显示到界面上，无法后面的验证是否有问题
                ShowBillInfo(soHeader);

                //判断单据状态是否为等待称重65以及66（等待装车，有可能称重出现了问题，需要重新称，只要没有发出去就应该允许重新称重）
                if (soHeader.Status != "65" && soHeader.Status != "66")
                {
                    lblMsg.Show("该托盘所属订单的状态不是等待称重或等待装车，请检查。", false);
                    return;
                }

                //获得当前托盘相关的 车次（如"SO2015081801050858"）
                string tuoPanVehicleNo = this.soWeightDal.GetVehicleTripByCurrentContainer(ctCode);
                if (tuoPanVehicleNo == null)
                {
                    lblMsg.Show("该托盘没有相关车次信息，请检查。", false);
                    return;
                }

                string displayVehicleNo = this.lblVehicleNo.Text.Trim();     //界面显示的车次
                if (displayVehicleNo.Length < 16)  //（首单的车次）车次称重完毕后需要设置为空
                {
                    lblVehicleNo.Text = displayVehicleNo = tuoPanVehicleNo;
                    this.lookUpEditForVehicles.Enabled = false;
                    //显示当前车次所有散货的预估件数
                    lblBulkNum.Text = ConvertUtil.ToString(this.soWeightDal.GetSoBillIdsByVehicleNo(displayVehicleNo));
                }
                this.vehicleTripContainers = ShowVehicleTripContainers(tuoPanVehicleNo, soHeader.BillID, ctCode, realWeight / 1000);

                if (tuoPanVehicleNo != displayVehicleNo)  //是否同车次
                {
                    lblMsg.Show(string.Format("托盘所属订单的车次[{0}]不在当前车次内，请检查。", tuoPanVehicleNo), false);
                    return;
                }

                //如果上一个单据称重没有全部完成，不能换单据
                if (BillID != -1 && BillID != soHeader.BillID)
                {
                    lblMsg.Show("上一个单据未完成称重，请检查。", false);
                    return;
                }

                this.BillID = soHeader.BillID;

                //当前车辆信息
                VehicleEntity vehicle = this.soWeightDal.GetVehicleIDByNO(Nodes.Utils.ConvertUtil.ToString(lookUpEditForVehicles.EditValue));
                //获得系统设置的标准偏差
                decimal diffSet = Nodes.Utils.ConvertUtil.ToDecimal(soWeightDal.GetSystemDiffSet("物流箱标准偏差"));
                //称重复核员
                string authUserCode = string.Empty;
                //检查是否有散件
                bool hasCase2 = true;

                //记录当前单据的所有托盘数量
                //tuoPanAllNum = this.vehicleTripContainers.Select("CT_TYPE = 50").Length;
                tuoPanAllNumByBill = this.vehicleTripContainers.Select(string.Format("BILL_ID = '{0}'", soHeader.BillID)).Length;

                bool isLastTuoPan = false;
                //查看是否为最后一个托盘
                if (tuoPanAllNumByBill - tuoPanWeightedByBill.Count == 1 && !tuoPanWeightedByBill.Contains(ctCode))
                {
                    isLastTuoPan = true;
                }

                if (isLastTuoPan)
                {
                    //检查是否超出偏差,超出偏差，要复核
                    decimal diffNow = Nodes.Utils.ConvertUtil.ToDecimal(lblDiffByPallet.Text) * 1000;
                    if (Math.Abs(diffNow) > diffSet)
                    {
                        lblMsg.Show("重量偏差过大！请检查。", false);
                        FrmTempAuthorize fta = new FrmTempAuthorize("称重复核员");
                        if (fta.ShowDialog() != DialogResult.OK)
                        {
                            return;
                        }
                        authUserCode = fta.AuthUserCode;
                    }

                    CheckUpdateAndLog(authUserCode, ctCode, vehicle.VehicleCode, tuoPanVehicleNo);

                    //检查是否有散件
                    hasCase2 = this.soWeightDal.IsHasCase2(this.BillID);
                    if (!hasCase2)
                    {
                        //更新全整货订单状态
                        this.soWeightDal.UpdateCurrentBillState(this.BillID, vehicle.ID, "66");
                    }
                    else
                    {
                        //只关联车辆
                        this.soWeightDal.UpdateCurrentBillState(this.BillID, vehicle.ID);
                    }

                    //写入出库单日志
                    this.soWeightDal.InsertSoLog(this.BillID, "整货称重完毕", DateTime.Now, GlobeSettings.LoginedUser.UserName);

                    lblMsg.Show("通过，订单称重完毕！", true);

                    //检验已称重托盘并循环判断是否重复,不重复的加入当前车次已称重托盘                    
                    foreach (string item in tuoPanWeightedByBill)
                    {
                        if (!tuoPanWeightedByCar.Contains(item))
                        {
                            tuoPanWeightedByCar.Add(item);
                        }
                    }

                    //当前车次的托盘 全部称重完毕
                    if (tuoPanWeightedByCar.Count == vehicleTripContainers.Rows.Count)
                    {
                        MessageBox.Show("当前车次的整货全部称重完毕，请在手持[散货装车]上确认车辆物流箱。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lookUpEditForVehicles.Enabled = true;
                        //初始化Car级数据
                        lookUpEditForVehicles.EditValue = null;
                        lblVehicleNo.Text = string.Empty;
                        lblBulkNum.Text = "0";
                        tuoPanWeightedByCar = new List<string>();
                    }

                    //初始化Bill级数据
                    InitWeightShowInfo();
                }
                else  //不是最后一个托盘
                {
                    //检查是否超出偏差,超出偏差，要复核
                    decimal diffNow = Nodes.Utils.ConvertUtil.ToDecimal(lblDiffByPallet.Text) * 1000;
                    if (Math.Abs(diffNow) > diffSet)
                    {
                        lblMsg.Show("重量偏差过大！请检查。", false);
                        FrmTempAuthorize fta = new FrmTempAuthorize("称重复核员");
                        if (fta.ShowDialog() != DialogResult.OK)
                        {
                            return;
                        }
                        authUserCode = fta.AuthUserCode;
                    }

                    CheckUpdateAndLog(authUserCode, ctCode, vehicle.VehicleCode, tuoPanVehicleNo);

                    //初始化TuoPan级数据
                    txtBoxForContainerCode.Text = string.Empty;
                    lblCurrentWeight.Text = "0";
                    //lblCalcWeight.Text = "0";
                    lblDiffByPallet.Text = "0";

                    lblMsg.Show("通过，请扫描当前订单的下一个托盘。", true);
                }//end if (isLastTuoPan)

            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

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

        /// <summary>
        /// 托盘编码框txtBoxForContainerCode的事件
        /// </summary>
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                WriteToSerialPort();  //向电子称串口发送指令
            }
        }

        /// <summary>
        /// 向电子称串口发送指令
        /// </summary>
        private void WriteToSerialPort()
        {
            //先查看托盘条码是否为空
            if (txtBoxForContainerCode.Text.Trim() == "")
            {
                lblMsg.Show("请扫描托盘！", false);
                txtBoxForContainerCode.Text = String.Empty;
                return;
            }

            //回车后，先给电子称串口发送指令“S\r\n"
            try
            {
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
                //Save();//测试用
            }
            catch (Exception ex)
            {
                lblMsg.Show(ex.Message, false);
            }
        }

        /// <summary>
        /// 打开/关闭 串口按钮
        /// </summary>
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

        /// <summary>
        /// 模拟重量 按钮（测试）
        /// </summary>
        private void btnWeightForTest_Click(object sender, EventArgs e)
        {
            lblCurrentWeight.Text = "400.00";
        }

        private void lookUpEditForVehicles_EditValueChanged(object sender, EventArgs e)
        {
            this.txtBoxForContainerCode.Focus();
            this.gridControlForContainer.DataSource = null;
            this.gridControlForDetail.DataSource = null;
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

        //异常报警
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

        //正常闪烁
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

        /// <summary>
        /// 托盘列选中事件
        /// </summary>
        private void gvContainers_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            DataRow row = this.gvContainers.GetFocusedDataRow();
            string ctCode = Nodes.Utils.ConvertUtil.ToString(row["CT_CODE"]);
            int billId = Nodes.Utils.ConvertUtil.ToInt(row["BILL_ID"]);
            //根据选择行的托盘获得该单该托盘明细
            this.gridControlForDetail.DataSource = soDal.GetPickRecordsByCtCode(billId, ctCode);
        }

        ContainerEntity SelectedContainer
        {
            get
            {
                if (this.gvContainers.FocusedRowHandle < 0)
                    return null;
                else
                    return gvContainers.GetFocusedRow() as ContainerEntity;
            }
        }

        /// <summary>
        /// 显示与订单相关的托盘信息与理论总量与偏差
        /// </summary>
        private DataTable ShowVehicleTripContainers(string vehicleNo, int billID, string ctCode, decimal lastWeight)
        {
            //显示所有与本单据关联的箱子以及重量 
            DataTable dataContainers = this.soWeightDal.GetCurrentVehicleTripAllPallets(vehicleNo);
            this.gridControlForContainer.DataSource = dataContainers;

            //托盘拣货明细
            gridControlForDetail.DataSource = this.soDal.GetPickRecordsByCtCode(billID, ctCode);

            //显示本箱偏差(kg)
            decimal ctCalcWeight = ConvertUtil.ToDecimal(dataContainers.Compute("SUM(CALC_WEIGHT)", string.Format("CT_CODE = '{0}'", ctCode)));
            //decimal ctHasWeight = ConvertUtil.ToDecimal(dataContainers.Compute("SUM(GROSS_WEIGHT)", string.Format("CT_CODE = '{0}'", ctCode)));

            lblCalcWeight.Text = string.Format("{0:f2}", ctCalcWeight);                //理论重量
            lblDiffByPallet.Text = string.Format("{0:f2}", lastWeight - ctCalcWeight);   //偏差 是 实际重量 减去 理论重量

            ////显示本单的偏差值，因为电子称显示的都是毛重，所以算的全部是毛重，这样人就更直观
            //decimal totalCalcWeight = ConvertUtil.ToDecimal(dataContainers.Compute("SUM(CALC_WEIGHT)", null));   //理论总重量
            //decimal totalHasWeight = ConvertUtil.ToDecimal(dataContainers.Compute("SUM(GROSS_WEIGHT)", null));   //已称重总重量
            //lblDiffByBill.Text = string.Format("{0:f2}-{1:f2}={2:f2}", totalHasWeight + lastWeight - ctHasWeight - wlxWeight, totalCalcWeight, totalHasWeight + lastWeight - ctHasWeight - wlxWeight - totalCalcWeight);

            return dataContainers;
        }

        /// <summary>
        /// 显示称重界面的当前订单的相关信息
        /// </summary>
        private void ShowBillInfo(SOHeaderEntity soHeader)
        {
            lblBIllNO.Text = soHeader.BillNO;
            lblCustomerName.Text = soHeader.CustomerName;
            lblCustomerAddress.Text = soHeader.Address;
        }

        /// <summary>
        /// 更新托盘状态和写入称重记录
        /// </summary>
        private void UpdateContainerStateAndInsertWeightRecord(string ctCode, string vehicleCode, string authUserCode)
        {
            decimal grossWeight = Nodes.Utils.ConvertUtil.ToDecimal(lblCurrentWeight.Text) * 1000;
            decimal ctWeight = this.soWeightDal.GetCurrentContainerInfo(ctCode).ContainerWeight;
            decimal netWeight = grossWeight - ctWeight;
            //更新托盘状态表信息
            this.soWeightDal.UpdateContainerStateInfo(ctCode, "87", vehicleCode, grossWeight, netWeight);

            string userCode = GlobeSettings.LoginedUser.UserCode;
            //写入称重记录
            this.soWeightDal.InsertWeightRecord(BillID, ctCode, grossWeight, netWeight, userCode, authUserCode, vehicleCode);
        }

        /// <summary>
        /// 初始化Bill级数据（每单称重完毕后初始化）
        /// </summary>
        private void InitWeightShowInfo()
        {
            //重置托盘数量
            tuoPanAllNumByBill = 0;
            tuoPanWeightedByBill = new List<string>();

            //因为是最后一个，把单号置为-1
            this.BillID = -1;

            //初始化界面显示
            txtBoxForContainerCode.Text = string.Empty;
            //lblCtCode.Text = string.Empty;
            lblCurrentWeight.Text = "0";
            //lblCalcWeight.Text = "0";
            lblDiffByPallet.Text = "0";
        }

        /// <summary>
        /// 刷新托盘列表按钮(当前车次所有单据的托盘列表)
        /// </summary>
        private void ReloadContainers()
        {
            string vehicleNo = this.lblVehicleNo.Text.Trim();     //界面显示的车次编号
            if (vehicleNo.Length < 16)
            {
                lblMsg.Show("当前车次暂未正确关联！", false);
                return;
            }
            this.vehicleTripContainers = this.soWeightDal.GetCurrentVehicleTripAllPallets(vehicleNo);
            this.gridControlForContainer.DataSource = vehicleTripContainers;
        }

        /// <summary>
        /// 提取的方法：更新托盘状态、更新订单状态、称重日志等
        /// </summary>
        private void CheckUpdateAndLog(string authUserCode, string ctCode, string vehicleCode, string tuoPanVehicleNo)
        {
            if (!tuoPanWeightedByBill.Contains(ctCode))
            {
                //记录称重托盘
                tuoPanWeightedByBill.Add(ctCode);
            }

            //更新托盘状态和写入称重记录
            UpdateContainerStateAndInsertWeightRecord(ctCode, vehicleCode, authUserCode);

            //ShowBillContainers(this.BillID, ctCode, weight / 1000, 0);
            this.vehicleTripContainers = ShowVehicleTripContainers(tuoPanVehicleNo, this.BillID, ctCode, realWeight / 1000);
        }

    }
}