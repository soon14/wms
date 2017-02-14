using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities
{
    /// <summary>
    /// 发货单，用于报表打印
    /// </summary>
    public class ReturnBody
    {
        /// <summary>
        /// 公司信息
        /// </summary>
        public CompanyEntity CompanyInfo { get; set; }

        /// <summary>
        /// 发货单的客户信息
        /// </summary>
        public CustomerEntity Customer { get; set; }

        /// <summary>
        /// 单据头
        /// </summary>
        public ReturnHeaderEntity Header { get; set; }

        /// <summary>
        /// 单据明细
        /// </summary>
        public List<ReturnDetailsEntity> Details { get; set; }
    }
}
