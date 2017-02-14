using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.Entities
{
   public class OccupyRecordEntity
    {
        /// <summary>
        /// 主键编号
        /// </summary>
       [ColumnName("OCCUPY_ID")]
       public int OccupyID { get; set; }

       /// <summary>
       /// 库存主键编号
       /// </summary>
       [ColumnName("STOCK_ID")]
       public int StockID { get; set; }

       /// <summary>
       /// 创建人
       /// </summary>
       [ColumnName("CREATOR")]
       public string Creator { get; set; }

       /// <summary>
       /// 创建时间
       /// </summary>
       [ColumnName("CREATEDATE")]
       public DateTime CreateDate { get; set; }

       /// <summary>
       /// 占用数量
       /// </summary>
       [ColumnName("OCCUPY_QTY")]
       public int OccupyQty { get; set; }

       /// <summary>
       /// 占用状态
       /// </summary>
       [ColumnName("STATUS")]
       public string Status { get; set; }

       /// <summary>
       /// 状态
       /// </summary>
       [ColumnName("STATUS_NAME")]
       public string StatusName { get; set; }

       /// <summary>
       /// 备注
       /// </summary>
       [ColumnName("REMARK")]
       public string Remark { get; set; }

       /// <summary>
       /// 货位编号
       /// </summary>
       [ColumnName("LOCATION")]
       public string Location { get; set; }

       /// <summary>
       /// 物料编号
       /// </summary>
       [ColumnName("MATERIAL")]
       public string Material { get; set; }

       /// <summary>
       /// 物料名称
       /// </summary>
       [ColumnName("MATERIAL_NAME")]
       public string MaterialName { get; set; }

       /// <summary>
       /// 批号
       /// </summary>
       [ColumnName("BATCH_NO")]
       public string BatchNO { get; set; }

       /// <summary>
       /// 效期
       /// </summary>
       [ColumnName("DUE_DATE")]
       public string DueDate { get; set; }

       /// <summary>
       /// 组分料名称
       /// </summary>
       [ColumnName("COM_MATERIAL")]
       public string ComMaterial { get; set; }
    }
}
