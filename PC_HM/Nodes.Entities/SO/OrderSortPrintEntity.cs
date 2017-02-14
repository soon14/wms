using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Dapper;
using Nodes.Utils;

namespace Nodes.Entities
{
    /// <summary>
    /// 装车打印实体
    /// </summary>
    public class OrderSortPrintEntity
    {
        private List<UserEntity> _userList = null;
        private List<OrderSortDetailPrintEntity> _details = null;

        /// <summary>
        /// 发货仓库
        /// </summary>
        public string Warehouse { get; set; }
        /// <summary>
        /// 发货车辆
        /// </summary>
        public string VehicleNO { get; set; }
        /// <summary>
        /// 司机以及助理
        /// </summary>
        public List<UserEntity> UserList
        {
            get
            {
                if (this._userList == null)
                    this._userList = new List<UserEntity>();
                return this._userList;
            }
            set
            {
                this._userList = value;
            }
        }
        public string UserListStr
        {
            get
            {
                string result = string.Empty;
                if (this.UserList.Count > 0)
                {
                    result = StringUtil.JoinBySign(this.UserList, "UserName", "、");
                }
                return result;
            }
        }
        /// <summary>
        /// 发货日期
        /// </summary>
        public string Date
        {
            get
            {
                return DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
        /// <summary>
        /// 6 位随机码
        /// </summary>
        public string RandomCode { get; set; }
        /// <summary>
        /// 装车明细
        /// </summary>
        public List<OrderSortDetailPrintEntity> Details
        {
            get
            {
                if (this._details == null)
                    this._details = new List<OrderSortDetailPrintEntity>();
                return this._details;
            }
            set
            {
                this._details = value;
            }
        }
    }
}
