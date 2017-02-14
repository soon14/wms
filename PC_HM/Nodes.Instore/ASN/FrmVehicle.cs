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
using Nodes.Utils;
using Nodes.Shares;
using Nodes.Entities;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Instore;
using Newtonsoft.Json;

namespace Nodes.Instore
{
    public partial class FrmVehicle : DevExpress.XtraEditors.XtraForm
    {
        private AsnQueryDal asnQueryDal = null;
        private CallingDal callDal = new CallingDal();
        public FrmVehicle()
        {
            InitializeComponent();
        }

        private void frmLoad(object sender, EventArgs e)
        {
            asnQueryDal = new AsnQueryDal();
            BindingData();
        }

        /// <summary>
        /// 读取等待到货（也就是没有做到货登记）的单据
        /// </summary>
        /// <param name="warehouseCode"></param>
        /// <returns></returns>
        private List<AsnBodyEntity> QueryNotRelatedBills(string warehouseCode)
        {
            List<AsnBodyEntity> list = new List<AsnBodyEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billState=").Append(BillStateConst.ASN_STATE_CODE_NOT_ARRIVE).Append("&");
                loStr.Append("wareHouseCode=").Append(warehouseCode);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_QueryNotRelatedBills);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonBills bill = JsonConvert.DeserializeObject<JsonBills>(jsonQuery);
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
                foreach (JsonBillsResult jbr in bill.result)
                {
                    AsnBodyEntity asnEntity = new AsnBodyEntity();
                    asnEntity.OriginalBillNO = jbr.originalBillNo;
                    asnEntity.InstoreTypeDesc = jbr.instoreTypeDesc;
                    asnEntity.Creator = jbr.creator;
                    asnEntity.InstoreType = jbr.instoreType;
                    asnEntity.ContractNO = jbr.contractNo;
                    asnEntity.BillType = jbr.billType;
                    //nameS
                    asnEntity.RowForeColor = Convert.ToInt32(jbr.rowColor);
                    asnEntity.BillState = jbr.billState;
                    asnEntity.BillStateDesc = jbr.billStateDesc;
                    asnEntity.Remark = jbr.remark;

                    #region
                    try
                    {
                        if (!string.IsNullOrEmpty(jbr.closeDate))
                            asnEntity.CloseDate = Convert.ToDateTime(jbr.closeDate);
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
                        if (!string.IsNullOrEmpty(jbr.printedTime))
                            asnEntity.PrintedTime = Convert.ToDateTime(jbr.printedTime);
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
                            asnEntity.CreateDate = Convert.ToDateTime(jbr.createDate);
                    }
                    catch (Exception msg)
                    {
                        MsgBox.Warn(msg.Message);
                        //LogHelper.errorLog("FrmVehicle+QueryNotRelatedBills", msg);
                    }
                    #endregion

                    asnEntity.WmsRemark = jbr.wmsRemark;
                    asnEntity.Printed = Convert.ToInt32(jbr.printed);
                    //sCode
                    //asnEntity.SupplierCode = jbr.cName;
                    asnEntity.SupplierName = jbr.cName;
                    asnEntity.BillID = Convert.ToInt32(jbr.billId);
                    asnEntity.BillNO = jbr.billNo;
                    asnEntity.Sales = jbr.salesMan;
                    asnEntity.BillTypeDesc = jbr.billTypeDesc;
                    list.Add(asnEntity);
                }
                return list;
                #endregion
            }
            catch(Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return list;
        }

