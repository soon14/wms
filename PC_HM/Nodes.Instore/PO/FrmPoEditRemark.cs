using System;
using System.Drawing;
using System.Windows.Forms;
using Nodes.Controls;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Shares;

namespace Nodes.Instore
{
    public partial class FrmPoEditRemark : DevExpress.XtraEditors.XtraForm
    {
        POBodyEntity PoHeader = null;
        PODal poDal = null;

        public FrmPoEditRemark(POBodyEntity poHeader)
        {
            InitializeComponent();

            this.PoHeader = poHeader;
            this.Text = string.Format("编辑备注(单号：{0})", poHeader.BillID);
        }

        private void OnSaveClick(object sender, EventArgs e)
        {
            int? color = null;
            if (colorFore.Color != Color.Empty)
                color = colorFore.Color.ToArgb();

            try
            {
                int result = poDal.UpdateRemark(PoHeader.BillID, txtRemark.Text.Trim(), color, GlobeSettings.LoginedUser.UserName);
                if (result == 1)
                {
                    this.PoHeader.Remark = txtRemark.Text.Trim();
                    this.PoHeader.RowForeColor = color;

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
            poDal = new PODal();
            txtRemark.Text = PoHeader.Remark;
            if (PoHeader.RowForeColor != null)
                colorFore.Color = Color.FromArgb(PoHeader.RowForeColor.Value);
            else
                colorFore.Color = Color.Empty;
        }
    }
}