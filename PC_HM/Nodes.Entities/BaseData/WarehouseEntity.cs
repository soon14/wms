using Nodes.Dapper;
using System.Collections.Generic;

namespace Nodes.Entities
{
    public class WarehouseEntity : CompanyEntity
    {
        #region Model

        /// <summary>
        /// 仓库编码
        /// </summary>
        [ColumnName("WH_CODE")]
        public string WarehouseCode
        {
            set;
            get;
        }

        /// <summary>
        /// 仓库名称
        /// </summary>
        [ColumnName("WH_NAME")]
        public string WarehouseName
        {
            set;
            get;
        }

        /// <summary>
        /// 仓库所属大区编号
        /// </summary>
        [ColumnName("ORG_CODE")]
        public string OrgCode
        {
            set;
            get;
        }

        /// <summary>
        /// 仓库所属大区名称
        /// </summary>
        [ColumnName("ORG_NAME")]
        public string OrgName
        {
            set;
            get;
        }

         /// <summary>
        /// 库房所属X坐标
        /// </summary>
        [ColumnName("X_COOR")]
        public decimal XCoor { get; set; }

        /// <summary>
        /// 库房所属Y坐标
        /// </summary>
        [ColumnName("Y_COOR")]
        public decimal YCoor { get; set; }
        #endregion Model
    }
}

