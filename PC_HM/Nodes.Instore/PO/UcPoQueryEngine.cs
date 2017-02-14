using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.Controls;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Shares;
using Nodes.Utils;
using System.Diagnostics;

namespace Nodes.Instore
{
    public partial class UcPoQueryEngine : UserControl
    {
        POQueryDal poQueryDal = new POQueryDal();
        bool hasLoadData = false;

        public UcPoQueryEngine()
        {
            InitializeComponent();
        }

        public void LoadDataSource()
        {
            if (hasLoadData)
                return;

            hasLoadData = true;

            dateEditFrom.DateTime = DateTime.Now.AddMonths(-1);
            dateEditTo.DateTime = DateTime.Now;

            try
            {
                //绑定供应商
                listSuppliers.Properties.DataSource = new SupplierDal().ListActiveSupplierByPriority();

                //绑定采购业务员
                listSales.Properties.DataSource = new UserDal().ListUsersByRoleAndOrgCode(GlobeSettings.LoginedUser.OrgCode, GlobeSettings.POSalesRoleName);

                //绑定单据类型并默认选中第一个
                listBillTypes.Properties.DataSource = BaseCodeDal.GetItemList(BaseCodeConstant.PO_TYPE);

                //绑定单据状态
                listBillStates.Properties.DataSource = BaseCodeDal.GetItemList(BaseCodeConstant.PO_STATE);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        #region 公开的事件及函数
        public delegate void QueryComplete(List<POBodyEntity> dataSource);
        public event QueryComplete QueryCompleted;

        private void InitUI()
        {
            materialCode = null;
            billID = null;
            supplierCode = null;
            billTypeCode = null;
            billStateCode = null;
            dateFrom = null;
            dateTo = null;
            sales = null;
        }

        private string materialCode;
        private string billID;
        private string sales;
        private string billStateCode;
        private string supplierCode;
        private string billTypeCode;
        private DateTime? dateFrom;
        private DateTime? dateTo;

        public string QueryCondition;
        public string ElapsedTime;
        private bool OnlyNotComplete = false;

        /// <summary>
        /// 锁定某一状态，创建收货单时需要查询二审及正在收货的单据
        /// </summary>
        /// <param name="stateCode"></param>
        public void LockThisState(string stateCode)
        {
            listBillStates.EditValue = stateCode;
            listBillStates.Enabled = false;
            listBillStates.RefreshEditValue();
        }

        public void DoQueryNotCompleteBill(string queryCondition)
        {
            if (!string.IsNullOrEmpty(queryCondition))
                this.QueryCondition = queryCondition;

            OnlyNotComplete = true;
            watch.Start();
            List<POBodyEntity> result = poQueryDal.QueryNotClosedBills(GlobeSettings.LoginedUser.OrgCode);
            watch.Stop();

            ElapsedTime = string.Format("查询完成：耗时{0}秒", watch.ElapsedMilliseconds / 1000f);
            watch.Reset();

            if (QueryCompleted != null)
                QueryCompleted(result);
        }

        public void DoQuery(string billStateCode, string queryCondition)
        {
            this.QueryCondition = queryCondition;
            DoQuery(null, billStateCode, null, null, null, null, null, null);
        }

        public void Reload()
        {
            if (OnlyNotComplete)
                DoQueryNotCompleteBill(null);
            else
                DoQuery(this.billID, this.billStateCode, this.supplierCode, this.billTypeCode,
                    this.materialCode, this.sales, this.dateFrom, this.dateTo);
        }

        public void DoQuery(DateTime dateFrom, DateTime dateTo, string queryCondition)
        {
            this.QueryCondition = queryCondition;
            DoQuery(null, null, null, null, null, null, this.dateFrom, this.dateTo);
        }

        Stopwatch watch = new Stopwatch();
        private void DoQuery(string billID, string billStateCode, string supplierCode,
            string billTypeCode, string materialCode, string sales, DateTime? dateFrom, DateTime? dateTo)
        {
            try
            {
                OnlyNotComplete = false;

                this.billID = billID;
                this.billStateCode = billStateCode;
                this.supplierCode = supplierCode;
                this.billTypeCode = billTypeCode;
                this.materialCode = materialCode;
                this.sales = sales;
                this.dateFrom = dateFrom;
                this.dateTo = dateTo;

                watch.Start();
                List<POBodyEntity> result = poQueryDal.QueryBills(GlobeSettings.LoginedUser.OrgCode, billID,
                    billStateCode, supplierCode, billTypeCode, materialCode, sales, dateFrom, dateTo);
                watch.Stop();

                ElapsedTime = string.Format("查询完成：耗时{0}秒", watch.ElapsedMilliseconds / 1000f);
                watch.Reset();

                if (QueryCompleted != null)
                    QueryCompleted(result);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        #endregion

        private void OnQueryClick(object sender, EventArgs e)
        {
            string billID = ConvertUtil.StringToNull(txtBillID.Text);
            string billStateCode = ConvertUtil.ObjectToNull(listBillStates.EditValue);
            string billTypeCode = ConvertUtil.ObjectToNull(listBillTypes.EditValue);
            string supplierCode = ConvertUtil.ObjectToNull(listSuppliers.EditValue);
            string materialCode = ConvertUtil.StringToNull(txtMaterial.Text);
            string sales = ConvertUtil.StringToNull(listSales.Text);
            DateTime? dateFrom = null, dateTo = null;
            if (dateEditFrom.EditValue != null)
                dateFrom = dateEditFrom.DateTime.Date;

            if (dateEditTo.EditValue != null)
                dateTo = dateEditTo.DateTime.AddDays(1).Date;

            string separator = " && ";
            this.QueryCondition = string.Format(@"{0}{1}{2}{3}{4}{5}{6}",
                string.IsNullOrEmpty(billID) ? "" : "采购单号=" + billID + separator,
                string.IsNullOrEmpty(supplierCode) ? "" : "供应商=" + listSuppliers.Text + separator,
                string.IsNullOrEmpty(billTypeCode) ? "" : "业务类型=" + listBillTypes.Text + separator,
                string.IsNullOrEmpty(billStateCode) ? "" : "单据状态=" + listBillStates.Text + separator,
                string.IsNullOrEmpty(materialCode) ? "" : "物料包含'" + materialCode + "'" + separator,
                string.IsNullOrEmpty(sales) ? "" : "业务员=" + sales + separator,
                "建单日期介于【" + dateFrom.Value.ToShortDateString() + "】与【" + dateTo.Value.AddDays(-1).ToShortDateString() + "】之间");

            DoQuery(billID, billStateCode, supplierCode, billTypeCode,
                materialCode, sales, dateFrom, dateTo);
        }

        private void OnCleanTextClick(object sender, EventArgs e)
        {
            txtBillID.Text = txtMaterial.Text = null;
            listBillStates.EditValue = listBillTypes.EditValue = listSuppliers.EditValue = listSales.EditValue = null;
        }

        private void OnControlKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                OnQueryClick(sender, e);
        }

        private void OnLookUpEditButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            BaseEdit editor = sender as BaseEdit;
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
                editor.EditValue = null;
        }

        private void OnBillStateButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
            {
                listBillStates.EditValue = null;
                listBillStates.RefreshEditValue();
            }
        }
    }
}
