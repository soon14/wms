using System;
using System.Windows.Forms;
using Nodes.UI;
//using Nodes.DBHelper;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.SystemManage;
using Newtonsoft.Json;

namespace Nodes.SystemManage
{
    public partial class FrmTempAuthorize : DevExpress.XtraEditors.XtraForm
    {
        private string RoleName;
        //private UserDal userDal;
        public string AuthUserCode = string.Empty;

        public FrmTempAuthorize(string roleName)
        {
            InitializeComponent();

            RoleName = roleName;
            //userDal = new UserDal();
            labelControl1.Text = string.Format("当前操作需要得到角色“{0}”的授权。", roleName);
        }

        private void OnOKClick(object sender, EventArgs e)
        {
            OkPressed();
        }

        private void OnUserKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.IsEntryComplete())
                {
                    this.OkPressed();
                }
                else
                {
                    if (String.IsNullOrEmpty(this.txtID.Text.Trim()))
                    {
                        this.ShowUserRequired();
                    }
                    else
                    {
                        this.txtPwd.Focus();
                    }
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
            }

        }

        private void OnPasswordKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.IsEntryComplete())
                {
                    this.OkPressed();
                }
                else
                {
                    if (String.IsNullOrEmpty(this.txtPwd.Text.Trim()))
                    {
                        this.ShowPasswordRequired();
                    }
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void ShowUserRequired()
        {
            if (!this.Disposing)
            {
                MsgBox.Warn("请输入工号、姓名或姓名拼音的头字母。");
                this.txtID.Focus();
            }
        }

        /// <summary>
        /// 根据用户身份信息，查看是否由此权限
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="pwd"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public string TempAuthorize(string userCode, string pwd, string roleName)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("userCode=").Append(userCode).Append("&");
                loStr.Append("password=").Append(pwd).Append("&");
                loStr.Append("roleName=").Append(roleName);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_TempAuthorize);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return null;
                }
                #endregion

                #region 正常错误处理

                JsonTempAuthorize bill = JsonConvert.DeserializeObject<JsonTempAuthorize>(jsonQuery);
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
                    return bill.result[0].userCode;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return null;
        }

        private void OkPressed()
        {
            if (this.txtID.Text.Trim().Length == 0)
            {
                this.ShowUserRequired();
                return;
            }

            if (String.IsNullOrEmpty(this.txtPwd.Text.Trim()))
            {
                this.ShowPasswordRequired();
                return;
            }

            try
            {
                string ret = TempAuthorize(txtID.Text.Trim(), txtPwd.Text.Trim(), this.RoleName);
                if (!string.IsNullOrEmpty(ret))
                {
                    AuthUserCode = ret;
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MsgBox.Warn("工号或密码有误、或该用户已被注销、或没有权限，若确认工号密码均无误，请联系管理员分配相关的权限。");
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// Determines if based on the current configuration has everything that is required been filed in.
        /// </summary>
        /// <returns>true or false</returns>
        private bool IsEntryComplete()
        {
            bool returnValue = false;

            if (!String.IsNullOrEmpty(this.txtID.Text.Trim()) && !String.IsNullOrEmpty(this.txtPwd.Text.Trim()))
            {
                returnValue = true;
            }

            return returnValue;
        }

        public void ShowBadUserPassword()
        {
            if (!this.Disposing)
            {
                //this.Reset();

                this.txtPwd.Text = string.Empty;
                this.txtPwd.Focus();
                MsgBox.Warn("工号或密码错误。");
            }
        }

        private void ShowPasswordRequired()
        {
            if (!this.Disposing)
            {
                MsgBox.Warn("请输入口令。");
                this.txtPwd.Focus();
            }
        }
    }
}