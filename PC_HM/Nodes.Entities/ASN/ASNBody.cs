using System.Collections.Generic;
using System.ComponentModel;

namespace Nodes.Entities
{
    /// <summary>
    /// 到货通知单实体，包含表头和表体
    /// </summary>
    public class ASNBody
    {
        /// <summary>
        /// 单据头
        /// </summary>
        public AsnHeaderEntity Header { get; set; }

        /// <summary>
        /// 单据明细
        /// </summary>
        public List<PODetailEntity> Details { get; set; }
    }
}
