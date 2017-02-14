using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.Entities
{
    public class PickPlanEntity : IEqualityComparer<PickPlanEntity>
    {
        [ColumnName("BILL_ID")]
        public int BillID { get; set; }

        [ColumnName("BILL_NO")]
        public string BillNO { get; set; }

        [ColumnName("DETAIL_ID")]
        public int DetailID { get; set; }

        [ColumnName("STOCK_ID")]
        public int STOCK_ID { get; set; }

        [ColumnName("LC_CODE")]
        public string Location { get; set; }

        /// <summary>
        /// 订购量
        /// </summary>
        [ColumnName("QTY")]
        public decimal Qty { get; set; }

        /// <summary>
        /// 可用库存
        /// </summary>
        [ColumnName("STOCK_QTY")]
        public decimal StockQty { get; set; }

        /// <summary>
        /// 备用字段
        /// </summary>
        public decimal DisableQty { get; set; }
        /// <summary>
        /// 越库区库存量
        /// </summary>
        public decimal DisableQty2 { get; set; }
        /// <summary>
        /// 安全库存
        /// </summary>
        [ColumnName("SECURITY_QTY")]
        public int SecurityQty { get; set; }

        [ColumnName("SKU_CODE")]
        public string Material { get; set; }

        [ColumnName("SKU_NAME")]
        public string MaterialName { get; set; }

        /// <summary>
        /// 库存单位转为销售单位时的换算倍数
        /// </summary>
        [ColumnName("SALE_TRANS_VALUE")]
        public int SaleUnitTransValue { get; set; }

        /// <summary>
        /// 订单明细行中的销售单位
        /// </summary>
        [ColumnName("SALE_UNIT")]
        public string SaleUnit { get; set; }

        [ColumnName("COM_MATERIAL")]
        public string ComMaterial { get; set; }

        /// <summary>
        /// 库存单位
        /// </summary>
        [ColumnName("UM_CODE")]
        public string UnitCode { get; set; }

        /// <summary>
        /// 库存单位
        /// </summary>
        [ColumnName("UM_NAME")]
        public string UnitName { get; set; }

        public string StockUnitQty
        {
            get
            {
                return string.Format("{0:N0}({1})", Qty, UnitName);
            }
        }

        public string SaleUnitQty
        {
            get
            {
                if (SaleUnitTransValue > 1)
                    return string.Format("{0}({1})", Qty / SaleUnitTransValue, SaleUnit);
                else
                    return StockUnitQty;
            }
        }

        [ColumnName("C_NAME")]
        public string CustomerName { get; set; }

        [ColumnName("SKU_BARCODE")]
        public string SkuBarcode { get; set; }

        [ColumnName("ROW_NO")]
        public int RowNO { get; set; }

        [ColumnName("EXP_DATE")]
        public DateTime ExpDate { get; set; }

        [ColumnName("CREATE_DATE")]
        public DateTime CreateData { get; set; }

        [ColumnName("CREATOR")]
        public string Creator { get; set; }

        [ColumnName("IS_CASE")]
        public int IsCase { get; set; }

        [ColumnName("PICK_ZN_TYPE")]
        public string PickZnType { get; set; }
        [ColumnName("SALE_QTY")]
        public int SaleQty { get; set; }

        #region IEqualityComparer<PickPlanEntity> 成员

        bool IEqualityComparer<PickPlanEntity>.Equals(PickPlanEntity x, PickPlanEntity y)
        {
            return x.BillID == y.BillID && x.DetailID == y.DetailID;
        }

        int IEqualityComparer<PickPlanEntity>.GetHashCode(PickPlanEntity obj)
        {
            return obj.ToString().GetHashCode();
        }

        #endregion
    }
}
