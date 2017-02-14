using System;
using System.ComponentModel;
using System.Windows.Forms;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Shares;
using Nodes.SystemManage;
using Nodes.Utils;
using Nodes.UI;
using Nodes.Stock;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Stock;
using Newtonsoft.Json;

namespace Nodes.Tools
{
    class MainApplicationContext : ApplicationContext
    {
        #region 变量

        BackgroundWorker loginWorker;
        FrmLogin frmLogin;
        string usercode = null;
        string password = null;
        bool shouldRun = true;
        bool hasException = false;
        Exception exception;
        UserEntity user = null;
        UserDal userDal = new UserDal();
        //UpdateUtil updateUtil = new UpdateUtil();
        string appId = "wms";

        #endregion

        #region 属性

        /// <summary>
        /// Gets if the main application should run.
        /// </summary>
        public bool ShouldRun
        {
            get
            {
                return this.shouldRun;
            }
        }

        #endregion

        #region 构造函数

        public MainApplicationContext()
        {
            loginWorker = new System.ComponentModel.BackgroundWorker();
            loginWorker.DoWork += OnDoWork;
            loginWorker.RunWorkerCompleted += OnWorkerCompleted;

            RunInstance();
        }

        #endregion

        void RunInstance()
        {
            frmLogin = new FrmLogin();
            frmLogin.LoginEvent += DoClickEvent;
            if (DialogResult.OK == frmLogin.ShowDialog())
            {
                // 判断当前是否有更新
                //if (updateUtil.HasUpdate(appId))
                //{
                //    // 如果强制更新，不提示用户直接更新
                //    if (!string.IsNullOrEmpty(updateUtil.LocalVersion.WH_CODE) && 
                //        updateUtil.LocalVersion.WH_CODE.IndexOf(user.WarehouseCode) > -1 && 
                //        ((updateUtil.LocalVersion != null && updateUtil.LocalVersion.UPDATE_FLAG == 1) ||
                //        MsgBox.AskOK("系统当前有更新，是否更新系统？") == DialogResult.OK))
                //    {
                //        updateUtil.UpdateNow();
                //        this.shouldRun = false;
                //        this.ExitThread();
                //        return;
                //    }
                //}
                FrmToolMain frmMain = new FrmToolMain();
                frmMain.FormClosed += OmMainFormClosed;

                frmMain.Show();
                frmMain.Activate();
            }
            else
            {
                this.shouldRun = false;
                this.ExitThread();
            }
        }

        /// <summary>
        /// 获取一个用户的详细信息
        /// </summary>
        /// <param name="USER_ID"></param>
        /// <returns></returns>
        public UserEntity GetUserInfo(string userCode)
        {
            UserEntity list = new UserEntity();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("userCode=").Append(userCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetUserInfo);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetUserInfo bill = JsonConvert.DeserializeObject<JsonGetUserInfo>(jsonQuery);
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

                foreach (JsonGetUserInfoResult tm in bill.result)
                {
                    #region 0-10
                    list.AllowEdit = tm.allowEdit;
                    list.BranchCode = tm.branchCode;
                    list.CenterWarehouseCode = tm.centerWhCode;
                    list.IsActive = tm.isActive;
                    list.IsCenter = Convert.ToInt32(tm.isCenterWh);
                    list.IsOwn = tm.isOwn;
                    list.MobilePhone = tm.mobilePhone;
                    list.UserPwd = tm.pwd;
                    list.Remark = tm.remark;
                    list.LastUpdateBy = tm.updateBy;
                    #endregion

                    #region 11-15
                    list.UserCode = tm.userCode;
                    list.UserName = tm.userName;
                    list.WarehouseCode = tm.whCode;
                    list.WarehouseName = tm.whName;
                    #endregion

                    if (!string.IsNullOrEmpty(tm.updateDate))
                        list.LastUpdateDate = Convert.ToDateTime(tm.updateDate);
                }
                return list;
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
                //MsgBox.Err(ex.Message);
            }
            return list;
        }

        private void OnDoWork(object sender, DoWorkEventArgs e)
        {
            hasException = false;
            user = null;

            try
            {
                user = GetUserInfo(usercode);
            }
            catch (Exception ex)
            {
                hasException = true;
                exception = ex;
            }
        }

        private void OnWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            frmLogin.SetEnable(true);

            if (user == null)
            {
                if (hasException)
                    MsgBox.Warn(exception.Message);
                else
                    MsgBox.Warn("帐号不存在。");
            }
            else if (user.UserPwd != password)
            {
                MsgBox.Warn("密码错误。");
            }
            else if (user != null)
            {
                //检验是否被注销登录
                if (user.IsActive == "N")
                    MsgBox.Warn("该帐号已被禁用，无法登录。");
                else
                {
                    user.IPAddress = IPUtil.GetLocalIP();
                    GlobeSettings.LoginedUser = user;
                    
                    //LoginLogEntiy LoginLog = new LoginLogEntiy();
                    //LoginLog.UserCode = user.UserCode;
                    //LoginLog.IP = user.IPAddress;
                    //LoginLog.LoginDate = System.DateTime.Now;
                    //LoginLog.LoginType = "登录";
                    //userDal.InsertLoginLog(LoginLog);

                    //登录成功后，记住用户名和密码
                    frmLogin.SaveMe();

                    frmLogin.DialogResult = DialogResult.OK;
                }
            }
        }

        private void DoClickEvent(object sender, EventArgs e)
        {
            password = SecurityUtil.MD5Encrypt(frmLogin.Password);
            usercode = frmLogin.User;

            loginWorker.RunWorkerAsync();
        }

        private void OmMainFormClosed(object sender, System.EventArgs e)
        {
            //userDal.InsertLoginLog(
            //    new LoginLogEntiy()
            //    {
            //        IP = user.IPAddress,
            //        UserCode = user.UserCode,
            //        LoginDate = System.DateTime.Now,
            //        LoginType = "退出"
            //    });

            this.ExitThread();
        }
    }
}