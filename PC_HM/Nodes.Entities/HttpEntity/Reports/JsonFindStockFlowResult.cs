using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Reports
{
    public class JsonFindStockFlowResult
    {
        #region 0-10
        /// <summary>
        /// 主键ID
        /// </summary>
        public string id
        {
            get;
            set;
        }
        /// <summary>
        /// 商品code
        /// </summary>
        public string sku_code
        {
            get;
            set;
        }
        /// <summary>
        /// 商品规格
        /// </summary>
        public string um_code
        {
            get;
            set;
        }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string um_name
        {
            get;
            set;
        }
        /// <summary>
        /// 入库数量
        /// </summary>
        public string in_sku_qty
        {
            get;
            set;
        }
        /// <summary>
        /// 出库数量
        /// </summary>
        public string out_sku_qty
        {
            get;
            set;
        }
        /// <summary>
        /// 现库存数量
        /// </summary>
        public string new_sku_qty
        {
            get;
            set;
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string create_time
        {
            get;
            set;
        }
        /// <summary>
        /// 默认修改时间
        /// </summary>
        public string updateTime
        {
            get;
            set;
        }
        /// <summary>
        /// 操作类型
        /// </summary>
        public string action_type
        {
            get;
            set;
        }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string sku_name
        {
            get;
            set;
        }
        /// <summary>
        /// 操作类型描述
        /// </summary>
        public string sku_desc
        {
            get;
            set;
        }
        /// <summary>
        /// 订单号
        /// </summary>
        public string bill_id
        {
            get;
            set;
        }
        #endregion
    }
}
