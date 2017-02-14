using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using Nodes.DBHelper;
using Nodes.UI;
using Nodes.Utils;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.Outstore;
using Newtonsoft.Json;

namespace Nodes.Outstore
{
    /// <summary>
    /// 地图订单排序查询
    /// </summary>
    public partial class FrmSOSortMapQuery : DevExpress.XtraEditors.XtraForm
    {
        #region 构造函数

        public FrmSOSortMapQuery()
        {
            InitializeComponent();
        }

        #endregion

        /// <summary>
        /// 订单排序查询
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public  DataTable Query(DateTime beginDate, DateTime endDate)
        {
            #region
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("VEHICLE_NO", Type.GetType("System.String"));
            tblDatas.Columns.Add("BILL_NO", Type.GetType("System.String"));
            tblDatas.Columns.Add("IN_VEHICLE_SORT", Type.GetType("System.String"));
            tblDatas.Columns.Add("PIECES_QTY", Type.GetType("System.String"));
            tblDatas.Columns.Add("SORT_DATE", Type.GetType("System.DateTime"));
            tblDatas.Columns.Add("CREATE_DATE", Type.GetType("System.DateTime"));
            tblDatas.Columns.Add("C_NAME", Type.GetType("System.String"));
            tblDatas.Columns.Add("ADDRESS", Type.GetType("System.String"));
            #endregion

            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("beginDate=").Append(beginDate).Append("&");
                loStr.Append("endDate=").Append(endDate);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_QuerySort);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonQuerySort bill = JsonConvert.DeserializeObject<JsonQuerySort>(jsonQuery);
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
                foreach (JsonQuerySortResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["VEHICLE_NO"] = tm.vehicleNo;
                    newRow["BILL_NO"] = tm.billNo;
                    newRow["IN_VEHICLE_SORT"] = tm.inVehicleSort;
                    newRow["PIECES_QTY"] = tm.piecesQty;
                    if(!string.IsNullOrEmpty(tm.sortDate))
                        newRow["SORT_DATE"] = Convert.ToDateTime(tm.sortDate);
                    if(!string.IsNullOrEmpty(tm.sortDate))
                        newRow["CREATE_DATE"] = Convert.ToDateTime(tm.createDate);
                    newRow["C_NAME"] = tm.cName;
                    newRow["ADDRESS"] = tm.address;
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

        #region 方法
        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            DateTime beginDate = ConvertUtil.ToDatetime(this.toolSortBeginDate.EditValue);
            DateTime endDate = ConvertUtil.ToDatetime(this.toolSortEndDate.EditValue).AddDays(1);
            this.gridControl1.DataSource = Query(beginDate.Date, endDate.Date);
            this.gridControl1.RefreshDataSource();
        }
        #endregion

        #region Override Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.toolSortBeginDate.EditValue = DateTime.Now.AddDays(-1);
            this.toolSortEndDate.EditValue = DateTime.Now;
            this.LoadData();
        }
        #endregion

        #region 事件
        /// <summary>
        /// 查询
        /// </summary>
        private void toolQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.LoadData();
        }
        #endregion
    }
}
