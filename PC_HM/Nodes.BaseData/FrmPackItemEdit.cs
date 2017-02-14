using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.Shares;
using Nodes.UI;

namespace Nodes.BaseData
{
    public partial class FrmPackItemEdit : DevExpress.XtraEditors.XtraForm
    {
        private MaterialEntity materialEntity = null;
        public event EventHandler DataSourceChanged = null;
        private bool isNew = true;
        private MaterialDal materialDal = null;

        public FrmPackItemEdit()
        {
            InitializeComponent();
        }

        public FrmPackItemEdit(MaterialEntity materialEntity)
            : this()
        {
            this.materialEntity = materialEntity;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            materialDal = new MaterialDal();
            cbIsActive.SelectedIndex = 0;

            if (materialEntity != null)
            {
                this.Text = "包材-修改";
                txtCode.Enabled = false;
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ShowEditInfo(materialEntity);
                isNew = false;
            }
        }

        #region 自定义方法

        private void ShowEditInfo(MaterialEntity material)
        {
            txtCode.Text = material.MaterialCode;
            txtName.Text = material.MaterialName;
            txtBrand.Text = material.Brand;

            if (material.Price.HasValue)
                spinEditPrice.Value = material.Price.Value;

            cbIsActive.SelectedIndex = 1 - material.IsActive;

            if (material.SortOrder.HasValue)
                spinSortOrder.Value = material.SortOrder.Value;
        }

        private void Continue()
        { 
            txtName.Text = "";
            txtBrand.Text = "";
            spinEditPrice.Value = 0;
            spinSortOrder.Value = spinSortOrder.Value + 1;

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
                MsgBox.Warn("包材编码不能为空。");
                return false;
            }

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MsgBox.Warn("包材名称不能为空。");
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
                MaterialEntity editEntity = PrepareSave();
                int ret = materialDal.MaterialAddAndUpdate(editEntity, isNew);
                if (ret == -1)
                    MsgBox.Warn("包材编号已存在，请改为其他的包材编号。");
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
        #endregion

        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            if (Save())
                this.DialogResult = DialogResult.OK;
        }

        #region IvCommonEdit 成员

        public MaterialEntity PrepareSave()
        {
            MaterialEntity editEntity = materialEntity;
            if (editEntity == null) editEntity = new MaterialEntity();

            editEntity.MaterialCode = txtCode.Text.Trim();
            editEntity.MaterialName = txtName.Text.Trim();
            editEntity.Brand = txtBrand.Text.Trim();
            editEntity.MaterialNamePY = "";
            editEntity.PackQty = null;
            editEntity.ProductLine = "";
            editEntity.Barcode = "";
            editEntity.Price = spinEditPrice.Value;
            editEntity.MaxStockQty = null;
            editEntity.MinStockQty = null;
            editEntity.MaterialType = SysCodeConstant.MATERIAL_TYPE_PACK;
            editEntity.SubType = null;
            editEntity.Unit = null;
            editEntity.IsActive = 1 - cbIsActive.SelectedIndex;
            editEntity.SortOrder = (int)spinSortOrder.Value;
            editEntity.SnOrBatch = SysCodeConstant.MATERIAL_ADMIN_TYPE_BATCH;
            editEntity.Temperature = 0;
            editEntity.Owner = 0;

            return editEntity;
        }

        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                if (materialEntity == null)
                    Continue();
                else
                    this.DialogResult = DialogResult.OK;
            }
        }
    }
}