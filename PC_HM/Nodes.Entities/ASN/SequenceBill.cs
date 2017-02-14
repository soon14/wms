using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.WMS.Entities
{
    public class SequenceBill
    {
        /// <summary>
        /// 主键序列号
        /// </summary>
        [ColumnName("SEQUENCY")]
        public string Sequency { get; set; }

        /// <summary>
        /// 到货通知单号
        /// </summary>
        [ColumnName("BILL_ID")]
        public string BillId { get; set; }

        /// <summary>
        /// 到货通知单明细编号
        /// </summary>
        [ColumnName("DETAIL_ID")]
        public string DetailId { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        [ColumnName("MATERIAL_CODE")]
        public string MaterialCode { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        [ColumnName("MATERIAL_NAME")]
        public string MaterialName { get; set; }

        /// <summary>
        /// 仓库
        /// </summary>
        [ColumnName("WAREHOUSE")]
        public string Warehouse { get; set; }

        /// <summary>
        /// 验收时间
        /// </summary>
        [ColumnName("CHECK_DATE")]
        public DateTime CheckDate { get; set; }

        /// <summary>
        /// 验收人
        /// </summary>
        [ColumnName("CHECK_MAN")]
        public string CheckMan { get; set; }

        /// <summary>
        /// 上架时间
        /// </summary>
        [ColumnName("PUTAWAY_DATE")]
        public DateTime? PutawayDate { get; set; }

        /// <summary>
        /// 上架人
        /// </summary>
        [ColumnName("PUTAWAY_MAN")]
        public string PutawayMan { get; set; }

        /// <summary>
        /// 库存状态
        /// </summary>
        [ColumnName("STATUS")]
        public string Status { get; set; }
    }
}
