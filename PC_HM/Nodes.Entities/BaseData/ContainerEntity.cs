using Nodes.Dapper;

namespace Nodes.Entities
{
    public class ContainerEntity
    {
        /// <summary>
        /// 托盘编码（物流箱）
        /// </summary>
        [ColumnName("CT_CODE")]
        public string ContainerCode { get; set; }

        /// <summary>
        /// 托盘名称（物流箱）
        /// </summary>
        [ColumnName("CT_NAME")]
        public string ContainerName { get; set; }

        [ColumnName("WH_CODE")]
        public string WarehouseCode { get; set; }

        /// <summary>
        /// 托盘类型（物流箱）
        /// </summary>
        [ColumnName("CT_TYPE")]
        public string ContainerType { get; set; }

        /// <summary>
        /// 容器类型描述
        /// </summary>
        [ColumnName("CT_TYPE_DESC")]
        public string ContainerTypeDesc { get; set; }

        /// <summary>
        /// 容器本身的重量
        /// </summary>
        [ColumnName("CT_WEIGHT")]
        public decimal ContainerWeight { get; set; }

        /// <summary>
        /// 容器状态
        /// </summary>
        [ColumnName("CT_STATE")]
        public string ContainerState { get; set; }

        /// <summary>
        /// 和该容器关联的订单
        /// </summary>
        [ColumnName("BILL_HEAD_ID")]
        public int BillHeadID { get; set; }

        /// <summary>
        /// 毛重; 总重; 连皮;（G） 实际称重
        /// </summary>
        [ColumnName("GROSS_WEIGHT")]
        public decimal GrossWeight { get; set; }

        /// <summary>
        /// 净重; 去皮;（G）   去除托盘(托盘和地牛)的重量
        /// </summary>
        [ColumnName("NET_WEIGHT")]
        public decimal NetWeight { get; set; }

        /// <summary>
        /// 理论重量
        /// </summary>
        public decimal CalcWeight { get; set; }

        /// <summary>
        /// 重量偏差
        /// </summary>
        public decimal DiffWeight { get; set; }
        [ColumnName("IS_DELETED")]
        public int IsDelete { get; set; }
    }
}
