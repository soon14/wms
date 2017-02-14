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
using Nodes.Shares;
using Nodes.UI;
using DevExpress.Utils;
using Nodes.Resources;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;
using Newtonsoft.Json;

namespace Nodes.Outstore
{
    public partial class FrmSOLPNState : DevExpress.XtraEditors.XtraForm
    {
        //private SODal soDal = new SODal();
        public FrmSOLPNState()
        {
            InitializeComponent();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            ImageCollection ic = AppResource.LoadToolImages();
            barManager1.Images = ic;
            barButtonItem1.ImageIndex = (int)AppResource.EIcons.refresh;

            Reload();
        }

        /// <summary>
        /// 物流箱状态查询－查询物流箱状态
        /// </summary>
        /// <param name="billNO"></param>
        /// <param name="containerCode"></param>
        /// <param name="containerState"></param>
        /// <returns></returns>
        public DataTable ListContainerState(string billNO, string containerCode, string containerState)
        {
            #region
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("CT_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("STATE_DESC", Type.GetType("System.String"));
            tblDatas.Columns.Add("LC_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("BILL_NO", Type.GetType("System.String"));
            tblDatas.Columns.Add("C_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("UNIQUE_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("BILL_HEAD_ID", Type.GetType("System.String"));
            #endregion

            try
            {
                #region 请求数据
                string ret = string.Empty;
                string jsonQuery = WebWork.SendRequest(ret, WebWork.URL_ListContainerStateZhangJinQiao);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonListContainerState bill = JsonConvert.DeserializeObject<JsonListContainerState>(jsonQuery);
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
                foreach (JsonListContainerStateResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["CT_CODE"] = tm.ctCode;
                    newRow["STATE_DESC"] = tm.stateDesc;
                    newRow["LC_CODE"] = tm.lcCode;
                    newRow["BILL_NO"] = tm.billNo;
                    newRow["C_NAME"] = tm.cName;
                    newRow["UNIQUE_CODE"] = tm.uniqueCode;
                    newRow["BILL_HEAD_ID"] = tm.billHeadId;
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

        private void Reload()
        {
            try
            {
                bindingSource1.DataSource = ListContainerState(null, null, null);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string tag = ConvertUtil.ToString(e.Item.Tag);
            switch (tag)
            {
                case "刷新":
                    Reload();
                    break;
            }
        }

        /// <summary>
        /// 物流箱状态查询－查询物流箱当前记录
        /// </summary>
        /// <param name="containerCode"></param>
        /// <returns></returns>
        public DataTable GetContainerRecords(int billId, string containerCode)
        {
            #region
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("SKU_BARCODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("SKU_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("QTY", Type.GetType("System.String"));
            tblDatas.Columns.Add("UM_NAME", Type.GetType("System.String"));
            //tblDatas.Columns.Add("CREATE_DATE", Type.GetType("System.String"));
            tblDatas.Columns.Add("CREATOR", Type.GetType("System.String"));
            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("ctCode=").Append(containerCode).Append("&");
                loStr.Append("billId=").Append(billId);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetContainerRecordsZhangJinQiao);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetContainerRecords bill = JsonConvert.DeserializeObject<JsonGetContainerRecords>(jsonQuery);
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
                foreach (JsonGetContainerRecordsResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["SKU_BARCODE"] = tm.skuBarCode;
                    newRow["SKU_NAME"] = tm.skuName;
                    newRow["QTY"] = tm.pickQty;
                    //newRow["CREATE_DATE"] = tm.crea;
                    newRow["UM_NAME"] = tm.umName;
                    newRow["CREATOR"] = tm.userCode;
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

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            gridView2.ViewCaption = "物流箱记录";
            if (e.RowHandle < 0)
                bindingSource2.DataSource = null;
            else
            {
                string uniqueCode = ConvertUtil.ToString(gridView1.GetFocusedRowCellValue("UNIQUE_CODE"));
                int billID = ConvertUtil.ToInt(gridView1.GetFocusedRowCellValue("BILL_HEAD_ID"));
                string ctCode = ConvertUtil.ToString(gridView1.GetFocusedRowCellValue("CT_CODE"));
                if (billID > 0)
                {
                    gridView2.ViewCaption = string.Format("物流箱记录-{0}", gridView1.GetFocusedRowCellValue("CT_CODE"));
                    bindingSource2.DataSource = GetContainerRecords(billID, ctCode);
                }
                else
                    bindingSource2.DataSource = null;
            }
        }
    }
}