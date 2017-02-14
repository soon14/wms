using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;
using Newtonsoft.Json;

namespace Nodes.Entities
{
    public class ReturnDetailsEntity
    {
        /// <summary>
        /// 单据ID，主键
        /// </summary>
        [ColumnName("ID")]
        public int BillID { get; set; }

        /// <summary>
        /// 单据头ID
        /// </summary>
        [ColumnName("BILL_ID")]
        public int HeaderID { get; set; }

        /// <summary>
        /// 行号
        /// </summary>
        [ColumnName("ROW_NO")]
        [JsonIgnore]
        public int RowNo { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        [ColumnName("SKU_CODE")]
        [JsonIgnore]
        public string SkuCode { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        [ColumnName("SKU_NAME")]
        [JsonIgnore]
        public string SkuName { get; set; }

        /// <summary>
        /// 商品条码
        /// </summary>
        [ColumnName("SKU_BARCODE")]
        [JsonIgnore]
        public string SkuBarcode { get; set; }

        /// <summary>
        /// 套装名称
        /// </summary>
        [ColumnName("COM_MATERIAL")]
        [JsonIgnore]
        public string ComMaterial { get; set; }
        [JsonIgnore]
        public string ProductName
        {
            get
            {
                if (String.IsNullOrEmpty(ComMaterial))
                    return SkuName;
                else
                    return String.Format("{0} {1}/{2}({3})", SkuName, Spec, UmName, Qty / SuitNum);
            }
        }
        [JsonIgnore]
        public string SkuTypeName
        {
            get
            {
                if (String.IsNullOrEmpty(ComMaterial))
                    return "单品";
                else
                    return "套餐";
            }
        }
        [JsonIgnore]
        [ColumnName("SUIT_NUM")]
        public decimal SuitNum { get; set; }
        /// <summary>
        /// 销售单位编码
        /// </summary>
        [ColumnName("UM_CODE")]
        [JsonIgnore]
        public string UmCode { get; set; }
        /// <summary>
        /// 销售单位名称
        /// </summary>
        [ColumnName("UM_NAME")]
        [JsonIgnore]
        public string UmName { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        [ColumnName("SPEC")]
        [JsonIgnore]
        public string Spec { get; set; }

        /// <summary>
        /// 效期
        /// </summary>
        [ColumnName("EXP_DATE")]
        [JsonIgnore]
        public string ExpDate { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        [ColumnName("BATCH_NO")]
        [JsonIgnore]
        public string BatchNo { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [ColumnName("REMARK")]
        [JsonIgnore]
        public string Remark { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [ColumnName("IS_DELETED")]
        [JsonIgnore]
        public int IsDelete { get; set; }

        /// <summary>
        /// 更新日期
        /// </summary>
        [ColumnName("LAST_UPDATETIME")]
        [JsonIgnore]
        public DateTime LastUpdateDate { get; set; }

        /// <summary>
        /// 单据状态
        /// </summary>
        [ColumnName("BILL_TYPE")]
        [JsonIgnore]
        public string BillType { get; set; }

        /// <summary>
        /// 发货明细ID
        /// </summary>
        [ColumnName("SO_ID")]
        [JsonIgnore]
        public int SoID { get; set; }

        /// <summary>
        /// 单位转换率
        /// </summary>
        [ColumnName("CAST_RATE")]
        [JsonIgnore]
        public Int32 CastRate { get; set; }

        /// <summary>
        /// 整散标记,1整，其他散
        /// </summary>
        [ColumnName("IS_CASE")]
        [JsonIgnore]
        public int IsCase { get; set; }
        [JsonIgnore]
        public string IsCaseName
        {
            get
            {
                return IsCase == 1 ? "整" : "散";
            }
        }

        /// <summary>
        /// 销售的单价
        /// </summary>
        [ColumnName("PRICE")]
        [JsonIgnore]
        public decimal Price { get; set; }

        /// <summary>
        /// 最小单位的单价
        /// </summary>
        [JsonIgnore]
        public decimal MinPrice
        {
            get
            {
                if (IsCase == 1)
                {
                    return Math.Round(Price / CastRate, 4);
                }
                else
                {
                    return Price;
                }
            }
        }

        /// <summary>
        /// 拣货数量
        /// </summary>
        [ColumnName("QTY")]
        public int Qty { get; set; }

        /// <summary>
        /// 已退数
        /// </summary>
        [ColumnName("RETURNED_QTY")]
        public decimal ReturnedQty { get; set; }

        /// <summary>
        /// 退货数
        /// </summary>
        [ColumnName("RETURN_QTY")]
        public decimal ReturnQty { get; set; }

        /// <summary>
        /// 退货金额
        /// </summary>
        [JsonIgnore]
        public decimal ReturnAmount
        {
            get
            {
                return Math.Round(ReturnQty * MinPrice, 2);
            }
        }

        /// <summary>
        /// 复核数
        /// </summary>
        [ColumnName("MIN_PUT_QTY")]
        public Int64 MinPutQty { get; set; }

        /// <summary>
        /// 基本单位代码
        /// </summary>
        [ColumnName("MIN_UM_CODE")]
        public string ReturnUnitCode { get; set; }

        /// <summary>
        /// 基本单位名称
        /// </summary>
        [ColumnName("MIN_UM_NAME")]
        [JsonIgnore]
        public string ReturnUnitName { get; set; }

        /// <summary>
        /// 清点数
        /// </summary>
        [ColumnName("MIN_CHECK_QTY")]
        public Int64 CheckQty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ColumnName("HAS_PICK_QTY")]
        public Int64 MinPickQty { get; set; }

        /// <summary>
        /// 退货原因
        /// </summary>
        [ColumnName("RETURN_REASON")]
        public string ReturnReasonDesc { get; set; }

        /// <summary>
        /// 明细对应发货单号
        /// </summary>
        [ColumnName("SALEORDER_NO")]
        public string SaleOrderNo { get; set; }
    }
}
