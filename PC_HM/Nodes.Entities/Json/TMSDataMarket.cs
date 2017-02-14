using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.Entities
{
    /// <summary>
    /// 市场对象
    /// </summary>
    [Serializable]
    public class TMSDataMarket
    {
        [ColumnName("MARKET_ID")]
        public string marketid { get; set; }   // 市场ID

        [ColumnName("GROUP_NO")]
        public decimal x { get; set; }        // 坐标位X

        [ColumnName("VH_TYPE")]
        public decimal y { get; set; }        // 坐标位Y

        [ColumnName("GROUP_NO")]
        public string GroupNo { get; set; }  //组别

        // 订单信息
        public Dictionary<string, TMSDataDetail> order_info { get; set; }

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
