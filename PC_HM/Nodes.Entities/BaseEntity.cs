using System;
using Nodes.Dapper;

namespace Nodes.Entities
{
    public class BaseEntity
    {
        /// <summary>
        /// 备注信息
        /// </summary>
        [ColumnName("REMARK")]
        public string Remark { get; set; }

        /// <summary>
        /// N：表示不允许编辑，系统预定义；Y：用户自建，可以编辑
        /// </summary>
        [ColumnName("ALLOW_EDIT")]
        public string AllowEdit { get; set; }

        /// <summary>
        /// N：表示禁用；Y：表示启用中
        /// </summary>
        [ColumnName("IS_ACTIVE")]
        public string IsActive { get; set; }

        /// <summary>
        /// 创建/最后一次更新/最后一次同步的用户名称
        /// </summary>
        [ColumnName("UPDATE_BY")]
        public string LastUpdateBy { get; set; }

        /// <summary>
        /// 创建/最后一次更新/最后一次同步日期
        /// </summary>
        [ColumnName("UPDATE_DATE")]
        public DateTime? LastUpdateDate { get; set; }

        /// <summary>
        /// Y: 表示系统自建；N：表示同步自第三方系统
        /// </summary>
        [ColumnName("IS_OWN")]
        public string IsOwn { get; set; }
    }
}
