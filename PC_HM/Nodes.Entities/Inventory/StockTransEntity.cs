using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.Entities
{
    public class StockTransEntity : StockRecordEntity
    {
        /// <summary>
        /// 目标货位
        /// </summary>
        [ColumnName("TO_LC_CODE")]
        public string TargetLocation { get; set; }

        /// <summary>
        /// 移出数量
        /// </summary>
        [ColumnName("TRANS_QTY")]
        public decimal TransferQty
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 库房编码
        /// </summary>
        public string WHCode { get; set; }
        [ColumnName("IS_CASE")]
        public int IsCase { get; set; }
    }
}
