using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
//using Nodes.DBHelper;
using Nodes.UI;
using Nodes.Entities;
using Nodes.Shares;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Stock;
using Newtonsoft.Json;

namespace Nodes.Stock
{
    public partial class FrmCreateReplenishBill : DevExpress.XtraEditors.XtraForm
    {
        //private ReplenishDal replenishDal = new ReplenishDal();

        public FrmCreateReplenishBill()
        {
            InitializeComponent();
        }

        public FrmCreateReplenishBill(List<StockTransEntity> results)
            : this()
        {
            bindingSource1.DataSource = results;
        }

        public FrmCreateReplenishBill(List<StockTransEntity> results, bool state)
            : this(results)
        {
            this.btnInquiry.Visible = this.btnRemoveSelected.Visible = false;
        }

        /// <summary>
        /// 触发补货任务---开始计算
        /// </summary>
        /// <returns></returns>
        public List<StockTransEntity> InquiryStock()
        {
            List<StockTransEntity> list = new List<StockTransEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billState=").Append(BillStateConst.ASN_STATE_CODE_COMPLETE).Append("&");
                //loStr.Append("wareHouseCode=").Append(warehouseCode);
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_InquiryStock);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonInquiryStock bill = JsonConvert.DeserializeObject<JsonInquiryStock>(jsonQuery);
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
                foreach (JsonInquiryStockResult jbr in bill.result)
                {
                    StockTransEntity asnEntity = new StockTransEntity();
                    #region 0-10
                    asnEntity.Location = jbr.lcCode;
                    asnEntity.Material = jbr.skuCode;
                    asnEntity.MaterialName = jbr.skuName;
                    asnEntity.OccupyQty = Convert.ToDecimal(jbr.occupyQty);
                    asnEntity.PickingQty = Convert.ToDecimal(jbr.pickingQty);
                    asnEntity.Qty = Convert.ToDecimal(jbr.qty);
                    asnEntity.StockID = Convert.ToInt32(jbr.stockId);
                    asnEntity.TransferQty = Convert.ToDecimal(jbr.transQty);
                    asnEntity.UnitName = jbr.umName;
                    asnEntity.TargetLocation = jbr.toLcCode;
                    #endregion
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
        private void btnInquiry_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                btnSave.Enabled = true;
                bindingSource1.DataSource = InquiryStock();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void btnRemoveSelected_Click(object sender, EventArgs e)
        {
            gridView1.DeleteSelectedRows();
            gridView1.RefreshData();
        }

        #region List转换成Json
        private string GetRes<T>(List<T> listobj, List<string> proptylist)
        {

            StringBuilder strb = new StringBuilder();
            List<string> result = new List<string>();
            string curname = default(string);
            foreach (var obj in listobj)
            {

                Type type = obj.GetType();

                curname = type.Name;


                List<string> curobjliststr = new List<string>();
                foreach (var curpropty in proptylist)
                {
                    string tmp = default(string);
                    var res01 = type.GetProperty(curpropty).GetValue(obj, null);
                    if (res01 == null)
                    {
                        tmp = null;
                    }
                    else
                    {
                        tmp = res01.ToString();
                    }
                    curobjliststr.Add("\"" + curpropty + "\"" + ":" + "\"" + tmp + "\"");
                }
                string curres = "{" + string.Join(",", curobjliststr.ToArray()) + "}";
                result.Add(curres);
            }
            strb.Append(":[" + string.Join(",", result.ToArray()) + "]");
            string ret = "\"" + curname + "\"" + strb.ToString();
            ret = ret.Insert(0, "{");
            ret = ret.Insert(ret.Length, "}");
            return ret;
        }


        private string GetResList<T>(List<T> listobj, List<string> proptylist)
        {

            StringBuilder strb = new StringBuilder();
            List<string> result = new List<string>();
            string curname = default(string);
            foreach (var obj in listobj)
            {

                Type type = obj.GetType();

                curname = type.Name;


                List<string> curobjliststr = new List<string>();
                foreach (var curpropty in proptylist)
                {
                    string tmp = default(string);
                    var res01 = type.GetProperty(curpropty).GetValue(obj, null);
                    if (res01 == null)
                    {
                        tmp = null;
                    }
                    else
                    {
                        tmp = res01.ToString();
                    }
                    curobjliststr.Add("\"" + curpropty + "\"" + ":" + "\"" + tmp + "\"");
                }
                string curres = "{" + string.Join(",", curobjliststr.ToArray()) + "}";
                result.Add(curres);
            }

            //strb.Append(":[" + string.Join(",", result.ToArray()) + "]");
            //string ret = "\""+ curname + "\"" + strb.ToString();
            //ret = ret.Insert(0, "{");
            //ret = ret.Insert(ret.Length, "}");
            return string.Join(",", result.ToArray());
        }

        private string GetResList<T>(string josnName, List<T> listobj, List<string> proptylist)
        {

            StringBuilder strb = new StringBuilder();
            List<string> result = new List<string>();
            string curname = default(string);
            foreach (var obj in listobj)
            {

                Type type = obj.GetType();

                curname = type.Name;


                List<string> curobjliststr = new List<string>();
                foreach (var curpropty in proptylist)
                {
                    string tmp = default(string);
                    var res01 = type.GetProperty(curpropty).GetValue(obj, null);
                    if (res01 == null)
                    {
                        tmp = null;
                    }
                    else
                    {
                        tmp = res01.ToString();
                    }
                    curobjliststr.Add("\"" + curpropty + "\"" + ":" + "\"" + tmp + "\"");
                }
                string curres = "{" + string.Join(",", curobjliststr.ToArray()) + "}";
                result.Add(curres);
            }

            strb.Append(":[" + string.Join(",", result.ToArray()) + "]");
            string ret = "\"" + josnName + "\"" + strb.ToString();
            //ret = ret.Insert(0, "{");
            //ret = ret.Insert(ret.Length, "}");
            return ret;
        }

        #endregion

        /// <summary>
        /// 库存转移--保存编辑的采购单
        /// </summary>
        /// <param name="billType"></param>
        /// <param name="remark"></param>
        /// <param name="whCode"></param>
        /// <param name="creator"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        public List<int> SaveBill(string billType, string remark, string whCode,
            string creator, List<StockTransEntity> details)
        {
            List<int> list = new List<int>();
            try
            {
                List<StockTransEntity> listCase1 = details.FindAll((item) => { return item.IsCase == 1; });
                List<StockTransEntity> listCase2 = details.FindAll((item) => { return item.IsCase != 1; });
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billType=").Append(billType).Append("&");
                loStr.Append("remark=").Append(remark).Append("&");
                loStr.Append("whCode=").Append(whCode).Append("&");
                loStr.Append("creator=").Append(creator).Append("&");
                loStr.Append("flagA=").Append(listCase1.Count).Append("&");
                loStr.Append("flagB=").Append(listCase2.Count).Append("&");
                List<string> prop = new List<string>() { "Material", "Location", "TargetLocation", "TransferQty" };
                string jsonDetailA = GetResList<StockTransEntity>("jsonDetailA", listCase1, prop);
                jsonDetailA = "{" + jsonDetailA + "}";
                loStr.Append("jsonDetailA=").Append(jsonDetailA).Append("&");
                string jsonDetailB = GetResList<StockTransEntity>("jsonDetailB", listCase2, prop);
                jsonDetailB = "{" + jsonDetailB + "}";
                loStr.Append("jsonDetailB=").Append(jsonDetailB);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveBill);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonSaveBill bill = JsonConvert.DeserializeObject<JsonSaveBill>(jsonQuery);
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
                foreach (JsonSaveBillResult jbr in bill.result)
                {
                    list.Add(Convert.ToInt32(jbr.billId));
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
        /// 库存转移--分派任务
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="taskType"></param>
        /// <returns></returns>
        public bool Schedule(int billID, string taskType)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID).Append("&");
                loStr.Append("taskType=").Append(taskType);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_Schedule);
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

