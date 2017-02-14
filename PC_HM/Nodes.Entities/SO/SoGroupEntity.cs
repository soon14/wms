using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.Entities
{
    public class SoGroupEntity
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [ColumnName("BILL_ID")]
        public int BillID { get; set; }

        /// <summary>
        /// 单据编号
        /// </summary>
        [ColumnName("BILL_NO")]
        public string BillNO { get; set; }

        /// <summary>
        /// 单据类型
        /// </summary>
        [ColumnName("BILL_TYPE")]
        public string BillType { get; set; }

        /// <summary>
        /// 单据状态
        /// </summary>
        [ColumnName("BILL_STATE")]
        public string BillState { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        [ColumnName("SKU_CODE")]
        public string SKUCode { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        [ColumnName("SKU_NAME")]
        public string SKUName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [ColumnName("QTY")]
        public decimal Qty { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        [ColumnName("UM_NAME")]
        public string UMName { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        [ColumnName("SPEC")]
        public string Spec { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        [ColumnName("PRICE")]
        public decimal Price { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        [ColumnName("C_NAME")]
        public string CustomerName { get; set; }

        /// <summary>
        /// 客户地址
        /// </summary>
        [ColumnName("ADDRESS")]
        public string CustomerAddress { get; set; }

        /// <summary>
        /// 区域分类
        /// </summary>
        [ColumnName("POSITION_TYPE")]
        public string PositionType { get; set; }

        /// <summary>
        /// 所属库房
        /// </summary>
        [ColumnName("WH_CODE")]
        public string WarehouseCode { get; set; }
    }
}
