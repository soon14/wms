using Nodes.Dapper;

namespace Nodes.Entities
{
    /// <summary>
    /// 公司信息
    /// 注意：被物料继承了
    /// </summary>
    public class CompanyEntity
    {
        [ColumnName("COMPANY_CODE")]
        public string CompanyCode
        {
            get;
            set;
        }

        [ColumnName("COMPANY_NAME")]
        public string CompanyName
        {
            get;
            set;
        }

        [ColumnName("ADDR")]
        public string Address
        {
            get;
            set;
        }

        [ColumnName("PHONE")]
        public string Phone
        {
            get;
            set;
        }

        [ColumnName("FAX")]
        public string Fax
        {
            get;
            set;
        }

        [ColumnName("EMAIL")]
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        /// 邮编
        /// </summary>
        [ColumnName("POSTCODE")]
        public string Postcode
        {
            get;
            set;
        }

        [ColumnName("REMARK")]
        public virtual string Remark
        {
            get;
            set;
        }
    }
}