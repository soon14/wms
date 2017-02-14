using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.Entities
{
    public class CountHeaderEntity
    {
        /// <summary>
        /// 单号
        /// </summary>
        [ColumnName("BILL_ID")]
        public int BillID { get; set; }

        /// <summary>
        /// 单据状态
        /// </summary>
        [ColumnName("BILL_STATE")]
        public string BillState { get; set; }

        [ColumnName("STATE_NAME")]
        public string StateName
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        [ColumnName("REMARK")]
        public string Remark { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [ColumnName("CREATOR")]
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [ColumnName("CREATE_DATE")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        [ColumnName("COMPLETE_DATE")]
        public DateTime? CompleteDate { get; set; }

        /// <summary>
        /// 仓库
        /// </summary>
        [ColumnName("WAREHOUSE")]
        public string Warehouse { get; set; }

        [ColumnName("TAG_DESC")]
        public string TagDesc { get; set; }
    }
}
