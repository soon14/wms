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
    public partial class FrmWarehouseManager : DevExpress.XtraEditors.XtraForm
    {
        //private WarehouseDal WarehouseDal = null;
        //private ZoneDal zoneDal = null;
        public FrmWarehouseManager()
        {
            InitializeComponent();
        }

        private void FrmWarehouseManager_Load(object sender, EventArgs e)
        {
            ImageCollection ic = AppResource.LoadToolImages();
            barManager1.Images = ic;
            toolAdd.ImageIndex = (int)AppResource.EIcons.add;
            toolEdit.ImageIndex = (int)AppResource.EIcons.edit;
            toolDel.ImageIndex = (int)AppResource.EIcons.delete;
            toolRefresh.ImageIndex = (int)AppResource.EIcons.refresh;
            toolSearch.ImageIndex = (int)AppResource.EIcons.search;

            LoadDataAndBindGrid();
        }

        ///<summary>
        ///查询所有仓库
        ///</summary>
        ///<returns></returns>
        public List<WarehouseEntity> GetAllWarehouse()
        {
            List<WarehouseEntity> list = new List<WarehouseEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("vhNo=").Append(vehicleNO);
                string jsons = string.Empty;
                string jsonQuery = WebWork.SendRequest(jsons, WebWork.URL_GetAllWarehouse);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetAllWarehouse bill = JsonConvert.DeserializeObject<JsonGetAllWarehouse>(jsonQuery);
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
                foreach (JsonGetAllWarehouseResult jbr in bill.result)
                {
                    WarehouseEntity asnEntity = new WarehouseEntity();
                    asnEntity.OrgCode = jbr.orgCode;
                    asnEntity.OrgName = jbr.orgName;
                    asnEntity.WarehouseCode = jbr.whCode;
                    asnEntity.WarehouseName = jbr.whName;
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
                //WarehouseDal = new WarehouseDal();
                //zoneDal = new ZoneDal();
                List<WarehouseEntity> WarehouseEntities = GetAllWarehouse();
                BindGrid(WarehouseEntities);
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
                    DoCreateWarehouse();
                    break;
                case "修改":
                    ShowEditWarehouse();
                    break;
                case "删除":
                    DoDeleteSelectedWarehouse();
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
        WarehouseEntity SelectedWarehouseRow
        {
            get
            {
                if (gridView1.FocusedRowHandle < 0) return null;

                return gridView1.GetRow(gridView1.FocusedRowHandle) as WarehouseEntity;
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
        private void DoCreateWarehouse()
        {
            FrmWarehouseEdit frmWarehouseEdit = new FrmWarehouseEdit();
            frmWarehouseEdit.DataSourceChanged += OnCreateChanage;
            frmWarehouseEdit.ShowDialog();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        private void ShowEditWarehouse()
        {
            WarehouseEntity editEntity = SelectedWarehouseRow;
            if (editEntity == null)
            {
                MsgBox.Warn("没有要修改的数据。");
                return;
            }

            FrmWarehouseEdit frmWarehouseEdit = new FrmWarehouseEdit(editEntity);
            frmWarehouseEdit.DataSourceChanged += OnEditChanage;
            frmWarehouseEdit.ShowDialog();
        }


        private void OnCreateChanage(object sender, EventArgs e)
        {
            WarehouseEntity newEntity = (WarehouseEntity)sender;
            bindingSource1.Add(newEntity);
            bindingSource1.ResetBindings(false);
        }

        private void OnEditChanage(object sender, EventArgs e)
        {
            bindingSource1.ResetBindings(false);
        }


        /// <summary>
        /// 删除
        /// </summary>
        private void DoDeleteSelectedWarehouse()
        {
            WarehouseEntity removeEntity = SelectedWarehouseRow;
            if (removeEntity == null)
            {
                MsgBox.Warn("没有要删除的数据。");
                return;
            }

            if (MsgBox.AskOK(string.Format("确定要删除仓库{0}吗？", removeEntity.WarehouseCode)) == DialogResult.OK)
            {
                bool ret = DeleteWarehouse(removeEntity);
                if (ret)
                {
                    ReLoad();
                }
                else
                    MsgBox.Warn("不能删除，该仓库有相关联的货区。");
            }
        }

        public bool DeleteWarehouse(WarehouseEntity removeEntity)
        {
            bool result = false;
            try
            {
                //result = WarehouseDal.DeleteWarehouse(removeEntity.WarehouseCode);
                if (result)
                    RemoveRowFromGrid(removeEntity);
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
            }
            return result;
        }
        private void gridView1_RowDoubleClick(object sender, EventArgs e)
        {
            ShowEditWarehouse();
        }

        #region IvWarehouse 成员

        public void BindGrid(List<WarehouseEntity> objs)
        {
            bindingSource1.DataSource = objs;
        }

        public void RemoveRowFromGrid(WarehouseEntity obj)
        {
            bindingSource1.Remove(obj);
        }

        #endregion

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            BindZone();
        }

        ///<summary>
        ///根据仓库查询所有货区和货位
        ///</summary>
        ///<returns></returns>
        public List<ZoneEntity> GetZoneByWarehouseCode(string warehouseCode)
        {
            List<ZoneEntity> list = new List<ZoneEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("whCode=").Append(warehouseCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetZoneByWarehouseCode);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetZoneByWarehouseCode bill = JsonConvert.DeserializeObject<JsonGetZoneByWarehouseCode>(jsonQuery);
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
                foreach (JsonGetZoneByWarehouseCodeResult jbr in bill.result)
                {
                    ZoneEntity asnEntity = new ZoneEntity();
                    asnEntity.ZoneCode = jbr.znCode;
                    asnEntity.ZoneName = jbr.znName;
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

        private void BindZone()
        {
            if (SelectedWarehouseRow != null)
                gridControl2.DataSource = GetZoneByWarehouseCode(SelectedWarehouseRow.WarehouseCode);
        }
    }
}