using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.Entities
{
    /// <summary>
    /// 订单排序记录
    /// </summary>
    [Serializable]
    public class OrderSortEntity
    {
        #region 属性

        /// <summary>
        /// 主键ID
        /// </summary>
        [ColumnName("SO_ID")]
        public int SoID { get; set; }
        /// <summary>
        /// 车次编号
        /// </summary>
        [ColumnName("VEHICLE_NO")]
        public string VehicleNo { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        [ColumnName("BILL_NO")]
        public string BillNo { get; set; }
        /// <summary>
        /// 车内顺序
        /// </summary>
        [ColumnName("IN_VEHICLE_SORT")]
        public int InVehicleSort { get; set; }
        /// <summary>
        /// 件数
        /// </summary>
        [ColumnName("PIECES_QTY")]
        public decimal PiecesQty { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [ColumnName("CREATE_DATE")]
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 排序附加属性（1：订单排序生成；10：生成装车任务生成）
        /// </summary>
        [ColumnName("Attri1")]
        public int Attri1 { get; set; }

        #endregion
    }
}
