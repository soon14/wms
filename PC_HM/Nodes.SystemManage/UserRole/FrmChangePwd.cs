using System;
using System.Windows.Forms;
using Nodes.DBHelper;
using Nodes.Utils;
using Nodes.Shares;
using Nodes.UI;

namespace Nodes.SystemManage
{
    public partial class FrmChangePwd : DevExpress.XtraEditors.XtraForm
    {
        public FrmChangePwd()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!IsFieldValueValid())
                return;

            try
            {
                UserDal userDal = new UserDal();
                string pwd = SecurityUtil.MD5Encrypt(txtNewPwd.Text.Trim().ToLower());
                int ret = userDal.ChangePassword(GlobeSettings.LoginedUser.UserCode, pwd);

                //一定要记得更新全局变量的密码
                if (ret == 1)
                {
                    GlobeSettings.LoginedUser.UserPwd = pwd;
                    MsgBox.OK("密码修改成功，请牢记新密码。");
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MsgBox.Warn("没有查到您的信息，请确认是否已经被其他人删除。");
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private bool IsFieldValueValid()
        {
            string currPwd = txtCurrentPwd.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(currPwd))
            {
                txtCurrentPwd.Focus();
                MsgBox.Warn("请重新填写您的当前密码。");
                return false;
            }

            currPwd = SecurityUtil.MD5Encrypt(currPwd);
            if (!currPwd.Equals(GlobeSettings.LoginedUser.UserPwd))
            {
                txtCurrentPwd.Focus();
                MsgBox.Warn("当前密码填写不正确，请重新填写您的当前密码。");
                return false;
            }

            if (txtNewPwd.Text.Trim().Length == 0)
            {
                txtNewPwd.Focus();
                MsgBox.Warn("密码不允许为空字符，请填写新密码。");
                return false;
            }

            if (!txtNewPwd.Text.Trim().Equals(txtPwdAgain.Text.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                txtNewPwd.Text = txtPwdAgain.Text = string.Empty;
                txtNewPwd.Focus();
                MsgBox.Warn("新密码与确认密码填写不一致，请重新输入。");
                return false;
            }

            return true;
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkShow.Checked)
            {
                txtCurrentPwd.Properties.PasswordChar = '\0';
                txtNewPwd.Properties.PasswordChar = '\0';
                txtPwdAgain.Properties.PasswordChar = '\0';
            }
            else
            {
                txtCurrentPwd.Properties.PasswordChar = '*';
                txtNewPwd.Properties.PasswordChar = '*';
                txtPwdAgain.Properties.PasswordChar = '*';
            }
        }
    }
}