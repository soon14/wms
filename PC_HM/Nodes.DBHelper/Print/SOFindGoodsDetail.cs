using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.DBHelper.Print
{
    public class SOFindGoodsDetail
    {
        [ColumnName("BILL_ID")]
        public int BillID { get; set; }
        [ColumnName("BILL_NO")]
        public string BillNo { get; set; }
        [ColumnName("LOADING_SORT")]
        public int LoadingSort { get; set; }
        [ColumnName("ZHENG_NUM")]
        public int ZhengNum { get; set; }
        [ColumnName("SAN_NUM")]
        public int SanNum { get; set; }
        [ColumnName("C_NAME")]
        public string CustomerName { get; set; }
        [ColumnName("ADDRESS")]
        public string CustomerAddress { get; set; }
        [ColumnName("DELAYMARK")]
        public int DelayMark { get; set; }
    }
}
