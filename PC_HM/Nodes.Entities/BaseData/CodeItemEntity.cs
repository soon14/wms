using Nodes.Dapper;
using System;

namespace Nodes.Entities
{
    public class CodeItemEntity
    {
        #region Model

        /// <summary>
        /// 代码项编号
        /// </summary>
        [ColumnName("COD")]
        public string Code
        {
            set;
            get;
        }

        /// <summary>
        /// 代码项名称
        /// </summary>
        [ColumnName("NAM")]
        public string Name
        {
            set;
            get;
        }

        #endregion Model
    }
}