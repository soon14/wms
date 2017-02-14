using System;
using System.Windows.Forms;
using DevExpress.Utils;
using Nodes.UI;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Resources;
using Nodes.Utils;
using DevExpress.XtraTreeList.Nodes;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.BaseData;
using Nodes.Entities.HttpEntity.Stock;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nodes.BaseData
{
    public partial class FrmAreaManager : DevExpress.XtraEditors.XtraForm
    {
        //private AreaDal dal = null;

        #region 私有变量

        private string mLocation = string.Empty;
        private string mSkuName = string.Empty;
        private bool isQueryPage = false;

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

        private int _pageCurrent =1;
        /// <summary>
        /// 当前页号
        /// </summary>
        public int PageCurrent
        {
            get { return _pageCurrent; }
            set { _pageCurrent = value; }
        }

        #endregion

        public FrmAreaManager()
        {
            InitializeComponent();
        }

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
        public List<AreaEntity> Query(int isPage = 0)
        {
            //查询从pageCur*_pageSize行到
            List<AreaEntity> data = new List<AreaEntity>();

            try
            { 
                int total;
                switch (isPage)//从（PageCurrent-1）*_pageSize行 到 （PageCurrent-1）*_pageSize + _pageSize 行{ 首页，前_pageSize行，最后也页总记录数整除_pageSize的行数}
                {
                    case 1: //首页，前_pageSize行
                        data = GetAreaAll(_pageSize, 0, out total);
                        break;
                    case 2://从PageCurrent*_pageSize行
                        data = GetAreaAll(_pageSize, (PageCurrent -1) * _pageSize, out total);
                        break;
                    case 3://最后也页总记录数整除_pageSize的行数
                        data = GetAreaAll(_pageSize, (PageCount - 1) * _pageSize, out total);
                        break;
                    case 4://调转页
                        data = GetAreaAll(_pageSize, (PageCurrent - 1) * _pageSize, out total);
                        break;
                    default://重新选择查询条件
                        data = GetAreaAll(_pageSize, 0, out total);
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
            barButtonItem1.ImageIndex = (int)AppResource.EIcons.design;
            barButtonItem2.ImageIndex = (int)AppResource.EIcons.tree;

            //this.dal = new AreaDal();
            ReLoad();
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
                    DoCreate();
                    break;
                case "修改":
                    ShowEditRow();
                    break;
                case "删除":
                    DoDeleteSelected();
                    break;
                case "收缩":
                    treeList1.CollapseAll();
                    break;
                case "展开":
                    treeList1.ExpandAll();
                    break;
            }
        }

        /// <summary>
        /// 获得选中数据
        /// </summary>
        AreaEntity SelectedRow
        {
            get
            {
                if (treeList1.FocusedNode == null)
                    return null;

                return treeList1.GetDataRecordByNode(treeList1.FocusedNode) as AreaEntity;
            }
        }

        ///<summary>
        ///查询所有
        ///</summary>
        ///<returns></returns>
        public List<AreaEntity> GetAreaAll(int nums, int begin, out int total)
        {
            List<AreaEntity> list = new List<AreaEntity>();
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
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetAreaAll);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetAreaAll bill = JsonConvert.DeserializeObject<JsonGetAreaAll>(jsonQuery);
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
                foreach (JsonGetAreaAllResult jbr in bill.result)
                {
                    AreaEntity asnEntity = new AreaEntity();
                    #region 11-16
                    asnEntity.Code = jbr.arCode;
                    asnEntity.Name = jbr.arName;
                    asnEntity.ID = Convert.ToInt32(jbr.id);
                    asnEntity.ParentID = Convert.ToInt32(jbr.parentId);
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

        /// <summary>
        /// 刷新
        /// </summary>
        private void ReLoad()
        {
            //bindingSource1.DataSource = 
            Query();
            treeList1.ExpandAll();
        }

        /// <summary>
        /// 新增
        /// </summary>
        private void DoCreate()
        {
            AreaEntity assetCategoriesEntity = SelectedRow;
            if (assetCategoriesEntity == null)
            {
                MsgBox.Warn("请选中一个分类节点，作为上级分类。");
                return;
            }

            FrmAreaEdit frmEdit = new FrmAreaEdit(assetCategoriesEntity, true);
            frmEdit.DataSourceChanged += OnEditChange;
            frmEdit.ShowDialog();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        private void ShowEditRow()
        {
            //AreaEntity assetCategoriesEntity = SelectedRow;
            //if (assetCategoriesEntity == null)
            //{
            //    MsgBox.Warn("请选中要修改的行。");
            //    return;
            //}

            //FrmAreaEdit frmEdit = new FrmAreaEdit(assetCategoriesEntity, false);
            //frmEdit.DataSourceChanged += OnEditChange;
            //frmEdit.ShowDialog();
        }

        private void OnEditChange(object sender, EventArgs e)
        {
            ReLoad();
        }

        /// <summary>
        /// 删除
        /// </summary>
        private void DoDeleteSelected()
        {
            #region 功能禁用
            //AreaEntity removeEntity = SelectedRow;
            //if (removeEntity == null)
            //{
            //    MsgBox.Warn("请选中要删除的行。");
            //    return;
            //}

            ////不允许删除根
            //if (treeList1.FocusedNode.ParentNode == null)
            //{
            //    MsgBox.Warn("根节点不允许删除。");
            //    return;
            //}

            //if (treeList1.FocusedNode.HasChildren == true)
            //{
            //    if (MsgBox.AskOK("该分类含有下级节点，将会级联删除下级节点，确定要删除吗？") != DialogResult.OK)
            //        return;
            //}
            //else if (MsgBox.AskOK(string.Format("确定要删除{0}吗？", removeEntity.Name)) != DialogResult.OK)
            //    return;

            //try
            //{
            //    //int result = this.dal.Delete(removeEntity.ID);
            //    //if (result == 0)
            //    //    ReLoad();
            //    //else if (result == -1)
            //    //    MsgBox.Warn("有资产数据在引用，无法删除。");
            //}
            //catch (Exception ex)
            //{
            //    MsgBox.Err(ex.Message);
            //}
            #endregion
        }

        private void OnTreelistDoubleClick(object sender, EventArgs e)
        {
            ShowEditRow();
        }

        private void txtFilter_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            txtFilter.Text = string.Empty;
        }

        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DoSearch();
            }
        }

        private void DoSearch()
        {
            if (string.IsNullOrEmpty(txtFilter.Text.Trim()))
                return;

            foreach (TreeListNode n in treeList1.Nodes)
            {
                TreeListNode node = TreeListUtil.SearchNode(n, txtFilter.Text.Trim(), "DisplayName");
                if (node != null)
                    treeList1.FocusedNode = node;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DoSearch();
        }

        #region 分页事件
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