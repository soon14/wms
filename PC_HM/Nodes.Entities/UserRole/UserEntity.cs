using Nodes.Dapper;
using Nodes.Utils;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Nodes.Entities
{
    /// <summary>
    /// 用户信息
    /// 对应表：USERS
    /// </summary>
    public class UserEntity : OrgEntity, IComparable, IComparable<UserEntity>
    {
        public bool HasChecked { get; set; }
        [ColumnName("USER_ID")]
        public int UserID
        {
            get;
            set;
        }

        [ColumnName("USER_CODE")]
        public string UserCode
        {
            get;
            set;
        }

        [ColumnName("USER_NAME")]
        public string UserName
        {
            get;
            set;
        }

        [ColumnName("ROLE_ID")]
        public int ROLE_ID
        {
            get;
            set;
        }

        [ColumnName("ATTRI1")]
        public int Attri1
        {
            get;
            set;
        }
        [ColumnName("ATTRI2")]
        public bool Attri2 { get; set; }

        [ColumnName("ROLE_NAME")]
        public string RoleName { get; set; }

        /// <summary>
        /// 所属分公司编号
        /// </summary>
        [ColumnName("BRANCH_CODE")]
        public string BranchCode
        {
            get;
            set;
        }

        public string UserNamePY
        {
            get { return PinYin.GetCapital(UserName); }
        }

        [ColumnName("PWD")]
        public string UserPwd
        {
            get;
            set;
        }

        /// <summary>
        /// 登录时，系统获取本机的IP地址
        /// </summary>
        public string IPAddress
        {
            get;
            set;
        }

        [ColumnName("MOBILE_PHONE")]
        public string MobilePhone
        {
            get;
            set;
        }

        [ColumnName("ROLE_LIST")]
        public string RoleNameListStr { get; set; }

        [ColumnName("IS_ONLINE")]
        public string IsOnline { get; set; }

        public string IsOnlineDesc
        {
            get
            {
                if (IsOnline == "Y")
                    return "Y";
                else
                    return "N";
            }
        }

        public string IsOnlineStr
        {
            get
            {
                if (this.IsOnline == "Y")
                    return "在线";
                else
                    return "离线";
            }
        }
        [ColumnName("USER_TYPE")]
        public string UserType { get; set; }
        [ColumnName("USER_TYPE_DESC")]
        public string UserTypeStr { get; set; }
        [ColumnName("USER_ATTRI")]
        public string UserAttri { get; set; }
        [ColumnName("USER_ATTRI_DESC")]
        public string UserAttriStr { get; set; }

        public int CompareTo(UserEntity other)
        {
            if (other.HasChecked == this.HasChecked)
                return 0;
            return other.HasChecked.CompareTo(this.HasChecked);
        }

        public int CompareTo(object obj)
        {
            if (!(obj is UserEntity))
                throw new InvalidOperationException("CompareTo: Not a UserEntity");
            return CompareTo((UserEntity)obj);
        }
    }
}