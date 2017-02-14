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
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Instore;
using Newtonsoft.Json;

namespace Nodes.Instore
{
    public partial class FrmListCard : DevExpress.XtraEditors.XtraForm
    {
        private AsnQueryDal asnDal = new AsnQueryDal();

        public FrmListCard()
        {
            InitializeComponent();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            ImageCollection ic = AppResource.LoadToolImages();
            barManager1.Images = ic;
            barButtonItem1.ImageIndex = (int)AppResource.EIcons.refresh;
            barButtonItem2.ImageIndex = (int)AppResource.EIcons.delete;
            barButtonItem3.ImageIndex = (int)AppResource.EIcons.log;

            Reload();
        }

        /// <summary>
        /// 已经登记，但是收货未完成的数据
        /// </summary>
        /// <returns></returns>
        /// 
        public DataTable GetVehicles(int? billID, string billNO, string cardNO, string cardState)
        {
            #region
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("BILL_NO", Type.GetType("System.String"));
            tblDatas.Columns.Add("BILL_STATE_DESC", Type.GetType("System.String"));
            tblDatas.Columns.Add("CARD_NO", Type.GetType("System.String"));
            tblDatas.Columns.Add("CARD_STATE_DESC", Type.GetType("System.String"));
            tblDatas.Columns.Add("C_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("CONTACT", Type.GetType("System.String"));
            tblDatas.Columns.Add("CREATE_DATE", Type.GetType("System.String"));
            tblDatas.Columns.Add("CREATOR", Type.GetType("System.String"));
            tblDatas.Columns.Add("DRIVER", Type.GetType("System.String"));
            tblDatas.Columns.Add("VEHICLE_NO", Type.GetType("System.String"));
            tblDatas.Columns.Add("cardState", Type.GetType("System.String"));
            #endregion

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
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonVehicles bill = JsonConvert.DeserializeObject<JsonVehicles>(jsonQuery);
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
                foreach (JsonVehiclesResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["BILL_NO"] = tm.billNo;
                    newRow["BILL_STATE_DESC"] = tm.billStateDesc;
                    newRow["CARD_NO"] = tm.cardNo;
                    newRow["CARD_STATE_DESC"] = tm.cardStateDesc;
                    newRow["C_NAME"] = tm.cName;
                    newRow["CONTACT"] = tm.contact;
                    newRow["CREATE_DATE"] = tm.createDate;
                    newRow["CREATOR"] = tm.creator;
                    newRow["DRIVER"] = tm.driver;
                    newRow["VEHICLE_NO"] = tm.vehicleNo;
                    newRow["cardState"] = tm.cardState;
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
        //public List<JsonVehiclesEntity> GetVehicles(int? billID, string billNO, string cardNO, string cardState)
        //{
        //    try
        //    {
        //        #region 请求数据
        //        System.Text.StringBuilder loStr = new System.Text.StringBuilder();
        //        loStr.Append("cardState=").Append(cardState);
        //        string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetVehicles);
        //        if (string.IsNullOrEmpty(jsonQuery))
        //        {
        //            //MsgBox.Warn(WebWork.RESULT_NULL);
        //            return null;
        //        }
        //        #endregion

        //        #region 正常错误处理

        //        JsonVehicles bill = JsonConvert.DeserializeObject<JsonVehicles>(jsonQuery);
        //        if (bill == null)
        //        {
        //            MsgBox.Warn(WebWork.JSON_DATA_NULL);
        //            return null;
        //        }
        //        if (bill.flag != 0)
        //        {
        //            MsgBox.Warn(bill.error);
        //            return null;
        //        }
        //        #endregion
        //        List<JsonVehiclesEntity> jb = new List<JsonVehiclesEntity>();
        //        #region 赋值
        //        foreach (JsonVehiclesResult tm in bill.result)
        //        {
        //            JsonVehiclesEntity ve = new JsonVehiclesEntity();
        //            ve.BILL_NO = tm.billNo;
        //            ve.BILL_STATE_DESC = tm.billStateDesc;
        //            ve.CARD_NO = tm.cardNo;
        //            ve.cardState = tm.cardState;
        //            ve.CARD_STATE_DESC = tm.cardStateDesc;
        //            ve.C_NAME = tm.cName;
        //            ve.CONTACT = tm.contact;
        //            ve.CREATE_DATE = tm.createDate;
        //            ve.CREATOR = tm.creator;
        //            ve.DRIVER = tm.driver;
        //            ve.VEHICLE_NO = tm.vehicleNo;
        //            jb.Add(ve);
        //        }
        //        return jb;
        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        MsgBox.Err(ex.Message);
        //    }
        //    return null;
        //}


        private void Reload()
        {
            try
            {
                bindingSource1.DataSource = GetVehicles(null, null, null, null);
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
                case "取消登记":
                    DoCancel();
                    break;
                case "查看使用记录":
                    DoShowCardHistory();
                    break;
            }
        }

        private void DoShowCardHistory()
        {
            DataRow row = gridView1.GetFocusedDataRow() as DataRow; 
            if (row == null)
            {
                MsgBox.Warn("请选中要查看的行。");
                return;
            }

            string cardNO = ConvertUtil.ToString(row["CARD_NO"]);
            FrmCardHistory frmHistory = new FrmCardHistory(cardNO);
            frmHistory.ShowDialog();
        }

        /// <summary>
        /// 送货牌列表，取消登记
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="creator"></param>
        /// <returns></returns>
        public bool CancelVechile(string cardNO, string creator)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("cardNo=").Append(cardNO).Append("&");
                loStr.Append("creator=").Append(creator);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_CancelVechile);
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
            DataRow row = gridView1.GetFocusedDataRow() as DataRow;
            if (row == null)
            {
                MsgBox.Warn("请选中要取消的行。");
                return;
            }

            string cardNO = ConvertUtil.ToString(row["CARD_NO"]);
            string cardState = ConvertUtil.ToString(row["cardState"]);
            
            //先从界面上查看是否为空闲
            if (cardState == BaseCodeConstant.CARD_STATE_KONG_XIAN)
            {
                MsgBox.Warn("选中的送货牌为空闲状态，无需取消。");
                return;
            }

            if (MsgBox.AskOK(string.Format("确定要取消送货牌“{0}”的登记信息吗？", cardNO)) != DialogResult.OK)
                return;

            try
            {
                bool result = CancelVechile(cardNO, GlobeSettings.LoginedUser.UserName);
                if (result)
                {
                    Reload();
                    MsgBox.OK("取消成功。");
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
    }
}