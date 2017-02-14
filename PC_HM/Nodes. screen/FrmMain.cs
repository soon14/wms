using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nodes.UI;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;

namespace Nodes.screen
{
    public partial class FrmMain : Form
    {
        FrmVehicleAndPickTask frmTask = new FrmVehicleAndPickTask();
        FrmEfficiencyScreen frmEfficiency = new FrmEfficiencyScreen();
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmLoad(object sender, EventArgs e)
        {
            LoadData();
            txtShowRowsDay.Text = GlobalSettings.DefaultShowRowsDay.ToString();
            txtShowRowsMonth.Text = GlobalSettings.DefaultSHowRowsMonth.ToString();
            this.xtraTabControl1.Focus();
        }

        private void LoadData()
        {
            try
            {
                //加载权重系数
                 List<BaseCodeEntity> listWeightRaido = BaseCodeDal.GetItemList(BaseCodeConstant.WEIGHT_RAIDO);
                 foreach (BaseCodeEntity baseCode in listWeightRaido)
                 {
                     switch (baseCode.ItemDesc)
                     {
                         case "正常拣货系数":
                             GlobalSettings.NormalPickRatio = ConvertUtil.ToDecimal(baseCode.ItemValue.Length >= 2 ? baseCode.ItemValue.Substring(1) : "0");
                             break;
                         case "批示拣货系数":
                             GlobalSettings.MostPickRatio = ConvertUtil.ToDecimal(baseCode.ItemValue.Length >= 2 ? baseCode.ItemValue.Substring(1) : "0");
                             break;
                         case "司机配送系数（箱货）":
                             GlobalSettings.DispatchingRatio1_221 = ConvertUtil.ToDecimal(baseCode.ItemValue.Length >= 2 ? baseCode.ItemValue.Substring(1) : "0");
                             break;
                         case "司机配送系数（金杯）":
                             GlobalSettings.DispatchingRatio1_220 = ConvertUtil.ToDecimal(baseCode.ItemValue.Length >= 2 ? baseCode.ItemValue.Substring(1) : "0");
                             break;
                         case "司机助理配送系数":
                             GlobalSettings.DispatchingRatio2 = ConvertUtil.ToDecimal(baseCode.ItemValue.Length >= 2 ? baseCode.ItemValue.Substring(1) : "0");
                             break;
                         case "装车系数":
                             GlobalSettings.LoadingRatio = ConvertUtil.ToDecimal(baseCode.ItemValue.Length >= 2 ? baseCode.ItemValue.Substring(1) : "0");
                             break;
                         case "叉车系数":
                             GlobalSettings.ForkliftRatio = ConvertUtil.ToDecimal(baseCode.ItemValue.Length >= 2 ? baseCode.ItemValue.Substring(1) : "0");
                             break;
                         default:
                             break;
                     }
                 }
                //加载大屏窗体
                frmTask.Visible = true;
                frmTask.Dock = DockStyle.Fill;
                frmTask.FormBorderStyle = FormBorderStyle.None;
                frmTask.TopLevel = false;
                xtraTabPage1.Controls.Add(frmTask);

                frmEfficiency.Visible = true;
                frmEfficiency.Dock = DockStyle.Fill;
                frmEfficiency.FormBorderStyle = FormBorderStyle.None;
                frmEfficiency.TopLevel = false;
                xtraTabPage2.Controls.Add(frmEfficiency);

                xtraTabControl1.SelectedTabPageIndex = 0;
                timeChange.Enabled = true;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void timeChange_Tick(object sender, EventArgs e)
        {
            
            if(xtraTabControl1.SelectedTabPageIndex==0)
                xtraTabControl1.SelectedTabPageIndex = 1;
            else
                xtraTabControl1.SelectedTabPageIndex = 0;
        }

        private void FrmClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void TxtEditValueChange(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.TextEdit col = (DevExpress.XtraEditors.TextEdit)sender;
            if (String.IsNullOrEmpty(col.Text.Trim()))
            {
                MsgBox.Warn("不能为空。");
                return;
            }
            if (col.Tag.ToString() == "Day")
            {
                GlobalSettings.DefaultShowRowsDay = ConvertUtil.ToInt(col.Text.Trim());
            }
            else if (col.Tag.ToString() == "Month")
            {
                GlobalSettings.DefaultSHowRowsMonth = ConvertUtil.ToInt(col.Text.Trim());
            }
        }
    }
}
