using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.UI;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;
using Newtonsoft.Json;

namespace Nodes.Common
{
    /// <summary>
    /// 选择车辆
    /// </summary>
    public partial class FrmChooseVehicle : DevExpress.XtraEditors.XtraForm
    {
        #region 变量
        //private VehicleDal vehicleDal = new VehicleDal();
        private int _vehicleID = -1;
        #endregion

        #region 构造函数

        public FrmChooseVehicle()
        {
            InitializeComponent();
        }
        public FrmChooseVehicle(int vehicleID)
            : this()
        {
            this._vehicleID = vehicleID;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 当前选择的车辆
        /// </summary>
        public VehicleEntity SelectedVehicle
        {
            get
            {
                return this.searchLookUpEdit1.EditValue as VehicleEntity;
            }
        }
        #endregion

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


        #region Override Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            try
            {
                List<VehicleEntity> list = GetCarAll();
                this.bindingSource1.DataSource = list;
                this.searchLookUpEdit1.EditValue = list.Find(new Predicate<VehicleEntity>((item) => { return item.ID == this._vehicleID; }));
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
        #endregion

        #region 事件
        /// <summary>
        /// 取消
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 确认
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.SelectedVehicle == null)
            {
                MsgBox.Warn("请选择车辆!");
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        #endregion
    }
}
