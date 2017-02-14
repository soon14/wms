using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.Instore
{
    public class JsonVehiclesResult
    {
        /// <summary>
        /// 车牌号
        /// </summary>
        public string vehicleNo//vehicleNo
        {
            get;
            set;
        }
        /// <summary>
        /// 登记人
        /// </summary>
        public string creator//creator
        {
            get;
            set;
        }
        //司机
        public string driver//driver
        {
            get;
            set;
        }
        public string cName
        {
            get;
            set;
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string contact//contact
        {
            get;
            set;
        }
        public string billStateDesc
        {
            get;
            set;
        }
        public string cardState
        {
            get;
            set;
        }
        public string cardStateDesc
        {
            get;
            set;
        }
        /// <summary>
        /// 送货牌
        /// </summary>
        public string cardNo//cardNo
        {
            get;
            set;
        }
        /// <summary>
        /// 入库单
        /// </summary>
        public string billNo//billNo
        {
            get;
            set;
        }
        /// <summary>
        /// 登记时间
        /// </summary>
        public string createDate//createDate
        {
            get;
            set;
        }
    }

    public class JsonVehiclesEntity
    {
        /// <summary>
        /// 车牌号
        /// </summary>
        public string VEHICLE_NO//vehicleNo
        {
            get;
            set;
        }
        /// <summary>
        /// 登记人
        /// </summary>
        public string CREATOR//creator
        {
            get;
            set;
        }
        //司机
        public string DRIVER//driver
        {
            get;
            set;
        }
        /// <summary>
        /// 供货商
        /// </summary>
        public string C_NAME
        {
            get;
            set;
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string CONTACT//contact
        {
            get;
            set;
        }
        /// <summary>
        /// 入库单状态
        /// </summary>
        public string BILL_STATE_DESC
        {
            get;
            set;
        }
        public string cardState
        {
            get;
            set;
        }
        /// <summary>
        /// 当前状态
        /// </summary>
        public string CARD_STATE_DESC
        {
            get;
            set;
        }
        /// <summary>
        /// 送货牌
        /// </summary>
        public string CARD_NO//cardNo
        {
            get;
            set;
        }
        /// <summary>
        /// 入库单
        /// </summary>
        public string BILL_NO//billNo
        {
            get;
            set;
        }
        /// <summary>
        /// 登记时间
        /// </summary>
        public string CREATE_DATE//createDate
        {
            get;
            set;
        }
    }
}
