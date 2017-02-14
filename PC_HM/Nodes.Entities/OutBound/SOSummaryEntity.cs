using System;
using Nodes.Dapper;
using Nodes.Utils;
using System.Collections.Generic;

namespace Nodes.Entities
{
    /// <summary>
    /// 到货通知单表头
    /// </summary>
    public class SOSummaryEntity : SOHeaderEntity
    {
        [ColumnName("X_COOR")]
        public decimal Xcoor { get; set; }

        [ColumnName("Y_COOR")]
        public decimal Ycoor { get; set; }

        [ColumnName("AMOUNT")]
        public decimal Amount { get; set; }

        [ColumnName("VOLUME")]
        public decimal Volume { get; set; }

        [ColumnName("RT_CODE")]
        public string RouteCode { get; set; }

        [ColumnName("RT_NAME")]
        public string RouteName { get; set; }

        [ColumnName("BILL_STATE")]
        public string BillState { get; set; }

        [ColumnName("ITEM_DESC")]
        public string BillStateName { get; set; }

        [ColumnName("CT_CODE")]
        public string CtCode { get; set; }

        /// <summary>
        /// 计算与库房之间的距离
        /// </summary>
        [ColumnName("DISTANCE")]
        public decimal  Distance
        {
            get;
            set;
            //get
            //{
            //    return MathUtil.GetDistance((double)WarehouseXcoor, (double)WarehouseYcoor, (double)Xcoor, (double)Ycoor);
            //}
        }

        /// <summary>
        /// 计算与库房之间的偏移方位
        /// </summary>
        public double Bearing
        {
            get
            {
                return -1;
                //return MathUtil.GetDistance((double)WarehouseXcoor, (double)WarehouseYcoor, (double)Xcoor, (double)Ycoor);
            }
        }
        /// <summary>
        /// 物料总件数
        /// </summary>
        [ColumnName("TOTAL_COUNT")]
        public int TotalCount
        {
            get;
            set;
        }
    }
}
