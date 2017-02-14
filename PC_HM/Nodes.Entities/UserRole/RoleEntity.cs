using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using Nodes.Dapper;

namespace Nodes.Entities
{
    /// <summary>
    /// 模块信息：是固化的，不需要维护
    /// </summary>
    public class ModuleEntity
    {
        #region public property define
        [ColumnName("MODULE_ID")]
        public string ModuleID
        {
            get;
            set;
        }

        [ColumnName("MENU_NAME")]
        public string MenuName
        {
            get;
            set;
        }

        [ColumnName("PARENT_ID")]
        public string ParentID
        {
            get;
            set;
        }

        [ColumnName("DEEP")]
        public int DEEP
        {
            get;
            set;
        }

        /// <summary>
        /// 窗体名称
        /// </summary>
        [ColumnName("FORM_NAME")]
        public string FormName
        {
            get;
            set;
        }

        /// <summary>
        /// 功能分类：1：PC；2：手持
        /// </summary>
        [ColumnName("MODULE_TYPE")]
        public int ModuleType
        {
            get;
            set;
        }

        public string TypeDesc
        {
            get
            {
                if (ModuleType == 1)
                    return "PC端功能";
                else
                    return "手持端功能";
            }
        }

        #endregion
    }

    /// <summary>
    /// 角色
    /// </summary>
    public class RoleEntity : BaseEntity
    {
        #region public property define
        [ColumnName("ROLE_ID")]
        public int RoleId
        {
            get;
            set;
        }

        [ColumnName("ROLE_NAME")]
        public string RoleName
        {
            get;
            set;
        }
        
        public bool Checked
        {
            get;
            set;
        }

        private int _delete = 0;
        /// <summary>
        /// 0代表自有数据，1代表共有数据
        /// </summary>
        public int Deleted
        {
            get { return _delete; }
            set { _delete = value; }
        }

        #endregion
    }
}
