using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils;
using Nodes.UI;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Reports;
using Newtonsoft.Json;

namespace Nodes.Reports
{
    public partial class FrmStockAccount : DevExpress.XtraEditors.XtraForm
    {
        public FrmStockAccount()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        public void Reload()
        {
            using (WaitDialogForm waitDialog = new WaitDialogForm("提示", "正在加载数据，请稍后..."))
            {
                this.bindingSource.DataSource = SearchStockAccount(this.txtMaterialCode.Text.Trim(), ConvertUtil.ToString(dateEditFrom.EditValue == null ? dateEditFrom.EditValue : ConvertUtil.ToDatetime(dateEditFrom.EditValue.ToString())),
                        ConvertUtil.ToString(dateEditTo.EditValue == null ? dateEditTo.EditValue : ConvertUtil.ToDatetime(dateEditTo.EditValue.ToString())));
            }
        }

        private void FrmStockAccount_Load(object sender, EventArgs e)
        {
            dateEditFrom.EditValue = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd HH:mm");
            dateEditTo.EditValue = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm");

            Reload();//默认加载头一天的数据
        }

        #region
        /// <summary>
        /// 查询库存对账
        /// </summary>
        /// <param name="skuCode">物料编码</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endTate">结束时间</param>
        /// <param name="startPage">开始页</param>
        /// <param name="pageSize">结束页</param>
        /// <returns></returns>
        public List<JsonSearchStockAccountResult> SearchStockAccount(string skuCode, string startDate, string endTate, int startPage = 0, int pageSize = 0)
        {
            List<JsonSearchStockAccountResult> list = new List<JsonSearchStockAccountResult>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                if (!string.IsNullOrEmpty(skuCode))
                    loStr.Append("skuCode=").Append(skuCode).Append("&");
                loStr.Append("startTime=").Append(startDate).Append("&");
                loStr.Append("endTime=").Append(endTate);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SearchStockAccount);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonSearchStockAccount bill = JsonConvert.DeserializeObject<JsonSearchStockAccount>(jsonQuery);
                if (bill == null)
                {
                    //MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return list;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return list;
                }
                #endregion

                #region 赋值数据
                foreach (JsonSearchStockAccountResult jbr in bill.result)
                {
                    //所有入库
                    int enterAll = Convert.ToInt32(jbr.enterAsnQty) + Convert.ToInt32(jbr.enterCrnQty) + Convert.ToInt32(jbr.enterTransQty) +
                        Convert.ToInt32(jbr.enterExchangeQty) + Convert.ToInt32(jbr.enterReturnQty) + Convert.ToInt32(jbr.enterCountPlusQty) +
                        Convert.ToInt32(jbr.enterOtherQty);
                    //所有出库
                    int outAll = Convert.ToInt32(jbr.outSalesQty) + Convert.ToInt32(jbr.outReturnTransQty) + Convert.ToInt32(jbr.outTransQty) +
                        Convert.ToInt32(jbr.outAsnReturnQty) + Convert.ToInt32(jbr.outBadQty) + Convert.ToInt32(jbr.outInsideSaleQty) +
                        Convert.ToInt32(jbr.outDiscount) + Convert.ToInt32(jbr.outExchangeQty) + Convert.ToInt32(jbr.outUseQty) +
                        Convert.ToInt32(jbr.outCancelQty) + Convert.ToInt32(jbr.outCountMinusQty) + Convert.ToInt32(jbr.outOtherQty);
                    //（期末库存-（期初库存+所有入库-所有出库））
                    jbr.diff = Convert.ToInt32(jbr.endQty) - (Convert.ToInt32(jbr.beginQty) + enterAll - outAll);
                    list.Add(jbr);
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
        #endregion

        private void btnQuery_Click(object sender, EventArgs e)
        {
            Reload();
        }
    }
}
