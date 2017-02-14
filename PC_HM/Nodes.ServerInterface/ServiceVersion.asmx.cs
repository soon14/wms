using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using Nodes.DBHelper;
using Newtonsoft.Json;
using System.Data;
using Nodes.Entities;
using System.IO;

namespace Nodes.ServerInterface
{
    /// <summary>
    /// 中央库提供给各库程序版本控制接口
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class ServiceVersion : System.Web.Services.WebService
    {
        #region 常量
        /// <summary>
        /// 系统默认版本
        /// </summary>
        private const string DEFAULT_VERSION = "4.0.0.0";
        private const string BETA_STR = "BETA_";
        private const string LOG_FULLPATH = @"D:\wms_version\version_{0}.log";

        #endregion

        #region 方法

        /// <summary>
        /// 获取服务端版本（适用于PC与PDA）
        /// </summary>
        /// <param name="appID">标识</param>
        /// <returns></returns>
        [WebMethod]
        public string GetVersion(string appID)
        {
            VersionInfo versionInfo = DBUtil.GetVersion(appID);
            return JsonConvert.SerializeObject(versionInfo);
        }
        /// <summary>
        /// 获取服务端版本（适用于PC与PDA）
        /// </summary>
        /// <param name="appID">标识</param>
        /// <param name="whCode">库房编号</param>
        /// <returns></returns>
        [WebMethod]
        public string GetVersionWithWarehouseCode(string appID, string whCode)
        {
            //string log_fullPath = string.Format(LOG_FULLPATH, DateTime.Now.ToString("yyyyMM"));
            //string default_dir = Path.GetDirectoryName(log_fullPath);
            //if (Directory.Exists(default_dir))
            //    Directory.CreateDirectory(default_dir);
            //using (StreamWriter sw = File.CreateText(log_fullPath))
            //{
            //    sw.WriteLine(string.Format("{0}-{1}\t{2}", appID, whCode, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            //}
            // 获取测试库存信息
            VersionInfo betaVersion = DBUtil.GetVersion(BETA_STR + appID);
            VersionInfo versionInfo = null;
            // 判断当前库存编号在测试程序中是否存在
            if (betaVersion != null && betaVersion.WH_CODE != null && betaVersion.WH_CODE.IndexOf(whCode) > -1)
            {
                versionInfo = betaVersion;
            }
            else
            {
                versionInfo = DBUtil.GetVersion(appID);
            }
            if (versionInfo != null && !string.IsNullOrEmpty(versionInfo.WH_CODE) && !string.IsNullOrEmpty(whCode))
            {
                if (versionInfo.WH_CODE.IndexOf(whCode) < 0) // 当前库房编号不存在
                {
                    // 修改服务端版本，不要升级
                    versionInfo.VER = DEFAULT_VERSION;
                }
            }
            return JsonConvert.SerializeObject(versionInfo);
        }
        /// <summary>
        /// 获取PC端的文件列表
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="oldVersion">旧版本</param>
        /// <param name="newVersion">新版本</param>
        /// <returns></returns>
        [WebMethod]
        public string GetFileList(string id, string oldVersion, string newVersion)
        {
            string newID = id;
            // 获取测试库存信息
            VersionInfo betaVersion = DBUtil.GetVersion(BETA_STR + id);
            if (betaVersion != null && betaVersion.WH_CODE != null && betaVersion.WH_CODE.IndexOf(id) > -1)
            {
                newID = BETA_STR + id;
            }
            DataTable dt = DBUtil.GetUpdateFiles(newID, oldVersion, newVersion);
            string result = string.Empty;
            if (dt != null && dt.Rows.Count > 0)
            {
                string[] array = new string[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    array[i] = dt.Rows[i].ItemArray[0].ToString();
                }
                result = JsonConvert.SerializeObject(array);
            }
            return result;
        }

        #endregion
    }
}
