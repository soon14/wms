using Nodes.Dapper;

namespace Nodes.Entities
{
    public class SkuWarehouseEntity
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [ColumnName("ID")]
        public int SkuWarehouseID
        {
            set;
            get;
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [ColumnName("SKU_CODE")]
        public string SkuCode
        {
            set;
            get;
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [ColumnName("WH_CODE")]
        public string WhCode
        {
            set;
            get;
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [ColumnName("SPEC")]
        public string Spec
        {
            set;
            get;
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [ColumnName("PRICE")]
        public decimal Price
        {
            set;
            get;
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [ColumnName("MIN_STOCK_QTY")]
        public int MinStockQty
        {
            set;
            get;
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [ColumnName("MAX_STOCK_QTY")]
        public int MaxStockQty
        {
            set;
            get;
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [ColumnName("LOWER_LOCATION")]
        public int LowerLocation
        {
            set;
            get;
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [ColumnName("UPPER_LOCATION")]
        public int UpperLocation
        {
            set;
            get;
        }

        /// <summary>
        /// 安全库存
        /// </summary>
        [ColumnName("SECURITY_QTY")]
        public int SecurityQty
        {
            set;
            get;
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [ColumnName("PICK_TYPE")]
        public int PickType
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        [ColumnName("SKU_NAME")]
        public string SkuName
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        [ColumnName("WH_NAME")]
        public string WhName
        {
            set;
            get;
        }
    }
}
