using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.Entities
{
    public class OrderSortDetailPrintEntity
    {
        private const string SIGN_SPLIT = "、";

        [ColumnName("WH_NAME")]
        public string Warehouse { get; set; }

        [ColumnName("BILL_ID")]
        public int BillID { get; set; }

        [ColumnName("BILL_NO")]
        public string BillNo { get; set; }
        [ColumnName("CANCEL_FLAG")]
        public int CancelFlag { get; set; }
        public string BillNoAndCancelFlag
        {
            get
            {
                if (this.CancelFlag == 0)
                    return this.BillNoSimple;
                else
                    return string.Format("{0}(已取消)", this.BillNoSimple);
            }
        }
        /// <summary>
        /// 订单编号(后6位)
        /// </summary>
        public string BillNoSimple
        {
            get
            {
                if (!string.IsNullOrEmpty(this.BillNo))
                {
                    if (this.BillNo.Length <= 6)
                        return this.BillNo;
                    else
                        return this.BillNo.Substring(this.BillNo.Length - 6, 6);
                }
                return null;
            }
        }
        /// <summary>
        /// 客户地址
        /// </summary>
        [ColumnName("ADDRESS")]
        public string CustomerAddress { get; set; }
        [ColumnName("C_NAME")]
        public string CustomerName { get; set; }
        public string CustomerNameAndAddress
        {
            get
            {
                return string.Format("编号：{2}{3}【{0}】 {1}", this.CustomerName, this.CustomerAddress, this.BillNo, Environment.NewLine);
            }
        }
        /// <summary>
        /// 装车顺序
        /// </summary>
        [ColumnName("IN_VH_SORT")]
        public int OrderSort { get; set; }
        /// <summary>
        /// 整货箱数
        /// </summary>
        [ColumnName("FULL_COUNT")]
        public int FullCount { get; set; }
        /// <summary>
        /// 物流箱列表
        /// </summary>
        public List<ContainerEntity> BoxList { get; set; }

        public string BoxListStr
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (this.BoxList != null && this.BoxList.Count > 0)
                {
                    foreach (ContainerEntity item in this.BoxList)
                    {
                        sb.AppendFormat("{0}{1}", item.ContainerCode.Substring(item.ContainerCode.Length - 5, 5), SIGN_SPLIT);
                    }
                    sb = sb.Remove(sb.Length - 1, 1);
                }
                return sb.ToString();
            }
        }

        public int BoxListCount { get { return this.BoxList == null ? 0 : this.BoxList.Count; } }
        [ColumnName("WMS_REMARK")]
        public string BillRemark { get; set; }
    }
}
