using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.Entities
{
    public class CustomFieldEntity
    {
        [ColumnName("ID")]
        public int ID { get; set; }

        [ColumnName("GROUP_NAME")]
        public string GroupName { get; set; }

        [ColumnName("FIELD_NAME")]
        public string FieldName { get; set; }

        [ColumnName("FIELD_DESC")]
        public string FieldDesc { get; set; }

        [ColumnName("IS_ACTIVE")]
        public string IsActive { get; set; }

        [ColumnName("REMARK")]
        public string Remark { get; set; }
    }
}
