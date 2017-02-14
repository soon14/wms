using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.Utils;
using Nodes.UI;
//using Nodes.DBHelper;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Reports;
using Newtonsoft.Json;

namespace Nodes.Reports
{
    public partial class FrmSaleSort : DevExpress.XtraEditors.XtraForm
    {
       // private SOQueryDal soDal = new SOQueryDal();
        public FrmSaleSort()
        {
            InitializeComponent();
        }

        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DoClickEvent(ConvertUtil.ToString(e.Item.Tag));
        }

        private void DoClickEvent(string tag)
        {
            switch (tag)
            {
                case "刷新":
                    LoadData();
                    break;
                case "查询":
                    LoadData();
                   
                    break;
            }
        }

        /// <summary>
        /// 查询统计（商品销量统计）
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public DataTable GetSKUSaleSort(string dateStart, string dateEnd)
        {
            #region
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("SKU_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("SKU_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("QTY", Type.GetType("System.Decimal"));
            tblDatas.Columns.Add("SPEC", Type.GetType("System.String"));
            tblDatas.Columns.Add("UM_NAME", Type.GetType("System.String"));
            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("startDate=").Append(dateStart).Append("&");
                loStr.Append("endDate=").Append(dateEnd);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetSKUSaleSort);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetSKUSaleSort bill = JsonConvert.DeserializeObject<JsonGetSKUSaleSort>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return tblDatas;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return tblDatas;
                }
                #endregion
                
                #region 赋值
                foreach (JsonGetSKUSaleSortResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["SKU_CODE"] = tm.skuCode;
                    newRow["SKU_NAME"] = tm.skuName;
                    newRow["QTY"] = Convert.ToDecimal(tm.qty);
                    newRow["SPEC"] = tm.spec;
                    newRow["UM_NAME"] = tm.umName;
                    tblDatas.Rows.Add(newRow);
                }
                return tblDatas;
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return tblDatas;
        }

        private void LoadData()
        {
            try
            {
                gridView1.ViewCaption = String.Format("{0} 至 {1} 商品销售排行", ConvertUtil.ToString(dateStart.EditValue == null ? dateStart.EditValue : ConvertUtil.ToDatetime(dateStart.EditValue.ToString()).ToString("yyyy-MM-dd")),
                        ConvertUtil.ToString(dateEnd.EditValue == null ? dateEnd.EditValue : ConvertUtil.ToDatetime(dateEnd.EditValue.ToString()).ToString("yyyy-MM-dd")));

                DataTable dtResult = GetSKUSaleSort(ConvertUtil.ToString(dateStart.EditValue == null ? dateStart.EditValue : ConvertUtil.ToDatetime(dateStart.EditValue.ToString())),
                    ConvertUtil.ToString(dateEnd.EditValue == null ? dateEnd.EditValue : ConvertUtil.ToDatetime(dateEnd.EditValue.ToString())));
                gridControl1.DataSource = dtResult;
                gridControl1.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void FrmLoad(object sender, EventArgs e)
        {
            try
            {
                dateStart.EditValue = DateTime.Now.AddDays(-7);
                dateEnd.EditValue = DateTime.Now;

                LoadData();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void gridView1_RowDoubleClick(object sender, EventArgs e)
        {
            try
            {
                string skuCode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SKU_CODE").ToString();
                if (skuCode == "")
                {
                    return;
                }
                //FrmSKULocation frmSKULocation = new FrmSKULocation(skuCode);
                //frmSKULocation.ShowDialog();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
    }
}