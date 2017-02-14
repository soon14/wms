using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.Entities;
using Nodes.UI;
using Nodes.DBHelper;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Instore;
using Newtonsoft.Json;

namespace Nodes.Instore
{
    public partial class FrmASNShowContainer : DevExpress.XtraEditors.XtraForm
    {
        private AsnDal asnDal = null;
        private AsnBodyEntity AsnEntity = null;
        public FrmASNShowContainer(AsnBodyEntity bodyEntity)
        {
            InitializeComponent();
            this.AsnEntity = bodyEntity;
        }

        private void OnFrmLoad(object sender, EventArgs e)
        {
            try
            {
                this.asnDal =new AsnDal();

                LoadData();

            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
                this.Close();
            }

        }

        /// <summary>
        /// 收货单据管理，查询托盘记录
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public DataTable GetContainerStateByBillID(int billID)
        {
            #region 创建DataTable

            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("CT_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("SKU_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("SKU_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("USER_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("CHECK_STATE", Type.GetType("System.String"));
            tblDatas.Columns.Add("CREATE_DATE", Type.GetType("System.DateTime"));
            tblDatas.Columns.Add("QTY", Type.GetType("System.String"));
            tblDatas.Columns.Add("UM_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("CT_STATE_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("CHECK_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("PRODUCT_DATE", Type.GetType("System.DateTime"));
            //tblDatas.Columns.Add("CHECK_NAME", Type.GetType("System.String"));
            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetContainerStateByBillID);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetContainer bill = JsonConvert.DeserializeObject<JsonGetContainer>(jsonQuery);
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
                foreach (JsonGetContainerResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["CT_CODE"] = tm.ctCode;
                    newRow["SKU_CODE"] = tm.skuCode;
                    newRow["SKU_NAME"] = tm.skuName;
                    newRow["USER_NAME"] = tm.userName;
                    newRow["CHECK_STATE"] = tm.checkState;          
                    newRow["QTY"] = tm.qty.ToString();
                    newRow["UM_NAME"] = tm.umName;
                    newRow["CT_STATE_NAME"] = tm.ctStateName;
                    newRow["CHECK_NAME"] = tm.checkName;

                    #region
                    try
                    {
                        if (!string.IsNullOrEmpty(tm.productDate))
                            newRow["PRODUCT_DATE"] = Convert.ToDateTime(tm.productDate);
                        
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmASNShowContainer+GetContainerStateByBillID", msg);
                    }
                    #endregion

                    #region
                    try
                    {
                        if (!string.IsNullOrEmpty(tm.createDate))
                            newRow["CREATE_DATE"] = Convert.ToDateTime(tm.createDate);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmASNShowContainer+GetContainerStateByBillID", msg);
                    }
                    #endregion
                    //newRow["CHECK_NAME"] = tm.che;
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
            gridView1.ViewCaption = String.Format("订单号：{0}  供应商：{1}  业务类型：{2}", this.AsnEntity.BillNO, this.AsnEntity.SupplierName, this.AsnEntity.BillTypeDesc);

            gridControl1.DataSource = GetContainerStateByBillID(this.AsnEntity.BillID);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                MsgBox.Warn(ex.Message);
            }

        }

    }
}