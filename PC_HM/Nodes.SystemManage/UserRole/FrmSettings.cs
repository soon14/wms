using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net;
using Nodes.UI;
using Nodes.Utils;
using Nodes.Shares;

namespace Nodes.SystemManage
{
    /// <summary>
    /// 登录界面的设置界面
    /// </summary>
    public partial class FrmSettings : DevExpress.XtraEditors.XtraForm
    {
        #region 变量
        private string _serverIP = string.Empty;
        #endregion

        #region 构造函数

        public FrmSettings()
        {
            InitializeComponent();
        }

        #endregion

        /// <summary>
        /// 限制输入的字符
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtServerIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            //数字0~9所对应的keychar为48~57，小数点是46，Backspace是8  
            e.Handled = true;
            //输入0-9和Backspace del 有效  
            if ((e.KeyChar >= 47 && e.KeyChar <= 58) || e.KeyChar == 8 || e.KeyChar == 46)
            {
                e.Handled = false;
            }
        }
        /// <summary>
        /// 保存IP地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                IPAddress ipAddress = IPAddress.None;
                if (!IPAddress.TryParse(this.txtServerIP.Text.Trim(), out ipAddress))
                {
                    MsgBox.Warn("请输入正确的IP地址！");
                    return;
                }
                GlobeSettings.ServerIP = ipAddress.ToString();
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        #region Override Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.txtServerIP.Text = GlobeSettings.ServerIP;
        }
        #endregion
    }
}
