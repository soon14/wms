using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.Entities.Inventory
{
    public class StockSKUEntity
    {
        [ColumnName("SKU_CODE")]
        public string SkuCode { get; set; }

        [ColumnName("SKU_NAME")]
        public string SkuName { get; set; }

        [ColumnName("TotalQty")]
        public decimal TotalQty { get; set; }

        [ColumnName("MIN_STOCK_QTY")]
        public decimal MinStockQty { get; set; }

        [ColumnName("MAX_STOCK_QTY")]
        public decimal MaxStockQty { get; set; }

        [ColumnName("UM_NAME")]
        public string UnitName { get; set; }

        /// <summary>
        /// 采购提醒
        /// </summary>
        public string IsSafe
        {
            get
            {
                if (TotalQty <= MinStockQty)
                {
                    return "Y";
                }
                else
                {
                    return "N";
                }
            }
        }
    }
}
