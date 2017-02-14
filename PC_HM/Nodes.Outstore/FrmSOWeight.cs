using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Nodes.UI;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Shares;
using Nodes.SystemManage;
using Nodes.Utils;

namespace Nodes.Outstore
{
    public partial class FrmSOWeight : DevExpress.XtraEditors.XtraForm
    {
        private SODal soDal = null;
        public FrmSOWeight()
        {
            InitializeComponent();
        }

        private void OnFrmLoad(object sender, EventArgs e)
        {
            this.soDal = new SODal();
            txtBox.Focus();

#if !DEBUG
            //simpleButton1.Visible = false;
#else
            listComs.Text = "100.00";
            lblCurrentWright.Text = listComs.Text;

            listComs.TextChanged += new EventHandler(simpleButton1_Click);
#endif

            if (!string.IsNullOrEmpty(Properties.Settings.Default.COMWeightNo))
            {
                listComs.Text = Properties.Settings.Default.COMWeightNo;
                TryOpenCom();
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                WriteToSerialPort();
            }
        }

        private void WriteToSerialPort()
        {
            //先查看物流箱条码是否为空
            if (txtBox.Text.Trim() == "")
            {
                lblMsg.Show("请扫描物流箱！", false);
                txtBox.Text = String.Empty;
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
                }
#else
                SaveAndPrint();   // DEBUG 测试用
#endif
            }
            catch (Exception ex)
            {
                lblMsg.Show(ex.Message, false);
            }
        }

        private void Clear()
        {
            txtBox.Text = String.Empty;
            txtBox.Focus();
        }

        #region "读取串口称数据并保存"
        string matchRex = @"^S\s{1,2}S\s{1,10}\S{1,10}\s{1,2}kg";
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
                        this.BeginInvoke(new ShowWeight(SaveToDB), new object[] { str });
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private delegate void ShowWeight(string str);
        private void SaveToDB(string str)
        {
            str = str.Replace("\r\n ", "").Replace("\r", "").Replace("\n", "");
            lblCurrentWright.Text = str;
            SaveAndPrint();
        }

