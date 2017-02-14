using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.UI;
using Nodes.DBHelper;
using Nodes.Utils;
using DevExpress.XtraLayout;
using Nodes.Shares;

namespace Nodes.Tools
{
    public partial class FrmToolMain : DevExpress.XtraEditors.XtraForm
    {
        public FrmToolMain()
        {
            InitializeComponent();
        }

        private void btn_Return_Modify_Click(object sender, EventArgs e)
        {
            try
            {
                label1.Text = String.Empty;
                if (Validate(txtReturnBillID))
                {
                    string result = ToolsDal.Tools_Return_Modify(ConvertUtil.ToInt(txtReturnBillID.Text));
                    SetResult(result, label1);
                }
            }
            catch (Exception ex)
            {
                label1.Text = " ";
                MsgBox.Err(ex.Message);
            }
        }

        public bool Validate(TextEdit txt)
        {
            if (String.IsNullOrEmpty(txt.Text))
            {
                MsgBox.Warn("文本框不能为空！");
                return false;
            }
            return true;
        }

        public void SetResult(string result,SimpleLabelItem label)
        {
            if (result == "Y")
            {
                label.Text =String.Format( "    修改成功");
                label.AppearanceItemCaption.BackColor = Color.PaleGreen;
                LogDal.Insert(Nodes.Entities.ELogType.应急处理_退货单, String.Format("{0}:{1}", GlobeSettings.LoginedUser.UserName, GlobeSettings.LoginedUser.UserCode), txtReturnBillID.Text, "应急处理");
            }
            else
            {
                label.Text = String.Format("    {0}",result);
                label.AppearanceItemCaption.BackColor = Color.Red;
            }
        }
        
    }
}