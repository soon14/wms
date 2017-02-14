using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Nodes.DBHelper;
using DevExpress.Utils;
using Nodes.Entities;
using Nodes.Resources;
using Nodes.Utils;
using Nodes.UI;

namespace Nodes.BaseData
{
    public partial class FrmTemperatureManager : DevExpress.XtraEditors.XtraForm
    {
        private TemperatureDal temperatureDal = null;
        public FrmTemperatureManager()
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

            temperatureDal = new TemperatureDal();
            LoadDataAndBindGrid();
        }

        public void LoadDataAndBindGrid()
        {
            try
            {
                bindingSource1.DataSource = temperatureDal.GetAll();
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
                    DoCreate();
                    break;
                case "修改":
                    ShowEdit();
                    break;
                case "删除":
                    DoDeleteSelected();
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
        TemperatureEntity SelectedUnitRow
        {
            get
            {
                if (gridView1.FocusedRowHandle < 0)
                    return null;

                return gridView1.GetFocusedRow() as TemperatureEntity;
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
            FrmTemperatureEdit frmEdit = new FrmTemperatureEdit();
            frmEdit.DataSourceChanged += OnCreateChanage;
            frmEdit.ShowDialog();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        private void ShowEdit()
        {
            TemperatureEntity editEntity = SelectedUnitRow;
            if (editEntity == null)
            {
                MsgBox.Warn("没有要修改的数据。");
                return;
            }

            FrmTemperatureEdit frmEdit = new FrmTemperatureEdit(editEntity);
            frmEdit.DataSourceChanged += OnEditChanage;
            frmEdit.ShowDialog();
        }

        private void OnCreateChanage(object sender, EventArgs e)
        {
            TemperatureEntity newEntity = (TemperatureEntity)sender;
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
        private void DoDeleteSelected()
        {
            TemperatureEntity removeEntity = SelectedUnitRow;
            if (removeEntity == null)
            {
                MsgBox.Warn("没有要删除的数据。");
                return;
            }

            if (removeEntity.AllowEdit == "N")
            {
                MsgBox.Warn("这是系统预定义数据，不允许删除。");
                return;
            }

            if (MsgBox.AskOK(string.Format("确定要删除温控“{0}[{1}]”吗？", removeEntity.TemperatureName, removeEntity.TemperatureCode)) == DialogResult.OK)
            {
                int ret = temperatureDal.DeleteOne(removeEntity.TemperatureCode);
                if (ret == 1)
                    bindingSource1.Remove(removeEntity);
                else if (ret == -1)
                    MsgBox.Warn("不允许删除，因为有货区在引用中。");
                else
                    MsgBox.Warn("删除失败。");
            }
        }

        private void gridView1_RowDoubleClick(object sender, EventArgs e)
        {
            ShowEdit();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            TemperatureEntity removeEntity = SelectedUnitRow;
            if (removeEntity == null || removeEntity.AllowEdit == "N")
                toolEdit.Enabled = toolDel.Enabled = false;
            else
                toolEdit.Enabled = toolDel.Enabled = true;
        }
    }
}