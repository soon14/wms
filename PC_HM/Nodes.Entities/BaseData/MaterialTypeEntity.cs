using Nodes.Dapper;

namespace Nodes.Entities
{
    /// <summary>
    /// 物料分类
    /// </summary>
    public class MaterialTypeEntity
    {
        #region Model

        /// <summary>
        /// 分类编码
        /// </summary>
        [ColumnName("TYP_CODE")]
        public string MaterialTypeCode
        {
            set;
            get;
        }

        /// <summary>
        /// 分类名称
        /// </summary>
        [ColumnName("TYP_NAME")]
        public string MaterialTypeName
        {
            set;
            get;
        }

        /// <summary>
        /// 存放货区
        /// </summary>
        [ColumnName("ZN_CODE")]
        public string ZoneCode
        {
            set;
            get;
        }

        /// <summary>
        /// 存放货区
        /// </summary>
        [ColumnName("ZN_NAME")]
        public string ZoneName
        {
            set;
            get;
        }

        #endregion Model
    }
}

