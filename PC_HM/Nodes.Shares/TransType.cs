using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.WMS.Shares
{
    /// <summary>
    /// 定义事件分类代码
    /// 需要与数据库表TRANS_TYPE一一对应
    /// </summary>
    public class TransType
    {
        public const string CREATE_RECEIVE_BILL = "100";
        public const string UPDATE_RECEIVE_BILL = "101";
    }
}
