using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;

namespace Nodes.Entities
{
    /// <summary>
    /// 品牌
    /// </summary>
    public class BrandEntity
    {
        /// <summary>
        /// 品牌编码
        /// </summary>
        [ColumnName("BRD_CODE")]
        public string BrandCode
        {
            set;
            get;
        }

        /// <summary>
        /// 品牌名称
        /// </summary>
        [ColumnName("BRD_NAME")]
        public string BrandName
        {
            set;
            get;
        }
    }
}
