using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.UI;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.Shares;

namespace Nodes.Outstore
{
    public partial class FrmCreateCRNBill : DevExpress.XtraEditors.XtraForm
    {
        private SOHeaderEntity soHeader = null;
        private SODal soDal;
        private UnitDal unitDal = null;
        private ReturnDal returnDal = null;
        private List<ReturnDetailEntity> lstDetail;
        private bool isEdite = false;

        public FrmCreateCRNBill(SOHeaderEntity selectedHeader)
        {
            InitializeComponent();

            this.soHeader = selectedHeader;
            
            this.Text = string.Format("新增退货单(出库单号：{0})", soHeader.BillID);
        }

        private void FrmCreateCRNBill_Load(object sender, EventArgs e)
        {
            try
            {
                soDal = new SODal();
                unitDal = new UnitDal();
                returnDal = new ReturnDal();
                dateReturnDate.DateTime = DateTime.Now;
                //获取选中销售单的明细
                GetSoDetails();
                BindiReturnReason();
                GetVehicleInfo();
                txtHandingPerson.Text = GlobeSettings.LoginedUser.UserName;
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
                return;
            }
        }

        private void GetVehicleInfo()
        { 
            string driverName = "";
            string vehicleNo = returnDal.GetVhicleInfo(soHeader.BillID, out driverName);
            txtReturnDriver.Text = driverName;
        }

        private void BindiReturnReason()
        {
            listReturnReason.Properties.DataSource = BaseCodeDal.GetItemList(BaseCodeConstant.RETURN_REASON);
        }

        private void GetSoDetails()
        {
            lstDetail = new List<ReturnDetailEntity>();
            lstDetail = returnDal.GetReturnDetails(soHeader.BillID);
            gridDetails.DataSource = lstDetail;
            gvDetails.ViewCaption = string.Format("单据号: {0};  客户名称：{1}", soHeader.BillNO, soHeader.CustomerName);
        }

