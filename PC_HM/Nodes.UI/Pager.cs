using System;
using System.Windows.Forms;

namespace Nodes.UI
{
    /// <summary>
    /// 申明委托
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    public delegate void EventPagingHandler(EventPagingArg e);

    /// <summary>
    /// 分页控件呈现
    /// </summary>
    public partial class Pager : UserControl
    {
        public event EventPagingHandler EventPaging;
        private int pageIndex = 1;
        private int pageSize = 100;
        public int totalRowCount = 0;

        public Pager()
        {
            InitializeComponent();
            toolStripComboBox1.SelectedIndex = 0;
        }

        /// <summary>
        /// 每页显示记录数
        /// </summary>
        public int PageSize
        {
            get
            {               
                return pageSize;
            }
            set
            {
                if (pageSize != value)
                {
                    pageSize = value;
                    pageIndex = 1;

                    Bind();
                }
            }
        }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalRowCount
        {
            get { return totalRowCount; }
            set{
                totalRowCount = value;
                Bind();
            }
        }

        /// <summary>
        /// 页数=总记录数/每页显示记录数
        /// </summary>
        public int PageCount
        {
            get
            {
                if (PageSize == -1)
                    return 1;

                if (TotalRowCount > 0)
                    return Convert.ToInt32(Math.Ceiling(Convert.ToDouble(this.TotalRowCount) / Convert.ToDouble(this.PageSize)));
                else
                    return 1;
            }
        }

        /// <summary>
        /// 当前页号
        /// </summary>
        public int PageCurrent
        {
            get { return pageIndex; }
            set
            {
                if (value != pageIndex)
                {
                    txtCurrentPage.Text = value.ToString();
                    pageIndex = value;

                    this.Bind();
                }
            }
        }

        /// <summary>
        /// 翻页控件数据绑定的方法
        /// </summary>
        private void Bind()
        {
            this.lblMaxPage.Text = string.Format("共 {0} 条记录", this.TotalRowCount);
            lblPageCount.Text = this.PageCount.ToString();
            this.txtCurrentPage.Text = this.PageCurrent.ToString();

            if (this.PageCurrent == 1)
            {
                this.btnPrev.Enabled = false;
                this.btnFirst.Enabled = false;
            }
            else
            {
                btnPrev.Enabled = true;
                btnFirst.Enabled = true;
            }

            if (this.PageCurrent == this.PageCount)
            {
                this.btnLast.Enabled = false;
                this.btnNext.Enabled = false;
            }
            else
            {
                btnLast.Enabled = true;
                btnNext.Enabled = true;
            }

            if (this.TotalRowCount == 0)
            {
                btnNext.Enabled = false;
                btnLast.Enabled = false;
                btnFirst.Enabled = false;
                btnPrev.Enabled = false;
            }

            if (EventPaging != null)
                EventPaging(new EventPagingArg(PageCurrent));
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            PageCurrent = 1;
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (PageCurrent <= 2)
                PageCurrent = 1;
            else
                PageCurrent -= 1;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (PageCurrent >= PageCount)
                PageCurrent = PageCount;
            else
                PageCurrent += 1;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            PageCurrent = PageCount;
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            ShowCurrentPage();
        }

        private void ShowCurrentPage()
        {
            string strPageIndex = this.txtCurrentPage.Text.Trim();
            if (!string.IsNullOrEmpty(strPageIndex))
            {
                int currPageIndex = 1;
                bool success = Int32.TryParse(txtCurrentPage.Text, out currPageIndex);
                if (success && currPageIndex >= 1 && currPageIndex <= PageCount)
                {
                    PageCurrent = currPageIndex;
                }
                else
                {
                    MessageBox.Show(string.Format("请输入1和{0}之间的整数。", PageCount));
                }
            }
        }

        private void txtCurrentPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ShowCurrentPage();
            }
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBox1.SelectedIndex == 0)
                PageSize = 50;
            else if (toolStripComboBox1.SelectedIndex == 1)
                PageSize = 100;
            else
                PageSize = -1;
        }
    }

    /// <summary>
    /// 自定义事件数据基类
    /// </summary>
    public class EventPagingArg : EventArgs
    {
        private int _intPageIndex;
        public EventPagingArg(int PageIndex)
        {
            _intPageIndex = PageIndex;
        }
    }
}
