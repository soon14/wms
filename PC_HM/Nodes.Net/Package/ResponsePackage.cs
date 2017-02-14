using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Nodes.Net
{
    [Serializable]
    public class ResponsePackage
    {
        private EResponseResult _result = EResponseResult.失败;
        private object _resultData = null;
        private string _errMessage = null;
        private CookieContainer _cookieContainer = null;

        public EResponseResult Result
        {
            get { return this._result; }
            set { this._result = value; }
        }
        public object ResultData
        {
            get { return this._resultData; }
            set { this._resultData = value; }
        }
        public string ErrMessage
        {
            get { return this._errMessage; }
            set { this._errMessage = value; }
        }
        public CookieContainer CookieContainer
        {
            get { return this._cookieContainer; }
            set { this._cookieContainer = value; }
        }
    }

    public enum EResponseResult
    {
        失败 = 0,
        成功 = 1,
    }
}
