using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.UI;

namespace Nodes.CycleCount
{
    public partial class FrmSelectDate : DevExpress.XtraEditors.XtraForm
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public FrmSelectDate()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dateFrom.EditValue == null)
            {
                MsgBox.Warn("请选择起始日期。");
                return;
            }

            if (dateTo.EditValue == null)
            {
                MsgBox.Warn("请选择截止日期。");
                return;
            }

            DateFrom = dateFrom.DateTime.Date;
            DateTo = dateTo.DateTime.Date.AddDays(1);

            if (DateFrom >= DateTo)
                MsgBox.Warn("截止日期不能小于起始日期。");
            else
                this.DialogResult = DialogResult.OK;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            //默认为当天
            dateFrom.DateTime = DateTime.Now;
            dateTo.DateTime = DateTime.Now;
        }
    }
}