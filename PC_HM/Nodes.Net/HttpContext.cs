using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Net;
using System.IO;
using System.Threading;

namespace Nodes.Net
{
    public class HttpContext
    {
        #region 变量
        private const char PARAM_SIGN = '&';
        private string _hostUrl = null;
        public static CookieContainer Cookie = new CookieContainer();
        #endregion

        #region 构造函数
        public HttpContext(string hostUrl)
        {
            this._hostUrl = hostUrl;
        }
        #endregion

        #region 方法

        public ResponsePackage Request(RequestPackage package)
        {
            ResponsePackage responsePackage = new ResponsePackage();
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            string url = this._hostUrl + package.RequestURL;
            string method = package.Method.ToString().ToLower();
            if (package.Method.ToLower() == EHttpMethod.Get.ToString().ToLower())
            {
                if (package.Params.Count > 0)
                    url = string.Format("{0}?{1}", url, JoinParams(package.Params));
                request = WebRequest.Create(url) as HttpWebRequest;
                request.Accept = package.Accept;
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; MALCJS; rv:11.0) like Gecko";
                request.CookieContainer = Cookie;
                request.Referer = this._hostUrl + package.RefererURL;
                request.Method = method;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
                string data = JoinParams(package.Params);
                byte[] b = package.Encoding.GetBytes(data);
                request.ContentType = "text/html";
                request.Accept = package.Accept;
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; MALCJS; rv:11.0) like Gecko";
                request.CookieContainer = Cookie;
                request.Referer = this._hostUrl + package.RefererURL;
                request.Method = method;
                request.ContentLength = b.Length;
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(b, 0, b.Length);
                }
            }
            try
            {
                //获取服务器返回的资源
                using (response = request.GetResponse() as HttpWebResponse)
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        if (response.Cookies.Count > 0)
                        {
                            Cookie.Add(response.Cookies);
                        }
                        responsePackage.CookieContainer = Cookie;
                        List<byte> dataList = new List<byte>();
                        while (true)
                        {
                            int data = stream.ReadByte();
                            if (data == -1)
                                break;
                            dataList.Add((byte)data);
                        }
                        responsePackage.ResultData = dataList.ToArray();
                    }
                }
                responsePackage.Result = EResponseResult.成功;
            }
            catch (WebException wex)
            {
                WebResponse wr = wex.Response;
                if (wr == null)
                {
                    responsePackage.ErrMessage = wex.Message;
                }
                else
                {
                    using (Stream st = wr.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(st, System.Text.Encoding.UTF8))
                        {
                            responsePackage.ErrMessage = sr.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                responsePackage.ErrMessage = "发生异常/n/r" + ex.Message;
            }
            return responsePackage;
        }
        #endregion

        #region Private Methods
        private string JoinParams(Dictionary<string, string> param)
        {
            StringBuilder data = new StringBuilder();
            if (param != null && param.Count > 0)
            {
                string lastKey = param.Keys.Last();
                foreach (string key in param.Keys)
                {
                    string value = param[key];
                    data.AppendFormat("{0}={1}", key, value);
                    if (key != lastKey)
                        data.Append(PARAM_SIGN);
                }
            }
            return data.ToString();
        }
        #endregion
    }
}
