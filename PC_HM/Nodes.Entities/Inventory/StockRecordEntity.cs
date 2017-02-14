using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;
using System.Globalization;
using Newtonsoft.Json;
using Nodes.Utils;

namespace Nodes.Entities
{
    public class StockRecordEntity
    {
        /// <summary>
        /// 主键编号
        /// </summary>
        [ColumnName("ID")]
        public int StockID { get; set; }

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

        /// <summary>
        /// 物料编号
        /// </summary>
        [ColumnName("SKU_CODE")]
        public string Material { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        [ColumnName("SKU_NAME")]
        public string MaterialName { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        [ColumnName("BATCH_NO")]
        public string BatchNO { get; set; }

        /// <summary>
        /// 效期
        /// </summary>
        [ColumnName("EXP_DATE")]
        public DateTime ExpDate
        {
            get;
            set;
        }

        /// <summary>
        /// 组分料名称
        /// </summary>
        [ColumnName("COM_MATERIAL")]
        public string ComMaterial { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        [ColumnName("QTY")]
        public decimal Qty { get; set; }

        /// <summary>
        /// 已占用数量
        /// </summary>
        [ColumnName("OCCUPY_QTY")]
        public decimal OccupyQty { get; set; }

        /// <summary>
        /// 最近一次入库时间
        /// </summary>
        [ColumnName("LATEST_IN")]
        public DateTime? LastInDate { get; set; }

        /// <summary>
        /// 最近一次出库时间
        /// </summary>
        [ColumnName("LATEST_OUT")]
        public DateTime? LastOutDate { get; set; }

        /// <summary>
        /// 拣货中
        /// </summary>
        [ColumnName("PICKING_QTY")]
        public decimal PickingQty { get; set; }

        public decimal IdleQty
        {
            get
            {
                return Qty - OccupyQty - PickingQty;
            }
        }

        [ColumnName("UM_NAME")]
        public string UnitName { get; set; }

        [ColumnName("SPEC")]
        public string Spec { get; set; }



        /// <summary>
        /// 是否已过期
        /// </summary>
        [JsonIgnore]
        public bool HasExpired
        {
            get
            {
                if (string.IsNullOrEmpty(ExpDate.ToString()))
                    return false;
                else if (ExpDate.CompareTo(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 剩余过期天数
        /// </summary>
        public int? ExpDiffDays
        {
            get
            {
                if (ExpDate != DateTime.MinValue)
                    return ExpDate.Subtract(DateTime.Now).Days;
                else
                    return null;//
            }
        }
        //保质期天数
        [ColumnName("EXP_DAYS")]
        public int ExpDays { get; set; }

        /// <summary>
        /// 是否临期
        /// </summary>
        public decimal IsPassEXP
        {
            get
            {
                //if (ExpDiffDays <= (ExpDays / 2))
                //{
                //    return "临期";
                //}
                //else
                //{
                //    return "正常";
                //}
                if (ExpDiffDays == null || ExpDiffDays == 0||ExpDays==null||ExpDays==0)
                    return 0;
                return Math.Round(ConvertUtil.ToDecimal(((double)ExpDiffDays / (double)ExpDays)), 2);
            }
        
        }
        [ColumnName("IS_ACTIVE")]
        public string LocationIsActive { get; set; }

        [ColumnName("CREATE_DATE")]
        public DateTime CreateDate { get; set; }

        [ColumnName("SKU_QUALITY")]
        public string SkuQuality { get; set; }
    }
}
