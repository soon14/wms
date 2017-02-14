using Nodes.Dapper;
using System;
using Nodes.Utils;

namespace Nodes.Entities
{
    /// <summary>
    /// 发货单明细信息
    /// </summary>
    public class SODetailReportEntity : SODetailEntity
    {
        public string SkuType
        {
            get;
            set;
        }
        public string SkuTypeName
        {
            get;
            set;
        }

        public string SkuCombName
        {
            get;
            set;
        }

        public decimal PickQtyReport
        {
            get
            {
                if (SkuTypeName == "套装")
                {
                    if (base.Qty == 0 || base.SuitNum == 0)
                        return 0.00M;
                    return Math.Round(Price1 * ConvertUtil.ToDecimal(base.PickQty / (base.Qty / base.SuitNum)), 2);
                }
                return base.TotalFactAmount;
            }
        }

        public string PickQtyReportStr
        {
            get
            {
                string str = "";
                if (SkuTypeName == "套装")
                {
                    if (base.Qty == 0 || base.SuitNum == 0)
                        return "0";
                    str = ConvertUtil.ToString(ConvertUtil.ToInt(base.PickQty / (base.Qty / base.SuitNum)));
                    //str = ConvertUtil.ToString(ConvertUtil.ToInt(PickQtyReport));
                }
                else
                {
                    if (PickQty == 0)
                    {
                        str = "缺货";
                    }
                    else
                    {
                        str = base.PickQty.ToString("f0");
                    }
                }
                return str;
            }
        }

        public string UmName
        {
            get
            {
                if (SkuTypeName == "套装")
                {
                    return "套";
                }
                else
                    return base.UnitName;
            }
        }

        /// <summary>
        /// 单位 与 规格
        /// </summary>
        public string UnitNameAndSpec
        {
            get
            {
                return string.Format("{0}\r\n{1}", this.UmName, base.Spec);
            }
        }
        /// <summary>
        /// 数量 与 单价
        /// </summary>
        public string PickQtyAndPrice
        {
            get
            {
                return string.Format("{0}\r\n{1}", this.PickQtyReportStr, base.Price);
            }
        }
        /// <summary>
        /// 订购量 与 发货量
        /// </summary>
        public string QtyAndPickQty
        {
            get
            {
                if (this.Qty == 0)
                    return "0\r\n0";
                decimal out_result = 0.00m;
                if (decimal.TryParse(this.PickQtyReportStr, out out_result))
                {
                    return string.Format("{0:f0}\r\n{1}", 
                        this.Qty / (this.Qty / this.SuitNum), 
                        out_result.ToString("f0"));
                }
                else
                {
                    return string.Format("{0:f0}\r\n{1}", 
                        this.Qty / (this.Qty / this.SuitNum), 
                        this.PickQtyReportStr);
                }
            }
        }
        /// <summary>
        /// 单价 与 总金额
        /// </summary>
        public string PriceAndPickQtyReport
        {
            get
            {
                return string.Format("{0}\r\n{1}", this.Price, this.PickQtyReport);
            }
        }
    }
}
