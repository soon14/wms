using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Nodes.UI;
//using Nodes.DBHelper;
using Nodes.Entities;
using Nodes.Utils;
using Nodes.Shares;
using Nodes.Entities.HttpEntity;
using Nodes.Entities.HttpEntity.SystemManage;
using Newtonsoft.Json;

namespace Nodes.SystemManage
{
    public partial class FrmTaskChange : DevExpress.XtraEditors.XtraForm
    {
        //private TaskDal taskDal = new TaskDal();
        private string TaskType = "";
        private string UserCode = "";
        private List<TaskEntity> List = null;
        private TaskEntity taskEntity = null;

        public FrmTaskChange(string taskType, TaskEntity entity)
        {
            InitializeComponent();
            this.TaskType = taskType;
            this.taskEntity = entity;
        }

        public FrmTaskChange(string taskType, string userCode, List<TaskEntity> list)
        {
            InitializeComponent();
            this.TaskType = taskType;
            this.UserCode = userCode;
            this.List = list;
        }

        private void OnfrmLoad(object sender, EventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// 获取当前符合任务角色的所有用户
        /// </summary>
        /// <param name="rowName"></param>
        /// <returns></returns>
        public DataSet GetAllUsers(string roleName)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                loStr.Append("roleName=").Append(roleName);
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_GetAllUsers);
                if (string.IsNullOrEmpty(jsonQuery))
                {
                    MsgBox.Warn(WebWork.RESULT_NULL);
                    //LogHelper.InfoLog(WebWork.RESULT_NULL);
                    return null;
                }
                #endregion

                #region 正常错误处理

                JsonGetAllUsers bill = JsonConvert.DeserializeObject<JsonGetAllUsers>(jsonQuery);
                if (bill == null)
                {
                    MsgBox.Warn(WebWork.JSON_DATA_NULL);
                    return null;
                }
                if (bill.flag != 0)
                {
                    MsgBox.Warn(bill.error);
                    return null;
                }
                #endregion


                DataSet ds = new DataSet();

                #region 赋值
                foreach (JsonGetAllUsersResult tm in bill.result)
                {
                    #region d1-d2
                    #region d1
                    DataTable tblDatas = new DataTable("CONG");
                    tblDatas.Columns.Add("USER_CODE", Type.GetType("System.String"));
                    tblDatas.Columns.Add("USER_NAME", Type.GetType("System.String"));
                    tblDatas.Columns.Add("TASKCOUNT", Type.GetType("System.String"));
                    tblDatas.Columns.Add("ITEM_DESC", Type.GetType("System.String"));
                    #endregion
                    foreach (JsonGetAllUsersResultDt1 tmDt1 in tm.dt1)
                    {
                        DataRow newRow;
                        newRow = tblDatas.NewRow();
                        newRow["USER_CODE"] = tmDt1.userCode;
                        newRow["USER_NAME"] = tmDt1.userName;
                        newRow["TASKCOUNT"] = tmDt1.taskCount;
                        newRow["ITEM_DESC"] = tmDt1.itemDesc;
                        tblDatas.Rows.Add(newRow);
                    }

                    #region d2
                    DataTable d2 = new DataTable("ZHU");
                    d2.Columns.Add("USER_CODE", Type.GetType("System.String"));
                    d2.Columns.Add("USER_NAME", Type.GetType("System.String"));
                    d2.Columns.Add("QTY", Type.GetType("System.String"));
                    #endregion
                    foreach (JsonGetAllUsersResultDt2 tmDt2 in tm.dt2)
                    {
                        DataRow newRow2;
                        newRow2 = d2.NewRow();
                        newRow2["USER_CODE"] = tmDt2.userCode;
                        newRow2["USER_NAME"] = tmDt2.userName;
                        newRow2["QTY"] = tmDt2.qty;
                        d2.Rows.Add(newRow2);
                    }
                    #endregion

                    ds.Tables.Add(d2);
                    ds.Tables.Add(tblDatas);
                }
                return ds;
                #endregion
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }

