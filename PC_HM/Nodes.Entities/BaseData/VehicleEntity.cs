using Nodes.Dapper;

namespace Nodes.Entities
{
    /// <summary>
    /// 车辆信息
    /// </summary>
    public class VehicleEntity
    {
        #region Model

        /// <summary>
        /// 主键
        /// </summary>
        [ColumnName("ID")]
        public int ID
        {
            set;
            get;
        }

        /// <summary>
        /// 车辆内部编码
        /// </summary>
        [ColumnName("VH_CODE")]
        public string VehicleCode
        {
            set;
            get;
        }

        /// <summary>
        /// 车牌号
        /// </summary>
        [ColumnName("VH_NO")]
        public string VehicleNO
        {
            set;
            get;
        }

        /// <summary>
        /// 车辆承载体积
        /// </summary>
        [ColumnName("VH_VOLUME")]
        public decimal VehicleVolume
        {
            set;
            get;
        }

        /// <summary>
        /// 司机编号
        /// </summary>
        [ColumnName("USER_CODE")]
        public string UserCode
        {
            set;
            get;
        }

        /// <summary>
        /// 司机姓名
        /// </summary>
        [ColumnName("USER_NAME")]
        public string UserName
        {
            set;
            get;
        }

        /// <summary>
        /// 司机姓名
        /// </summary>
        [ColumnName("MOBILE_PHONE")]
        public string UserPhone
        {
            set;
            get;
        }

        /// <summary>
        /// 送货路线编号
        /// </summary>
        [ColumnName("RT_CODE")]
        public string RouteCode
        {
            set;
            get;
        }

        /// <summary>
        /// 送货路线描述
        /// </summary>
        [ColumnName("RT_NAME")]
        public string RouteName
        {
            set;
            get;
        }

        /// <summary>
        /// 是否被禁用
        /// </summary>
        [ColumnName("IS_ACTIVE")]
        public string IsActive
        {
            set;
            get;
        }

        /// <summary>
        /// 是否在线：Y/N
        /// </summary>
        [ColumnName("IS_ONLINE")]
        public string IsOnline
        {
            set;
            get;
        }

        public string JianCheng
        {
            get
            {
                return VehicleNO + "  |  " + RouteName;
            }
        }

        /// <summary>
        /// 车辆所属：公司、第三方
        /// </summary>
        [ColumnName("VH_TYPE")]
        public string VhType
        {
            set;
            get;
        }

        [ColumnName("VH_TYPE_DESC")]
        public string VhTypeStr { get; set; }
        [ColumnName("VH_ATTRI")]
        public string VhAttri { get; set; }
        [ColumnName("VH_ATTRI_DESC")]
        public string VhAttriStr { get; set; }

        #endregion Model
    }
}