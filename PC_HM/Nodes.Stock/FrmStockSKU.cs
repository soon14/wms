using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.Utils;
//using Nodes.DBHelper;
using Nodes.UI;
using Nodes.Entities.Inventory;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Stock;
using Newtonsoft.Json;


namespace Nodes.Stock
{
    public partial class FrmStockSKU : DevExpress.XtraEditors.XtraForm
    {

        List<StockSKUEntity> list = null;
        public FrmStockSKU()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 查看商品库存信息
        /// </summary>
        /// <param name="type">0-全部；1-查看拣货区</param>
        /// <returns></returns>
        public List<StockSKUEntity> GetStockSKU(int type)
        {
            List<StockSKUEntity> list = new List<StockSKUEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("type=").Append(type);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetStockSKU);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetStockSKU bill = JsonConvert.DeserializeObject<JsonGetStockSKU>(jsonQuery);
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
                foreach (JsonGetStockSKUResult jbr in bill.result)
                {
                    StockSKUEntity asnEntity = new StockSKUEntity();
                    asnEntity.MaxStockQty = Convert.ToDecimal(jbr.maxStockQty);
                    asnEntity.MinStockQty = Convert.ToDecimal(jbr.minStockQty);
                    asnEntity.SkuCode = jbr.skuCode;
                    asnEntity.SkuName = jbr.skuName;
                    asnEntity.TotalQty = Convert.ToDecimal(jbr.totalQty);
                    asnEntity.UnitName = jbr.umName;
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
                return list;
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return list;
        }

        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            string tag = ConvertUtil.ToString(e.Item.Tag);
            switch (tag)
            {
                case "全部物料信息":
                    bindingSource1.DataSource = GetStockSKU(0);
                    break;
                case "拣货区物料信息":
                    bindingSource1.DataSource = GetStockSKU(1);
                    break;
            }
        }

        private void FrmStockSKU_Load(object sender, EventArgs e)
        {
            bindingSource1.DataSource = GetStockSKU(0);
        }
    }
}