using Nodes.Dapper;
using System.Collections.Generic;

namespace Nodes.Entities
{
    public class ZoneEntity
    {
        #region Model

        /// <summary>
        /// 货区编码
        /// </summary>
        [ColumnName("ZN_CODE")]
        public string ZoneCode
        {
            set;
            get;
        }

        /// <summary>
        /// 货区名称
        /// </summary>
        [ColumnName("ZN_NAME")]
        public string ZoneName
        {
            set;
            get;
        }

        /// <summary>
        /// 货区所属仓库编号
        /// </summary>
        [ColumnName("WH_CODE")]
        public string WarehouseCode
        {
            set;
            get;
        }

        /// <summary>
        /// 货区所属仓库名称
        /// </summary>
        [ColumnName("WH_NAME")]
        public string WarehouseName
        {
            set;
            get;
        }

        /// <summary>
        /// 功能区分类代码
        /// </summary>
        [ColumnName("ZT_CODE")]
        public string ZoneTypeCode
        {
            set;
            get;
        }

        /// <summary>
        /// 功能区分类名称
        /// </summary>
        [ColumnName("ZT_NAME")]
        public string ZoneTypeName
        {
            set;
            get;
        }

        /// <summary>
        /// 温控属性代码
        /// </summary>
        [ColumnName("TEMP_CODE")]
        public string TemperatureCode
        {
            set;
            get;
        }        

        /// <summary>
        /// 温控属性名称
        /// </summary>
        [ColumnName("TEMP_NAME")]
        public string TemperatureName
        {
            set;
            get;
        }

        [ColumnName("IS_ACTIVE")]
        public string IsActive
        {
            get;
            set;
        }

        #endregion Model
    }
}

