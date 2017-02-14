using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nodes.Entities;
using Nodes.Shares;
using System.Linq;
using Nodes.DBHelper;
using Nodes.Utils;
using Nodes.UI;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;
using Newtonsoft.Json;

namespace Nodes.Outstore
{
    public partial class FrmTempPickPlan : DevExpress.XtraEditors.XtraForm
    {
        #region 变量

        private string tempID, errMsg = null;
        private SODal soDal = new SODal();
        public event EventHandler DataChanged;

        List<PickPlanEntity> _pickResult = null;
        List<PickPlanEntity> _pickResultError = null;
        private string _bills = string.Empty;

        #endregion

        #region 构造函数

        public FrmTempPickPlan(string tempID, string errMsg)
        {
            InitializeComponent();

            this.tempID = tempID;
            this.errMsg = errMsg;
        }

        public FrmTempPickPlan(string tempID, string errMsg, string bills)
            : this(tempID, errMsg)
        {
            this._bills = bills;
        }

        #endregion

        /// <summary>
        /// 拣货计划 ：保存拣配结果 之 ： 查询临时表信息 （拣货临时表和缺货临时表）
        /// </summary>
        /// <param name="tempID"></param>
        /// <returns></returns>
        public JsonGetTempPickResult GetTempPickResult(string tempID)
        {
            JsonGetTempPickResult temp = new JsonGetTempPickResult();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("tempID=").Append(tempID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetTempPickResult);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return temp;
                }
                #endregion

                #region 正常错误处理

                JsonGetTempPickResult bill = JsonConvert.DeserializeObject<JsonGetTempPickResult>(jsonQuery);
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
                if (bill.result != null)
                    return bill;

            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return temp;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            //this._pickResult = soDal.GetTempPickResult(this.tempID);
            //this._pickResultError = soDal.GetTempPickResultError(this.tempID);

            ////读取临时保存的拣配结果
            //bindingSource1.DataSource = this._pickResult;
            //bindingSource2.DataSource = this._pickResultError;

            #region 接口
            JsonGetTempPickResult temp = GetTempPickResult(this.tempID);
            List<PickPlanEntity> listPick = new List<PickPlanEntity>();
            List<PickPlanEntity> listPickError = new List<PickPlanEntity>();
            if (temp.result != null)
            {
                if (temp.result[0].picktemp != null)
                {
                    #region 赋值数据 拣货临时表
                    foreach (JsonGetTempPickResultPick jbr in temp.result[0].picktemp)
                    {
                        PickPlanEntity asnEntity = new PickPlanEntity();
                        #region 0-10
                        asnEntity.BillID = Convert.ToInt32(jbr.billId);
                        asnEntity.BillNO = jbr.billNo;
                        asnEntity.ComMaterial = jbr.comMaterial;
                        asnEntity.CustomerName = jbr.customerName;
                        asnEntity.DetailID = Convert.ToInt32(jbr.detailID);
                        asnEntity.IsCase = Convert.ToInt32(jbr.isCase);
                        asnEntity.Location = jbr.location;
                        asnEntity.Material = jbr.skuCode;
                        asnEntity.MaterialName = jbr.skuName;
                        asnEntity.Qty = Convert.ToDecimal(jbr.qty);
                        #endregion
                        asnEntity.RowNO = Convert.ToInt32(jbr.rowNO);
                        asnEntity.UnitName = jbr.unitName;
                        asnEntity.UnitCode = jbr.unitCode;
                        asnEntity.STOCK_ID = Convert.ToInt32(jbr.stockId);
                        //try
                        //{
                        //    if (!string.IsNullOrEmpty(jbr.createDate))
                        //        asnEntity.CreateData = Convert.ToDateTime(jbr.createDate);
                        //    if (!string.IsNullOrEmpty(jbr.expDate))
                        //        asnEntity.ExpDate = Convert.ToDateTime(jbr.expDate);
                        //}
                        //catch (Exception msg)
                        //{
                        //    LogHelper.errorLog("FrmListPickPlan+GetPickPlan", msg);
                        //}
                        listPick.Add(asnEntity);
                    }
                    #endregion
                }

                if (temp.result[0].picktemperror != null)
                {
                    #region 赋值数据 缺货临时表
                    foreach (JsonGetTempPickResultPickErr err in temp.result[0].picktemperror)
                    {
                        PickPlanEntity asnEntity = new PickPlanEntity();
                        #region 0-10
                        asnEntity.BillID = Convert.ToInt32(err.billId);
                        asnEntity.BillNO = err.billNo;
                        asnEntity.ComMaterial = err.comMaterial;
                        asnEntity.CustomerName = err.customerName;
                        asnEntity.UnitName = err.unitName;
                        asnEntity.Qty = Convert.ToDecimal(err.qty);
                        asnEntity.DisableQty = Convert.ToDecimal(err.DisableQty);
                        asnEntity.DisableQty2 = Convert.ToDecimal(err.disableQty2);
                        asnEntity.Material = err.material;
                        asnEntity.MaterialName = err.materialName;
                        asnEntity.PickZnType = err.pickZnType;
                        asnEntity.StockQty = Convert.ToDecimal(err.stockQty);
                        asnEntity.SaleQty = Convert.ToInt32(err.saleQty);
                        asnEntity.SecurityQty = Convert.ToInt32(err.securityQty);
                        #endregion

                        //try
                        //{
                        //    if (!string.IsNullOrEmpty(jbr.createDate))
                        //        asnEntity.CreateData = Convert.ToDateTime(jbr.createDate);
                        //    if (!string.IsNullOrEmpty(jbr.expDate))
                        //        asnEntity.ExpDate = Convert.ToDateTime(jbr.expDate);
                        //}
                        //catch (Exception msg)
                        //{
                        //    LogHelper.errorLog("FrmListPickPlan+GetPickPlan", msg);
                        //}
                        listPickError.Add(asnEntity);
                    }
                    #endregion
                }
            }
            this._pickResult = listPick;
            this._pickResultError = listPickError;
            #endregion

