using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.Entities
{
    /// <summary>
    /// 明细
    /// </summary>
    [Serializable]
    public class TMSDataDetail
    {
        [ColumnName("GROUP_NO")]
        public string GroupNo { get; set; }

        [ColumnName("BILL_NO")]
        public string orderid { get; set; }  //单据ID

        [ColumnName("IN_SORT")]
        public int sort { get; set; }        //车内顺序

        [ColumnName("WHLOE_QTY")]
        public int zhengnum { get; set; }    //整货件数

        [ColumnName("BULK_QTY")]
        public int sannum { get; set; }      //散货件数

        [ColumnName("DETAIL_ID")]
        public int DetailID { get; set; }    //订单编号

        [ColumnName("MARKET_ID")]
        public string MarketID { get; set; } //市场ID

        [ColumnName("ATTRI_1")]
        public string Attri1 { get; set; }

        [ColumnName("ATTRI_2")]
        public string Attri2 { get; set; }

        [ColumnName("ATTRI_3")]
        public string Attri3 { get; set; }

        [ColumnName("ATTRI_4")]
        public string Attri4 { get; set; }

        [ColumnName("ATTRI_5")]
        public string Attri5 { get; set; }
    }
}
