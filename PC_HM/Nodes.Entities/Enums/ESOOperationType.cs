using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities
{
    public enum ESOOperationType
    {
        完成排序,
        生成拣货指令,
        已完成分派,
        整货拣货完成,
        整货称重完成,
        散货拣货完成,
        散货称重完成,
        已分派装车,
        整货已称重,
        物流箱已到,
        散货已经装车,
        装车完毕,
        已打印销售发货单,
        已发车,
        已关闭,
        二次发货,
        订单取消
    }
}
