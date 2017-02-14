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
    public partial class FrmMaterialTypeManager : DevExpress.XtraEditors.XtraForm
    {
        //private MaterialTypeDal mtlTypeDal = null;
        public FrmMaterialTypeManager()
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

            //mtlTypeDal = new MaterialTypeDal();
            LoadDataAndBindGrid();
        }

        ///<summary>
        ///查询所有分类
        ///</summary>
        ///<returns></returns>
        public List<MaterialTypeEntity> GetMaterialTypeAll()
        {
            List<MaterialTypeEntity> list = new List<MaterialTypeEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("roleId=").Append(roleId);
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_GetMaterialTypeAll);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetMaterialTypeAll bill = JsonConvert.DeserializeObject<JsonGetMaterialTypeAll>(jsonQuery);
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
                foreach (JsonGetMaterialTypeAllResult jbr in bill.result)
                {
                    MaterialTypeEntity asnEntity = new MaterialTypeEntity();
                    
                    #region 11-14
                    asnEntity.MaterialTypeCode = jbr.typCode;
                    asnEntity.MaterialTypeName = jbr.typName;
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
                bindingSource1.DataSource = GetMaterialTypeAll();
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
                    DoCreate();
                    break;
                case "修改":
                    ShowEdit();
                    break;
                case "删除":
                    DoDelete();
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
        MaterialTypeEntity SelectedHeaderRow
        {
            get
            {
                if (gridView1.FocusedRowHandle < 0) return null;

                return gridView1.GetRow(gridView1.FocusedRowHandle) as MaterialTypeEntity;
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
        private void DoCreate()
        {
            FrmMaterialTypeEdit frmEdit = new FrmMaterialTypeEdit();
            frmEdit.DataSourceChanged += OnCreateChanage;
            frmEdit.ShowDialog();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        private void ShowEdit()
        {
            MaterialTypeEntity editEntity = SelectedHeaderRow;
            if (editEntity == null)
            {
                MsgBox.Warn("没有要修改的数据。");
                return;
            }

            FrmMaterialTypeEdit frmEdit = new FrmMaterialTypeEdit(editEntity);
            frmEdit.DataSourceChanged += OnEditChanage;
            frmEdit.ShowDialog();
        }

        private void OnCreateChanage(object sender, EventArgs e)
        {
            MaterialTypeEntity newEntity = (MaterialTypeEntity)sender;
            bindingSource1.Add(newEntity);
            bindingSource1.ResetBindings(false);
        }

        private void OnEditChanage(object sender, EventArgs e)
        {
            bindingSource1.ResetBindings(false);
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool DeleteSkuType(string code)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("typCode=").Append(code);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_DeleteSkuType);
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
        private void DoDelete()
        {
            MaterialTypeEntity removeEntity = SelectedHeaderRow;
            if (removeEntity == null)
            {
                MsgBox.Warn("没有要删除的数据。");
                return;
            }

            if (MsgBox.AskOK(string.Format("确定要删除分类{0}({1})吗？", removeEntity.MaterialTypeCode, removeEntity.MaterialTypeName)) == DialogResult.OK)
            {
                bool ret = DeleteSkuType(removeEntity.MaterialTypeCode);
                if (ret)
                {
                    bindingSource1.Remove(removeEntity);
                }
                //else
                //    MsgBox.Warn("删除失败，可能已经被其他人删除。");
            }
        }

        private void gridView1_RowDoubleClick(object sender, EventArgs e)
        {
            ShowEdit();
        }
    }
}