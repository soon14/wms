using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Shares;
using Nodes.UI;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Instore;
using Newtonsoft.Json;

namespace Nodes.Instore
{
    public partial class UcAsnBody : UserControl
    {
        AsnQueryDal asnQueryDal = null;

        /// <summary>
        /// 选中行变化事件
        /// </summary>
        /// <param name="focusedHeader"></param>
        public delegate void FocusedRowChanged(AsnBodyEntity focusedHeader);
        public event FocusedRowChanged FocusedHandlerChanged;

        public UcAsnBody()
        {
            InitializeComponent();
            asnQueryDal = new AsnQueryDal();
        }

        #region 返回焦点行的三种形式供外边调用：单选，多选，多选的单据编号
        public AsnBodyEntity FocusedHeader
        {
            get
            {
                if (gvHeader.FocusedRowHandle < 0)
                    return null;
                else
                    return gvHeader.GetFocusedRow() as AsnBodyEntity;
            }
        }

        public int FocusedRowCount
        {
            get
            {
                return gvHeader.GetSelectedRows().Length;
            }
        }

        public List<AsnBodyEntity> FocusedHeaders
        {
            get
            {
                if (FocusedRowCount == 0)
                    return null;

                List<AsnBodyEntity> focusedHeaders = new List<AsnBodyEntity>();
                int[] focusedHandles = gvHeader.GetSelectedRows();
                foreach (int handle in focusedHandles)
                {
                    if (handle >= 0)
                        focusedHeaders.Add(gvHeader.GetRow(handle) as AsnBodyEntity);
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
            AsnBodyEntity header = gvHeader.GetRow(e.RowHandle) as AsnBodyEntity;
            if (header != null && header.RowForeColor != null)
                e.Appearance.ForeColor = Color.FromArgb(header.RowForeColor.Value);
        }

        /// <summary>
        /// 收货单据管理， 查询入库单明细
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public List<PODetailEntity> GetDetailByBillID(int billID)
        {
            List<PODetailEntity> list = new List<PODetailEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetDetailByBillID);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetDetailByBillID bill = JsonConvert.DeserializeObject<JsonGetDetailByBillID>(jsonQuery);
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
                foreach (JsonGetDetailByBillIDResult jbr in bill.result)
                {
                    PODetailEntity asnEntity = new PODetailEntity();
                    asnEntity.Barcode1 = jbr.barCode1;
                    asnEntity.BatchNO = jbr.batchNo;
                    asnEntity.BillID = Convert.ToInt32(jbr.billId);
                    asnEntity.DetailID = Convert.ToInt32(jbr.id);
                    asnEntity.ExpDate = jbr.expDate;
                    //asnEntity.MaterialName = jbr.namePy;
                    asnEntity.MaterialName = jbr.skuName;
                    asnEntity.MaterialNameS = jbr.skuNameS;
                    asnEntity.MaterialCode = jbr.skuCode;
                    asnEntity.PlanQty = Convert.ToInt32(jbr.qty);
                    asnEntity.Price = jbr.price;
                    asnEntity.PutQty = Convert.ToInt32(jbr.putQty);
                    asnEntity.Remark = jbr.remark;
                    asnEntity.Spec = jbr.spec;
                    asnEntity.UnitName = jbr.umName;                  
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

        public void BindingDetail()
        {
            AsnBodyEntity header = FocusedHeader;
            if (header == null)
            {
                gdDetails.DataSource = null;
                gvDetails.ViewCaption = "未选择单据";
            }
            else
            {
                if (header.Details == null)
                    header.Details = GetDetailByBillID(header.BillID);

                gdDetails.DataSource = header.Details;
                gvDetails.ViewCaption = string.Format("明细-{0}|{1}", header.BillNO, header.SupplierName);
            }
        }

        //public void CustomGridCaption()
        //{
        //    CustomFields.AppendLotExpFields(gvDetails, false);
        //    CustomFields.AppendMaterialFields(gvDetails);
        //}

        public void RemoveColumn(string fieldName)
        {
            GridColumn col = gvDetails.Columns.ColumnByFieldName(fieldName);
            if (col != null)
                gvDetails.Columns.Remove(col);
        }
    }
}
