using System;

using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.Entities
{
    /// <summary>
    /// 任务池实体
    /// </summary>
    public class TaskEntity
    {
        private DateTime? _beginDate = null;


        [ColumnName("ID")]
        public int TaskID { get; set; }
        [ColumnName("TASK_TYPE")]
        public string TaskType { get; set; }
        [ColumnName("TaskTypeNo")]
        public int TaskTypeNo { get; set; }
        [ColumnName("ITEM_DESC")]
        public string TaskName { get; set; }
        [ColumnName("TASK_DESC")]
        public string TaskDesc { get; set; }
        [ColumnName("BILL_ID")]
        public int BillID { get; set; }
        [ColumnName("USER_CODE")]
        public string UserCode { get; set; }
        [ColumnName("USER_NAME")]
        public string UserName { get; set; }
        [ColumnName("IS_ONLINE")]
        public string IsOnline { get; set; }
        [ColumnName("QTY")]
        public int Qty { get; set; }
        [ColumnName("CREATE_DATE")]
        public DateTime? CreateDate { get; set; }

        [ColumnName("BILL_NO")]
        public string BillNO { get; set; }
        [ColumnName("BILL_DESC")]
        public string BillDesc { get; set; }

        [ColumnName("BILL_TYPE_DESC")]
        public string BillTypeDess { get; set; }
        [ColumnName("CONFIRM_DATE")]
        public DateTime? ConfirmDate { get; set; }

        public bool HasChecked { get; set; }

        public string TaskQty
        {
            get
            {
                return string.Format("{0} {1}", Qty, TaskDesc);
            }
        }
        /// <summary>
        /// 任务开始时间
        /// </summary>
        public DateTime? BeginDate
        {
            get { return this._beginDate; }
            set
            {
                if (value == DateTime.MinValue)
                    this._beginDate = null;
                else
                    this._beginDate = value;
            }
        }
        /// <summary>
        /// 延时
        /// </summary>
        public double Delay
        {
            get
            {
                if (this.BeginDate == null || this.BeginDate == DateTime.MinValue)
                    return 0.0;
                TimeSpan timeSpan = (TimeSpan)(this.BeginDate - this.CreateDate);
                return timeSpan.TotalMinutes;
            }
        }
        /// <summary>
        /// 用时
        /// </summary>
        public double UseTime
        {
            get
            {
                if (this.BeginDate == null || this.BeginDate == DateTime.MinValue)
                    return 0.0;
                TimeSpan timeSpan = (TimeSpan)(DateTime.Now - this.BeginDate);
                return timeSpan.TotalMinutes;
            }
        }
        /// <summary>
        /// 已完成任务量
        /// </summary>
        [ColumnName("TASK_COUNT")]
        public long CompletedTaskCount { get; set; }
        /// <summary>
        /// 等待时间差（分钟）
        /// </summary>
        [ColumnName("DIFF_TIME")]
        public long DiffTimeValue { get; set; }
        public string DiffTimeStr
        {
            get
            {
                if (this.DiffTimeValue == 0)
                    return null;
                string result = string.Empty;
                if (this.DiffTimeValue % 60 == 0)
                {
                    result = string.Format("{0}小时", (this.DiffTimeValue / 60));
                }
                else
                {
                    result = string.Format("{0}小时{1}分钟", (this.DiffTimeValue / 60), this.DiffTimeValue % 60);
                }
                return result;
            }
        }

        [ColumnName("ROLE_NAME")]
        public string RoleName { get; set; }

        [ColumnName("ROLE_ID")]
        public int RoleID { get; set; }

        [ColumnName("U_ATTRI")]
        public int UserAttri { get; set; }

        [ColumnName("ROLE_ENABLED")]
        public bool RoleEnabled { get; set; }

        public string TaskNameAndUserAttri
        {
            get
            {
                return string.Format("{0}  {1}", this.TaskName, this.UserAttri);
            }
        }

        [ColumnName("CLOSE_DATE")]
        public DateTime? CloseDate { get; set; }

        [ColumnName("IS_CASE")]
        public int? IsCase { get; set; }
        public string IsCaseStr
        {
            get
            {
                if (this.IsCase == 1)
                    return "整";
                else if (this.IsCase == 2)
                    return "散";
                else
                    return "未知";
            }
        }
        [ColumnName("TASK_QTY")]
        public decimal TaskQtyDecimal { get; set; }
        public double HistoryUseTime
        {
            get
            {
                if (this.ConfirmDate == null || this.ConfirmDate == DateTime.MinValue ||
                    this.CloseDate == null || this.CloseDate == DateTime.MinValue)
                    return 0.0;
                TimeSpan timeSpan = (TimeSpan)(this.CloseDate - this.ConfirmDate);
                return timeSpan.TotalMinutes;
            }
        }
    }
}
