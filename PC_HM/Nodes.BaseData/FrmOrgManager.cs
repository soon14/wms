using System;
using System.Windows.Forms;
using DevExpress.Utils;
using Nodes.UI;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Resources;

namespace Nodes.BaseData
{
    public partial class FrmOrgManager : DevExpress.XtraEditors.XtraForm
    {
        private OrganizationDal OrgDal = null;
        private WarehouseDal WarehouseDal = null;
        public FrmOrgManager()
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

            OrgDal = new OrganizationDal();
            WarehouseDal = new WarehouseDal();

            ReLoad();
        }

        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (e.Item.Tag.ToString())
            {
                case "刷新":
                    ReLoad();
                    break;
                case "新增":
                    DoCreateArea();
                    break;
                case "修改":
                    ShowEditRow();
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
        OrgEntity SelectedRow
        {
            get
            {
                if (gridView1.FocusedRowHandle < 0) return null;
                return gridView1.GetRow(gridView1.FocusedRowHandle) as OrgEntity;
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        private void ReLoad()
        {
            bindingSource1.DataSource = OrgDal.GetAll();
        }

        /// <summary>
        /// 新增
        /// </summary>
        private void DoCreateArea()
        {
            FrmOrgEdit frmEdit = new FrmOrgEdit();
            frmEdit.DataSourceChanged += OnCreateChanage;
            frmEdit.ShowDialog();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        private void ShowEditRow()
        {
            OrgEntity editEntity = SelectedRow;
            if (editEntity == null)
            {
                MsgBox.Warn("请选中要修改的行。");
                return;
            }

            FrmOrgEdit frmEdit = new FrmOrgEdit(editEntity);
            frmEdit.DataSourceChanged += OnEditChanage;
            frmEdit.ShowDialog();
        }

        private void OnCreateChanage(object sender, EventArgs e)
        {
            OrgEntity newEntity = (OrgEntity)sender;
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
            OrgEntity removeEntity = SelectedRow;
            if (removeEntity == null)
            {
                MsgBox.Warn("请选中要删除的行。");
                return;
            }

            if (MsgBox.AskOK(string.Format("确定要删除{0}吗？", removeEntity.OrgName)) == DialogResult.OK)
            {
                int result = OrgDal.Delete(removeEntity.OrgCode);
                if (result == 1)
                {
                    bindingSource1.Remove(removeEntity);
                    ReLoad();
                }
                else if (result == -1)
                {
                    MsgBox.Warn("不能删除，该行有相关联的仓库。");
                }
                else if (result == -2)
                {
                    MsgBox.Warn("不能删除，该行有相关联的用户。");
                }
            }
        }

        private void OnGridViewRowDoubleClick(object sender, EventArgs e)
        {
            ShowEditRow();
        }

        private void OnGridViewFocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gridControl2.DataSource = WarehouseDal.GetAllWarehouseByOrg(SelectedRow.OrgCode);
        }
    }
}