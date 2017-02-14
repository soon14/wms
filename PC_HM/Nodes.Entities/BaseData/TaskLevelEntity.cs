using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.Entities
{
    [Serializable]
    public class TaskLevelEntity
    {
        [ColumnName("T_ID")]
        public int ID { get; set; }
        [ColumnName("TASK_TYPE")]
        public int TaskType { get; set; }
        [ColumnName("TASK_TYPE_DESC")]
        public string TaskTypeDesc { get; set; }
        [ColumnName("TASK_LEVEL")]
        public int TaskLevel { get; set; }
        [ColumnName("BEGIN_TIME")]
        public DateTime BeginTime { get; set; }
        [ColumnName("END_TIME")]
        public DateTime EndTime { get; set; }
        [ColumnName("DIFF_VALUE")]
        public int DiffValue { get; set; }
    }
}
