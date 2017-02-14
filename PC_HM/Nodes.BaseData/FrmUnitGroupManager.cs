using System;
using System.Collections.Generic;
using System.Windows.Forms;
//using Nodes.DBHelper;
using DevExpress.Utils;
using Nodes.Entities;
using Nodes.Resources;
using Nodes.Shares;
using Nodes.Utils;
using Nodes.UI;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.BaseData;
using Newtonsoft.Json;

namespace Nodes.BaseData
{
    public partial class FrmUnitGroupManager : DevExpress.XtraEditors.XtraForm
    {
        //private UnitGroupDal unitGrpDal = null;
        public FrmUnitGroupManager()
        {
            InitializeComponent();
        }

        #region 私有变量

        private string mLocation = string.Empty;
        private string mSkuName = string.Empty;

        /// <summary>
        /// 每页显示记录数
        /// </summary>
        private int _pageSize = 200;

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
        public List<UnitGroupEntity> Query(int isPage = 0)
        {
            //查询从pageCur*_pageSize行到
            List<UnitGroupEntity> data = new List<UnitGroupEntity>();
            try
            {
                int total;
                switch (isPage)//从（PageCurrent-1）*_pageSize行 到 （PageCurrent-1）*_pageSize + _pageSize 行{ 首页，前_pageSize行，最后也页总记录数整除_pageSize的行数}
                {
                    case 1: //首页，前_pageSize行
                        data = GetAllZJQ(_pageSize, 0, out total);
                        break;
                    case 2://从PageCurrent*_pageSize行
                        data = GetAllZJQ(_pageSize, (PageCurrent - 1) * _pageSize, out total);
                        break;
                    case 3://最后也页总记录数整除_pageSize的行数
                        data = GetAllZJQ(_pageSize, (PageCount - 1) * _pageSize, out total);
                        break;
                    case 4://调转页
                        data = GetAllZJQ(_pageSize, (PageCurrent - 1) * _pageSize, out total);
                        break;
                    default://重新选择查询条件
                        data = GetAllZJQ(_pageSize, 0, out total);
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

        private void OnFormLoad(object sender, EventArgs e)
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

            //unitGrpDal = new UnitGroupDal();
            LoadDataAndBindGrid();
        }

        /// <summary>
        /// string转换decimal 得到2位小数点 
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private decimal GetTwoDecimal(string num)
        {
            string ret = num;
            if (ret.Contains("."))
                ret = ret.Insert(ret.Length, "00");
            else
                ret = ret.Insert(ret.Length, ".00");
            return Math.Round(Convert.ToDecimal(ret), 2);
        }

        ///<summary>
        ///查询所有计量单位组
        ///</summary>
        ///<returns></returns>
        public List<UnitGroupEntity> GetAllZJQ(int nums, int begin, out int total)
        {
            List<UnitGroupEntity> list = new List<UnitGroupEntity>();
            total = 0;
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                if (begin == 0)
                    loStr.Append("beginRow=").Append("&");
                else
                    loStr.Append("beginRow=").Append(begin).Append("&");
                loStr.Append("warehouseType=").Append(EUtilStroreType.WarehouseTypeToInt(GlobeSettings.LoginedUser.WarehouseType)).Append("&");
                if (begin == 0)
                    loStr.Append("rows=");
                else
                    loStr.Append("rows=").Append(nums);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetAllZJQ);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetAllZJQ bill = JsonConvert.DeserializeObject<JsonGetAllZJQ>(jsonQuery);
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
                foreach (JsonGetAllZJQResult jbr in bill.result)
                {
                    UnitGroupEntity asnEntity = new UnitGroupEntity();
                    #region 0-10
                    asnEntity.Height = StringToDecimal.GetTwoDecimal(jbr.height);
                    asnEntity.ID = Convert.ToInt32(jbr.id);
                    asnEntity.IsActive = jbr.isActive;
                    asnEntity.Length = StringToDecimal.GetTwoDecimal(jbr.length);
                    asnEntity.Qty = Convert.ToInt32(jbr.qty);
                    asnEntity.SkuBarcode = jbr.skuBarcode;
                    asnEntity.SkuCode = jbr.skuCode;
                    asnEntity.SkuLevel = Convert.ToInt32(jbr.skuLevel);
                    asnEntity.SkuName = jbr.skuName;
                    asnEntity.Spec = jbr.spec;
                    #endregion

                    #region 11-15
                    asnEntity.UnitCode = jbr.umCode;
                    asnEntity.UnitName = jbr.umName;
                    asnEntity.Weight = StringToDecimal.GetTwoDecimal(jbr.weight);
                    asnEntity.Width = StringToDecimal.GetTwoDecimal(jbr.width);
                    if (GlobeSettings.LoginedUser.WarehouseType == EWarehouseType.散货仓)
                    {
                        asnEntity.Skuvol = StringToDecimal.GetTwoDecimal(jbr.skuVol);
                    }
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
            WaitDialogForm wdf = null;
            try
            {
                using (wdf = new WaitDialogForm("请稍候...", "正在获取包装关系..."))
                {
                    Query();//bindingSource1.DataSource = GetAllZJQ();
                }
            }
            catch (Exception ex)
            {
                if (wdf != null)
                {
                    wdf.Close();
                    wdf.Dispose();
                }
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
                case "添加关联行":
                    UnitGroupEntity selectedHeaderRow = SelectedHeaderRow;
                    if (selectedHeaderRow == null)
                    {
                        MsgBox.Warn("请选中一行包装关系。");
                    }
                    else
                    {
                        using (FrmUnitGroupItem frmAddItem = new FrmUnitGroupItem(selectedHeaderRow))
                        {
                            frmAddItem.DataSourceChanged += new EventHandler(frmAddItem_DataSourceChanged);
                            frmAddItem.ShowDialog();
                        }
                    }
                    break;
                case "修改":
                    ShowEdit();
                    break;
                case "删除":
                    DoDeleteSelectedItem();
                    break;
                case "快速查找":
                    if (gridView1.IsFindPanelVisible)
                        gridView1.HideFindPanel();
                    else
                        gridView1.ShowFindPanel();
                    break;
            }
        }

        void frmAddItem_DataSourceChanged(object sender, EventArgs e)
        {
            ShowUnitItems();
        }

        /// <summary>
        /// 获得选中数据
        /// </summary>
        UnitGroupEntity SelectedHeaderRow
        {
            get
            {
                if (gridView1.FocusedRowHandle < 0) return null;

                return gridView1.GetRow(gridView1.FocusedRowHandle) as UnitGroupEntity;
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        private void ReLoad()
        {
            LoadDataAndBindGrid();
            ShowUnitItems();
        }

        /// <summary>
        /// 新增
        /// </summary>
        private void DoCreate()
        {
            FrmUnitGroupEdit frmUnitGroupEdit = new FrmUnitGroupEdit();
            frmUnitGroupEdit.DataSourceChanged += OnEditChanage;
            frmUnitGroupEdit.ShowDialog();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        private void ShowEdit()
        {
            UnitGroupEntity editEntity = SelectedHeaderRow;
            if (editEntity == null)
            {
                MsgBox.Warn("没有要修改的数据。");
                return;
            }

            FrmUnitGroupEdit frmUnitGroupEdit = new FrmUnitGroupEdit(editEntity);
            frmUnitGroupEdit.DataSourceChanged += OnEditChanage;
            frmUnitGroupEdit.ShowDialog();
        }

        private void OnEditChanage(object sender, EventArgs e)
        {
            ReLoad();
        }

        /// <summary>
        /// 删除
        /// </summary>
        private void DoDeleteSelectedItem()
        {
            UnitGroupEntity removeEntity = SelectedHeaderRow;
            if (removeEntity == null)
            {
                MsgBox.Warn("没有要删除的数据。");
                return;
            }

            if (MsgBox.AskOK(string.Format("确定要删除包装关系行{0}吗？", removeEntity.SkuName)) == DialogResult.OK)
            {
                //int ret = unitGrpDal.Delete(removeEntity.ID);
                //if (ret >= 1)
                //{
                //    bindingSource1.Remove(removeEntity);
                //}
                //else
                //    MsgBox.Warn("删除失败，该包装关系行正在使用中。");
            }
        }

        private void gridView1_RowDoubleClick(object sender, EventArgs e)
        {
            ShowEdit();//修改
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ShowUnitItems();
        }

        private void ShowUnitItems()
        {
            //UnitGroupEntity selectedRow = SelectedHeaderRow;
            //if (selectedRow == null)
            //    gridControl2.DataSource = null;
            //else
            //    gridControl2.DataSource = unitGrpDal.GetItemsByGrpCode(selectedRow.GrpCode);
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