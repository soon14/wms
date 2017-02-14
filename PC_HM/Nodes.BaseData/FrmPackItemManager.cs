using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Nodes.DBHelper;
using DevExpress.Utils;
using Nodes.Entities;
using Nodes.Resources;
using Nodes.Utils;
using Nodes.Shares;

namespace Nodes.BaseData
{
    public partial class FrmPackItemManager : DevExpress.XtraEditors.XtraForm
    {
        private MaterialDal materialDal = new MaterialDal();
        PagerDal pageData = new PagerDal();

        public FrmPackItemManager()
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

            LoadDataAndBindGrid();
        }

        public void LoadDataAndBindGrid()
        {
            List<MaterialEntity> materials = materialDal.GetMaterialsByType(SysCodeConstant.MATERIAL_TYPE_PACK);
            bindingSource1.DataSource = materials;
        }

        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (e.Item.Tag.ToString())
            {
                case "刷新":
                    ReLoad();
                    break;
                case "新增":
                    DoCreateMaterial();
                    break;
                case "修改":
                    ShowEditMaterial();
                    break;
                case "删除":
                    DoDeleteSelectedMaterial();
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
        MaterialEntity SelectedMaterialRow
        {
            get
            {
                if (gridView1.FocusedRowHandle < 0) return null;

                return gridView1.GetRow(gridView1.FocusedRowHandle) as MaterialEntity;
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
        private void DoCreateMaterial()
        {
            FrmPackItemEdit frmMaterialEdit = new FrmPackItemEdit();
            frmMaterialEdit.DataSourceChanged += OnCreateChanage;
            frmMaterialEdit.ShowDialog();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        private void ShowEditMaterial()
        {
            MaterialEntity editEntity = SelectedMaterialRow;
            if (editEntity == null)
            {
                MsgBox.Warn("没有要修改的数据。");
                return;
            }

            FrmPackItemEdit frmMaterialEdit = new FrmPackItemEdit(editEntity);
            frmMaterialEdit.DataSourceChanged += OnEditChanage;
            frmMaterialEdit.ShowDialog();
        }

        private void OnCreateChanage(object sender, EventArgs e)
        {
            MaterialEntity newEntity = (MaterialEntity)sender;
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
        private void DoDeleteSelectedMaterial()
        {
            MaterialEntity removeEntity = SelectedMaterialRow;
            if (removeEntity == null)
            {
                MsgBox.Warn("没有要删除的数据。");
                return;
            }

            if (MsgBox.AskOK(string.Format("确定要删除包材{0}吗？", removeEntity.MaterialCode)) == DialogResult.OK)
            {
                bool ret = DeleteMaterial(removeEntity);
                if (ret)
                {
                    ReLoad();
                }
                else
                    MsgBox.Warn("删除失败。");
            }
        }

        public bool DeleteMaterial(MaterialEntity removeEntity)
        {
            bool result = false;
            try
            {
                result = materialDal.DeleteMaterial(removeEntity.MaterialCode);
                if (result)
                    RemoveRowFromGrid(removeEntity);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return result;
        }
        private void gridView1_RowDoubleClick(object sender, EventArgs e)
        {
            ShowEditMaterial();
        }

        #region IvMaterial 成员

        public void BindGrid(List<MaterialEntity> objs)
        {
            bindingSource1.DataSource = objs;
        }

        public void RemoveRowFromGrid(MaterialEntity obj)
        {
            bindingSource1.Remove(obj);
        }

        #endregion
    }
}