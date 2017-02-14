using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.Entities
{
    public class CountDetailEntity
    {
        /// <summary>
        /// 主键编号
        /// </summary>
        [ColumnName("ID")]
        public int ID { get; set; }

        /// <summary>
        /// 盘点单编号
        /// </summary>
        [ColumnName("BILL_ID")]
        public int BillID { get; set; }

        /// <summary>
        /// 货区编号
        /// </summary>
        [ColumnName("ZN_CODE")]
        public string ZoneCode { get; set; }

        /// <summary>
        /// 货区名称
        /// </summary>
        [ColumnName("ZN_NAME")]
        public string ZoneName { get; set; }

        /// <summary>
        /// 货位编号
        /// </summary>
        [ColumnName("LC_CODE")]
        public string Location { get; set; }

        [ColumnName("LC_STATE")]
        public string LocationState { get; set; }

        /// <summary>
        /// 分派给who
        /// </summary>
        [ColumnName("USER_CODE")]
        public string UserCode { get; set; }

        /// <summary>
        /// 分派给who
        /// </summary>
        [ColumnName("USER_NAME")]
        public string UserName { get; set; }

        /// <summary>
        /// 物料编号
        /// </summary>
        [ColumnName("SKU_CODE")]
        public string MaterialCode { get; set; }

        [ColumnName("SPEC")]
        public string Spec { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        [ColumnName("SKU_NAME")]
        public string MaterialName { get; set; }

        /// <summary>
        /// 组分料名称
        /// </summary>
        [ColumnName("COM_MATERIAL")]
        public string ComMaterial { get; set; }

        /// <summary>
        /// 盘点数量
        /// </summary>
        [ColumnName("QTY")]
        public decimal Qty { get; set; }

        /// <summary>
        /// 盘点时间
        /// </summary>
        [ColumnName("CREATE_DATE")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 实盘数量
        /// </summary>
        [ColumnName("REAL_QTY")]
        public decimal? RealQty { get; set; }

        public decimal? DifferenceQty
        {
            get;
            set;
        }

        /// <summary>
        /// 通道编号
        /// </summary>
        [ColumnName("PASSAGE_CODE")]
        public string PassageCode
        {
            set;
            get;
        }

        /// <summary>
        /// 层号
        /// </summary>
        [ColumnName("FLOOR_CODE")]
        public string FloorCode
        {
            set;
            get;
        }

        /// <summary>
        /// 货架号
        /// </summary>
        [ColumnName("SHELF_CODE")]
        public string ShelfCode
        {
            set;
            get;
        }

        /// <summary>
        /// 货位号（最小的编号了）
        /// </summary>
        [ColumnName("CELL_CODE")]
        public string CellCode
        {
            set;
            get;
        }
    }
}