        /// <summary>
        /// 任务自动刷新
        /// </summary>
        /// <returns></returns>
        public bool AutoAssignTask()
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("wareHouseType=").Append(EUtilStroreType.WarehouseTypeToInt(GlobeSettings.LoginedUser.WarehouseType));
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_AutoAssignTask);
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


        private void btnSave_Click(object sender, EventArgs e)
        {
            List<StockTransEntity> data = bindingSource1.DataSource as List<StockTransEntity>;
            if (data == null)
            {
                MsgBox.Warn("没有数据需要保存。");
                return;
            }

            //把数量大于0的取出
            data = data.FindAll(p => p.TransferQty > 0);
            if (data.Count == 0)
            {
                MsgBox.Warn("没有数据需要保存。");
                return;
            }

            if (MsgBox.AskOK("确定要保存吗？") != DialogResult.OK)
                return;

            try
            {
                #region 整混
                List<int> billIDList = SaveBill(
                    BaseCodeConstant.BILL_TYPE_REPLENISH,
                    string.Empty,
                    GlobeSettings.LoginedUser.WarehouseCode,
                    GlobeSettings.LoginedUser.UserName,
                    data);
                //if (MsgBox.AskYes("保存成功，是否现在分派任务？") == DialogResult.Yes)
                //{
                if (billIDList != null && billIDList.Count > 0)
                {
                    bool result = false;
                    foreach (int item in billIDList)
                    {
                        result = Schedule(item, "144");
                        if (result)
                            AutoAssignTask();
                    }
                    if (result)
                    {
                        btnSave.Enabled = false;
                        this.DialogResult = DialogResult.OK;
                    }
                    //else
                    //{
                    //    MsgBox.Warn(result);
                    //}
                }
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
    }
}