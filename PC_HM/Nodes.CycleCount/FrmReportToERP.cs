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
using DevExpress.Utils;
using Nodes.Resources;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.CycleCount;
using Newtonsoft.Json;

namespace Nodes.CycleCount
{
    public partial class FrmReportToERP : DevExpress.XtraEditors.XtraForm
    {
        CycleCountDal countDal = new CycleCountDal();
        private int billID;

        public FrmReportToERP(int billID)
        {
            InitializeComponent();

            this.billID = billID;
        }

        /// <summary>
        /// 盘点单管理---报告上传
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public DataTable GetReportOnlyDiff(int billID)
        {
            #region DataTable
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("ZN_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("LC_CODE", Type.GetType("System.String"));
            tblDatas.Columns.Add("SKU_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("COUNT_QTY", Type.GetType("System.Decimal"));
            tblDatas.Columns.Add("STOCK_QTY", Type.GetType("System.Decimal"));
            tblDatas.Columns.Add("DIFF_QTY", Type.GetType("System.Decimal"));
            tblDatas.Columns.Add("STOCK_EXP_DATE", Type.GetType("System.DateTime"));
            tblDatas.Columns.Add("EXP_DATE", Type.GetType("System.DateTime"));
            tblDatas.Columns.Add("SPEC", Type.GetType("System.String"));
            tblDatas.Columns.Add("REMARK", Type.GetType("System.String"));
            tblDatas.Columns.Add("UPLOADED", Type.GetType("System.Boolean"));
            tblDatas.Columns.Add("ID", Type.GetType("System.Int32"));

            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetReportOnlyDiff);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetReportOnlyDiff bill = JsonConvert.DeserializeObject<JsonGetReportOnlyDiff>(jsonQuery);
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
                foreach (JsonGetReportOnlyDiffResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["ZN_NAME"] = tm.znName;
                    newRow["LC_CODE"] = tm.lcCode;
                    newRow["SKU_NAME"] = tm.skuName;
                    newRow["COUNT_QTY"] = StringToDecimal.GetTwoDecimal(tm.countQty);
                    newRow["STOCK_QTY"] = StringToDecimal.GetTwoDecimal(tm.stockQty);
                    newRow["ID"] = Convert.ToInt32(tm.id);
                    newRow["DIFF_QTY"] = StringToDecimal.GetTwoDecimal(tm.countQty) - StringToDecimal.GetTwoDecimal(tm.stockQty);
                    newRow["SPEC"] = tm.spec;
                    newRow["REMARK"] = tm.remark;
                    newRow["UPLOADED"] = Convert.ToBoolean(tm.uploaded);
                    if (!string.IsNullOrEmpty(tm.stockExpDate))
                        newRow["STOCK_EXP_DATE"] = tm.stockExpDate;
                    if (!string.IsNullOrEmpty(tm.expDate))
                        newRow["EXP_DATE"] = tm.expDate;
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

        private void OnFormLoad(object sender, EventArgs e)
        {
            ImageCollection ic = AppResource.LoadToolImages();
            barManager1.Images = ic;
            barButtonItem1.ImageIndex = (int)AppResource.EIcons.save;
            barButtonItem2.ImageIndex = (int)AppResource.EIcons.remove;
            barButtonItem3.ImageIndex = (int)AppResource.EIcons.back;
            barButtonItem4.ImageIndex = (int)AppResource.EIcons.ok;
            barButtonItem5.ImageIndex = (int)AppResource.EIcons.approved;

            try
            {
                bindingSource1.DataSource = GetReportOnlyDiff(this.billID);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 盘点单管理---保存
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public bool SaveReportDetail(DataRow row)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("remark=").Append(ConvertUtil.ToString(row["REMARK"])).Append("&");
                loStr.Append("id=").Append(ConvertUtil.ToInt(row["ID"])).Append("&");
                //loStr.Append("uploaded=").Append(ConvertUtil.ToBool(row["UPLOADED"]));
                loStr.Append("uploaded=").Append(Convert.ToInt32(ConvertUtil.ToBool(row["UPLOADED"])));
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveReportDetail);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
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

        private void DoSave()
        {
            gridView1.CloseEditor();
            DataTable dataChanged = bindingSource1.DataSource as DataTable;
            //DataTable dataChanged = data.GetChanges(DataRowState.Modified);

            //if (dataChanged == null || dataChanged.Rows.Count == 0)
            //{
            //    MsgBox.Warn("没有修改的数据需要保存。");
            //}
            //else
            //{
                try
                {
                    string errMsg = string.Empty;
                    foreach (DataRow row in dataChanged.Rows)
                        if (!SaveReportDetail(row))
                            errMsg += ConvertUtil.ToInt(row["ID"]);

                    dataChanged.AcceptChanges();
                    if (string.IsNullOrEmpty(errMsg))
                        MsgBox.OK("保存成功。");
                    else
                    {
                        errMsg = "部分数据保存失败,请仔细核对，" + errMsg;
                        MsgBox.Err(errMsg);
                    }

                    //foreach (DataRow row in dataChanged.Rows)
                    //    SaveReportDetail(row);

                    //dataChanged.AcceptChanges();
                    //MsgBox.OK("保存成功。");
                }
                catch (Exception ex)
                {
                    MsgBox.Err(ex.Message);
                }
            //}
        }

        /// <summary>
        /// 盘点差异调整--更新状态
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="billState"></param>
        /// <returns></returns>
        public bool UpdateBillState(int billID, string billState)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID).Append("&");
                loStr.Append("billState=").Append(billState);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_UpdateBillState);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
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

        private void DoCancel()
        {
            if (MsgBox.AskOK("确定要将盘点单置为“作废”状态吗？作废后将不可以上传。") != DialogResult.OK)
                return;

            try
            {
                bool result = UpdateBillState(this.billID, "133");
                if (result)
                    MsgBox.OK("已成功置为作废状态。");
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 盘点任务分派--同步上传状态
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="syncState"></param>
        /// <returns></returns>
        public bool UpdateBillSyncState(int billID, string syncState)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID).Append("&");
                loStr.Append("syncState=").Append(syncState);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_UpdateBillSyncState);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
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

        private void DoUpload()
        {
            if (MsgBox.AskOK("是否确认上传，在上传之前请确保将修改的数据保存。") != DialogResult.OK)
                return;

            try
            {
                gridView1.CloseEditor();
                DataTable dataChanged = bindingSource1.DataSource as DataTable;
                DataRow[] rows  = dataChanged.Select("UPLOADED = 1");
                if (rows.Length == 0)
                {
                    MsgBox.Warn("至少要选中一条盘点单！");
                    return;
                }

                bool result = UpdateBillSyncState(this.billID, "1");
                result = UpdateBillState(this.billID, "134");
                MsgBox.OK("已成功将同步状态置为等待上传。");
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void DoCheckAll()
        {
            DataTable data = bindingSource1.DataSource as DataTable;
            if (data != null)
            {
                foreach (DataRow row in data.Rows)
                    row["UPLOADED"] = true;
            }
        }

        private void DoCheckDiff()
        {
            DataTable data = bindingSource1.DataSource as DataTable;
            if (data != null)
            {
                foreach (DataRow row in data.Rows)
                {
                    if (ConvertUtil.ToDecimal(row["COUNT_QTY"]) != ConvertUtil.ToDecimal(row["STOCK_QTY"]))
                        row["UPLOADED"] = true;
                    else
                        row["UPLOADED"] = false;
                }
            }
        }

        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string tag = ConvertUtil.ToString(e.Item.Tag);
            switch (tag)
            {
                case "保存":
                    DoSave();
                    break;
                case "作废":
                    DoCancel();
                    break;
                case "上传":
                    DoUpload();
                    break;
                case "只选中有差异的行":
                    DoCheckDiff();
                    break;
                case "选中所有行":
                    DoCheckAll();
                    break;
            }
        }
    }
}