using System;
using System.Windows.Forms;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.UI;

namespace Nodes.BaseData
{
    public partial class FrmProvinceEdit : DevExpress.XtraEditors.XtraForm
    {
        private ProvinceEntity ProvinceEntity = null;
        public event EventHandler DataSourceChanged = null;
        private bool isNew = true;
        private ProvinceDal ProvinceDal = null;

        public FrmProvinceEdit()
        {
            InitializeComponent();
        }
        public FrmProvinceEdit(ProvinceEntity ProvinceEntity)
            : this()
        {
            this.ProvinceEntity = ProvinceEntity;
        }

        private void FrmProvinceEdit_Load(object sender, EventArgs e)
        {
            ProvinceDal = new ProvinceDal();

            if (ProvinceEntity != null)
            {
                this.Text = "省份-修改";
                txtCode.Enabled = false;
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ShowEditInfo(ProvinceEntity);
                isNew = false;
            }
        }

        #region 自定义方法

        private void ShowEditInfo(ProvinceEntity provinceEntity)
        {
            txtCode.Text = provinceEntity.ProvinceCode;
            txtName.Text = provinceEntity.ProvinceName;
            txtAliasName.Text = provinceEntity.AliasName;
            txtAreaCode.Text = provinceEntity.AreaCode;
            txtCapital.Text = provinceEntity.Capital;
            txtNamePY.Text = provinceEntity.NamePY;
        }

        private void Continue()
        {
            txtName.Text = txtAliasName.Text = txtAreaCode.Text = 
                txtCapital.Text = txtNamePY.Text = "";

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
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                MsgBox.Warn("编号不能为空。");
                return false;
            }

            if (string.IsNullOrEmpty(txtName.Text))
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
                ProvinceEntity editEntity = PrepareSave();
                int ret = ProvinceDal.Save(editEntity, isNew);
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
                MsgBox.Warn(ex.Message);
            }
            return success;
        }
        #endregion

        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            if (Save())
                this.DialogResult = DialogResult.OK;
        }

        public ProvinceEntity PrepareSave()
        {
            ProvinceEntity editEntity = ProvinceEntity;
            if (editEntity == null) editEntity = new ProvinceEntity();
            editEntity.ProvinceCode = txtCode.Text.Trim();
            editEntity.ProvinceName = txtName.Text.Trim();
            editEntity.AliasName = txtAliasName.Text.Trim();
            editEntity.AreaCode = txtAreaCode.Text.Trim();
            editEntity.Capital = txtCapital.Text.Trim();
            editEntity.NamePY = txtNamePY.Text.Trim();

            return editEntity;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                if (ProvinceEntity == null)
                    Continue();
                else
                    this.DialogResult = DialogResult.OK;
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            txtNamePY.Text = PinYin.GetCapital(txtName.Text.Trim());
        }
    }
}