using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;

namespace Nodes.WMS.Stock
{
    public partial class FrmStockOccupyManager : DevExpress.XtraEditors.XtraForm
    {
        OccupyRecordDal occupydal = new OccupyRecordDal();
        private string  BillState, Creator;
        private DateTime DateFrom, DateTo;
        public FrmStockOccupyManager()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            BindData();
        }

        private OccupyRecordEntity SelectedOccupyRecord
        {
            get
            {
                if (gridView1.FocusedRowHandle < 0)
                    return null;
                else
                    return gridView1.GetFocusedRow() as OccupyRecordEntity;
            }
        }
        /// <summary>
        /// 默认系统加载数据
        /// </summary>
        private void InitDate()
        {
            dateEditFrom.DateTime = System.DateTime.Now.AddDays(-7);
            dateEditTo.DateTime = System.DateTime.Now.AddDays(7);
        }

        private void BindData()
        {
            if (ConvertUtil.ToInt(rdStatus.EditValue) == 0)
                BillState = SysCodeConstant.OCCUPY_STATUS_OK;
            else if (ConvertUtil.ToInt(rdStatus.EditValue) == 1)
                BillState = SysCodeConstant.OCCUPY_STATUS_CANCLE;
            else
                BillState = null;
            DateFrom=dateEditFrom.DateTime.Date;
            DateTo = dateEditTo.DateTime.Date;
            if (DateFrom > DateTo)
            {
                MsgBox.Warn("开始时间不能大于结束时间。");
                return;
            }

            if (DateFrom.Subtract(DateTo).Days > 180)
            {
                MsgBox.Warn("时间区间不能超过180天。");
                return;
            }

            List<OccupyRecordEntity> listOccupyRecord = occupydal.QueryOccupyRecod(Creator, DateFrom, DateTo, BillState);
            gridControl1.DataSource = listOccupyRecord;
        }

        private void FrmStockOccupyManager_Load(object sender, EventArgs e)
        {
            InitDate();
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            UpdateOccupyStatus();
        }

        private void UpdateOccupyStatus()
        {
            if (SelectedOccupyRecord == null)
            {
                MsgBox.Warn("请选择需要解除占用的行。");
                return;
            }

            if (SelectedOccupyRecord.Status == SysCodeConstant.OCCUPY_STATUS_CANCLE)
            {
                MsgBox.Err("此行已解除占用。");
                return;
            }

            if (MsgBox.AskOK(string.Format("确认解除占用物资{0}", SelectedOccupyRecord.MaterialName)) == DialogResult.Cancel)
                return;
            int Result = occupydal.OccupyRecordStatusEdit(SelectedOccupyRecord.OccupyID, SysCodeConstant.OCCUPY_STATUS_CANCLE, SelectedOccupyRecord.StockID, SelectedOccupyRecord.OccupyQty);
            if (Result > 0)
            {
                MsgBox.OK("解除占用成功。");
                BindData();
            }
            else
                MsgBox.Err("解除占用失败。");
        }
    }
}