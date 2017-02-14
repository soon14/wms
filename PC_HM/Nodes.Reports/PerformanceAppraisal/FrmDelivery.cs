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
    public partial class FrmDelivery : DevExpress.XtraEditors.XtraForm
    {
        public FrmDelivery()
        {
            InitializeComponent();
        }
        ReportDal dal = new ReportDal();
        private void barItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (e.Item.Tag.ToString())
            { 
                case "查询":
                    using (new WaitDialogForm("正在查询...", "请稍等"))
                    {
                        GetDelivery();
                    }
                    break;
                case "本月发货率":
                    using (new WaitDialogForm("正在查询...","请稍等"))
                    {
                        GetAllDelivery();
                    }
                    break;
            }
        }

        private void FrmDelivery_Load(object sender, EventArgs e)
        {
            GetAllDelivery();
        }

        private void GetAllDelivery()
        {
            DateTime dt = DateTime.Now;
            DateTime dt_First = dt.AddDays(1 - (dt.Day));
            barStart.EditValue = dt_First;
            barEnd.EditValue = dt;
            gridControl1.DataSource = dal.GetAllDelivery();
        }

        private void GetDelivery()
        {
            DateTime startTime;
            DateTime endTime;
            try
            {
                startTime = ConvertUtil.ToDatetime(barStart.EditValue).Date;
                endTime = ConvertUtil.ToDatetime(barEnd.EditValue).AddDays(1).Date;
                gridControl1.DataSource = dal.GetDelivery(startTime, endTime);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
    }
}