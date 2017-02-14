using System;
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace Nodes.Utils
{
    public class LogHelper
    {
        public static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("logerror");

        /// <summary>
        /// 信息日志输出
        /// </summary>
        /// <param name="info"></param>
        public static void InfoLog(string info)
        {
            if (loginfo.IsInfoEnabled)
            {
                StackTrace ss = new StackTrace(true);
                MethodBase mb = ss.GetFrame(1).GetMethod();
                string fullName = mb.DeclaringType.FullName;
                string methodsName = mb.Name;
                //string infos = string.Format("[调用方法]{0}.{1}  [信息输出]{2}", fullName,methodsName, info);
                loginfo.InfoFormat("【调用方法】{0}.{1}\r\n【信息输出】{2}\r\n", fullName, methodsName, info);
            }
        }

        


        /// <summary>
        /// 信息日志输出
        /// </summary>
        /// <param name="info"></param>
        public static void InfoLogError(string info)
        {
            loginfo.ErrorFormat("【附加信息】{0}\n", new object[] { info });
        }
        /// <summary>
        /// 输出异常日志
        /// </summary>
        /// <param name="error">Exception异常类</param>
        public static void errorLog(string info, Exception error)
        {
            if (!string.IsNullOrEmpty(info) && error == null)
            {
                loginfo.ErrorFormat("【附加信息】{0}\n", new object[] { info });
            }
            else if (!string.IsNullOrEmpty(info) && error != null)
            {
                string errorMsg = BeautyErrorMsg(error);
                loginfo.ErrorFormat("【附加信息】{0}\r\n{1}", new object[] { info, errorMsg });
            }
            else if (string.IsNullOrEmpty(info) && error != null)
            {
                string errorMsg = BeautyErrorMsg(error);
                loginfo.Error(errorMsg);
            }
        }
        public static string errorLogString(Exception error)
        {
            return BeautyErrorMsg(error);
        }
        /// <summary>
        /// 美化错误信息
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>错误信息</returns>
        private static string BeautyErrorMsg(Exception ex)
        {
            string errorMsg = string.Format("【异常类型】{0}\r\n【异常信息】{1}\r\n【堆栈调用】{2}\r\n", new object[] { ex.GetType().Name, ex.Message, ex.StackTrace });
            //errorMsg = errorMsg.Replace("\r\n", "<br>");
            errorMsg = errorMsg.Replace("位置", "\r\n【异常位置】");
            return errorMsg;
        }
    }

    public static class LogerHelper
    {
        #region  创建日志
        ///-----------------------------------------------------------------------------
        /// <summary>创建错误日志 在c:\ErrorLog\</summary>
        /// <param name="message">记录信息
        /// <returns></returns>
        ///-----------------------------------------------------------------------------
        public static void CreateLogTxt(string message)
        {
            string strPath;                                                   //文件的路径
            DateTime dt = DateTime.Now;
            try
            {
                strPath = Directory.GetCurrentDirectory() + "\\Log";          //winform工程\bin\目录下 创建日志文件夹 

                if (Directory.Exists(strPath) == false)                          //工程目录下 Log目录 '目录是否存在,为true则没有此目录
                {
                    Directory.CreateDirectory(strPath);                       //建立目录　Directory为目录对象
                }
                strPath = strPath + "\\" + dt.ToString("yyyy-MM");

                if (Directory.Exists(strPath) == false)
                {
                    Directory.CreateDirectory(strPath);
                }
                strPath = strPath + "\\" + dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString() + ".txt";

                StreamWriter FileWriter = new StreamWriter(strPath, true);           //创建日志文件
                FileWriter.WriteLine("[" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "]  " + message);
                FileWriter.Close();                                                 //关闭StreamWriter对象
            }
            catch (Exception ex)
            {
                string str = ex.Message.ToString();
            }
        }
        #endregion

    }
}
