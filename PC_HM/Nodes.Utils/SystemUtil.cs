using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Nodes.Utils
{
    public class SystemUtil
    {
        public static void SetAutoRun(bool auto)
        {
            try
            {
                if (auto) //设置开机自启动  
                {
                    string path = Application.ExecutablePath;
                    RegistryKey rk = Registry.LocalMachine;
                    RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                    rk2.SetValue("JcShutdown", path);
                    rk2.Close();
                    rk.Close();
                }
                else //取消开机自启动  
                {
                    MessageBox.Show("取消开机自启动，需要修改注册表", "提示");
                    string path = Application.ExecutablePath;
                    RegistryKey rk = Registry.LocalMachine;
                    RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                    rk2.DeleteValue("JcShutdown", false);
                    rk2.Close();
                    rk.Close();
                }
            }
            catch { }
        }
    }
}
