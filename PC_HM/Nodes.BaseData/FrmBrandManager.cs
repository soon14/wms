using System;
using System.Windows.Forms;
using DevExpress.Utils;
using Nodes.UI;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Resources;
using System.Collections.Generic;
using Nodes.Shares;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.BaseData;
using Nodes.Entities.HttpEntity.Stock;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nodes.BaseData
{
    public partial class FrmBrandManager : DevExpress.XtraEditors.XtraForm
    {
        //private BrandDal brandDal = null; 
        public FrmBrandManager()
        {
            InitializeComponent();
        }

        private void FomBrandsManager_Load(object sender, EventArgs e)
        {
            ImageCollection ic = AppResource.LoadToolImages();
            barManager1.Images = ic;
            toolAdd.ImageIndex = (int)AppResource.EIcons.add;
            toolEdit.ImageIndex = (int)AppResource.EIcons.edit;
            toolDel.ImageIndex = (int)AppResource.EIcons.delete;
            toolRefresh.ImageIndex = (int)AppResource.EIcons.refresh;
            toolSearch.ImageIndex = (int)AppResource.EIcons.search;
            barButtonItem1.ImageIndex = (int)AppResource.EIcons.tree;
            barButtonItem2.ImageIndex = (int)AppResource.EIcons.remove;
            

            //brandDal = new BrandDal();
            LoadDataAndBindGrid();
        }

        /// <summary>
        /// 获取所有的品牌
        /// </summary>
        public List<BrandEntity> GetAllBrands()
        {
            List<BrandEntity> list = new List<BrandEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("roleId=").Append(roleId);
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_GetAllBrands);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetAllBrands bill = JsonConvert.DeserializeObject<JsonGetAllBrands>(jsonQuery);
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
                foreach (JsonGetAllBrandsResult jbr in bill.result)
                {
                    BrandEntity asnEntity = new BrandEntity();
                    #region 11-16
                    asnEntity.BrandCode = jbr.brdCode;
                    asnEntity.BrandName = jbr.brdName;
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
                bindingSource1.DataSource = GetAllBrands();
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
                    DoCreateBrands();
                    break;
                case "修改":
                    ShowEditBrands();
                    break;
                case "删除":
                    DoDeleteSelectedBrands();
                    break;
                case "关联供应商":
                    AddNewRelationWithSupplier();
                    break;
                case "取消关联":
                    DeleteRelationSupplier();
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
        BrandEntity SelectedBrandsRow
        {
            get
            {
                if (gridView1.FocusedRowHandle < 0)
                    return null;

                return gridView1.GetFocusedRow() as BrandEntity;
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
        private void DoCreateBrands()
        {
            FrmBrandEdit frmunitEdit = new FrmBrandEdit();
            frmunitEdit.DataSourceChanged += OnCreateChanage;
            frmunitEdit.ShowDialog();
        }

        private void OnCreateChanage(object sender, EventArgs e)
        {
            BrandEntity newEntity = (BrandEntity)sender;
            bindingSource1.Add(newEntity);
            bindingSource1.ResetBindings(false);
        }

        private void OnEditChanage(object sender, EventArgs e)
        {
            bindingSource1.ResetBindings(false);
        }


        /// <summary>
        /// 编辑
        /// </summary>
        private void ShowEditBrands()
        {
            //BrandEntity editEntity = SelectedBrandsRow;
            //if (editEntity == null)
            //{
            //    MsgBox.Warn("没有要修改的数据。");
            //    return;
            //}

            //FrmBrandEdit frmunitEdit = new FrmBrandEdit(editEntity);
            //frmunitEdit.DataSourceChanged += OnEditChanage;
            //frmunitEdit.ShowDialog();
        }

        /// <summary>
        /// 删除
        /// </summary>
        private void DoDeleteSelectedBrands()
        {
            //BrandEntity removeEntity = SelectedBrandsRow;
            //if (removeEntity == null)
            //{
            //    MsgBox.Warn("没有要删除的数据。");
            //    return;
            //}

            //if (MsgBox.AskOK(string.Format("确定要删除“({0}){1}”吗？", removeEntity.BrandCode, removeEntity.BrandName)) == DialogResult.OK)
            //{
            //    int ret = brandDal.Delete(removeEntity.BrandCode);
            //    if (ret == 1)
            //    {
            //        bindingSource1.Remove(removeEntity);
            //    }
            //    else
            //        MsgBox.Warn("删除失败。");
            //}   
        }

        private void gridView1_RowDoubleClick(object sender, EventArgs e)
        {
            ShowEditBrands();
        }
       


        #region 关联供应商
        /// <summary>
        /// 关联供应商
        /// </summary>
        private void AddNewRelationWithSupplier()
        {
            BrandEntity brand = SelectedBrandsRow;
            if (brand == null)
            {
                MsgBox.Warn("请选中品牌。");
                return;
            }

            using (FrmChooseSuppliers frmSupplier = new FrmChooseSuppliers())
            {
                if (frmSupplier.ShowDialog() == DialogResult.OK)
                {
                    DoCreateRelation(brand, frmSupplier.Suppliers);
                }
            }
        }

        /// <summary>
        /// 获得选中数据
        /// </summary>
        BrandEntity SelectedRow
        {
            get
            {
                if (gridView1.FocusedRowHandle < 0) return null;

                return gridView1.GetRow(gridView1.FocusedRowHandle) as BrandEntity;
            }
        }

        /// <summary>
        /// //存入数据库，排除已经关联的
        /// </summary>
        /// <param name="brandCode"></param>
        /// <param name="suppliers"></param>
        /// <returns></returns>
        public bool CreateRelationWithSupplier(string brandCode, List<SupplierEntity> suppliers)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("brdCode=").Append(brandCode).Append("&");
                string jsons = string.Empty;
                foreach (SupplierEntity tem in suppliers)
                {
                    jsons += tem.SupplierCode;
                    jsons += ",";
                }
                jsons = jsons.Substring(0, jsons.Length - 1);
                loStr.Append("sCodes=").Append(jsons);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_CreateRelationWithSupplier);
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

        private void DoCreateRelation(BrandEntity Brand, List<SupplierEntity> suppliers)
        {
            try
            {
                //存入数据库，排除已经关联的
                CreateRelationWithSupplier(Brand.BrandCode, suppliers);

                //重新绑定关联的供应商
                LoadRelationSupplier(Brand.BrandCode);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 基础管理（品牌信息-断开品牌与供应商关联）
        /// </summary>
        /// <param name="brandCode"></param>
        /// <param name="supplierCode"></param>
        /// <returns></returns>
        public bool DeleteRelationSupplier(string brandCode, string supplierCode)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("brdCode=").Append(brandCode).Append("&");
                loStr.Append("sCode=").Append(supplierCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_DeleteRelationSupplier);
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

        private void DeleteRelationSupplier()
        {
            //断开关联的供应商
            BrandEntity brand = SelectedRow;
            if (brand == null)
            {
                MsgBox.Warn("请选中品牌行。");
                return;
            }

            if (gridView2.FocusedRowHandle < 0)
            {
                MsgBox.Warn("请选中要取消的供应商。");
                return;
            }

            SupplierEntity supplier = gridView2.GetRow(gridView2.FocusedRowHandle) as SupplierEntity;
            if (DialogResult.OK !=
                MsgBox.AskOK(string.Format("确定要断开品牌“{0}”与供应商“{1}”之间的关联关系吗？",
                brand.BrandName, supplier.SupplierName)))
            {
                return;
            }

            try
            {
                bool result = DeleteRelationSupplier(brand.BrandCode, supplier.SupplierCode);
                if (result)
                    LoadRelationSupplier(brand.BrandCode);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void OnGridFocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            BrandEntity m = SelectedRow;
            if (m != null)
                LoadRelationSupplier(m.BrandCode);
        }

        /// <summary>
        /// 基础管理（品牌信息-重新绑定关联的供应商）
        /// </summary>
        /// <param name="brandCode"></param>
        /// <returns></returns>
        public List<SupplierEntity> ListRelationSuppliers(string brandCode)
        {
            List<SupplierEntity> list = new List<SupplierEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("brdCode=").Append(brandCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_ListRelationSuppliers);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonListRelationSuppliers bill = JsonConvert.DeserializeObject<JsonListRelationSuppliers>(jsonQuery);
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
                foreach (JsonListRelationSuppliersResult jbr in bill.result)
                {
                    SupplierEntity asnEntity = new SupplierEntity();
                    #region 
                    asnEntity.SupplierCode = jbr.sCode;
                    asnEntity.SupplierName = jbr.sName;
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

        private void LoadRelationSupplier(string brandCode)
        {
            try
            {
                //重新绑定关联的供应商
                gridControl2.DataSource = ListRelationSuppliers(brandCode);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        #endregion 

    }
}
