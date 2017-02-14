using System;
using System.Windows.Forms;
using Nodes.Utils;
using DevExpress.Skins;
using Nodes.UI;
using Nodes.Resources;
using System.Runtime.InteropServices;

namespace Nodes.SystemManage
{
    public partial class FrmLogin : Form
    {
        public event EventHandler LoginEvent;

        /// <summary>
        /// Gets or sets the text currently in the user textbox.
        /// </summary>
        public string User
        {
            get { return txtID.Text.Trim(); }
        }

        /// <summary>
        /// Gets the text currently in the password textbox
        /// </summary>
        public string Password
        {
            get { return txtPwd.Text.Trim().ToLower(); }
        }

        public bool RememberMe
        {
            get
            {
                //return checkRememberMe.Checked;
                return false;
            }
        }

        public FrmLogin()
        {
            //读取设置
            Properties.Settings _setting = Properties.Settings.Default;
            
            //按照用户保存的设置改变皮肤
            SkinUtil.RegisterSkin(System.IO.Path.Combine(Application.StartupPath, "skins.dll"));
            SkinUtil.SetSkin(_setting.Skin);
            SkinManager.EnableFormSkins();
            SkinUtil.SetFont(_setting.FontFamily, _setting.FontSize);

            InitializeComponent();

            txtID.Text = _setting.Usercode;
            this.btnSettings.Image = AppResource.GetIcon(AppResource.EIcons.settings);
            //txtPwd.Text = SecurityUtil.Base64ToString(_setting.password);
            //checkRememberMe.Checked = _setting.RememberMe;
        }

        private void OnCancel(object sender, EventArgs e)
        {
            this.Close();
        }

        #region "处理登录"
        private void ShowUserRequired()
        {
            if (!this.Disposing)
            {
                this.txtID.Focus();
                MsgBox.Warn("请填写帐号。");
            }
        }

        private void ShowPasswordRequired()
        {
            if (!this.Disposing)
            {
                txtPwd.Focus();
                MsgBox.Warn("请填写密码。");
            }
        }

        public void SetFocusToPwd()
        {
            txtPwd.SelectAll();
            txtPwd.Focus();
        }

        public void SaveMe()
        {
            //登录成功后，记住用户名和密码
            Properties.Settings.Default.Usercode = User;
            Properties.Settings.Default.RememberMe = RememberMe;
            if (RememberMe)
                Properties.Settings.Default.password = SecurityUtil.StringToBase64(Password);
            else
                Properties.Settings.Default.password = "";

            Properties.Settings.Default.Save();
        }

        public void SetEnable(bool enable)
        {
            if (!this.Disposing)
            {
                layoutControlItem4.ContentVisible = loadingCircle1.Active = !enable;
                Application.DoEvents();

                txtID.Enabled = enable;
                txtPwd.Enabled = enable;
                btnOK.Enabled = enable;
            }
        }

        private bool IsFieldValueValid()
        {
            if (string.IsNullOrEmpty(User))
            {
                ShowUserRequired();
                return false;
            }

            if (string.IsNullOrEmpty(Password))
            {
                ShowPasswordRequired();
                return false;
            }

            return true;
        }

        [DllImport("kernel32.dll", EntryPoint = "GetSystemDefaultLCID")]
        public static extern int GetSystemDefaultLCID();
        [DllImport("kernel32.dll", EntryPoint = "SetLocaleInfoA")]
        public static extern int SetLocaleInfo(int Locale, int LCType, string lpLCData);
        public const int LOCALE_SLONGDATE = 0x20;
        public const int LOCALE_SSHORTDATE = 0x1F;
        public const int LOCALE_STIME = 0x1003;

        public void SetDateTimeFormat()
        {
            try
            {
                int x = GetSystemDefaultLCID();
                SetLocaleInfo(x, LOCALE_STIME, "HH:mm:ss");        //时间格式  
                SetLocaleInfo(x, LOCALE_SSHORTDATE, "yyyy-MM-dd");   //短日期格式    
                SetLocaleInfo(x, LOCALE_SLONGDATE, "yyyy-MM-dd");   //长日期格式   
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
            }
        }  


        private void OnBtnOKClick(object sender, EventArgs e)
        {
            SetDateTimeFormat();
            DoLogin();
        }

        private void DoLogin()
        {
            if (IsFieldValueValid())
            {
                SetEnable(false);

                if (LoginEvent != null)
                {
                    LoginEvent(this, EventArgs.Empty);
                }
            }
        }

        private void OnUserKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(User))
                {
                    ShowUserRequired();
                }
                else if (string.IsNullOrEmpty(Password))
                {
                    txtPwd.Focus();
                }
                else
                {
                    DoLogin();
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void OnPasswordKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(Password))
                {
                    ShowPasswordRequired();
                }
                else if (string.IsNullOrEmpty(User))
                {
                    txtID.Focus();
                }
                else
                {
                    this.DoLogin();
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            loadingCircle1.Active = false;
            loadingCircle1.Dispose();
        }

        #endregion

        private void txtID_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
            {
                txtID.Text = "";
                txtPwd.Text = "";

                Properties.Settings.Default.Usercode = "";
                Properties.Settings.Default.password = "";
                Properties.Settings.Default.Save();
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            using (FrmSettings frmSettings = new FrmSettings())
            {
                frmSettings.ShowDialog();
            }
        }
    }
}
