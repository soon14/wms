using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Nodes.UI;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Resources;
using Nodes.Shares;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;
using Newtonsoft.Json;

namespace Nodes.Outstore
{
    public partial class FrmSOSort : DevExpress.XtraEditors.XtraForm
    {
        //private SODal soDal = null;
        //private SOQueryDal soQueryDal = null;
        private List<SOSummaryEntity> groupedBills = new List<SOSummaryEntity>();
        private List<SOSummaryEntity> ungroupedBills = null;

        public FrmSOSort()
        {
            InitializeComponent();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            //soDal = new SODal();
            //soQueryDal = new SOQueryDal();

            ImageCollection ic = AppResource.LoadToolImages();
            barManager1.Images = ic;
            barButtonItem1.ImageIndex = (int)AppResource.EIcons.refresh;
            barButtonItem2.ImageIndex = (int)AppResource.EIcons.save;
            barButtonItem3.ImageIndex = (int)AppResource.EIcons.report;

            Reload();
        }

        /// <summary>
        /// 查询未分组的订单
        /// </summary>
        /// <param name="billStates"></param>
        /// <returns></returns>
        public List<SOSummaryEntity> QueryBills(string billStates)
        {
            List<SOSummaryEntity> list = new List<SOSummaryEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billStates=").Append(billStates);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_QueryBillsSortMap);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonQueryBillsSortMap bill = JsonConvert.DeserializeObject<JsonQueryBillsSortMap>(jsonQuery);
                if (bill == null)
                {
                    //MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return list;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return list;
                }
                #endregion

                #region 赋值数据
                foreach (JsonQueryBillsSortMapResult jbr in bill.result)
                {
                    SOSummaryEntity asnEntity = new SOSummaryEntity();
                    #region 0-10
                    asnEntity.Address = jbr.address;
                    asnEntity.Amount = Convert.ToDecimal(jbr.amount);
                    asnEntity.BillID = Convert.ToInt32(jbr.billId);
                    asnEntity.BillNO = jbr.billNo;
                    asnEntity.BillState = jbr.billState;
                    asnEntity.BillStateName = jbr.itemDesc;
                    asnEntity.CustomerName = jbr.cName;
                    asnEntity.Distance = Convert.ToDecimal(jbr.distance);
                    asnEntity.FromWarehouse = jbr.fromWhCode;
                    asnEntity.OrderSort = Convert.ToInt32(jbr.sortOrder);
                    #endregion

                    #region 11-20
                    asnEntity.RouteCode = jbr.rtCode;
                    asnEntity.RouteName = jbr.rtName;
                    asnEntity.TotalCount = Convert.ToInt32(jbr.totalCount);
                    asnEntity.Volume = Convert.ToDecimal(jbr.volume);
                    asnEntity.WarehouseName = jbr.whName;
                    asnEntity.Xcoor = Convert.ToInt32(jbr.xCoor);
                    asnEntity.Ycoor = Convert.ToInt32(jbr.yCoor);
                    asnEntity.CustomerCode = jbr.cCode;
                    #endregion
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
            try
            {
                ungroupedBills = QueryBills(BaseCodeConstant.SO_WAIT_GROUP);
                bindingSource1.DataSource = ungroupedBills;
                gridControl1.RefreshDataSource();

                groupedBills.Clear();
                bindingSource2.DataSource = groupedBills;
                gridControl2.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private string FormatBillIDs()
        {
            if (gridView2.RowCount == 0)
                return null;

            List<SOSummaryEntity> items = bindingSource2.DataSource as List<SOSummaryEntity>;

            string billIDs = string.Empty;
            foreach (SOSummaryEntity header in items)
                billIDs += string.Format("{0},", header.BillID);

            //去除最后一个逗号
            return billIDs.TrimEnd(',');
        }

        #region 处理拖拽
        GridHitInfo downHitInfo = null;
        private void gridView1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
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

        List<SOSummaryEntity> GetDragData(GridView view)
        {
            int[] selection = view.GetSelectedRows();
            if (selection == null) return null;
            int count = selection.Length;

            List<SOSummaryEntity> result = new List<SOSummaryEntity>();
            for (int i = 0; i < count; i++)
                result.Add(view.GetRow(selection[i]) as SOSummaryEntity);
            return result;
        }

        private void gridView1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
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

        private int dragDirection = 0;
        private void gridControl2_DragDrop(object sender, DragEventArgs e)
        {
            if (dragDirection == -1)
                return;

            List<SOSummaryEntity> data = e.Data.GetData(typeof(List<SOSummaryEntity>)) as List<SOSummaryEntity>;
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

            List<SOSummaryEntity> data = e.Data.GetData(typeof(List<SOSummaryEntity>)) as List<SOSummaryEntity>;
            ungroupedBills.AddRange(data);
            data.ForEach(p => groupedBills.Remove(p));

            bindingSource1.DataSource = ungroupedBills;
            bindingSource2.DataSource = groupedBills;
            gridControl1.RefreshDataSource();
            gridControl2.RefreshDataSource();
            gridView1.FocusedRowHandle = gridView2.FocusedRowHandle = -1;
        }
        #endregion

        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string tag = ConvertUtil.ToString(e.Item.Tag);
            switch (tag)
            {
                case "重新加载":
                    Reload();
                    break;
                case "保存":
                    Save();
                    break;
                case "查看分布图":
                    break;
            }
        }

        #region 保存
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
        /// 存储排序记录
        /// </summary>
        /// <param name="list"></param>
        public bool Insert(List<OrderSortEntity> list)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                List<string> prop = new List<string>() { "BillNo", "Attri1", "VehicleNo", "InVehicleSort", "PiecesQty" };
                string pickPlanEntity = GetResList<OrderSortEntity>("jsonStr", list, prop);
                string jsons = "{" + pickPlanEntity + "," + pickPlanEntity + "}";
                loStr.Append("jsonStr=").Append(jsons);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_InsertSortBill);
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
        /// 存储排序记录
        /// </summary>
        /// <param name="billIDs"></param>
        /// <param name="userName"></param>
        /// <param name="errBillNO"></param>
        /// <returns></returns>
        public bool SaveSortOrders(string billIDs, string userName, out string errBillNO)
        {
            errBillNO = "";
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billIds=").Append(billIDs).Append("&");
                loStr.Append("userName=").Append(userName).Append("&");
                loStr.Append("warehouseType=").Append(EUtilStroreType.WarehouseTypeToInt(GlobeSettings.LoginedUser.WarehouseType)).Append("&");
                loStr.Append("uuid=").Append(Guid.NewGuid().ToString().Replace("-", ""));
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveSortOrders);
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
        #endregion

        private void Save()
        {
            string billIDs = FormatBillIDs();
            if (string.IsNullOrEmpty(billIDs))
            {
                MsgBox.Warn("请将确认后的排序单据拖拽至右边的表格内。");
                return;
            }

            if (MsgBox.AskOK("确定要保存吗？") != DialogResult.OK)
                return;

            try
            {
                // 获取已排序的订单存到 wm_order_sort 表中
                string soNO = DateTime.Now.ToString("SOyyyyMMddHHmmssms");
                List<OrderSortEntity> sortList = new List<OrderSortEntity>();
                for (int i = 0; i < this.gridView2.RowCount; i++)
                {
                    SOSummaryEntity data = this.gridView2.GetRow(i) as SOSummaryEntity;
                    if (data == null) continue;
                    sortList.Add(new OrderSortEntity()
                    {
                        VehicleNo = soNO,
                        BillNo = data.BillNO,
                        InVehicleSort = (i + 1),
                        PiecesQty = data.TotalCount,
                        Attri1 = 1
                    });
                }
                Insert(sortList);
                // 更改已排序的订单状态
                string errBillNO = string.Empty;
                bool result = SaveSortOrders(billIDs, GlobeSettings.LoginedUser.UserName, out errBillNO);
                if (result)
                {
                    Reload();
                    MsgBox.OK("保存成功。");
                }
                //else if (result == -1)
                //    MsgBox.Warn(string.Format("未找到单据“{0}”，已取消所有的保存。", errBillNO));
                //else if (result == -2)
                //    MsgBox.Warn(string.Format("单据“{0}”已经分组，保存已取消，您可以将单据“{0}”拖拽至左边的表格中，也可以“重新加载未分组的单据”。", errBillNO));
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
    }
}