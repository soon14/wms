using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities
{
   public class PrintLogEntity
    {
        #region Model
        private string _start_seq;
        private int _qty;
        private string _print_user;
        private DateTime? _print_date;
        private string _typ = "1";
        /// <summary>
        /// 起始流水号
        /// </summary>
        public string START_SEQ
        {
            set { _start_seq = value; }
            get { return _start_seq; }
        }
        /// <summary>
        /// 打印数量
        /// </summary>
        public int QTY
        {
            set { _qty = value; }
            get { return _qty; }
        }
        /// <summary>
        /// 打印人
        /// </summary>
        public string PRINT_USER
        {
            set { _print_user = value; }
            get { return _print_user; }
        }
        /// <summary>
        /// 打印日期
        /// </summary>
        public DateTime? PRINT_DATE
        {
            set { _print_date = value; }
            get { return _print_date; }
        }
        /// <summary>
        /// 分类（1：流水号标签；2：物料标签；3：货位标签；4：入库通知单；5：出库清单）
        /// </summary>
        public string TYP
        {
            set { _typ = value; }
            get { return _typ; }
        }
        #endregion Model
    }
}
