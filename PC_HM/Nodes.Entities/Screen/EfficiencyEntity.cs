using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.Entities
{
    public class EfficiencyEntity
    {
       

        /// <summary>
        /// 用户名称
        /// </summary>
        [ColumnName("USER_NAME")]
        public string UserName { get; set; }
        /// <summary>
        /// 用户编码
        /// </summary>
        [ColumnName("USER_CODE")]
        public string UserCode { get; set; }

        /// <summary>
        /// 被踢次数
        /// </summary>
        [ColumnName("TIMEOUT_QTY")]
        public long TimeoutQty { get; set; }
        /// <summary>
        /// 任务数
        /// </summary>
        [ColumnName("TASK_QTY")]
        public long TaskCount { get; set; }
        /// <summary>
        /// 拣货任务等待总时长（单位：小时）
        /// </summary>
        [ColumnName("PICK_CONFIRMTDIFF")]
        public decimal PickConfirmDate { get; set; }
        public decimal PickConfirmDateHour 
        {
            get
            {
                return Math.Round(PickConfirmDate / 3600, 2);
            }
        }
        /// <summary>
        /// 拣货任务执行总时长（单位：秒）
        /// </summary>
        [ColumnName("PICK_EXECUTETDIFF")]
        public decimal PickExecuteDate { get; set; }
        /// <summary>
        /// 拣货任务执行总时长（单位：小时）
        /// </summary>
        public decimal PickExecuteDateHour
        {
            get
            {
                return Math.Round(PickExecuteDate / 3600, 2);
            }
        }
        /// <summary>
        /// 拣货数量
        /// </summary>
        [ColumnName("PICK_QTY")]
        public decimal PickQty { get; set; }
        /// <summary>
        /// 拣货效率（件/小时）
        /// </summary>
        public decimal PickEfficiency
        {
            get
            {
                if (PickExecuteDateHour <= 0)
                    return 0;
                else
                    return Math.Round(PickQty / PickExecuteDateHour, 2);
            }
        }
       
        /// <summary>
        /// 盘点任务等待总时长
        /// </summary>
        [ColumnName("COUNT_CONFIRMTDIFF")]
        public decimal CountConfirmDate { get; set; }
        /// <summary>
        /// 盘点任务执行总时长
        /// </summary>
        [ColumnName("COUNT_EXECUTETDIFF")]
        public decimal CountExecuteDate { get; set; }
        /// <summary>
        /// 盘点货位总数
        /// </summary>
        [ColumnName("COUNT_QTY")]
        public decimal CountQty { get; set; }
        /// <summary>
        /// 盘点效率（个货位/小时）
        /// </summary>
        public decimal CountEfficiency
        {
            get
            {
                if (CountExecuteDate <= 0)
                    return 0;
                else
                    return Math.Round(CountQty/(CountExecuteDate /3600) , 2);
            }
        }
        /// <summary>
        /// 补货任务等待总时长
        /// </summary>
        [ColumnName("TRANS_CONFIRMTDIFF")]
        public decimal TransConfirmDate { get; set; }
        /// <summary>
        /// 补货任务执行总时长
        /// </summary>
        [ColumnName("TRANS_EXECUTETDIFF")]
        public decimal TransExecuteDate { get; set; }
        public decimal TransExecuteDateHour
        {
            get { return Math.Round(TransExecuteDate / 3600, 2); }
        }
        /// <summary>
        /// 补货货位数
        /// </summary>
        [ColumnName("TRANS_QTY")]
        public decimal TransQty { get; set; }
        /// <summary>
        /// 补货效率（个货位/小时）
        /// </summary>
        public decimal TransEfficiency
        {
            get
            {
                if (TransExecuteDateHour <= 0)
                    return 0;
                else
                    return Math.Round(TransQty / TransExecuteDateHour, 2);
            }
        }
        

        /// <summary>
        /// 清点总时长（单位：分）
        /// </summary>
        [ColumnName("RECEIVE_EXECUTETDIFF")]
        public decimal ReceiveExecuteDate { get; set; }

        /// <summary>
        /// 清点总时长（单位：小时）
        /// </summary>
        public decimal ReceiveExecuteDateHour 
        {
            get { return Math.Round(ReceiveExecuteDate / 3600, 2); }
        }
        /// <summary>
        /// 清点数量(单位：件)
        /// </summary>
        [ColumnName("RECEIVE_QTY")]
        public decimal ReceiveQty { get; set; }

        public decimal ReceiveEfficiency
        {
            get
            {
                if (ReceiveExecuteDateHour <= 0)
                    return 0;
                else
                    return Math.Round(ReceiveQty / ReceiveExecuteDateHour, 2);
            }
        }

        /// <summary>
        /// 上架总时长（单位：分）
        /// </summary>
        [ColumnName("PUT_EXECUTETDIFF")]
        public decimal PutExecuteDate { get; set; }
        /// <summary>
        /// 上架总时长（单位：小时）
        /// </summary>
        public decimal PutExecuteDateHour
        {
            get { return Math.Round(PutExecuteDate / 3600, 2); }
        }
        /// <summary>
        /// 上架托盘数
        /// </summary>
        [ColumnName("PUT_QTY")]
        public decimal PutQty { get; set; }

        public decimal PutEfficiency
        {
            get
            {
                if (PutExecuteDateHour <= 0)
                    return 0;
                else
                    return Math.Round(PutQty / PutExecuteDateHour, 2);
            }
        }
       
        /// <summary>
        /// 复核时长（单位：秒）
        /// </summary>
        [ColumnName("CHECK_EXECUTETDIFF")]
        public decimal CheckExecuteDate { get; set; }
        /// <summary>
        /// 复核时长（单位：小时）
        /// </summary>
        public decimal CheckExecuteDateHour
        {
            get { return Math.Round(CheckExecuteDate / 3600, 2); }
        }
        /// <summary>
        /// 复核数量
        /// </summary>
        [ColumnName("CHECK_QTY")]
        public decimal CheckQty { get; set; }
        /// <summary>
        /// 复核效率
        /// </summary>
        public decimal CheckEfficiency
        {
            get
            {
                if (CheckExecuteDateHour <= 0)
                    return 0;
                else
                    return Math.Round(CheckQty / CheckExecuteDateHour, 2);
            }
        }
        /// <summary>
        /// 装车时长（秒）
        /// </summary>
        [ColumnName("LOADING_EXECUTETDIFF")]
        public decimal LoadingExecuteDate { get; set; }
        /// <summary>
        /// 装车时长（小时）
        /// </summary>
        public decimal LoadingExecuteDateHour
        {
            get { return Math.Round(LoadingExecuteDate / 3600, 2); }
        }
        /// <summary>
        /// 装车量
        /// </summary>
        [ColumnName("LOADING_QTY")]
        public decimal LoadingQty { get; set; }
        /// <summary>
        /// 装车效率
        /// </summary>
        public decimal LoadingEfficiency
        {
            get
            {
                if (LoadingExecuteDateHour <= 0)
                    return 0;
                else
                    return Math.Round(LoadingQty / LoadingExecuteDateHour, 2);
            }
        }
        
        /// <summary>
        /// 配送时长（秒）
        /// </summary>
        [ColumnName("SEND_EXECUTETDIFF")]
        public decimal SendExecuteDate { get; set; }
        /// <summary>
        /// 配送时长（小时）
        /// </summary>
        public decimal SendExecuteDateHour
        {
            get { return Math.Round(SendExecuteDate / 3600, 2); }
        }
        /// <summary>
        /// 配送件数
        /// </summary>
        [ColumnName("SEND_QTY")]
        public decimal SendQty { get; set; }
        /// <summary>
        /// 配送效率
        /// </summary>
        public decimal SendEfficiency
        {
            get
            {
                if (SendExecuteDateHour <= 0)
                    return 0;
                else
                    return Math.Round(SendQty / SendExecuteDateHour, 2);
            }
        }

        #region --------------------------得 分-------------------------------
        /// <summary>
        /// 常规拣货分数
        /// </summary>
        public decimal NormalPickGrade
        {
            get
            {
                if (PickQty == 0)
                {
                    return 0;
                }
                else
                {
                    return Math.Round(PickEfficiency * GlobalSettings.NormalPickRatio, 2);
                }

            }
        }
        /// <summary>
        /// 补货绩效
        /// </summary>
        public decimal TransGrade
        {
            get
            {
                if (TransQty == 0)
                    return 0;
                else
                    return Math.Round(TransEfficiency * GlobalSettings.ForkliftRatio, 2);
            }
        }
        /// <summary>
        /// 上架绩效
        /// </summary>
        public decimal PutGrade
        {
            get
            {
                if (PutQty == 0)
                    return 0;
                else
                    return Math.Round(PutEfficiency * GlobalSettings.ForkliftRatio, 2);
            }
        }
        /// <summary>
        /// 装车得分
        /// </summary>
        public decimal LoadingGrade
        {
            get
            {
                if (LoadingQty == 0)
                    return 0;
                else
                    return Math.Round(LoadingEfficiency * GlobalSettings.LoadingRatio, 2);
            }
        }
       
        /// <summary>
        /// 配送得分
        /// </summary>
        public decimal SendGrade
        {
            get
            {
                if (SendQty == 0)
                    return 0;
                else
                    return Math.Round(SendEfficiency * GlobalSettings.DispatchingRatio1_221, 2);
            }
        }

        /// <summary>
        /// 综合总分数
        /// </summary>
        public decimal UserAllGrade
        {
            get
            {
                decimal sumALL = NormalPickGrade + PutGrade + TransQty+LoadingGrade+SendGrade;
                if (sumALL == 0)
                {
                    return 0;
                }

                else
                {
                    return sumALL;
                }
            }
        }
        #endregion
    }
}