            //读取临时保存的拣配结果
            bindingSource1.DataSource = this._pickResult;
            bindingSource2.DataSource = this._pickResultError;

            //if (!string.IsNullOrEmpty(errMsg))
            //{
            //    errMsg = FormatErrMsg(errMsg);
            //    memoEdit1.Text = errMsg;
            //}
        }
        /// <summary>
        /// 验证订单（对比生成的拣货计划和订单商品数量是否一致）
        /// </summary>
        /// <returns></returns>
        //private bool ValidateBills()
        //{
        //    bool result = false;
        //    try
        //    {
        //        string[] billArray = this._bills.Split(',');
        //        List<SODetailEntity> details = SOQueryDal.GetDetailsByBills(this._bills, 1);
        //        if (billArray == null || billArray.Length == 0)
        //        {
        //            result = true;
        //        }
        //        else
        //        {
        //            if (this._pickResult == null && this._pickResult.Count == 0 &&
        //                this._pickResultError == null && this._pickResultError.Count == 0)
        //            {
        //                result = false;
        //            }
        //            else
        //            {
        //                List<PickPlanEntity> pickPlanList = new List<PickPlanEntity>();
        //                List<SODetailEntity> detailList = new List<SODetailEntity>();
        //                foreach (string billIDStr in billArray)// 在每个订单中找商品数量
        //                {
        //                    if (string.IsNullOrEmpty(billIDStr)) continue;
        //                    int billID = ConvertUtil.ToInt(billIDStr);
        //                    detailList.AddRange(details.FindAll(u => u.BillID == billID && string.IsNullOrEmpty(u.CombMaterial)));
        //                    pickPlanList.AddRange(this._pickResult.FindAll(u => u.BillID == billID && string.IsNullOrEmpty(u.ComMaterial)));
        //                    pickPlanList.AddRange(this._pickResultError.FindAll(u => u.BillID == billID && string.IsNullOrEmpty(u.ComMaterial)));

