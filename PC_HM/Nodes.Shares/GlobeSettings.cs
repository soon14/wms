using System.Windows.Forms;
using Nodes.UI;
using Nodes.DBHelper;
using Nodes.Entities;
using System.Collections.Generic;
using System.Data;
using System;
using Nodes.Net;
using Nodes.UI;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Shares;
using Newtonsoft.Json;

namespace Nodes.Shares
{
    public class GlobeSettings
    {
        public static UserEntity LoginedUser = null;
        public static VersionInfo LocalVersion = null;
        public const string InitialPassword = "123456";
        public static string AppPath = Application.StartupPath;
        //public static HttpClient HttpClient = new HttpClient("http://192.168.159.1:8100/");

        public const string POSalesRoleName = "业务员";
        public const string DriverRoleName = "司机";
        public const string CycleCountRoleName = "盘点员";
        public const string ReceiveRoleName = "收货员";
        public const string SOPickPersonRoleName = "拣货员";
        public const string PutAwayRoleName = "上架员";


        /// <summary>
        /// 拣货计划-----是否含有
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public static bool HasThisRight(string userCode, short moduleId)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("userCode=").Append(userCode).Append("&");
                loStr.Append("moduleId=").Append(moduleId);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_HasThisRight);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return false;
                }
                #endregion

                #region 正常错误处理

                JsonHasThisRight bill = JsonConvert.DeserializeObject<JsonHasThisRight>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return false;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return false;
                }
                #endregion
                if(bill.result != null && bill.result.Length > 0)
                    return !string.IsNullOrEmpty(bill.result[0].userCode);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return false;
        }

        public static bool HasRight(short moduleID)
        {
            if (HasThisRight(GlobeSettings.LoginedUser.UserCode, moduleID))
                return true;
            else
            {
                MsgBox.Warn("没有权限。");
                return false;
            }
        }

        /// <summary>
        /// 获取系统设置
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSysLoadingSetting()
        {
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("ID", Type.GetType("System.String"));
            tblDatas.Columns.Add("SET_ITEM", Type.GetType("System.String"));
            tblDatas.Columns.Add("SET_VALUE", Type.GetType("System.String"));
            tblDatas.Columns.Add("SET_GROUP", Type.GetType("System.String"));
            tblDatas.Columns.Add("REMARK", Type.GetType("System.String"));

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("cardState=").Append(cardState);
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_GetSysLoadingSetting);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetSysLoadingSetting bill = JsonConvert.DeserializeObject<JsonGetSysLoadingSetting>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return tblDatas;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return tblDatas;
                }
                #endregion

                #region 赋值
                foreach (JsonGetSysLoadingSettingResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["ID"] = tm.id;
                    newRow["SET_ITEM"] = tm.setItem;
                    newRow["SET_VALUE"] = tm.setValue;
                    newRow["SET_GROUP"] = tm.setGroup;
                    newRow["REMARK"] = tm.remark;
                    tblDatas.Rows.Add(newRow);
                }
                return tblDatas;
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return tblDatas;
        }

        /// <summary>
        /// 获取系统设置
        /// </summary>
        public static Dictionary<string, object> SystemSettings
        {
            get
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                DataTable dt = GetSysLoadingSetting();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        object keyObj = item["SET_ITEM"];
                        if (keyObj == null) continue;
                        string key = item["SET_ITEM"] == DBNull.Value ? null : item["SET_ITEM"].ToString();
                        if (key != null && !dic.ContainsKey(key))
                        {
                            dic.Add(key, item["SET_VALUE"]);
                        }
                    }
                }
                return dic;
            }
        }


        private static HttpContext httpClient = null;
        public static HttpContext HttpClient
        {
            get
            {
                if (httpClient == null)
                    httpClient = new HttpContext(string.Format("http://{0}:8898/", ServerIP));
                return httpClient;
            }
        }

        #region 属性
        private static string serverIP = null;
        public static string ServerIP
        {
            get
            {
                if (string.IsNullOrEmpty(serverIP))
                {
                    string connectionStr = XmlBaseClass.ReadConfigValue("ConnectionString", "Value");
                    //int index1 = connectionStr.IndexOf('=');
                    //int index2 = connectionStr.IndexOf(';');
                    serverIP = connectionStr;//.Substring(index1 + 1, index2 - index1 - 1);
                }
                return serverIP;
            }
            set
            {
                if (serverIP != value)
                {
                    string connectionStr = XmlBaseClass.ReadConfigValue("ConnectionString", "Value");
                    string newConStr = connectionStr.Replace(serverIP, value);
                    XmlBaseClass.WriteConfigValue("ConnectionString", "Value", newConStr);
                    string newyy = XmlBaseClass.ReadConfigValue("WebServiceAddress", "Value");
                    string newConStr111 = connectionStr.Replace(serverIP, value);
                    XmlBaseClass.WriteConfigValue("WebServiceAddress", "Value", newConStr111);
                    serverIP = value;
                }
            }
        }
        #endregion
    }
}
