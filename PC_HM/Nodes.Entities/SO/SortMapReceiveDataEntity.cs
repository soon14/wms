using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities
{
    /// <summary>
    /// [订单排序]-百度地图，从地图中获取的数据
    /// </summary>
    [Serializable]
    public class SortMapReceiveDataEntity
    {
        #region 属性

        /// <summary>
        /// 序列号（年月日时分秒）
        /// </summary>
        public string zu { get; set; }
        /// <summary>
        /// 序列编号
        /// </summary>
        public string paixu { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string orderId { get; set; }
        /// <summary>
        /// 件数
        /// </summary>
        public int boxNum { get; set; }

        #endregion
    }
}
