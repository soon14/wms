using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.Entities
{
    public class SODetailAttributeEntity
    {
        #region 属性

        [ColumnName("ID")]
        public int ID { get; set; }

        [ColumnName("BILL_ID")]
        public int BillID { get; set; }

        [ColumnName("BILL_NO")]
        public string BillNO { get; set; }

        [ColumnName("TYPE")]
        public int Type { get; set; }

        [ColumnName("TYPE")]
        public string TypeName
        {
            get
            {
                if (Type == 0)
                    return "商品";
                else if (Type == 1)
                    return "套餐";
                else if (Type == 2)
                    return "实物劵";
                else if (Type == 3)
                    return "套餐";
                else if (Type == 4)
                    return "预付";
                else
                    return "其他";

            }
        }

        [ColumnName("SKU_CODE")]
        public int SkuCode { get; set; }

        [ColumnName("SKU_NAME")]
        public string SkuName { get; set; }

        [ColumnName("UM_CODE")]
        public string Um { get; set; }

        [ColumnName("NUM")]
        public string Num { get; set; }

        [ColumnName("BUY_PRICE")]
        public decimal BuyPrice { get; set; }

        [ColumnName("SELL_PRICE")]
        public decimal SellPrice { get; set; }

        [ColumnName("LAST_UPDATETIME")]
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 预付名称
        /// </summary>
        [ColumnName("YUFU_NAME")]
        public string YuFuName { get; set; }

        #endregion

        #region Override Methods
        public override bool Equals(object obj)
        {
            SODetailAttributeEntity attr = obj as SODetailAttributeEntity;
            if (attr == null) return false;
            return attr.ID == this.ID;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}
