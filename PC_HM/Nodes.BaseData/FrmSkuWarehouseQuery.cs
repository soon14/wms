using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.Utils;
using Nodes.UI;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Resources;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.BaseData;
using Nodes.Entities.HttpEntity.Stock;
using Newtonsoft.Json;

namespace Nodes.BaseData
{
    public partial class FrmSkuWarehouseQuery : DevExpress.XtraEditors.XtraForm
    {
        //private SkuWarehouseDal skuWarehouseDal = null;

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
        public List<SkuWarehouseEntity> Query(int isPage = 0)
        {
            //查询从pageCur*_pageSize行到
            List<SkuWarehouseEntity> data = new List<SkuWarehouseEntity>();
            try
            {
                int total;
                switch (isPage)//从（PageCurrent-1）*_pageSize行 到 （PageCurrent-1）*_pageSize + _pageSize 行{ 首页，前_pageSize行，最后也页总记录数整除_pageSize的行数}
                {
                    case 1: //首页，前_pageSize行
                        data = GetAllSkuWarehouse(_pageSize, 0, out total);
                        break;
                    case 2://从PageCurrent*_pageSize行
                        data = GetAllSkuWarehouse(_pageSize, (PageCurrent - 1) * _pageSize, out total);
                        break;
                    case 3://最后也页总记录数整除_pageSize的行数
                        data = GetAllSkuWarehouse(_pageSize, (PageCount - 1) * _pageSize, out total);
                        break;
                    case 4://调转页
                        data = GetAllSkuWarehouse(_pageSize, (PageCurrent - 1) * _pageSize, out total);
                        break;
                    default://重新选择查询条件
                        data = GetAllSkuWarehouse(_pageSize, 0, out total);
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

        public FrmSkuWarehouseQuery()
        {
            InitializeComponent();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            ImageCollection ic = AppResource.LoadToolImages();
            barManager1.Images = ic;
            
            toolRefresh.ImageIndex = (int)AppResource.EIcons.refresh;
            toolSearch.ImageIndex = (int)AppResource.EIcons.search;
            toolModify.ImageIndex = (int)AppResource.EIcons.edit;

            //skuWarehouseDal = new SkuWarehouseDal();
            LoadDataAndBindGrid();
        }

        ///<summary>
        ///查询所有
        ///</summary>
        ///<returns></returns>
        public List<SkuWarehouseEntity> GetAllSkuWarehouse(int nums, int begin, out int total)
        {
            List<SkuWarehouseEntity> list = new List<SkuWarehouseEntity>();
            total = 0;
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("billState=").Append(BillStateConst.ASN_STATE_CODE_COMPLETE).Append("&");
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
                //loStr.Append("wareHouseCode=").Append(warehouseCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetAllSkuWarehouse);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetAllSkuWarehouse bill = JsonConvert.DeserializeObject<JsonGetAllSkuWarehouse>(jsonQuery);
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
                foreach (JsonGetAllSkuWarehouseResult jbr in bill.result)
                {
                    SkuWarehouseEntity asnEntity = new SkuWarehouseEntity();
                    #region 0-10
                    asnEntity.LowerLocation = Convert.ToInt32(jbr.lowerLocation);
                    asnEntity.MaxStockQty = Convert.ToInt32(jbr.maxStockQty);
                    asnEntity.MinStockQty = Convert.ToInt32(jbr.minStockQty);
                    asnEntity.PickType = Convert.ToInt32(jbr.pickType);
                    asnEntity.SecurityQty = Convert.ToInt32(jbr.securityQty);
                    asnEntity.SkuCode = jbr.skuCode;
                    asnEntity.SkuName = jbr.skuName;
                    asnEntity.Spec = jbr.spec;
                    asnEntity.UpperLocation = Convert.ToInt32(jbr.upperLocation);
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

        public void LoadDataAndBindGrid()
        {
            Query();//this.bindingSource1.DataSource = Query();//GetAllSkuWarehouse();
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
                case "修改":
                    ShowEditLocation();
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
        /// 刷新
        /// </summary>
        private void ReLoad()
        {
            LoadDataAndBindGrid();
        }

        /// <summary>
        /// 获得选中数据
        /// </summary>
        SkuWarehouseEntity SelectedLocationRow
        {
            get
            {
                if (gridView1.FocusedRowHandle < 0) return null;

                return gridView1.GetRow(gridView1.FocusedRowHandle) as SkuWarehouseEntity;
            }
        }

        private void ShowEditLocation()
        {
            SkuWarehouseEntity editEntity = SelectedLocationRow;
            if (editEntity == null)
            {
                MsgBox.Warn("没有要修改的数据。");
                return;
            }

            FrmSKUWarehouseEdit frmSKUWarehouseEdit = new FrmSKUWarehouseEdit(editEntity);
            frmSKUWarehouseEdit.DataSourceChanged += OnEditChanage;
            frmSKUWarehouseEdit.ShowDialog();
        }

        private void OnEditChanage(object sender, EventArgs e)
        {
            bindingSource1.ResetBindings(false);
        }

        private void gridView1_RowDoubleClick(object sender, EventArgs e)
        {
            ShowEditLocation();
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

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

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
