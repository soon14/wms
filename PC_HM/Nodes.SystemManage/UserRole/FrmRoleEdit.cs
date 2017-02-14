using System;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using DevExpress.XtraTreeList.Nodes;
using Nodes.UI;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.SystemManage;
using Newtonsoft.Json;

namespace Nodes.SystemManage
{
    public partial class FrmRoleEdit : DevExpress.XtraEditors.XtraForm
    {
        public RoleEntity roleEdited = null;
        //private RoleDal roleDal = new RoleDal();
        public event EventHandler RoleChanged;
        private List<ModuleEntity> lstModules = null;
        bool isCreateNew = true;

        public FrmRoleEdit()
        {
            InitializeComponent();
        }

        public FrmRoleEdit(RoleEntity role)
            : this()
        {
            this.roleEdited = role;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            bindTree();
            if (roleEdited != null)
            {
                txtName.Text = roleEdited.RoleName;
                txtDesc.Text = roleEdited.Remark;
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                CheckMyModules(roleEdited.RoleId);
                this.Text = "角色-修改";
                isCreateNew = false;

                //不允许编辑的时候，把保存按钮变灰
                if (roleEdited.AllowEdit == "N")
                {
                    txtName.Enabled = txtDesc.Enabled = false;
                    lblMsgInfo.Text = "系统预定义角色，不允许修改名称。";
                }
            }
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

        void CheckMyModules(int roleId)
        {
            List<ModuleEntity> myModules = ListModulesByRoleID(roleId);
            if (myModules == null) return;
            foreach (ModuleEntity m in myModules)
            {
                TreeListNode node = treeList1.FindNodeByKeyID(m.ModuleID);
                if (node != null) node.Checked = true;
            }
        }


        public List<ModuleEntity> ListModules()
        {
            List<ModuleEntity> list = new List<ModuleEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("roleId=").Append(roleId);
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_ListModules);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonListModules bill = JsonConvert.DeserializeObject<JsonListModules>(jsonQuery);
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
                foreach (JsonListModulesResult jbr in bill.result)
                {
                    ModuleEntity asnEntity = new ModuleEntity();
                    asnEntity.DEEP = Convert.ToInt32(jbr.deep);
                    asnEntity.MenuName = jbr.menuName;
                    asnEntity.ModuleType = Convert.ToInt32(jbr.moduleType);
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

        void bindTree()
        {
            lstModules = ListModules();
            treeList1.DataSource = lstModules;
        }

        private void treeList1_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            bool hasChildren = e.Node.HasChildren;
            if (e.Node.Checked)
                CheckNodeCascade(e.Node);

            if (!e.Node.Checked)
            {
                UncheckNodeCascade(e.Node);
            }
        }

        void CheckNodeCascade(TreeListNode node)
        {
            if (node.ParentNode != null)
            {
                node.ParentNode.Checked = true;
                CheckNodeCascade(node.ParentNode);
            }
        }

        void UncheckNodeCascade(TreeListNode node)
        {
            foreach (TreeListNode n in node.Nodes)
            {
                n.Checked = false;
                UncheckNodeCascade(n);
            }
        }

        private bool IsFieldValueValid()
        {
            if (txtName.Text.Trim().Length == 0)
            {
                MsgBox.Warn("请填写角色名称。");
                return false;
            }

            return true;
        }

        private List<ModuleEntity> GetTreeCheck()
        {
            List<ModuleEntity> Modules = new List<ModuleEntity>();
            foreach (ModuleEntity module in lstModules)
            {
                TreeListNode node = treeList1.FindNodeByKeyID(module.ModuleID);
                if (node != null)
                {
                    CheckState result = node.CheckState;
                    if (result == CheckState.Checked)
                    {
                        Modules.Add(module);
                    }
                }
            }

            return Modules;
        }

        void Continue()
        {
            txtName.Text = txtDesc.Text = "";
            bindTree();
        }

        #region List转换成Json
        private string GetRes<T>(List<T> listobj, List<string> proptylist)
        {

            StringBuilder strb = new StringBuilder();
            List<string> result = new List<string>();
            string curname = default(string);
            foreach (var obj in listobj)
            {

                Type type = obj.GetType();

                curname = type.Name;


                List<string> curobjliststr = new List<string>();
                foreach (var curpropty in proptylist)
                {
                    string tmp = default(string);
                    var res01 = type.GetProperty(curpropty).GetValue(obj, null);
                    if (res01 == null)
                    {
                        tmp = null;
                    }
                    else
                    {
                        tmp = res01.ToString();
                    }
                    curobjliststr.Add("\"" + curpropty + "\"" + ":" + "\"" + tmp + "\"");
                }
                string curres = "{" + string.Join(",", curobjliststr.ToArray()) + "}";
                result.Add(curres);
            }
            strb.Append(":[" + string.Join(",", result.ToArray()) + "]");
            string ret = "\"" + curname + "\"" + strb.ToString();
            ret = ret.Insert(0, "{");
            ret = ret.Insert(ret.Length, "}");
            return ret;
        }


        private string GetResList<T>(List<T> listobj, List<string> proptylist)
        {

            StringBuilder strb = new StringBuilder();
            List<string> result = new List<string>();
            string curname = default(string);
            foreach (var obj in listobj)
            {

                Type type = obj.GetType();

                curname = type.Name;


                List<string> curobjliststr = new List<string>();
                foreach (var curpropty in proptylist)
                {
                    string tmp = default(string);
                    var res01 = type.GetProperty(curpropty).GetValue(obj, null);
                    if (res01 == null)
                    {
                        tmp = null;
                    }
                    else
                    {
                        tmp = res01.ToString();
                    }
                    curobjliststr.Add("\"" + curpropty + "\"" + ":" + "\"" + tmp + "\"");
                }
                string curres = "{" + string.Join(",", curobjliststr.ToArray()) + "}";
                result.Add(curres);
            }

            //strb.Append(":[" + string.Join(",", result.ToArray()) + "]");
            //string ret = "\""+ curname + "\"" + strb.ToString();
            //ret = ret.Insert(0, "{");
            //ret = ret.Insert(ret.Length, "}");
            return string.Join(",", result.ToArray());
        }

        private string GetResList<T>(string josnName, List<T> listobj, List<string> proptylist)
        {

            StringBuilder strb = new StringBuilder();
            List<string> result = new List<string>();
            string curname = default(string);
            foreach (var obj in listobj)
            {

                Type type = obj.GetType();

                curname = type.Name;


                List<string> curobjliststr = new List<string>();
                foreach (var curpropty in proptylist)
                {
                    string tmp = default(string);
                    var res01 = type.GetProperty(curpropty).GetValue(obj, null);
                    if (res01 == null)
                    {
                        tmp = null;
                    }
                    else
                    {
                        tmp = res01.ToString();
                    }
                    curobjliststr.Add("\"" + curpropty + "\"" + ":" + "\"" + tmp + "\"");
                }
                string curres = "{" + string.Join(",", curobjliststr.ToArray()) + "}";
                result.Add(curres);
            }

            strb.Append(":[" + string.Join(",", result.ToArray()) + "]");
            string ret = "\"" + josnName + "\"" + strb.ToString();
            //ret = ret.Insert(0, "{");
            //ret = ret.Insert(ret.Length, "}");
            return ret;
        }

        #endregion

        public bool SaveRole(RoleEntity role, List<ModuleEntity> modules, bool isCreateNew)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("roleName=").Append(role.RoleName).Append("&");
                loStr.Append("allowEdit=").Append(role.AllowEdit).Append("&");
                loStr.Append("remark=").Append(role.Remark).Append("&");
                loStr.Append("roleId=").Append(role.RoleId).Append("&");
                loStr.Append("isCreateNew=").Append(isCreateNew).Append("&");
                #region jsons
                List<string> prop = new List<string>() { "ModuleID" };
                string jsonStr = GetResList<ModuleEntity>("jsonStr", modules, prop);
                jsonStr = "{" + jsonStr + "}";
                #endregion
                loStr.Append("jsonStr=").Append(jsonStr);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveRole);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return false;
                }
                #endregion

