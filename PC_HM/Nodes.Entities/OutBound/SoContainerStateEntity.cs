using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.Entities.OutBound
{
    public class SoContainerStateEntity
    {

        [ColumnName("CTL_NAME")]
        public string CTLName { get; set; }

        public string CTLName_old { get; set; }
        [ColumnName("ITEM_DESC")]
        public string CTLState { get; set; }
        [ColumnName("ITEM_DESC")]
        public string BillNo { get; set; }
        [ColumnName("ITEM_DESC")]
        public string CTCODE { get; set; }
        [ColumnName("ITEM_DESC")]
        public string AsoTime { get; set; }
        [ColumnName("ITEM_DESC")]
        public string BillState { get; set; }
        [ColumnName("ITEM_DESC")]
        public string CT_State { get; set; }

    }
}