        private void GetReturnUnit(string skuCode)
        {
            try
            {
                listReturnUnit.Properties.DataSource = returnDal.GetAllUnitBySku(skuCode);
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
                return;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtReturnQty.Text = "";
            listReturnUnit.EditValue = null;
            txtReturnQty.Focus();
        }

        private void btnCommit_Click(object sender, EventArgs e)
        {
            ReturnDetailEntity selectDetail = SelectedDetail;
            if (selectDetail == null)
            {
                MsgBox.Warn("没有选中的行！");
                return;
            }
            if (string.IsNullOrEmpty(txtReturnQty.Text.Trim()))
            {
                MsgBox.Warn("退货数量不能为空！");
                txtReturnQty.Focus();
                return;
            }
            if (!ConvertUtil.IsDecimal(txtReturnQty.Text.Trim()))
            { 
                MsgBox.Warn("退货数量必须是数字！");
                txtReturnQty.Focus();
                return;
            }
            if (ConvertUtil.ToDecimal(txtReturnQty.Text.Trim()) < 0)
            {
                MsgBox.Warn("退货数量不能小于0！");
                txtReturnQty.Focus();
                return;
            }
            if (ConvertUtil.ToString(listReturnUnit.EditValue) == string.Empty)
            {
                MsgBox.Warn("退货单位不能为空！");
                listReturnUnit.Focus();
                return;
            }
            try
            {
                decimal minReturnQty = 0;
                if (selectDetail.ReturnUnitCode == ConvertUtil.ToString(listReturnUnit.EditValue))
                {
                    minReturnQty = ConvertUtil.ToDecimal(txtReturnQty.Text.Trim());
                }
                else
                {
                    //castRate = returnDal.GetCastRateBySku(selectDetail.MaterialCode, selectDetail.SkuBarcode, selectDetail.UnitCode, minUnitCode);
                    minReturnQty = selectDetail.CastRate * ConvertUtil.ToDecimal(txtReturnQty.Text.Trim());
                }

                if (minReturnQty > selectDetail.MinPickQty - selectDetail.ReturnedQty)
                {
                    MsgBox.Warn("退货数量超出！");
                    txtReturnQty.Focus();
                    return;
                }

                selectDetail.ReturnQty = minReturnQty;
                selectDetail.ReturnUnitName = selectDetail.ReturnUnitName;
                selectDetail.ReturnUnitCode = selectDetail.ReturnUnitCode;
                
                gvDetails.RefreshData();
                txtCrnAmount.Text = String.Format("{0:f2}", gridColumn9.SummaryItem.SummaryValue);
                this.isEdite = true;
                btnClear_Click(null, null);
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
                return;
            }
        }

        ReturnDetailEntity SelectedDetail
        {
            get
            {
                if (gvDetails.FocusedRowHandle < 0)
                {
                    return null;
                }
                else
                {
                    return gvDetails.GetFocusedRow() as ReturnDetailEntity;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (this.isEdite == true && MsgBox.AskYes("关闭之后数据将丢失，是否关闭？") != DialogResult.Yes)
            {
                return;
            }
            this.Close();
        }

        string errMsg = string.Empty;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHandingPerson.Text.Trim()))
            {
                MsgBox.Warn("经办人不能为空！");
                txtHandingPerson.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtReturnDriver.Text.Trim()))
            {
                MsgBox.Warn("退货司机不能为空！");
                txtReturnDriver.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtCrnAmount.Text.Trim()))
            {
                MsgBox.Warn("退货金额不能为空！");
                txtCrnAmount.Focus();
                return;
            }
            if (!ConvertUtil.IsDecimal(txtCrnAmount.Text.Trim()))
            {
                MsgBox.Warn("退货金额必须是数字！");
                txtCrnAmount.Focus();
                return;
            }
            if (ConvertUtil.ToDecimal(txtCrnAmount.Text.Trim()) < 0)
            {
                MsgBox.Warn("退货金额不能小于0！");
                txtCrnAmount.Focus();
                return;
            }
            if (listReturnReason.EditValue == null)
            {
                MsgBox.Warn("退货原因不能为空！");
                listReturnReason.Focus();
                return;
            }
            if (MsgBox.AskOK("确定保存该退货单吗？") != DialogResult.OK) return;
            //保存退货单
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                returnDal.SaveReturnBill(soHeader, lstDetail, GlobeSettings.LoginedUser.WarehouseCode, ConvertUtil.ToDatetime(dateReturnDate.EditValue), 
                    txtReturnDriver.Text.Trim(), txtHandingPerson.Text.Trim(), txtWmsRemark.Text.Trim(), ConvertUtil.ToDecimal(txtReturnAmount.Text.Trim()), 
                    GlobeSettings.LoginedUser.UserCode, ConvertUtil.ToString(listReturnReason.EditValue), txtReturnRemark.Text, 
                    ConvertUtil.ToDecimal(txtCrnAmount.Text.Trim()), out errMsg);

                if (!string.IsNullOrEmpty(errMsg))
                {
                    MsgBox.Warn(errMsg);
                }
                else
                {
                    MsgBox.OK("保存成功！");
                    this.isEdite = false;
                    //GetSoDetails();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void OnLookUpEditButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            BaseEdit editor = sender as BaseEdit;
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
                editor.EditValue = null;
        }

        private void gvDetails_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                ReturnDetailEntity selectDetail = SelectedDetail;
                GetReturnUnit(selectDetail.MaterialCode);
                listReturnUnit.Text = selectDetail.ReturnUnitName;
                listReturnUnit.Focus();
                txtReturnQty.Focus();
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
                return;
            }
        }

        private void txtReturnQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnCommit_Click(null, null);
            }
        }


        private void FrmCreateCRNBill_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.isEdite == true && MsgBox.AskYes("关闭之后数据将丢失，是否关闭？") != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }
    }
}