using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.Entities;
using Nodes.Utils;
//using Nodes.DBHelper;
using Nodes.Shares;
using Nodes.UI;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Stock;
using Newtonsoft.Json;

namespace Nodes.Stock
{
    public partial class FrmStockTransfer : DevExpress.XtraEditors.XtraForm
    {
        List<StockTransEntity> groupedBills = new List<StockTransEntity>();
        List<StockTransEntity> ungroupedBills = null;
        //private StockTransferDal transferDal = null;

        public FrmStockTransfer()
        {
            InitializeComponent();

            //this.transferDal = new StockTransferDal();
        }

        private void OnQueryClick(object sender, EventArgs e)
        {
            Query();
        }

        /// <summary>
        /// 库存转移--查询
        /// </summary>
        /// <param name="location"></param>
        /// <param name="materialName"></param>
        /// <returns></returns>
        public List<StockTransEntity> QueryStockRemove(string location, string materialName)
        {
            List<StockTransEntity> list = new List<StockTransEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();

                loStr.Append("location=").Append(location).Append("&");
                loStr.Append("materialName=").Append(materialName);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_QueryStockRemove);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    //MsgBox.Warn(WebWork.RESULT_NULL);
                    LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonQueryStockRemove bill = JsonConvert.DeserializeObject<JsonQueryStockRemove>(jsonQuery);
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
                foreach (JsonQueryStockRemoveResult jbr in bill.result)
                {
                    StockTransEntity asnEntity = new StockTransEntity();
                    #region 0- 10
                    asnEntity.Location = jbr.lcCode;
                    asnEntity.OccupyQty = Convert.ToDecimal(jbr.occupyQty);
                    asnEntity.PickingQty = Convert.ToDecimal(jbr.pickingQty);
                    asnEntity.Qty = Convert.ToDecimal(jbr.qty);
                    asnEntity.StockID = Convert.ToInt32(jbr.stkId);
                    asnEntity.UnitName = jbr.umName;
                    asnEntity.ZoneName = jbr.znName;
                    asnEntity.Material = jbr.skuCode;
                    asnEntity.MaterialName = jbr.skuName;
                    #endregion

                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.expDate))
                            asnEntity.ExpDate = Convert.ToDateTime(jbr.expDate);
                    }
                    catch (Exception msg)
                    {
                        LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
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

        private void Query()
        {
            try
            {
                //StockDal stockDal = new StockDal();
                ungroupedBills = QueryStockRemove(
                    txtLocation.Text.Trim().Length == 0 ? null : txtLocation.Text.Trim().ToUpper(),
                    txtMC.Text.Trim().Length == 0 ? null : txtMC.Text.Trim().ToUpper());

                bindingSource1.DataSource = ungroupedBills;
                bindingSource2.DataSource = groupedBills;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        #region "处理拖拽"
        GridHitInfo downHitInfo = null;
        int dragDirection = 0;
        private void OnGridViewMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GridView view = sender as GridView;
            downHitInfo = null;

            GridHitInfo hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));
            if (Control.ModifierKeys != Keys.None) return;
            if (e.Button == MouseButtons.Left && hitInfo.InRow && hitInfo.HitTest != GridHitTest.RowIndicator)
                downHitInfo = hitInfo;

            //标记一下移动方向，防止同一个表格内落放，造成数据重复
            if (ConvertUtil.ToString(view.Tag) == "source")
                dragDirection = 1;
            else
                dragDirection = -1;
        }

        List<StockTransEntity> GetDragData(GridView view)
        {
            int[] selection = view.GetSelectedRows();
            if (selection == null) return null;
            int count = selection.Length;

            List<StockTransEntity> result = new List<StockTransEntity>();
            for (int i = 0; i < count; i++)
                result.Add(view.GetRow(selection[i]) as StockTransEntity);

            return result;
        }

        private void OnGridViewMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Button == MouseButtons.Left && downHitInfo != null)
            {
                Size dragSize = SystemInformation.DragSize;
                Rectangle dragRect = new Rectangle(new Point(downHitInfo.HitPoint.X - dragSize.Width / 2,
                    downHitInfo.HitPoint.Y - dragSize.Height / 2), dragSize);

                if (!dragRect.Contains(new Point(e.X, e.Y)))
                {
                    view.GridControl.DoDragDrop(GetDragData(view), DragDropEffects.All);
                    downHitInfo = null;
                }
            }
        }

        private void gridControl2_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void gridControl2_DragDrop(object sender, DragEventArgs e)
        {
            if (dragDirection == -1)
                return;

            List<StockTransEntity> data = e.Data.GetData(typeof(List<StockTransEntity>)) as List<StockTransEntity>;
            groupedBills.AddRange(data);

            data.ForEach(p => ungroupedBills.Remove(p));
            bindingSource1.DataSource = ungroupedBills;
            bindingSource2.DataSource = groupedBills;
            gridControl1.RefreshDataSource();
            gridControl2.RefreshDataSource();
            gridView1.FocusedRowHandle = gridView2.FocusedRowHandle = -1;
        }

        private void gridControl1_DragDrop(object sender, DragEventArgs e)
        {
            if (dragDirection == 1)
                return;

            List<StockTransEntity> data = e.Data.GetData(typeof(List<StockTransEntity>)) as List<StockTransEntity>;
            ungroupedBills.AddRange(data);

            data.ForEach(p => groupedBills.Remove(p));
            bindingSource1.DataSource = ungroupedBills;
            bindingSource2.DataSource = groupedBills;
            gridControl1.RefreshDataSource();
            gridControl2.RefreshDataSource();
            gridView1.FocusedRowHandle = gridView2.FocusedRowHandle = -1;
        }
        #endregion

        #region List转换成Json
        private string GetRes<T>(List<T> listobj, List<string> proptylist)
        {

            StringBuilder strb = new StringBuilder();
            List<string> result = new List<string>();
            string curname = default(string);
            foreach (var obj in listobj)
            {

                Type type = obj.GetType();

                curname = type.Name;


                List<string> curobjliststr = new List<string>();
                foreach (var curpropty in proptylist)
                {
                    string tmp = default(string);
                    var res01 = type.GetProperty(curpropty).GetValue(obj, null);
                    if (res01 == null)
                    {
                        tmp = null;
                    }
                    else
                    {
                        tmp = res01.ToString();
                    }
                    curobjliststr.Add("\"" + curpropty + "\"" + ":" + "\"" + tmp + "\"");
                }
                string curres = "{" + string.Join(",", curobjliststr.ToArray()) + "}";
                result.Add(curres);
            }
            strb.Append(":[" + string.Join(",", result.ToArray()) + "]");
            string ret = "\"" + curname + "\"" + strb.ToString();
            ret = ret.Insert(0, "{");
            ret = ret.Insert(ret.Length, "}");
            return ret;
        }


        private string GetResList<T>(List<T> listobj, List<string> proptylist)
        {

            StringBuilder strb = new StringBuilder();
            List<string> result = new List<string>();
            string curname = default(string);
            foreach (var obj in listobj)
            {

                Type type = obj.GetType();

                curname = type.Name;


                List<string> curobjliststr = new List<string>();
                foreach (var curpropty in proptylist)
                {
                    string tmp = default(string);
                    var res01 = type.GetProperty(curpropty).GetValue(obj, null);
                    if (res01 == null)
                    {
                        tmp = null;
                    }
                    else
                    {
                        tmp = res01.ToString();
                    }
                    curobjliststr.Add("\"" + curpropty + "\"" + ":" + "\"" + tmp + "\"");
                }
                string curres = "{" + string.Join(",", curobjliststr.ToArray()) + "}";
                result.Add(curres);
            }

            //strb.Append(":[" + string.Join(",", result.ToArray()) + "]");
            //string ret = "\""+ curname + "\"" + strb.ToString();
            //ret = ret.Insert(0, "{");
            //ret = ret.Insert(ret.Length, "}");
            return string.Join(",", result.ToArray());
        }

        private string GetResList<T>(string josnName, List<T> listobj, List<string> proptylist)
        {

            StringBuilder strb = new StringBuilder();
            List<string> result = new List<string>();
            string curname = default(string);
            foreach (var obj in listobj)
            {

                Type type = obj.GetType();

                curname = type.Name;


                List<string> curobjliststr = new List<string>();
                foreach (var curpropty in proptylist)
                {
                    string tmp = default(string);
                    var res01 = type.GetProperty(curpropty).GetValue(obj, null);
                    if (res01 == null)
                    {
                        tmp = null;
                    }
                    else
                    {
                        tmp = res01.ToString();
                    }
                    curobjliststr.Add("\"" + curpropty + "\"" + ":" + "\"" + tmp + "\"");
                }
                string curres = "{" + string.Join(",", curobjliststr.ToArray()) + "}";
                result.Add(curres);
            }

            strb.Append(":[" + string.Join(",", result.ToArray()) + "]");
            string ret = "\"" + josnName + "\"" + strb.ToString();
            //ret = ret.Insert(0, "{");
            //ret = ret.Insert(ret.Length, "}");
            return ret;
        }

        #endregion

        /// <summary>
        /// 库存转移--保存编辑的采购单
        /// </summary>
        /// <param name="billType"></param>
        /// <param name="remark"></param>
        /// <param name="whCode"></param>
        /// <param name="creator"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        public List<int> SaveBill(string billType, string remark, string whCode,
            string creator, List<StockTransEntity> details)
        {
            List<int> list = new List<int>();
            try
            {
                List<StockTransEntity> listCase1 = details.FindAll((item) => { return item.IsCase == 1; });
                List<StockTransEntity> listCase2 = details.FindAll((item) => { return item.IsCase != 1; });
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billType=").Append(billType).Append("&");
                loStr.Append("remark=").Append(remark).Append("&");
                loStr.Append("whCode=").Append(whCode).Append("&");
                loStr.Append("creator=").Append(creator).Append("&");
                loStr.Append("flagA=").Append(listCase1.Count).Append("&");
                loStr.Append("flagB=").Append(listCase2.Count).Append("&");
                List<string> prop = new List<string>() { "Material", "Location", "TargetLocation", "TransferQty" };
                string jsonDetailA = GetResList<StockTransEntity>("jsonDetailA", listCase1, prop);
                jsonDetailA = "{" + jsonDetailA + "}";
                loStr.Append("jsonDetailA=").Append(jsonDetailA).Append("&");
                string jsonDetailB = GetResList<StockTransEntity>("jsonDetailB", listCase2, prop);
                jsonDetailB = "{" + jsonDetailB + "}";
                loStr.Append("jsonDetailB=").Append(jsonDetailB);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveBill);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    //MsgBox.Warn(WebWork.RESULT_NULL);
                    LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonSaveBill bill = JsonConvert.DeserializeObject<JsonSaveBill>(jsonQuery);
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
                foreach (JsonSaveBillResult jbr in bill.result)
                {
                    list.Add(Convert.ToInt32(jbr.billId));
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
        /// 库存转移--分派任务
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="taskType"></param>
        /// <returns></returns>
        public bool Schedule(int billID, string taskType)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID).Append("&");
                loStr.Append("taskType=").Append(taskType);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_Schedule);
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

        private void OnbtnSaveClick(object sender, EventArgs e)
        {
            List<StockTransEntity> items = bindingSource2.DataSource as List<StockTransEntity>;

            if (items == null || items.Count == 0)
            {
                MsgBox.Warn("请先选择要进行移库的行。");
                return;
            }
            List<LocationEntity> localtionList = repositoryItemSearchLookUpEdit1.DataSource as List<LocationEntity>;
            //检查是否全部填写了
            foreach (StockTransEntity line in items)
            {
                LocationEntity locEntity = localtionList.Find(u => u.LocationCode == line.TargetLocation);
                if (locEntity != null && locEntity.IsActive == "N")
                {
                    MsgBox.Warn(string.Format("目标货位 {0} 被禁用，不允许执行移库操作。", locEntity.LocationCode));
                    return;
                }
                if (line.TransferQty <= 0)
                {
                    MsgBox.Warn(string.Format("请填写“移出数量”，对应的物料为“{0}”。", line.MaterialName));
                    return;
                }

                if (line.TransferQty > line.IdleQty)
                {
                    MsgBox.Warn(string.Format("“移出数量”不能大于“可移出量”，对应的物料为“{0}”。", line.MaterialName));
                    return;
                }

                if (string.IsNullOrEmpty(line.TargetLocation))
                {
                    MsgBox.Warn(string.Format("请填写“移至货位”，对应的物料为“{0}”。", line.MaterialName));
                    return;
                }
            }

            if (MsgBox.AskOK("确定要保存吗？") != DialogResult.OK)
                return;

            try
            {
                int billID = -1;
                List<int> billIdList = SaveBill(
                    BaseCodeConstant.BILL_TYPE_TRANS,
                    string.Empty,
                    GlobeSettings.LoginedUser.WarehouseCode,
                    GlobeSettings.LoginedUser.UserName,
                    items);

                //if (MsgBox.AskYes("保存成功，是否现在分派任务？") == DialogResult.Yes)
                //{
                if (billIdList.Count > 0)
                {
                    bool result = false;
                    foreach (int item in billIdList)
                        result = Schedule(item, "144");
                    if (result)
                    {
                        this.DialogResult = DialogResult.OK;
                        btnSave.Enabled = false;
                    }
                    //string result = string.Empty;
                    //foreach (int item in billIdList)
                    //    result = TaskDal.Schedule(item, "144");
                    //if (result == "Y")
                    //{
                    //    this.DialogResult = DialogResult.OK;
                    //    btnSave.Enabled = false;
                    //}
                    //else
                    //{
                    //    MsgBox.Warn(result);
                    //}
                }
                //}
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 库存转移---库存转移
        /// </summary>
        /// <returns></returns>
        public List<LocationEntity> GetAllLocation()
        {
            List<LocationEntity> list = new List<LocationEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billState=").Append(BillStateConst.ASN_STATE_CODE_COMPLETE).Append("&");
                //loStr.Append("wareHouseCode=").Append(warehouseCode);
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_GetAllLocation);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    //MsgBox.Warn(WebWork.RESULT_NULL);
                    LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetAllLocation bill = JsonConvert.DeserializeObject<JsonGetAllLocation>(jsonQuery);
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
                foreach (JsonGetAllLocationResult jbr in bill.result)
                {
                    LocationEntity asnEntity = new LocationEntity();
                    #region 0-10
                    asnEntity.CellCode = jbr.cellCode;
                    asnEntity.FloorCode = jbr.floorCode;
                    asnEntity.LocationCode = jbr.lcCode;
                    asnEntity.LocationName = jbr.lcName;
                    asnEntity.LowerSize = Convert.ToInt32(jbr.lowerSize);
                    asnEntity.IsActive = jbr.isActive;
                    asnEntity.PassageCode = jbr.passageCode;
                    asnEntity.ShelfCode = jbr.shelfCode;
                    asnEntity.SortOrder = Convert.ToInt32(jbr.shortOrder);
                    asnEntity.UpperSize = Convert.ToInt32(jbr.upperSize);
                    #endregion
                    asnEntity.WarehouseCode = jbr.whCode;
                    asnEntity.WarehouseName = jbr.whName;
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
                        LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
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

        private void OnFormLoad(object sender, EventArgs e)
        {
            try
            {
                repositoryItemSearchLookUpEdit1.DataSource = GetAllLocation();
                repositoryItemSearchLookUpEdit1View.ExpandAllGroups();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
    }
}