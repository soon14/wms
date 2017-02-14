using Nodes.Dapper;
using System;
using Nodes.Utils;

namespace Nodes.Entities
{
    public class CustomerEntity : BaseEntity
    {
        #region Model
        /// <summary>
        /// 客户编码
        /// </summary>
        [ColumnName("C_CODE")]
        public string CustomerCode
        {
            set;
            get;
        }

        /// <summary>
        /// 客户名称
        /// </summary>
        [ColumnName("C_NAME")]
        public string CustomerName
        {
            set;
            get;
        }

        /// <summary>
        /// 客户简称
        /// </summary>
        [ColumnName("NAME_S")]
        public string CustomerNameS
        {
            set;
            get;
        }

        /// <summary>
        /// 客户简称
        /// </summary>
        public string CustomerNamePY
        {
            get
            {
                // return PinYin.GetCapital(CustomerName);
                return CustomerName;
            }
        }

        /// <summary>
        /// 客户所属区域
        /// </summary>
        [ColumnName("AREA_ID")]
        public string AreaID
        {
            set;
            get;
        }
        /// <summary>
        /// 区域名称
        /// </summary>
        [ColumnName("AR_NAME")]
        public string AreaName
        {
            set;
            get;
        }

        /// <summary>
        /// 是否启用
        /// </summary>
        [ColumnName("IS_ACTIVE")]
        public string IsActive
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        [ColumnName("SORT_ORDER")]
        public int SortOrder
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

        [ColumnName("IS_OWN")]
        public string isOwn
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
        /// 联系人
        /// </summary>
        [ColumnName("CONTACT")]
        public string Contact
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
        /// 邮编
        /// </summary>
        [ColumnName("POSTCODE")]
        public string PostCode
        {
            set;
            get;
        }
        /// <summary>
        /// 纬度
        /// </summary>
        [ColumnName("X_CODE")]
        public string XCode
        {
            set;
            get;
        }

        /// <summary>
        /// 经度
        /// </summary>
        [ColumnName("Y_CODE")]
        public string YCode
        {
            set;
            get;
        }

        /// <summary>
        /// 路线编号
        /// </summary>
        [ColumnName("RT_CODE")]
        public string RouteCode
        {
            set;
            get;
        }

        [ColumnName("RT_NAME")]
        public string RouteName
        {
            set;
            get;
        }

        /// <summary>
        /// 库房编码
        /// </summary>
        [ColumnName("WH_CODE")]
        public string WHCode
        {
            set;
            get;
        }

        /// <summary>
        /// 库房名称
        /// </summary>
        [ColumnName("WH_NAME")]
        public string WHName
        {
            set;
            get;
        }
        /// <summary>
        /// 距离
        /// </summary>
        [ColumnName("DISTANCE")]
        public decimal Distance
        {
            set;
            get;
        }
        #endregion Model
    }
}

