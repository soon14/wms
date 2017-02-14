using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Nodes.UI;
using System.Text;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Resources;
using Nodes.Utils;
using Nodes.Shares;
using System.Threading;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;
using Newtonsoft.Json;

namespace Nodes.Outstore
{
    public partial class FrmBackConfirm : DevExpress.XtraEditors.XtraForm
    {
        //private SODal soDal = null;
        //private VehicleDal vehicleDal = null;
        List<SOHeaderEntity> List = null;
        public FrmBackConfirm()
        {
            InitializeComponent();
        }

        private void OnFrmLoad(object sender, EventArgs e)
        {
            try
            {
                toolConfirm.ImageIndex = (int)AppResource.EIcons.eye;
                toolBookIn.ImageIndex = (int)AppResource.EIcons.save;
                toolRefresh.ImageIndex = (int)AppResource.EIcons.refresh;
                toolQueryHistory.ImageIndex = (int)AppResource.EIcons.search;
                //this.soDal = new SODal();
                //this.vehicleDal = new VehicleDal();

                LoadDataAndBindGrid();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
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
                    LoadDataAndBindGrid();
                    OnbtnQueryClick(null, null);
                    break;
                case "回款确认":
                    ConfirmAmount();
                    break;
                case "金额录入":
                    SaveBackAmount();
                    break;
                case "确认历史查询":
                    FrmBackConfirmReport frm = new FrmBackConfirmReport();
                    frm.ShowDialog();
                    break;
                case "标记延交订单":
                    MarkDelayedOrder();
                    LoadDataAndBindGrid();
                    OnbtnQueryClick(null, null);
                    break;

                default:
                    break;
            }
        }

        public void SaveBackAmount()
        {
            SOHeaderEntity header = GetFocusedBill();
            if (header == null)
            {
                MsgBox.Warn("请选中要录入的单据。");
                return;
            }

            FrmConfirmAmount frm = new FrmConfirmAmount(header);
            frm.VehicleNo = lstVehicle.Text.Trim();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                OnbtnQueryClick(null, null);
            }
        }

        public SOHeaderEntity GetFocusedBill()
        {
            return SelectedHeader;
        }

        SOHeaderEntity SelectedHeader
        {
            get
            {
                if (this.gridView1.FocusedRowHandle < 0)
                    return null;
                else
                    return gridView1.GetFocusedRow() as SOHeaderEntity;
            }
        }

        /// <summary>
        /// 装车信息--查询所有
        /// </summary>
        /// <returns></returns>
        public List<VehicleEntity> GetCarAll()
        {
            List<VehicleEntity> list = new List<VehicleEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                //loStr.Append("vhNo=").Append(vehicleNO);
                string jsons = string.Empty;
                string jsonQuery = WebWork.SendRequest(jsons, WebWork.URL_GetCarAll);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetCarAll bill = JsonConvert.DeserializeObject<JsonGetCarAll>(jsonQuery);
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
                foreach (JsonGetCarAllResult jbr in bill.result)
                {
                    VehicleEntity asnEntity = new VehicleEntity();
                    asnEntity.ID = Convert.ToInt32(jbr.id);
                    asnEntity.IsActive = jbr.isActive;
                    asnEntity.RouteCode = jbr.rtCode;
                    asnEntity.RouteName = jbr.rtName;
                    asnEntity.UserCode = jbr.userCode;
                    asnEntity.UserName = jbr.userName;
                    asnEntity.UserPhone = jbr.mobilePhone;
                    asnEntity.VehicleCode = jbr.vhCode;
                    asnEntity.VehicleNO = jbr.vhNo;
                    asnEntity.VehicleVolume = Convert.ToDecimal(jbr.vhVolume);
                    asnEntity.VhAttri = jbr.vhAttri;
                    asnEntity.VhType = jbr.vhType;
                    asnEntity.VhAttriStr = jbr.itemDesc;
                    asnEntity.VhTypeStr = jbr.typeDesc;
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

        public void LoadDataAndBindGrid()
        {
            try
            {
                List<VehicleEntity> list = GetCarAll();
                VehicleEntity itm = new VehicleEntity();
                itm.ID = -1;
                itm.VehicleNO = "ALL";
                list.Insert(0, itm);
                bindingSource1.DataSource = list;
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

        #endregion

        /// <summary>
        /// 回款确认－更新发货单的回款确认标记
        /// </summary>
        /// <param name="lstHeader"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public bool UpdateConfirmFlag(List<SOHeaderEntity> lstHeader, string loginName)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("loginName=").Append(loginName).Append("&");
                #region list 转 json
                List<string> prop = new List<string>() { "ReceiveAmount", "RealAmount", "CrnAmount", "OtherAmount", "BillID", "PaymentBy" };
                string soHeaderEntity = GetRes<SOHeaderEntity>(lstHeader, prop);
                #endregion
                loStr.Append("lstHeader=").Append(soHeaderEntity);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_UpdateConfirmFlag);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return false;
                }
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

        public void ConfirmAmount()
        {
            List<SOHeaderEntity> focusedBills = GetFocusedBills();
            if (focusedBills.Count == 0)
            {
                MsgBox.Warn("请选中要确认的单据。");
                return;
            }

            if (MsgBox.AskOK(string.Format("你选择了<" + focusedBills.Count.ToString() + ">个单据，确定要对选中的单据进行回款确认吗？")) != DialogResult.OK)
                return;
            foreach (SOHeaderEntity itm in focusedBills)
            {
                if (itm.RealAmount != itm.ReceiveAmount - itm.CrnAmount + itm.OtherAmount)
                {
                    MsgBox.Warn("单据<" + itm.BillNO + ">的<实收现金>不等于<应收金额>减掉<退货金额>加上<它项金额>。");
                    return;
                }
            }
            bool rtn = UpdateConfirmFlag(focusedBills, GlobeSettings.LoginedUser.UserName);
            if (rtn)
            {
                MsgBox.OK("确认成功。");
            }
            else
            {
                MsgBox.Warn("确认失败。");
            }
            OnbtnQueryClick(null, null);
        }

        /// <summary>
        /// 回款确认－根据车辆信息获取未做回款确认的出库单信息
        /// </summary>
        /// <param name="vehicleID"></param>
        /// <returns></returns>
        public List<SOHeaderEntity> GetVhicleHeadersByVehicleID(int vehicleID)
        {
            List<SOHeaderEntity> list = new List<SOHeaderEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("vehicleID=").Append(vehicleID);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetVhicleHeadersByVehicleID);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonGetVhicleHeadersByVehicleID bill = JsonConvert.DeserializeObject<JsonGetVhicleHeadersByVehicleID>(jsonQuery);
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
                foreach (JsonGetVhicleHeadersByVehicleIDResult jbr in bill.result)
                {
                    SOHeaderEntity entity = new SOHeaderEntity();
                    entity.Address = jbr.address;
                    entity.BillID = Convert.ToInt32(jbr.billId);
                    entity.BillNO = jbr.billNo;
                    entity.ConfirmFlag = Convert.ToInt32(jbr.confirmFlag);
                    entity.Consignee = jbr.contact;
                    entity.CrnAmount = Convert.ToDecimal(jbr.crnAmount);
                    entity.CustomerName = jbr.cName;
                    entity.OtherAmount = Convert.ToDecimal(jbr.otherAmount);
                    entity.OtherRemark = jbr.otherRemark;
                    entity.PaymentFlag = Convert.ToInt32(jbr.paymentFlag);
                    entity.RealAmount = Convert.ToDecimal(jbr.realAmount);
                    entity.ReceiveAmount = Convert.ToDecimal(jbr.receiveAmount);

                    #region
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.closeDate))
                            entity.CloseDate = Convert.ToDateTime(jbr.closeDate);

                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    }
                    #endregion

