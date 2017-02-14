using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Reports
{
    public class JsonSearchStockAccountResult
    {
        #region 0-10
        /// <summary>
        /// 时间
        /// </summary>
        public string checkTime
        {
            get;
            set;
        }
        /// <summary>
        /// 商品ID
        /// </summary>
        public string skuCode
        {
            get;
            set;
        }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string skuName 
        {
            get;
            set;
        }
        /// <summary>
        /// 单位
        /// </summary>
        public string umName
        {
            get;
            set;
        }
        /// <summary>
        /// 期初库存
        /// </summary>
        public string beginQty
        {
            get;
            set;
        }
        /// <summary>
        /// 期末库存
        /// </summary>
        public string endQty
        {
            get;
            set;
        }
        /// <summary>
        /// 采购入库
        /// </summary>
        public string enterAsnQty
        {
            get;
            set;
        }
        /// <summary>
        /// 销售退货入库
        /// </summary>
        public string enterCrnQty
        {
            get;
            set;
        }
        /// <summary>
        /// 调拨入库
        /// </summary>
        public string enterTransQty
        {
            get;
            set;
        }
        /// <summary>
        /// 采购换货入库
        /// </summary>
        public string enterExchangeQty
        {
            get;
            set;
        }
        #endregion

        #region 11-20
        /// <summary>
        /// 采购退货入库
        /// </summary>
        public string enterReturnQty
        {
            get;
            set;
        }
        /// <summary>
        /// 盘盈入库
        /// </summary>
        public string enterCountPlusQty
        {
            get;
            set;
        }
        /// <summary>
        /// 其他入库
        /// </summary>
        public string enterOtherQty
        {
            get;
            set;
        }
        /// <summary>
        /// 销售出库
        /// </summary>
        public string outSalesQty
        {
            get;
            set;
        }
        /// <summary>
        /// 退货调拨出库
        /// </summary>
        public string outReturnTransQty
        {
            get;
            set;
        }
        /// <summary>
        /// 正常调拨出库
        /// </summary>
        public string outTransQty
        {
            get;
            set;
        }
        /// <summary>
        /// 采购退货出库
        /// </summary>
        public string outAsnReturnQty
        {
            get;
            set;
        }
        /// <summary>
        /// 报损出库
        /// </summary>
        public string outBadQty
        {
            get;
            set;
        }
        /// <summary>
        /// 内卖出库
        /// </summary>
        public string outInsideSaleQty
        {
            get;
            set;
        }
        /// <summary>
        /// 折扣出库
        /// </summary>
        public string outDiscount
        {
            get;
            set;
        }
        #endregion

        #region 21-27
        /// <summary>
        /// 采购换货出库
        /// </summary>
        public string outExchangeQty
        {
            get;
            set;
        }
        /// <summary>
        /// 内领出库
        /// </summary>
        public string outUseQty
        {
            get;
            set;
        }
        /// <summary>
        /// 取消订单调拨出库
        /// </summary>
        public string outCancelQty
        {
            get;
            set;
        }
        /// <summary>
        /// 盘亏出库
        /// </summary>
        public string outCountMinusQty
        {
            get;
            set;
        }
        /// <summary>
        /// 其他出库
        /// </summary>
        public string outOtherQty
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string id
        {
            get;
            set;
        }
        /// <summary>
        /// 核对昨日时间
        /// </summary>
        public string chekDate
        {
            get;
            set;
        }
        #endregion

        /// <summary>
        /// 差异
        /// </summary>
        public int diff
        {
            get;
            set;
        }
    }
}
