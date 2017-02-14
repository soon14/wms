using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Nodes.SystemManage
{
    public partial class FrmDispatch : DevExpress.XtraEditors.XtraForm
    {
        public FrmDispatch()
        {
            InitializeComponent();
        }

        private void frmLoad(object sender, EventArgs e)
        {

        }

        public void Reload()
        {
            try
            { 
                //加载单据状态数据

                //加载人员当前状态数据

                // 加载任务数据
            }
            catch
            { }
        }
    }
}