using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nodes.UI;
using System.Text.RegularExpressions;
using Nodes.DBHelper;
using Nodes.Shares;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.BaseData;
using Newtonsoft.Json;

namespace Nodes.BaseData
{
    /// <summary>
    /// 容器重量维护
    /// </summary>
    public partial class FrmContainerMaintain : DevExpress.XtraEditors.XtraForm
    {
        #region 常量
        private static readonly string[] BUTTON_STR_ARRAY = { "关闭串口", "打开串口" };
        private const string MATCH_REX = @"^S\s{1,2}S\s{1,10}\S{1,10}\s{1,2}kg";
        #endregion

        #region 变量

        private ContainerDal _containerDal = new ContainerDal();
        private ContainerEntity _entity = null;
        private decimal _newWeight = 0.00m;
        
        #endregion

        #region 构造函数

        public FrmContainerMaintain()
        {
            InitializeComponent();
        }

        #endregion

        #region 委托
        private delegate void DegCtrl(string str);
        #endregion

        #region 方法
        private void TryOpenCom()
        {
            try
            {
                if (!this.serialPort1.IsOpen)
                {
                    string comName = comboBoxEditCom.Text.Trim();
                    this.serialPort1.PortName = comName;
                    this.serialPort1.BaudRate = 9600;
                    this.serialPort1.Open();

                    //打开成功后，记录下最近一次的串口，下次启动时自动填充并尝试打开
                    Properties.Settings.Default.COMWeightNo = comName;
                    Properties.Settings.Default.Save();

                    //lblComState.Text = "地磅串口打开成功；";
                    comboBoxEditCom.Enabled = false;
                    btnOpenCom.Text = BUTTON_STR_ARRAY[0];
                    this.txtContainerCode.Focus();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err("错误:" + ex.Message);
            }
        }
        private void TryCloseCom()
        {
            try
            {
                if (this.serialPort1.IsOpen)
                {
                    serialPort1.Close();

                    this.btnOpenCom.Text = BUTTON_STR_ARRAY[1];
                    this.btnOpenCom.Enabled = true;
                    this.comboBoxEditCom.Enabled = true;
                    //this.lblComState.Text = "地磅串口已成功关闭";
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err("错误:" + ex.Message);
            }
        }
        private void InvokeFunction(string str)
        {
            str = str.Replace("\r\n ", "").Replace("\r", "").Replace("\n", "");
            this._newWeight = ConvertUtil.ToDecimal(str) * 1000;
            this.lblActualWeight.Text = string.Format("{0} 克", this._newWeight);
        }

        /// <summary>
        /// string转换decimal 得到2位小数点 
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private decimal GetTwoDecimal(string num)
        {
            string ret = num;
            if (ret.Contains("."))
                ret = ret.Insert(ret.Length, "00");
            else
                ret = ret.Insert(ret.Length, ".00");
            return Math.Round(Convert.ToDecimal(ret), 2);
        }

        /// <summary>
        /// 根据托盘编号找到对应的托盘
        /// </summary>
        /// <param name="code">托盘编号</param>
        /// <returns></returns>
        public ContainerEntity GetContainerByCode(string code, string warehouseCode)
        {
           ContainerEntity list = new ContainerEntity();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("whCode=").Append(warehouseCode).Append("&");
                loStr.Append("ctCode=").Append(code);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetContainerByCode);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetAllContainer bill = JsonConvert.DeserializeObject<JsonGetAllContainer>(jsonQuery);
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
                foreach (JsonGetAllContainerResult jbr in bill.result)
                {
                    #region
                    list.ContainerCode = jbr.ctCode;
                    list.ContainerName = jbr.ctName;
                    list.ContainerType = jbr.ctType;
                    list.ContainerTypeDesc = jbr.ctTypeDesc;
                    list.ContainerWeight = StringToDecimal.GetTwoDecimal(jbr.ctWeight);
                    list.IsDelete = Convert.ToInt32(jbr.isDeleted);
                    #endregion
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

        private void ShowContainerInfo()
        {
            string ctCode = this.txtContainerCode.Text.Trim();
            string warehouse = GlobeSettings.LoginedUser.WarehouseCode;
            
            this._entity = GetContainerByCode(ctCode, warehouse);
            if (this._entity == null) return;
            this.lblContainerType.Text = this._entity.ContainerTypeDesc;
            this.lblCurrentWeight.Text = string.Format("{0:f0} 克", this._entity.ContainerWeight);
        }
        private void ClearData()
        {
            this.txtContainerCode.Text = string.Empty;
            this.lblContainerType.Text = string.Empty;
            this.lblCurrentWeight.Text = string.Empty;
            this.lblActualWeight.Text = string.Empty;
            this._entity = null;
            this._newWeight = 0.00m;
            this.txtContainerCode.Focus();
        }
        #endregion

        #region 事件

        /// <summary>
        /// 打开串口
        /// </summary>
        private void btnOpenCom_Click(object sender, EventArgs e)
        {
            if (this.comboBoxEditCom.Text.Trim() == "")
            {
                MsgBox.Warn("请设置串口号！");
                return;
            }
            if (this.btnOpenCom.Text == BUTTON_STR_ARRAY[0])
            {
                this.TryCloseCom();
            }
            else
            {
                this.TryOpenCom();
            }
        }
        /// <summary>
        /// 接收数据
        /// </summary>
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                System.Threading.Thread.Sleep(100);
                if (!serialPort1.IsOpen)
                {
                    MsgBox.Warn("请先打开串口。");
                    return;
                }

                string str = serialPort1.ReadExisting();
                Match m = Regex.Match(str, MATCH_REX, RegexOptions.IgnoreCase);
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

        /// <summary>
        /// 基础管理（容器信息-更新容器重量）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public bool UpdateWeight(ContainerEntity entity, decimal weight)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("ctWeight=").Append(weight).Append("&");
                loStr.Append("ctCode=").Append(entity.ContainerCode).Append("&");
                loStr.Append("whCode=").Append(entity.WarehouseCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_UpdateWeight);
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
        /// 提交重量
        /// </summary>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.serialPort1.IsOpen)
                {
                    MsgBox.Warn("请先打开串口!");
                    return;
                }
                if (this._entity == null)
                {
                    MsgBox.Warn("无效的容器!");
                    return;
                }
                if (MsgBox.AskOK("是否确认修改?") == DialogResult.OK)
                {
                    this._entity.WarehouseCode = GlobeSettings.LoginedUser.WarehouseCode;
                    if (UpdateWeight(this._entity, this._newWeight))
                    {
                        this.ClearData();
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err("错误:" + ex.Message);
            }
        }
        /// <summary>
        /// 显示容器信息
        /// </summary>
        private void txtContainerCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.ShowContainerInfo();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err("错误:" + ex.Message);
            }
        }
        /// <summary>
        /// 用于测试
        /// </summary>
        private void btnTest_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(new DegCtrl(InvokeFunction), new object[] { "18" });
        }
        #endregion

        #region Override Methods
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            this.TryCloseCom();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.ClearData();
        }
        #endregion
    }
}
