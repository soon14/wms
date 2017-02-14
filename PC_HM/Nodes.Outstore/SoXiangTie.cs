using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.SO
{
    public class SoXiangTie
    {
        public SoXiangTie_Header Header { get; set; }

        public List<SoXiangTie_Detail> Details { get; set; }
    }

    public class SoXiangTie_Header
    {
        public int BILL_ID { get; set; }
        public string C_CODE { get; set; }
        public string C_NAME { get; set; }
    }

    public class SoXiangTie_Detail
    {
        public int BILL_ID { get; set; }

        public string CT_CODE { get; set; }

        public int PICK_ID { get; set; }

        public string SKU_CODE { get; set; }

        public string SKU_NAME { get; set; }

        public decimal PICK_QTY { get; set; }

        public string UM_NAME { get; set; }
    }
}
