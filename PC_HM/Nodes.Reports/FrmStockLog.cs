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
    public partial class FrmStockLog : DevExpress.XtraEditors.XtraForm
    {
        public FrmStockLog()
        {
            InitializeComponent();
        }

        private void FrmStockLog_Load(object sender, EventArgs e)
        {

            //dateBegin.DateTime = DateTime.Now.AddMonths(-1);//.ToString("yyyy-MM-dd HH:mm");
            //dateEnd.DateTime = DateTime.Now;//.ToString("yyyy-MM-dd HH:mm");
            dateBegin.EditValue = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd HH:mm");
            dateEnd.EditValue   = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

            using (WaitDialogForm waitDialog = new WaitDialogForm("提示", "正在加载数据，请稍后..."))
            {
                this.bindingSource1.DataSource = StockTotalFlow();
            }
        }

        #region 初始化数据
        /// <summary>
        /// 查询总库存新
        /// </summary>
        /// <param name="warehouseCode"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public List<JsonStockTotalFlowResult> StockTotalFlow()
        {
            List<JsonStockTotalFlowResult> list = new List<JsonStockTotalFlowResult>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("warehouseCode=").Append("").Append("&");
                loStr.Append("roleName=").Append("");
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_StockTotalFlow);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonStockTotalFlow bill = JsonConvert.DeserializeObject<JsonStockTotalFlow>(jsonQuery);
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
                foreach (JsonStockTotalFlowResult jbr in bill.result)
                {
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


        /// <summary>
        /// 查询物料流水日志
        /// </summary>
        /// <param name="skuCode"></param>
        /// <param name="startDate"></param>
        /// <param name="endTate"></param>
        /// <returns></returns>
        public List<JsonFindStockFlowResult> FindStockFlow(string skuCode, string startDate, string endTate)
        {
            List<JsonFindStockFlowResult> list = new List<JsonFindStockFlowResult>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("skuCode=").Append(skuCode).Append("&");
                loStr.Append("startDate=").Append(startDate).Append("&");
                loStr.Append("endDate=").Append(endTate);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_FindStockFlow);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonFindStockFlow bill = JsonConvert.DeserializeObject<JsonFindStockFlow>(jsonQuery);
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
                foreach (JsonFindStockFlowResult jbr in bill.result)
                {
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
         
        #region 事件
        private void gvHeader_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvHeader.FocusedRowHandle < 0)
                return;
            JsonStockTotalFlowResult entity = gvHeader.GetRow(gvHeader.FocusedRowHandle) as JsonStockTotalFlowResult;
            gvDetails.ViewCaption = String.Format("商品名称：{0}", entity.sku_name);
            using (WaitDialogForm waitDialog = new WaitDialogForm("提示", "正在加载数据，请稍后..."))
            {
                gdDetails.DataSource = FindStockFlow(entity.sku_code, ConvertUtil.ToString(dateBegin.EditValue == null ? dateBegin.EditValue : ConvertUtil.ToDatetime(dateBegin.EditValue.ToString())),
                        ConvertUtil.ToString(dateEnd.EditValue == null ? dateEnd.EditValue : ConvertUtil.ToDatetime(dateEnd.EditValue.ToString())));

            }
        }
        #endregion

        private void sbFlash_Click(object sender, EventArgs e)
        {
            using (WaitDialogForm waitDialog = new WaitDialogForm("提示", "正在加载数据，请稍后..."))
            {
                this.bindingSource1.DataSource = StockTotalFlow();
            }
        }
    }
}
