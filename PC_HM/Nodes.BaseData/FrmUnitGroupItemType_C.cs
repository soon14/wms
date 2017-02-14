using System;
using Nodes.UI;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;

namespace Nodes.BaseData
{
    public partial class FrmUnitGroupItemType_C : DevExpress.XtraEditors.XtraForm
    {
        private UnitGroupEntity unitGrpEntity = null;
        public event EventHandler DataSourceChanged = null;
        private UnitGroupDal ugDal = null;

        public FrmUnitGroupItemType_C()
        {
            InitializeComponent();
        }

        public FrmUnitGroupItemType_C(UnitGroupEntity unitGrpEntity)
            : this()
        {
            this.unitGrpEntity = unitGrpEntity;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            ugDal = new UnitGroupDal();
            BindingCombox();
            //txtName.Text = unitGrpEntity.GrpName;
            //txtNum.Properties.Buttons[0].Caption = unitGrpEntity.UnitName;
        }

        private void BindingCombox()
        {
            listUnit.Properties.DataSource = new UnitDal().GetAllUnit();
        }

        private bool IsFieldValueValid()
        {
            if (listUnit.EditValue == null)
            {
                MsgBox.Warn("计量单位必须填写。");
                return false;
            }

            //不能跟组中的最小单位重复
            string unit = ConvertUtil.ToString(listUnit.EditValue);
            if (unitGrpEntity.UnitCode.Equals(unit))
            {
                MsgBox.Warn("选择的单位与组的最小单位重复，请重新选择。");
                return false;
            }

            if (txtNum.Text.Trim().Length == 0 || !ConvertUtil.IsNumeric(txtNum.Text.Trim()))
            {
                MsgBox.Warn("内件数必须填写，且必须为大于0的数值。");
                return false;
            }
            else if (ConvertUtil.ToDecimal(txtNum.Text.Trim()) <= 0)
            {
                MsgBox.Warn("内件数填写无效，必须为大于0的数值");
                return false;
            }

            return true;
        }

        private void Continue()
        {
            listUnit.EditValue = null;
            txtNum.Text = string.Empty;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //if (Save())
            //{
            //    Continue();
            //}
        }

        //private bool Save()
        //{
        //    if (!IsFieldValueValid()) return false;

        //    try
        //    {
        //        string unit = ConvertUtil.ToString(listUnit.EditValue);
        //        decimal qty = ConvertUtil.ToDecimal(txtNum.Text.Trim());
        //        int result = ugDal.SaveItem(unitGrpEntity.GrpCode, unit, qty);
        //        if (result == -1)
        //        {
        //            MsgBox.Warn("保存失败，选中的计量单位已经存在于当前组中。");
        //            return false;
        //        }
        //        else
        //        {
        //            if (DataSourceChanged != null)
        //                DataSourceChanged(null, null);

        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MsgBox.Err(ex.Message);
        //        return false;
        //    }
        //}
    }
}