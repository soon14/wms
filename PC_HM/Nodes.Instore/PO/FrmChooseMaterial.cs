using System;
using System.Collections.Generic;
using Nodes.Controls;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.Shares;

namespace Nodes.Instore
{
    public partial class FrmChooseMaterial : DevExpress.XtraEditors.XtraForm
    {
        public delegate void InsertItem(MaterialEntity material, decimal qty);
        public event InsertItem OnInsertItem;
        private string SupplierCode = null;

        public FrmChooseMaterial(string supplierCode)
        {
            InitializeComponent();
            this.SupplierCode = supplierCode;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            queryConditionPanel.InitUI();
            gridMaterial.AppendFields();

            //绑定上一界面选定供应商的物料
            if (!string.IsNullOrEmpty(this.SupplierCode))
            {
                queryConditionPanel.SupplierCode = this.SupplierCode;
                queryConditionPanel.DoQuery();
            }
        }

        private void ShowSelectedMaterial()
        {
            MaterialEntity material = gridMaterial.FocusedHeader;
            if (material == null)
            {
                txtMaterial.Text = txtQty.Text = "";
                listUnits.EditValue = null;
            }
            else
            {
                txtMaterial.Text = material.MaterialCode;                
                List<UnitGroupItemEntity> units = new UnitGroupDal().GetItemsByGrpCode(material.UnitGrpCode);
                listUnits.Properties.DataSource = units;

                //默认选中基本单位
                listUnits.EditValue = material.UnitCode;
                txtQty.Text = "1";
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!IsFieldValid()) return;
        }

        bool IsFieldValid()
        {
            MaterialEntity material = gridMaterial.FocusedHeader;
            if (material == null)
            {
                MsgBox.Warn("请选中一行物料。");
                return false;
            }

            if (txtQty.Text.Trim().Length == 0)
            {
                MsgBox.Warn("请输入数量。");
                return false;
            }

            decimal qty = ConvertUtil.ToDecimal(txtQty.Text.Trim());
            if (qty <= decimal.Zero)
            {
                MsgBox.Warn("数量必须为大于0的数值。");
                return false;
            }

            //若物料设置了基本计量单位，并且用户选择的单位与基本单位不一样，转换为基本包装单位
            string unitCode = ConvertUtil.ToString(listUnits.EditValue);
            if (!string.IsNullOrEmpty(material.UnitCode) && !material.UnitCode.Equals(unitCode))
            {
                List<UnitGroupItemEntity> unitGroups = listUnits.Properties.DataSource as List<UnitGroupItemEntity>;
                UnitGroupItemEntity ug = unitGroups.Find(u => u.UnitCode == material.UnitCode);
                if (ug != null)
                {
                    decimal basePackQty = unitGroups.Find(u => u.UnitCode == material.UnitCode).PackQty;
                    decimal selectedUnitPackQty = unitGroups.Find(u => u.UnitCode == unitCode).PackQty;

                    //按比例折算
                    qty = qty * selectedUnitPackQty / basePackQty;
                }
            }

            MaterialEntity m = material.Clone() as MaterialEntity;
            m.Remark = txtRemark.Text.Trim();
            AddItem(m, qty);
            return true;
        }

        private void AddItem(MaterialEntity material,  decimal qty)
        {
            //把选中的物料克隆，修改为用户选中的计量单位，作为新的单据行
            if (OnInsertItem != null)
            {
                OnInsertItem(material, qty);
                lblInfo.Show(true);
            }
        }

        private void OnQueryComplete(object sender, EventArgs e)
        {
            gridMaterial.DataSource = sender as List<MaterialEntity>;
            ShowSelectedMaterial();
        }

        private void OnFocusedHandlerChanged(MaterialEntity focusedHeader)
        {
            ShowSelectedMaterial();
        }

        private void OnGridRowDoubleClick()
        {
            MaterialEntity material = gridMaterial.FocusedHeader;
            if (material != null)
            {
                MaterialEntity m = material.Clone() as MaterialEntity;
                m.Remark = string.Empty;
                AddItem(material, 1m);
            }
        }
    }
}