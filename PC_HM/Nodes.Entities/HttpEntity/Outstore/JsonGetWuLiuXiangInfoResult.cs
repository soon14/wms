using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Outstore
{
    public class JsonGetWuLiuXiangInfoResult
    {
        public string lcCode
        {
            get;
            set;
        }
        public string ctCode
        {
            get;
            set;
        }
        public string ctlId
        {
            get;
            set;
        }
        public string ShowInfo
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}-{1}-{2}",
                    string.IsNullOrEmpty(ctlId) ? "(空)" : ctlId,
                    string.IsNullOrEmpty(lcCode) ? "(空)" : lcCode,
                    string.IsNullOrEmpty(ctCode) ? "(空)" : ctCode);
                {
                    sb.Append("\r\n");
                }

                return sb.ToString();
            }
        }
    }
}
