using System;
using System.Windows.Forms;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using System.Collections.Generic;
using Nodes.UI;

namespace Nodes.BaseData
{
    public partial class FrmPDAEdit : DevExpress.XtraEditors.XtraForm
    {
        private PDAEntity pdaEntity = null;
        public event EventHandler DataSourceChanged = null;
        private bool isNew = true;
        private PDADal pdaDal = null;

        public FrmPDAEdit()
        {
            InitializeComponent();
        }

        public FrmPDAEdit(PDAEntity pdaEntity)
            : this()
        {
            this.pdaEntity = pdaEntity;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            BindWareHouse();
            pdaDal = new PDADal();

            if (pdaEntity != null)
            {
                this.Text = "手持机-修改";
                txtCode.Enabled = false;
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ShowEditInfo(pdaEntity);
                isNew = false;
            }
        }

        private void BindWareHouse()
        {
            WarehouseDal warehouseDal = new WarehouseDal();
            List<WarehouseEntity> lstWareHouses = warehouseDal.GetAllWarehouse();
            cbWareHouse.Properties.DataSource = lstWareHouses;

            //默认选中第一个
            if (lstWareHouses.Count > 0)
                cbWareHouse.EditValue = lstWareHouses[0].WarehouseCode;
        }

        #region 自定义方法

        private void ShowEditInfo(PDAEntity pdaEntity)
        {
            txtCode.Text = pdaEntity.PDACode;
            txtName.Text = pdaEntity.PDAName;
            cbWareHouse.EditValue = pdaEntity.WarehouseCode;
            txtIpAddress.Text = pdaEntity.IpAddress;
            comboBoxEdit1.Text = pdaEntity.IsActive;
        }

        private void Continue()
        {
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

            if (cbWareHouse.EditValue == null)
            {
                MsgBox.Warn("仓库必须选择。");
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
                PDAEntity editEntity = PrepareSave();
                int ret = pdaDal.Save(editEntity, isNew);
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

        #region IvCommonEdit 成员

        public PDAEntity PrepareSave()
        {
            PDAEntity editEntity = pdaEntity;
            if (editEntity == null) editEntity = new PDAEntity();

            editEntity.PDACode = txtCode.Text.Trim();
            editEntity.WarehouseCode = ConvertUtil.ToString(cbWareHouse.EditValue);
            editEntity.WarehouseName = cbWareHouse.Text;
            editEntity.PDAName = txtName.Text.Trim();
            editEntity.IsActive = comboBoxEdit1.Text;
            editEntity.IpAddress = txtIpAddress.Text.Trim();

            return editEntity;
        }

        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                if (pdaEntity == null)
                    Continue();
                else
                    this.DialogResult = DialogResult.OK;
            }
        }
    }
}