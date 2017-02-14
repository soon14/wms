using System;
using System.Collections.Generic;
using System.Windows.Forms;
//using Nodes.DBHelper;
using DevExpress.Utils;
using Nodes.Entities;
using Nodes.Resources;
using Nodes.Utils;
using Nodes.UI;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.BaseData;
using Newtonsoft.Json;

namespace Nodes.BaseData
{
    public partial class FrmLocationManager : DevExpress.XtraEditors.XtraForm
    {
        //private LocationDal locationDal = null;
        public FrmLocationManager()
        {
            InitializeComponent();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            ImageCollection ic = AppResource.LoadToolImages();
            barManager1.Images = ic;
            toolAdd.ImageIndex = (int)AppResource.EIcons.add;
            toolEdit.ImageIndex = (int)AppResource.EIcons.edit;
            toolDel.ImageIndex = (int)AppResource.EIcons.delete;
            toolRefresh.ImageIndex = (int)AppResource.EIcons.refresh;
            toolSearch.ImageIndex = (int)AppResource.EIcons.search;
            toolPrint.ImageIndex = (int)AppResource.EIcons.print;
            toolDesign.ImageIndex = (int)AppResource.EIcons.tree;

            //locationDal = new LocationDal();
            LoadDataAndBindGrid();
        }

        ///<summary>
        ///查询所有货位
        ///</summary>
        ///<returns></returns>
        public List<LocationEntity> GetAllLocation()
        {
            List<LocationEntity> list = new List<LocationEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("roleId=").Append(roleId);
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_GetAllLocationZJQ);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetAllLocationByZone bill = JsonConvert.DeserializeObject<JsonGetAllLocationByZone>(jsonQuery);
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
                foreach (JsonGetAllLocationByZoneResult jbr in bill.result)
                {
                    LocationEntity asnEntity = new LocationEntity();
                    #region 0-10
                    asnEntity.CellCode = jbr.cellCode;
                    asnEntity.FloorCode = jbr.floorCode;
                    asnEntity.IsActive = jbr.isActive;
                    asnEntity.LocationCode = jbr.lcCode;
                    asnEntity.LocationName = jbr.lcName;
                    asnEntity.LowerSize = Convert.ToInt32(jbr.lowerSize);
                    asnEntity.PassageCode = jbr.passageCode;
                    asnEntity.ShelfCode = jbr.shelfCode;
                    asnEntity.SortOrder = Convert.ToInt32(jbr.sortOrder);
                    asnEntity.UpperSize = Convert.ToInt32(jbr.upperSize);
                    #endregion

                    #region 11-14
                    asnEntity.WarehouseCode = jbr.whCode;
                    asnEntity.WarehouseName = jbr.whName;
                    asnEntity.ZoneCode = jbr.znCode;
                    asnEntity.ZoneName = jbr.znName;
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

        public void LoadDataAndBindGrid()
        {
            try
            {
                bindingSource1.DataSource = GetAllLocation();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (e.Item.Tag.ToString())
            {
                case "刷新":
                    ReLoad();
                    break;
                case "新增":
                    DoCreateLocation();
                    break;
                case "修改":
                    ShowEditLocation();
                    break;
                case "删除":
                    DoDeleteSelectedLocation();
                    break;
                case "打印":
                    DoPrint();
                    break;
                case "设计":
                    RibbonReportDesigner.MainForm designForm = new RibbonReportDesigner.MainForm();
                    RepLocation rep = new RepLocation();
                    try
                    {
                        designForm.OpenReport(rep, rep.RepFileName);
                        designForm.ShowDialog();
                        designForm.Dispose();
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Err(ex.Message);
                    }
                    break;
                case "快速查找":
                    if (gridView1.IsFindPanelVisible)
                        gridView1.HideFindPanel();
                    else
                        gridView1.ShowFindPanel();
                    break;
            }
        }

        private void DoPrint()
        {
            int[] selectedIndex = gridView1.GetSelectedRows();
            if (selectedIndex.Length == 0)
            {
                MsgBox.Warn("请选中要打印的行。");
                return;
            }

            if (MsgBox.AskOK("确定开始打印吗？") == DialogResult.OK)
            {
                List<LocationEntity> locations = new List<LocationEntity>();
                foreach (int i in selectedIndex)
                {
                    if (i >= 0)
                        locations.Add(gridView1.GetRow(i) as LocationEntity);
                }
                RepLocation repLocation = new RepLocation(locations, 1);
                repLocation.Print();
            }
        }

        /// <summary>
        /// 获得选中数据
        /// </summary>
        LocationEntity SelectedLocationRow
        {
            get
            {
                if (gridView1.FocusedRowHandle < 0) return null;

                return gridView1.GetRow(gridView1.FocusedRowHandle) as LocationEntity;
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        private void ReLoad()
        {
            LoadDataAndBindGrid();
        }

        /// <summary>
        /// 新增
        /// </summary>
        private void DoCreateLocation()
        {
            FrmLocationEdit frmLocationEdit = new FrmLocationEdit();
            frmLocationEdit.DataSourceChanged += OnCreateChanage;
            frmLocationEdit.ShowDialog();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        private void ShowEditLocation()
        {
            LocationEntity editEntity = SelectedLocationRow;
            if (editEntity == null)
            {
                MsgBox.Warn("没有要修改的数据。");
                return;
            }

            FrmLocationEdit frmLocationEdit = new FrmLocationEdit(editEntity);
            frmLocationEdit.DataSourceChanged += OnEditChanage;
            frmLocationEdit.ShowDialog();
        }


        private void OnCreateChanage(object sender, EventArgs e)
        {
            LocationEntity newEntity = (LocationEntity)sender;
            bindingSource1.Add(newEntity);
            bindingSource1.ResetBindings(false);
        }

        private void OnEditChanage(object sender, EventArgs e)
        {
            bindingSource1.ResetBindings(false);
        }

        /// <summary>
        /// 删除货位
        /// </summary>
        /// <param name="StockLocationCode"></param>
        /// <returns></returns>
        public bool DeleteLocation(string LocationCode)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("lcCode=").Append(LocationCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_DeleteLocation);
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
        /// 删除
        /// </summary>
        private void DoDeleteSelectedLocation()
        {
            LocationEntity removeEntity = SelectedLocationRow;
            if (removeEntity == null)
            {
                MsgBox.Warn("没有要删除的数据。");
                return;
            }

            if (MsgBox.AskOK(string.Format("确定要删除货位{0}吗？", removeEntity.LocationCode)) == DialogResult.OK)
            {
                //long stockQty = locationDal.JudgeStock(removeEntity.LocationCode);
                //if (stockQty > 0)
                //{
                //    MsgBox.Warn("该货位上有商品，不能删除");
                //    return;
                //}

                bool ret = DeleteLocation(removeEntity.LocationCode);
                if (ret)
                {
                    bindingSource1.Remove(removeEntity);
                }
                else 
                    MsgBox.Warn("删除失败，该货位有库存，只有未使用的货位才能删除。");
            }
        }

        private void gridView1_RowDoubleClick(object sender, EventArgs e)
        {
            ShowEditLocation();
        }
    }
}