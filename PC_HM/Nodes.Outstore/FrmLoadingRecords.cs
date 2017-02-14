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
    /// 装车记录查询
    /// </summary>
    public partial class FrmLoadingRecords : DevExpress.XtraEditors.XtraForm
    {
        #region 构造函数

        public FrmLoadingRecords()
        {
            InitializeComponent();
        }

        #endregion

        /// <summary>
        /// 装车信息--装车记录查询
        /// </summary>
        /// <param name="billNo"></param>
        /// <returns></returns>
        public DataTable GetLoadingRecords(string billNo)
        {
            #region
            DataTable tblDatas = new DataTable("Datas");
            tblDatas.Columns.Add("装车编号", Type.GetType("System.String"));
            tblDatas.Columns.Add("车牌号", Type.GetType("System.String"));
            tblDatas.Columns.Add("订单编号", Type.GetType("System.String"));
            tblDatas.Columns.Add("装车顺序", Type.GetType("System.Int32"));
            tblDatas.Columns.Add("创建人员", Type.GetType("System.String"));
            tblDatas.Columns.Add("装车人员", Type.GetType("System.String"));
            tblDatas.Columns.Add("装车时间", Type.GetType("System.String"));
            #endregion
            
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("billNo=").Append(billNo);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetLoadingRecords);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return tblDatas;
                }
                #endregion

                #region 正常错误处理

                JsonGetLoadingRecords bill = JsonConvert.DeserializeObject<JsonGetLoadingRecords>(jsonQuery);
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
                foreach (JsonGetLoadingRecordsResult tm in bill.result)
                {
                    DataRow newRow;
                    newRow = tblDatas.NewRow();
                    newRow["装车编号"] = tm.trainNo;
                    newRow["车牌号"] = tm.vhNo;
                    newRow["订单编号"] = tm.billNo;
                    newRow["装车顺序"] = Convert.ToInt32(tm.vhSort);
                    newRow["创建人员"] = tm.userName;
                    newRow["装车人员"] = tm.groupName;
                    newRow["装车时间"] = tm.updateDate;
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
        private void Query()
        {
            try
            {
                string billNo = this.itemBillNo.EditValue as string;
                if (billNo == null)
                    return;
                this.gridControl1.DataSource = GetLoadingRecords(billNo);
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }
        #endregion

        #region 事件

        private void item_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string tag = e.Item.Tag as string;
            if (tag == null) return;
            switch (tag)
            {
                case "查询":
                    this.Query();
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
