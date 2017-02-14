using Nodes.Dapper;

namespace Nodes.Entities
{
    /// <summary>
    /// 移动终端: WM_PDA
    /// </summary>
    public class PDAEntity
    {
        /// <summary>
        /// 编码
        /// </summary>
        [ColumnName("PDA_CODE")]
        public string PDACode
        {
            set;
            get;
        }

        /// <summary>
        /// 名称
        /// </summary>
        [ColumnName("PDA_NAME")]
        public string PDAName
        {
            set;
            get;
        }

        /// <summary>
        /// 所属仓库
        /// </summary>
        [ColumnName("WH_CODE")]
        public string WarehouseCode
        {
            set;
            get;
        }

        /// <summary>
        /// 所属仓库名称
        /// </summary>
        [ColumnName("WH_NAME")]
        public string WarehouseName
        {
            set;
            get;
        }

        /// <summary>
        /// IP地址
        /// </summary>
        [ColumnName("IP_ADDRESS")]
        public string IpAddress
        {
            set;
            get;
        }

        [ColumnName("IS_ACTIVE")]
        public string IsActive
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
                if (IsActive == "Y")
                    return "正常";
                else
                    return "停用";
            }
        }
    }
}