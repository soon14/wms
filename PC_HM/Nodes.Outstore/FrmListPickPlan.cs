using System;
using System.Collections.Generic;
//using Nodes.DBHelper;
using Nodes.Utils;
using Nodes.UI;
using Nodes.Entities;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;
using Newtonsoft.Json;

namespace Nodes.Outstore
{
    public partial class FrmListPickPlan : DevExpress.XtraEditors.XtraForm
    {
        //SODal soDal = new SODal();
        private int billID;

        public FrmListPickPlan(int billID, string billNO)
        {
            InitializeComponent();

            this.billID = billID;
            this.Text = this.Text + string.Format("(单据：{0})", billNO);
        }

        public List<PickPlanEntity> GetPickPlan(int billID)
        {
            List<PickPlanEntity> list = new List<PickPlanEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billID=").Append(billID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetPickPlanLongMiao);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetPickPlan bill = JsonConvert.DeserializeObject<JsonGetPickPlan>(jsonQuery);
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
                foreach (JsonGetPickPlanResult jbr in bill.result)
                {
                    PickPlanEntity asnEntity = new PickPlanEntity();
                    #region 0-10
                    asnEntity.BillID = Convert.ToInt32(jbr.billId);
                    asnEntity.BillNO = jbr.billNo;
                    asnEntity.ComMaterial = jbr.comMaterial;
                    asnEntity.DetailID = Convert.ToInt32(jbr.detailId);
                    asnEntity.Location = jbr.lcCode;
                    asnEntity.Material = jbr.skuCode;
                    asnEntity.MaterialName = jbr.skuName;
                    asnEntity.Qty = jbr.qty;
                    asnEntity.RowNO = Convert.ToInt32(jbr.rowNo);
                    asnEntity.SaleUnit = jbr.saleUnit;
                    #endregion
                    #region 11-20
                    asnEntity.SaleUnitTransValue = Convert.ToInt32(jbr.saleTransValue);
                    asnEntity.SkuBarcode = jbr.skuBarcode;
                    asnEntity.STOCK_ID = Convert.ToInt32(jbr.stockId);
                    asnEntity.UnitCode = jbr.umCode;
                    asnEntity.UnitName = jbr.umName;
                    #endregion

                    #region
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.createDate))
                            asnEntity.CreateData = Convert.ToDateTime(jbr.createDate);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmListPickPlan+GetPickPlan", msg);
                    }
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
                        //LogHelper.errorLog("FrmListPickPlan+GetPickPlan", msg);
                    }
                    #endregion

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

        private void OnFormLoad(object sender, EventArgs e)
        {
            gridPlans.DataSource = GetPickPlan(billID);
        }
    }
}