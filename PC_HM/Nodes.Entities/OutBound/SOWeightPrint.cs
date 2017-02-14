using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.Entities
{
    public class SOWeightPrint
    {
        [ColumnName("SKU_NAME")]
        public string SKUName { get; set; }
        [ColumnName("SKU_BARCODE")]
        public string SKUBarcode { get; set; }
        [ColumnName("QTY")]
        public int Qty { get; set; }
        [ColumnName("PICK_QTY")]
        public int PickQty { get; set; }
        [ColumnName("WEIGHT")]
        public decimal Weigth { get; set; }
        [ColumnName("UM_NAME")]
        public string UmName { get; set; }
    }
}
