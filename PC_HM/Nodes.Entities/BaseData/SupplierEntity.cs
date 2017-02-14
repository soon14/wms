using Nodes.Dapper;
using System;

namespace Nodes.Entities
{
    public class SupplierEntity : BaseEntity
    {
        #region Model

        /// <summary>
        /// 供应商编码
        /// </summary>
        [ColumnName("S_CODE")]
        public string SupplierCode
        {
            set;
            get;
        }

        /// <summary>
        /// 供应商名称
        /// </summary>
        [ColumnName("S_NAME")]
        public string SupplierName
        {
            set;
            get;
        }

        /// <summary>
        /// 供应商简称
        /// </summary>
        [ColumnName("NAME_S")]
        public string SupplierNameS
        {
            set;
            get;
        }

        /// <summary>
        /// 供应商名称拼音缩写
        /// </summary>
        [ColumnName("NAME_PY")]
        public string SupplierNamePY
        {
            set;
            get;
        }

        [ColumnName("SORT_ORDER")]
        public int SortOrder
        {
            set;
            get;
        }

        /// <summary>
        /// 区域ID
        /// </summary>
        [ColumnName("AREA_ID")]
        public string AreaID
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        [ColumnName("AR_NAME")]
        public string AreaName
        {
            set;
            get;
        }

        /// <summary>
        /// 联系人
        /// </summary>
        [ColumnName("CONTACT")]
        public string ContactName
        {
            set;
            get;
        }

        /// <summary>
        /// 联系电话
        /// </summary>
        [ColumnName("PHONE")]
        public string Phone
        {
            set;
            get;
        }

        /// <summary>
        /// 地址
        /// </summary>
        [ColumnName("ADDRESS")]
        public string Address
        {
            set;
            get;
        }

        /// <summary>
        /// 邮编
        /// </summary>
        [ColumnName("POSTCODE")]
        public string Postcode
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
        /// 操作人
        /// </summary>
        [ColumnName("UPDATE_BY")]
        public string UpdateBy
        {
            set;
            get;
        }

        /// <summary>
        /// 更新时间
        /// </summary>
        [ColumnName("UPDATE_DATE")]
        public DateTime UpdateDate
        {
            set;
            get;
        }
        #endregion Model
    }
}

