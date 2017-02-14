using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.Utils;
using Nodes.UI;
using Nodes.Utils;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Resources;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.BaseData;
using Newtonsoft.Json;

namespace Nodes.BaseData
{
    public partial class FrmRouteManager : DevExpress.XtraEditors.XtraForm
    {
        private RouteDal routeDal = null;
        public FrmRouteManager()
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

            routeDal = new RouteDal();
            LoadDataAndBindGrid();
        }

        ///<summary>
        ///查询所有路线
        ///</summary>
        ///<returns></returns>
        public List<RouteEntity> GetAll()
        {
            List<RouteEntity> list = new List<RouteEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("groupCode=").Append(groupCode);
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_GetAllRoute);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetAllRoute bill = JsonConvert.DeserializeObject<JsonGetAllRoute>(jsonQuery);
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
                foreach (JsonGetAllRouteResult jbr in bill.result)
                {
                    RouteEntity asnEntity = new RouteEntity();
                    asnEntity.RouteCode = jbr.rtCode;
                    asnEntity.RouteName = jbr.rtName;
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
                bindingSource1.DataSource = GetAll();
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
                    DoCreateUnit();
                    break;
                case "修改":
                    ShowEditUnit();
                    break;
                case "删除":
                    DoDeleteSelectedUnit();
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
        RouteEntity SelectedUnitRow
        {
            get
            {
                if (gridView1.FocusedRowHandle < 0)
                    return null;

                return gridView1.GetFocusedRow() as RouteEntity;
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
        private void DoCreateUnit()
        {
            FrmRouteEdit frmunitEdit = new FrmRouteEdit();
            frmunitEdit.DataSourceChanged += OnCreateChanage;
            frmunitEdit.ShowDialog();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        private void ShowEditUnit()
        {
            RouteEntity editEntity = SelectedUnitRow;
            if (editEntity == null)
            {
                MsgBox.Warn("没有要修改的数据。");
                return;
            }

            FrmRouteEdit frmunitEdit = new FrmRouteEdit(editEntity);
            frmunitEdit.DataSourceChanged += OnEditChanage;
            frmunitEdit.ShowDialog();
        }

        private void OnCreateChanage(object sender, EventArgs e)
        {
            RouteEntity newEntity = (RouteEntity)sender;
            bindingSource1.Add(newEntity);
            bindingSource1.ResetBindings(false);
        }

        private void OnEditChanage(object sender, EventArgs e)
        {
            bindingSource1.ResetBindings(false);
        }

        /// <summary>
        /// 删除计量单位
        /// </summary>
        /// <param name="RouteCode"></param>
        /// <returns></returns>
        public bool DeleteUnit(string RouteCode)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("rtCode=").Append(RouteCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_DeleteUnit);
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
        private void DoDeleteSelectedUnit()
        {
            RouteEntity removeEntity = SelectedUnitRow;
            if (removeEntity == null)
            {
                MsgBox.Warn("没有要删除的数据。");
                return;
            }

            if (MsgBox.AskOK(string.Format("确定要删除路线“({0}){1}”吗？", removeEntity.RouteCode, removeEntity.RouteName)) == DialogResult.OK)
            {
                bool ret = DeleteUnit(removeEntity.RouteCode);
                if (ret)
                {
                    bindingSource1.Remove(removeEntity);
                }
                else
                    MsgBox.Warn("删除失败。");
            }
        }

        private void gridView1_RowDoubleClick(object sender, EventArgs e)
        {
            ShowEditUnit();
        }
    }
}