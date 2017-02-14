using Nodes.Dapper;

namespace Nodes.Entities
{
    public class ProvinceEntity
    {
        #region Model

        /// <summary>
        /// 省份编码
        /// </summary>
        [ColumnName("PROV_CODE")]
        public string ProvinceCode
        {
            set;
            get;
        }

        /// <summary>
        /// 省份名称
        /// </summary>
        [ColumnName("PROV_NAME")]
        public string ProvinceName
        {
            set;
            get;
        }

        /// <summary>
        /// 简称，例如广西简称桂
        /// </summary>
        [ColumnName("ALIASNAME")]
        public string AliasName
        {
            set;
            get;
        }

        /// <summary>
        /// 区号
        /// </summary>
        [ColumnName("AREACODE")]
        public string AreaCode
        {
            set;
            get;
        }

        /// <summary>
        /// 省会
        /// </summary>
        [ColumnName("CAPITAL")]
        public string Capital
        {
            set;
            get;
        }

        /// <summary>
        /// 名称的拼音缩写，例如北京市-》BJ
        /// </summary>
        [ColumnName("NAME_PY")]
        public string NamePY
        {
            set;
            get;
        } 

        #endregion Model
    }
}

