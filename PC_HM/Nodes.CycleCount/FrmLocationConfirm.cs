using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using DevExpress.XtraEditors;
using Nodes.UI;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Shares;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Instore;
using Newtonsoft.Json;

namespace Nodes.CycleCount
{
    public partial class FrmLocationConfirm : XtraForm
    {
        List<LocationEntity> Locations;
        string remark;

        public FrmLocationConfirm(List<LocationEntity> locations, string remark)
        {
            InitializeComponent();
            this.Locations = locations;
            this.remark = remark;
        }

        /// <summary>
        /// 收货单据管理， baseCode信息查询(用于业务类型和单据状态筛选条件)
        /// 获取活动状态的集合
        /// </summary>
        /// <param name="groupCode"></param>
        /// <returns></returns>
        public  List<BaseCodeEntity> GetItemList(string groupCode)
        {
            List<BaseCodeEntity> list = new List<BaseCodeEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("groupCode=").Append(groupCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetItemList);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonBaseCodeInfo bill = JsonConvert.DeserializeObject<JsonBaseCodeInfo>(jsonQuery);
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
                foreach (JsonBaseCodeInfoResult jbr in bill.result)
                {
                    BaseCodeEntity asnEntity = new BaseCodeEntity();
                    asnEntity.GroupCode = jbr.groupCode;
                    asnEntity.IsActive = jbr.isActive;
                    asnEntity.ItemDesc = jbr.itemDesc;
                    asnEntity.ItemValue = jbr.itemValue;
                    asnEntity.Remark = jbr.remark;
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
            gridControl1.DataSource = this.Locations;
            try
            {
                this.cboCountTag.Properties.DataSource = GetItemList("123");
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
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
            string ret = "{\"" + josnName + "\"" + strb.ToString() + "}";
            //ret = ret.Insert(0, "{");
            //ret = ret.Insert(ret.Length, "}");
            return ret;
        }

        #endregion

        /// <summary>
        /// 创建盘点单---保存盘点单
        /// </summary>
        /// <param name="remark"></param>
        /// <param name="userName"></param>
        /// <param name="warehouse"></param>
        /// <param name="locations"></param>
        /// <param name="tagCode"></param>
        /// <returns></returns>
        public bool SaveCountBill(string remark, string userName, string warehouse, List<LocationEntity> locations, string tagCode)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("remark=").Append(remark).Append("&");
                loStr.Append("userName=").Append(userName).Append("&");
                loStr.Append("warehouse=").Append(warehouse).Append("&");
                loStr.Append("tagCode=").Append(tagCode).Append("&");
                List<string> prop = new List<string>() { "LocationCode" };
                string jsonStr = GetResList<LocationEntity>("jsonStr", locations, prop);
                loStr.Append("jsonStr=").Append(jsonStr);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveCountBill);
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

        private void OnSaveClick(object sender, EventArgs e)
        {
            try
            {
                BaseCodeEntity countTag = this.cboCountTag.EditValue as BaseCodeEntity;
                if (countTag == null)
                {
                    MsgBox.Warn("请选择盘点标签！");
                    return;
                }

                List<LocationEntity> locations = gridControl1.DataSource as List<LocationEntity>;
                if (locations == null)
                {
                    locations = new List<LocationEntity>();
                }

                bool billID = SaveCountBill(remark,
                    GlobeSettings.LoginedUser.UserName,
                    GlobeSettings.LoginedUser.WarehouseCode,
                    locations, 
                    countTag.ItemValue);
                if (billID)
                    MsgBox.OK("盘点单保存成功");  
                //MsgBox.OK(string.Format("盘点单保存成功，单号为“{0}”。", billID));

                ////成功后，需要分派任务
                //string result = TaskDal.Schedule(billID, "140");
                //if (result == "Y")
                //{
                //    this.DialogResult = DialogResult.OK;
                //}
                //else
                //{
                //    MsgBox.Warn(result);
                //}
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        

        private void checkEdit_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit checkEdit = sender as CheckEdit;
            if (checkEdit == null || !checkEdit.Checked || this.Locations == null || this.Locations.Count == 0)
                return;
            List<LocationEntity> list = null;
            switch (checkEdit.Text)
            {
                case "全部":
                    list = this.Locations;
                    break;
                case "日期差异":
                    list = this.Locations.FindAll((item) => { return item.ExpDate != item.ExpDateStock; });
                    break;
                case "数量差异":
                    list = this.Locations.FindAll((item) => { return item.CountQty != item.StockQty; });
                    break;
            }
            gridControl1.DataSource = list;
        }
    }
}