        /// <summary>
        /// 已经登记，但是收货未完成的数据
        /// </summary>
        /// <returns></returns>
        public List<JsonVehiclesEntity> GetVehicles(int? billID, string billNO, string cardNO, string cardState)
        {
            List<JsonVehiclesEntity> jb = new List<JsonVehiclesEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("cardState=").Append(cardState);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetVehicles);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return jb;
                }
                #endregion

                #region 正常错误处理

                JsonVehicles bill = JsonConvert.DeserializeObject<JsonVehicles>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return jb;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return jb;
                }
                #endregion
               
                #region 赋值
                foreach(JsonVehiclesResult tm in bill.result)
                {
                    JsonVehiclesEntity ve = new JsonVehiclesEntity();
                    ve.BILL_NO = tm.billNo;
                    ve.BILL_STATE_DESC = tm.billStateDesc;
                    ve.CARD_NO = tm.cardNo;
                    ve.cardState = tm.cardState;
                    ve.CARD_STATE_DESC = tm.cardStateDesc;
                    ve.C_NAME = tm.cName;
                    ve.CONTACT = tm.contact;
                    ve.CREATE_DATE = tm.createDate;
                    ve.CREATOR = tm.creator;
                    ve.DRIVER = tm.driver;
                    ve.VEHICLE_NO = tm.vehicleNo;
                    jb.Add(ve);
                }
                return jb;
                #endregion
            }
            catch(Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
            return jb;
        }

        /// <summary>
        /// 根据角色找人员
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        private List<UserEntity> GetUserByRole(string roleName)
        {
            List<UserEntity> list = new List<UserEntity>();
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("roleName=").Append(roleName);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetUserByRole);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return list;
                }
                #endregion

                #region 正常错误处理

                JsonUserEntity bill = JsonConvert.DeserializeObject<JsonUserEntity>(jsonQuery);
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
                foreach (JsonUserEntityResult jbr in bill.result)
                {
                    UserEntity user = new UserEntity();
                    user.UserName = jbr.userName;
                    user.UserCode = jbr.userCode;
                    list.Add(user);
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

        private void BindingData()
        {
            try
            {
                #region  读取等待到货（也就是没有做到货登记）的单据
                listBills.Properties.DataSource = QueryNotRelatedBills(GlobeSettings.LoginedUser.WarehouseCode);
                #endregion

                #region 显示已经登记，但是收货未完成的数据
                bindingSource1.DataSource = GetVehicles(null, null, null, BaseCodeConstant.CARD_STATE_ZAI_YONG);
                #endregion


                #region 指定人员分配任务
                listReceive.Properties.DataSource = GetUserByRole(BaseCodeConstant.ROLE_RECEIVE);
                listCheck.Properties.DataSource = GetUserByRole(BaseCodeConstant.ROLE_CHECK);
                listPut.Properties.DataSource = GetUserByRole(BaseCodeConstant.ROLE_PUT);
                #endregion

                ////读取等待到货（也就是没有做到货登记）的单据
                //listBills.Properties.DataSource = asnQueryDal.QueryNotRelatedBills(GlobeSettings.LoginedUser.WarehouseCode);

                ////显示已经登记，但是收货未完成的数据
                //bindingSource1.DataSource = asnQueryDal.GetVehicles(null, null, null, BaseCodeConstant.CARD_STATE_ZAI_YONG);

                //listReceive.Properties.DataSource = AsnQueryDal.GetUserByRole(BaseCodeConstant.ROLE_RECEIVE);
                //listCheck.Properties.DataSource = AsnQueryDal.GetUserByRole(BaseCodeConstant.ROLE_CHECK);
                //listPut.Properties.DataSource = AsnQueryDal.GetUserByRole(BaseCodeConstant.ROLE_PUT);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        /// <summary>
        /// 到货登记，生成清点、复核、上架任务
        /// </summary>
        /// <param name="billlNO"></param>
        /// <param name="userQD"></param>
        /// <param name="userCheck"></param>
        /// <param name="userPutaway"></param>
        /// <param name="cardNO"></param>
        /// <param name="creator"></param>
        /// <returns></returns>
        private bool CreateAsnPlan(int billlNO, string userQD, string userCheck, string userPutaway,string cardNO, string creator)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billlNO).Append("&");
                loStr.Append("userQD=").Append(userQD).Append("&");
                loStr.Append("userCheck=").Append(userCheck).Append("&");
                loStr.Append("userPutaway=").Append(userPutaway).Append("&");
                loStr.Append("cardNo=").Append(cardNO).Append("&");
                loStr.Append("creator=").Append(creator);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_CreateAsnPlan);
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
        /// 到货登记,绑定送货牌与入库单
        /// </summary>
        /// <param name="billNO"></param>
        /// <param name="cardNO"></param>
        /// <param name="driver"></param>
        /// <param name="contact"></param>
        /// <param name="vehicleNO"></param>
        /// <param name="creator"></param>
        /// <param name="userQd"></param>
        /// <param name="userCheck"></param>
        /// <param name="userPutaway"></param>
        /// <param name="descriptinQd"></param>
        /// <param name="descriptionCheck"></param>
        /// <param name="descriptionPutaway"></param>
        /// <returns></returns>
        private bool  CreateVechile(int billNO, string cardNO, string driver, string contact, string vehicleNO,
            string creator,string userQd, string userCheck,string userPutaway,string descriptinQd,
            string descriptionCheck, string descriptionPutaway)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billId=").Append(billNO).Append("&");
                loStr.Append("cardNo=").Append(cardNO).Append("&");

                loStr.Append("driver=").Append(driver).Append("&");
                loStr.Append("contact=").Append(contact).Append("&");
                loStr.Append("vehicleNo=").Append(vehicleNO).Append("&");

                loStr.Append("creator=").Append(creator).Append("&");
                loStr.Append("userQD=").Append(userQd).Append("&");
                loStr.Append("userCheck=").Append(userCheck).Append("&");
                loStr.Append("userPutaway=").Append(userPutaway).Append("&");
                loStr.Append("descriptionQD=").Append(descriptinQd).Append("&");
                loStr.Append("descriptionCheck=").Append(descriptionCheck).Append("&");
                loStr.Append("descriptionPutaway=").Append(descriptionPutaway);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_CreateVechile);
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
        /// 查询送货牌状态
        /// </summary>
        /// <param name="carNo"></param>
        /// <returns></returns>
        private bool CarNoIsExit(string carNo)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("carNo=").Append(carNo);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_CarNoIsExit);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return false;
                }
                #endregion

                #region 正常错误处理
                if (jsonQuery.Contains("\"cardState\":\"41\""))
                {
                    MsgBox.Warn("该送货牌正在使用，请使用其他送货牌！");
                    return false;
                }
                else if (jsonQuery.Contains("\"result\":[],\"flag\":0,"))
                {
                    MsgBox.Warn("送货牌不存在，请确认送货牌！！！");
                    return false;
                }
                else if (jsonQuery.Contains("\"cardState\":\"40\""))
                    return true;
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return false;
        }

        private void btnSave(object sender, EventArgs e)
        {
            #region 为空判断
            if (listBills.EditValue == null)
            {
                MsgBox.Warn("请选择入库单！");
                return;
            }

            if (txtBarcode.Text.Trim() == "")
            {
                MsgBox.Warn("请扫描送货牌条码！");
                return;
            }

            if (String.IsNullOrEmpty(listReceive.Text.Trim()))
            {
                MsgBox.Warn("请选择收货人员！");
                return;
            }

            if (String.IsNullOrEmpty(listCheck.Text.Trim()))
            {
                MsgBox.Warn("请选择复核人员！");
                return;
            }
            if (String.IsNullOrEmpty(listPut.Text.Trim()))
            {
                MsgBox.Warn("请选择上架人员！");
                return;
            }
            #endregion

            if (!CarNoIsExit(txtBarcode.Text.Trim()))
                return;

            bool msg = CreateAsnPlan(ConvertUtil.ToInt(listBills.EditValue), listReceive.EditValue.ToString(), listCheck.EditValue.ToString(), listPut.EditValue.ToString(), txtBarcode.Text.Trim(), GlobeSettings.LoginedUser.UserCode);
            if (!msg)
            {
                return;
            }
            else
            {
                MsgBox.Warn("收货任务生成！");
            }
            bool result = CreateVechile(ConvertUtil.ToInt(listBills.EditValue), txtBarcode.Text.Trim(),
                txtDeriver.Text.Trim(), txtContactPhone.Text.Trim(),
                txtVehicleNo.Text.Trim(), GlobeSettings.LoginedUser.UserName, ConvertUtil.ToString(listReceive.EditValue), ConvertUtil.ToString(listCheck.EditValue), ConvertUtil.ToString(listPut.EditValue)
                , listReceive.Text, listCheck.Text, listPut.Text);
            if (result)
            {
                BindingData();

                listBills.EditValue = listReceive.EditValue = listCheck.EditValue = listPut.EditValue = null;
                txtBarcode.Text = txtContactPhone.Text = txtDeriver.Text = txtVehicleNo.Text = "";

                splashLabel1.Show(true);
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            BindingData();
        }

        private void listBills_ProcessNewValue(object sender, DevExpress.XtraEditors.Controls.ProcessNewValueEventArgs e)
        {
            txtBarcode.SelectAll();
            txtBarcode.Focus();
        }
    }
}