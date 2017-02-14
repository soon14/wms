using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.Entities.BaseData
{
    public class RecLocationEntity 
    {
        /// <summary>
        /// 推荐货位
        /// </summary>
        [ColumnName("RECLOC")]
        public string RecLoc { get; set; }

        /// <summary>
        /// 货位
        /// </summary>
        [ColumnName("LC_CODE")]
        public string Location { get; set; }

        /// <summary>
        /// 货区名称
        /// </summary>
        [ColumnName("ZN_NAME")]
        public string ZnName { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        [ColumnName("SKU_CODE")]
        public string SkuCode { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [ColumnName("SKU_NAME")]
        public string SkuName { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        [ColumnName("SPEC")]
        public string Spec { get; set; }
    }
}
