using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.DBHelper;
using DevExpress.Utils;
using Nodes.Controls;
using Nodes.Utils;

namespace Reports
{
    public partial class FrmReturnedMoney : DevExpress.XtraEditors.XtraForm
    {
        public FrmReturnedMoney()
        {
            InitializeComponent();
        }
        ReportDal dal = new ReportDal();
        private void barItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (e.Item.Tag.ToString())
            { 
                case "查询":
                    using (new WaitDialogForm("正在查询...","请稍等"))
                    {
                        GetReturnedMoney();
                    }
                    break;
                case "本月回款":
                    using (new WaitDialogForm("正在查询...", "请稍等"))
                    {
                        GetAllReturnedMoney();
                    }
                    break;
            }
        }

        private void GetAllReturnedMoney()
        {
            DateTime dt = DateTime.Now;
            DateTime dt_First = dt.AddDays(1 - (dt.Day));
            barStart.EditValue = dt_First;
            barEnd.EditValue = dt;
            gridControl1.DataSource = dal.GetAllReturnedMoney();
        }

        private void GetReturnedMoney()
        {
            DateTime startTime;
            DateTime endTime;
            try
            {
                startTime = ConvertUtil.ToDatetime(barStart.EditValue.ToString());
                endTime = ConvertUtil.ToDatetime(barEnd.EditValue.ToString());
                gridControl1.DataSource = dal.GetReturnedMoney(startTime, endTime);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void FrmReturnedMoney_Load(object sender, EventArgs e)
        {
            this.barStart.EditValue = DateTime.Now.AddDays(-7);
            this.barEnd.EditValue = DateTime.Now;
            GetAllReturnedMoney();
        }
    }
}