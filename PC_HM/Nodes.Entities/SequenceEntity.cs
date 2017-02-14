using System;
using Nodes.Dapper;

namespace Nodes.Entities
{
    public class SequenceEntity
    {
        [ColumnName("SEQ")]
        public string SequenceNO { get; set; }

        [ColumnName("MATERIAL")]
        public string Material { get; set; }

        [ColumnName("BATCH_NO")]
        public string BatchNO { get; set; }

        [ColumnName("DUE_DATE")]
        public string DueDate { get; set; }

        [ColumnName("COM_MATERIAL")]
        public string ComMaterial { get; set; }

        [ColumnName("STAT")]
        public string State { get; set; }

        [ColumnName("STATUS_NAME")]
        public string StateName { get; set; }

        [ColumnName("MATERIAL_NAME")]
        public string MaterialName { get; set; }

        [ColumnName("UNQUAL_REASON")]
        public string UnqualReason { get; set; }
    }
}
