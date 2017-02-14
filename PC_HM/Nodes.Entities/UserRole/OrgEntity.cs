using Nodes.Dapper;

namespace Nodes.Entities
{
    /// <summary>
    /// 组织信息
    /// 注意：被用户信息继承了
    /// 对应表：ORGANIZATIONS
    /// </summary>
    public class OrgEntity : BaseEntity
    {
        [ColumnName("ORG_CODE")]
        public string OrgCode
        {
            get;
            set;
        }

        [ColumnName("ORG_NAME")]
        public string OrgName
        {
            get;
            set;
        }

        [ColumnName("WH_NAME")]
        public string WarehouseName
        {
            get;
            set;
        }

        [ColumnName("WH_CODE")]
        public string WarehouseCode
        {
            get;
            set;
        }
        [ColumnName("IS_CENTER_WH")]
        public int IsCenter { get; set; }

        [ColumnName("CENTER_WH_CODE")]
        public string CenterWarehouseCode { get; set; }

        private EWarehouseType _warehouseType = EWarehouseType.未知;

        public EWarehouseType WarehouseType
        {
            get
            {
                if (this._warehouseType == EWarehouseType.未知)
                {
                    if (this.IsCenter == 0 && this.CenterWarehouseCode == "0")
                        this._warehouseType = EWarehouseType.混合仓;
                    else if (this.IsCenter == 1)
                        this._warehouseType = EWarehouseType.散货仓;
                    else
                        this._warehouseType = EWarehouseType.整货仓;
                }
                return this._warehouseType;
            }
        }

        /// <summary>
        /// 应用于交互中的头信息
        /// </summary>
        public int whType { get; set; }
    }
}