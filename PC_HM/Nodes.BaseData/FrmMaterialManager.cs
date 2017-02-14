using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.Utils;
using Nodes.UI;
using Nodes.Utils;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Resources;
using System.Collections.Generic;
using Nodes.Shares;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.BaseData;
using Nodes.Entities.HttpEntity.Stock;
using Newtonsoft.Json;

namespace Nodes.BaseData
{
    public partial class FrmMaterialManager : DevExpress.XtraEditors.XtraForm
    {
        #region 变量

        //private MaterialDal materialDal = new MaterialDal();
        private static readonly string[] TAG_ARRAY = { "所有物料信息", "本库物料信息" };
        private string _currentTag = TAG_ARRAY[0];

        #endregion

        #region 私有变量

        private string mLocation = string.Empty;
        private string mSkuName = string.Empty;
        private bool isAllMaterial = true;

        /// <summary>
        /// 每页显示记录数
        /// </summary>
        private int _pageSize = 100;

        private int _nMax = 0;
        /// <summary>
        /// 总记录数
        /// </summary>
        public int NMax
        {
            get { return _nMax; }
            set
            {
                _nMax = value;
                GetPageCount();
            }
        }

        private int _pageCount = 0;
        /// <summary>
        /// 页数=总记录数/每页显示记录数
        /// </summary>
        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = value; }
        }

        private int _pageCurrent = 1;
        /// <summary>
        /// 当前页号
        /// </summary>
        public int PageCurrent
        {
            get { return _pageCurrent; }
            set { _pageCurrent = value; }
        }

        #endregion

        #region 分页方法
        private void GetPageCount()
        {
            if (this.NMax > 0)
            {
                this.PageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(this.NMax) / Convert.ToDouble(this._pageSize)));
            }
            else
            {
                this.PageCount = 0;
            }
        }

        /// <summary>
        /// 翻页控件数据绑定的方法
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="flag">表明是否重新查询</param>
        public void Bind(int nums, bool flag = false)
        {
            this.NMax = nums;

            //if (!flag)
            //    this.txtCurePage.Text = this.PageCurrent.ToString();       
            //else
            //    this.txtCurePage.Text = "1";

            //if (this._pageCurrent == 0)
            this.txtCurePage.Text = this._pageCurrent.ToString();
            this.txtCountPage.Text = this.PageCount.ToString();

            if (this.PageCurrent > this.PageCount)
            {
                this.PageCurrent = this.PageCount;
            }

            if (this.PageCurrent == 1)
            {
                this.labPre.Enabled = false;
                this.labFirst.Enabled = false;
            }
            else
            {
                labPre.Enabled = true;
                labFirst.Enabled = true;
            }

            if (this.PageCurrent == this.PageCount)
            {
                this.labLast.Enabled = false;
                this.labNext.Enabled = false;
            }
            else
            {
                labLast.Enabled = true;
                labNext.Enabled = true;
            }

            if (this.NMax == 0)
            {
                labNext.Enabled = false;
                labLast.Enabled = false;
                labFirst.Enabled = false;
                labPre.Enabled = false;
            }
        }

        //页面显示个数
        public List<MaterialEntity> Query(int isPage = 0)
        {
            //查询从pageCur*_pageSize行到
            List<MaterialEntity> data = new List<MaterialEntity>();
            try
            {
                int total;
                switch (isPage)//从（PageCurrent-1）*_pageSize行 到 （PageCurrent-1）*_pageSize + _pageSize 行{ 首页，前_pageSize行，最后也页总记录数整除_pageSize的行数}
                {
                    case 1: //首页，前_pageSize行
                        if (isAllMaterial)
                            data = GetAll(_pageSize, 0, out total);
                        else
                            data = GetLocalAll(_pageSize, 0, out total);
                        break;
                    case 2://从PageCurrent*_pageSize行
                        if(isAllMaterial)
                            data = GetAll(_pageSize, (PageCurrent - 1) * _pageSize, out total);
                        else
                            data = GetLocalAll(_pageSize, (PageCurrent - 1) * _pageSize, out total);
                        break;
                    case 3://最后也页总记录数整除_pageSize的行数
                        if(isAllMaterial)
                            data = GetAll(_pageSize, (PageCount - 1) * _pageSize, out total);
                        else
                            data = GetLocalAll(_pageSize, (PageCount - 1) * _pageSize, out total);
                        break;
                    case 4://调转页
                        if(isAllMaterial)
                            data = GetAll(_pageSize, (PageCurrent - 1) * _pageSize, out total);
                        else
                            data = GetLocalAll(_pageSize, (PageCurrent - 1) * _pageSize, out total);
                        break;
                    default://重新选择查询条件
                        {
                            if (isAllMaterial)
                                data = GetAll(_pageSize, 0, out total);
                            else
                                data = GetLocalAll(_pageSize, 0, out total);
                            PageCurrent = 1;
                            this.txtCurePage.Text = this.PageCurrent.ToString();
                        }
                        break;
                }

                this.Bind(total);//分页数据
                #region 暂时不需要
                //是否是重新查询
                //if (isQueryPage)
                //{
                //    PageCurrent = 1;
                //    this.txtCurePage.Text = this.PageCurrent.ToString();
                //    isQueryPage = false;
                //}
                #endregion

                bindingSource1.DataSource = data;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return data;
        }
        #endregion

        public FrmMaterialManager()
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
            toolCopyNew.ImageIndex = (int)AppResource.EIcons.copy;
            toolPrint.ImageIndex = (int)AppResource.EIcons.print;
            toolDesign.ImageIndex = (int)AppResource.EIcons.design;
            toolMaterial.ImageIndex = (int)AppResource.EIcons.search;
            //barButtonItem1.ImageIndex = (int)AppResource.EIcons.tree;
            //barButtonItem2.ImageIndex = (int)AppResource.EIcons.remove;

            //materialDal = new MaterialDal();
            //CustomFields.AppendMaterialFields(gridView1);
            LoadDataAndBindGrid();
        }

        /// <summary>
        /// 查询所有物料，用于物料维护，如果是填充其他界面，请调用GetActiveMaterials()函数
        /// </summary>
        /// <returns></returns>
        public List<MaterialEntity> GetAll(int nums, int begin, out int total)
        {
            List<MaterialEntity> list = new List<MaterialEntity>();
            total = 0;
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                if (begin == 0)
                {
                    loStr.Append("beginRow=").Append("&");
                    loStr.Append("rows=");
                }
                else
                {
                    loStr.Append("beginRow=").Append(begin).Append("&");
                    loStr.Append("rows=").Append(nums);
                }
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetAll,20000);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetAllSku bill = JsonConvert.DeserializeObject<JsonGetAllSku>(jsonQuery);
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
                foreach (JsonGetAllSkuResult jbr in bill.result)
                {
                    MaterialEntity asnEntity = new MaterialEntity();
                    #region 0-10
                    asnEntity.ExpDays = Convert.ToInt32(jbr.expDays);
                    asnEntity.BrandName = jbr.brdName;
                    asnEntity.MaterialCode = jbr.skuCode;
                    asnEntity.MaterialName = jbr.skuName;
                    asnEntity.MaxStockQty = Convert.ToInt32(jbr.maxStockQty);
                    asnEntity.MinStockQty = Convert.ToInt32(jbr.minStockQty);
                    asnEntity.SecurityQty = Convert.ToInt32(jbr.securityQty);
                    asnEntity.SkuType = jbr.skuType;
                    asnEntity.SkuTypeDesc = jbr.itemDesc;
                    asnEntity.Spec = jbr.spec;
                    #endregion

                    #region 11-20
                    asnEntity.TemperatureName = jbr.tempName;
                    asnEntity.TemperatureCode = jbr.tempCode;
                    asnEntity.TotalStockQty = StringToDecimal.GetTwoDecimal(jbr.totalStockQty);//Math.Round(Convert.ToDecimal(ret), 2);
                    asnEntity.MaterialTypeName = jbr.typName;
                    //asnEntity.UnitGrpCode
                    #endregion
                    try
                    {
                        //if (!string.IsNullOrEmpty(jbr.closeDate))
                        //    asnEntity.CloseDate = Convert.ToDateTime(jbr.closeDate);
                        //if (!string.IsNullOrEmpty(jbr.printedTime))
                        //    asnEntity.PrintedTime = Convert.ToDateTime(jbr.printedTime);
                        //if (!string.IsNullOrEmpty(jbr.createDate))
                        //    asnEntity.CreateDate = Convert.ToDateTime(jbr.createDate);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    }
                    list.Add(asnEntity);
                }

                total = bill.total;
                return list;
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return list;
        }

        ///<summary>
        ///基础管理（物料信息-获取本库物料信息）
        ///</summary>
        ///<returns></returns>
        public List<MaterialEntity> GetLocalAll(int nums, int begin, out int total)
        {
            List<MaterialEntity> list = new List<MaterialEntity>();
            total = 0;
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                if (begin == 0)
                {
                    loStr.Append("beginRow=").Append("&");
                    loStr.Append("rows=");
                }
                else
                {
                    loStr.Append("beginRow=").Append(begin).Append("&");
                    loStr.Append("rows=").Append(nums);
                }
                
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetLocalAll);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetLocalAll bill = JsonConvert.DeserializeObject<JsonGetLocalAll>(jsonQuery);
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
                foreach (JsonGetLocalAllResult jbr in bill.result)
                {
                    MaterialEntity asnEntity = new MaterialEntity();
                    #region 0-10
                    asnEntity.BrandName = jbr.brdName;
                    asnEntity.ExpDays = Convert.ToInt32(jbr.expDays);
                    asnEntity.MaterialCode = jbr.skuCode;
                    asnEntity.MaterialName = jbr.skuName;
                    asnEntity.SkuType = jbr.skuType;
                    asnEntity.SkuTypeDesc = jbr.itemDesc;
                    asnEntity.MaxStockQty = Convert.ToInt32(jbr.maxStockQty);
                    asnEntity.MinStockQty = Convert.ToInt32(jbr.minStockQty);
                    asnEntity.SecurityQty = Convert.ToInt32(jbr.securityQty);
                    asnEntity.Spec = jbr.spec;
                    #endregion

                    #region 11-16
                    asnEntity.TemperatureCode = jbr.tempCode;
                    asnEntity.TemperatureName = jbr.tempName;
                    asnEntity.TotalStockQty = StringToDecimal.GetTwoDecimal(jbr.totalStockQty);//Math.Round(Convert.ToDecimal(ret), 2);
                    asnEntity.MaterialTypeName = jbr.typName;
                    #endregion
                    list.Add(asnEntity);
                }

                total = bill.total;

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
            if (this._currentTag == TAG_ARRAY[0])
            {
                if(!isAllMaterial)
                    isAllMaterial = true;
               //this.bindingSource1.DataSource = GetAll();
            }
            else if (this._currentTag == TAG_ARRAY[1])
            {
                if(isAllMaterial)
                    isAllMaterial = false;
                //Query();
            }

            Query();
        }

        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (e.Item.Tag.ToString())
            {
                case "刷新":
                    using (WaitDialogForm frm = new WaitDialogForm("查询中...", "请稍等"))
                    {
                        ReLoad();
                    }
                    
                    break;
                case "新增":
                    DoCreateMaterial();
                    break;
                case "复制新增":
                    DoCopyNewMaterial();
                    break;
                case "修改":
                    ShowEditMaterial();
                    break;
                case "删除":
                    DoDeleteSelectedMaterial();
                    break;
                //case "关联供应商":
                //    AddNewRelationWithSupplier();                    
                //    break;
                //case "取消关联":
                //    DeleteRelationSupplier();
                //    break;
                case "快速查找":
                    if (gridView1.IsFindPanelVisible)
                        gridView1.HideFindPanel();
                    else
                        gridView1.ShowFindPanel();
                    break;
                case "本库物料信息":
                    using (WaitDialogForm frm = new WaitDialogForm("查询中...", "请稍等"))
                    {
                        if (e.Item.Caption == (this._currentTag = TAG_ARRAY[0]))
                        {
                            e.Item.Caption = TAG_ARRAY[1];
                        }
                        else if (e.Item.Caption == (this._currentTag = TAG_ARRAY[1]))
                        {
                            e.Item.Caption = TAG_ARRAY[0];
                        }
                        this.ReLoad();
                    }
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
            FrmMaterialEdit frmMaterialEdit = new FrmMaterialEdit(null, true);
            frmMaterialEdit.DataSourceChanged += OnEditChanage;
            frmMaterialEdit.ShowDialog();
        }

        //复制新增
        private void DoCopyNewMaterial()
        {
            MaterialEntity editEntity = SelectedMaterialRow;
            if (editEntity == null)
            {
                MsgBox.Warn("请选中一行作为样本数据。");
                return;
            }

            FrmMaterialEdit frmMaterialEdit = new FrmMaterialEdit(editEntity, true);
            frmMaterialEdit.DataSourceChanged += OnEditChanage;
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

            FrmMaterialEdit frmMaterialEdit = new FrmMaterialEdit(editEntity, false);
            frmMaterialEdit.DataSourceChanged += OnEditChanage;
            frmMaterialEdit.ShowDialog();
        }

        private void OnEditChanage(object sender, EventArgs e)
        {
            ReLoad();
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

            if (MsgBox.AskOK(string.Format("确定要删除物料{0}吗？", removeEntity.MaterialCode)) == DialogResult.OK)
            {
                //bool ret = materialDal.DeleteMaterial(removeEntity.MaterialCode);
                //if (ret)
                //    bindingSource1.Remove(removeEntity);
                //else
                //    MsgBox.Warn("删除失败。");
            }
        }

        //private void AddNewRelationWithSupplier()
        //{
        //    MaterialEntity material = SelectedMaterialRow;
        //    if (material == null)
        //    {
        //        MsgBox.Warn("请选中物料行。");
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(material.SupplierCode))
        //    {
        //        MsgBox.Warn("若要关联其他供应商，必须先设置一个默认供应商。");
        //        return;
        //    }

        //    using (FrmChooseSuppliers frmSupplier = new FrmChooseSuppliers())
        //    {
        //        if (frmSupplier.ShowDialog() == DialogResult.OK)
        //        {
        //            DoCreateRelation(material, frmSupplier.Suppliers);
        //        }
        //    }
        //}

        //private void DoCreateRelation(MaterialEntity material, List<SupplierEntity> suppliers)
        //{
        //    try
        //    {
        //        //存入数据库，排除已经关联的
        //        materialDal.CreateRelationWithSupplier(material.MaterialCode, material.SupplierCode, suppliers);

        //        //重新绑定关联的供应商
        //        LoadRelationSupplier(material.MaterialCode);
        //    }
        //    catch (Exception ex)
        //    {
        //        MsgBox.Err(ex.Message);
        //    }
        //}

        //private void LoadRelationSupplier(string materialCode)
        //{
        //    try
        //    {
        //        //重新绑定关联的供应商
        //        gridControl2.DataSource = materialDal.ListRelationSuppliers(materialCode);
        //    }
        //    catch (Exception ex)
        //    {
        //        MsgBox.Err(ex.Message);
        //    }
        //}

        //private void DeleteRelationSupplier()
        //{

        //    //断开关联的供应商
        //    MaterialEntity material = SelectedMaterialRow;
        //    if (material == null)
        //    {
        //        MsgBox.Warn("请选中物料行。");
        //        return;
        //    }

        //    if (gridView2.FocusedRowHandle < 0)
        //    {
        //        MsgBox.Warn("请选中要取消的供应商。");
        //        return;
        //    }

        //    SupplierEntity supplier = gridView2.GetRow(gridView2.FocusedRowHandle) as SupplierEntity;
        //    if (DialogResult.OK !=
        //        MsgBox.AskOK(string.Format("确定要断开物料“{0}”与供应商“{1}”之间的关联关系吗？",
        //        material.MaterialName, supplier.SupplierName)))
        //    {
        //        return;
        //    }

        //    try
        //    {
        //        int result = materialDal.DeleteRelationSupplier(material.MaterialCode, supplier.SupplierCode);
        //        if (result != 1)
        //            MsgBox.Warn("断开关联失败，可能已经被其他人断开了关联，请稍后重试。");
        //        else
        //            LoadRelationSupplier(material.MaterialCode);
        //    }
        //    catch (Exception ex)
        //    {
        //        MsgBox.Err(ex.Message);
        //    }
        //}

        private void gridView1_RowDoubleClick(object sender, EventArgs e)
        {
            ShowEditMaterial();
        }

        private void OnGridMaterialFocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //MaterialEntity m = SelectedMaterialRow;
            //if (m != null)
            //    LoadRelationSupplier(m.MaterialCode);
        }

        #region 分页事件
        private void labDump_Click(object sender, EventArgs e)
        {
            if (this.textBoxDump.Text != null && textBoxDump.Text != "")
            {
                if (Int32.TryParse(textBoxDump.Text, out _pageCurrent))
                {
                    if (_pageCurrent > PageCount || _pageCurrent < 1)
                    {
                        string err = "跳转的页数请在1-" + PageCount + "数据之间";
                        MsgBox.Warn(err);
                        return;
                    }

                    Query(2);
                }
                else
                {
                    MsgBox.Warn("输入数字格式错误！");
                }
            }
        }

        private void labFirst_Click(object sender, EventArgs e)
        {
            PageCurrent = 1;
            Query(1);
        }

        private void labPre_Click(object sender, EventArgs e)
        {
            PageCurrent -= 1;
            if (PageCurrent <= 0)
                PageCurrent = 1;

            Query(2);
        }

        private void labNext_Click(object sender, EventArgs e)
        {
            PageCurrent += 1;
            if (PageCurrent > PageCount)
                PageCurrent = PageCount;

            Query(2);
        }

        private void labLast_Click(object sender, EventArgs e)
        {
            PageCurrent = PageCount;

            Query(3);
        }

        private void textBoxDump_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.textBoxDump.Text != null && textBoxDump.Text != "")
                {
                    if (Int32.TryParse(textBoxDump.Text, out _pageCurrent))
                    {
                        if (_pageCurrent > PageCount || _pageCurrent < 1)
                        {
                            string err = "跳转的页数请在1-" + PageCount + "数据之间";
                            MsgBox.Warn(err);
                            return;
                        }

                        Query(2);
                    }
                    else
                    {
                        MsgBox.Warn("输入数字格式错误！");
                    }
                }
            }
        }

        #endregion
    }
}