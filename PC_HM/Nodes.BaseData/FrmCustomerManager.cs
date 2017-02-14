using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.Utils;
using Nodes.UI;
using Nodes.Utils;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Resources;
using Nodes.Shares;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.BaseData;
using Newtonsoft.Json;

namespace Nodes.BaseData
{
    public partial class FrmCustomerManager : DevExpress.XtraEditors.XtraForm
    {
        private CustomerDal customerDal = null;
        public FrmCustomerManager()
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
        public List<CustomerEntity> Query(int isPage = 0)
        {
            //查询从pageCur*_pageSize行到
            List<CustomerEntity> data = new List<CustomerEntity>();
            try
            {
                int total;
                switch (isPage)//从（PageCurrent-1）*_pageSize行 到 （PageCurrent-1）*_pageSize + _pageSize 行{ 首页，前_pageSize行，最后也页总记录数整除_pageSize的行数}
                {
                    case 1: //首页，前_pageSize行
                        data = GetAllCustomer(_pageSize, 0, out total);
                        break;
                    case 2://从PageCurrent*_pageSize行
                        data = GetAllCustomer(_pageSize, (PageCurrent - 1) * _pageSize, out total);
                        break;
                    case 3://最后也页总记录数整除_pageSize的行数
                        data = GetAllCustomer(_pageSize, (PageCount - 1) * _pageSize, out total);
                        break;
                    case 4://调转页
                        data = GetAllCustomer(_pageSize, (PageCurrent - 1) * _pageSize, out total);
                        break;
                    default://重新选择查询条件
                        data = GetAllCustomer(_pageSize, 0, out total);
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
            toolRefresh.ImageIndex = (int)AppResource.EIcons.refresh;
            toolSearch.ImageIndex = (int)AppResource.EIcons.search;
            toolAdd.ImageIndex = (int)AppResource.EIcons.add;
            toolEdit.ImageIndex = (int)AppResource.EIcons.edit;
            toolDel.ImageIndex = (int)AppResource.EIcons.delete;

            customerDal = new CustomerDal();
            LoadDataAndBindGrid();
        }

        ///<summary>
        ///查询所有客户及默认地址
        ///</summary>
        ///<returns></returns>
        public List<CustomerEntity> GetAllCustomer(int nums, int begin, out int total)
        {
            List<CustomerEntity> list = new List<CustomerEntity>();
            total = 0;
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("whCode=").Append(GlobeSettings.LoginedUser.WarehouseCode).Append("&");
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
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetAllCustomer);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                GetAllCustomer bill = JsonConvert.DeserializeObject<GetAllCustomer>(jsonQuery);
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
                foreach (GetAllCustomerResult jbr in bill.result)
                {
                    CustomerEntity asnEntity = new CustomerEntity();
                    #region 0-10
                    asnEntity.Address = jbr.address;
                    asnEntity.AreaID = jbr.areaId;
                    asnEntity.AreaName = jbr.arName;
                    asnEntity.CustomerCode = jbr.cCode;
                    asnEntity.CustomerName = jbr.cName;
                    asnEntity.Contact = jbr.contact;
                    asnEntity.Distance = Convert.ToDecimal(jbr.distance);
                    asnEntity.IsActive = jbr.isActive;
                    asnEntity.isOwn = jbr.isOwn;
                    asnEntity.CustomerNameS = jbr.nameS;
                    #endregion

                    #region 11-20
                    asnEntity.Phone = jbr.phone;
                    asnEntity.PostCode = jbr.postcode;
                    asnEntity.Remark = jbr.remark;
                    asnEntity.RouteCode = jbr.rtCode;
                    asnEntity.RouteName = jbr.rtName;
                    asnEntity.SortOrder = Convert.ToInt32(jbr.sortOrder);
                    asnEntity.UpdateBy = jbr.updateBy;
                    asnEntity.WHCode = jbr.whCode;
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.updateDate))
                            asnEntity.UpdateDate = Convert.ToDateTime(jbr.updateDate);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmListPickPlan+GetPickPlan", msg);
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
            try
            {
                Query();//bindingSource1.DataSource = GetAllCustomer(GlobeSettings.LoginedUser.WarehouseCode);
            
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
                    using (WaitDialogForm frm = new WaitDialogForm("查询中...", "请稍等"))
                    {
                        ReLoad();
                    }
                    break;
                case "新增":
                    DoCreateCustomer();
                    break;
                case "修改":
                    ShowEditCustomer();
                    break;
                case "删除":
                    DoDeleteSelectedCustomer();
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
        CustomerEntity SelectedCustomerRow
        {
            get
            {
                if (gridView1.FocusedRowHandle < 0) return null;

                return gridView1.GetRow(gridView1.FocusedRowHandle) as CustomerEntity;
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
        private void DoCreateCustomer()
        {
            //FrmCustomerEdit frmCustomerEdit = new FrmCustomerEdit();
            //frmCustomerEdit.DataSourceChanged += OnCreateChanage;
            //frmCustomerEdit.ShowDialog();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        private void ShowEditCustomer()
        {
            //CustomerEntity editEntity = SelectedCustomerRow;
            //if (editEntity == null)
            //{
            //    MsgBox.Warn("没有要修改的数据。");
            //    return;
            //}

            //FrmCustomerEdit frmCustomerEdit = new FrmCustomerEdit(editEntity);
            //frmCustomerEdit.DataSourceChanged += OnEditChanage;
            //frmCustomerEdit.ShowDialog();
        }

        private void OnCreateChanage(object sender, EventArgs e)
        {
            CustomerEntity newEntity = (CustomerEntity)sender;
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
        private void DoDeleteSelectedCustomer()
        {
            CustomerEntity removeEntity = SelectedCustomerRow;
            if (removeEntity == null)
            {
                MsgBox.Warn("没有要删除的数据。");
                return;
            }

            if (MsgBox.AskOK(string.Format("确定要删除客户“{0}({1})”吗？", removeEntity.CustomerName, removeEntity.CustomerCode)) == DialogResult.OK)
            {
                bool ret = customerDal.DeleteCustomer(removeEntity.CustomerCode);
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
            ShowEditCustomer();
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