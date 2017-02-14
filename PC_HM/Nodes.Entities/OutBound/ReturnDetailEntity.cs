using Nodes.Dapper;
using System;

namespace Nodes.Entities
{
    /// <summary>
    /// 发货单明细信息
    /// </summary>
    public class ReturnDetailEntity
    {
        [ColumnName("ID")]
        public int SoDetailID { get; set; }

        /// <summary>
        /// 出库单主键编号
        /// </summary>
        [ColumnName("BILL_ID")]
        public int BillID { get; set; }

        /// <summary>
        /// 行号
        /// </summary>
        [ColumnName("ROW_NO")]
        public int RowNO { get; set; }

        /// <summary>
        /// 套餐信息
        /// </summary>
        [ColumnName("COM_MATERIAL")]
        public string CombMaterial { get; set; }

        public string ProductName
        {
            get
            {
                if (String.IsNullOrEmpty(CombMaterial))
                    return MaterialName;
                else
                    return String.Format("{0} {1}/{2}({3})", MaterialName, Spec, UnitName, Qty / SuitNum);
            }
        }

        public string SkuTypeName
        {
            get
            {
                if (String.IsNullOrEmpty(CombMaterial))
                    return "单品";
                else
                    return "套餐";
            }
        }

        [ColumnName("SUIT_NUM")]
        public decimal SuitNum { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        [ColumnName("SKU_CODE")]
        public string MaterialCode { get; set; }

        [ColumnName("SKU_BARCODE")]
        public string SkuBarcode { get; set; }

        [ColumnName("SPEC")]
        public string Spec { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        [ColumnName("SKU_NAME")]
        public string MaterialName { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        [ColumnName("BRAND")]
        public string Brand { get; set; }

        [ColumnName("UM_CODE")]
        public string UnitCode { get; set; }

        /// <summary>
        /// 计量单位名称
        /// </summary>
        [ColumnName("UM_NAME")]
        public string UnitName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [ColumnName("QTY")]
        public decimal Qty { get; set; }

        public decimal TotalAmount
        {
            get
            {
                return Price * Qty;
            }
        }
        /// <summary>
        /// 已下架数量
        /// </summary>
        [ColumnName("PICK_QTY")]
        public decimal PickQty { get; set; }

        public decimal TotalFactAmount
        {
            get
            {
                return Math.Round(Price1 * PickQty, 2);
            }
        }

        /// <summary>
        /// 效期
        /// </summary>
        [ColumnName("DUE_DATE")]
        public string DueDate { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        [ColumnName("BATCH_NO")]
        public string BatchNO { get; set; }

        /// <summary>
        /// 指定货位
        /// </summary>
        [ColumnName("LOCATION")]
        public string Location { get; set; }

        /// <summary>
        /// 指定状态
        /// </summary>
        [ColumnName("STAT")]
        public string Status { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        [ColumnName("PRICE")]
        public decimal Price1 { get; set; }

        public decimal Price
        {
            get
            {
                return Math.Round(Price1, 2);
            }
        }

        /// <summary>
        /// 最小单位的单价
        /// </summary>
        public decimal MinPrice
        {
            get
            {

                return Math.Round(Price / CastRate, 4);
                
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        [ColumnName("REMARK")]
        public string Remark { get; set; }
        /// <summary>
        /// 整散标记
        /// </summary>
        [ColumnName("IS_CASE")]
        public int IsCase { get; set; }

        public string IsCaseName
        {
            get
            {
                return IsCase == 1 ? "整" : "散";
            }
        }

        /// <summary>
        /// 最小单位和销售单位的转换率
        /// </summary>
        [ColumnName("CAST_RATE")]
        public Int64 CastRate { get; set; }

        /// <summary>
        /// 最小单位拣货数量
        /// </summary>
        [ColumnName("MIN_PICK_QTY")]
        public decimal MinPickQty { get; set; }

        /// <summary>
        /// 最小单位销售单数量
        /// </summary>
        [ColumnName("MIN_SO_QTY")]
        public decimal MinSoQty { get; set; }

        /// <summary>
        /// 最小单位已退数量
        /// </summary>
        [ColumnName("RETURNED_QTY")]
        public decimal ReturnedQty { get; set; }
        /// <summary>
        /// 退货数量（最小单位）
        /// </summary>
        [ColumnName("RETURN_QTY")]
        public decimal ReturnQty { get; set; }

        /// <summary>
        /// 退货单位名称（最小单位）
        /// </summary>
        [ColumnName("MIN_UM_NAME")]
        public string ReturnUnitName { get; set; }

        /// <summary>
        /// 退货单位编码（最小单位）
        /// </summary>
        [ColumnName("MIN_UM_CODE")]
        public string ReturnUnitCode { get; set; }

        /// <summary>
        /// 退货金额
        /// </summary>
        public decimal ReturnPrice 
        {
            get
            {

                return Math.Round(Price / CastRate * ReturnQty, 4);
                
            }
        }
    }
}
