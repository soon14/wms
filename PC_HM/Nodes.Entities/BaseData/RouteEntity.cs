using Nodes.Dapper;

namespace Nodes.Entities
{
    /// <summary>
    /// 计量单位
    /// </summary>
    public class RouteEntity
    {
        #region Model

        /// <summary>
        /// 计量单位编码
        /// </summary>
        [ColumnName("RT_CODE")]
        public string RouteCode
        {
            set;
            get;
        }

        /// <summary>
        /// 计量单位名称
        /// </summary>
        [ColumnName("RT_NAME")]
        public string RouteName
        {
            set;
            get;
        }

        #endregion Model
    }

}