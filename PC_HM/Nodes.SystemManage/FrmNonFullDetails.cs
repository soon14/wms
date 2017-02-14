using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nodes.Entities;
using Nodes.UI;

namespace Nodes.SystemManage
{
    /// <summary>
    /// 应退回套餐商品
    /// </summary>
    public partial class FrmNonFullDetails : DevExpress.XtraEditors.XtraForm
    {
        #region 变量
        private List<SODetailEntity> _list = null;
        #endregion

        #region 构造函数

        public FrmNonFullDetails()
        {
            InitializeComponent();
            this.lblReminder.Text = "因套餐中部分商品拣货量不足,请将以下商品退回!";
        }
        public FrmNonFullDetails(List<SODetailEntity> list)
            : this()
        {
            this.bindingSource1.DataSource = this._list = list;
        }
        #endregion

        #region 事件
        /// <summary>
        /// 确认
        /// </summary>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            DialogResult result = MsgBox.AskOK("是否确认已将商品退回？");
            if (result == DialogResult.OK)
            {
                this.DialogResult = result;
            }
        }
        #endregion
    }
}
