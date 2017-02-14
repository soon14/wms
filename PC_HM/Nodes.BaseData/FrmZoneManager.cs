using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.Utils;
using Nodes.UI;
using Nodes.Utils;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Resources;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.BaseData;
using Newtonsoft.Json;

namespace Nodes.BaseData
{
    public partial class FrmZoneManager : DevExpress.XtraEditors.XtraForm
    {
        //private ZoneDal zoneDal = null;
        //private LocationDal LocationDal = null;
        public FrmZoneManager()
        {
            InitializeComponent();
        }

        private void FrmPartitionManager_Load(object sender, EventArgs e)
        {
            ImageCollection ic = AppResource.LoadToolImages();
            barManager1.Images = ic;
            toolAdd.ImageIndex = (int)AppResource.EIcons.add;
            toolEdit.ImageIndex = (int)AppResource.EIcons.edit;
            toolDel.ImageIndex = (int)AppResource.EIcons.delete;
            toolRefresh.ImageIndex = (int)AppResource.EIcons.refresh;
            toolSearch.ImageIndex = (int)AppResource.EIcons.search;

            //zoneDal = new ZoneDal();
            //LocationDal = new LocationDal();
            LoadDataAndBindGrid();
        }

        ///<summary>
        ///查询所有货区
        ///</summary>
        ///<returns></returns>
        public List<ZoneEntity> GetAllZone()
        {
            List<ZoneEntity> list = new List<ZoneEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("vhNo=").Append(vehicleNO);
                string jsons = string.Empty;
                string jsonQuery = WebWork.SendRequest(jsons, WebWork.URL_GetAllZone);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetAllZone bill = JsonConvert.DeserializeObject<JsonGetAllZone>(jsonQuery);
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
                foreach (JsonGetAllZoneResult jbr in bill.result)
                {
                    ZoneEntity asnEntity = new ZoneEntity();
                    asnEntity.IsActive = jbr.isActive;
                    asnEntity.TemperatureCode = jbr.tempCode;
                    asnEntity.TemperatureName = jbr.tempName;
                    asnEntity.WarehouseCode = jbr.whCode;
                    asnEntity.WarehouseName = jbr.whName;
                    asnEntity.ZoneCode = jbr.znCode;
                    asnEntity.ZoneName = jbr.znName;
                    asnEntity.ZoneTypeCode = jbr.ztCode;
                    asnEntity.ZoneTypeName = jbr.ztName;
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
                bindingSource1.DataSource = GetAllZone();
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
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
                    DoCreatePartition();
                    break;
                case "修改":
                    ShowEditZone();
                    break;
                case "删除":
                    DoDeleteSelectedZone();
                    break;
                case "快速查找":
                    if (gridView1.IsFindPanelVisible)
                        gridView1.HideFindPanel();
                    else
                        gridView1.ShowFindPanel();
                    break;
            }
        }

        /// <summary>
        /// 获得选中数据
        /// </summary>
        ZoneEntity SelectedZoneRow
        {
            get
            {
                if (gridView1.FocusedRowHandle < 0) return null;

                return gridView1.GetRow(gridView1.FocusedRowHandle) as ZoneEntity;
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
        private void DoCreatePartition()
        {
            FrmZoneEdit frmPartitionEdit = new FrmZoneEdit();
            frmPartitionEdit.DataSourceChanged += OnCreateChanage;
            frmPartitionEdit.ShowDialog();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        private void ShowEditZone()
        {
            ZoneEntity editEntity = SelectedZoneRow;
            if (editEntity == null)
            {
                MsgBox.Warn("没有要修改的数据。");
                return;
            }

            FrmZoneEdit frmPartitionEdit = new FrmZoneEdit(editEntity);
            frmPartitionEdit.DataSourceChanged += OnEditChanage;
            frmPartitionEdit.ShowDialog();
        }

        private void OnCreateChanage(object sender, EventArgs e)
        {
            ZoneEntity newEntity = (ZoneEntity)sender;
            bindingSource1.Add(newEntity);
            bindingSource1.ResetBindings(false);
        }

        private void OnEditChanage(object sender, EventArgs e)
        {
            bindingSource1.ResetBindings(false);
        }

        /// <summary>
        /// 删除货区
        /// </summary>
        /// <param name="StockZoneCode"></param>
        /// <returns></returns>
        public bool DeleteZone(string zoneCode)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("znCode=").Append(zoneCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_DeleteZone);
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
        private void DoDeleteSelectedZone()
        {
            ZoneEntity removeEntity = SelectedZoneRow;
            if (removeEntity == null)
            {
                MsgBox.Warn("没有要删除的数据。");
                return;
            }

            if (MsgBox.AskOK(string.Format("确定要删除货区{0}吗？", removeEntity.ZoneCode)) == DialogResult.OK)
            {
                bool ret = DeleteZone(removeEntity.ZoneCode);
                if (ret)
                {
                    bindingSource1.Remove(removeEntity);
                }
                else 
                    MsgBox.Warn("不能删除，该货区有相关联的货位。");
            }
        }

        private void gridView1_RowDoubleClick(object sender, EventArgs e)
        {
            ShowEditZone();
        }

        ///<summary>
        ///根据所选货区查询所有货位
        ///</summary>
        ///<returns></returns>
        public List<LocationEntity> GetAllLocationByZone(string zoneCode)
        {
            List<LocationEntity> list = new List<LocationEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("znCode=").Append(zoneCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetAllLocationByZone);
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

        private void ListLocation()
        {
            if (SelectedZoneRow != null)
            {
                List<LocationEntity> locationLists = GetAllLocationByZone(SelectedZoneRow.ZoneCode);
                if (locationLists != null)
                    gridControl2.DataSource = locationLists;
            }
            else
                gridControl2.DataSource = null;
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ListLocation();
        }
    }
}