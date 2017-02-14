using System;
using System.Windows.Forms;
using Nodes.UI;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;

namespace Nodes.BaseData
{
    public partial class FrmOrgEdit : DevExpress.XtraEditors.XtraForm
    {
        private OrgEntity orgEntity = null;
        public event EventHandler DataSourceChanged = null;
        private bool isNew = true;
        private OrganizationDal orgDal = null;

        public FrmOrgEdit()
        {
            InitializeComponent();
        }

        public FrmOrgEdit(OrgEntity areaEntity)
            : this()
        {
            this.orgEntity = areaEntity;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            orgDal = new OrganizationDal();

            if (orgEntity != null)
            {
                this.Text = "组织-修改";
                txtCode.Enabled = false;
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ShowEditInfo(orgEntity);
                isNew = false;
            }
        }

        private void ShowEditInfo(OrgEntity AreaEntity)
        {
            txtCode.Text = AreaEntity.OrgCode;
            txtName.Text = AreaEntity.OrgName;
        }

        private void Continue()
        {
            txtName.Text = "";

            if (checkAutoIncrement.Checked)
            {
                txtCode.Text = AutoIncrement.NextCode(txtCode.Text.Trim());
                txtName.Focus();
            }
            else
            {
                txtCode.Text = "";
                txtCode.Focus();
            }
        }

        private bool IsFieldValueValid()
        {
            if (string.IsNullOrEmpty(txtCode.Text.Trim()))
            {
                MsgBox.Warn("编码不能为空。");
                return false;
            }

            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                MsgBox.Warn("名称不能为空。");
                return false;
            }

            return true;
        }

        private bool Save()
        {
            if (!IsFieldValueValid()) return false;
            bool success = false;
            try
            {
                OrgEntity editEntity = PrepareSave();
                int ret = orgDal.Save(editEntity, isNew);
                if (ret == -1)
                    MsgBox.Warn("编号已存在，请改为其他的编号。");
                else if (ret == -2)
                    MsgBox.Warn("更新失败，该行已经被其他人删除。");
                else
                {
                    success = true;
                    if (DataSourceChanged != null)
                    {
                        DataSourceChanged(editEntity, null);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return success;
        }

        public OrgEntity PrepareSave()
        {
            OrgEntity editEntity = orgEntity;
            if (editEntity == null) editEntity = new OrgEntity();

            editEntity.OrgCode = txtCode.Text.Trim();
            editEntity.OrgName = txtName.Text.Trim();
            editEntity.IsActive = "Y";
            editEntity.AllowEdit = "Y";
            return editEntity;
        }

        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            if (Save())
                this.DialogResult = DialogResult.OK;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                if (orgEntity == null)
                    Continue();
                else
                    this.DialogResult = DialogResult.OK;
            }
        }
    }
}