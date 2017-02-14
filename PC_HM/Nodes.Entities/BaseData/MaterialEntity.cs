using Nodes.Dapper;
using System;
using Nodes.Utils;

namespace Nodes.Entities
{
    /// <summary>
    /// WM_SKU
    /// </summary>
    public class MaterialEntity : BaseEntity, ICloneable
    {
        [ColumnName("SKU_ID")]
        public int MaterialID
        {
            set;
            get;
        }

        /// <summary>
        ///物料编码
        /// </summary>
        [ColumnName("SKU_CODE")]
        public virtual string MaterialCode
        {
            set;
            get;
        }

        /// <summary>
        /// 物料名称
        /// </summary>
        [ColumnName("SKU_NAME")]
        public string MaterialName
        {
            set;
            get;
        }

        /// <summary>
        /// 物料助记符或简称
        /// </summary>
        [ColumnName("SKU_NAME_S")]
        public string MaterialNameS
        {
            set;
            get;
        }

        /// <summary>
        /// 拼音简写
        /// </summary>
        public string MaterialNamePY
        {
            get
            {
                return PinYin.GetCapital(MaterialName);
            }
        }

        /// <summary>
        /// 计量单位组编号
        /// </summary>
        [ColumnName("UG_CODE")]
        public string UnitGrpCode
        {
            set;
            get;
        }

        [ColumnName("UG_NAME")]
        public string UnitGrpName
        {
            set;
            get;
        }

        /// <summary>
        /// 计量单位编码
        /// </summary>
        [ColumnName("UM_CODE")]
        public string UnitCode
        {
            set;
            get;
        }

        [ColumnName("UM_NAME")]
        public string UnitName
        {
            set;
            get;
        }

        /// <summary>
        /// 品牌编码
        /// </summary>
        [ColumnName("BRD_CODE")]
        public string BrandCode
        {
            set;
            get;
        }

        [ColumnName("BRD_NAME")]
        public string BrandName
        {
            set;
            get;
        }

        /// <summary>
        /// 保质期（单位：天）
        /// </summary>
        [ColumnName("EXP_DAYS")]
        public int ExpDays
        {
            set;
            get;
        }

        /// <summary>
        /// 最小库存数量 MIN_STOCK_QTY
        /// </summary>
        [ColumnName("MIN_STOCK_QTY")]
        public int? MinStockQty
        {
            set;
            get;
        }

        /// <summary>
        /// 最大库存数量 MAX_STOCK_QTY
        /// </summary>
        [ColumnName("MAX_STOCK_QTY")]
        public int? MaxStockQty
        {
            set;
            get;
        }

        /// <summary>
        /// 安全库存 SECURITY_QTY
        /// </summary>
        [ColumnName("SECURITY_QTY")]
        public int? SecurityQty
        {
            set;
            get;
        }

        /// <summary>
        /// 条码1
        /// </summary>
        [ColumnName("BARCODE1")]
        public string Barcode1
        {
            set;
            get;
        }

        /// <summary>
        /// 条码2
        /// </summary>
        [ColumnName("BARCODE2")]
        public string Barcode2
        {
            set;
            get;
        }

        /// <summary>
        /// 温度存储条件
        /// </summary>
        [ColumnName("TEMP_CODE")]
        public string TemperatureCode
        {
            set;
            get;
        }

        [ColumnName("TEMP_NAME")]
        public string TemperatureName
        {
            set;
            get;
        }

        /// <summary>
        /// 物料类别编号
        /// </summary>
        [ColumnName("TYP_CODE")]
        public string MaterialTypeCode
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        [ColumnName("TYP_NAME")]
        public string MaterialTypeName
        {
            set;
            get;
        }

        /// <summary>
        /// 供应商编码
        /// </summary>
        [ColumnName("C_CODE")]
        public string SupplierCode
        {
            set;
            get;
        }

        /// <summary>
        /// 供应商名称
        /// </summary>
        [ColumnName("C_NAME")]
        public string SupplierName
        {
            set;
            get;
        }

        [ColumnName("SPEC")]
        public string Spec
        {
            get;
            set;
        }

        [ColumnName("SORT_ORDER")]
        public int SortOrder
        {
            set;
            get;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        [ColumnName("SKU_TYPE")]
        public string SkuType
        {
            set;
            get;
        }

        [ColumnName("ITEM_DESC")]
        public string SkuTypeDesc
        {
            set;
            get;
        }
        [ColumnName("TOTAL_STOCK_QTY")]
        public decimal TotalStockQty { get; set; }
    }
}