                #region 正常错误处理

                JsonSaveRole bill = JsonConvert.DeserializeObject<JsonSaveRole>(jsonQuery);
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
                if (isCreateNew && string.IsNullOrEmpty(bill.result[0].roleId))
                    role.RoleId = Convert.ToInt32(bill.result[0].roleId);

                return true;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return false;
        }

        bool Save()
        {
            if (!IsFieldValueValid()) return false;

            RoleEntity role = PrepareSave();
            List<ModuleEntity> modules = GetTreeCheck();
            bool result = SaveRole(role, modules, isCreateNew);
            if (result)
            {
                if (RoleChanged != null)
                    RoleChanged(role, null);
            }
            else
            {
                return false;
            }
            //else if (result == -1 || result == -2)
            //{
            //    MsgBox.Warn("角色名称已经存在，请改为其他的名称。");
            //    return false;
            //}

            return true;
        }

        public RoleEntity PrepareSave()
        {
            RoleEntity role = new RoleEntity();
            if (roleEdited == null)
                role.AllowEdit = "Y";
            else
                role.RoleId = roleEdited.RoleId;

            role.RoleName = txtName.Text.Trim();
            role.Remark = txtDesc.Text.Trim();

            return role;
        }

        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            if (Save()) this.DialogResult = DialogResult.OK;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                if (this.roleEdited == null)
                    Continue();
                else
                    this.DialogResult = DialogResult.OK;
            }
        }
    }
}