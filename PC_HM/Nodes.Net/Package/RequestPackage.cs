using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Nodes.Net
{
    [Serializable]
    public class RequestPackage
    {
        #region 变量
        private string _requestURL = string.Empty;
        private string _refererURL = string.Empty;
        private Dictionary<string, string> _params = null;
        private string _method = EHttpMethod.Post.ToString();
        private string _accept = "*/*";
        private Encoding _encoding = Encoding.ASCII;
        #endregion

        #region 构造函数

        public RequestPackage()
        { }
        public RequestPackage(string requestUrl)
        {
            this._requestURL = requestUrl;
        }
        #endregion

        #region 属性

        public string RequestURL 
        {
            get { return this._requestURL; }
            set { this._requestURL = value; }
        }
        public string RefererURL 
        {
            get { return this._refererURL; }
            set { this._refererURL = value; }
        }
        public Dictionary<string, string> Params
        {
            get
            {
                if (this._params == null)
                    this._params = new Dictionary<string, string>();
                return this._params;
            }
        }
        public string Method
        {
            get { return this._method; }
            set { this._method = value; }
        }
        public string Accept
        {
            get { return this._accept; }
            set { this._accept = value; }
        }
        public Encoding Encoding
        {
            get { return this._encoding; }
            set { this._encoding = value; }
        }
        #endregion
    }
}