                    #region
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.createDate))
                            entity.CreateDate = Convert.ToDateTime(jbr.createDate);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    }
                    #endregion

                    list.Add(entity);
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

        private void OnbtnQueryClick(object sender, EventArgs e)
        {
            try
            {
                int vehicleID = ConvertUtil.ToInt(lstVehicle.EditValue);
                //if (vehicleID < 0)
                //{
                //    MsgBox.Warn("请选择车辆信息。");
                //    return;
                //}
                List = GetVhicleHeadersByVehicleID(vehicleID);
                gridControl1.DataSource = List;
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void gridView1_RowDoubleClick(object sender, EventArgs e)
        {
            SaveBackAmount();
        }

        public List<SOHeaderEntity> GetFocusedBills()
        {
            List<SOHeaderEntity> checkedBills = new List<SOHeaderEntity>();
            int[] focusedHandles = this.gridView1.GetSelectedRows();
            foreach (int handle in focusedHandles)
            {
                if (handle >= 0)
                    checkedBills.Add(gridView1.GetRow(handle) as SOHeaderEntity);
            }

            return checkedBills;
        }

        /// <summary>
        /// 回款确认－根据单据ID更新状态和标记
        /// </summary>
        /// <param name="billID"></param>
        /// <param name="creator"></param>
        /// <param name="billNO"></param>
        /// <returns></returns>
        public bool UpdateDelayedOrder(int billID, string creator, string billNO)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billID=").Append(billID).Append("&");
                loStr.Append("creator=").Append(creator).Append("&");
                loStr.Append("billNO=").Append(billNO);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_UpdateDelayedOrderd);
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

        public void MarkDelayedOrder()
        {
            SOHeaderEntity header = GetFocusedBill();
            List<SOHeaderEntity> focusedBills = GetFocusedBills();
            if (focusedBills.Count > 1)
            {
                MsgBox.Warn("每次只能标记一张延时单据！");
                return;
            }

            if (header == null)
            {
                MsgBox.Warn("请选中要标记的单据。");
                return;
            }
            if (focusedBills.Count == 1)
            {
                if (DialogResult.Yes == MsgBox.AskYes("确定将此单据标记为延时？"))
                {
                    bool result = UpdateDelayedOrder(header.BillID, GlobeSettings.LoginedUser.UserName,header.BillNO);
                    if (result)
                    {
                        MsgBox.Warn("标记成功!");
                        return;
                    }
                }
            }

        }
    }
}