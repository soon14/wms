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
using Nodes.Shares;

namespace Nodes.Outstore
{
    /// <summary>
    /// 此功能专门为7-1库设计，在电子标签拣货前，先扫描物流箱跟表格中的第一个单据关联
    /// </summary>
    public partial class FrmScanContainer : DevExpress.XtraEditors.XtraForm
    {
        private SODal soDal = new SODal();

        public FrmScanContainer()
        {
            InitializeComponent();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            DoReload();
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                DoCommit();
        }

        private void OnReloadClick(object sender, EventArgs e)
        {
            DoReload();
        }

        /// <summary>
        /// 处理扫描的物流箱与第一个单据的关联
        /// </summary>
        private void DoCommit()
        {
            string barcode = txtBarcode.Text.Trim();
            txtBarcode.Text = "";
            if (string.IsNullOrEmpty(barcode))
            {
                lblMsg.Show("请扫描物流箱条码。", false);
                return;
            }

            if (bindingSource1.Count == 0)
            {
                //如果没有数据，重新刷新一下，如果仍然没有数据，证明确实没有单据
                DoReload();

                if (bindingSource1.Count == 0)
                {
                    lblMsg.Show("列表没有单据，请刷新单据后重试。", false);
                    return;
                }
            }

            //开始保存
            try
            {
                SOHeaderEntity header = gvHeader.GetRow(0) as SOHeaderEntity;
                int result = soDal.JoinContainerBill(barcode, header.BillID, GlobeSettings.LoginedUser.UserName);
                if (result == 1)
                {
                    //保存完成后，刷新一下数据
                    lblMsg.Show(string.Format("物流箱“{0}”与单据“{1}”关联成功。", barcode, header.BillNO), true);
                }
                else if(result == -1)
                {
                    lblMsg.Show("未查到单据，可能已经被删除，请刷新后重试。", false);
                }
                else if (result == -2)
                {
                    lblMsg.Show("单据“{0}”状态不是等待拣货，请刷新后重试。", false);
                }
                else if (result == -3)
                {
                    lblMsg.Show(string.Format("未查到物流箱“{0}”，条码有误。", barcode), false);
                }
                else if (result == -4)
                {
                    lblMsg.Show(string.Format("物流箱“{0}”被占用中，不能同时关联多张单据。", barcode), false);
                }

                DoReload();

                txtBarcode.SelectAll();
                txtBarcode.Focus();
            }
            catch (Exception ex)
            {
                lblMsg.Show(ex.Message, false);
                txtBarcode.SelectAll();
                txtBarcode.Focus();
            }
        }

        private void DoReload()
        {
            //加载状态为已拣配计算与已分派任务的单据
            bindingSource1.DataSource = soDal.QueryBillsByStatus(
                BaseCodeConstant.SO_WAIT_TASK + "," + 
                BaseCodeConstant.SO_WAIT_PICKING, 0
                //+ "," + 
                //BaseCodeConstant.SO_DO_PICKING + "," +
                //BaseCodeConstant.SO_WAIT_WEIGHT
                );
        }
    }
}