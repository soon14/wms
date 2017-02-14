using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.Entities;
using Nodes.Shares;
using Nodes.DBHelper;
using Nodes.Utils;
using Nodes.UI;

namespace Nodes.Outstore
{
    public partial class FrmSelectGroup : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DoUpdateGroup(string GroupNo);
        public DoUpdateGroup UpdateGroup;

        public FrmSelectGroup()
        {
            InitializeComponent();
        }

        private void FrmSelectGroup_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCommit_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboGroupNo.SelectedIndex < 0)
                {
                    MsgBox.Warn("请选中一个分组编号！");
                    cboGroupNo.Focus();
                    return;
                }
                UpdateGroup(cboGroupNo.Text.Trim());
                this.Close();
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
                return;
            }
        }
    }
}