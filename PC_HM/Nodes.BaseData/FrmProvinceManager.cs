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
    public partial class FrmProvinceManager : DevExpress.XtraEditors.XtraForm
    {
        private ProvinceDal ProvinceDal = null;
        private SupplierDal SupplierDal = null;
        private CustomerDal CustomerDal = null;
        public FrmProvinceManager()
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

            ProvinceDal = new ProvinceDal();
            CustomerDal = new CustomerDal();
            SupplierDal = new SupplierDal();
            LoadDataAndBindGrid();
        }

        public void LoadDataAndBindGrid()
        {
            try
            {
                bindingSource1.DataSource = ProvinceDal.GetAllProvince();
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
                    DoCreateProvince();
                    break;
                case "修改":
                    ShowEditProvince();
                    break;
                case "删除":
                    DoDeleteSelectedProvince();
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
        ProvinceEntity SelectedProvinceRow
        {
            get
            {
                if (gridView1.FocusedRowHandle < 0) return null;

                return gridView1.GetRow(gridView1.FocusedRowHandle) as ProvinceEntity;
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
        private void DoCreateProvince()
        {
            FrmProvinceEdit frmProvinceEdit = new FrmProvinceEdit();
            frmProvinceEdit.DataSourceChanged += OnCreateChanage;
            frmProvinceEdit.ShowDialog();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        private void ShowEditProvince()
        {
            ProvinceEntity editEntity = SelectedProvinceRow;
            if (editEntity == null)
            {
                MsgBox.Warn("没有要修改的数据。");
                return;
            }

            FrmProvinceEdit frmProvinceEdit = new FrmProvinceEdit(editEntity);
            frmProvinceEdit.DataSourceChanged += OnEditChanage;
            frmProvinceEdit.ShowDialog();
        }

        private void OnCreateChanage(object sender, EventArgs e)
        {
            ProvinceEntity newEntity = (ProvinceEntity)sender;
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
        private void DoDeleteSelectedProvince()
        {
            ProvinceEntity removeEntity = SelectedProvinceRow;
            if (removeEntity == null)
            {
                MsgBox.Warn("没有要删除的数据。");
                return;
            }

            if (MsgBox.AskOK(string.Format("确定要删除省份{0}吗？", removeEntity.ProvinceName)) == DialogResult.OK)
            {
                int result = ProvinceDal.DeleteProvince(removeEntity.ProvinceCode);
                if (result == 1)
                {
                    bindingSource1.Remove(removeEntity);
                }
                else
                {
                    MsgBox.Warn("删除失败，可能已经被其他人删除。");
                }
            }
        }

        private void gridView1_RowDoubleClick(object sender, EventArgs e)
        {
            ShowEditProvince();
        }
    }
}