        //                    IEnumerable<SODetailEntity> dList = detailList.AsEnumerable().Distinct(new SODetailEntity());
        //                    //IEnumerable<PickPlanEntity> pList = pickPlanList.AsEnumerable().Distinct(new PickPlanEntity());
        //                    if (dList.Count() != pickPlanList.Count)
        //                    {
        //                        result = false;
        //                        break;
        //                    }
        //                    else
        //                    {
        //                        result = true;
        //                    }
        //                    pickPlanList.Clear();
        //                    detailList.Clear();
        //                }
        //            }
        //        }
        //    }
        //    catch { result = false; }
        //    return result;
        //}

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

        /// <summary>
        /// 拣货计划 ： 判断货位是否被禁用
        /// </summary>
        /// <param name="skuStr"></param>
        /// <returns></returns>
        public bool QueryNoActiveLocBySku(string skuStr)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("skuStr=").Append(skuStr);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_QueryNoActiveLocBySku);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return false;
                }
                #endregion

                #region 正常错误处理

                JsonQueryNoActiveLocBySku bill = JsonConvert.DeserializeObject<JsonQueryNoActiveLocBySku>(jsonQuery);
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
                if (bill.result != null && bill.result.Length > 0)
                    return true;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return false;
        }

        /// <summary>
        /// 拣货计划 ： 排除缺货的订单，清空缺货信息
        /// </summary>
        /// <param name="billIds"></param>
        /// <returns></returns>
        public bool DeleteTempPickAll(string billIds)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("errorBillIds=").Append(billIds);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_DeleteTempPickAll);
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
        /// 转移特殊字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public  string DeleteStr(string str)
        {
            string dataStr = string.Empty;
            //if (str.Contains("%"))
            //  dataStr = str.Replace("%", "%25");
            if (str.Contains("&"))
                dataStr = str.Replace("&","");
            //if (str.Contains("@"))
            //dataStr = str.Replace("@", "%40");
            //if (str.Contains("#"))
            //    dataStr = str.Replace("#", "%23");

            if (string.IsNullOrEmpty(dataStr))
                dataStr = str;
            return dataStr;
        }

        /// <summary>
        /// 拣货计划 ： 保存拣配结果
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userName"></param>
        /// <param name="whType"></param>
        /// <returns></returns>
        public string SavePickPlan(List<PickPlanEntity> data, string userName, EWarehouseType whType)
        {
            #region    //取出不重复的单据编号
            var billids = (from d in data select d).Distinct();
            List<PickPlanEntity> list = new List<PickPlanEntity>();
            foreach (PickPlanEntity tm in data)
            {
                bool flag = false;
                foreach (PickPlanEntity temp in list)
                {
                    if (tm.BillID == temp.BillID)
                    {
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                    list.Add(tm);
            }
            #endregion

            List<PickPlanEntity> listDetails = new List<PickPlanEntity>();


            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("WarehouseType=").Append(EUtilStroreType.WarehouseTypeToInt(whType)).Append("&");
                loStr.Append("UserName=").Append(userName).Append("&");
                #region list 转 json
                List<string> prop = new List<string>() { "BillID", "BillNO" };
                string pickPlanEntity = GetResList<PickPlanEntity>("billids", list, prop);
                //loStr.Append("billids=").Append(pickPlanEntity).Append("&");
                #region  //组装details数据
                /*string jsons = string.Empty;
                foreach (PickPlanEntity ID in list)
                {
                   
                    var details = from d in data where d.BillID == ID.BillID && d.STOCK_ID != 0 select d;

                    foreach (PickPlanEntity detail in details)
                    {
                        listDetails.Add(detail);
                    }
                    List<string> prop1 = new List<string>() { "BillID", "DetailID", "STOCK_ID", "Qty", "IsCase", "BillNO", "MaterialName" };
                    jsons = GetResList<PickPlanEntity>(listDetails, prop1);
                    jsons = jsons.Insert(0, "{\"Group\":[");
                    jsons = jsons.Insert(jsons.Length, "]}");
                    jsons += ",";
                }
                jsons = jsons.Substring(0, jsons.Length - 1);

                jsons = jsons.Insert(0, "{\"details\":[");
                jsons = jsons.Insert(jsons.Length, "]}");*/
                #endregion
                List<string> prop1 = new List<string>() { "BillID", "DetailID", "STOCK_ID", "Qty", "IsCase", "BillNO", "Material" };
                string pickPlanEntity1 = GetResList<PickPlanEntity>("details", data, prop1);
                pickPlanEntity1 = pickPlanEntity1.Replace("Material", "MaterialName");
                string jsons = "{" + pickPlanEntity + "," + pickPlanEntity1 + "}";
                loStr.Append("pickJson=").Append(jsons);
                #endregion

                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SavePickPlan);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    //MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return WebWork.RESULT_NULL;
                }
                #endregion

                #region 正常错误处理

                JosnSavePickPlan bill = JsonConvert.DeserializeObject<JosnSavePickPlan>(jsonQuery);
                if (bill == null)
                {
                    // MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return WebWork.JSON_DATA_NULL;
                }
                if (bill.flag != 0)
                {
                    //MsgBox.Warn(bill.error);
                    return bill.error;
                }
                #endregion
                if (bill.result != null && bill.result.Length > 0)
                    return bill.result[0].result;
            }
            catch (Exception ex)
            {
                //MsgBox.Err(ex.Message);
                return ex.Message;
            }

            return null;
        }

        /// <summary>
        /// 拣货计划（获得值）
        /// </summary>
        /// <param name="item"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public  SettingEntity GetValue(string item, string group)
        {
            SettingEntity asnEntity = new SettingEntity();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("setItem=").Append(item);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetValue);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return asnEntity;
                }
                #endregion

                #region 正常错误处理

                JsonGetValue bill = JsonConvert.DeserializeObject<JsonGetValue>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return asnEntity;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return asnEntity;
                }
                #endregion
                List<SettingEntity> list = new List<SettingEntity>();

                #region 赋值数据
                foreach (JsonGetValueResult jbr in bill.result)
                {
                    asnEntity.ID = Convert.ToInt32(jbr.id);
                    asnEntity.Group = jbr.setGroup;
                    asnEntity.Item = jbr.setItem;
                    asnEntity.Remark = jbr.remark;
                    asnEntity.Value = jbr.setValue;

                    //try
                    //{
                    //    if (!string.IsNullOrEmpty(jbr.closeDate))
                    //        asnEntity.CloseDate = Convert.ToDateTime(jbr.closeDate);
                    //    if (!string.IsNullOrEmpty(jbr.createDate))
                    //        asnEntity.CreateDate = Convert.ToDateTime(jbr.createDate);
                    //}
                    //catch (Exception msg)
                    //{
                    //    LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    //}
                    list.Add(asnEntity);
                }
                return asnEntity;
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return asnEntity;
        }

        /// <summary>
        /// 拣货计划（删除拣货计划临时数据 ）
        /// </summary>
        /// <returns></returns>
        public bool DeletePickTemp()
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                string jsonQuery = WebWork.SendRequest(string.Empty, WebWork.URL_DeletePickTemp);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return false;
                }
                #endregion

                #region 正常错误处理

                JsonJudgeIsNext bill = JsonConvert.DeserializeObject<JsonJudgeIsNext>(jsonQuery);
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
                if (bill.result != null)
                    return bill.result[0].flag;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return false;
        }

        private void OnSaveClick(object sender, EventArgs e)
        {
            #region 
            SettingEntity settingEntity = GetValue("越库区补货", "补货设置");
            List<PickPlanEntity> errorData = this.bindingSource2.DataSource as List<PickPlanEntity>;
            if (errorData != null && errorData.Count > 0)
            {
                // 判断备货区是否有货
                List<PickPlanEntity> list = errorData.FindAll(
                    u => (u.DisableQty > 0 && u.PickZnType != "71" && u.PickZnType != "72") ||
                    (settingEntity != null && settingEntity.Value == "0" && u.DisableQty2 > 0 && u.PickZnType != "73"));

                string skuStr = StringUtil.JoinBySign<PickPlanEntity>(errorData, "Material");
                // 判断货位是否被禁用
                if (QueryNoActiveLocBySku(skuStr))
                {
                    DialogResult dialog = MsgBox.AskYes(string.Format(
                        "商品：{0} 包含有库存被禁用的货位，是否确认保存拣货计划？", skuStr));
                    if (dialog != DialogResult.Yes)
                        return;
                }
            }
            if (gridView2.RowCount > 0)
            {
                if (MsgBox.AskYes("部分订单库存不足，是否生成拣货计划，生成后不可修改。") == DialogResult.No)
                {
                    return;
                }
            }
            List<PickPlanEntity> data = bindingSource1.DataSource as List<PickPlanEntity>;
            data.AddRange(bindingSource2.DataSource as List<PickPlanEntity>);
            if (data != null)
            {
                try
                {
                    // 排除缺货的订单
                    if (this._pickResultError != null && this._pickResultError.Count > 0)
                    {
                        // 找到缺货订单ID，只有明细总库存大于销售单位才添加到缺货订单里
                        List<int> errorBillIds = new List<int>();
                        foreach (PickPlanEntity item in this._pickResultError)
                        {
                            if (errorBillIds.Contains(item.BillID))
                                continue;
                            if (item.DisableQty > item.SaleQty || item.DisableQty2 > item.SaleQty)
                                errorBillIds.Add(item.BillID);
                        }
                        List<PickPlanEntity> tempList = new List<PickPlanEntity>(this._pickResult);
                        foreach (PickPlanEntity item in tempList)
                        {
                            if (errorBillIds.Contains(item.BillID))
                                this._pickResult.Remove(item);
                        }
                        if (errorBillIds.Count > 0)
                        {
                            DeleteTempPickAll(StringUtil.JoinBySign<int>(errorBillIds, null));
                        }
                        // 清空缺货信息
                        this._pickResultError.Clear();

                    }
                    if (this._pickResult.Count == 0)
                    {
                        MsgBox.Warn("请对【缺货明细】中的商品进行补货后再操作！");
                        return;
                    }
                    string errMsg1 = SavePickPlan(data, GlobeSettings.LoginedUser.UserName, GlobeSettings.LoginedUser.WarehouseType);
                    if (!string.IsNullOrEmpty(errMsg1))
                    {
                        MsgBox.Warn(string.Format("保存失败，错误信息: {0}", errMsg1));
                    }
                    else
                    {
                        bool result = AutoAssignTask();
                        if (result)
                        {
                            MsgBox.OK("任务分派成功。");
                        }

                        if (DataChanged != null)
                            DataChanged(null, null);
                        DeletePickTemp();
                        this.DialogResult = DialogResult.OK;
                    }
                }
                catch (Exception ex)
                {
                    MsgBox.Err(ex.Message);
                }
            }
            #endregion

            #region 
            //SettingEntity settingEntity = SettingDal.GetValue("越库区补货", "补货设置");
            //List<PickPlanEntity> errorData = this.bindingSource2.DataSource as List<PickPlanEntity>;
            //if (errorData != null && errorData.Count > 0)
            //{
            //    // 判断备货区是否有货
            //    List<PickPlanEntity> list = errorData.FindAll(
            //        u => (u.DisableQty > 0 && u.PickZnType != "71" && u.PickZnType != "72") ||
            //        (settingEntity != null && settingEntity.Value == "0" && u.DisableQty2 > 0 && u.PickZnType != "73"));
            //    //if (list != null && list.Count > 0)
            //    //{
            //    //    StringBuilder sb = new StringBuilder();
            //    //    foreach (PickPlanEntity item in list)
            //    //    {
            //    //        sb.AppendFormat("{0},", item.BillNO);
            //    //    }
            //    //    MsgBox.Warn(string.Format(
            //    //        "订单编号：{0} 缺货商品在【备货区】或【越库区】有货，请执行补货操作后再生成拣货计划！",
            //    //        sb.ToString().TrimEnd(',')));
            //    //    return;
            //    //}
            //    string skuStr = StringUtil.JoinBySign<PickPlanEntity>(errorData, "Material");
            //    // 判断货位是否被禁用
            //    if (StockDal.QueryNoActiveLocBySku(skuStr) > 0)
            //    {
            //        DialogResult dialog = MsgBox.AskYes(string.Format(
            //            "商品：{0} 包含有库存被禁用的货位，是否确认保存拣货计划？", skuStr));
            //        if (dialog != DialogResult.Yes)
            //            return;
            //    }
            //}
            //if (gridView2.RowCount > 0)
            //{
            //    if (MsgBox.AskYes("部分订单库存不足，是否生成拣货计划，生成后不可修改。") == DialogResult.No)
            //    {
            //        return;
            //    }
            //}
            //// 验证订单订单实际商品数 与 生成拣货任务 的商品数是否能对应
            ////if (!this.ValidateBills())
            ////{
            ////    MsgBox.Err("部分商品未生成拣货计划，请备份数据库联系技术人员！");
            ////    return;
            ////}
            //List<PickPlanEntity> data = bindingSource1.DataSource as List<PickPlanEntity>;
            //data.AddRange(bindingSource2.DataSource as List<PickPlanEntity>);
            //if (data != null)
            //{
            //    try
            //    {
            //        // 排除缺货的订单
            //        if (this._pickResultError != null && this._pickResultError.Count > 0)
            //        {
            //            // 找到缺货订单ID，只有明细总库存大于销售单位才添加到缺货订单里
            //            List<int> errorBillIds = new List<int>();
            //            foreach (PickPlanEntity item in this._pickResultError)
            //            {
            //                if (errorBillIds.Contains(item.BillID))
            //                    continue;
            //                if (item.DisableQty > item.SaleQty || item.DisableQty2 > item.SaleQty)
            //                    errorBillIds.Add(item.BillID);
            //            }
            //            List<PickPlanEntity> tempList = new List<PickPlanEntity>(this._pickResult);
            //            foreach (PickPlanEntity item in tempList)
            //            {
            //                if (errorBillIds.Contains(item.BillID))
            //                    this._pickResult.Remove(item);
            //            }
            //            soDal.DeleteTempPickAll(StringUtil.JoinBySign<int>(errorBillIds, null));
            //            // 清空缺货信息
            //            this._pickResultError.Clear();
            //        }
            //        if (this._pickResult.Count == 0)
            //        {
            //            MsgBox.Warn("请对【缺货明细】中的商品进行补货后再操作！");
            //            return;
            //        }
            //        string errMsg1 = soDal.SavePickPlan(data, GlobeSettings.LoginedUser.UserName, GlobeSettings.LoginedUser.WarehouseType);
            //        if (!string.IsNullOrEmpty(errMsg1))
            //        {
            //            MsgBox.Warn(string.Format("保存失败，错误信息: {0}", errMsg1));
            //        }
            //        else
            //        {
            //            //if (MsgBox.AskOK("保存成功，是否现在执行自动分派任务。") == DialogResult.OK)
            //            //{
            //            string result = soDal.AutoAssignTask();
            //            if (result == "Y")
            //            {
            //                MsgBox.OK("任务分派成功。");
            //            }
            //            else if (result == "T")
            //            {
            //                MsgBox.Warn("拣货任务已生成,由于装车任务过慢,无法分配到人员，请尽快装车！");
            //            }
            //            else
            //            {
            //                MsgBox.Warn(result);
            //            }
            //            // }

            //            if (DataChanged != null)
            //                DataChanged(null, null);
            //            SODal.DeletePickTemp();
            //            this.DialogResult = DialogResult.OK;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MsgBox.Err(ex.Message);
            //    }
            //}
            #endregion
        }

        private string FormatErrMsg(string errorMsg)
        {
            return string.Format("-------- {0} --------\r\n{1}", DateTime.Now.ToShortTimeString(), errorMsg.Replace(";", "\r\n"));
        }

        #region Override Methods
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            // 采购退货注释
            //// 如果存在【采购退货单】库存不足的情况，对操作人员进行提示
            //List<SOHeaderEntity> list = ReturnDal.GetBillsByPickError();
            //if (list == null || list.Count == 0)
            //    return;
            //else
            //{
            //    StringBuilder bills = new StringBuilder();   // 用逗号连接订单编号
            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        bills.Append(list[i].BillNO);
            //        if (i < list.Count - 1)
            //            bills.Append(",");
            //    }
            //    MsgBox.Warn(string.Format("订单编号：{0} 商品还未从备货区转出，请先做货位移动！", bills));
            //}
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            DeletePickTemp();
        }
        #endregion
    }
}