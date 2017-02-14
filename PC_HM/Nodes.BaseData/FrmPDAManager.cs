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
    public partial class FrmPDAManager : DevExpress.XtraEditors.XtraForm
    {
        private PDADal hhmDal = null;
        public FrmPDAManager()
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
            try
            {
                hhmDal = new PDADal();
                List<PDAEntity> unitEntities = hhmDal.GetAll();
                BindGrid(unitEntities);
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
                    ShowEditForm();
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
        PDAEntity SelectedRow
        {
            get
            {
                return gridView1.GetFocusedRow() as PDAEntity;
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
            FrmPDAEdit frmCreate = new FrmPDAEdit();
            frmCreate.DataSourceChanged += OnCreateChanage;
            frmCreate.ShowDialog();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        private void ShowEditForm()
        {
            PDAEntity editEntity = SelectedRow;
            if (editEntity == null)
            {
                MsgBox.Warn("没有要修改的数据。");
                return;
            }

            FrmPDAEdit frmEdit = new FrmPDAEdit(editEntity);
            frmEdit.DataSourceChanged += OnEditChanage;
            frmEdit.ShowDialog();
        }

        private void OnCreateChanage(object sender, EventArgs e)
        {
            PDAEntity newEntity = (PDAEntity)sender;
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
            PDAEntity removeEntity = SelectedRow;
            if (removeEntity == null)
            {
                MsgBox.Warn("没有要删除的数据。");
                return;
            }

            if (MsgBox.AskOK(string.Format("确定要删除编号为“{0}”的手持机吗？", removeEntity.PDACode)) == DialogResult.OK)
            {
                int ret = Delete(removeEntity);
                if (ret == 1)
                {
                    ReLoad();
                }
                else 
                    MsgBox.Warn("删除失败。");
            }
        }

        public int Delete(PDAEntity removeEntity)
        {
            int result = 0;
            try
            {
                result = hhmDal.Delete(removeEntity.PDACode);
                if (result == 1)
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
            ShowEditForm();
        }

        #region IvWarehouse 成员

        public void BindGrid(List<PDAEntity> objs)
        {
            bindingSource1.DataSource = objs;
        }

        public void RemoveRowFromGrid(PDAEntity obj)
        {
            bindingSource1.Remove(obj);
        }

        #endregion
    }
}