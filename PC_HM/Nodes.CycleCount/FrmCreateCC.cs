using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Nodes.Shares;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.UI;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Stock;
using Nodes.Entities.HttpEntity.CycleCount;
using Newtonsoft.Json;

namespace Nodes.CycleCount
{
    public partial class FrmCreateCC : DevExpress.XtraEditors.XtraForm
    {
        #region 变量


        #endregion

        #region 构造函数

        public FrmCreateCC()
        {
            InitializeComponent();
        }

        #endregion

        private void OnFormLoad(object sender, EventArgs e)
        {
            //列出所有货位
            LoadLocations();
        }

        /// <summary>
        /// 创建盘点单--获取存储区货位
        /// </summary>
        /// <returns></returns>
        public List<LocationEntity> GetStockLocation()
        {
            List<LocationEntity> list = new List<LocationEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billState=").Append(BillStateConst.ASN_STATE_CODE_COMPLETE).Append("&");
                //loStr.Append("wareHouseCode=").Append(warehouseCode);
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_GetStockLocation);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetStockLocation bill = JsonConvert.DeserializeObject<JsonGetStockLocation>(jsonQuery);
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
                foreach (JsonGetStockLocationResult jbr in bill.result)
                {
                    LocationEntity asnEntity = new LocationEntity();
                    asnEntity.BillID = Convert.ToInt32(jbr.billId);
                    asnEntity.BillState = jbr.billState;
                    asnEntity.LocationCode = jbr.lcCode;
                    asnEntity.LocationName = jbr.lcName;
                    asnEntity.ZoneCode = jbr.znCode;
                    asnEntity.ZoneName = jbr.znName;
                    try
                    {
                        //if (!string.IsNullOrEmpty(jbr.closeDate))
                        //    asnEntity.CloseDate = Convert.ToDateTime(jbr.closeDate);
                        //if (!string.IsNullOrEmpty(jbr.printedTime))
                        //    asnEntity.PrintedTime = Convert.ToDateTime(jbr.printedTime);
                        //if (!string.IsNullOrEmpty(jbr.createDate))
                        //    asnEntity.CreateDate = Convert.ToDateTime(jbr.createDate);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    }
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

        private void LoadLocations()
        {
            try
            {
                bindingSource1.DataSource = GetStockLocation();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private List<LocationEntity> GetCheckedLocations()
        {
            List<LocationEntity> locations = new List<LocationEntity>();

            int[] rowIndexs = gridView1.GetSelectedRows();
            foreach (int i in rowIndexs)
            {
                if (gridView1.IsDataRow(i))
                {
                    LocationEntity loc = gridView1.GetRow(i) as LocationEntity;
                    locations.Add(loc);
                }
            }

            return locations;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<LocationEntity> locations = GetCheckedLocations();
            if (locations == null || locations.Count == 0)
            {
                MsgBox.Warn("请选中要盘点的货位。");
                return;
            }
            // 判断当前选择的货位中是否存在进行中的货位
            if (locations.Exists(u => u.BillState != null))
            {
                MsgBox.Warn("<已创建盘点单> <正在盘点> <等待差异调整> 的货位不允许再建盘点单!");
                return;
            }
            using (FrmLocationConfirm frmConfirm = new FrmLocationConfirm(locations, txtRemark.Text.Trim()))
            {
                if (frmConfirm.ShowDialog() == DialogResult.OK)
                {
                    this.LoadLocations();
                }
            }
        }

        #region 选货位
        private void OnCheckAllClick(object sender, EventArgs e)
        {
            LoadLocations();
            gridView1.SelectAll();
        }
        #endregion

        /// <summary>
        /// 创建盘点单--列出今天库存发生变动的货位
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public List<LocationEntity> ListChangedLocations(DateTime fromDate, DateTime toDate)
        {
            List<LocationEntity> list = new List<LocationEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("fromDate=").Append(fromDate).Append("&");
                loStr.Append("toDate=").Append(toDate);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_ListChangedLocations);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonListChangedLocations bill = JsonConvert.DeserializeObject<JsonListChangedLocations>(jsonQuery);
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
                foreach (JsonListChangedLocationsResult jbr in bill.result)
                {
                    LocationEntity asnEntity = new LocationEntity();
                    asnEntity.BillID = Convert.ToInt32(jbr.billId);
                    asnEntity.BillState = jbr.billState;
                    asnEntity.LocationCode = jbr.lcCode;
                    asnEntity.ZoneName = jbr.znName;
                    try
                    {
                        //if (!string.IsNullOrEmpty(jbr.closeDate))
                        //    asnEntity.CloseDate = Convert.ToDateTime(jbr.closeDate);
                        //if (!string.IsNullOrEmpty(jbr.printedTime))
                        //    asnEntity.PrintedTime = Convert.ToDateTime(jbr.printedTime);
                        //if (!string.IsNullOrEmpty(jbr.createDate))
                        //    asnEntity.CreateDate = Convert.ToDateTime(jbr.createDate);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    }
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            FrmSelectDate frmSelectDate = new FrmSelectDate();
            if (frmSelectDate.ShowDialog() == DialogResult.OK)
            {
                bindingSource1.DataSource = ListChangedLocations(frmSelectDate.DateFrom, frmSelectDate.DateTo);
                gridView1.SelectAll();
            }
        }

        /// <summary>
        /// 实时库存查询
        /// </summary>
        /// <param name="location"></param>
        /// <param name="materialName"></param>
        /// <param name="withZeroQty"></param>
        /// <returns></returns>
        public List<StockRecordEntity> QueryStock(string location, string materialName, bool withZeroQty)
        {
            List<StockRecordEntity> list = new List<StockRecordEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("location=").Append(location).Append("&");
                loStr.Append("materialName=").Append(materialName).Append("&");
                loStr.Append("withZeroQty=").Append(withZeroQty).Append("&");
                loStr.Append("startPage=").Append("&");
                loStr.Append("pageSize=").Append("&");
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_QueryStock, 30000);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonQueryStock bill = JsonConvert.DeserializeObject<JsonQueryStock>(jsonQuery);
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
                foreach (JsonQueryStockResult jbr in bill.result)
                {
                    StockRecordEntity asnEntity = new StockRecordEntity();
                    #region 0- 10
                    asnEntity.ExpDays = Convert.ToInt32(jbr.expDays);
                    asnEntity.Location = jbr.lcCode;
                    asnEntity.LocationIsActive = jbr.active;
                    asnEntity.Material = jbr.skuCode;
                    asnEntity.MaterialName = jbr.skuName;
                    asnEntity.OccupyQty = Convert.ToDecimal(jbr.occupyQty);
                    asnEntity.PickingQty = Convert.ToDecimal(jbr.pickingQty);
                    asnEntity.Qty = Convert.ToDecimal(jbr.qty);
                    asnEntity.SkuQuality = jbr.skuQuality;
                    asnEntity.Spec = jbr.spec;
                    #endregion
                    asnEntity.StockID = Convert.ToInt32(jbr.id);
                    asnEntity.UnitName = jbr.umName;
                    asnEntity.ZoneName = jbr.znName;

                    #region
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.createDate))
                            asnEntity.CreateDate = Convert.ToDateTime(jbr.createDate);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    }
                    #endregion

                    #region
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.expDate))
                            asnEntity.ExpDate = Convert.ToDateTime(jbr.expDate);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    }
                    #endregion

                    #region
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.latestIn))
                            asnEntity.LastInDate = Convert.ToDateTime(jbr.latestIn);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    }
                    #endregion

                    #region
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.latestOut))
                            asnEntity.LastOutDate = Convert.ToDateTime(jbr.latestOut);
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

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                LocationEntity loc = gridView1.GetRow(e.FocusedRowHandle) as LocationEntity;
                gridControl2.DataSource = QueryStock(loc.LocationCode, null, false);
                gridView2.ViewCaption = string.Format("货位: {0}", loc.LocationCode);

            }
            else
            {
                gridControl2.DataSource = null;
                gridView2.ViewCaption = "选中货位可以查看库存";
            }
        }
    }
}