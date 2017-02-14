using Nodes.Dapper;

namespace Nodes.Entities
{
    public class StatusEntity
    {
        #region Model

        /// <summary>
        /// 状态编码
        /// </summary>
        [ColumnName("COD")]
        public int StatusCode
        {
            set;
            get;
        }

        /// <summary>
        /// 状态名称
        /// </summary>
        [ColumnName("NAM")]
        public string StatusName
        {
            set;
            get;
        } 

        #endregion Model

    }
}

