using System;
using System.Windows.Forms;
using DevExpress.Utils;
using Nodes.UI;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Resources;

namespace Nodes.BaseData
{
    public partial class FrmSupplierManager : DevExpress.XtraEditors.XtraForm
    {
        private SupplierDal supplierDal = null;
        public FrmSupplierManager()
        {
            InitializeComponent();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            ImageCollection ic = AppResource.LoadToolImages();
            barManager1.Images = ic;
            toolRefresh.ImageIndex = (int)AppResource.EIcons.refresh;
            toolSearch.ImageIndex = (int)AppResource.EIcons.search;
            toolAdd.ImageIndex = (int)AppResource.EIcons.add;
            toolEdit.ImageIndex = (int)AppResource.EIcons.edit;
            toolDel.ImageIndex = (int)AppResource.EIcons.delete;

            supplierDal = new SupplierDal();
            LoadDataAndBindGrid();
        }

        public void LoadDataAndBindGrid()
        {
            try
            {
                bindingSource1.DataSource = supplierDal.GetAllSupplier();
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
                    DoCreateSupplier();
                    break;
                case "修改":
                    ShowEditSupplier();
                    break;
                case "删除":
                    DoDeleteSelectedSupplier();
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
        SupplierEntity SelectedSupplierRow
        {
            get
            {
                if (gridView1.FocusedRowHandle < 0) return null;

                return gridView1.GetRow(gridView1.FocusedRowHandle) as SupplierEntity;
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
        private void DoCreateSupplier()
        {
            FrmSupplierEdit frmSupplierEdit = new FrmSupplierEdit();
            frmSupplierEdit.DataSourceChanged += OnCreateChanage;
            frmSupplierEdit.ShowDialog();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        private void ShowEditSupplier()
        {
            SupplierEntity editEntity = SelectedSupplierRow;
            if (editEntity == null)
            {
                MsgBox.Warn("没有要修改的数据。");
                return;
            }

            FrmSupplierEdit frmSupplierEdit = new FrmSupplierEdit(editEntity);
            frmSupplierEdit.DataSourceChanged += OnEditChanage;
            frmSupplierEdit.ShowDialog();
        }

        private void OnCreateChanage(object sender, EventArgs e)
        {
            SupplierEntity newEntity = (SupplierEntity)sender;
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
        private void DoDeleteSelectedSupplier()
        {
            SupplierEntity removeEntity = SelectedSupplierRow;
            if (removeEntity == null)
            {
                MsgBox.Warn("没有要删除的数据。");
                return;
            }

            if (MsgBox.AskOK(string.Format("确定要删除供应商“{0}({1})”吗？", removeEntity.SupplierName, removeEntity.SupplierCode)) == DialogResult.OK)
            {
                bool ret = supplierDal.DeleteSupplier(removeEntity.SupplierCode);
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
            //ShowEditSupplier();
        }
    }
}