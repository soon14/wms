using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.DBHelper;
using Nodes.UI;
using Nodes.Utils;
using Nodes.Entities;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;
using Nodes.Entities.HttpEntity.Instore;
using Newtonsoft.Json;

namespace Nodes.Outstore
{
    public partial class FrmSKULocation : DevExpress.XtraEditors.XtraForm
    {
        private string SkuCode = "";
        public FrmSKULocation(string skuCode)
        {
            InitializeComponent();
            this.SkuCode = skuCode;
        }

        /// <summary>
        /// 当前订单量--双击，查看明细
        /// </summary>
        /// <param name="skuCode"></param>
        /// <returns></returns>
        public DataTable GetSKULocation(string skuCode)
        {
            #region
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("ZN_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("LC_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("SKU_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("SKU_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("STOCKQTY", Type.GetType("System.String"));
            tblDatas.Columns.Add("STOCKUMNAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("SALEUMNAME", Type.GetType("System.String"));
            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("skuCode=").Append(skuCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetSKULocation);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetSKULocation bill = JsonConvert.DeserializeObject<JsonGetSKULocation>(jsonQuery);
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

                List<JsonVehiclesEntity> jb = new List<JsonVehiclesEntity>();
                #region 赋值
                foreach (JsonGetSKULocationResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["ZN_NAME"] = tm.znName;
                    newRow["LC_CODE"] = tm.lcCode;
                    newRow["SKU_NAME"] = tm.skuName;
                    newRow["SKU_CODE"] = tm.skuCode;
                    newRow["STOCKQTY"] = StringToDecimal.GetTwoDecimal(tm.stockQty);
                    newRow["STOCKUMNAME"] = tm.stockUmName;
                    newRow["SALEUMNAME"] = tm.saleUmName;
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

        private void FrmSKULocation_Load(object sender, EventArgs e)
        {
            try
            {
                SODal soDal = new SODal();
                gridControl1.DataSource = GetSKULocation(this.SkuCode); 
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
    }
}