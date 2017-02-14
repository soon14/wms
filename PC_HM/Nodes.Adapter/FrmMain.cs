using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nodes.Utils;
using Nodes.Net;
using System.Threading;

namespace Nodes.Adapter
{
    public partial class FrmMain : Form
    {
        #region 构造函数

        public FrmMain()
        {
            InitializeComponent();
        }

        #endregion

        #region Override Methods
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            try
            {
#if !DEBUG
                SystemUtil.SetAutoRun(true);
#endif
                this.tsmiLocalIP.Text = string.Format("本机IP地址：{0}", AppContext.LocalIPAddress);
                ThreadPool.QueueUserWorkItem((item) => 
                {
                    //if (AppContext.HttpServer == null)
                    //{
                    //    AppContext.HttpServer = new Nodes.Net.HttpServer(AppContext.LocalURL);
                    //}
                    //AppContext.HttpServer.RequestReceivedDataEvent -= new ReceivedDataHandler(HttpServer_ReceivedDataEvent);
                    //AppContext.HttpServer.RequestReceivedDataEvent += new ReceivedDataHandler(HttpServer_ReceivedDataEvent);
                    //AppContext.HttpServer.Start();
                    //this.Invoke(new Action(() => 
                    //{
                    //    this.tsmiState.Text = string.Format("状态：正常");
                    //}));
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //void HttpServer_ReceivedDataEvent(object sender, ReceivedData e)
        //{
        //    if (e.Data == null)
        //        return;
        //}
        #endregion
    }
}
