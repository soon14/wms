using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities
{
    public class BillSKUNum
    {
        bool hasChecked = false;
        /// <summary>
        /// 选中，用于表格操作中的选中，不绑定到数据库
        /// </summary>
        public bool HasChecked 
        {    get { return hasChecked; }
            set { hasChecked = value; } 
        }
        public string SKUName { get; set; }
        public string SKUCode { get; set; }
        public string Spec { get; set; }
        public string UmName { get; set; }
        public string UmCode { get; set; }
        public decimal Qty { get; set; }
        public decimal BillQty { get; set; }
        public decimal TotalQty { get; set; }
        public decimal StockTotalQty { get; set; }
        public decimal BackupQty { get; set; }
        public int IsCase { get; set; }
        public int Flag { get; set; }
        public string AdviceQty { get; set; }
        public string AdviceUmName { get; set; }

        public string IsCaseName
        {
            get
            {
                if (this.IsCase == 1)
                    return "整";
                else
                    return "散";
            }
        }
        public string State
        {
            get
            {
                if (BillQty > TotalQty)
                {
                    return "N";
                }
                else
                {
                    return "Y";
                }
            }
        }

        public string IsFlag
        {
            get
            {
                if (Flag == 1)
                {
                    return "正在补货";
                }
                else
                {
                    return " ";
                }
            }
        }

        public string AdiceQtyUmName 
        {
            get 
            {
                return  AdviceQty+" /" + AdviceUmName;
            }
        }
    }
}
