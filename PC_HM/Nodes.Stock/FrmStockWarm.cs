using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.UI;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.SystemManage;
using Newtonsoft.Json;

namespace Nodes.Stock
{
    public partial class FrmStockWarm : DevExpress.XtraEditors.XtraForm
    {
        public FrmStockWarm()
        {
            InitializeComponent();
        }

        private void btnOKClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 当前订单量（加载报警窗体）
        /// </summary>
        /// <returns></returns>
        public List<StockRecordEntity> QueryStockWarm()
        {
            List<StockRecordEntity> temp = new List<StockRecordEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                string jsons = string.Empty;
                string jsonQuery = WebWork.SendRequest(jsons, WebWork.URL_QueryStockWarm);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return temp;
                }
                #endregion

                #region 正常错误处理

                JsonQueryStockWarm bill = JsonConvert.DeserializeObject<JsonQueryStockWarm>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return temp;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return temp;
                }
                #endregion

                #region 赋值数据
                foreach (JsonQueryStockWarmResult jbr in bill.result)
                {
                    StockRecordEntity asnEntity = new StockRecordEntity();
                    #region 0-10
                    asnEntity.ExpDays = Convert.ToInt32(jbr.expDays);
                    asnEntity.StockID = Convert.ToInt32(jbr.id);
                    asnEntity.Location = jbr.lcCode;
                    asnEntity.OccupyQty = Convert.ToDecimal(jbr.occupyQty);
                    asnEntity.PickingQty = Convert.ToDecimal(jbr.pickingQty);
                    asnEntity.Qty = Convert.ToDecimal(jbr.qty);
                    asnEntity.Material = jbr.skuCode;
                    asnEntity.MaterialName = jbr.skuName;
                    asnEntity.Spec = jbr.spec;
                    asnEntity.UnitName = jbr.umName;
                    asnEntity.ZoneName = jbr.znName;
                    #endregion

                    #region
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.expDate))
                            asnEntity.ExpDate = Convert.ToDateTime(jbr.expDate);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    }
                    #endregion

                    #region
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.latestIn))
                            asnEntity.LastInDate = Convert.ToDateTime(jbr.latestIn);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    }
                    #endregion

                    #region
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.latestOut))
                            asnEntity.LastOutDate = Convert.ToDateTime(jbr.latestOut);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    }
                    #endregion

                    temp.Add(asnEntity);
                }

                return temp;
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return temp;
        }

        private void FrmLoad(object sender, EventArgs e)
        {
            try
            {
                List<StockRecordEntity> list = QueryStockWarm();
                List<StockRecordEntity> listExp = new List<StockRecordEntity>();
                listExp.AddRange(list.FindAll(s => s.IsPassEXP <= ConvertUtil.ToDecimal(0.5)));
                if (listExp.Count > 0)
                    gridControl1.DataSource = listExp;
                else
                    this.DialogResult = DialogResult.OK;
            }
            catch
            { }
        }
    }
}