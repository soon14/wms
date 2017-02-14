using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.XtraEditors.Controls;
using DevExpress.Data.Filtering;
using Nodes.Shares;

namespace Nodes.BaseData
{
    public partial class FrmCombItemEdit : DevExpress.XtraEditors.XtraForm
    {
        MaterialDal materialDal = new MaterialDal();
        CombMaterialDal combMaterialDal = new CombMaterialDal();
        MaterialEntity material;

        public FrmCombItemEdit(MaterialEntity material)
        {
            InitializeComponent();

            this.material = material;
        }

        private void lookUpEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                LoadData(lookUpEdit1.Text.Trim());
            }
        }

        protected void LoadData(string keyword)
        {
            lookUpEdit1.EditValue = null;

            string exp = LikeData.CreateContainsPattern(keyword);
            List<MaterialEntity> materials = materialDal.QueryMaterial(exp, SysCodeConstant.MATERIAL_TYPE_MATERIAL);
            lookUpEdit1.Properties.DataSource = materials;
            lookUpEdit1.ShowPopup();
            lookUpEdit1.Text = keyword;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            //加载已关联列表
            List<MaterialEntity> materials = combMaterialDal.ListMaterialsByCombCode(material.MaterialCode);
            bindingSource1.DataSource = materials;
        }

        private void OnAddItemClick(object sender, EventArgs e)
        {
            string item = ConvertUtil.ToString(lookUpEdit1.EditValue);
            if (string.IsNullOrEmpty(item))
            {
                MsgBox.Warn("请选中要添加的行。");
                return;
            }

            MaterialEntity m = materialDal.FindMaterialByCode(item);
            if (m != null)
            {
                if (m.MaterialCode == material.MaterialCode)
                {
                    MsgBox.Warn("不能添加自己为子物料。");
                    return;
                }

                List<MaterialEntity> materials = bindingSource1.DataSource as List<MaterialEntity>;
                if (materials.Exists(i => i.MaterialCode == m.MaterialCode))
                {
                    MsgBox.Warn("物料已存在于列表中。");
                    return;
                }

                //先写入数据库关联表，更新物料类别为组分料
                combMaterialDal.AddItemToComb(material.MaterialCode, item);

                materials.Add(m);
                bindingSource1.ResetBindings(false);
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            MaterialEntity m = gridView1.GetFocusedRow() as MaterialEntity;
            if (m != null)
            {
                combMaterialDal.RemoveItem(material.MaterialCode, m.MaterialCode);
                gridView1.DeleteSelectedRows();
            }
        }
    }
}