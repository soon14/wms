using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Reports
{
    public class JsonStockTotalFlowResult
    {
        #region 0-10
        /// <summary>
        /// 总库存备注
        /// </summary>
        public string sku_desc
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
        /// 商品单位
        /// </summary>
        public string um_name
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
        /// 商品名称
        /// </summary>
        public string sku_name
        {
            get;
            set;
        }
        public string id
        {
            get;
            set;
        }
        /// <summary>
        /// 商品库存总计
        /// </summary>
        public string sku_qty_total
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
        /// 库房号
        /// </summary>
        public string wh_code
        {
            get;
            set;
        }
        /// <summary>
        /// 第一次插入时间
        /// </summary>
        public string latestIn
        {
            get;
            set;
        }
        /// <summary>
        /// 获取名称
        /// </summary>
        public string zone_type
        {
            get;
            set;
        }
        /// <summary>
        /// 货区名称
        /// </summary>
        public string item_desc
        {
            get;
            set;
        }
        /// <summary>
        /// 获取名称
        /// </summary>
        public string znName
        {
            get 
            {
                if (Convert.ToInt32(zone_type) == 0)
                    return "总库存数量";
                return item_desc;
            }
        }

        /// <summary>
        /// 获取名称
        /// </summary>
        public string total
        {
            get
            {
                return "总库存数量"; 
            }
        }
        #endregion
    }
}
