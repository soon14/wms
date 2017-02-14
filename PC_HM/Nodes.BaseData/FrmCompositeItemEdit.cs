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

namespace Nodes.WMS.BaseData
{
    public partial class FrmCompositeItemEdit : DevExpress.XtraEditors.XtraForm
    {
        MaterialDal materialDal = new MaterialDal();

        public FrmCompositeItemEdit()
        {
            InitializeComponent();
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
            List<MaterialEntity> materials = materialDal.QueryMaterial(exp);
            lookUpEdit1.Properties.DataSource = materials;
            lookUpEdit1.ShowPopup();
            lookUpEdit1.Text = keyword;
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {
        }
    }
}