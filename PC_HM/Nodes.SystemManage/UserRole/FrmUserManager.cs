using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.Utils;
using Nodes.Entities;
using Nodes.Resources;
using Nodes.Utils;
using Nodes.Shares;
using Nodes.UI;

namespace Nodes.SystemManage
{
    public partial class FrmUserManager : DevExpress.XtraEditors.XtraForm, IvUserManager
    {
        private PreUserManager pUserManager = null;

        public FrmUserManager()
        {
            InitializeComponent();
            pUserManager = new PreUserManager(this);
        }

        public void BindGrid(List<UserEntity> users)
        {
            bindingSource1.DataSource = users;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            ImageCollection ic = AppResource.LoadToolImages();
            barManager1.Images = ic;
            toolAdd.ImageIndex = (int)AppResource.EIcons.add;
            toolEdit.ImageIndex = (int)AppResource.EIcons.edit;
            toolDelete.ImageIndex = (int)AppResource.EIcons.delete;
            toolRefresh.ImageIndex = (int)AppResource.EIcons.refresh;
            toolResetPwd.ImageIndex = (int)AppResource.EIcons._lock;
            toolPrint.ImageIndex = (int)AppResource.EIcons.print;
            toolDesign.ImageIndex = (int)AppResource.EIcons.tree;

            pUserManager.ListUsersAndBindToGrid();
        }

        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string tag = e.Item.Tag.ToString();
            DoEvent(tag);
        }

        private void DoEvent(string tag)
        {
            switch (tag)
            {
                case "刷新":
                    Reload();
                    break;
                case "新增":
                    //if (GlobeSettings.HasRight(4001))
                        DoCreateUser();
                    break;
                case "修改":
                    //if (GlobeSettings.HasRight(4003))
                        ShowEditForm();
                    break;
                case "删除":
                    //if (GlobeSettings.HasRight(4002))
                        DeleteSelectedUser();
                    break;
                case "打印":
                    DoPrint();
                    break;
                case "设计":
                    RibbonReportDesigner.MainForm designForm = new RibbonReportDesigner.MainForm();
                    RepUserCard rep = new RepUserCard();
                    try
                    {
                        designForm.OpenReport(rep, rep.RepFileName);
                        designForm.ShowDialog();
                        designForm.Dispose();
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Err(ex.Message);
                    }
                    break;
                case "重置密码":
                    ResetPwd();
                    break;
            }
        }

        private void DoPrint()
        {
            int[] selectedUserIndex = gridView1.GetSelectedRows();
            if (selectedUserIndex.Length == 0)
            {
                MsgBox.Warn("请选中要打印的行。");
            }
            else
            {
                List<UserEntity> users = new List<UserEntity>();
                foreach (int i in selectedUserIndex)
                    users.Add(gridView1.GetRow(i) as UserEntity);
                RepUserCard repUserCard = new RepUserCard(users, 1);
                repUserCard.Print();
            }
        }

        #region "新建用户"
        void DoCreateUser()
        {
            FrmUserEdit frmUserEdit = new FrmUserEdit();
            frmUserEdit.DataSourceChanged += OnCreateUser;
            frmUserEdit.ShowDialog();
        }

        void OnCreateUser(object sender, EventArgs e)
        {
            UserEntity user = sender as UserEntity;
            bindingSource1.Add(user);
        }
        #endregion

        #region "修改用户"
        void ShowEditForm()
        {
            UserEntity user = SelectedUserRow;
            if (user == null)
            {
                MsgBox.Warn("请选中要修改的用户行。");
                return;
            }

            if (user.UserCode == "000999")
            {
                if (GlobeSettings.LoginedUser.UserCode != "000999")
                {
                    MsgBox.Warn("普通用户没有修改管理员的权限。");
                    return;
                }
            }

            FrmUserEdit frmUserEdit = new FrmUserEdit(user);
            frmUserEdit.DataSourceChanged += OnUserChanged;
            frmUserEdit.ShowDialog();
        }

        void OnUserChanged(object sender, EventArgs e)
        {
            //刷新当前数据行
            bindingSource1.ResetBindings(false);

            //Reload();
        }
        #endregion

        UserEntity SelectedUserRow
        {
            get
            {
                if (gridView1.FocusedRowHandle < 0)
                    return null;

                return gridView1.GetFocusedRow() as UserEntity;
            }
        }

        #region "删除用户"
        void DeleteSelectedUser()
        {
            UserEntity user = SelectedUserRow;
            if (user == null)
            {
                MsgBox.Warn("请选中要删除的用户行。");
                return;
            }

            if (user.AllowEdit == "N")
            {
                MsgBox.Warn("这是系统预定义的用户，不允许删除。");
                return;
            }

            //不能把自己删除了，但是可以禁用
            if (user.UserCode == GlobeSettings.LoginedUser.UserCode)
            {
                MsgBox.Warn("不能删除自己。");
                return;
            }

            //必须保留一个用户
            if (gridView1.RowCount <= 1)
            {
                MsgBox.Warn("不能再删除了，至少要保留一个可以正常使用的用户。");
                return;
            }

            if (MsgBox.AskOK(string.Format("确定要删除用户“{0}”吗？", user.UserName)) == DialogResult.OK)
            {
                pUserManager.DeleteUser(user);
            }
        }

        public void RemoveRowFromGrid(UserEntity user)
        {
            bindingSource1.Remove(user);
        }

        #endregion

        #region "重置密码"
        void ResetPwd()
        {
            UserEntity user = SelectedUserRow;
            if (user == null)
            {
                MsgBox.Warn("请选中要重置密码的用户行。");
                return;
            }
            else if(user.UserCode == "000999")
            {
                MsgBox.Warn("您没有足够权限修改当前用户密码！");
                return;
            }

            if (DialogResult.OK ==
                MsgBox.AskOK(string.Format("确定要将用户“{0}”的密码重置为“{1}”吗？", user.UserName, GlobeSettings.InitialPassword)))
            {
                pUserManager.ResetPwd(user);
            }
        }
        #endregion

        void Reload()
        {
            pUserManager.ListUsersAndBindToGrid();
        }

        private void gridView1_RowDoubleClick(object sender, EventArgs e)
        {
            ShowEditForm();
        }        
    }
}