        /// <summary>
        /// 散货称重 主流程
        /// </summary>
        private void SaveAndPrint()
        {
            string strWeight = lblCurrentWright.Text;
            string ctCode = txtBox.Text.Trim();
            string customer = "", billNO = "", address = "", contact = "";
            int billID;
            int cancelFlag = 0;

            decimal weight = 0, containerWeight = 0;
            txtBox.Text = "";
            lblCtCode.Text = ctCode;

            //确认读取的电子称重量是否有效
            try
            {
                weight = ConvertUtil.ToDecimal(strWeight);
                if (weight <= 0)
                {
                    lblMsg.Show("读取的称重数据无效，必须为大于0的数值。", false);
                    return;
                }

                //转换为克，更精确
                weight = weight * 1000;
            }
            catch (Exception ex)
            {
                lblMsg.Show(ex.Message, false);
                return;
            }

            //开始处理保存到数据库
            try
            {
                //获取物流箱绑定的订单信息
                DataTable dt = this.soDal.GetSOBillMsg(ctCode);
                if (dt == null || dt.Rows.Count == 0)
                {
                    lblMsg.Show("该物流箱未绑定订单！", false);
                    return;
                }

                DataRow row = dt.Rows[0];

                //本单拣货必须全部完成才可以称重
                string billState = ConvertUtil.ToString(row["BILL_STATE"]);

                billID = ConvertUtil.ToInt(row["BILL_HEAD_ID"].ToString());
                customer = ConvertUtil.ToString(row["C_NAME"]);
                address = ConvertUtil.ToString(row["ADDRESS"]);
                contact = ConvertUtil.ToString(row["CONTACT"]);
                containerWeight = ConvertUtil.ToDecimal(row["CT_WEIGHT"]) / 1000;
                billNO = ConvertUtil.ToString(row["BILL_NO"]);
                cancelFlag = ConvertUtil.ToInt(row["CANCEL_FLAG"]);     // 取消标记
                if (cancelFlag == 1 && soDal.GetWeightRecordsCountByBillID(billID, ctCode) == 0)
                {
                    lblMsg.Show("该订单已取消，不允许再称重", false);
                    return;
                }
                lblBillNO.Text = billNO;

                DataTable dtContaner = ShowContainerSku(billID, ctCode);
                decimal netWeight = ConvertUtil.ToDecimal(dtContaner.Compute("SUM(LINE_WEIGHT)", null)) / 1000;
                lblCalcWeight.Text = string.Format("{0:f2}={1:f2}+{2:f2}", netWeight + containerWeight, netWeight, containerWeight);

                ////判断整货完毕进行称重
                //if (billState != BaseCodeConstant.SO_WAIT_WEIGHT && billState != BaseCodeConstant.SO_WAIT_LOADING)
                //{
                //    lblMsg.Show("该物流箱绑定订单拣货尚未全部完成！", false);
                //    return;
                //}

                //读取箱子的序号以及是否为最后一个箱子，还要防止重复称重
                int ctWeightIndex;
                int ctTotalCount;
                //decimal lastWeight = 0;
                int result = this.soDal.SaveCheckWeightWLX(ctCode, weight, GlobeSettings.LoginedUser.UserCode, null, out ctWeightIndex, out ctTotalCount);
                if (result == -1)
                {
                    lblMsg.Show("该物流箱未绑定订单！", false);
                    return;
                }
                if (result == -3)
                {
                    lblMsg.Show("订单散货任务尚未完成，请稍后再称重该订单的物流箱！", false);
                    return;
                }
                else if (result == 1) //表示已称重过，重量更新了，补打一张标签
                {
                    lblMsg.Show("补打标签。", true);
                    PrintLabel(ctCode, ctTotalCount, ctWeightIndex, customer, address, contact, billNO, billID, lblCurrentWright.Text);
                }
                else if (result == 2) //表示不是最后一个箱子，保存成功了，不用打印
                {
                    //什么也不用干
                }
                else if (result == 3) //最后一个箱子，需要打印标签
                {
                    PrintBill(billID, customer, address, contact, billNO);
                }
                else if (result == -2) //最后一个箱子，重量有问题，需要打印标签
                {
                    ShowBillContainers(billID, weight / 1000);

                    FrmTempAuthorize frm = new FrmTempAuthorize("称重复核员");
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        result = this.soDal.SaveCheckWeightWLX(ctCode, weight,
                            GlobeSettings.LoginedUser.UserCode, frm.AuthUserCode, out ctWeightIndex, out ctTotalCount);

                        if (result > 0)
                            PrintBill(billID, customer, address, contact, billNO);
                    }
                }
                else
                {
                    lblMsg.Show("未知错误！", false);
                }

                ShowBillContainers(billID, 0);
                lblCtCode.Text = ctCode;
                Clear();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
                txtBox.SelectAll();
            }
        }

        private void PrintLabel(string ctCode, int ctTotalCount, int ctWeightIndex,
            string customer, string address, string contact, string billNO, int BillID, string weight)
        {
            RptXT rptXT = new RptXT();
            rptXT.LPN = ctCode;
            rptXT.LPNCount = ctTotalCount;
            rptXT.LPNNum = ctWeightIndex;
            rptXT.CustomerName = customer;
            rptXT.CustomerAddress = address;
            rptXT.Customer = contact;
            rptXT.BillNO = billNO;
            rptXT.BillID = BillID;
            rptXT.Weight = weight;
            rptXT.Print();
        }

        private void PrintBill(int billID, string customer, string address, string contact, string billNO)
        {
            DataTable data = this.soDal.GetContainerWeightByBillID(billID);
            gridControl2.DataSource = data;

            for (int i = 0; i < data.Rows.Count; i++)
            {
                PrintLabel(ConvertUtil.ToString(data.Rows[i]["CT_CODE"]), data.Rows.Count, i + 1, customer, address, contact, billNO, billID,
                    string.Format("{0:f2}", data.Rows[i]["GROSS_WEIGHT"]));
                System.Threading.Thread.Sleep(100);
            }
        }
        #endregion

        #region "打开与关闭串口"
        private void OnOpenComClick(object sender, EventArgs e)
        {
            if (listComs.Text.Trim() == "")
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

        private void TryCloseCom()
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Close();

                    btnOpenCom.Text = "打开串口";
                    btnOpenCom.Enabled = true;
                    listComs.Enabled = true;
                    lblComState.Text = "串口已成功关闭";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Show(ex.Message, false);
            }
        }

        private void TryOpenCom()
        {
            try
            {
                if (!serialPort1.IsOpen)
                {
                    string comName = listComs.Text.Trim();
                    serialPort1.PortName = comName;
                    serialPort1.BaudRate = 9600;
                    serialPort1.Open();

                    //打开成功后，记录下最近一次的串口，下次启动时自动填充并尝试打开
                    Properties.Settings.Default.COMWeightNo = comName;
                    Properties.Settings.Default.Save();

                    lblComState.Text = "串口打开成功";
                    listComs.Enabled = false;
                    btnOpenCom.Text = "关闭串口";
                    this.txtBox.Focus();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Show(ex.Message, false);
            }
        }

        #endregion

        private void OnFrmClosing(object sender, FormClosingEventArgs e)
        {
            TryCloseCom();
        }

        private void gridView2_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            DataRow row = gridView2.GetFocusedDataRow();
            if (row == null)
                return;

            int billID = ConvertUtil.ToInt(row["BILL_HEAD_ID"]);
            string ctCode = ConvertUtil.ToString(row["CT_CODE"]);

            ShowContainerSku(billID, ctCode);
        }

        private DataTable ShowContainerSku(int billID, string ctCode)
        {
            //读取箱内数据
            DataTable dtContaner = this.soDal.GetContainerSKUMIX(billID, ctCode);

            gridControl1.DataSource = dtContaner;
            gridView1.BestFitColumns();

            return dtContaner;
        }

        private DataTable ShowBillContainers(int billID, decimal lsatWeight)
        {
            //显示所有与本单据关联的箱子以及重量 
            DataTable dataContainers = this.soDal.GetContainerWeightByBillID(billID);
            gridControl2.DataSource = dataContainers;

            //显示本单的偏差值，因为电子称显示的都是毛重，所以算的全部是毛重，这样人就更直观
            decimal totalCalcWeight = ConvertUtil.ToDecimal(dataContainers.Compute("SUM(CALC_WEIGHT)", null));
            decimal totalHasWeight = ConvertUtil.ToDecimal(dataContainers.Compute("SUM(GROSS_WEIGHT)", null));
            lblDiffByBill.Text = string.Format("{0:f2}-{1:f2}={2:f2}", totalHasWeight + lsatWeight, totalCalcWeight, totalHasWeight + lsatWeight - totalCalcWeight);

            return dataContainers;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.lblCurrentWright.Text = listComs.Text;   //方便测试lblCurrentWright
        }
    }
}