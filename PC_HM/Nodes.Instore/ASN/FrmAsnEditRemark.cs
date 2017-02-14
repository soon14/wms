using System;
using System.Drawing;
using System.Windows.Forms;
using Nodes.UI;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Shares;

namespace Nodes.Instore
{
    public partial class FrmAsnEditRemark : DevExpress.XtraEditors.XtraForm
    {
        AsnBodyEntity asnHeader = null;
        AsnDal asnDal = null;

        public FrmAsnEditRemark(AsnBodyEntity asnHeader)
        {
            InitializeComponent();

            this.asnHeader = asnHeader;
            this.Text = string.Format("编辑备注(单号：{0})", asnHeader.BillID);
        }

        private void OnSaveClick(object sender, EventArgs e)
        {
            int? color = null;
            if (colorFore.Color != Color.Empty)
                color = colorFore.Color.ToArgb();

            try
            {
                int result = asnDal.UpdateRemark(asnHeader.BillID, txtRemark.Text.Trim(), color, GlobeSettings.LoginedUser.UserName);
                if (result == 1)
                {
                    this.asnHeader.WmsRemark = txtRemark.Text.Trim();
                    this.asnHeader.RowForeColor = color;

                    this.DialogResult = DialogResult.OK;
                }
                else
                    MsgBox.Warn("更新失败，该单据已经被其他人删除。");
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void OnColorForeButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
                colorFore.Color = Color.Empty;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            asnDal = new AsnDal();
            txtRemark.Text = asnHeader.WmsRemark;
            if (asnHeader.RowForeColor != null)
                colorFore.Color = Color.FromArgb(asnHeader.RowForeColor.Value);
            else
                colorFore.Color = Color.Empty;
        }
    }
}