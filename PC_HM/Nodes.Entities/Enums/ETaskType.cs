using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities
{
    [Flags]
    public enum ETaskType
    {
        无 = 0,
        盘点任务 = 140,
        移库任务 = 141,
        上架任务 = 142,
        拣货任务 = 143,
        补货任务 = 144,
        装车任务 = 145,
    }
}
