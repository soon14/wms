using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.UI;
//using Nodes.DBHelper;
using Nodes.Utils;
using Nodes.Entities;
using System.Linq;
using Nodes.Stock;
using Nodes.Shares;
using DevExpress.Utils;
using Nodes.Resources;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Stock;
using Nodes.Entities.HttpEntity.Outstore;
using Newtonsoft.Json;

namespace Nodes.Outstore
{
    public partial class FrmShowNeedSKU : DevExpress.XtraEditors.XtraForm
    {
        #region 变量

        //private ReplenishDal repDal = new ReplenishDal();
        private ESearchType _searchType = ESearchType.未排序订单;

        BillSKUNum BillskuNum
        {
            get
            {
                if (gridView1.FocusedRowHandle < 0)
                    return null;
                else
                    return gridView1.GetFocusedRow() as BillSKUNum;
            }
        }

        #endregion

        #region 构造函数

        public FrmShowNeedSKU()
        {
            InitializeComponent();
        }

        #endregion
        #region "选中与复选框"
        private void LoadCheckBoxImage()
        {
            gridView1.Images = GridUtil.GetCheckBoxImages();
            colmChecked.ImageIndex = 0;
        }

        private void OnViewMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                CheckOneGridColumn(gridView1, "HasChecked", MousePosition);
            }
        }

        //private void OnViewCellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        //{
        //    if (e.Column.FieldName != "HasChecked") return;
        //    BillSKUNum billskunum = BillskuNum;

        //    if (billskunum == null) return;

        //    billskunum.HasChecked = ConvertUtil.ToBool(e.Value);
        //    gridView1.CloseEditor();
        //}

        private void CheckOneGridColumn(GridView view, string checkedField, Point mousePosition)
        {
            Point p = view.GridControl.PointToClient(mousePosition);
            GridHitInfo hitInfo = view.CalcHitInfo(p);
            if (hitInfo.HitTest == GridHitTest.Column && hitInfo.Column.FieldName == checkedField)
            {
                List<BillSKUNum> _data = gridView1.DataSource as List<BillSKUNum>;
                if (_data == null) return;

                int currentIndex = hitInfo.Column.ImageIndex;
                bool flag = currentIndex == 0;
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    if (gridView1.IsRowVisible(i) == RowVisibleState.Visible)
                    {
                        gridView1.SetRowCellValue(i, "HasChecked", flag);

                    }
                }
                //_data.ForEach(d => d.HasChecked = flag);
                hitInfo.Column.ImageIndex = 4 - currentIndex;
            }
        }
        #endregion
        #region 方法
        /// <summary>
        /// 当前订单量－当前订单量 未排序订单，传60,,已排序订单，不传

        /// </summary>
        /// <param name="wType"></param>
        /// <param name="billState"></param>
        /// <returns></returns>
        public List<BillSKUNum> GetBillPlans(EWarehouseType wType, string billState)
        {
            List<BillSKUNum> list = new List<BillSKUNum>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("wType=").Append(EUtilStroreType.WarehouseTypeToInt(wType)).Append("&");
                loStr.Append("billState=").Append(billState);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetBillPlans, 300000);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetBillPlans bill = JsonConvert.DeserializeObject<JsonGetBillPlans>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return list;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return list;
                }
                #endregion
                #region 赋值

                BillSKUNum sku = null;
                foreach (JsonGetBillPlansResult tm in bill.result)
                {
                    sku = new BillSKUNum();
                    sku.SKUCode = tm.skuCode;
                    sku.SKUName = tm.skuName;
                    sku.Spec = tm.spec;
                    sku.UmCode = tm.umCode;
                    sku.UmName = tm.umName;
                    sku.Qty = Convert.ToDecimal(tm.qty);
                    sku.BillQty = Convert.ToDecimal(tm.billQty);
                    sku.TotalQty = StringToDecimal.GetTwoDecimal(tm.totalQty);
                    sku.StockTotalQty = StringToDecimal.GetTwoDecimal(tm.stockTotalQty);
                    sku.BackupQty = StringToDecimal.GetTwoDecimal(tm.backupQty);
                    sku.IsCase = Convert.ToInt32(tm.isCase);
                    sku.Flag = Convert.ToInt32(tm.flag);
                    sku.AdviceQty = tm.qtyAdviceQty;
                    sku.AdviceUmName = tm.adviceUmName;

                    list.Add(sku);
                }
                return list;
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return list;
        }

        private void LoadData()
        {
            LoadCheckBoxImage();

            //根据类型绑定数据
            List<BillSKUNum> list = new List<BillSKUNum>();
            if (this._searchType == ESearchType.未排序订单)
                list = GetBillPlans(GlobeSettings.LoginedUser.WarehouseType, "60");
            else
                list = GetBillPlans(GlobeSettings.LoginedUser.WarehouseType, "");

            gridControl1.DataSource = list;
        }
        private void DoClickEvent(string tag)
        {
            switch (tag)
            {
                case "刷新":
                    LoadData();
                    break;
                case "生成补货任务":
                    DoCalc();
                    break;
                case "单品补货":
                    // 找到缺货商品
                    List<BillSKUNum> shortSummary = gridControl1.DataSource as List<BillSKUNum>;
                    List<BillSKUNum> list = shortSummary.FindAll(u => u.State == "N");
                    using (FrmSingleReplenish frmSingle = new FrmSingleReplenish(list))
                    {
                        frmSingle.ShowDialog();
                    }
                    break;
                case "安全库存触发补货":
                    SafeSup();
                    break;
                case "未排序订单":
                    this._searchType = (ESearchType)Enum.Parse(typeof(ESearchType), tag);
                    this.LoadData();
                    break;
                case "已排序订单":
                    this._searchType = (ESearchType)Enum.Parse(typeof(ESearchType), tag);
                    this.LoadData();
                    break;
            }
        }

         /// <summary>
        /// 获取没有分派的补货任务
        /// </summary>
        /// <returns></returns>
        public int GetSupQty()
        {
            int ret = -1;
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_GetSupQty);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return ret;
                }
                #endregion

                #region 正常错误处理

                JsonGetNumOfContainer bill = JsonConvert.DeserializeObject<JsonGetNumOfContainer>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return ret;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return ret;
                }
                #endregion

                if (bill.result != null && bill.result.Length > 0)
                    return Convert.ToInt32(bill.result[0].counts);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return ret;
        }

        private void SafeSup()
        {
            int supQty = GetSupQty();
            if (supQty>0)
            {
                MsgBox.Warn("请将当前补货任务做完再重新操作！");
                return;
            }

            FrmSafeSup frmSafeSup = new FrmSafeSup();
            frmSafeSup.Show();
        }

        /// <summary>
        /// 删除临时补货
        /// </summary>
        /// <returns></returns>
        public bool DeleteTempReplenish()
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_DeleteTempReplenish);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return false;
                }
                #endregion

                #region 正常错误处理

                Sucess bill = JsonConvert.DeserializeObject<Sucess>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return false;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return false;
                }
                #endregion

                return true;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return false;
        }

        /// <summary>
        /// 当前订单量（获取结果集）
        /// </summary>
        /// <param name="gID"></param>
        /// <returns></returns>
        public List<StockTransEntity> GetResultByGID(string gID)
        {
            List<StockTransEntity> list = new List<StockTransEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("gId=").Append(gID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetResultByGID);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetResultByGID bill = JsonConvert.DeserializeObject<JsonGetResultByGID>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return list;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return list;
                }
                #endregion
                #region 赋值

                foreach (JsonGetResultByGIDResult tm in bill.result)
                {
                    StockTransEntity sku = new StockTransEntity();
                    //tm.fromStockId;
                    sku.IsCase = Convert.ToInt32(tm.isCase);
                    sku.Location = tm.Location;
                    sku.Material = tm.Material;
                    sku.TransferQty = Convert.ToDecimal(tm.TransferQty);
                    sku.MaterialName = tm.skuName;
                    sku.Spec = tm.spec;
                    sku.TargetLocation = tm.TargetLocation;
                    sku.UnitName = tm.umName;
                    list.Add(sku);
                }
                return list;
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return list;
        }

        /// <summary>
        /// 通过商品编码审计
        /// </summary>
        /// <param name="skuCode"></param>
        /// <param name="shortQty"></param>
        /// <param name="gID"></param>
        /// <param name="isCase"></param>
        /// <returns></returns>
        public bool InquiryBySku(string skuCode, decimal shortQty, string gID, int isCase)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("skuCode=").Append(skuCode).Append("&");
                loStr.Append("shortQty=").Append(shortQty).Append("&");
                if (GlobeSettings.LoginedUser.WarehouseType == EWarehouseType.混合仓)
                    loStr.Append("isCase=").Append(isCase).Append("&");
                loStr.Append("gId=").Append(gID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_InquiryBySku);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return false;
                }
                #endregion

                #region 正常错误处理

                Sucess bill = JsonConvert.DeserializeObject<Sucess>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return false;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return false;
                }
                #endregion

                return true;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return false;
        }

        private void DoCalc()
        {
            List<BillSKUNum> shortSummary = gridControl1.DataSource as List<BillSKUNum>;


            if (shortSummary == null || shortSummary.Count <= 0)
                return;

            //var groupedSummary = from p in shortSummary 
            //                     where p.State.Contains('N')
            //                     group p by p.SKUCode into g
            //                     select new
            //                     {
            //                         g.Key,
            //                         ShortQty = g.Sum(p => p.TotalQty*p.Qty)
            //                     };

            DeleteTempReplenish();



            string gid = Guid.NewGuid().ToString().Replace("-", "");
            foreach (BillSKUNum a in shortSummary)
            {
                decimal tempQty = a.BillQty - a.TotalQty;
                if (tempQty > 0 && (a.StockTotalQty > 0 || a.BackupQty > 0) && a.HasChecked != false)
                    InquiryBySku(a.SKUCode, tempQty * a.Qty, gid, a.IsCase);
            }

            List<StockTransEntity> results = GetResultByGID(gid);
            FrmCreateReplenishBill frm = new FrmCreateReplenishBill(results, false);
            frm.ShowDialog();

            //刷新
            LoadData();
        }
        #endregion

        #region 事件

        private void FrmShowNeedSKU_Load(object sender, EventArgs e)
        {
            try
            {
                ImageCollection ic = AppResource.LoadToolImages();
                barManager1.Images = ic;
                barButtonItem3.ImageIndex = (int)AppResource.EIcons.search;
                barButtonItem5.ImageIndex = (int)AppResource.EIcons.search;
                LoadData();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DoClickEvent(ConvertUtil.ToString(e.Item.Tag));
        }
        private void gridView1_RowDoubleClick(object sender, EventArgs e)
        {
            try
            {
                string skuCode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SKUCode").ToString();
                if (skuCode == string.Empty)
                {
                    return;
                }
                FrmSKULocation frmSKULocation = new FrmSKULocation(skuCode);
                frmSKULocation.ShowDialog();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
        #endregion

        #region Enums
        enum ESearchType
        {
            未排序订单 = 0,
            已排序订单 = 1,
        }
        #endregion


        private void check_CheckStateChanged(object sender, EventArgs e)
        {
            CheckEdit check = sender as CheckEdit;
            BillSKUNum a = gridView1.GetRow(gridView1.FocusedRowHandle) as BillSKUNum;
            a.HasChecked = (bool)check.Checked;
        }

        //private void gridView1_Click(object sender, EventArgs e)
        //{
        //    CheckOneGridColumn(gridView1, "HasChecked", MousePosition);
        //}
    }
}