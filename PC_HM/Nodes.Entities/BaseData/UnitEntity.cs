using Nodes.Dapper;

namespace Nodes.Entities
{
    /// <summary>
    /// 计量单位
    /// </summary>
    public class UnitEntity
    {
        #region Model

        /// <summary>
        /// 计量单位编码
        /// </summary>
        [ColumnName("UM_CODE")]
        public string UnitCode
        {
            set;
            get;
        }

        /// <summary>
        /// 计量单位名称
        /// </summary>
        [ColumnName("UM_NAME")]
        public string UnitName
        {
            set;
            get;
        }

        #endregion Model
    }

    /// <summary>
    /// 计量单位组（包装关系组）
    /// </summary>
    public class UnitGroupEntity : BaseEntity
    {
        //[ColumnName("UG_CODE")]
        //public string GrpCode { get; set; }

        //[ColumnName("UG_NAME")]
        //public string GrpName { get; set; }

        [ColumnName("ID")]
        public int ID { get; set; }

        /// <summary>
        /// 最小单位
        /// </summary>
        [ColumnName("UM_CODE")]
        public string UnitCode { get; set; }

        [ColumnName("UM_NAME")]
        public string UnitName { get; set; }

        [ColumnName("QTY")]
        public int Qty { get; set; }

        [ColumnName("SKU_CODE")]
        public string SkuCode { get; set; }

        [ColumnName("SKU_NAME")]
        public string SkuName { get; set; }

        [ColumnName("SKU_BARCODE")]
        public string SkuBarcode { get; set; }

        [ColumnName("SPEC")]
        public string Spec { get; set; }

        [ColumnName("WEIGHT")]
        public decimal Weight { get; set; }

        [ColumnName("LENGTH")]
        public decimal Length { get; set; }

        [ColumnName("WIDTH")]
        public decimal Width { get; set; }

        [ColumnName("HEIGHT")]
        public decimal Height { get; set; }
        [ColumnName ("SKU_LEVEL")]
        public int SkuLevel { get; set; }

        /// <summary>
        /// 商品的体积
        /// </summary>
        [ColumnName("SKU_VOL")]
        public decimal Skuvol { get; set; }

    }

    public class UnitGroupItemEntity : UnitEntity
    {
        [ColumnName("UG_CODE")]
        public string GrpCode { get; set; }

        [ColumnName("PACK_QTY")]
        public decimal PackQty { get; set; }
    }
}