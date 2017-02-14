using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.Utils;
using Nodes.UI;
using Nodes.Utils;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Resources;
using Nodes.DBHelper.BaseData;
using Nodes.Entities.BaseData;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.BaseData;
using Newtonsoft.Json;

namespace Nodes.BaseData
{
    public partial class FrmReclocationManager : DevExpress.XtraEditors.XtraForm
    {
        private RecLocationDal recLocationDal = null;

        public FrmReclocationManager()
        {
            InitializeComponent();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            ImageCollection ic = AppResource.LoadToolImages();
            barManager1.Images = ic;
            
            toolRefresh.ImageIndex = (int)AppResource.EIcons.refresh;
            toolSearch.ImageIndex = (int)AppResource.EIcons.search;
            toolModify.ImageIndex = (int)AppResource.EIcons.edit;
            toolClear.ImageIndex = (int)AppResource.EIcons.delete;

            recLocationDal = new RecLocationDal();
            LoadDataAndBindGrid();
        }

        /// <summary>
        /// 基础管理（推荐货位-查询所有推荐货位）
        /// </summary>
        /// <returns></returns>
        public List<RecLocationEntity> GetAllRecLocation()
        {
            List<RecLocationEntity> list = new List<RecLocationEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("roleId=").Append(roleId);
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_GetAllRecLocation);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetAllRecLocation bill = JsonConvert.DeserializeObject<JsonGetAllRecLocation>(jsonQuery);
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
                foreach (JsonGetAllRecLocationResult jbr in bill.result)
                {
                    RecLocationEntity asnEntity = new RecLocationEntity();
                    asnEntity.Location = jbr.lcCode;
                    asnEntity.RecLoc = jbr.recLoc;
                    asnEntity.SkuCode = jbr.skuCode;
                    asnEntity.SkuName = jbr.skuName;
                    asnEntity.Spec = jbr.spec;
                    asnEntity.ZnName = jbr.znName;
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
            this.bindingSource1.DataSource = GetAllRecLocation();
        }

        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (e.Item.Tag.ToString())
            {
                case "刷新":
                    ReLoad();
                    break;
                case "修改":
                    ShowEditLocation();
                    break;
                case "清空":
                    Delete();
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
        /// 刷新
        /// </summary>
        private void ReLoad()
        {
            LoadDataAndBindGrid();
        }

        /// <summary>
        /// 获得选中数据
        /// </summary>
        RecLocationEntity SelectedLocationRow
        {
            get
            {
                if (gridView1.FocusedRowHandle < 0) return null;

                return gridView1.GetRow(gridView1.FocusedRowHandle) as RecLocationEntity;
            }
        }

        private void ShowEditLocation()
        {
            RecLocationEntity editEntity = SelectedLocationRow;
            if (editEntity == null)
            {
                MsgBox.Warn("没有要修改的数据。");
                return;
            }

            FrmRecLocationEdit frmRecLocationEdit = new FrmRecLocationEdit(editEntity);
            frmRecLocationEdit.DataSourceChanged += OnEditChanage;
            frmRecLocationEdit.ShowDialog();
        }

        private void OnEditChanage(object sender, EventArgs e)
        {
            bindingSource1.ResetBindings(false);
        }

        private void gridView1_RowDoubleClick(object sender, EventArgs e)
        {
            ShowEditLocation();
        }

        public bool DeleteRecLoc(string recLoc)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("lcCode=").Append(recLoc);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_DeleteRecLoc);
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

        public void Delete()
        {
            RecLocationEntity removeEntity = SelectedLocationRow;
            if (removeEntity == null)
            {
                MsgBox.Warn("没有要清空的数据。");
                return;
            }
            if (MsgBox.AskOK(string.Format("确定要清空货位\" {0} \"的推荐商品\" {1} \"吗？", removeEntity.RecLoc, removeEntity.SkuName)) == DialogResult.OK)
            {
                bool ret = DeleteRecLoc(removeEntity.RecLoc);
                if (ret)
                {
                    removeEntity.SkuName = "";
                    removeEntity.SkuCode = "";
                    removeEntity.Spec = "";
                    bindingSource1.ResetBindings(false);
                }
            }

        }
    }
}
