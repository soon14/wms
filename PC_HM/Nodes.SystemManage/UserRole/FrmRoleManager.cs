using System;
using System.Collections.Generic;
using System.Windows.Forms;
//using Nodes.DBHelper;
using DevExpress.Utils;
using Nodes.Entities;
using Nodes.Resources;
using Nodes.Utils;
using Nodes.UI;
using Nodes.Shares;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.SystemManage;
using Newtonsoft.Json;

namespace Nodes.SystemManage
{
    public partial class FrmRoleManager : DevExpress.XtraEditors.XtraForm, IvRoleManager
    {
        private PreRoleManager pRoleManager = null;
        //private RoleDal rdl = new RoleDal();

        public FrmRoleManager()
        {
            InitializeComponent();
            pRoleManager = new PreRoleManager(this);
        }

        public void BindGrid(List<RoleEntity> roles)
        {
            bindingSource1.DataSource = roles;
        }

        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string tag = e.Item.Tag.ToString();
            DoEvent(tag);
        }

        private void OnMenuItemClick(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            string tag = item.Tag.ToString();
            DoEvent(tag);
        }

        private void DoEvent(string tag)
        {
            switch (tag)
            {
                case "新增":
                    CreateRole();
                    break;
                case "修改":
                    EditFocusedRole();
                    break;
                case "删除":
                    DoDelete();
                    break;
                case "刷新":
                    pRoleManager.ListRolesAndBindToGrid();
                    break;
            }
        }

        RoleEntity FocusedRoleRow
        {
            get
            {
                if (gvMain.FocusedRowHandle < 0)
                    return null;

                return gvMain.GetFocusedRow() as RoleEntity;
            }
        }

