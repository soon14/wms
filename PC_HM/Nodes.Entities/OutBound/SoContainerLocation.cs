using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.Entities.OutBound
{
    public class SoContainerLocation
    {

        //[ColumnName("ID")]
        //public int TaskID { get; set; }
        //[ColumnName("TASK_TYPE")]
        //public string TaskType { get; set; }
        [ColumnName("CTL_NAME")]
        public string CTLName { get; set; }

        public string CTLName_Old { get; set; }
        [ColumnName("ITEM_DESC1")]
        public string CTLState { get; set; }
        [ColumnName("ITEM_DESC2")]
        public string CTlType { get; set; }

    }
}
