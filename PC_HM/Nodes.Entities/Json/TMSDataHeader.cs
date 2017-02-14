using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.Entities
{
    /// <summary>
    /// 组别
    /// </summary>
    [Serializable]
    public class TMSDataHeader
    {
        [ColumnName("HEADER_ID")]
        public int HeaderID { get; set; }   
      
        [ColumnName("GROUP_NO")]
        public string id { get; set; }          // 派单id 32位（组别）

        [ColumnName("VH_TYPE")]
        public string car_type { get; set; }    // 车类型

        [ColumnName("START_TIME")]
        public DateTime start_time { get; set; }//开始时间

        [ColumnName("WH_CODE")]
        public string storehouse { get; set; }  // 库房id

        [ColumnName("LOC_STATE")]
        public int LocalState { get; set; }

        [ColumnName("CREATE_DATE")]
        public DateTime CreateDate { get; set; }

        // 市场信息
        public Dictionary<string, TMSDataMarket> order_list { get; set; }

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
