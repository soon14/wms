using System;
using System.Windows.Forms;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.UI;

namespace Nodes.BaseData
{
    public partial class FrmTemperatureEdit : DevExpress.XtraEditors.XtraForm
    {
        private TemperatureEntity temperatureEntity = null;
        public event EventHandler DataSourceChanged = null;
        private bool isNew = true;
        private TemperatureDal temperatureDal = null;

        public FrmTemperatureEdit()
        {
            InitializeComponent();
        }

        public FrmTemperatureEdit(TemperatureEntity temperatureEntity)
            : this()
        {
            this.temperatureEntity = temperatureEntity;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            temperatureDal = new TemperatureDal();

            if (temperatureEntity != null)
            {
                this.Text = "温控-修改";
                txtCode.Enabled = false;
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ShowEditInfo(temperatureEntity);
                isNew = false;
            }
        }

        #region 自定义方法

        private void ShowEditInfo(TemperatureEntity entity)
        {
            txtCode.Text = entity.TemperatureCode;
            txtName.Text = entity.TemperatureName;
            if (entity.LowerLimit.HasValue)
                spinLower.Value = (decimal)entity.LowerLimit.Value;
            else
                spinLower.EditValue = null;

            if (entity.UpperLimit.HasValue)
                spinUpper.Value = (decimal)entity.UpperLimit.Value;
            else
                spinUpper.EditValue = null;

            if (entity.AllowEdit == "N")
            {
                this.lblMsgInfo.Text = "系统预定义属性，不允许删除与修改。";
                this.btnSave.Enabled = this.btnSaveClose.Enabled = false;
            }
        }

        private void Continue()
        {
            txtName.Text = "";
            spinLower.EditValue = spinUpper.EditValue = null;

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
                MsgBox.Warn("编码不能为空。");
                return false;
            }

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MsgBox.Warn("名称不能为空。");
                return false;
            }

            if (spinLower.Value > spinUpper.Value)
            {
                MsgBox.Warn("温度下限不能大于上限。");
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
                TemperatureEntity editEntity = PrepareSave();
                int ret = temperatureDal.Save(editEntity, isNew);
                if (ret == -1)
                    MsgBox.Warn("编码已存在，请改为其他的编码。");
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

        public TemperatureEntity PrepareSave()
        {
            TemperatureEntity editEntity = temperatureEntity;
            if (editEntity == null)
            {
                editEntity = new TemperatureEntity();
                editEntity.AllowEdit = "Y";
            }

            editEntity.TemperatureCode = txtCode.Text.Trim();
            editEntity.TemperatureName = txtName.Text.Trim();
            editEntity.LowerLimit = (int)spinLower.Value;
            editEntity.UpperLimit = (int)spinUpper.Value;

            return editEntity;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                if (temperatureEntity == null)
                    Continue();
                else
                    this.DialogResult = DialogResult.OK;
            }
        }
    }
}