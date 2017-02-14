using System;
using System.Data;
using System.Collections.Generic;
//using Nodes.DBHelper;
using Nodes.Utils;
using Nodes.UI;
using Nodes.Entities;
using DevExpress.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;
using Nodes.Entities.HttpEntity.Reports;
using Newtonsoft.Json;

namespace Reports
{
    public partial class FrmLoadRecords : DevExpress.XtraEditors.XtraForm
    {
        //private SODal soDal = new SODal();
        public FrmLoadRecords()
        {
            InitializeComponent();
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            try
            {
                this.dateStart.DateTime = DateTime.Now.AddDays(1 - DateTime.Now.Day);
                this.dateOver.DateTime = DateTime.Now;

                Reload();
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
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

        public void Reload()
        {
            try 
            {
                bindingSource1.DataSource = GetCarAll();
            }
            catch
            { }
        }

        /// <summary>
        /// 获得选中数据
        /// </summary>
        VehicleEntity SelectedUnitRow
        {
            get
            {
                if (gridView2.FocusedRowHandle < 0)
                    return null;

                return gridView2.GetFocusedRow() as VehicleEntity;
            }
        }

        /// <summary>
        /// 查询统计（装车记录查询-查询指定车辆的装车记录）
        /// </summary>
        /// <param name="whCode"></param>
        /// <param name="dateBegin"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public DataTable GetLoadRecordsByWhCode(string whCode, DateTime dateBegin, DateTime dateEnd)
        {
            #region
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("BILL_NO", Type.GetType("System.String"));
            tblDatas.Columns.Add("C_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("CREATE_DATE", Type.GetType("System.DateTime"));
            tblDatas.Columns.Add("LAST_UPDATETIME", Type.GetType("System.DateTime"));
            tblDatas.Columns.Add("QTY", Type.GetType("System.Decimal"));
            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("vhCode=").Append(whCode).Append("&");
                loStr.Append("beginDate=").Append(dateBegin).Append("&");
                loStr.Append("endDate=").Append(dateEnd);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetLoadRecordsByWhCode);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetLoadRecordsByWhCode bill = JsonConvert.DeserializeObject<JsonGetLoadRecordsByWhCode>(jsonQuery);
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
                foreach (JsonGetLoadRecordsByWhCodeResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["BILL_NO"] = tm.billNo;
                    newRow["C_NAME"] = tm.cName;
                    if(!string.IsNullOrEmpty(tm.createDate))
                        newRow["CREATE_DATE"] = Convert.ToDateTime(tm.createDate);
                    if (!string.IsNullOrEmpty(tm.lastUpdateTime))
                        newRow["LAST_UPDATETIME"] = Convert.ToDateTime(tm.lastUpdateTime);
                    newRow["QTY"] = Convert.ToDecimal(tm.qty);
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

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (SelectedUnitRow == null)
            {
                MsgBox.Warn("请选择一行进行查询。");
                return;
            }
            using (WaitDialogForm frm = new WaitDialogForm("查询中...", "请稍等"))
            {
                DataTable data = GetLoadRecordsByWhCode(
                    SelectedUnitRow.VehicleCode, this.dateStart.DateTime, this.dateOver.DateTime);
                gridView1.ViewCaption = string.Format("车辆：{0} 单据信息", SelectedUnitRow.VehicleNO);
                gridControl1.DataSource = data;
            }
        }
    }
}