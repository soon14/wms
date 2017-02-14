using System;
using System.Data;
using System.Diagnostics;
using Nodes.DBHelper;
using Nodes.Shares;
using Nodes.Utils;
using Nodes.UI;
using Nodes.WMSClient.ServerInterface;
using Newtonsoft.Json;
using System.IO;
using Nodes.Entities;

namespace Nodes.WMSClient
{
    public class UpdateUtil
    {
        #region 变量

        private VersionInfo _serverVersion = null;
        private VersionInfo _localVersion = null;
        private ServiceVersion _webVersion = new ServiceVersion();
        private static uint CallCount = 0;

        #endregion

        #region 属性
        public VersionInfo LocalVersion
        {
            get
            {
                return this._localVersion;
            }
        }
        #endregion

        #region 方法
        public bool HasUpdate(string appId)
        {
            CallCount++;
            bool _hasUpdate = false;
            try
            {
                string whCode = GlobeSettings.LoginedUser == null ? null : GlobeSettings.LoginedUser.WarehouseCode;
                _localVersion = XmlBaseClass.TryDeserial<VersionInfo>(Path.Combine(PathUtil.ApplicationStartupPath, "manifest.xml"));
#if !DEBUG
                string webResult = this._webVersion.GetVersionWithWarehouseCode(appId, whCode);
                Console.WriteLine(webResult);
                if (!string.IsNullOrEmpty(webResult))
                {
                    this._serverVersion = JsonConvert.DeserializeObject<VersionInfo>(webResult);
                    this._localVersion.UPDATE_FLAG = this._serverVersion.UPDATE_FLAG;
                    this._localVersion.WH_CODE = this._serverVersion.WH_CODE;
                }
#else
                //this._webVersion.Url = "http://localhost:57100/ServiceVersion.asmx";
                //string webResult = this._webVersion.GetVersionWithWarehouseCode(appId, whCode);
                //Console.WriteLine(webResult);
                //if (!string.IsNullOrEmpty(webResult))
                //{
                //    this._serverVersion = JsonConvert.DeserializeObject<VersionInfo>(webResult);
                //    this._localVersion.UPDATE_FLAG = 1;
                //    this._localVersion.WH_CODE = this._serverVersion.WH_CODE;
                //}
#endif
            }
            catch (Exception ex)
            {
                if (CallCount > 1)
                    MsgBox.Err(ex.Message);
                else
                    HasUpdate(appId);
            }
            finally
            {
                if (_serverVersion == null)
                    _hasUpdate = false;
                else if (_serverVersion.VER.CompareTo(_localVersion.VER) > 0)
                    _hasUpdate = true;
                else
                    _hasUpdate = false;
            }

            return _hasUpdate;
        }

        public void UpdateNow()
        {
            try
            {
                //获取更新文件
                string _files = "";
                string[] array = JsonConvert.DeserializeObject<string[]>(_webVersion.GetFileList(_serverVersion.ID, _localVersion.VER, _serverVersion.VER));
                if (array != null && array.Length > 0)
                {
                    //DBUtil.GetUpdateFiles(_serverVersion.ID, _localVersion.VER, _serverVersion.VER);
                    foreach (string row in array)
                        _files += row + ",";

                    if (string.IsNullOrEmpty(_files)) return;
                    else _files = _files.Substring(0, _files.Length - 1);

                    Process pro = new Process();
                    pro.StartInfo.FileName = "Nodes.Updater.exe";
                    pro.StartInfo.WorkingDirectory = PathUtil.ApplicationStartupPath;
                    pro.StartInfo.Arguments = string.Format("{0} {1} {2} \"{3}\" \"{4}\" \"{5}\"",
                        _serverVersion.ID,
                        System.IO.Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName),
                        _serverVersion.VER,
                        _serverVersion.URL,
                        _files,
                        _serverVersion.REMARK);
                    pro.StartInfo.CreateNoWindow = true;
                    pro.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                    pro.Start();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        #endregion
    }
}
