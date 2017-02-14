using Nodes.Dapper;
using System;

namespace Nodes.Entities
{
    public class LocationEntity : BaseEntity
    {
        #region Model

        /// <summary>
        /// 货位编码
        /// </summary>
        [ColumnName("LC_CODE")]
        public string LocationCode
        {
            set;
            get;
        }

        /// <summary>
        /// 货位名称
        /// </summary>
        [ColumnName("LC_NAME")]
        public string LocationName
        {
            set;
            get;
        }

        /// <summary>
        /// 货位所属货区编号
        /// </summary>
        [ColumnName("ZN_CODE")]
        public string ZoneCode
        {
            set;
            get;
        }

        /// <summary>
        /// 货位所属货区名称
        /// </summary>
        [ColumnName("ZN_NAME")]
        public string ZoneName
        {
            set;
            get;
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

        /// <summary>
        /// 拣货顺序
        /// </summary>
        [ColumnName("SORT_ORDER")]
        public int SortOrder
        {
            set;
            get;
        }

        /// <summary>
        /// 所属组织
        /// </summary>
        [ColumnName("ORG_CODE")]
        public string OrgCode
        {
            set;
            get;
        }

        /// <summary>
        /// 库容下限，0表示不限制
        /// </summary>
        [ColumnName("LOWER_SIZE")]
        public int LowerSize
        {
            set;
            get;
        }

        /// <summary>
        /// 库容上限，0表示不限制
        /// </summary>
        [ColumnName("UPPER_SIZE")]
        public int UpperSize
        {
            set;
            get;
        }

        /// <summary>
        /// 计量单位组ID
        /// </summary>
        [ColumnName("UG_CODE")]
        public string GrpCode { get; set; }

        /// <summary>
        /// 计量单位组名称
        /// </summary>
        [ColumnName("UG_NAME")]
        public string GrpName { get; set; }

        /// <summary>
        /// 计量单位编码
        /// </summary>
        [ColumnName("UM_CODE")]
        public string UnitCode
        {
            set;
            get;
        }

        /// <summary>
        /// 计量单位名称
        /// </summary>
        [ColumnName("UM_NAME")]
        public string UnitName
        {
            set;
            get;
        }

        /// <summary>
        /// 所属仓库编号
        /// </summary>
        [ColumnName("WH_CODE")]
        public string WarehouseCode
        {
            set;
            get;
        }

        /// <summary>
        /// 所属仓库名称
        /// </summary>
        [ColumnName("WH_NAME")]
        public string WarehouseName
        {
            set;
            get;
        }

        [ColumnName("BILL_ID")]
        public int? BillID { get; set; }

        [ColumnName("BILL_STATE")]
        public string BillState { get; set; }

        [ColumnName("COUNT_QTY")]
        public decimal CountQty { get; set; }
        [ColumnName("STOCK_QTY")]
        public decimal StockQty { get; set; }
        [ColumnName("EXP_DATE")]
        public DateTime ExpDate { get; set; }
        [ColumnName("STOCK_EXP_DATE")]
        public DateTime ExpDateStock { get; set; }


        /// <summary>
        /// 拣货通道编码
        /// </summary>
        [ColumnName("CH_CODE")]
        public int Ch_Code
        {
            set;
            get;
        }
        /// <summary>
        /// 拣货通道名称
        /// </summary>
        [ColumnName("CH_NAME")]
        public string Ch_Name
        {
            set;
            get;
        }

        #endregion Model
    }
}

