using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Nodes.WMSClient
{
    /// <summary>
    /// 客户端（此程序运行于各库房）
    /// </summary>
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            log4net.Config.XmlConfigurator.Configure(); 
            Process instance = null;
#if !DEBUG
            instance = RunningInstance();
#endif
            if (instance == null)
            {
                Application.SetCompatibleTextRenderingDefault(false);
                MainApplicationContext mainApplicationContext = new MainApplicationContext();
                if (mainApplicationContext.ShouldRun)
                    Application.Run(mainApplicationContext);
            }
            else
            {
                //There is another instance of this process.  
                HandleRunningInstance(instance);
            }
        }
        public static Process RunningInstance()
        {
            Process _current = Process.GetCurrentProcess();

            //读取当前可执行程序的路径
            string _currExecutingLocation = _current.MainModule.FileName;

            //从进程中获取同名的应用程序
            Process[] _processes = Process.GetProcessesByName(_current.ProcessName);

            //1. 排除自己（p.Id != _current.Id）
            //2. 同目录下的文件
            //可能存在同名的应用程序，所以对于不在同一目录下的同名的应用程序允许启动当前的应用程序，
            //所以这儿有个漏洞，若想启动两个或多个进程，可以复制到不同目录后再启动
            foreach (Process process in _processes)
            {
                //Ignore the current process
                if (process.Id != _current.Id)
                {
                    //Make sure that the process is running from the exe file.  
                    if (_currExecutingLocation == _current.MainModule.FileName)
                    {
                        //Return the other process instance.  
                        return process;
                    }
                }
            }

            return null;
        }

        public static void HandleRunningInstance(Process instance)
        {
            //Make sure the window is not minimized or maximized  
            ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL);

            //Set the real intance to foreground window  
            SetForegroundWindow(instance.MainWindowHandle);
        }

        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int WS_SHOWNORMAL = 1;
    }
}