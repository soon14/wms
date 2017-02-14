using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Nodes.Entities
{
    /// <summary>
    /// 订单排序-百度地图，传入地图的数据
    /// </summary>
    [Serializable]
    public class SortMapSendDataEntity
    {
        #region 属性

        /// <summary>
        /// 订单ID
        /// </summary>
        [JsonIgnore]
        public int BillID { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string orderId { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string marketName { get; set; } 
        /// <summary>
        /// 客户地址
        /// </summary>
        public string marketAddress { get; set; } 
        /// <summary>
        /// 纬度
        /// </summary>
        public decimal lat { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public decimal lng { get; set; }
        /// <summary>
        /// 件数
        /// </summary>
        public int boxNum { get; set; }
        /// <summary>
        /// 整货件数
        /// </summary>
        public int zhengBoxNum { get; set; }
        /// <summary>
        /// 散货件数
        /// </summary>
        public int caseBoxNum { get; set; }

        #endregion

        #region Override Methods

        public override bool Equals(object obj)
        {
            SortMapSendDataEntity data = obj as SortMapSendDataEntity;
            if (data == null || data.BillID != this.BillID)
                return false;
            return true;
        }

        #endregion
    }
}
