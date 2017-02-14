using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Nodes.Shares;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.UI;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Nodes.Stock;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Stock;
using Nodes.Entities.HttpEntity.Outstore;
using Newtonsoft.Json;

namespace Nodes.Outstore
{
    public partial class FrmSafeSup : DevExpress.XtraEditors.XtraForm
    {
        //private ReplenishDal replenishDal = new ReplenishDal();
        //private ReplenishDal repDal = new ReplenishDal();
        StockSafeSUPEntity StockSafeSUPEntity
        {
            get
            {
                if (gridView1.FocusedRowHandle < 0)

                    return null;
                else
                    return gridView1.GetFocusedRow() as StockSafeSUPEntity;
            }
        }

        public FrmSafeSup()
        {
            InitializeComponent();
        }

        private void FrmSafeSup_Load(object sender, EventArgs e)
        {
            Reload();
        }

        /// <summary>
        /// 当前订单量	查询安全库存
        /// </summary>
        /// <returns></returns>
        public List<StockSafeSUPEntity> GetNotSafeStock()
        {
            List<StockSafeSUPEntity> list = new List<StockSafeSUPEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("status=").Append(status).Append("&");
                //loStr.Append("setting=").Append(setting);
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_GetNotSafeStock);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetNotSafeStock bill = JsonConvert.DeserializeObject<JsonGetNotSafeStock>(jsonQuery);
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

                #region 赋值数据
                foreach (JsonGetNotSafeStockResult jbr in bill.result)
                {
                    StockSafeSUPEntity asnEntity = new StockSafeSUPEntity();
                    #region 0-9
                    asnEntity.DIFF_QTY = StringToDecimal.GetTwoDecimal(jbr.diffQty);
                    asnEntity.LC_CODE = jbr.lcCode;
                    asnEntity.PIKING_QTY = StringToDecimal.GetTwoDecimal(jbr.pickingQty);
                    asnEntity.QTY = StringToDecimal.GetTwoDecimal(jbr.qty);
                    asnEntity.SKU_CODE = jbr.skuCode;
                    asnEntity.SKU_NAME = jbr.skuName;
                    asnEntity.SPEC = jbr.spec;
                    asnEntity.UM_NAME = jbr.umName;
                    #endregion

                    #region
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.altestOut)) 
                            ;
                            //asnEntity. = Convert.ToDateTime(jbr.altestOut);

                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    }
                    #endregion

                    list.Add(asnEntity);
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

        private void Reload()
        {
            gridControl1.DataSource = GetNotSafeStock();
            LoadCheckBoxImage();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (ConvertUtil.ToString(e.Item.Tag))
            {
                case "刷新":
                    Reload();
                    break;
                case "生成补货计划":
                    //生成补货计划
                    CreatePlan();
                    break;
            }
        }

        #region
        private void LoadCheckBoxImage()
        {
            gridView1.Images = GridUtil.GetCheckBoxImages();
            columChecked.ImageIndex = 0;
        }

        private void gridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                CheckOneGridColumn(gridView1, "HasChecked", MousePosition);
            }
        }
        private void CheckOneGridColumn(GridView view, string checkedField, Point mousePosition)
        {
            Point p = view.GridControl.PointToClient(mousePosition);
            GridHitInfo hitInfo = view.CalcHitInfo(p);
            if (hitInfo.HitTest == GridHitTest.Column && hitInfo.Column.FieldName == checkedField)
            {
                List<StockSafeSUPEntity> _data = gridView1.DataSource as List<StockSafeSUPEntity>;
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

        private void CreatePlan()
        {

            List<StockSafeSUPEntity> stocksUnsafe = gridControl1.DataSource as List<StockSafeSUPEntity>;
            if (stocksUnsafe == null)
                return;

            DeleteTempReplenish();

            string gid = Guid.NewGuid().ToString().Replace("-", "");
            foreach (StockSafeSUPEntity a in stocksUnsafe)
            {
                //decimal tempQty = a.BillQty - a.TotalQty;
                if (a.HasChecked != false && a.QTY != 0)
                    InquiryBySku(a.SKU_CODE, a.QTY, gid, 1);
            }

            List<StockTransEntity> results = GetResultByGID(gid);
            FrmCreateReplenishBill frm = new FrmCreateReplenishBill(results, false);
            frm.ShowDialog();

            //刷新
            Reload();

            //try
            //{
            //    foreach (StockSafeSUPEntity item in stocksUnsafe)
            //    {
            //        replenishDal.CreateReplenishPlan(item.LC_CODE, item.SKU_CODE, GlobeSettings.LoginedUser.UserCode);
            //    }
            //    MsgBox.Warn("生成补货任务完成！");
            //}
            //catch (Exception ex)
            //{
            //    MsgBox.Warn(ex.Message);
            //}

        }
    }
}