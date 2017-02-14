using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Nodes.DBHelper;
using Nodes.UI;
using DevExpress.XtraEditors;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.Shares;

namespace Nodes.BaseData
{
    public partial class UcQueryMaterial : UserControl
    {
        MaterialDal materialDal = new MaterialDal();
        bool hasLoadData = false;

        public UcQueryMaterial()
        {
            InitializeComponent();
        }

        public void InitUI()
        {
            if (hasLoadData)
                return;

            hasLoadData = true;

            if (this.TopLevelControl is Form)
                ((Form)this.TopLevelControl).AcceptButton = simpleButton1;

            try
            {
                listType.Properties.DataSource = new MaterialTypeDal().GetAll();
                listSupplier.Properties.DataSource = new SupplierDal().ListActiveSupplierByPriority();

                //List<CustomFieldEntity> customFields = CustomFields.MaterialCustomFields;
                //CustomFieldEntity field = customFields.Find(p => p.FieldName == "MTL_STR1");
                //itemStr1.Text = field.FieldDesc;
                //itemStr1.Visibility = field.IsActive == "Y" ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void OnEditButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            BaseEdit editor = sender as BaseEdit;
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
                editor.EditValue = null;
        }

        #region 公开的事件及函数
        public event EventHandler QueryComplete;

        public string MaterialCode
        {
            get
            {
                return ConvertUtil.StringToNull(txtMaterial.Text);
            }
            set
            {
                txtMaterial.Text = value;
            }
        }

        public string MaterialName
        {
            get
            {
                return ConvertUtil.StringToNull(txtMaterialName.Text);
            }
            set
            {
                txtMaterialName.Text = value;
            }
        }

        public string Barcode
        {
            get
            {
                return ConvertUtil.StringToNull(txtBarcode.Text);
            }
            set
            {
                txtBarcode.Text = value;
            }
        }

        public string MTL_STR1
        {
            get
            {
                return ConvertUtil.StringToNull(txtStr1.Text);
            }
            set
            {
                txtStr1.Text = value;
            }
        }

        public string Spec
        {
            get
            {
                return ConvertUtil.StringToNull(txtSpec.Text);
            }
            set
            {
                txtSpec.Text = value;
            }
        }

        public string SupplierCode
        {
            get
            {
                return ConvertUtil.ObjectToNull(listSupplier.EditValue);
            }
            set
            {
                listSupplier.EditValue = value;
            }
        }

        public string MaterialTypeCode
        {
            get
            {
                return ConvertUtil.ObjectToNull(listType.EditValue);
            }
            set
            {
                listType.EditValue = value;
            }
        }

        public void DoQuery()
        {
            DoQuery(MaterialCode, MaterialName, MaterialTypeCode, SupplierCode, Spec, Barcode, MTL_STR1);
        }

        public void DoQuery(string materialCode, string materialNameOrPY, string materialType, string supplier, 
            string spec, string barcode, string brand)
        {
            try
            {
                List<MaterialEntity> result = materialDal.QueryMaterials(materialCode, materialNameOrPY, 
                    materialType, supplier, spec, barcode, brand);

                if (QueryComplete != null)
                    QueryComplete(result, null);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
        #endregion

        private void OnQueryClick(object sender, EventArgs e)
        {
            DoQuery();
        }
    }
}
