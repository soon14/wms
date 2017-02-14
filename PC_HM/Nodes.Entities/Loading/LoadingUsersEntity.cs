using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.Entities
{
    [Serializable]
    public class LoadingUserEntity
    {
        [ColumnName("ID")]
        public int ID { get; set; }
        [ColumnName("VH_TRAIN_NO")]
        public string LoadingNO { get; set; }
        [ColumnName("USER_NAME")]
        public string UserName { get; set; }
        [ColumnName("USER_CODE")]
        public string UserCode { get; set; }
        [ColumnName("UPDATE_DATE")]
        public DateTime UpdateDate { get; set; }
        [ColumnName("ATTRI1")]
        public string TaskType { get; set; }
        [ColumnName("ITEM_DESC")]
        public string TaskDesc { get; set; }
    }
}
