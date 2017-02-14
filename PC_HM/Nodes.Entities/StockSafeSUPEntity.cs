using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.Entities
{
    public class StockSafeSUPEntity
    {
        bool hasChecked = false;
        public bool HasChecked
        {
            get { return hasChecked; }
            set { hasChecked = value; }
        }
        //货位号
        [ColumnName("LC_CODE")]
        public string LC_CODE { get; set; }
        //商品条码
        [ColumnName("SKU_CODE")]
        public string SKU_CODE { get; set; }
        //库存数量
        [ColumnName("QTY")]
        public decimal QTY { get; set; }
        //安全库存差值
        [ColumnName("DIFF_QTY")]
        public decimal DIFF_QTY { get; set; }
        //拣货占用数量
        [ColumnName("PICKING_QTY")]
        //商品规格
        public decimal PIKING_QTY { get; set; }
        [ColumnName("SPEC")]
        //库存单位
        public string SPEC { get; set; }
        [ColumnName("UM_NAME")]
        public string UM_NAME { get; set; }
        //整散标记
        [ColumnName("SKU_NAME")]
        public string SKU_NAME { get; set; }
    }
}
