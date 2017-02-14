using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.Entities
{
    //托盘状态表功能实体类
    public class TrayStatusTableEntity
    {
        //托盘编号，托盘状态，关联入库单，供应商
        //C.CT_CODE, S.CT_STATE, ST.ITEM_DESC STATE_DESC, S.UNIQUE_CODE,  S.BILL_HEAD_ID, "
        //                 + " wah.BILL_NO AS 'IN_BILL', wsh.BILL_NO AS 'OUT_BILL', "
        //                 + " CIN.C_NAME AS 'IN_CNAME', COUT.C_NAME AS 'OUT_CNAME

        public string CT_CODE { get; set; }//托盘编码

        public string STATE_DESC { get; set; }//状态描述：如空闲        

        public string BILL_TYPE { get; set; }//托盘所关联的单据类型：入库单据 或 出库单据 或 未关联

        //关联单据的编号
        public string BILL_NO
        {
            get
            {
                if (!string.IsNullOrEmpty(IN_BILL))
                {
                    BILL_TYPE = "入库单据";
                    return IN_BILL;
                }
                else if (!string.IsNullOrEmpty(OUT_BILL))
                {
                    BILL_TYPE = "出库单据";
                    return OUT_BILL;
                }
                else
                {
                    BILL_TYPE = "未关联";
                    return null;
                }
            }
        }

        //客户名称
        public string C_NAME
        {
            get
            {
                if (!string.IsNullOrEmpty(IN_BILL))
                {
                    return IN_CNAME;
                }
                else if (!string.IsNullOrEmpty(OUT_BILL))
                {
                    return OUT_CNAME;
                }
                else
                {
                    return null;
                }
            }
        }

        public string IN_BILL { get; set; }

        public string OUT_BILL { get; set; }

        public string IN_CNAME { get; set; }

        public string OUT_CNAME { get; set; }

        public string IN_UCODE { get; set; }

        public string OUT_UCODE { get; set; }
    }
}
