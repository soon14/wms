using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Nodes.Updater
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#if DEBUG
            string[] array = new string[6];
            array[0] = "wms";
            array[1] = "Nodes.WMSClient.vshost.exe";
            array[2] = "2.0.0.0";
            array[3] = "http://114.215.98.144:8008/PC";
            array[4] = "BaseData.dll,CycleCount.dll,Dapper.dll,DBHelper.dll,Entities.dll,Instore.dll,Outstore.dll,Reports.dll,Stock.dll,SystemManage.dll,Utils.dll,WMSClient.exe,Print.dll";
            array[5] = "1.修正称重时显示的BUG\r\n2.人员状态中添加关联任务，方便查看，拣货人员是否在有任务的情况下签退；";
            Application.Run(new Form1(array));
#else
            if (args != null && args.Length == 6)
            {
                Application.Run(new Form1(args));
            }
            else
            {
                MessageBox.Show("升级程序不允许单独运行！");
            }
#endif
        }
    }
}