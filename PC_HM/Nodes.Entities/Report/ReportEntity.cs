using System;
using Nodes.Dapper;

namespace Nodes.Entities.Report
{
    public class ReportEntity
    {
       /// <summary>
       /// 入库单数
       /// </summary>
       [ColumnName("INLIST")]
       public Int64 InList { get; set; }

       /// <summary>
       /// 出库单数
       /// </summary>
       [ColumnName("OUTLIST")]
       public Int64 OutList { get; set; }

        /// <summary>
        /// 出库数量
        /// </summary>
       [ColumnName("OUTCOUNT")]
       public decimal OutCount { get; set; }

        /// <summary>
        /// 出库金额
        /// </summary>
       [ColumnName("OUTPRICE")]
       public decimal OutPrice { get; set; }

        /// <summary>
        /// 打印数量
        /// </summary>
       [ColumnName("PRINTCOUNT")]
       public Int64 PrintCount { get; set; }

        /// <summary>
        /// 发货金额
        /// </summary>
       [ColumnName("DELIVERPRICE")]
       public decimal DeliverPrice { get; set; }
    }
}
