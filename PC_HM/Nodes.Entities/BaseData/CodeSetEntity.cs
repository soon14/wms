using Nodes.Dapper;

namespace Nodes.Entities
{
    public class CodeSetEntity
    {
        #region Model

        /// <summary>
        /// 代码集编号
        /// </summary>
        [ColumnName("COD")]
        public string Code
        {
            set;
            get;
        }

        /// <summary>
        /// 代码集名称
        /// </summary>
        [ColumnName("NAM")]
        public string Name
        {
            set;
            get;
        }

        /// <summary>
        /// 代码集类型
        /// </summary>
        [ColumnName("CODE_TYPE")]
        public string CodeType
        {
            set;
            get;
        }

        /// <summary>
        /// 备注
        /// </summary>
        [ColumnName("REMARK")]
        public string Remark
        {
            set;
            get;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        [ColumnName("CREATE_DATE")]
        public string CreateDate
        {
            set;
            get;
        }

        /// <summary>
        /// 创建人
        /// </summary>
        [ColumnName("CREATE_USER")]
        public string CreateUser
        {
            set;
            get;
        }

        /// <summary>
        /// 是否停用名称
        /// </summary>
        [ColumnName("IS_ACTIVE")]
        public int IsActive
        {
            set;
            get;
        }

        /// <summary>
        ///是否停用名称
        /// </summary>
        public string IsActiveName
        {
            get
            {
                if (IsActive == 1)
                    return "正常";
                else
                    return "停用";
            }
        }

        #endregion Model
    }
}