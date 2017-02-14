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
    public partial class FrmDistribution : DevExpress.XtraEditors.XtraForm
    {
        public FrmDistribution()
        {
            InitializeComponent();
        }
        ReportDal dal = new ReportDal();
        private void FrmDistribution_Load(object sender, EventArgs e)
        {
            GetAllDistribution();
        }
       

        private void GetAllDistribution()
        {
            SetTime();
            gridControl1.DataSource = dal.GetAllDistribution();
        }

        private void barItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (e.Item.Tag.ToString())
            { 
                case "查询":
                    using (new WaitDialogForm("查询中...", "请稍等"))
                    {
                        GetDistribution();
                    }
                    break;
                case  "本月配货率":
                    using (new WaitDialogForm("查询中...","请稍等"))
                    {
                        GetAllDistribution();
                    }
                    break;
            }
        }
        private void GetDistribution()
        { 
            DateTime startTime;
            DateTime endTime;
            try
            {
                startTime = ConvertUtil.ToDatetime(barStart.EditValue).Date;
                endTime = ConvertUtil.ToDatetime(barEnd.EditValue).AddDays(1).Date;
                gridControl1.DataSource = dal.GetDistribution(startTime, endTime);
            }
            catch( Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void SetTime()
        {
            DateTime dt = DateTime.Now;
            DateTime dt_First = dt.AddDays(1 - (dt.Day));
            barStart.EditValue = dt_First;
            barEnd.EditValue = dt;
        }
    }
}