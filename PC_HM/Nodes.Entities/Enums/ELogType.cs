using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities
{
    /// <summary>
    /// 日志类型
    /// </summary>
    public enum ELogType : uint
    {
        订单状态变更,
        打印,
        操作任务,
        容器状态,
        退货单,
        越库,
        用户,
        库存,
        盘点,
        任务,
        修改数量,
        装车,
        角色,
        应急处理_退货单,
        修改包装关系,
        签退
    }
}
