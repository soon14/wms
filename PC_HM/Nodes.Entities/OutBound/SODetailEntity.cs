using Nodes.Dapper;
using System;
using System.Collections.Generic;

namespace Nodes.Entities
{
    /// <summary>
    /// 发货单明细信息
    /// </summary>
    public class SODetailEntity : IEqualityComparer<SODetailEntity>
    {
        /// <summary>
        /// 主键编号
        /// </summary>
        [ColumnName("DETAIL_ID")]
        public int DetailID { get; set; }

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

        [ColumnName("SPEC")]
        public string Spec { get; set; }

        /// <summary>
        /// 组分料编号
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
                {
                    if (SuitNum == 0)
                        return String.Format("{0} {1}/{2}(0)", MaterialName, Spec, UnitName);
                    return String.Format("{0} {1}/{2}({3})", MaterialName, Spec, UnitName, Qty / SuitNum);
                }
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

        /// <summary>
        /// 物料编码
        /// </summary>
        [ColumnName("SKU_CODE")]
        public string MaterialCode { get; set; }

        [ColumnName("SKU_BARCODE")]
        public string SkuBarcode { get; set; }

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
                return Math.Round( Price1 * PickQty,2);
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

        ///// <summary>
        ///// 指定状态，可以为null
        ///// </summary>
        //public string StatusName
        //{
        //    get
        //    {
        //        if (Status == SysCodeConstant.SEQ_STATUS_UNQUALIFIED)
        //            return SysCodeConstant.SEQ_STATUS_UNQUALIFIED_DESC;
        //        else if (Status == SysCodeConstant.SEQ_STATUS_QUALIFIED)
        //            return SysCodeConstant.SEQ_STATUS_QUALIFIED_DESC;
        //        else
        //            return null;
        //} }

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
                return IsCase == 2 ? "散" : "整";
            }
        }

        [ColumnName("SUIT_NUM")]
        public decimal SuitNum { get; set; }
        /// <summary>
        /// 应返回数
        /// </summary>
        public decimal ReturnQty { get; set; }

        #region IEqualityComparer<SOSummaryEntity> 成员

        bool IEqualityComparer<SODetailEntity>.Equals(SODetailEntity x, SODetailEntity y)
        {
            return x.BillID == y.BillID && x.RowNO == y.RowNO && x.MaterialCode == y.MaterialCode;
        }

        int IEqualityComparer<SODetailEntity>.GetHashCode(SODetailEntity obj)
        {
            return obj.ToString().GetHashCode();
        }

        #endregion
    }
}
