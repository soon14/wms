using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.WMS.Shares
{
    public class ReceiveBillState
    {
        /// <summary>
        /// 入库单-草稿
        /// </summary>
        public const int STATE_DRAFT = 10;

        /// <summary>
        /// 提交待审
        /// </summary>
        public const int STATE_WAITING_CHECK = 20;

        /// <summary>
        /// 已审核
        /// </summary>
        public const int STATE_HAS_CHECK = 30;

        /// <summary>
        /// 开始收货，货到确认
        /// </summary>
        public const int STATE_BEGIN_RECEIVE = 40;

        /// <summary>
        /// 状态为-1、草稿、等待审批可以编辑
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static bool CanCommitTrans(int state)
        {
            if (state <= 0 ||
                state == STATE_DRAFT ||
                state == STATE_WAITING_CHECK)
                return true;

            return false;
        }
    }
}
