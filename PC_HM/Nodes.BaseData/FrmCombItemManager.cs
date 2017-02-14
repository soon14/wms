using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Nodes.DBHelper;
using DevExpress.Utils;
using Nodes.Entities;
using Nodes.Resources;
using Nodes.Utils;
using Nodes.Shares;
using Nodes.UI;

namespace Nodes.BaseData
{
    public partial class FrmCombItemManager : DevExpress.XtraEditors.XtraForm
    {
        private MaterialDal materialDal = null;
        public FrmCombItemManager()
        {
            InitializeComponent();
        }

        private void FrmLocationManager_Load(object sender, EventArgs e)
        {
            ImageCollection ic = AppResource.LoadToolImages();
            barManager1.Images = ic;
            toolEdit.ImageIndex = (int)AppResource.EIcons.edit;
            toolRefresh.ImageIndex = (int)AppResource.EIcons.refresh;
            toolSearch.ImageIndex = (int)AppResource.EIcons.search;
            barButtonItem1.ImageIndex = (int)AppResource.EIcons.back;

            LoadDataAndBindGrid();
        }

        public void LoadDataAndBindGrid()
        {
            try
            {
                materialDal = new MaterialDal();
                List<MaterialEntity> materials = materialDal.GetMaterialsByType(SysCodeConstant.MATERIAL_TYPE_COMB);
                bindingSource1.DataSource = materials;
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
                case "修改":
                    ShowEditDlg();
                    break;
                case "转为普通物料":
                    ChangeToMaterial();
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
        MaterialEntity SelectedRow
        {
            get
            {
                return gridView1.GetFocusedRow() as MaterialEntity;
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        private void ReLoad()
        {
            LoadDataAndBindGrid();
            ShowFocusedHeaderDetails();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        private void ShowEditDlg()
        {
            MaterialEntity editEntity = SelectedRow;
            if (editEntity == null)
            {
                MsgBox.Warn("请选中要修改的行。");
                return;
            }

            FrmCombItemEdit frmLocationEdit = new FrmCombItemEdit(editEntity);
            frmLocationEdit.ShowDialog();
            ShowFocusedHeaderDetails();
        }

        void ChangeToMaterial()
        {
            MaterialEntity editEntity = SelectedRow;
            if (editEntity == null)
            {
                MsgBox.Warn("请选中要修改的行。");
                return;
            }

            if (MsgBox.AskOK("确定要重置为普通物料吗？") != DialogResult.OK)
                return;

            try
            {
                int result = new CombMaterialDal().UpdateComToMaterialType(editEntity.MaterialCode);
                bindingSource1.RemoveCurrent();
                ShowFocusedHeaderDetails();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void gridView1_RowDoubleClick(object sender, EventArgs e)
        {
            ShowEditDlg();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ShowFocusedHeaderDetails();
        }

        void ShowFocusedHeaderDetails()
        {
            string itemCode = ConvertUtil.ToString(gridView1.GetFocusedRowCellValue("MaterialCode"));

            if (string.IsNullOrEmpty(itemCode))
            {
                gridControl2.DataSource = null;
            }
            else
            {
                List<MaterialEntity> materials = new CombMaterialDal().ListMaterialsByCombCode(itemCode);
                gridControl2.DataSource = materials;
            }
        }
    }
}