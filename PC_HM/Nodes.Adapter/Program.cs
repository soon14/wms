using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Nodes.Adapter
{
    /// <summary>
    /// 适配器（此程序运行在库房服务器；为中央服务器与库房客户端的桥梁）
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