        #region 插入日志记录
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="type">日志类型</param>
        /// <param name="creator">当前操作人</param>
        /// <param name="billNo">订单编号</param>
        /// <param name="description">操作描述</param>
        /// <param name="module">模块</param>
        /// <param name="createTime">创建时间</param>
        /// <param name="remark">备注信息</param>
        /// <returns></returns>
        public  bool Insert(ELogType type, string creator, string billNo, string description,
            string module, DateTime createTime, string remark)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("type=").Append(type).Append("&");
                loStr.Append("creator=").Append(creator).Append("&");
                loStr.Append("billNo=").Append(billNo).Append("&");
                loStr.Append("description=").Append(description).Append("&");
                loStr.Append("module=").Append(module).Append("&");
                loStr.Append("remark=").Append(remark);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_Insert);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return false;
                }
                #endregion

                #region 正常错误处理

                Sucess bill = JsonConvert.DeserializeObject<Sucess>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return false;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return false;
                }
                #endregion

                return true;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return false;
        }
        public  bool Insert(ELogType type, string creator, string billNo, string description,
            string module, string remark)
        {
            return Insert(type, creator, billNo, description, module, DateTime.Now, remark);
        }
        public  bool Insert(ELogType type, string creator, string billNo, string description,
            string module)
        {
            return Insert(type, creator, billNo, description, module, DateTime.Now, null);
        }
        public  bool Insert(ELogType type, string creator, string billNo, string module)
        {
            return Insert(type, creator, billNo, string.Empty, module, DateTime.Now, null);
        }
        #endregion

        void DoDelete()
        {
            RoleEntity role = FocusedRoleRow;
            if (role == null)
            {
                MsgBox.Warn("请选中要删除的角色。");
                return;
            }

            if (role.AllowEdit == "N")
            {
                MsgBox.Warn("这是系统预定义的角色，不允许删除。");
                return;
            }

            if (MsgBox.AskOK(string.Format("确定要删除角色“{0}”吗？会同时断开与用户、权限之间的关联关系。", role.RoleName)) != DialogResult.OK)
                return;

            //开始存入数据库
            //int ret = pRoleManager.DeleteRole(role);
            //if (ret == 0)
            //{
            //    MsgBox.Warn("这是系统预定义的角色，不允许删除。");
            //}
            bool ret = pRoleManager.DeleteRole(role);
            if(ret)
            {
                // 添加删除角色时的日志
                Insert(ELogType.角色, GlobeSettings.LoginedUser.UserName, role.RoleId.ToString(), role.RoleName, this.Text);
                bindingSource1.Remove(role);
            }
        }

        void CreateRole()
        {
            FrmRoleEdit frmCreateRole = new FrmRoleEdit();
            frmCreateRole.RoleChanged += OnCreateRole;
            frmCreateRole.ShowDialog();
            pRoleManager.ListRolesAndBindToGrid();
        }

        void OnCreateRole(object sender, EventArgs e)
        {
            RoleEntity role = sender as RoleEntity;
            bindingSource1.Add(role);
            BindTreeAndUser();
        }

        void EditFocusedRole()
        {
            RoleEntity role = FocusedRoleRow;
            if (role == null)
            {
                MsgBox.Warn("请选中要修改的行。");
                return;
            }

            FrmRoleEdit frmEditRole = new FrmRoleEdit(role);
            frmEditRole.RoleChanged += OnRoleChanged;
            frmEditRole.ShowDialog();
        }

        void OnRoleChanged(object sender, EventArgs e)
        {
            pRoleManager.ListRolesAndBindToGrid();
            BindTreeAndUser();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            ImageCollection ic = AppResource.LoadToolImages();
            barManager1.Images = ic;
            barButtonItem2.ImageIndex = (int)AppResource.EIcons.add;
            barButtonItem3.ImageIndex = (int)AppResource.EIcons.edit;
            barButtonItem4.ImageIndex = (int)AppResource.EIcons.delete;
            barButtonItem1.ImageIndex = (int)AppResource.EIcons.refresh;

            pRoleManager.ListRolesAndBindToGrid();
        }

        private void gridView1_RowDoubleClick(object sender, EventArgs e)
        {
            EditFocusedRole();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            BindTreeAndUser();
        }

        /// <summary>
        /// 列出某个角色下面的所有用户
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public List<UserEntity> ListUsersByRoleID(int roleId)
        {
            List<UserEntity> list = new List<UserEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("roleId=").Append(roleId);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_ListUsersByRoleID);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonListUsersByRoleID bill = JsonConvert.DeserializeObject<JsonListUsersByRoleID>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return list;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return list;
                }
                #endregion

                #region 赋值数据
                foreach (JsonListUsersByRoleIDResult jbr in bill.result)
                {
                    UserEntity asnEntity = new UserEntity();
                    asnEntity.IsOnline = jbr.isOnline;
                    asnEntity.UserCode = jbr.userCode;
                    asnEntity.UserName = jbr.userName;
                    asnEntity.WarehouseCode = jbr.whCode;
                    asnEntity.WarehouseName = jbr.whName;
                    list.Add(asnEntity);
                }
                return list;
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return list;
        }

        /// <summary>
        /// 列出某个角色下面的所有权限及下级权限
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public List<ModuleEntity> ListModulesByRoleID(int roleId)
        {
            List<ModuleEntity> list = new List<ModuleEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("roleId=").Append(roleId);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_ListModulesByRoleID);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonListModulesByRoleID bill = JsonConvert.DeserializeObject<JsonListModulesByRoleID>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return list;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return list;
                }
                #endregion

                #region 赋值数据
                foreach (JsonListModulesByRoleIDResult jbr in bill.result)
                {
                    ModuleEntity asnEntity = new ModuleEntity();
                    asnEntity.DEEP = Convert.ToInt32(jbr.deep);
                    asnEntity.MenuName = jbr.menuName;
                    asnEntity.ModuleID = jbr.moduleId;
                    asnEntity.ParentID = jbr.parentId;
                    list.Add(asnEntity);
                }
                return list;
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return list;
        }

        private void BindTreeAndUser()
        {
            RoleEntity role = FocusedRoleRow;
            if (role == null)
                return;

            //如果不允许编辑，把删除按钮变灰
            barButtonItem4.Enabled = role.AllowEdit == "Y";
            treeList1.DataSource = ListModulesByRoleID(role.RoleId);
            treeList1.ExpandAll();
            gridControl2.DataSource = ListUsersByRoleID(role.RoleId);
        }
    }
}