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
using Nodes.Shares;
using Nodes.UI;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Instore;
using Nodes.Entities.HttpEntity.CycleCount;
using Newtonsoft.Json;
using DevExpress.Utils;

namespace Nodes.CycleCount
{
    public partial class FrmCountTask : DevExpress.XtraEditors.XtraForm
    {
        
        public FrmCountTask()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 盘点单管理---根据条件查询盘点单
        /// </summary>
        /// <param name="warehouse"></param>
        /// <param name="billNO"></param>
        /// <param name="billStatus"></param>
        /// <param name="showNotComplete"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public List<CountHeaderEntity> QueryBills(string warehouse, string billNO,
          string billStatus, bool showNotComplete, DateTime? dateFrom, DateTime? dateTo)
        {
            List<CountHeaderEntity> list = new List<CountHeaderEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("warehouse=").Append(warehouse).Append("&");
                loStr.Append("billNO=").Append(billNO).Append("&");
                loStr.Append("billStatus=").Append(billStatus).Append("&");
                loStr.Append("showNotComplete=").Append(showNotComplete).Append("&");
                loStr.Append("dateFrom=").Append(dateFrom).Append("&");
                loStr.Append("dateTo=").Append(dateTo);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_QueryBills_PanDian);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonQueryBillsPanDian bill = JsonConvert.DeserializeObject<JsonQueryBillsPanDian>(jsonQuery);
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
                foreach (JsonQueryBillsPanDianResult jbr in bill.result)
                {
                    CountHeaderEntity asnEntity = new CountHeaderEntity();
                    asnEntity.BillID = Convert.ToInt32(jbr.billId);
                    asnEntity.BillState = jbr.billState;
                    asnEntity.Creator = jbr.creator;
                    asnEntity.Remark = jbr.remark;
                    asnEntity.StateName = jbr.itemDesc;
                    asnEntity.TagDesc = jbr.tagDesc;
                    asnEntity.Warehouse = jbr.whCode;
                    try
                    {
                        //if (!string.IsNullOrEmpty(jbr.closeDate))
                        //    asnEntity.CloseDate = Convert.ToDateTime(jbr.closeDate);
                        //if (!string.IsNullOrEmpty(jbr.printedTime))
                        //    asnEntity.PrintedTime = Convert.ToDateTime(jbr.printedTime);
                        if (!string.IsNullOrEmpty(jbr.createDate))
                            asnEntity.CreateDate = Convert.ToDateTime(jbr.createDate);
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

        /// <summary>
        /// 盘点任务分派--根据角色查询人员
        /// </summary>
        /// <param name="warehouseCode"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public List<UserEntity> ListUsersByRoleAndWarehouseCodeForCount(string warehouseCode, string roleName)
        {
            List<UserEntity> list = new List<UserEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("warehouseCode=").Append(warehouseCode).Append("&");
                loStr.Append("roleName=").Append(roleName);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_ListUsersByRoleAndWarehouseCodeForCount);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonListUsersByRoleAndWarehouseCodeForCount bill = JsonConvert.DeserializeObject<JsonListUsersByRoleAndWarehouseCodeForCount>(jsonQuery);
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
                foreach (JsonListUsersByRoleAndWarehouseCodeForCountResult jbr in bill.result)
                {
                    UserEntity asnEntity = new UserEntity();
                    asnEntity.AllowEdit = jbr.allowEdit;
                    asnEntity.IsActive = jbr.isActive;
                    asnEntity.MobilePhone = jbr.mobilePhone;
                    asnEntity.Remark = jbr.remark;
                    asnEntity.UserCode = jbr.userCode;
                    asnEntity.UserName = jbr.userName;
                    asnEntity.UserPwd = jbr.pwd;
                    asnEntity.WarehouseCode = jbr.whCode;
                    asnEntity.WarehouseName = jbr.whName;
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

        private void OnFormLoad(object sender, EventArgs e)
        {
            try
            {
                List<CountHeaderEntity> counts = QueryBills(GlobeSettings.LoginedUser.WarehouseCode, null, null, true, null, null);
                lookUpEdit1.Properties.DataSource = counts;

                listBoxControl1.DataSource = ListUsersByRoleAndWarehouseCodeForCount(GlobeSettings.LoginedUser.WarehouseCode, "盘点员");
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 盘点任务分派---编辑查看
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public List<CountDetailEntity> GetCountLocations(int billID)
        {
            List<CountDetailEntity> list = new List<CountDetailEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetCountLocations);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetCountLocation bill = JsonConvert.DeserializeObject<JsonGetCountLocation>(jsonQuery);
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
                foreach (JsonGetCountLocationResult jbr in bill.result)
                {
                    CountDetailEntity asnEntity = new CountDetailEntity();
                    asnEntity.BillID = Convert.ToInt32(jbr.billId);
                    asnEntity.CellCode = jbr.cellCode;
                    asnEntity.FloorCode = jbr.floorCode;
                    asnEntity.ID = Convert.ToInt32(jbr.id);
                    asnEntity.Location = jbr.lcCode;
                    asnEntity.LocationState = jbr.lcState;
                    asnEntity.PassageCode = jbr.passageCode;
                    asnEntity.ShelfCode = jbr.shelfCode;
                    asnEntity.ZoneCode = jbr.znCode;
                    asnEntity.ZoneName = jbr.znName;
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

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            object obj = lookUpEdit1.EditValue;
            if (obj == null)
            {
                return;
            }

            try
            {
                int billID = ConvertUtil.ToInt(obj);
                gridControl1.DataSource = GetCountLocations(billID);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private List<CountDetailEntity> GetCheckedLocations()
        {
            List<CountDetailEntity> locations = new List<CountDetailEntity>();

            int[] rowIndexs = gridView1.GetSelectedRows();
            foreach (int i in rowIndexs)
            {
                if (gridView1.IsDataRow(i))
                {
                    CountDetailEntity loc = gridView1.GetRow(i) as CountDetailEntity;
                    locations.Add(loc);
                }
            }

            return locations;
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
        /// 盘点任务分派--保存任务分派
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="billID"></param>
        /// <param name="userCode"></param>
        /// <param name="locations"></param>
        /// <returns></returns>
        public bool SaveCountTask(string userName, int billID, string userCode, List<CountDetailEntity> locations)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("userName=").Append(userName).Append("&");
                loStr.Append("billId=").Append(billID).Append("&");
                loStr.Append("counts=").Append(locations.Count).Append("&");
                loStr.Append("userCode=").Append(userCode).Append("&");
                loStr.Append("wareHouseType=").Append(EUtilStroreType.WarehouseTypeToInt(GlobeSettings.LoginedUser.WarehouseType)).Append("&");
                List<string> prop = new List<string>() { "ID" };
                string jsonDetail = GetResList<CountDetailEntity>("jsonDetail", locations, prop);
                loStr.Append("jsonDetail=").Append(jsonDetail);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_SaveCountTask);
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //必须选中一个盘点员
            //if (listBoxControl1.SelectedValue == null)
            //{
            //    MsgBox.Warn("请选中一个盘点员。");
            //    return;
            //}

            string userCode = null;
            //至少要选中一个货位
            List<CountDetailEntity> locations = GetCheckedLocations();
            if (locations == null || locations.Count == 0)
            {
                MsgBox.Warn("请选中要分派的货位。");
                return;
            }


            //必须选中一个盘点员

            userCode = ConvertUtil.ToString(listBoxControl1.SelectedValue);
            int billID = ConvertUtil.ToInt(lookUpEdit1.EditValue);

            try
            {
                if (SaveCountTask(GlobeSettings.LoginedUser.UserName,
                    billID, userCode, locations))
                {
                    MsgBox.OK("任务分派成功。");
                    gridControl1.DataSource = GetCountLocations(billID);
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
    }
}