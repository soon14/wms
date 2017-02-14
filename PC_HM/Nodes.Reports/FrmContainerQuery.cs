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
    public partial class FrmContainerQuery : DevExpress.XtraEditors.XtraForm
    {
        //private SOQueryDal soDal = new SOQueryDal();
        public FrmContainerQuery()
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
        /// 查询统计（容器位查询）
        /// </summary>
        /// <returns></returns>
        public  DataTable GetContainerInfo()
        {
            #region DataTable
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("CTL_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("CTL_STATE", Type.GetType("System.String"));
            tblDatas.Columns.Add("CTL_TYPE", Type.GetType("System.String"));
            tblDatas.Columns.Add("ITEM_DESC", Type.GetType("System.String"));
            tblDatas.Columns.Add("CT_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("BILL_HEAD_ID", Type.GetType("System.String"));
            tblDatas.Columns.Add("BILL_NO", Type.GetType("System.String"));
            tblDatas.Columns.Add("BILL_STATE", Type.GetType("System.String"));
            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                #region 参数
                //loStr.Append("beginDate=").Append(dateFrom).Append("&");
                //loStr.Append("endDate=").Append(dateTo).Append("&");
                //loStr.Append("billNo=").Append(billNO).Append("&");
                //loStr.Append("skuCode=").Append(materialCode).Append("&");
                //loStr.Append("skuName=").Append(materialName).Append("&");
                //loStr.Append("cName=").Append(customerName);
                #endregion

                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_GetContainerInfo);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetContainerInfo bill = JsonConvert.DeserializeObject<JsonGetContainerInfo>(jsonQuery);
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
                foreach (JsonGetContainerInfoResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["CTL_NAME"] = tm.ctlName;
                    newRow["CTL_STATE"] = tm.ctlState;
                    newRow["CTL_TYPE"] = tm.ctlType;
                    newRow["ITEM_DESC"] = tm.itemDesc;
                    newRow["CT_CODE"] = tm.ctCode;
                    newRow["BILL_HEAD_ID"] = tm.billHeadId;
                    newRow["BILL_NO"] = tm.billNo;
                    newRow["BILL_STATE"] = tm.billState;
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
                gridControl1.DataSource = GetContainerInfo();
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
                //string skuCode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SKU_CODE").ToString();
                //if (skuCode == "")
                //{
                //    return;
                //}
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