            return null;
        }

        private void LoadData()
        {
            try
            {
                string roleName = "";
                switch (this.TaskType)
                { 
                    case "140":
                        roleName = "盘点员";
                        break;
                    case "141":
                        roleName = "移库员";
                        break;
                    case "142":
                        roleName = "上架员";
                        break;
                    case "143":
                        roleName = "拣货员";
                        break;
                    case "144":
                        roleName = "补货员";
                        break;
                }
                if (roleName != "")
                {
                    DataSet ds= GetAllUsers(roleName);
                    //DataRelation dr = new DataRelation("任务量", ds.Tables["ZHU"].Columns["USER_CODE"], ds.Tables["CONG"].Columns["USER_CODE"]);
                    //ds.Relations.Add(dr);
                    //gridControl1.DataSource = ds.Tables["ZHU"];
                    gridView2.ViewCaption = String.Format("任务描述：{0}         当前责任人：{1}", this.taskEntity.TaskQty, this.taskEntity.UserCode);
                    gridControl2.DataSource = ds.Tables["ZHU"];
                }
                else
                {
                    MsgBox.Warn("不存在该角色信息！");
                }
            }
            catch (Exception ex)
            {
                MsgBox.Err(ex.Message);
            }
        }

        private void OnItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (e.Item.Tag.ToString())
            { 
                case "保存":
                    Save();
                    break;
                case "全部展开":
                    gridView1.ExpandMasterRow(0);
                    break;
            }
        }


        /// <summary>
        /// 改变任务
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="dic"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool TaskChange(string userCode, Dictionary<string, int> dic, TaskEntity entity)
        {
            try
            {
                #region 请求数据
                System.Text.StringBuilder loStr = new System.Text.StringBuilder();
                #region
                loStr.Append("taskId=").Append(entity.TaskID).Append("&");
                loStr.Append("userCode=").Append(userCode).Append("&");
                loStr.Append("billId=").Append(entity.BillID).Append("&");
                loStr.Append("taskDesc=").Append(entity.TaskDesc).Append("&");
                loStr.Append("taskType=").Append(entity.TaskType).Append("&");
                string keyStr = string.Empty;
                foreach (string str in dic.Keys)
                {
                    keyStr += str;
                    keyStr += ",";
                }
                keyStr = keyStr.Substring(0, keyStr.Length - 1);

                loStr.Append("arrKey=").Append(keyStr).Append("&");
                string arrVal = string.Empty;
                foreach (string sql in dic.Keys)
                {
                    arrVal += dic[sql];
                    arrVal += ",";
                }
                arrVal = arrVal.Substring(0, arrVal.Length - 1);
                loStr.Append("arrVal=").Append(arrVal);
                #endregion
                string jsonQuery = WebWork.SendRequest(loStr.ToString(), WebWork.URL_TaskChange);
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

        private void Save()
        {
            if (gridView2.RowCount == 0)
            {
                return;
            }
            gridView2.FocusedRowHandle=-1;
            Dictionary<string, int> dic = new Dictionary<string, int>();
            int result = 0;
            for (int i = 0; i < gridView2.RowCount; i++)
            {
                string usercode = ConvertUtil.ToString(gridView2.GetRowCellValue(i, "USER_CODE"));
                if (gridView2.GetRowCellValue(i, "QTY").ToString() == "")
                {
                    continue;
                }
                int qty = ConvertUtil.ToInt(gridView2.GetRowCellValue(i, "QTY"));
                result += qty;
                dic[usercode] = qty;

            }
            if (this.taskEntity.Qty != result)
            {
                MsgBox.Warn("设置的任务数与原始任务数量不一致。");
            }
            else
            {
                bool resultMsg = TaskChange(GlobeSettings.LoginedUser.UserCode, dic, this.taskEntity);
                if (resultMsg)
                {
                    this.DialogResult = DialogResult.OK;
                }
                //string resultMsg = this.taskDal.TaskChange(GlobeSettings.LoginedUser.UserCode, dic, this.taskEntity);
                //if (resultMsg == "Y")
                //{
                //    this.DialogResult = DialogResult.OK;
                //}
                //else
                //{
                //    MsgBox.Warn(resultMsg);
                //}

            }
        }

        private void gridView1_GotFocus(object sender, EventArgs e)
        {

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }
    }
}