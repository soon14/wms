using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities
{
    public class BillStateConst
    {
        #region 采购单状态
        public const string PO_STATE_CODE_DRAFT = "10";
        public const string PO_STATE_DESC_DRAFT = "草稿";

        public const string PO_STATE_CODE_COMMITED = "11";
        public const string PO_STATE_DESC_COMMITED = "待审";

        public const string PO_STATE_CODE_FIRST_APPROVED = "12";
        public const string PO_STATE_DESC_FIRST_APPROVED = "已审(一审)";

        public const string PO_STATE_CODE_SECOND_APPROVED = "13";
        public const string PO_STATE_DESC_SECOND_APPROVED = "已审(二审)";

        public const string PO_STATE_CODE_RECEIVING = "14";
        public const string PO_STATE_DESC_RECEIVING = "正在收货";

        public const string PO_STATE_CODE_COMPLETE = "15";
        public const string PO_STATE_DESC_COMPLETE = "已完成";
        #endregion

        #region 收货单状态
        public const string ASN_STATE_CODE_NOT_ARRIVE = "20";
        public const string ASN_STATE_DESC_NOT_ARRIVE = "等待到货";

        public const string ASN_STATE_CODE_ARRIVED = "21";
        public const string ASN_STATE_DESC_ARRIVED = "等待验收";

        public const string ASN_STATE_CODE_COMPLETE = "27";
        public const string ASN_STATE_DESC_COMPLETE = "收货完成";
        #endregion

    }
}
