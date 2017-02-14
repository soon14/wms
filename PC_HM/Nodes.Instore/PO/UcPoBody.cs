using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Shares;
using DevExpress.XtraGrid.Columns;

namespace Nodes.Instore
{
    public partial class UcPoBody : UserControl
    {
        POQueryDal poQueryDal = null;

        /// <summary>
        /// 选中行变化事件
        /// </summary>
        /// <param name="focusedHeader"></param>
        public delegate void FocusedRowChanged(POBodyEntity focusedHeader);
        public event FocusedRowChanged FocusedHandlerChanged;

        public UcPoBody()
        {
            InitializeComponent();
            poQueryDal = new POQueryDal();
        }

        #region 返回焦点行的三种形式供外边调用：单选，多选，多选的单据编号
        public POBodyEntity FocusedHeader
        {
            get
            {
                if (gvHeader.FocusedRowHandle < 0)
                    return null;
                else
                    return gvHeader.GetFocusedRow() as POBodyEntity;
            }
        }

        public int FocusedRowCount
        {
            get
            {
                return gvHeader.GetSelectedRows().Length;
            }
        }

        public List<POBodyEntity> FocusedHeaders
        {
            get
            {
                if (FocusedRowCount == 0)
                    return null;

                List<POBodyEntity> focusedHeaders = new List<POBodyEntity>();
                int[] focusedHandles = gvHeader.GetSelectedRows();
                foreach (int handle in focusedHandles)
                {
                    if (handle >= 0)
                        focusedHeaders.Add(gvHeader.GetRow(handle) as POBodyEntity);
                }

                return focusedHeaders;
            }
        }

        #endregion

        #region 绑定数据源
        public object DataSource
        {
            set
            {
                int lastFocusedRowHandle = gvHeader.FocusedRowHandle;
                bindingSource1.DataSource = value;

                //重新置焦点
                if (gvHeader.IsMultiSelect)
                    gvHeader.SelectRow(gvHeader.FocusedRowHandle);

                //若上次的焦点行与重新绑定数据源后的焦点行相同，则重新加载明细
                if (lastFocusedRowHandle == gvHeader.FocusedRowHandle)
                    BindingDetail();
            }
            get
            {
                return bindingSource1.DataSource;
            }
        }
        #endregion

        public void ShowCondition(string condition)
        {
            lblCondition.Text = condition;
        }

        public void ShowTimeMsg(string msg)
        {
            lblMsg.Show(msg);
        }

        public void RefreshMeMemory()
        {
            bindingSource1.ResetBindings(false);
        }

        private void OnHeaderFocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            BindingDetail();

            if (FocusedHandlerChanged != null)
                FocusedHandlerChanged(FocusedHeader);
        }

        private void OnHeaderRowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            POBodyEntity header = gvHeader.GetRow(e.RowHandle) as POBodyEntity;
            if (header != null && header.RowForeColor != null)
                e.Appearance.ForeColor = Color.FromArgb(header.RowForeColor.Value);
        }

        public void BindingDetail()
        {
            POBodyEntity header = FocusedHeader;
            if (header == null)
            {
                gdDetails.DataSource = null;
                gvDetails.ViewCaption = "未选择单据";
            }
            else
            {
                if (header.Details == null)
                    header.Details = poQueryDal.GetDetailByBillID(header.BillID);

                gdDetails.DataSource = header.Details;
                gvDetails.ViewCaption = string.Format("明细-{0}", header.BillID);
            }
        }

        public void CustomGridCaption()
        {
            CustomFields.AppendMaterialFields(gvDetails);
        }

        public void RemoveColumn(string fieldName)
        {
            GridColumn col = gvDetails.Columns.ColumnByFieldName(fieldName);
            if (col != null)
                gvDetails.Columns.Remove(col);
        }
    }
}
