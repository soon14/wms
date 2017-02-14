using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.Shares;
using Nodes.UI;
using DevExpress.Utils;
using Nodes.Resources;
using Newtonsoft.Json;
using Nodes.Entities.HttpEntity.Instore;
using Nodes.Entities.HttpEntity;
using Nodes.Utils;

namespace Nodes.Instore
{
    public partial class FrmContainerState : DevExpress.XtraEditors.XtraForm
    {
        private AsnQueryDal asnDal = new AsnQueryDal();

        public FrmContainerState()
        {
            InitializeComponent();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            ImageCollection ic = AppResource.LoadToolImages();
            barManager1.Images = ic;
            barButtonItem1.ImageIndex = (int)AppResource.EIcons.refresh;
            toolClear.ImageIndex = (int)AppResource.EIcons.delete;

            Reload();
        }

        /// <summary>
        /// 托盘状态列表，初始化加载托盘数据列表
        /// </summary>
        /// <param name="billNO"></param>
        /// <param name="containerCode"></param>
        /// <param name="containerState"></param>
        /// <param name="warehouse"></param>
        /// <returns></returns>
        public List<TrayStatusTableEntity> ListContainerState(string billNO, string containerCode, string containerState, string warehouse)
        {
            List<TrayStatusTableEntity> list = new List<TrayStatusTableEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("whCode=").Append(warehouse);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_ListContainerState);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonContainerState bill = JsonConvert.DeserializeObject<JsonContainerState>(jsonQuery);
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
                foreach (JsonContainerStateResult jbr in bill.result)
                {
                    TrayStatusTableEntity tste = new TrayStatusTableEntity();
                    tste.IN_BILL = jbr.inBill;
                    tste.IN_CNAME = jbr.inCname;
                    tste.IN_UCODE = jbr.UNIQUE_CODE;
                    tste.OUT_BILL = jbr.outBill;
                    tste.OUT_CNAME = jbr.outCname;
                    tste.OUT_UCODE = jbr.UNIQUE_CODE;
                    tste.STATE_DESC = jbr.stateDesc;
                    tste.CT_CODE = jbr.ctCode;
                    list.Add(tste);
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

        private void Reload()
        {
            try
            {
                bindingSource1.DataSource = ListContainerState(null, null, null, GlobeSettings.LoginedUser.WarehouseCode);
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
                case "清空":
                    ClearLPN();
                    break;
            }
        }

        /// <summary>
        /// 托盘状态列表，清空托盘
        /// </summary>
        /// <param name="ctCode"></param>
        /// <returns></returns>
        public bool CleanLPNSend(string ctCode)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("ctCode=").Append(ctCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_CleanLPN);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    return false;
                }
                #endregion

                #region 正常错误处理

                Sucess bill = JsonConvert.DeserializeObject<Sucess>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return false;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return false;
                }
                #endregion

                return true;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return false;
        }
        

        public void ClearLPN()
        {
            if (gridView1.FocusedRowHandle < 0)
            {
                MsgBox.Warn("请选择要清空的托盘。");
                return;
            }
            if (MsgBox.AskYes("确定是释放该托盘?") == DialogResult.Yes)
            {
                string lpnCode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CT_CODE").ToString();
                if (!String.IsNullOrEmpty(lpnCode))
                {
                    bool ret = CleanLPNSend(lpnCode);
                    if (ret)
                    {
                        Reload();
                    }
                    
                }
            }

        }

        /// <summary>
        /// 托盘状态列表，托盘使用记录
        /// </summary>
        /// <param name="containerCode"></param>
        /// <returns></returns>
        public DataTable GetContainerRecords(string ctCode, string billNo, string billType)
        {
            #region
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("SKU_BARCODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("SKU_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("QTY", Type.GetType("System.String"));
            tblDatas.Columns.Add("UM_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("CREATE_DATE", Type.GetType("System.String"));
            tblDatas.Columns.Add("CREATOR", Type.GetType("System.String"));   
            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("ctCode=").Append(ctCode).Append("&");
                loStr.Append("billNo=").Append(billNo).Append("&");
                loStr.Append("billType=").Append(billType);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetContainerRecords);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetContainerRecord bill = JsonConvert.DeserializeObject<JsonGetContainerRecord>(jsonQuery);
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
                foreach (JsonGetContainerRecordResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["SKU_BARCODE"] = tm.skuBarCode;
                    newRow["SKU_NAME"] = tm.skuName;
                    newRow["QTY"] = tm.qty;
                    newRow["UM_NAME"] = tm.umName;
                    newRow["CREATE_DATE"] = tm.createDate;
                    newRow["CREATOR"] = tm.creator;
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
            gridView2.ViewCaption = "托盘记录";
            if (e.RowHandle < 0)
                bindingSource2.DataSource = null;
            else
            {
                string ctCode = gridView1.GetFocusedRowCellValue("CT_CODE").ToString();
                string billNo = ConvertUtil.ToString(gridView1.GetFocusedRowCellValue("BILL_NO"));
                string billType = ConvertUtil.ToString(gridView1.GetFocusedRowCellValue("BILL_TYPE"));//出库单拣货一托盘多商品多个UNIQUE_CODE
                gridView2.ViewCaption = string.Format("托盘记录-{0}", ctCode);
                if (!String.IsNullOrEmpty(billNo))
                    bindingSource2.DataSource = GetContainerRecords(ctCode, billNo, billType);
                else
                    bindingSource2.DataSource = null;

            }
        }
    }
}