using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.Entities;
using Nodes.DBHelper;

namespace Nodes.WMS.Inbound
{
    public partial class FrmEditAsn : DevExpress.XtraEditors.XtraForm
    {
        AsnHeaderEntity Header = null;
        public FrmEditAsn(AsnHeaderEntity header)
        {
            InitializeComponent();
            Header = header;

            this.Text = string.Format("填写备注(单号：{0})", Header.BillNO);
        }

        private void OnOKClick(object sender, EventArgs e)
        {
            int? color = null;
            if (colorBack.Color != Color.Empty)
                color = colorBack.Color.ToArgb();

            new AsnDal().UpdateWmsRemark(Header.BillID, txtRemark.Text.Trim(), color);
            this.DialogResult = DialogResult.OK;
        }

        private void colorBack_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
                colorBack.Color = Color.Empty;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            txtRemark.Text = Header.WmsRemark;
            if (Header.RowBackgroundColor != null)
                colorBack.Color = Color.FromArgb(Header.RowBackgroundColor.Value);
        }
    }
}