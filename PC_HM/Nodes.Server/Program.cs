using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Nodes.Server
{
    /// <summary>
    /// 服务端（此程序运行于中央服务器，用于控制各库房或收集各库房信息等）
    /// </summary>
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    }
}
