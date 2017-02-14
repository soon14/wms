using System;
using System.Collections.Generic;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;

namespace Nodes.SystemManage
{
    public partial class FrmModulesManager : DevExpress.XtraEditors.XtraForm
    {
        RoleDal rdl = new RoleDal();
        public FrmModulesManager()
        {
            InitializeComponent();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            bindTree();
        }

        void bindTree()
        {
            treeList1.DataSource = rdl.ListModules();
            treeList1.ExpandAll();
        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            string keyID = ConvertUtil.ToString(e.Node.GetValue("ModuleID"));
            bindDetail(keyID);
        }

        void bindDetail(string ModuleID)
        {
            List<RoleEntity> roles = rdl.ListRolesByModuleID(ModuleID);
            gridMain.DataSource = roles;
            List<UserEntity> users = rdl.ListUsersByModuleID(ModuleID);
            gridControl2.DataSource = users;
        }
    }
}